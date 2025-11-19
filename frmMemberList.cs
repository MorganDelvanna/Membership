using OdsReadWrite;
using PFGA_Membership.MembershipTableAdapters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Windows.Forms;
using System.Web.Script;


namespace PFGA_Membership
{
    public partial class frmMemberList : Form
    {
        
        MembershipTableAdapters.MemberListTableAdapter daMemberList = new PFGA_Membership.MembershipTableAdapters.MemberListTableAdapter();
        Membership.MemberListDataTable dtMemberList = new Membership.MemberListDataTable();
        DataView dvMemberList;
        clsHelpers cHelper = new clsHelpers();

        public frmMemberList()
        {
            InitializeComponent();
        }

        private void frmMemberList_Load(object sender, EventArgs e)
        {
            this.membershipTypeTableAdapter.Fill(this.membership.MembershipType);
            try
            {
                this.dgMemberList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMemberList_CellClick);
                BindGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Loading frmMemberList: ", ex, true);
            }
        }

        private void BindGrid()
        {
            try
            {
                DataColumn[] keys = new DataColumn[1];

                dgMemberList.AutoGenerateColumns = false;
                dgMemberList.Columns["colID"].DataPropertyName = "ID";
                dgMemberList.Columns["colPaid"].DataPropertyName = "MaxYearPaid";
                dgMemberList.Columns["colCard"].DataPropertyName = "Card";
                dgMemberList.Columns["colFirstName"].DataPropertyName = "First Name";
                dgMemberList.Columns["colLastName"].DataPropertyName = "Last Name";
                dgMemberList.Columns["colMemberType"].DataPropertyName = "Membership Type";
                dgMemberList.Columns["colWalk"].DataPropertyName = "Walk";
                dgMemberList.Columns["colYear"].DataPropertyName = "MaxOfMembershipYear";
                dgMemberList.Columns["colDateJoined"].DataPropertyName = "Date Joined";

                daMemberList.Fill(dtMemberList);
                keys[0] = dtMemberList.Columns["ID"];
                dtMemberList.PrimaryKey = keys;
                dvMemberList = new DataView(dtMemberList);
                dvMemberList.Sort = "MaxYearPaid DESC, [Last Name] ASC";
                dgMemberList.DataSource = dvMemberList;

                dgMemberList.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error in BindGrid: ", ex, true);
            }
        }

        private void reFormatGrid()
        {
            int iYear;
            int curYear;
            int colWalk; 
            int colYear;

            StringBuilder sFilter = new StringBuilder();

            if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 9)
            {
                curYear = DateTime.Today.Year - 1;
            }
            else
            {
                curYear = DateTime.Today.Year;
            }

            try
            {
                if (chkGeneral.Checked == true)
                {
                    if (sFilter.Length > 0)
                    {
                        sFilter.Append(" AND ");
                    }
                    sFilter.Append("Active = 0 or Active = 1");
                }
                else
                {
                    if (sFilter.Length > 0)
                    {
                        sFilter.Append(" AND ");
                    }
                    sFilter.Append("Active = 1");
                }

                dvMemberList.RowFilter = sFilter.ToString();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error filtering list", ex, true);
            }

            try
            {
                colWalk = dgMemberList.Columns["colWalk"].Index;
                colYear = dgMemberList.Columns["colPaid"].Index;

                foreach (DataGridViewRow Row in dgMemberList.Rows)
                {

                    if (Row.Cells[colWalk].Value.ToString() != null)
                    {
                        if (Row.Cells[colWalk].Value.ToString() != "Done" && chkWalk.Checked)
                        {
                            Row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else
                        {
                            Row.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (chkWalk.Checked)
                        {
                            Row.DefaultCellStyle.BackColor = Color.Yellow;
                        }
                    }

                    int.TryParse(Row.Cells[colYear].Value.ToString(), out iYear);

                    if (iYear == curYear)
                    {
                        if (Row.DefaultCellStyle.BackColor != Color.Yellow)
                        {
                            Row.DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                    else
                    {
                        if (chkPaid.Checked)
                        {
                            Row.DefaultCellStyle.BackColor = Color.Red;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error colouring the List", ex, true);
            }
            dgMemberList.Refresh();

        }

        private void dgMemberList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgMemberList.Columns["btnWalk"].Index)
            {
                UpdateWalk(e);
            }
            else if (e.ColumnIndex == dgMemberList.Columns["btnEdit"].Index)
            {
                EditMember(e);
            }
            else if (e.ColumnIndex == dgMemberList.Columns["btnDelete"].Index)
            {
                DeleteMember(e);
            }
        }

        private void UpdateWalk(DataGridViewCellEventArgs e)
        {
            try
            {
                int ID = Int32.Parse(dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colID"].Index].Value.ToString());

                MembershipTableAdapters.MembersTableAdapter da = new PFGA_Membership.MembershipTableAdapters.MembersTableAdapter();
                Membership.MembersDataTable dt = new Membership.MembersDataTable();

                da.Fill(dt);
                dt.Rows.Find(ID)["Walk"] = "Done";
                da.Update(dt);

                dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colWalk"].Index].Value = "Done";
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error in UpdateWalk", ex, true);
            }

            reFormatGrid();

        }

        
        private void EditMember(DataGridViewCellEventArgs e)
        {
            try
            {
                int ID = Int32.Parse(dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colID"].Index].Value.ToString());

                frmParent frm = (frmParent)this.ParentForm;
                frm.editMember(ID);
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Editing Member: ", ex, true);
            }
        }

        private void DeleteMember(DataGridViewCellEventArgs e)
        {
            try
            {
                int id = Int32.Parse(dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colID"].Index].Value.ToString());
                string firstName = dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colFirstName"].Index].Value.ToString();
                string lastName = dgMemberList.Rows[e.RowIndex].Cells[dgMemberList.Columns["colLastName"].Index].Value.ToString();

                if (MessageBox.Show(string.Format("Do you really want to delete {0} {1}", firstName, lastName), "Delete Member"
                    , MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();
                    da.ArchiveMember(id);
                    MessageBox.Show(String.Format("{0} {1} Deleted", firstName, lastName));
                }
                reFormatGrid();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Deleting Member: ", ex, true);
            }
        }
 
        private void btnNew_Click(object sender, EventArgs e)
        {
            try
            {
                frmParent frm = (frmParent)this.ParentForm;
                frm.newMember();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to create a new user", ex, true);
            }
        }

        private void FilterMemberList(object sender, EventArgs e)
        {
            
            string filter = string.Empty;
            try
            {
                if (txtSearch.TextLength > 0)
                {
                    int test;
                    if (int.TryParse(txtSearch.Text, out test))
                    {
                        filter = String.Format("Card = {0}", txtSearch.Text);
                    }
                    else
                    {
                        filter = String.Format("[Last Name] LIKE '{0}*' OR [First Name] LIKE '{0}*' OR [Email Address] LIKE '{0}*' OR [Website UserNames] LIKE '{0}*' ", txtSearch.Text);
                    }
                    dvMemberList.RowFilter = filter;
                }
                else
                {
                    dvMemberList.RowFilter = filter;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error in FilterMemberList", ex, true);
            }
        }

        private void TextSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                FilterMemberList(txtSearch, new EventArgs());
            }
        }

        private void chkPaid_CheckedChanged(object sender, EventArgs e)
        {
            reFormatGrid();
        }

        private void chkWalk_CheckedChanged(object sender, EventArgs e)
        {
            reFormatGrid();
        }

        private void chkGeneral_CheckedChanged(object sender, EventArgs e)
        {
            reFormatGrid();
        }

        private void exportMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            DataSet dsExport;
            OdsReaderWriter odsWriter = new OdsReaderWriter();

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();
            
            try
            {
                Cursor.Current = Cursors.WaitCursor;

                cnn = new SqlConnection(config);
                cnn.Open();

                clsExport cExport = new clsExport(cnn);
                dsExport = cExport.exportMembers();
                
                String fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                            string.Concat("PFGAMembershipReport_", DateTime.Now.ToString("MMM"), DateTime.Now.Year.ToString(), ".ods"));

                odsWriter.WriteOdsFile(dsExport, fileName);

                cnn.Close();

                MessageBox.Show(string.Format("A report has been created on your desktop"), "Creating Report"
                    , MessageBoxButtons.OK);


            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
            finally
            {              
                Cursor.Current = Cursors.Default;
            }
        }

        private void mailingListEmailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SqlConnection cnn;
            SqlCommand cmd;
            DataTable dtEmails;
            SqlDataAdapter da;

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string fileName;

                cnn = new SqlConnection(config);
                cnn.Open();

                String qryEmails = $@"SELECT email FROM (SELECT [Email Address] as email FROM qryExport WHERE (YearPaid = {cHelper.thisYear()}) AND Len([Email Address]) > 0 AND NoBackTrack = 0 AND NoEmailing = 0) as Members 
                    UNION 
                    SELECT email FROM (SELECT [Email Address] as email FROM qryExportExtra WHERE (YearPaid = {cHelper.thisYear()}) AND Len([Email Address]) > 0) as Extra";

                cmd = new SqlCommand(qryEmails, cnn);
                dtEmails = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtEmails);

                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Mailing_List_Emails.txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    foreach (DataRow row in dtEmails.Rows)
                    {
                        if (row["email"].ToString().Length > 0)
                        {
                            file.WriteLine(row["email"].ToString());
                        }                        
                    }
                    file.Close();
                }


                qryEmails = @"SELECT [Email Address], [Website Usernames]
                    FROM qryExport
                    JOIN (SELECT ID, Max(YearPaid) AS MaxYearPaid  FROM qryExport GROUP BY ID HAVING Max(YearPaid) = " + (cHelper.thisYear() - 1) + @") AS MaxPaid
                        ON qryExport.ID = MaxPaid.ID
                    WHERE Len([Email Address]) > 0
                    UNION
                    SELECT [Email Address], [Website Usernames]
	                    FROM Members 
	                    WHERE ([NoBackTrack] = 1 OR [NoEmailing] = 1) AND  Len([Email Address]) > 0";

                cmd = new SqlCommand(qryEmails, cnn);
                dtEmails = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtEmails);

                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Remove_Emails.csv");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    foreach (DataRow row in dtEmails.Rows)
                    {
                        file.WriteLine(String.Concat(row["Email Address"].ToString(), ",", row["Website Usernames"].ToString()));
                    }
                    file.Close();
                }              
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Done", "Mailing List Emails", MessageBoxButtons.OK);
            }
        }

        private void makeCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();
            da.qryCards(cHelper.thisYear());

            frmParent frm = (frmParent)this.ParentForm;
            frm.showCards();
            this.Close();
            this.Dispose();
        }

        private void updateMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();

            da.UpdateCardMade();
            da.DeleteTempCards();
            MessageBox.Show("Members Updated");
        }

        private void toFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();
            da.UpdatePendingToFull();
            da.UpdateSeniors();
            BindGrid();
            MessageBox.Show("Members Updated");

        }

        private void toHalfYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();
            da.UpdatePendingToHalf();
            da.UpdateSeniors();
            BindGrid();
            MessageBox.Show("Members Updated");
        }

        private void viewCardLIstToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                frmParent frm = (frmParent)this.ParentForm;
                frm.showCardList();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Showing Card List: ", ex, true);
            }
        }

        private void pendingMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            string section;
            string fileName;
            string findSection = "<<Section>>";
            string template = Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "NewMemberWelcomeLetter.rtf");
            System.Data.SqlClient.SqlConnection cnn;
            clsHelpers helper = new clsHelpers();

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();
            
            cnn = new SqlConnection(config);
            cnn.Open();

            string query = @"SELECT Card, COALESCE(NULLIF([Email Address],''), [First Name] + ' ' + [Last Name]) AS Title, SectionFlag 
                            FROM            qryExport
                            WHERE (YearPaid = " + cHelper.thisYear() + ") AND (MemberTypeID = 13)";

            SqlCommand cmd = new SqlCommand(query, cnn);
            DataTable dtEmails = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtEmails);

            try
            {
                Cursor.Current = Cursors.WaitCursor;
      
                if (MessageBox.Show(string.Format("This will create {0} documents on your desktop", dtEmails.Rows.Count), "Creating Document"
                    , MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {

                    foreach (DataRow row in dtEmails.Rows)
                    {
                        // Copy and rename template
                        fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                            string.Concat(row["Title"].ToString(), ".rtf"));                       

                        richTextBox1.LoadFile(template, RichTextBoxStreamType.RichText);
                        int index;

                        section = helper.getSectionLabels(row["SectionFlag"].ToString());
                        if (section.Length > 0)
                        {
                            index = richTextBox1.Find(findSection);
                            if (index >= 0)
                            {
                                richTextBox1.SelectionStart = index;
                                richTextBox1.Select(index, findSection.Length);
                                richTextBox1.SelectedText = section;
                            }
                        }

                        index = richTextBox1.Find("<<CardNo>>");
                        if (index >= 0)
                        {
                            richTextBox1.SelectionStart = index;
                            richTextBox1.Select(index, "<<CardNo>>".Length);
                            richTextBox1.SelectedText = row["Card"].ToString();
                        }

                        richTextBox1.SaveFile(fileName, RichTextBoxStreamType.RichText);
                        richTextBox1.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate Pending emails", ex, true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show("Done", "Pending Member Emails", MessageBoxButtons.OK);
            }             
        }

        private void renewingMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string year;

            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "YearlyRenewalLetter.rtf"); ;
            string template = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "YearlyRenewalLetter.rtf");

            SqlConnection cnn;

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            cnn = new SqlConnection(config);
            cnn.Open();

            string query = @"SELECT Card, [Email Address], SectionFlag 
                            FROM            qryExport
                            WHERE " + string.Format("MembertypeId <> 13 AND DatePaid = '{0}'", DateTime.Today);

            SqlCommand cmd = new SqlCommand(query, cnn);
            DataTable dtEmails = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtEmails);

            if (dtEmails.Rows.Count > 0)
            {
                richTextBox1.LoadFile(template);

                year = (cHelper.thisYear() + 1).ToString();

                int index = richTextBox1.Find("<<Year>>");
                if (index >= 0)
                {
                    richTextBox1.SelectionStart = index;
                    richTextBox1.Select(index, "<<Year>>".Length);
                    richTextBox1.SelectedText = year;
                }
                richTextBox1.SaveFile(fileName);

                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                
                    if (MessageBox.Show("This will create 2 documents on your desktop", "Creating Document", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                    {
                        fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "RenewalAddresses.txt"); ;
                        using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                        {
                            foreach (DataRow row in dtEmails.Rows)
                            {
                                if (row["Email Address"].ToString().Length > 0)
                                {
                                    file.WriteLine(row["Email Address"].ToString());
                                }                                
                            }
                            file.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Error trying to generate Renewal Emails", ex, true);
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            } else
            {
                MessageBox.Show("No renewals", "Creating Document", MessageBoxButtons.OK);
            }           
        }

        private void cboMemberTypeFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            String filter = "[Membership Type] = '" + cboMemberTypeFilter.Text + "'";
            dvMemberList.RowFilter = filter;
            dgMemberList.Refresh();
        }

        private void halfToFullToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();

            da.UpdateHalftoFull();
            BindGrid();
            MessageBox.Show("Members Updated");
        }

        private void safetyWalkMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "OrientationWalkLetter.rtf"); ;
            string template = Path.Combine(System.IO.Path.GetDirectoryName(Application.ExecutablePath), "OrientationWalkLetter.rtf");

            System.Data.SqlClient.SqlConnection cnn;

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            cnn = new SqlConnection(config);
            cnn.Open();

            string query = @"SELECT Members.[First Name], Members.[Last Name], Members.[Email Address] FROM Members
                                INNER JOIN Members extra ON Members.ID = extra.MasterRecord
                                WHERE (Members.Walk = 'Need' OR extra.Walk = 'Need') AND Members.Archived = 0
                                GROUP BY Members.[First Name], Members.[Last Name], Members.[Email Address]
                                ORDER BY Members.[Last Name];";

            SqlCommand cmd = new SqlCommand(query, cnn);
            DataTable dtEmails = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtEmails);

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                if (MessageBox.Show("This will create 2 documents on your desktop", "Creating Document", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    File.Copy(template, fileName, true);

                    fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "OrientationWalkAddresses.txt");
                    using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                    {
                        foreach (DataRow row in dtEmails.Rows)
                        {
                            if (row["Email Address"].ToString().Length > 0)
                            {
                                file.WriteLine(row["Email Address"].ToString());
                            }                            
                        }
                        file.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate Renewal Emails", ex, true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // read connectionstring from config file
            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            Cursor.Current = Cursors.WaitCursor;

            // read backup folder from config file ("C:/temp/")
            var backupFolder = ConfigurationManager.AppSettings["BACKUP_PATH"];

            // set backupfilename (you will get something like: "C:/temp/MyDatabase-2013-12-07.bak")
            var backupFileName = String.Format("{0}PFGAMembership-{1}.bak",
                backupFolder,
                DateTime.Now.ToString("yyyy-MM-dd"));

            using (var connection = new SqlConnection(config))
            {
                var query = String.Format("BACKUP DATABASE PFGAMembership TO DISK='{0}'", backupFileName);

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }

            Cursor.Current = Cursors.Default;
            MessageBox.Show("BackUp Done");
        }

        private void cardAvailableListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection cnn;
            SqlCommand cmd;
            DataTable dtEmails;
            SqlDataAdapter da;

            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            try
            {
                Cursor.Current = Cursors.WaitCursor;

                string fileName;

                cnn = new SqlConnection(config);
                cnn.Open();

                String qryEmails = $@"SELECT [Email Address] email 
                    FROM Cards 
                    INNER JOIN Members ON Members.ID = Cards.ID
                    WHERE DATALENGTH(Picture) <> 0";


                cmd = new SqlCommand(qryEmails, cnn);
                dtEmails = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtEmails);

                fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Cards_Available.txt");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName))
                {
                    foreach (DataRow row in dtEmails.Rows)
                    {
                        file.WriteLine(row["email"].ToString());
                    }
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        private void exportToAtriumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmParent frm = (frmParent)this.ParentForm;
            frm.showAtrium();
            this.Close();
            this.Dispose();
        }
    }
}


