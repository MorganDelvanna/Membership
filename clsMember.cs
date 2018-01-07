using System;
using System.Data;
using System.Data.SqlClient;

namespace PFGA_Membership
{
    public class clsMembership
    {
        private bool _Active = true;
        private bool _Executive = false;
        private bool _NoBackTrack = false;
        private bool _NoEmail = false;
        private bool _SwipeCard = false;
        private bool _CardMade = false;
        private int _Card = -1;
        private int _ID = -1;
        private int _MasterRecord = -1;
        private int _MemberTypeID = 13;
        private Double _Pal = 0;
        private ulong _SectionFlag = 0;
        private ulong _Participation = 0;
        private string _Address = string.Empty;
        private string _CityProv = string.Empty;
        private string _Email = string.Empty;
        private string _FirstName = string.Empty;
        private string _LastName = string.Empty;
        private string _MembershipType = string.Empty;
        private string _Notes = string.Empty;
        private string _Phone = string.Empty;
        private string _Cell = string.Empty;
        private string _Postal = string.Empty;
        private string _Sponsor = string.Empty;
        private string _UserName = string.Empty;
        private string _Walk = string.Empty;
        private string _pOther = string.Empty;
        private DateTime _BirthDate = DateTime.Parse("1900-01-01");
        private DateTime _JoinedDate = Program.getNextTuesday();
        private DateTime _PalExpDate = DateTime.Parse("1900-01-01");
        private DataTable _ExtraCards = new DataTable();
        private DataTable _paidHistory = new DataTable();

        public int ID { get { return _ID; } set { _ID = value; } }
        public string LastName { get { return _LastName; } set { _LastName = value; } }
        public string FirstName { get { return _FirstName; } set { _FirstName = value; } }
        public string Address { get { return _Address; } set { _Address = value; } }
        public string CityProv { get { return _CityProv; } set { _CityProv = value; } }
        public string Postal { get { return _Postal; } set { _Postal = value; } }
        public string Phone { get { return _Phone; } set { _Phone = value; } }
        public string Cell { get { return _Cell; } set { _Cell = value; } }
        public string Email { get { return _Email; } set { _Email = value; } }
        public string MembershipType { get { return _MembershipType; } set { _MembershipType = value; } }
        public int MemberTypeID { get { return _MemberTypeID; } set { _MemberTypeID = value; } }
        public DateTime JoinedDate { get { return _JoinedDate; } set { _JoinedDate = value; } }
        public DateTime BirthDate { get { return _BirthDate; } set { _BirthDate = value; } }
        public string Walk { get { return _Walk; } set { _Walk = value; } }
        public Double Pal { get { return _Pal; } set { _Pal = value; } }
        public DateTime PalExpDate { get { return _PalExpDate; } set { _PalExpDate = value; } }
        public string UserName { get { return _UserName; } set { _UserName = value; } }
        public bool NoBackTrack { get { return _NoBackTrack; } set { _NoBackTrack = value; } }
        public bool NoEmail { get { return _NoEmail; } set { _NoEmail = value; } }
        public bool Active { get { return _Active; } set { _Active = value; } }
        public bool SwipeCard { get { return _SwipeCard; } set { _SwipeCard = value; } }
        public bool Executive { get { return _Executive; } set { _Executive = value; } }
        public bool CardMade { get { return _CardMade; } set { _CardMade = value; } }
        public int MasterRecord { get { return _MasterRecord; } set { _MasterRecord = value; } }
        public ulong SectionFlag { get { return _SectionFlag; } set { _SectionFlag = value; } }
        public ulong Participation { get { return _Participation; } set { _Participation = value; } }
        public string ParticipationOther { get { return _pOther; } set { _pOther = value; } }
        public int Card { get { return _Card; } set { _Card = value; } }
        public string Notes { get { return _Notes; } set { _Notes = value; } }
        public string Sponsor { get { return _Sponsor; } set { _Sponsor = value; } }
        public DataTable PaidHistory { get { return _paidHistory; } }
        public DataTable ExtraCards { get { return _ExtraCards; } }
        public byte[] BadgeImage { get; set; }
        public string MasterRecordName { get; set; }
        
        public clsMembership(int ID, int PromoteID)
        {
            try
            {
                // ID >=0 means this is an existing member
                if (ID >= 0)
                {
                    LoadMember(ID);
                    // Currently an Extra Card
                    if (PromoteID > 0)
                    {
                        PromoteMember();
                    }
                }
                else if (ID < 0 && PromoteID > 0)
                {
                    // New Extra Card
                    MasterRecord = PromoteID;
                    MemberTypeID = 11;
                    Walk = "Need";
                    BadgeImage = new Byte[0];
                    Card = getNewCard();
                }
                else if (ID < 0 && PromoteID < 0)
                {
                    // New Member
                    Card = getNewCard();
                    MemberTypeID = 13;
                    Walk = "Need";
                    BadgeImage = new Byte[0];
                    _paidHistory = getPaidHistory();
                    _ExtraCards = getExtraCards();
                    setDefaultPaid();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error initializing clsMember", ex, true);
            }
        }

        private void LoadMember(int ID)
        {
            try
            {
                MembershipTableAdapters.MembersTableAdapter daMember = new MembershipTableAdapters.MembersTableAdapter();
                Membership.MembersDataTable dtMember = new Membership.MembersDataTable();

                daMember.Fill(dtMember);
                Membership.MembersRow row = dtMember.FindByID(ID);

                _ID = row.ID;
                _LastName = (row.Last_Name == null ? string.Empty : row.Last_Name);
                _FirstName = (row.First_Name == null ? string.Empty : row.First_Name);
                _Address = (row.Address == null ? string.Empty : row.Address);
                _CityProv = (row.City__Prov == null ? string.Empty : row.City__Prov);
                _Postal = (row.Postal == null ? string.Empty : row.Postal);
                _Phone = (row.Phone == null ? string.Empty : row.Phone);
                _Email = (row.Email_Address == null ? string.Empty : row.Email_Address);
                _MemberTypeID = row.MemberTypeID;
                _JoinedDate = row.Date_Joined;
                _BirthDate = row.Birth_Date;
                _Walk = (row.Walk == null ? string.Empty : row.Walk);
                _Pal = row.Pal;
                _PalExpDate = row.Pal_Exp_Date;
                _UserName = (row.Website_Usernames == null ? string.Empty : row.Website_Usernames);
                _NoBackTrack = row.NoBackTrack;
                _NoEmail = row.NoEmailing;
                _MasterRecord = row.MasterRecord;
                if (_MasterRecord > 0)
                {
                    MasterRecordName = getMasterRecordName(row.MasterRecord);
                }
                _SectionFlag = row.SectionFlag;
                _Card = row.Card;
                _paidHistory = getPaidHistory();
                _ExtraCards = getExtraCards();
                _Active = row.Active;
                _Notes = (row.Notes == null ? string.Empty : row.Notes);
                _SwipeCard = row.Swipe;
                _Cell = (row.Cell == null ? string.Empty : row.Cell);
                _Participation = row.Participation;
                _pOther = (row.pOther == null ? string.Empty : row.pOther);
                BadgeImage = row.Image;
                _CardMade = row.CardMade;
                _Sponsor = row.Sponsor;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error loading member information", ex, true);
            }
        }

        private DataTable getPaidHistory()
        {
            
            DataTable retVal = new DataTable();
            try
            {
                DataColumn dcYearPaid = new DataColumn("YearPaid", System.Type.GetType("System.Int32"));
                DataColumn dcMembershipYear = new DataColumn("MembershipYear", System.Type.GetType("System.String"));
                DataColumn dcDeleted = new DataColumn("Deleted", System.Type.GetType("System.Boolean"));
                DataColumn dcNewRec = new DataColumn("NewRec", System.Type.GetType("System.Boolean"));
                DataColumn dcPaymentType = new DataColumn("PaymentType", System.Type.GetType("System.Int32"));
                DataColumn dcPaymentName = new DataColumn("PaymentName", System.Type.GetType("System.String"));

                DataRow dr;
                retVal.Columns.Add(dcYearPaid);
                retVal.Columns.Add(dcMembershipYear);
                retVal.Columns.Add(dcDeleted);
                retVal.Columns.Add(dcNewRec);
                retVal.Columns.Add(dcPaymentType);
                retVal.Columns.Add(dcPaymentName);

                MembershipTableAdapters.PaidTableAdapter daPaid = new PFGA_Membership.MembershipTableAdapters.PaidTableAdapter();
                Membership.PaidDataTable dtPaid = new Membership.PaidDataTable();
                DataView dvPaid;

                daPaid.Fill(dtPaid);
                dvPaid = new DataView(dtPaid);
                dvPaid.RowFilter = String.Format("MemberID = {0}", ID);

                foreach (DataRowView row in dvPaid)
                {
                    int paymentType = -1;
                    if (int.TryParse(row["PaymentType"].ToString(), out paymentType) == false)
                    {
                        paymentType = -1;
                    }

                    dr = retVal.NewRow();
                    dr["YearPaid"] = int.Parse(row["YearPaid"].ToString());
                    dr["MembershipYear"] = row["MembershipYear"].ToString();
                    dr["Deleted"] = false;
                    dr["NewRec"] = false;
                    dr["PaymentType"] = paymentType;
                    retVal.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error getting Paid Records from the database", ex, true);
            }
            return retVal;
        }

        private DataTable getExtraCards()
        {
            DataTable retVal = new DataTable();

            try
            {
                MembershipTableAdapters.MembersTableAdapter daMember = new PFGA_Membership.MembershipTableAdapters.MembersTableAdapter();
                Membership.MembersDataTable dtMember = new Membership.MembersDataTable();
                DataColumn dcID = new DataColumn("ID", System.Type.GetType("System.Int32"));
                DataColumn dcName = new DataColumn("Name", System.Type.GetType("System.String"));
                DataColumn dcDeleted = new DataColumn("Deleted", System.Type.GetType("System.Boolean"));
                DataColumn dcPromote = new DataColumn("Promote", System.Type.GetType("System.Boolean"));

                DataRow dr;
                retVal.Columns.Add(dcID);
                retVal.Columns.Add(dcName);
                retVal.Columns.Add(dcDeleted);
                retVal.Columns.Add(dcPromote);

                DataView dvMember;

                daMember.Fill(dtMember);
                dvMember = new DataView(dtMember);
                dvMember.RowFilter = String.Format("MasterRecord = {0}", ID);

                foreach (DataRowView row in dvMember)
                {
                    dr = retVal.NewRow();
                    dr[dcID] = int.Parse(row["ID"].ToString());
                    dr[dcName] = String.Format("{0}, {1}", row["Last Name"].ToString(), row["First Name"].ToString());
                    dr[dcDeleted] = false;
                    dr[dcPromote] = false;
                    retVal.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error getting Extra Cards", ex, true);
            }
            return retVal;
        }

        public void Save()
        {
            try
            {

                MembershipTableAdapters.MembersTableAdapter daMember = new MembershipTableAdapters.MembersTableAdapter();
                Membership.MembersDataTable dtMember = new Membership.MembersDataTable();
                Membership.MembersRow row;

                daMember.Fill(dtMember);

                if (ID > 0)
                {
                    row = dtMember.FindByID(ID);
                }
                else
                {
                    row = dtMember.NewMembersRow();
                }

                row.Last_Name = _LastName;
                row.First_Name = _FirstName;
                row.Address = Address;
                row.City__Prov = _CityProv;
                row.Postal = _Postal;
                row.Phone = _Phone;
                row.Email_Address = _Email;
                row.MemberTypeID = _MemberTypeID;
                row.Date_Joined = _JoinedDate;
                row.Birth_Date = _BirthDate;
                row.Walk = _Walk;
                row.Pal = _Pal;
                row.Pal_Exp_Date = _PalExpDate;
                row.Website_Usernames = _UserName;
                row.NoBackTrack = _NoBackTrack;
                row.NoEmailing = _NoEmail;
                row.Active = _Active;
                row.MasterRecord = _MasterRecord;
                row.SectionFlag = byte.Parse(_SectionFlag.ToString());
                row.Card = _Card;
                row.Notes = _Notes;
                row.Swipe = _SwipeCard;
                row.Executive = _Executive;
                row.Sponsor = _Sponsor;
                row.Cell = _Cell;
                row.Participation = byte.Parse(_Participation.ToString());
                row.pOther = _pOther;
                row.Image = BadgeImage;
                row.CardMade = _CardMade;

                // Old columns
                row.ATT_Expiry = new DateTime(1900, 01, 01);
                row.Membership_Type = string.Empty;
                row.Section = string.Empty;

                if (ID > 0)
                {
                    daMember.Update(row);
                }
                else
                {
                    // Include an event to fill in the Autonumber value.
                    dtMember.AddMembersRow(row);
                    daMember.Update(dtMember);
                    dtMember.AcceptChanges();
                    ID = row.ID;
                }

                if (ID > 0 && MasterRecord <= 0)
                {
                    savePaidHistory();
                    saveExtraCard();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error saving Member", ex, true);
            }
        }

        private void savePaidHistory()
        {
            DataRow[] search;
            int retVal = 0;
            try
            {
                search = _paidHistory.Select("Deleted = True OR NewRec = True");

                if (search.Length > 0)
                {
                    MembershipTableAdapters.PaidTableAdapter daPaid = new PFGA_Membership.MembershipTableAdapters.PaidTableAdapter();

                    foreach (DataRow row in search)
                    {
                        int test;
                        int? paymentType = null;
                        if (int.TryParse(row["PaymentType"].ToString(), out test))
                        {
                            paymentType = test;
                        }
                        
                        if (bool.Parse(row["Deleted"].ToString()) == true)
                        {
                            retVal = daPaid.Delete(_ID, 0, row["MembershipYear"].ToString(), int.Parse(row["YearPaid"].ToString()), paymentType, null);
                        }
                        else if (bool.Parse(row["NewRec"].ToString()) == true)
                        {
                            retVal = daPaid.Insert(_ID, 0, row["MembershipYear"].ToString(), int.Parse(row["YearPaid"].ToString()), paymentType, DateTime.Today);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error saving Paid History", ex, true);
            }
        }

        private void saveExtraCard()
        {
            DataRow[] search;
            int retVal = 0;

            try
            {
                search = _ExtraCards.Select("Deleted = True");
                ErrorLogger.Log(string.Format("{0} Cards to Delete", search.Length.ToString()), null, false);

                if (search.Length > 0)
                {
                    MembershipTableAdapters.MembersTableAdapter daExtra = new PFGA_Membership.MembershipTableAdapters.MembersTableAdapter();
                    
                    foreach (DataRow row in search)
                    {
                        retVal = daExtra.DeleteQuery(int.Parse(row["ID"].ToString()));

                        ErrorLogger.Log(string.Format("{0} Extra Cards deleted", retVal), null, false);
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error saving extra card", ex, true);
            }
        }

        private void PromoteMember()
        {
            try
            {
                MembershipTableAdapters.MembersTableAdapter daMember = new MembershipTableAdapters.MembersTableAdapter();
                Membership.MembersDataTable dtMember = new Membership.MembersDataTable();

                daMember.Fill(dtMember);
                Membership.MembersRow row = dtMember.FindByID(this.MasterRecord);

                SectionFlag = row.SectionFlag;
                MemberTypeID = 5;
                MasterRecord = -1;

                setDefaultPaid();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to promot extra card", ex, true);
            }
        }

        private void setDefaultPaid()
        {
            int curYear;

            try
            {
                if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 9)
                {
                    curYear = DateTime.Today.Year - 1;
                }
                else
                {
                    curYear = DateTime.Today.Year;
                }

                DataRow[] search = PaidHistory.Select(String.Format("YearPaid={0}", curYear.ToString()));

                if (search.Length == 0)
                {
                    DataRow dr = PaidHistory.NewRow();
                    dr["YearPaid"] = curYear;
                    dr["MembershipYear"] = string.Format("{0} - {1}", curYear, curYear + 1);
                    dr["Deleted"] = false;
                    dr["NewRec"] = true;
                    PaidHistory.Rows.Add(dr);
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error creating a default paid record", ex, true);
            }
        }

        private int getNewCard()
        {
            int retval;

            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();

            retval = (int)da.NewCard();

            return retval;
        }

        private string getMasterRecordName(int MasterId)
        {
            string retVal;

            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();

            retVal = da.GetMasterName(MasterId).ToString();

            return retVal;
        }

        private static void OnRowUpdated(object sender, SqlRowUpdatedEventArgs e)
        {
            try
            {
                // Conditionally execute this code block on inserts only.
                if (e.StatementType == StatementType.Insert)
                {
                    MembershipTableAdapters.MembersTableAdapter mbr = new PFGA_Membership.MembershipTableAdapters.MembersTableAdapter();

                    SqlConnection conn = mbr.Connection;
                    conn.Open();
                    SqlCommand cmdNewID = new SqlCommand("SELECT Max(ID) + 1 FROM Members", conn);
                    // Retrieve the Autonumber and store it in the CategoryID column.
                    e.Row["ID"] = (int)cmdNewID.ExecuteScalar();
                    e.Status = UpdateStatus.SkipCurrentRow;
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error retrieving the newly added ID number", ex, true);
            }
        }

        public void refreshCards()
        {
            _ExtraCards = getExtraCards();
        }
    }

    
}
