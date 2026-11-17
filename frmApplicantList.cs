using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http; 
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmApplicantList : Form
    {
        private readonly List<DecryptedApplicant> decryptedApplicants = new List<DecryptedApplicant>();

        public frmApplicantList()
        {
            InitializeComponent();
            ConfigureApplicantGrid();
        }

        private void ConfigureApplicantGrid()
        {
            dgApplicants.AutoGenerateColumns = false;
            AddGridColumn("Relationship", "Relationship", 105);
            AddGridColumn("First name", "FirstName", 100);
            AddGridColumn("Last name", "LastName", 100);
            AddGridColumn("Alias", "Alias", 100);
            AddGridColumn("Date of birth", "DateOfBirth", 95);
            AddGridColumn("Application type", "ApplicationType", 105);
            AddGridColumn("Membership fee", "MembershipFee", 105);
            AddGridColumn("PFGA number", "PfgaNumber", 95);
            AddGridColumn("Address", "Address", 150);
            AddGridColumn("City", "City", 100);
            AddGridColumn("Province", "Province", 85);
            AddGridColumn("Postal", "Postal", 75);
            AddGridColumn("Home phone", "HomePhone", 105);
            AddGridColumn("Cell phone", "CellPhone", 105);
            AddGridColumn("Email", "Email", 180);
            AddGridColumn("PAL type", "PalType", 80);
            AddGridColumn("PAL date", "PalDate", 80);
            AddGridColumn("PAL number", "PalNumber", 95);
            AddGridColumn("PAL expiry", "PalExpiry", 95);
            AddGridColumn("Disciplines", "Disciplines", 180);
            AddGridColumn("Clubs", "Clubs", 220);
            AddGridColumn("Courses", "Courses", 220);
            AddGridColumn("Extra", "Extra", 55);
            AddGridColumn("Family count", "FamilyCount", 85);
            AddGridColumn("Terms", "Terms", 60);
            AddGridColumn("Photo", "PhotoName", 180);
            dgApplicants.CellFormatting += dgApplicants_CellFormatting;
        }

        private void AddGridColumn(string headerText, string propertyName, int width)
        {
            dgApplicants.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = headerText,
                DataPropertyName = propertyName,
                Name = propertyName,
                Width = width,
                SortMode = DataGridViewColumnSortMode.NotSortable
            });
        }

        private async void frmApplicantList_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "Loading applicants...";
            System.Diagnostics.Debug.WriteLine("Loading applicants...");
            System.Diagnostics.Trace.WriteLine("Loading applicants...");
            try
            {
                String apiRoot = System.Configuration.ConfigurationManager.AppSettings["API_ROOT"];
                String apiUser = System.Configuration.ConfigurationManager.AppSettings["API_USER"];
                String apiPassword = System.Configuration.ConfigurationManager.AppSettings["API_PASSWORD"];
                

                Uri apiUri;
                if (String.IsNullOrWhiteSpace(apiRoot) ||
                    !Uri.TryCreate(apiRoot, UriKind.Absolute, out apiUri) ||
                    (apiUri.Scheme != Uri.UriSchemeHttp && apiUri.Scheme != Uri.UriSchemeHttps))
                {
                    MessageBox.Show("The API is not configured correctly.", "API connection error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (String.IsNullOrWhiteSpace(apiUser) || String.IsNullOrWhiteSpace(apiPassword))
                {
                    MessageBox.Show("The API credentials are not configured.", "API connection error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (var client = new HttpClient())
                {
                    client.BaseAddress = apiUri;
                    client.Timeout = TimeSpan.FromSeconds(30);

                    
                    var token = await client.PostAsync("api/login", new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        { "username", apiUser },
                        { "password", apiPassword }
                    }));

                    if (!token.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Unable to sign in to the API ({(int)token.StatusCode} {token.ReasonPhrase}).",
                            "API connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    var tokenResponse = await token.Content.ReadAsStringAsync();
                    var tokenData = DeserializeJson<TokenResponse>(tokenResponse);
                    var accessToken = tokenData == null ? null : tokenData.Token;
                    if (String.IsNullOrWhiteSpace(accessToken))
                    {
                        MessageBox.Show("The API returned an invalid sign-in response.", "API connection error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

                    System.Diagnostics.Debug.WriteLine($"Access Token: {accessToken}");

                    var response = await client.GetAsync("api/applicants");
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Unable to retrieve applicants ({(int)response.StatusCode} {response.ReasonPhrase}).",
                            "API connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    System.Diagnostics.Debug.WriteLine($"Response: {jsonResponse}");
                    System.Diagnostics.Trace.WriteLine($"Applicant response received ({jsonResponse.Length} characters).");
                    var applicants = DeserializeJson<List<String>>(jsonResponse);
                    if (applicants == null)
                    {
                        MessageBox.Show("The API returned an invalid applicants response.", "API connection error",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    var applicantRows = new List<ApplicantResponse>();
                    var applicantDisplayRows = new List<ApplicantDisplayRow>();
                     decryptedApplicants.Clear();
                    foreach (var applicant in applicants)
                    {
                        var decryptedResponse = DecryptLaravelValue(applicant);
                        System.Diagnostics.Debug.WriteLine($"Decrypted applicant data: {decryptedResponse}");
                        ApplicantResponse responseJson = DeserializeJson<ApplicantResponse>(decryptedResponse);
                        var decriptedApplicant = DecryptLaravelValue(responseJson.Data);
                        System.Diagnostics.Debug.WriteLine($"Decrypted applicant data: {decriptedApplicant}");
                        System.Diagnostics.Trace.WriteLine($"Decrypted applicant {responseJson.Id}.");

                        var decryptedApplicant = DeserializeJson<DecryptedApplicant>(decriptedApplicant);
                        applicantRows.Add(responseJson);
                         if (decryptedApplicant != null)
                         {
                             decryptedApplicant.ResponseId = responseJson.Id;
                             decryptedApplicants.Add(decryptedApplicant);
                         }

                        var displayRows = CreateDisplayRows(decryptedApplicant);
                        if (displayRows.Count > 0)
                        {
                            applicantDisplayRows.AddRange(displayRows);
                        }
                    }

                    dgApplicants.DataSource = applicantDisplayRows;
                    lblStatus.Text = applicantRows.Count == 0
                        ? "No applicants were returned."
                        : String.Format("{0} applicant{1} loaded.", applicantRows.Count,
                            applicantRows.Count == 1 ? String.Empty : "s");
                }
            }

            catch (TaskCanceledException)
            {
                lblStatus.Text = "Loading timed out.";
                MessageBox.Show("The API request timed out.", "API connection error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (HttpRequestException)
            {
                lblStatus.Text = "The API could not be reached.";
                MessageBox.Show("The API could not be reached. Check the API address and network connection.",
                    "API connection error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (SerializationException)
            {
                lblStatus.Text = "The API returned an unexpected response.";
                MessageBox.Show("The API returned an unexpected response.", "API connection error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception)
            {
                lblStatus.Text = "An unexpected error occurred while loading applicants.";
                MessageBox.Show("An unexpected error occurred while connecting to the API.", "API connection error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private static List<ApplicantDisplayRow> CreateDisplayRows(DecryptedApplicant applicant)
        {
            var rows = new List<ApplicantDisplayRow>();
            if (applicant == null)
            {
                return rows;
            }

            rows.Add(new ApplicantDisplayRow
            {
                Relationship = "Applicant",
                ApplicationType = applicant.ApplicationType,
                MembershipFee = applicant.MembershipFee,
                FirstName = applicant.FirstName,
                LastName = applicant.LastName,
                Alias = applicant.Alias,
                DateOfBirth = applicant.DateOfBirth,
                PfgaNumber = applicant.PfgaNumber,
                Address = applicant.Address,
                City = applicant.City,
                Province = applicant.Province,
                Postal = applicant.Postal,
                HomePhone = applicant.HomePhone,
                CellPhone = applicant.CellPhone,
                Email = applicant.Email,
                PalType = applicant.PalType,
                PalDate = applicant.PalDate,
                PalNumber = applicant.PalNumber,
                PalExpiry = applicant.PalExpiry,
                Disciplines = JoinValues(applicant.Disciplines),
                Clubs = FormatClubs(applicant.Clubs),
                Courses = FormatCourses(applicant.Courses),
                Extra = applicant.Extra.HasValue ? applicant.Extra.Value.ToString() : String.Empty,
                FamilyCount = applicant.FamilyCount.HasValue ? applicant.FamilyCount.Value.ToString() : String.Empty,
                Terms = applicant.Terms.HasValue ? applicant.Terms.Value.ToString() : String.Empty,
                PhotoName = applicant.Photo == null ? String.Empty : applicant.Photo.Name
            });

            if (applicant.Family != null)
            {
                foreach (var familyMember in applicant.Family)
                {
                    if (familyMember == null)
                    {
                        continue;
                    }

                    rows.Add(new ApplicantDisplayRow
                    {
                        Relationship = "  Family member",
                        FirstName = familyMember.FirstName,
                        LastName = familyMember.LastName,
                        DateOfBirth = familyMember.DateOfBirth,
                        PalNumber = familyMember.PalNumber,
                        PalExpiry = familyMember.PalExpiry,
                        PhotoName = familyMember.Photo == null ? String.Empty : familyMember.Photo.Name,
                        IsFamilyMember = true
                    });
                }
            }

            return rows;
        }

        private static string JoinValues(List<string> values)
        {
            return values == null ? String.Empty : String.Join(", ", values);
        }

        private static string FormatClubs(List<Club> clubs)
        {
            if (clubs == null)
            {
                return String.Empty;
            }

            var values = new List<string>();
            foreach (var club in clubs)
            {
                if (club != null)
                {
                    values.Add(String.Format("{0} ({1}, {2}-{3})", club.Name, club.City, club.From, club.To));
                }
            }

            return String.Join("; ", values);
        }

        private static string JoinAddress(DecryptedApplicant applicant)
        {
            return String.Join(", ", new[] { applicant.Address, applicant.City, applicant.Province, applicant.Postal }
                .Where(value => !String.IsNullOrWhiteSpace(value)));
        }

        private static string FormatCourseLines(List<Course> courses)
        {
            if (courses == null)
            {
                return String.Empty;
            }

            return String.Join(Environment.NewLine, courses.Where(course => course != null)
                .Select(course => String.Join(", ", new[] { course.Description, course.Trainer, course.Location, course.Date }
                    .Where(value => !String.IsNullOrWhiteSpace(value)))));
        }

        private static string FormatClubLines(List<Club> clubs)
        {
            if (clubs == null)
            {
                return String.Empty;
            }

            return String.Join(Environment.NewLine, clubs.Where(club => club != null)
                .Select(club => String.Join(", ", new[] { club.Name, club.City, club.From, club.To }
                    .Where(value => !String.IsNullOrWhiteSpace(value)))));
        }

        private static DateTime ParseDate(string value)
        {
            DateTime parsedDate;
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate)
                ? parsedDate
                : DateTime.Parse("1900-01-01");
        }

        private static double ParsePal(string value)
        {
            double pal;
            return Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out pal) ? pal : 0;
        }

        private static ulong GetSectionFlag(List<string> disciplines)
        {
            ulong mask = 0;
            if (disciplines == null)
            {
                return mask;
            }

            foreach (var discipline in disciplines)
            {
                if (String.IsNullOrWhiteSpace(discipline))
                {
                    continue;
                }

                string value = discipline.Trim().ToLowerInvariant();
                if (value.Contains("archery")) mask |= (ulong)BitField.Flag.f1;
                else if (value.Contains("handgun") || value.Contains("hand gun")) mask |= (ulong)BitField.Flag.f2;
                else if (value.Contains("smallbore") || value.Contains("small bore")) mask |= (ulong)BitField.Flag.f3;
                else if (value.Contains("sca")) mask |= (ulong)BitField.Flag.f4;
                else if (value.Contains("rifle")) mask |= (ulong)BitField.Flag.f5;
                else if (value.Contains("action")) mask |= (ulong)BitField.Flag.f6;
            }

            return mask;
        }

        private static byte[] GetPhotoData(Photo photo)
        {
            if (photo == null || String.IsNullOrWhiteSpace(photo.Data))
            {
                return new byte[0];
            }

            string data = photo.Data;
            int separator = data.IndexOf(',');
            if (data.StartsWith("data:", StringComparison.OrdinalIgnoreCase) && separator >= 0)
            {
                data = data.Substring(separator + 1);
            }

            try
            {
                return Convert.FromBase64String(data);
            }
            catch (FormatException)
            {
                return new byte[0];
            }
        }

        private static int GetPendingMembershipTypeId()
        {
            MembershipTableAdapters.MembershipTypeTableAdapter adapter =
                new MembershipTableAdapters.MembershipTypeTableAdapter();
            Membership.MembershipTypeDataTable types = new Membership.MembershipTypeDataTable();
            adapter.Fill(types);

            foreach (Membership.MembershipTypeRow row in types.Rows)
            {
                if (String.Equals(row.Membership_Type, "Pending", StringComparison.OrdinalIgnoreCase))
                {
                    return row.MembershipTypeID;
                }
            }

            return 0;
        }

        private static clsMembership FindMemberByCard(string pfgaNumber)
        {
            int card;
            if (!Int32.TryParse(pfgaNumber, out card))
            {
                return null;
            }

            MembershipTableAdapters.MembersTableAdapter adapter =
                new MembershipTableAdapters.MembersTableAdapter();
            Membership.MembersDataTable members = new Membership.MembersDataTable();
            adapter.Fill(members);

            foreach (Membership.MembersRow row in members.Rows)
            {
                if (row.Card == card)
                {
                    return new clsMembership(row.ID, -1);
                }
            }

            return null;
        }

        private static List<CheckupCard> GetCurrentYearCards()
        {
            DateTime today = DateTime.Today;
            int currentYear = today.Month >= 8 ? today.Year : today.Year - 1;
            MembershipTableAdapters.MembersTableAdapter membersAdapter =
                new MembershipTableAdapters.MembersTableAdapter();
            Membership.MembersDataTable members = new Membership.MembersDataTable();
            membersAdapter.Fill(members);

            Dictionary<int, int> cardsByMemberId = new Dictionary<int, int>();
            foreach (Membership.MembersRow member in members.Rows)
            {
                if (!member.IsCardNull())
                {
                    cardsByMemberId[member.ID] = member.Card;
                }
            }

            MembershipTableAdapters.PaidTableAdapter paidAdapter =
                new MembershipTableAdapters.PaidTableAdapter();
            Membership.PaidDataTable paid = new Membership.PaidDataTable();
            paidAdapter.Fill(paid);

            return paid.Rows.Cast<Membership.PaidRow>()
                .Where(row => row.YearPaid == currentYear && cardsByMemberId.ContainsKey(row.MemberID))
                .Select(row => new CheckupCard
                {
                    CardNumber = cardsByMemberId[row.MemberID],
                    YearPaid = row.YearPaid
                })
                .GroupBy(card => new { card.CardNumber, card.YearPaid })
                .Select(group => group.First())
                .OrderBy(card => card.CardNumber)
                .ToList();
        }

        private static HashSet<string> GetExistingMemberKeys()
        {
            HashSet<string> memberKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            MembershipTableAdapters.MembersTableAdapter adapter =
                new MembershipTableAdapters.MembersTableAdapter();
            Membership.MembersDataTable members = new Membership.MembersDataTable();
            adapter.Fill(members);

            foreach (Membership.MembersRow row in members.Rows)
            {
                string key = GetMemberKey(row.First_Name, row.Last_Name, row.Email_Address);
                if (!String.IsNullOrEmpty(key))
                {
                    memberKeys.Add(key);
                }
            }

            return memberKeys;
        }

        private static string GetMemberKey(string firstName, string lastName, string email)
        {
            if (String.IsNullOrWhiteSpace(firstName) || String.IsNullOrWhiteSpace(lastName) ||
                String.IsNullOrWhiteSpace(email))
            {
                return String.Empty;
            }

            return String.Join("\u001f", firstName.Trim(), lastName.Trim(), email.Trim());
        }

        private static clsMembership CreateMember(DecryptedApplicant applicant, int pendingMembershipTypeId, int card)
        {
            clsMembership member = new clsMembership(-1, -1);
            member.Card = card;
            ApplyApplicant(member, applicant, pendingMembershipTypeId, false);
            return member;
        }

        private static void ApplyApplicant(clsMembership member, DecryptedApplicant applicant, int pendingMembershipTypeId,
            bool preserveExisting)
        {
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.LastName)) member.LastName = applicant.LastName;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.FirstName)) member.FirstName = applicant.FirstName;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.Alias)) member.Alias = applicant.Alias;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.DateOfBirth)) member.BirthDate = ParseDate(applicant.DateOfBirth);
            if (!preserveExisting || HasAddressValue(applicant)) member.Address = JoinAddress(applicant);
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.HomePhone)) member.Phone = applicant.HomePhone;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.CellPhone)) member.Cell = applicant.CellPhone;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.Email)) member.Email = applicant.Email;
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.PalNumber)) member.Pal = ParsePal(applicant.PalNumber);
            if (!preserveExisting || !String.IsNullOrWhiteSpace(applicant.PalExpiry)) member.PalExpDate = ParseDate(applicant.PalExpiry);
            member.MemberTypeID = pendingMembershipTypeId;
            member.MembershipType = "Pending";
            member.JoinedDate = DateTime.Today;
            if (!preserveExisting || applicant.Disciplines != null) member.SectionFlag = GetSectionFlag(applicant.Disciplines);
            if (!preserveExisting || applicant.Courses != null || applicant.Clubs != null)
            {
                member.Notes = String.Join(Environment.NewLine, new[] { FormatCourseLines(applicant.Courses), FormatClubLines(applicant.Clubs) }
                    .Where(value => !String.IsNullOrWhiteSpace(value)));
            }

            if (!preserveExisting || applicant.Photo != null && !String.IsNullOrWhiteSpace(applicant.Photo.Data))
            {
                member.BadgeImage = GetPhotoData(applicant.Photo);
            }
        }

        private static bool HasAddressValue(DecryptedApplicant applicant)
        {
            return !String.IsNullOrWhiteSpace(applicant.Address) ||
                !String.IsNullOrWhiteSpace(applicant.City) ||
                !String.IsNullOrWhiteSpace(applicant.Province) ||
                !String.IsNullOrWhiteSpace(applicant.Postal);
        }

        private static clsMembership CreateFamilyMember(FamilyMember familyMember, int pendingMembershipTypeId, int card)
        {
            clsMembership member = new clsMembership(-1, -1)
            {
                Card = card,
                LastName = familyMember.LastName,
                FirstName = familyMember.FirstName,
                BirthDate = ParseDate(familyMember.DateOfBirth),
                Pal = ParsePal(familyMember.PalNumber),
                PalExpDate = ParseDate(familyMember.PalExpiry),
                MemberTypeID = pendingMembershipTypeId,
                MembershipType = "Pending",
                JoinedDate = DateTime.Today,
                BadgeImage = GetPhotoData(familyMember.Photo)
            };

            return member;
        }

        private static int SaveFamilyMembers(DecryptedApplicant applicant, int parentId,
            int pendingMembershipTypeId, ref int nextCard)
        {
            if (applicant.Family == null)
            {
                return 0;
            }

            if (nextCard < 0)
            {
                nextCard = new clsMembership(-1, -1).Card;
            }

            int savedCount = 0;
            foreach (var familyMember in applicant.Family)
            {
                if (familyMember == null)
                {
                    continue;
                }

                clsMembership member = CreateFamilyMember(familyMember, pendingMembershipTypeId, nextCard++);
                member.MasterRecord = parentId;
                member.Save();
                if (member.ID <= 0)
                {
                    WriteDiagnostic(String.Format("Family member {0} {1} could not be saved.",
                        member.FirstName, member.LastName));
                    continue;
                }

                InsertPaidRecord(member.ID);
                DebugMember("Family member", member);
                DebugPaidRecord(member.ID);
                savedCount++;
            }

            return savedCount;
        }

        private static string FormatCourses(List<Course> courses)
        {
            if (courses == null)
            {
                return String.Empty;
            }

            var values = new List<string>();
            foreach (var course in courses)
            {
                if (course != null)
                {
                    values.Add(String.Format("{0} ({1}, {2}, {3})", course.Description, course.Trainer,
                        course.Location, course.Date));
                }
            }

            return String.Join("; ", values);
        }

        private sealed class ApplicantDisplayRow
        {
            public string Relationship { get; set; }
            public string ApplicationType { get; set; }
            public string MembershipFee { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Alias { get; set; }
            public string DateOfBirth { get; set; }
            public string PfgaNumber { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string Province { get; set; }
            public string Postal { get; set; }
            public string HomePhone { get; set; }
            public string CellPhone { get; set; }
            public string Email { get; set; }
            public string PalType { get; set; }
            public string PalDate { get; set; }
            public string PalNumber { get; set; }
            public string PalExpiry { get; set; }
            public string Disciplines { get; set; }
            public string Clubs { get; set; }
            public string Courses { get; set; }
            public string Extra { get; set; }
            public string FamilyCount { get; set; }
            public string Terms { get; set; }
            public string PhotoName { get; set; }
            public bool IsFamilyMember { get; set; }
        }

        private static string DecryptLaravelValue(string encryptedValue)
        {
            var appKey = System.Configuration.ConfigurationManager.AppSettings["ENCRYPTION_KEY"];
            if (String.IsNullOrWhiteSpace(appKey))
            {
                throw new InvalidOperationException("The Laravel encryption key is not configured.");
            }

            try
            {
                // Laravel encrypts a JSON payload, then Base64-encodes the JSON.
                byte[] payloadBytes = Convert.FromBase64String(encryptedValue);
                var payload = DeserializeJson<LaravelEncryptedPayload>(
                    Encoding.UTF8.GetString(payloadBytes));

                if (payload == null || String.IsNullOrWhiteSpace(payload.Iv) ||
                    String.IsNullOrWhiteSpace(payload.Value))
                {
                    throw new FormatException("The Laravel encrypted payload is missing the IV or value.");
                }

                byte[] key = DecodeLaravelKey(appKey);
                byte[] iv = Convert.FromBase64String(payload.Iv);
                byte[] cipherText = Convert.FromBase64String(payload.Value);

                if (String.IsNullOrWhiteSpace(payload.Mac))
                {
                    throw new FormatException("The Laravel encrypted payload is missing the MAC.");
                }

                if (iv.Length != 16)
                {
                    throw new FormatException("The Laravel IV must be 16 bytes.");
                }

                if (cipherText.Length == 0 || cipherText.Length % 16 != 0)
                {
                    throw new FormatException("The Laravel ciphertext is not a valid AES block sequence.");
                }

                using (var hmac = new HMACSHA256(key))
                {
                    byte[] expectedMac = hmac.ComputeHash(
                        Encoding.UTF8.GetBytes(payload.Iv + payload.Value));
                    byte[] actualMac = DecodeLaravelMac(payload.Mac);

                    if (!FixedTimeEquals(expectedMac, actualMac))
                    {
                        throw new CryptographicException(
                            "The Laravel payload MAC is invalid. Verify that ENCRYPTION_KEY matches the Laravel APP_KEY.");
                    }
                }

                // Decrypt using AES-256-CBC
                byte[] decryptedBytes = DecryptAes256Cbc(cipherText, key, iv);

                // Convert to string and deserialize JSON
                string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
                return NormalizeLaravelPlaintext(decryptedString);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to decrypt Laravel encrypted data: " + ex.Message);
            }
        }

        private static string NormalizeLaravelPlaintext(string plaintext)
        {
            if (String.IsNullOrWhiteSpace(plaintext) || plaintext[0] != 's')
            {
                return plaintext;
            }

            int firstColon = plaintext.IndexOf(':');
            int secondColon = plaintext.IndexOf(':', firstColon + 1);
            if (firstColon != 1 || secondColon <= firstColon + 1 ||
                secondColon + 1 >= plaintext.Length ||
                plaintext[secondColon + 1] != '"')
            {
                return plaintext;
            }

            if (!Int32.TryParse(plaintext.Substring(firstColon + 1, secondColon - firstColon - 1),
                out int byteLength))
            {
                return plaintext;
            }

            int contentStart = secondColon + 2;
            if (byteLength < 0 || contentStart > plaintext.Length)
            {
                throw new FormatException("The Laravel serialized string length is invalid.");
            }

            byte[] serializedBytes = Encoding.UTF8.GetBytes(plaintext.Substring(contentStart));
            if (byteLength > serializedBytes.Length)
            {
                throw new FormatException("The Laravel serialized string is truncated.");
            }

            string value = Encoding.UTF8.GetString(serializedBytes, 0, byteLength);
            int suffixStart = contentStart + byteLength;
            if (suffixStart + 2 > plaintext.Length ||
                plaintext[suffixStart] != '"' || plaintext[suffixStart + 1] != ';')
            {
                throw new FormatException("The Laravel serialized string terminator is invalid.");
            }

            return value;
        }

        private static byte[] DecodeLaravelMac(string mac)
        {
            if (String.IsNullOrWhiteSpace(mac) || mac.Length % 2 != 0)
            {
                throw new FormatException("The Laravel MAC is not valid hexadecimal text.");
            }

            byte[] result = new byte[mac.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                int high = HexValue(mac[i * 2]);
                int low = HexValue(mac[i * 2 + 1]);
                if (high < 0 || low < 0)
                {
                    throw new FormatException("The Laravel MAC is not valid hexadecimal text.");
                }

                result[i] = (byte)((high << 4) | low);
            }

            return result;
        }

        private static int HexValue(char value)
        {
            if (value >= '0' && value <= '9')
            {
                return value - '0';
            }

            if (value >= 'a' && value <= 'f')
            {
                return value - 'a' + 10;
            }

            if (value >= 'A' && value <= 'F')
            {
                return value - 'A' + 10;
            }

            return -1;
        }

        private static byte[] DecodeLaravelKey(string appKey)
        {
            string keyValue = appKey.StartsWith("base64:", StringComparison.OrdinalIgnoreCase)
                ? appKey.Substring("base64:".Length)
                : appKey;

            byte[] key;
            try
            {
                key = Convert.FromBase64String(keyValue);
            }
            catch (FormatException)
            {
                key = Encoding.UTF8.GetBytes(keyValue);
            }

            if (key.Length != 32)
            {
                throw new CryptographicException(
                    "The Laravel APP_KEY must decode to exactly 32 bytes for AES-256-CBC; it currently contains " +
                    key.Length + " bytes.");
            }

            return key;
        }

        private static bool FixedTimeEquals(byte[] left, byte[] right)
        {
            if (left == null || right == null || left.Length != right.Length)
            {
                return false;
            }

            int difference = 0;
            for (int i = 0; i < left.Length; i++)
            {
                difference |= left[i] ^ right[i];
            }

            return difference == 0;
        }

        private static byte[] DecryptAes256Cbc(byte[] cipherText, byte[] key, byte[] iv)
        {
            using (Aes aes = Aes.Create())
            {
                aes.KeySize = 256;
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    return decryptor.TransformFinalBlock(cipherText, 0, cipherText.Length);
                }
            }
        }

        private static T DeserializeJson<T>(string json)
        {
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                return (T)serializer.ReadObject(stream);
            }
        }

        private static string SerializeJson<T>(T value)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                serializer.WriteObject(stream, value);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        private static async Task<bool> NotifyUpdatedApplicantsAsync(List<int> memberIds)
        {
            String apiRoot = System.Configuration.ConfigurationManager.AppSettings["API_ROOT"];
            String apiUser = System.Configuration.ConfigurationManager.AppSettings["API_USER"];
            String apiPassword = System.Configuration.ConfigurationManager.AppSettings["API_PASSWORD"];
            Uri apiUri;

            if (String.IsNullOrWhiteSpace(apiRoot) ||
                !Uri.TryCreate(apiRoot, UriKind.Absolute, out apiUri) ||
                (apiUri.Scheme != Uri.UriSchemeHttp && apiUri.Scheme != Uri.UriSchemeHttps) ||
                String.IsNullOrWhiteSpace(apiUser) || String.IsNullOrWhiteSpace(apiPassword))
            {
                return false;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = apiUri;
                client.Timeout = TimeSpan.FromSeconds(30);

                var tokenResponse = await client.PostAsync("api/login", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "username", apiUser },
                    { "password", apiPassword }
                }));

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    return false;
                }

                var tokenData = DeserializeJson<TokenResponse>(await tokenResponse.Content.ReadAsStringAsync());
                if (tokenData == null || String.IsNullOrWhiteSpace(tokenData.Token))
                {
                    return false;
                }

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

                string requestJson = SerializeJson(new ApplicantIdRequest { Ids = memberIds });
                using (var content = new StringContent(requestJson,
                    Encoding.UTF8, "application/json"))
                {
                    System.Diagnostics.Debug.WriteLine($"Notify updated applicants JSON: {requestJson}");
                    var response = await client.PostAsync("api/applicants", content);
                    
                    return response.IsSuccessStatusCode;
                }
            }
        }

        private static async Task<bool> NotifyCheckupAsync(List<CheckupCard> cards)
        {
            String apiRoot = System.Configuration.ConfigurationManager.AppSettings["API_ROOT"];
            String apiUser = System.Configuration.ConfigurationManager.AppSettings["API_USER"];
            String apiPassword = System.Configuration.ConfigurationManager.AppSettings["API_PASSWORD"];
            Uri apiUri;

            if (String.IsNullOrWhiteSpace(apiRoot) ||
                !Uri.TryCreate(apiRoot, UriKind.Absolute, out apiUri) ||
                (apiUri.Scheme != Uri.UriSchemeHttp && apiUri.Scheme != Uri.UriSchemeHttps) ||
                String.IsNullOrWhiteSpace(apiUser) || String.IsNullOrWhiteSpace(apiPassword))
            {
                return false;
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = apiUri;
                client.Timeout = TimeSpan.FromSeconds(30);

                var tokenResponse = await client.PostAsync("api/login", new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    { "username", apiUser },
                    { "password", apiPassword }
                }));

                if (!tokenResponse.IsSuccessStatusCode)
                {
                    return false;
                }

                var tokenData = DeserializeJson<TokenResponse>(await tokenResponse.Content.ReadAsStringAsync());
                if (tokenData == null || String.IsNullOrWhiteSpace(tokenData.Token))
                {
                    return false;
                }

                client.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenData.Token);

                using (var content = new StringContent(SerializeJson(new CheckupRequest { Cards = cards }),
                    Encoding.UTF8, "application/json"))
                {
                    var response = await client.PostAsync("api/checkup", content);
                    return response.IsSuccessStatusCode;
                }
            }
        }

        [DataContract]
        private sealed class CheckupRequest
        {
            [DataMember(Name = "cards")]
            public List<CheckupCard> Cards { get; set; }
        }

        [DataContract]
        private sealed class CheckupCard
        {
            [DataMember(Name = "card_number")]
            public int CardNumber { get; set; }

            [DataMember(Name = "yearPaid")]
            public int YearPaid { get; set; }
        }

        [DataContract]
        private sealed class ApplicantIdRequest
        {
            [DataMember(Name = "ids")]
            public List<int> Ids { get; set; }
        }

        [DataContract]
        private sealed class LaravelEncryptedPayload
        {
            [DataMember(Name = "iv")]
            public string Iv { get; set; }

            [DataMember(Name = "value")]
            public string Value { get; set; }

            [DataMember(Name = "mac")]
            public string Mac { get; set; }

            [DataMember(Name = "tag")]
            public string Tag { get; set; }
        }

        [DataContract]
        private sealed class TokenResponse
        {

            [DataMember(Name = "token")]
            public string Token { get; set; }

            [DataMember(Name = "token_type")]
            public string TokenType { get; set; }

            [DataMember(Name = "expires_at")]
            public string ExpiresAt { get; set; }

        }

        [DataContract]
        private sealed class DecryptedApplicant
        {
            public int ResponseId { get; set; }

            [DataMember(Name = "applicationType")]
            public string ApplicationType { get; set; }

            [DataMember(Name = "membershipFee")]
            public string MembershipFee { get; set; }

            [DataMember(Name = "firstname")]
            public string FirstName { get; set; }

            [DataMember(Name = "lastname")]
            public string LastName { get; set; }

            [DataMember(Name = "alias")]
            public string Alias { get; set; }

            [DataMember(Name = "dob")]
            public string DateOfBirth { get; set; }

            [DataMember(Name = "pfgaNumber")]
            public string PfgaNumber { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "province")]
            public string Province { get; set; }

            [DataMember(Name = "postal")]
            public string Postal { get; set; }

            [DataMember(Name = "homephone")]
            public string HomePhone { get; set; }

            [DataMember(Name = "cellphone")]
            public string CellPhone { get; set; }

            [DataMember(Name = "email")]
            public string Email { get; set; }

            [DataMember(Name = "palType")]
            public string PalType { get; set; }

            [DataMember(Name = "palDate")]
            public string PalDate { get; set; }

            [DataMember(Name = "palNum")]
            public string PalNumber { get; set; }

            [DataMember(Name = "palExpiry")]
            public string PalExpiry { get; set; }

            [DataMember(Name = "disciplines")]
            public List<string> Disciplines { get; set; }

            [DataMember(Name = "family")]
            public List<FamilyMember> Family { get; set; }

            [DataMember(Name = "clubs")]
            public List<Club> Clubs { get; set; }

            [DataMember(Name = "courses")]
            public List<Course> Courses { get; set; }

            [DataMember(Name = "extra")]
            public int? Extra { get; set; }

            [DataMember(Name = "familyCount")]
            public int? FamilyCount { get; set; }

            [DataMember(Name = "terms")]
            public bool? Terms { get; set; }

            [DataMember(Name = "photo")]
            public Photo Photo { get; set; }
        }

        [DataContract]
        private sealed class FamilyMember
        {
            [DataMember(Name = "firstname")]
            public string FirstName { get; set; }

            [DataMember(Name = "lastname")]
            public string LastName { get; set; }

            [DataMember(Name = "pal")]
            public string PalNumber { get; set; }

            [DataMember(Name = "palExpiry")]
            public string PalExpiry { get; set; }

            [DataMember(Name = "dob")]
            public string DateOfBirth { get; set; }

            [DataMember(Name = "photo")]
            public Photo Photo { get; set; }
        }

        [DataContract]
        private sealed class Photo
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "data")]
            public string Data { get; set; }
        }

        [DataContract]
        private sealed class Club
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "from")]
            public string From { get; set; }

            [DataMember(Name = "to")]
            public string To { get; set; }
        }

        [DataContract]
        private sealed class Course
        {
            [DataMember(Name = "desc")]
            public string Description { get; set; }

            [DataMember(Name = "trainer")]
            public string Trainer { get; set; }

            [DataMember(Name = "location")]
            public string Location { get; set; }

            [DataMember(Name = "date")]
            public string Date { get; set; }
        }

        [DataContract]
        public sealed class ApplicantResponse
        {
            [DataMember(Name = "id")]
            public int Id { get; set; }
            [DataMember(Name = "uuid")]
            public string Uuid { get; set; }
            [DataMember(Name ="type")]
            public string Type { get; set; }
            [DataMember(Name = "data")]
            public string Data { get; set; }
            [DataMember(Name = "created_at")]
            public string CreatedAt { get; set; }
            [DataMember(Name = "emailed")]
            public bool Emailed { get; set; }
            [DataMember(Name = "emailed_at")]
            public string EmailedAt { get; set; }
            [DataMember(Name = "canceled_at")]
            public string CanceledAt { get; set; }
            [DataMember(Name = "email")]
            public string Email { get; set; }
            [DataMember(Name = "processed")]
            public int Processed { get; set; }
        }

        private void dgApplicants_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgApplicants_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            var row = dgApplicants.Rows[e.RowIndex].DataBoundItem as ApplicantDisplayRow;
            if (row == null || !row.IsFamilyMember)
            {
                return;
            }

            dgApplicants.Rows[e.RowIndex].DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(245, 245, 245);
            dgApplicants.Rows[e.RowIndex].DefaultCellStyle.ForeColor = System.Drawing.Color.DimGray;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                frmParent frm = (frmParent)this.ParentForm;
                frm.showList();
                
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to cancel", ex, true);
            }
        }

        private async void btnImport_Click(object sender, EventArgs e)
        {
            btnImport.Enabled = false;
            prgImport.Visible = true;
            prgImport.Minimum = 0;
            prgImport.Maximum = Math.Max(1, decryptedApplicants.Count);
            prgImport.Value = 0;

            try
            {
                int pendingMembershipTypeId = GetPendingMembershipTypeId();
                int importedCount = 0;
                int nextCard = -1;
                List<int> savedApplicantIds = new List<int>();
                HashSet<string> existingMemberKeys = GetExistingMemberKeys();

                for (int applicantIndex = 0; applicantIndex < decryptedApplicants.Count; applicantIndex++)
                {
                    var applicant = decryptedApplicants[applicantIndex];
                    prgImport.Value = applicantIndex + 1;
                    lblStatus.Text = String.Format("Importing applicant {0} of {1}...",
                        applicantIndex + 1, decryptedApplicants.Count);
                    Refresh();

                    if (String.Equals(applicant.ApplicationType, "new", StringComparison.OrdinalIgnoreCase))
                    {
                        string memberKey = GetMemberKey(applicant.FirstName, applicant.LastName, applicant.Email);
                        if (!String.IsNullOrEmpty(memberKey) && existingMemberKeys.Contains(memberKey))
                        {
                            WriteDiagnostic(String.Format("Applicant {0} {1} was not imported because the member already exists.",
                                applicant.FirstName, applicant.LastName));
                            continue;
                        }

                        if (nextCard < 0)
                        {
                            nextCard = new clsMembership(-1, -1).Card;
                        }

                        clsMembership parent = CreateMember(applicant, pendingMembershipTypeId, nextCard++);
                        AddPaidHistoryRecord(parent);
                        parent.Save();
                        if (parent.ID <= 0)
                        {
                            WriteDiagnostic("Applicant could not be saved; family records were skipped.");
                            continue;
                        }

                        savedApplicantIds.Add(applicant.ResponseId);
                        if (!String.IsNullOrEmpty(memberKey))
                        {
                            existingMemberKeys.Add(memberKey);
                        }
                        DebugMember("Applicant", parent);
                        DebugPaidRecord(parent.ID);
                        importedCount++;

                        importedCount += SaveFamilyMembers(applicant, parent.ID,
                            pendingMembershipTypeId, ref nextCard);
                    }
                    else if (String.Equals(applicant.ApplicationType, "renew", StringComparison.OrdinalIgnoreCase) ||
                             String.Equals(applicant.ApplicationType, "update", StringComparison.OrdinalIgnoreCase))
                    {
                        clsMembership member = FindMemberByCard(applicant.PfgaNumber);
                        if (member == null)
                        {
                            WriteDiagnostic(String.Format("{0}: no member found for PFGA number {1}.",
                                applicant.ApplicationType, applicant.PfgaNumber));
                            continue;
                        }

                        ApplyApplicant(member, applicant, pendingMembershipTypeId, true);
                        DebugMember(applicant.ApplicationType, member);
                        DebugPaidRecord(member.ID);
                        importedCount += SaveFamilyMembers(applicant, member.ID,
                            pendingMembershipTypeId, ref nextCard);
                        savedApplicantIds.Add(applicant.ResponseId);
                        importedCount++;
                    }
                }

                bool apiUpdated = savedApplicantIds.Count == 0 ||
                    await NotifyUpdatedApplicantsAsync(savedApplicantIds.Distinct().ToList());
                if (!apiUpdated)
                {
                    MessageBox.Show("Members were saved locally, but the applicant API could not be updated.",
                        "Applicant API update failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                List<CheckupCard> checkupCards = GetCurrentYearCards();
                bool checkupUpdated = await NotifyCheckupAsync(checkupCards);
                if (!checkupUpdated)
                {
                    MessageBox.Show("Members were saved locally, but the checkup API could not be updated.",
                        "Checkup API update failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                WriteDiagnostic(String.Format("Applicant import complete. {0} member record(s) saved; applicant API notification {1}; checkup notification {2} for {3} card(s).",
                    importedCount, apiUpdated ? "succeeded" : "failed", checkupUpdated ? "succeeded" : "failed", checkupCards.Count));
                lblStatus.Text = String.Format("Import complete: {0} record(s) saved. Applicant API {1}; checkup API {2}.",
                    importedCount, apiUpdated ? "updated" : "update failed", checkupUpdated ? "updated" : "update failed");
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error importing applicants", ex, true);
                lblStatus.Text = "Import failed; records already saved were not rolled back.";
                MessageBox.Show("The import encountered an error. Any records already saved were not rolled back.",
                    "Applicant import failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                prgImport.Value = prgImport.Maximum;
                btnImport.Enabled = true;
            }
        }

        private static void DebugMember(string relationship, clsMembership member)
        {
            WriteDiagnostic(String.Format(
                "{0}: ID={1}, MasterRecord={2}, Card={3}, Name={4}, {5}, Alias={6}, BirthDate={7:yyyy-MM-dd}, Address={8}, Phone={9}, Cell={10}, Email={11}, PAL={12}, PAL expiry={13:yyyy-MM-dd}, MembershipType={14}, JoinedDate={15:yyyy-MM-dd}, SectionFlag={16}, Notes={17}, BadgeImageBytes={18}",
                relationship,
                member.ID,
                member.MasterRecord,
                member.Card,
                member.LastName,
                member.FirstName,
                member.Alias,
                member.BirthDate,
                member.Address,
                member.Phone,
                member.Cell,
                member.Email,
                member.Pal,
                member.PalExpDate,
                member.MembershipType,
                member.JoinedDate,
                member.SectionFlag,
                member.Notes,
                member.BadgeImage == null ? 0 : member.BadgeImage.Length));
        }

        private static void WriteDiagnostic(string message)
        {
            Debug.WriteLine(message);
            Trace.WriteLine(message);
        }

        private static void DebugPaidRecord(int memberId)
        {
            int yearPaid;
            string membershipYear;
            GetPaidValues(DateTime.Today, out yearPaid, out membershipYear);

            WriteDiagnostic(String.Format(
                "Paid preview: MemberID={0}, DatePaid={1:yyyy-MM-dd}, YearPaid={2}, MembershipYear={3}",
                memberId, DateTime.Today, yearPaid, membershipYear));
        }

        private static void GetPaidValues(DateTime datePaid, out int yearPaid, out string membershipYear)
        {
            if (datePaid.Month >= 8)
            {
                yearPaid = datePaid.Year;
                membershipYear = String.Format("{0} - {1}", datePaid.Year, datePaid.Year + 1);
            }
            else
            {
                yearPaid = datePaid.Year - 1;
                membershipYear = String.Format("{0} - {1}", datePaid.Year - 1, datePaid.Year);
            }
        }

        private static void AddPaidHistoryRecord(clsMembership member)
        {
            int yearPaid;
            string membershipYear;
            GetPaidValues(DateTime.Today, out yearPaid, out membershipYear);

            DataRow row = member.PaidHistory.NewRow();
            row["YearPaid"] = yearPaid;
            row["MembershipYear"] = membershipYear;
            row["Deleted"] = false;
            row["NewRec"] = true;
            member.PaidHistory.Rows.Add(row);
        }

        private static void InsertPaidRecord(int memberId)
        {
            int yearPaid;
            string membershipYear;
            GetPaidValues(DateTime.Today, out yearPaid, out membershipYear);

            MembershipTableAdapters.PaidTableAdapter adapter =
                new MembershipTableAdapters.PaidTableAdapter();
            adapter.Insert(memberId, membershipYear, yearPaid, DateTime.Today);
        }
    }
}
