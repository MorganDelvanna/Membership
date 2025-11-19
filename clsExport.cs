using System;
using System.Data.SqlClient;
using System.Data;

namespace PFGA_Membership
{
    internal class clsExport
    {
        clsHelpers cHelper = new clsHelpers();
        SqlConnection cnn = new SqlConnection();

        public clsExport(SqlConnection conn)
        {
            cnn = conn;
        }

        public DataSet exportMembers()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add(createMemberTable());
            ds.Tables.Add(createExtraTable());
            ds.Tables.Add(createNotRenewed());
            ds.Tables.Add(createReport());
            ds.Tables.Add(createMemberSectionReport());
            ds.Tables.Add(createNonMemberSectionReport());

            return ds;
        }

        private DataTable createMemberTable()
        {
            int year = cHelper.thisYear();
            String qryMembers = @"SELECT Card, [Last Name], [First Name], Age, [Membership Type], Walk, PAL, [Pal Exp Date], Swipe, YearPaid, Phone,
                                Cell, [Email Address], [Date Joined], [Website Usernames], Notes, Sponsor, SectionFlag, Participation, 
                                NoBackTrack, NoEmailing, pOther, CardMade, DatePaid
                            FROM            qryExport
                            WHERE (YearPaid = " + year + ") OR (MemberTypeID = 6)";

            SqlCommand cmd = new SqlCommand(qryMembers, cnn);
            DataTable dtMembers = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtMembers);
            DataTable dtExport = MemberTableModel(year);

            DataRow headers = dtExport.NewRow();
            for (int col = 0; col < dtExport.Columns.Count; col++)
            {
                headers[col] = dtExport.Columns[col].ColumnName;
            }
            dtExport.Rows.Add(headers);

            for (int row = 0; row < dtMembers.Rows.Count; row++)
            {
                DataRow dr = dtExport.NewRow();
                dr["Card"] = dtMembers.Rows[row][0];
                dr["Last Name"] = dtMembers.Rows[row][1];
                dr["First Name"] = dtMembers.Rows[row][2];
                dr["Age"] = dtMembers.Rows[row][3];
                dr["Membership Type"] = dtMembers.Rows[row][4];
                dr["Walk"] = dtMembers.Rows[row][5];
                dr["PAL"] = dtMembers.Rows[row][6];
                dr["Pal Expiry Date"] = DateTime.Parse(dtMembers.Rows[row][7].ToString()).ToString("dd/MMM/yyyy");
                dr["Swipe"] = dtMembers.Rows[row][8];
                dr["Year Paid"] = dtMembers.Rows[row][9];
                dr["Phone"] = dtMembers.Rows[row][10];
                dr["Cell"] = dtMembers.Rows[row][11];
                dr["Email Address"] = dtMembers.Rows[row][12];
                dr["Date Joined"] = DateTime.Parse(dtMembers.Rows[row][13].ToString()).ToString("dd/MMM/yyyy");
                dr["Website Username"] = dtMembers.Rows[row][14];
                dr["Notes"] = dtMembers.Rows[row][15];
                dr["Sponsor"] = dtMembers.Rows[row][16];
                dr["Section"] = cHelper.getSectionLabels(dtMembers.Rows[row][17].ToString());
                dr["Participation"] = cHelper.getParticipation(dtMembers.Rows[row][18].ToString());
                dr["No BackTrack"] = dtMembers.Rows[row][19];
                dr["No Email"] = dtMembers.Rows[row][20];
                dr["Other"] = dtMembers.Rows[row][21];
                dr["Card Made"] = dtMembers.Rows[row][22];
                dr["Date Paid"] = DateTime.Parse(dtMembers.Rows[row][23].ToString()).ToString("dd/MMM/yyyy");
                dtExport.Rows.Add(dr);
            } 
            da.Dispose();
                        
            return dtExport;
        }

        private DataTable MemberTableModel(int year)
        {
            DataTable dtExport = new DataTable("Members " + year);

            DataColumn colCard = new DataColumn();
            colCard.DataType = System.Type.GetType("System.String");
            colCard.ColumnName = "Card";
            dtExport.Columns.Add(colCard);

            DataColumn colLName = new DataColumn();
            colLName.DataType = System.Type.GetType("System.String");
            colLName.ColumnName = "Last Name";
            dtExport.Columns.Add(colLName);

            DataColumn colFName = new DataColumn();
            colFName.DataType = System.Type.GetType("System.String");
            colFName.ColumnName = "First Name";
            dtExport.Columns.Add(colFName);

            DataColumn colAge = new DataColumn();
            colAge.DataType = System.Type.GetType("System.String");
            colAge.ColumnName = "Age";
            dtExport.Columns.Add(colAge);

            DataColumn colMemberType = new DataColumn();
            colMemberType.DataType = System.Type.GetType("System.String");
            colMemberType.ColumnName = "Membership Type";
            dtExport.Columns.Add(colMemberType);

            DataColumn colWalk = new DataColumn();
            colWalk.DataType = System.Type.GetType("System.String");
            colWalk.ColumnName = "Walk";
            dtExport.Columns.Add(colWalk);

            DataColumn colPal = new DataColumn();
            colPal.DataType = System.Type.GetType("System.String");
            colPal.ColumnName = "PAL";
            dtExport.Columns.Add(colPal);

            DataColumn colPalExp = new DataColumn();
            colPalExp.DataType = System.Type.GetType("System.String");
            colPalExp.ColumnName = "PAL Expiry Date";
            dtExport.Columns.Add(colPalExp);

            DataColumn colSwipe = new DataColumn();
            colSwipe.DataType = System.Type.GetType("System.String");
            colSwipe.ColumnName = "Swipe";
            dtExport.Columns.Add(colSwipe);

            DataColumn colPaid = new DataColumn();
            colPaid.DataType = System.Type.GetType("System.String");
            colPaid.ColumnName = "Year Paid";
            dtExport.Columns.Add(colPaid);

            DataColumn colPhone = new DataColumn();
            colPhone.DataType = System.Type.GetType("System.String");
            colPhone.ColumnName = "Phone";
            dtExport.Columns.Add(colPhone);

            DataColumn colCell = new DataColumn();
            colCell.DataType = System.Type.GetType("System.String");
            colCell.ColumnName = "Cell";
            dtExport.Columns.Add(colCell);

            DataColumn colEmail = new DataColumn();
            colEmail.DataType = System.Type.GetType("System.String");
            colEmail.ColumnName = "Email Address";
            dtExport.Columns.Add(colEmail);

            DataColumn colJoined = new DataColumn();
            colJoined.DataType = System.Type.GetType("System.String");
            colJoined.ColumnName = "Date Joined";
            dtExport.Columns.Add(colJoined);

            DataColumn colWeb = new DataColumn();
            colWeb.DataType = System.Type.GetType("System.String");
            colWeb.ColumnName = "Website Username";
            dtExport.Columns.Add(colWeb);

            DataColumn colNotes = new DataColumn();
            colNotes.DataType = System.Type.GetType("System.String");
            colNotes.ColumnName = "Notes";
            dtExport.Columns.Add(colNotes);

            DataColumn colSponsor = new DataColumn();
            colSponsor.DataType = System.Type.GetType("System.String");
            colSponsor.ColumnName = "Sponsor";
            dtExport.Columns.Add(colSponsor);

            DataColumn colSection = new DataColumn();
            colSection.DataType = System.Type.GetType("System.String");
            colSection.ColumnName = "Section";
            dtExport.Columns.Add(colSection);

            DataColumn colParticipation = new DataColumn();
            colParticipation.DataType = System.Type.GetType("System.String");
            colParticipation.ColumnName = "Participation";
            dtExport.Columns.Add(colParticipation);

            DataColumn colNoBackTrack = new DataColumn();
            colNoBackTrack.DataType = System.Type.GetType("System.String");
            colNoBackTrack.ColumnName = "No BackTrack";
            dtExport.Columns.Add(colNoBackTrack);

            DataColumn colNoEmail = new DataColumn();
            colNoEmail.DataType = System.Type.GetType("System.String");
            colNoEmail.ColumnName = "No Email";
            dtExport.Columns.Add(colNoEmail);

            DataColumn colOther = new DataColumn();
            colOther.DataType = System.Type.GetType("System.String");
            colOther.ColumnName = "Other";
            dtExport.Columns.Add(colOther);

            DataColumn colCardMade = new DataColumn();
            colCardMade.DataType = System.Type.GetType("System.String");
            colCardMade.ColumnName = "Card Made";
            dtExport.Columns.Add(colCardMade);

            DataColumn colDatePaid = new DataColumn();
            colDatePaid.DataType = System.Type.GetType("System.String");
            colDatePaid.ColumnName = "Date Paid";
            dtExport.Columns.Add(colDatePaid);

            return dtExport;

        }

        private DataTable createExtraTable()
        {
            int year = cHelper.thisYear();
            String qryExtra = @"SELECT Card, [Last Name], [First Name], Age, [Membership Type], Walk, Pal, [Pal Exp Date], [Master Record], Swipe, YearPaid, Phone,
                        [Email Address], [Date Joined], [Website Usernames], Notes, Sponsor, SectionFlag, Participation, pOther, CardMade
                    FROM qryExportExtra
                    WHERE (YearPaid = " + year + ")";

            SqlCommand cmd = new SqlCommand(qryExtra, cnn);
            DataTable dtExtra = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtExtra);

            DataTable dtExport = ExtraTableModel();

            DataRow headers = dtExport.NewRow();
            for (int col = 0; col < dtExport.Columns.Count; col++)
            {
                headers[col] = dtExport.Columns[col].ColumnName;
            }
            dtExport.Rows.Add(headers);

            for (int row = 0; row < dtExtra.Rows.Count; row++)
            {
                DataRow dr = dtExport.NewRow();
                dr["Card"] = dtExtra.Rows[row][0];
                dr["Last Name"] = dtExtra.Rows[row][1];
                dr["First Name"] = dtExtra.Rows[row][2];
                dr["Age"] = dtExtra.Rows[row][3];
                dr["Membership Type"] = dtExtra.Rows[row][4];
                dr["Walk"] = dtExtra.Rows[row][5];
                dr["PAL"] = dtExtra.Rows[row][6];
                dr["Pal Expiry Date"] = DateTime.Parse(dtExtra.Rows[row][7].ToString()).ToString("dd/MMM/yyyy");
                dr["Master Record"] = dtExtra.Rows[row][8];
                dr["Swipe"] = dtExtra.Rows[row][9];
                dr["Year Paid"] = dtExtra.Rows[row][10];
                dr["Phone"] = dtExtra.Rows[row][11];
                dr["Email Address"] = dtExtra.Rows[row][12];
                dr["Date Joined"] = DateTime.Parse(dtExtra.Rows[row][13].ToString()).ToString("dd/MMM/yyyy");
                dr["Website Username"] = dtExtra.Rows[row][14];
                dr["Notes"] = dtExtra.Rows[row][15];
                dr["Sponsor"] = dtExtra.Rows[row][16];
                dr["Section"] = cHelper.getSectionLabels(dtExtra.Rows[row][17].ToString());
                dr["Participation"] = cHelper.getParticipation(dtExtra.Rows[row][18].ToString());
                dr["Other"] = dtExtra.Rows[row][19];
                dr["Card Made"] = dtExtra.Rows[row][20];
                dtExport.Rows.Add(dr);
            }
            da.Dispose();

            return dtExport;
        }

        private DataTable ExtraTableModel()
        {
            DataTable dtExtra = new DataTable("Extra Cards");

            DataColumn colCard = new DataColumn();
            colCard.DataType = System.Type.GetType("System.String");
            colCard.ColumnName = "Card";
            dtExtra.Columns.Add(colCard);

            DataColumn colLName = new DataColumn();
            colLName.DataType = System.Type.GetType("System.String");
            colLName.ColumnName = "Last Name";
            dtExtra.Columns.Add(colLName);

            DataColumn colFName = new DataColumn();
            colFName.DataType = System.Type.GetType("System.String");
            colFName.ColumnName = "First Name";
            dtExtra.Columns.Add(colFName);

            DataColumn colAge = new DataColumn();
            colAge.DataType = System.Type.GetType("System.String");
            colAge.ColumnName = "Age";
            dtExtra.Columns.Add(colAge);

            DataColumn colMemberType = new DataColumn();
            colMemberType.DataType = System.Type.GetType("System.String");
            colMemberType.ColumnName = "Membership Type";
            dtExtra.Columns.Add(colMemberType);

            DataColumn colWalk = new DataColumn();
            colWalk.DataType = System.Type.GetType("System.String");
            colWalk.ColumnName = "Walk";
            dtExtra.Columns.Add(colWalk);

            DataColumn colPal = new DataColumn();
            colPal.DataType = System.Type.GetType("System.String");
            colPal.ColumnName = "PAL";
            dtExtra.Columns.Add(colPal);

            DataColumn colPalExp = new DataColumn();
            colPalExp.DataType = System.Type.GetType("System.String");
            colPalExp.ColumnName = "PAL Expiry Date";
            dtExtra.Columns.Add(colPalExp);

            DataColumn colMaster = new DataColumn();
            colMaster.DataType = System.Type.GetType("System.String");
            colMaster.ColumnName = "Master Record";
            dtExtra.Columns.Add(colMaster);

            DataColumn colSwipe = new DataColumn();
            colSwipe.DataType = System.Type.GetType("System.String");
            colSwipe.ColumnName = "Swipe";
            dtExtra.Columns.Add(colSwipe);

            DataColumn colPaid = new DataColumn();
            colPaid.DataType = System.Type.GetType("System.String");
            colPaid.ColumnName = "Year Paid";
            dtExtra.Columns.Add(colPaid);

            DataColumn colPhone = new DataColumn();
            colPhone.DataType = System.Type.GetType("System.String");
            colPhone.ColumnName = "Phone";
            dtExtra.Columns.Add(colPhone);

            DataColumn colCell = new DataColumn();
            colCell.DataType = System.Type.GetType("System.String");
            colCell.ColumnName = "Cell";
            dtExtra.Columns.Add(colCell);

            DataColumn colEmail = new DataColumn();
            colEmail.DataType = System.Type.GetType("System.String");
            colEmail.ColumnName = "Email Address";
            dtExtra.Columns.Add(colEmail);

            DataColumn colJoined = new DataColumn();
            colJoined.DataType = System.Type.GetType("System.String");
            colJoined.ColumnName = "Date Joined";
            dtExtra.Columns.Add(colJoined);

            DataColumn colWeb = new DataColumn();
            colWeb.DataType = System.Type.GetType("System.String");
            colWeb.ColumnName = "Website Username";
            dtExtra.Columns.Add(colWeb);

            DataColumn colNotes = new DataColumn();
            colNotes.DataType = System.Type.GetType("System.String");
            colNotes.ColumnName = "Notes";
            dtExtra.Columns.Add(colNotes);

            DataColumn colSponsor = new DataColumn();
            colSponsor.DataType = System.Type.GetType("System.String");
            colSponsor.ColumnName = "Sponsor";
            dtExtra.Columns.Add(colSponsor);

            DataColumn colSection = new DataColumn();
            colSection.DataType = System.Type.GetType("System.String");
            colSection.ColumnName = "Section";
            dtExtra.Columns.Add(colSection);

            DataColumn colParticipation = new DataColumn();
            colParticipation.DataType = System.Type.GetType("System.String");
            colParticipation.ColumnName = "Participation";
            dtExtra.Columns.Add(colParticipation);

            DataColumn colOther = new DataColumn();
            colOther.DataType = System.Type.GetType("System.String");
            colOther.ColumnName = "Other";
            dtExtra.Columns.Add(colOther);

            DataColumn colCardMade = new DataColumn();
            colCardMade.DataType = System.Type.GetType("System.String");
            colCardMade.ColumnName = "Card Made";
            dtExtra.Columns.Add(colCardMade);

            return dtExtra;

        }

        private DataTable createNotRenewed()
        {
            int year = cHelper.thisYear();
            String qryNonRenewals = @"SELECT Card, [Last Name], [First Name], Age, [Membership Type], Walk, [Pal Exp Date], Pal, Swipe, YearPaid, Phone,
                        Cell, [Email Address], [Date Joined], [Website Usernames], Notes, Sponsor, SectionFlag, Participation, 
                        NoBackTrack, NoEmailing, pOther, CardMade, DatePaid
                    FROM qryExport
                    JOIN (
                            SELECT ID, Max(YearPaid) AS MaxYearPaid 
                            FROM qryExport GROUP BY ID HAVING Max(YearPaid) = " + (year - 1) + @") AS MaxPaid
                    ON qryExport.ID = MaxPaid.ID";

            SqlCommand cmd = new SqlCommand(qryNonRenewals, cnn);
            DataTable dtNon = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtNon);
            DataTable dtNonExport = RenewalTableModel();

            DataRow headers = dtNonExport.NewRow();
            for (int col = 0; col < dtNonExport.Columns.Count; col++)
            {
                headers[col] = dtNonExport.Columns[col].ColumnName;
            }
            dtNonExport.Rows.Add(headers);

            for (int row = 0; row < dtNon.Rows.Count; row++)
            {
                DataRow dr = dtNonExport.NewRow();
                dr["Card"] = dtNon.Rows[row][0];
                dr["Last Name"] = dtNon.Rows[row][1];
                dr["First Name"] = dtNon.Rows[row][2];
                dr["Age"] = dtNon.Rows[row][3];
                dr["Membership Type"] = dtNon.Rows[row][4];
                dr["Walk"] = dtNon.Rows[row][5];
                dr["PAL"] = dtNon.Rows[row][6];
                dr["Pal Expiry Date"] = dtNon.Rows[row][7];
                dr["Swipe"] = dtNon.Rows[row][8];
                dr["Year Paid"] = dtNon.Rows[row][9];
                dr["Phone"] = dtNon.Rows[row][10];
                dr["Cell"] = dtNon.Rows[row][11];
                dr["Email Address"] = dtNon.Rows[row][12];
                dr["Date Joined"] =DateTime.Parse(dtNon.Rows[row][13].ToString()).ToString("dd/MMM/yyyy");
                dr["Website Username"] = dtNon.Rows[row][14];
                dr["Notes"] = dtNon.Rows[row][15];
                dr["Sponsor"] = dtNon.Rows[row][16];
                dr["Section"] = cHelper.getSectionLabels(dtNon.Rows[row][17].ToString());
                dr["Participation"] = cHelper.getParticipation(dtNon.Rows[row][18].ToString());
                dr["No BackTrack"] = dtNon.Rows[row][19];
                dr["No Email"] = dtNon.Rows[row][20];
                dr["Other"] = dtNon.Rows[row][21];
                dr["Card Made"] = dtNon.Rows[row][22];
                dr["Date Paid"] = DateTime.Parse(dtNon.Rows[row][23].ToString()).ToString("dd/MMM/yyyy");
                dtNonExport.Rows.Add(dr);
            }
            da.Dispose();
            return dtNonExport;

        }

        private DataTable RenewalTableModel()
        {
            DataTable dtNonRenew = new DataTable("Not Renewed");

            DataColumn colCard = new DataColumn();
            colCard.DataType = System.Type.GetType("System.String");
            colCard.ColumnName = "Card";
            dtNonRenew.Columns.Add(colCard);

            DataColumn colLName = new DataColumn();
            colLName.DataType = System.Type.GetType("System.String");
            colLName.ColumnName = "Last Name";
            dtNonRenew.Columns.Add(colLName);

            DataColumn colFName = new DataColumn();
            colFName.DataType = System.Type.GetType("System.String");
            colFName.ColumnName = "First Name";
            dtNonRenew.Columns.Add(colFName);

            DataColumn colAge = new DataColumn();
            colAge.DataType = System.Type.GetType("System.String");
            colAge.ColumnName = "Age";
            dtNonRenew.Columns.Add(colAge);

            DataColumn colMemberType = new DataColumn();
            colMemberType.DataType = System.Type.GetType("System.String");
            colMemberType.ColumnName = "Membership Type";
            dtNonRenew.Columns.Add(colMemberType);

            DataColumn colWalk = new DataColumn();
            colWalk.DataType = System.Type.GetType("System.String");
            colWalk.ColumnName = "Walk";
            dtNonRenew.Columns.Add(colWalk);

            DataColumn colPal = new DataColumn();
            colPal.DataType = System.Type.GetType("System.String");
            colPal.ColumnName = "PAL";
            dtNonRenew.Columns.Add(colPal);

            DataColumn colPalExp = new DataColumn();
            colPalExp.DataType = System.Type.GetType("System.String");
            colPalExp.ColumnName = "PAL Expiry Date";
            dtNonRenew.Columns.Add(colPalExp);

            DataColumn colSwipe = new DataColumn();
            colSwipe.DataType = System.Type.GetType("System.String");
            colSwipe.ColumnName = "Swipe";
            dtNonRenew.Columns.Add(colSwipe);

            DataColumn colPaid = new DataColumn();
            colPaid.DataType = System.Type.GetType("System.String");
            colPaid.ColumnName = "Year Paid";
            dtNonRenew.Columns.Add(colPaid);

            DataColumn colPhone = new DataColumn();
            colPhone.DataType = System.Type.GetType("System.String");
            colPhone.ColumnName = "Phone";
            dtNonRenew.Columns.Add(colPhone);

            DataColumn colCell = new DataColumn();
            colCell.DataType = System.Type.GetType("System.String");
            colCell.ColumnName = "Cell";
            dtNonRenew.Columns.Add(colCell);

            DataColumn colEmail = new DataColumn();
            colEmail.DataType = System.Type.GetType("System.String");
            colEmail.ColumnName = "Email Address";
            dtNonRenew.Columns.Add(colEmail);

            DataColumn colJoined = new DataColumn();
            colJoined.DataType = System.Type.GetType("System.String");
            colJoined.ColumnName = "Date Joined";
            dtNonRenew.Columns.Add(colJoined);

            DataColumn colWeb = new DataColumn();
            colWeb.DataType = System.Type.GetType("System.String");
            colWeb.ColumnName = "Website Username";
            dtNonRenew.Columns.Add(colWeb);

            DataColumn colNotes = new DataColumn();
            colNotes.DataType = System.Type.GetType("System.String");
            colNotes.ColumnName = "Notes";
            dtNonRenew.Columns.Add(colNotes);

            DataColumn colSponsor = new DataColumn();
            colSponsor.DataType = System.Type.GetType("System.String");
            colSponsor.ColumnName = "Sponsor";
            dtNonRenew.Columns.Add(colSponsor);

            DataColumn colSection = new DataColumn();
            colSection.DataType = System.Type.GetType("System.String");
            colSection.ColumnName = "Section";
            dtNonRenew.Columns.Add(colSection);

            DataColumn colParticipation = new DataColumn();
            colParticipation.DataType = System.Type.GetType("System.String");
            colParticipation.ColumnName = "Participation";
            dtNonRenew.Columns.Add(colParticipation);

            DataColumn colNoBackTrack = new DataColumn();
            colNoBackTrack.DataType = System.Type.GetType("System.String");
            colNoBackTrack.ColumnName = "No BackTrack";
            dtNonRenew.Columns.Add(colNoBackTrack);

            DataColumn colNoEmail = new DataColumn();
            colNoEmail.DataType = System.Type.GetType("System.String");
            colNoEmail.ColumnName = "No Email";
            dtNonRenew.Columns.Add(colNoEmail);

            DataColumn colOther = new DataColumn();
            colOther.DataType = System.Type.GetType("System.String");
            colOther.ColumnName = "Other";
            dtNonRenew.Columns.Add(colOther);

            DataColumn colCardMade = new DataColumn();
            colCardMade.DataType = System.Type.GetType("System.String");
            colCardMade.ColumnName = "Card Made";
            dtNonRenew.Columns.Add(colCardMade);

            DataColumn colDatePaid = new DataColumn();
            colDatePaid.DataType = System.Type.GetType("System.String");
            colDatePaid.ColumnName = "Date Paid";
            dtNonRenew.Columns.Add(colDatePaid);

            return dtNonRenew;
        }

        private DataTable createReport()
        {
            SqlCommand cmd = new SqlCommand("qryReport", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dtReport = ReportTableModel();

            DataRow headers = dtReport.NewRow();
            for (int col = 0; col < dtReport.Columns.Count; col++)
            {
                headers[col] = dtReport.Columns[col].ColumnName;
            }
            dtReport.Rows.Add(headers);

            while (rdr.Read())
            {
                DataRow dr = dtReport.NewRow();
                dr["Year"] = rdr["YearPaid"];
                dr["Total"] = rdr["Total"];
                dr["New Members"] = rdr["totalNew"];
                dr["Renewals"] = rdr["renewals"];
                dr["Archery"] = rdr["Archery"];
                dr["Handgun"] = rdr["Handgun"];
                dr["Smallbore"] = rdr["Smallbore"];
                dr["Rifle"] = rdr["Rifle"];
                dr["Action Pistol"] = rdr["ActionPistol"];
                dtReport.Rows.Add(dr);
            }
            return dtReport;

        }

        private DataTable ReportTableModel()
        {
            DataTable dtReport = new DataTable("Report");

            DataColumn colYear = new DataColumn();
            colYear.DataType = System.Type.GetType("System.String");
            colYear.ColumnName = "Year";
            dtReport.Columns.Add(colYear);

            DataColumn colTotal = new DataColumn();
            colTotal.DataType = System.Type.GetType("System.String");
            colTotal.ColumnName = "Total";
            dtReport.Columns.Add(colTotal);

            DataColumn colFamily = new DataColumn();
            colFamily.DataType = System.Type.GetType("System.String");
            colFamily.ColumnName = "Family Members";
            dtReport.Columns.Add(colFamily);

            DataColumn colNew = new DataColumn();
            colNew.DataType = System.Type.GetType("System.String");
            colNew.ColumnName = "New Members";
            dtReport.Columns.Add(colNew);

            DataColumn colRenew = new DataColumn();
            colRenew.DataType = System.Type.GetType("System.String");
            colRenew.ColumnName = "Renewals";
            dtReport.Columns.Add(colRenew);

            DataColumn colArchery = new DataColumn();
            colArchery.DataType = System.Type.GetType("System.String");
            colArchery.ColumnName = "Archery";
            dtReport.Columns.Add(colArchery);

            DataColumn colHandgun = new DataColumn();
            colHandgun.DataType = System.Type.GetType("System.String");
            colHandgun.ColumnName = "Handgun";
            dtReport.Columns.Add(colHandgun);

            DataColumn colSmallbore = new DataColumn();
            colSmallbore.DataType = System.Type.GetType("System.String");
            colSmallbore.ColumnName = "Smallbore";
            dtReport.Columns.Add(colSmallbore);

            DataColumn colRifle = new DataColumn();
            colRifle.DataType = System.Type.GetType("System.String");
            colRifle.ColumnName = "Rifle";
            dtReport.Columns.Add(colRifle);

            DataColumn colAction = new DataColumn();
            colAction.DataType = System.Type.GetType("System.String");
            colAction.ColumnName = "Action Pistol";
            dtReport.Columns.Add(colAction);

            return dtReport;
        }

        private DataTable createMemberSectionReport()
        {

            int year = cHelper.thisYear();
            String qryMembers = @"SELECT Card, [Last Name], [First Name], Walk, SectionFlag
                            FROM qryExport
                            WHERE (YearPaid = " + year + ") OR (MemberTypeID = 6)";

            SqlCommand cmd = new SqlCommand(qryMembers, cnn);
            DataTable dtMembers = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtMembers);
            DataTable dtReport = MemberSectionModel();
            BitField section = new BitField();

            DataRow headers = dtReport.NewRow();
            for (int col = 0; col < dtReport.Columns.Count; col++)
            {
                headers[col] = dtReport.Columns[col].ColumnName;
            }
            dtReport.Rows.Add(headers);

            for (int row = 0; row < dtMembers.Rows.Count; row++)
            {
                section.Mask = ulong.Parse(dtMembers.Rows[row]["SectionFlag"].ToString());
                DataRow dr = dtReport.NewRow();
                dr["Card"] = dtMembers.Rows[row]["Card"].ToString();
                dr["Last Name"] = dtMembers.Rows[row]["last name"].ToString();
                dr["First Name"] = dtMembers.Rows[row]["first name"].ToString();
                dr["Handgun"] = section.AnyOn(BitField.Flag.f2) ? "Yes" : "";
                dr["Action"] = section.AnyOn(BitField.Flag.f6) ? "Yes" : "";
                dr["Rifle"] = section.AnyOn(BitField.Flag.f5) ? "Yes" : "";
                dr["Smallbore"] = section.AnyOn(BitField.Flag.f3) ? "Yes" : "";
                dr["Archery"] = section.AnyOn(BitField.Flag.f1) ? "Yes" : "";
                dr["Safety Walk"] = dtMembers.Rows[row]["Walk"].ToString() == "Done" ? "Yes" : "No";
                //dr["Extra Card"] = dtMembers.Rows[row]["Master Record"].ToString().Length > 0 ? "Yes" : "No";
                dtReport.Rows.Add(dr);
            }
            da.Dispose();
            return dtReport;
        }

        private DataTable MemberSectionModel()
        {
            DataTable dtReport = new DataTable("Member Sections");

            DataColumn colCard = new DataColumn();
            colCard.DataType = System.Type.GetType("System.String");
            colCard.ColumnName = "Card";
            dtReport.Columns.Add(colCard);

            DataColumn colLastName = new DataColumn();
            colLastName.DataType = System.Type.GetType("System.String");
            colLastName.ColumnName = "Last Name";
            dtReport.Columns.Add(colLastName);

            DataColumn colFirstName = new DataColumn();
            colFirstName.DataType = System.Type.GetType("System.String");
            colFirstName.ColumnName = "First Name";
            dtReport.Columns.Add(colFirstName);

            DataColumn colHandgun = new DataColumn();
            colHandgun.DataType = System.Type.GetType("System.String");
            colHandgun.ColumnName = "Handgun";
            dtReport.Columns.Add(colHandgun);

            DataColumn colAction = new DataColumn();
            colAction.DataType = System.Type.GetType("System.String");
            colAction.ColumnName = "Action";
            dtReport.Columns.Add(colAction);

            DataColumn colRifle = new DataColumn();
            colRifle.DataType = System.Type.GetType("System.String");
            colRifle.ColumnName = "Rifle";
            dtReport.Columns.Add(colRifle);

            DataColumn colSmallbore = new DataColumn();
            colSmallbore.DataType = System.Type.GetType("System.String");
            colSmallbore.ColumnName = "Smallbore";
            dtReport.Columns.Add(colSmallbore);

            DataColumn colArchery = new DataColumn();
            colArchery.DataType = System.Type.GetType("System.String");
            colArchery.ColumnName = "Archery";
            dtReport.Columns.Add(colArchery);

            DataColumn colSafety = new DataColumn();
            colSafety.DataType = System.Type.GetType("System.String");
            colSafety.ColumnName = "Safety Walk";
            dtReport.Columns.Add(colSafety);

            DataColumn colExtra = new DataColumn();
            colExtra.DataType = System.Type.GetType("System.String");
            colExtra.ColumnName = "Extra Card";
            dtReport.Columns.Add(colExtra);

            return dtReport;
        }

        private DataTable createNonMemberSectionReport() {

            int year = cHelper.thisYear();
            String qryNonRenewals = @"SELECT Card, [Last Name], [First Name], Age, [Membership Type], Walk, [Pal Exp Date], Pal, Swipe, YearPaid, Phone,
                        Cell, [Email Address], [Date Joined], [Website Usernames], Notes, Sponsor, SectionFlag, Participation, 
                        NoBackTrack, NoEmailing, pOther, CardMade, DatePaid
                    FROM qryExport
                    JOIN (
                            SELECT ID, Max(YearPaid) AS MaxYearPaid 
                            FROM qryExport GROUP BY ID HAVING Max(YearPaid) = " + (year - 1) + @") AS MaxPaid
                    ON qryExport.ID = MaxPaid.ID";

            SqlCommand cmd = new SqlCommand(qryNonRenewals, cnn);
            DataTable dtNon = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtNon);
            DataTable dtReport = NonMemberSectionModel();
            BitField section = new BitField();

            DataRow headers = dtReport.NewRow();
            for (int col = 0; col < dtReport.Columns.Count; col++)
            {
                headers[col] = dtReport.Columns[col].ColumnName;
            }
            dtReport.Rows.Add(headers);

            for (int row = 0; row < dtNon.Rows.Count; row++)
            {
                section.Mask = ulong.Parse(dtNon.Rows[row]["SectionFlag"].ToString());
                DataRow dr = dtReport.NewRow();
                dr["Card"] = dtNon.Rows[row]["card"].ToString();
                dr["Last Name"] = dtNon.Rows[row]["last name"].ToString();
                dr["First Name"] = dtNon.Rows[row]["first name"].ToString();
                dr["Handgun"] = section.AnyOn(BitField.Flag.f2) ? "Yes" : "";
                dr["Action"] = section.AnyOn(BitField.Flag.f6) ? "Yes" : "";
                dr["Rifle"] = section.AnyOn(BitField.Flag.f5) ? "Yes" : "";
                dr["Smallbore"] = section.AnyOn(BitField.Flag.f3) ? "Yes" : "";
                dr["Archery"] = section.AnyOn(BitField.Flag.f1) ? "Yes" : "";
                dr["Safety Walk"] = dtNon.Rows[row]["Walk"].ToString() == "Done" ? "Yes" : "No";
                dtReport.Rows.Add(dr);
            }
            da.Dispose();
            return dtReport;

        }

        private DataTable NonMemberSectionModel()
        {
            DataTable dtReport = new DataTable("NonMember Sections");

            DataColumn colCard = new DataColumn();
            colCard.DataType = System.Type.GetType("System.String");
            colCard.ColumnName = "Card";
            dtReport.Columns.Add(colCard);

            DataColumn colLastName = new DataColumn();
            colLastName.DataType = System.Type.GetType("System.String");
            colLastName.ColumnName = "Last Name";
            dtReport.Columns.Add(colLastName);

            DataColumn colFirstName = new DataColumn();
            colFirstName.DataType = System.Type.GetType("System.String");
            colFirstName.ColumnName = "First Name";
            dtReport.Columns.Add(colFirstName);

            DataColumn colHandgun = new DataColumn();
            colHandgun.DataType = System.Type.GetType("System.String");
            colHandgun.ColumnName = "Handgun";
            dtReport.Columns.Add(colHandgun);

            DataColumn colAction = new DataColumn();
            colAction.DataType = System.Type.GetType("System.String");
            colAction.ColumnName = "Action";
            dtReport.Columns.Add(colAction);

            DataColumn colRifle = new DataColumn();
            colRifle.DataType = System.Type.GetType("System.String");
            colRifle.ColumnName = "Rifle";
            dtReport.Columns.Add(colRifle);

            DataColumn colSmallbore = new DataColumn();
            colSmallbore.DataType = System.Type.GetType("System.String");
            colSmallbore.ColumnName = "Smallbore";
            dtReport.Columns.Add(colSmallbore);

            DataColumn colArchery = new DataColumn();
            colArchery.DataType = System.Type.GetType("System.String");
            colArchery.ColumnName = "Archery";
            dtReport.Columns.Add(colArchery);

            DataColumn colSafety = new DataColumn();
            colSafety.DataType = System.Type.GetType("System.String");
            colSafety.ColumnName = "Safety Walk";
            dtReport.Columns.Add(colSafety);

            DataColumn colExtra = new DataColumn();
            colExtra.DataType = System.Type.GetType("System.String");
            colExtra.ColumnName = "Extra Card";
            dtReport.Columns.Add(colExtra);

            return dtReport;
        }
    }
}
