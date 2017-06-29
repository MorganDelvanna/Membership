﻿using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
//using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.Collections.Generic;
using System.IO;


namespace PFGA_Membership
{
    public partial class frmMemberList : Form
    {
        
        MembershipTableAdapters.MemberListTableAdapter daMemberList = new PFGA_Membership.MembershipTableAdapters.MemberListTableAdapter();
        Membership.MemberListDataTable dtMemberList = new Membership.MemberListDataTable();
        DataView dvMemberList;
        
        public frmMemberList()
        {
            InitializeComponent();
        }

        private void frmMemberList_Load(object sender, EventArgs e)
        {
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

        private void reFormatGrid(int Index)
        {
            int iYear = -1;
            int curYear;
            int colWalk; 
            int colYear;
            DateTime dYear = DateTime.MaxValue;
            DateTime dTest = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.DaysInMonth(DateTime.Today.Year, DateTime.Today.Month));
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

            reFormatGrid(e.RowIndex);

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
            reFormatGrid(0);
        }

        private void chkWalk_CheckedChanged(object sender, EventArgs e)
        {
            reFormatGrid(0);
        }

        private void chkGeneral_CheckedChanged(object sender, EventArgs e)
        {
            reFormatGrid(0);
        }

        private void btnMailing_Click(object sender, EventArgs e)
        {
            try
            {
                frmParent frm = (frmParent)this.ParentForm;
                frm.showLabels();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate labels", ex, true);
            }
        }

        private void mnuSummary_Click(object sender, EventArgs e)
        {
            try
            {
                frmParent frm = (frmParent)this.ParentForm;
                frm.showSummary();
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
        }

        private void exportMembersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            int nYear = 0;
            int nCount = 0;
            int newCount = 0;
            int renewCount = 0;
            BitField  section = new BitField();
            int[] SectionCounts = new int[6];
            DateTime dateJoined;
            DateTime startDate;
            DateTime endDate;
            
            MembershipTableAdapters.qryExportTableAdapter daExport = new PFGA_Membership.MembershipTableAdapters.qryExportTableAdapter();
            MembershipTableAdapters.qryExportExtraTableAdapter daExtra = new PFGA_Membership.MembershipTableAdapters.qryExportExtraTableAdapter();
            MembershipTableAdapters.qryExportNonTableAdapter daNon = new PFGA_Membership.MembershipTableAdapters.qryExportNonTableAdapter();
            Membership.qryExportDataTable dtExport = new Membership.qryExportDataTable();
            Membership.qryExportExtraDataTable dtExtra = new Membership.qryExportExtraDataTable();
            Membership.qryExportNonDataTable dtNon = new Membership.qryExportNonDataTable();
                        
            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                oSheet.Name = string.Format("Members {0}", thisYear().ToString());

                daExport.Fill(dtExport, thisYear());

                //Add table headers going cell by cell.
                for (int col = 0; col < dtExport.Columns.Count; col++)
                {
                    oSheet.Cells[1, col + 1] = dtExport.Columns[col].ColumnName;
                    for (int row = 0; row < dtExport.Rows.Count; row++)
                    {
                        if (dtExport.Columns[col].ColumnName == "Section")
                        {
                            oSheet.Cells[row + 2, col + 1] = getSectionLabels(dtExport.Rows[row][col].ToString());
                        }
                        else if (dtExport.Columns[col].ColumnName == "Participation")
                        {
                            oSheet.Cells[row + 2, col + 1] = getParticipation(dtExport.Rows[row][col].ToString());
                        }
                        else
                        {
                            oSheet.Cells[row + 2, col + 1] = dtExport.Rows[row][col].ToString();
                        }
                    }
                }
                
                //Format A1:D1 as bold, vertical alignment = center.
                string lastCol = string.Format("{0}1", Number2String(dtExport.Columns.Count));
                oSheet.get_Range("A1", lastCol).Font.Bold = true;
                oSheet.get_Range("A1", lastCol).VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", lastCol).EntireColumn.AutoFit();
                oSheet.get_Range("E2", string.Format("E{0}", dtExport.Rows.Count+1)).NumberFormat = "yyyy-mm-dd";
                oSheet.get_Range("H2", string.Format("H{0}", dtExport.Rows.Count+1)).NumberFormat = "yyyy-mm-dd";
                oSheet.get_Range("Q2", string.Format("Q{0}", dtExport.Rows.Count+1)).NumberFormat = "yyyy-mm-dd";

                daExtra.Fill(dtExtra, thisYear());

                oSheet = (Excel._Worksheet)oWB.Sheets.Add(System.Reflection.Missing.Value, oWB.Sheets[oWB.Sheets.Count], 1, Excel.XlSheetType.xlWorksheet);
                oSheet.Name = "Extra Cards";

                //Add table headers going cell by cell.
                for (int col = 0; col < dtExtra.Columns.Count; col++)
                {
                    oSheet.Cells[1, col + 1] = dtExtra.Columns[col].ColumnName;
                    for (int row = 0; row < dtExtra.Rows.Count; row++)
                    {
                        if (dtExtra.Columns[col].ColumnName == "Section")
                        {
                            oSheet.Cells[row + 2, col + 1] = getSectionLabels(dtExtra.Rows[row][col].ToString());
                        }
                        else if (dtExtra.Columns[col].ColumnName == "Participation")
                        {
                            oSheet.Cells[row + 2, col + 1] = getParticipation(dtExtra.Rows[row][col].ToString());
                        }
                        else
                        {
                            oSheet.Cells[row + 2, col + 1] = dtExtra.Rows[row][col].ToString();
                        }
                    }
                }

                //Format A1:D1 as bold, vertical alignment = center.
                lastCol = string.Format("{0}1", Number2String(dtExtra.Columns.Count));
                oSheet.get_Range("A1", lastCol).Font.Bold = true;
                oSheet.get_Range("A1", lastCol).VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", lastCol).EntireColumn.AutoFit();
                oSheet.get_Range("E2", string.Format("E{0}", dtExtra.Rows.Count + 1)).NumberFormat = "yyyy-mm-dd";
                oSheet.get_Range("H2", string.Format("H{0}", dtExtra.Rows.Count + 1)).NumberFormat = "yyyy-mm-dd";

                daNon.Fill(dtNon, thisYear() - 1);

                oSheet = (Excel._Worksheet)oWB.Sheets.Add(System.Reflection.Missing.Value, oWB.Sheets[oWB.Sheets.Count], 1, Excel.XlSheetType.xlWorksheet);
                oSheet.Name = "Not Renewed";

                //Add table headers going cell by cell.
                for (int col = 0; col < dtNon.Columns.Count; col++)
                {
                    oSheet.Cells[1, col + 1] = dtNon.Columns[col].ColumnName;
                    for (int row = 0; row < dtNon.Rows.Count; row++)
                    {
                        if (dtNon.Columns[col].ColumnName == "Section")
                        {
                            oSheet.Cells[row + 2, col + 1] = getSectionLabels(dtNon.Rows[row][col].ToString());
                        }
                        else
                        {
                            oSheet.Cells[row + 2, col + 1] = dtNon.Rows[row][col].ToString();
                        }
                    }
                }

                //Format A1:D1 as bold, vertical alignment = center.
                lastCol = string.Format("{0}1", Number2String(dtNon.Columns.Count));
                oSheet.get_Range("A1", lastCol).Font.Bold = true;
                oSheet.get_Range("A1", lastCol).VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", lastCol).EntireColumn.AutoFit();
                oSheet.get_Range("E2", string.Format("E{0}", dtNon.Rows.Count)).NumberFormat = "yyyy-mm-dd";
                oSheet.get_Range("H2", string.Format("H{0}", dtNon.Rows.Count)).NumberFormat = "yyyy-mm-dd";
                oSheet.get_Range("Q2", string.Format("Q{0}", dtNon.Rows.Count)).NumberFormat = "yyyy-mm-dd";

                oSheet = (Excel._Worksheet)oWB.Sheets.Add(System.Reflection.Missing.Value, oWB.Sheets[oWB.Sheets.Count], 1, Excel.XlSheetType.xlWorksheet);
                oSheet.Name = "Report";

                //Add table headers going cell by cell.
                oSheet.Cells[1, 1] = "Year";
                oSheet.Cells[1, 2] = "Total";
                oSheet.Cells[1, 3] = "New Members";
                oSheet.Cells[1, 4] = "Renewals";
                oSheet.Cells[1, 5] = "Archery";
                oSheet.Cells[1, 6] = "Handgun";
                oSheet.Cells[1, 7] = "Smallbore";
                oSheet.Cells[1, 8] = "SCA";
                oSheet.Cells[1, 9] = "Rifle";
                oSheet.Cells[1, 10] = "Action";

                nCount = 2;
                for (nYear = 2008; nYear <= thisYear(); nYear++)
                {
                    newCount = 0;
                    renewCount = 0;
                    SectionCounts = new int[6];

                    oSheet.Cells[nCount, 1] = nYear;
                    daExport.Fill(dtExport, nYear);
                    oSheet.Cells[nCount, 2] = dtExport.Rows.Count;
                    for (int row = 0; row < dtExport.Rows.Count; row++)
                    {
                        if (DateTime.TryParse(dtExport[row]["Date Joined"].ToString(), out dateJoined) == false)
                        {
                            dateJoined = new DateTime(1900, 01, 01);
                        }
                        startDate = new DateTime(nYear, 09, 01);
                        endDate = new DateTime(nYear + 1, 08, 31);
                        if (dateJoined >= startDate && dateJoined <= endDate)
                        {
                            newCount++;
                        }
                        if (dateJoined < startDate)
                        {
                            renewCount++;
                        }

                        section.Mask = ulong.Parse(dtExport.Rows[row]["Section"].ToString());
                        if (section.AnyOn(BitField.Flag.f1)) // Archery
                        {
                            SectionCounts[0] += 1;
                        }

                        if (section.AnyOn(BitField.Flag.f2)) //Handgun
                        {
                            SectionCounts[1] += 1;
                        }

                        if (section.AnyOn(BitField.Flag.f3)) //Smallbore
                        {
                            SectionCounts[2] += 1;
                        }

                        if (section.AnyOn(BitField.Flag.f4)) //SCA
                        {
                            SectionCounts[3] += 1;
                        }

                        if (section.AnyOn(BitField.Flag.f5)) //Rifle
                        {
                            SectionCounts[4] += 1;
                        }

                        if (section.AnyOn(BitField.Flag.f6)) //Action
                        {
                            SectionCounts[5] += 1;
                        }
                    }
                    oSheet.Cells[nCount, 3] = newCount;
                    oSheet.Cells[nCount, 4] = renewCount;
                    oSheet.Cells[nCount, 5] = SectionCounts[0];
                    oSheet.Cells[nCount, 6] = SectionCounts[1];
                    oSheet.Cells[nCount, 7] = SectionCounts[2];
                    oSheet.Cells[nCount, 8] = SectionCounts[3];
                    oSheet.Cells[nCount, 9] = SectionCounts[4];
                    oSheet.Cells[nCount, 10] = SectionCounts[5];
                    nCount++;
                }

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "J1").Font.Bold = true;
                oSheet.get_Range("A1", "J1").VerticalAlignment =
                    Excel.XlVAlign.xlVAlignCenter;
                oSheet.get_Range("A1", "J1").EntireColumn.AutoFit();

                //Make sure Excel is visible and give the user control
		        //of Microsoft Excel's lifetime.
		        oXL.Visible = true;
		        oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
        }

        private String Number2String(int number)
        {
            Char c;
            string retval; 

            if (number <= 25)
            {
                c = (Char)(65 + (number - 1));
                retval = c.ToString();
            }
            else
            {
                c = (Char)(65 + (number - 26));
                retval = string.Concat("A", c.ToString());
            }
            
            return retval;
        }

        private int thisYear()
        {
            int retVal;

            if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 9)
            {
                retVal = DateTime.Today.Year -1;
            }
            else
            {
                retVal = DateTime.Today.Year;
            }

            return retVal;
        }

        private string getSectionLabels(string sectionFlag)
        {
            StringBuilder retVal = new StringBuilder();
            BitField section = new BitField();
            ulong sectionMask = 0;

            if (ulong.TryParse(sectionFlag, out sectionMask))
            {

                section.Mask = sectionMask;

                try
                {
                    if (section.AnyOn(BitField.Flag.f1)) //
                    {
                        retVal.Append("Archery, ");
                    }

                    if (section.AnyOn(BitField.Flag.f2)) //
                    {
                        retVal.Append("Handgun, ");
                    }

                    if (section.AnyOn(BitField.Flag.f3)) //
                    {
                        retVal.Append("Smallbore, ");
                    }

                    if (section.AnyOn(BitField.Flag.f4)) //
                    {
                        retVal.Append("SCA, ");
                    }

                    if (section.AnyOn(BitField.Flag.f5)) //
                    {
                        retVal.Append("Rifle, ");
                    }

                    if (section.AnyOn(BitField.Flag.f6)) //
                    {
                        retVal.Append("Action, ");
                    }

                    if (retVal.Length > 0)
                    {
                        retVal.Remove(retVal.Length - 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Error setting Section Checkboxes", ex, true);
                }
            }

            return retVal.ToString();
        }

        private string getParticipation(string participationFlag)
        {
            StringBuilder retVal = new StringBuilder();
            BitField participation = new BitField();
            ulong participationMask = 0;

            if (ulong.TryParse(participationFlag, out participationMask))
            {
                participation.Mask = participationMask;

                try
                {
                    if (participation.AnyOn(BitField.Flag.f1)) //
                    {
                        retVal.Append("Work Party, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f2)) //
                    {
                        retVal.Append("Events, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f3)) //
                    {
                        retVal.Append("Executive, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f4)) //
                    {
                        retVal.Append("Range Officer, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f5)) //
                    {
                        retVal.Append("Training Officer, ");
                    }

                    if (participation.AnyOn(BitField.Flag.f6)) //
                    {
                        retVal.Append("Other ");
                    }

                    if (retVal.Length > 0)
                    {
                        retVal.Remove(retVal.Length - 2, 2);
                    }
                }
                catch (Exception ex)
                {
                    ErrorLogger.Log("Error setting Section Checkboxes", ex, true);
                }
            }

            return retVal.ToString();
        }

        private void mailingListEmailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;

            MembershipTableAdapters.qryExportTableAdapter daEmails = new PFGA_Membership.MembershipTableAdapters.qryExportTableAdapter();
            MembershipTableAdapters.qryExportNonTableAdapter daRemove = new PFGA_Membership.MembershipTableAdapters.qryExportNonTableAdapter();
            Membership.qryExportDataTable dtEmails = new Membership.qryExportDataTable();
            Membership.qryExportNonDataTable dtRemove = new Membership.qryExportNonDataTable();

            try
            {
                int lastRow;
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = false;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                oSheet.Name = string.Format("Members {0}", thisYear().ToString());

                daEmails.FillEmails(dtEmails, thisYear());

                for (int row = 1; row < dtEmails.Rows.Count; row++)
                {
                    oSheet.Cells[row, 1] = dtEmails.Rows[row]["Email Address"].ToString();
                    oSheet.Cells[row, 2] = dtEmails.Rows[row]["Website Usernames"].ToString();
                }
                oSheet.get_Range("A1", "A1").EntireColumn.AutoFit();

                dtEmails.Rows.Clear();
                daEmails.FillRemoveEmails(dtEmails, thisYear());

                oSheet = (Excel._Worksheet)oWB.Sheets.Add(System.Reflection.Missing.Value, oWB.Sheets[oWB.Sheets.Count], 1, Excel.XlSheetType.xlWorksheet);
                oSheet.Name = "Remove";

                for (int row = 1; row < dtEmails.Rows.Count; row++)
                {
                    oSheet.Cells[row, 1] = dtEmails.Rows[row]["Email Address"].ToString();
                    oSheet.Cells[row, 2] = dtEmails.Rows[row]["Website Usernames"].ToString();
                }

                lastRow = dtEmails.Rows.Count - 1;

                daRemove.FillRemoveEmails(dtRemove, thisYear() - 1);
                for (int row = 1; row < dtRemove.Rows.Count; row++)
                {
                    oSheet.Cells[row + lastRow, 1] = dtRemove.Rows[row]["Email Address"].ToString();
                    oSheet.Cells[row + lastRow, 2] = dtRemove.Rows[row]["Website Usernames"].ToString();
                }

                lastRow += dtRemove.Rows.Count;
                oSheet.get_Range("A1", string.Format("A{0}", lastRow)).EntireColumn.AutoFit();

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;

            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate summary report", ex, true);
            }
        }

        private void renewalToolStripMenuItem_Click(object sender, EventArgs e)
        {
         /*   Word.Application App;
            Word.Document Doc;
            //OBJECT OF MISSING "NULL VALUE"
            object oMissing = System.Reflection.Missing.Value;
            object fileName = "C:\\Program Files (x86)\\PFGA Membership\\Membership.doc";
            object bmRenewalYear = "RenewalYear";
            object oConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=\"C:\\Program Files (x86)\\PFGA Membership\\MembershipDB.accdb\"";
            object oQuery = string.Format("SELECT * FROM 'qryExport' WHERE 'YearPaid'={0}", DateTime.Now.Year);

            App = new Word.Application();
            App.Visible = true;
            Doc = App.Documents.Open(ref fileName, ref oMissing, ref oMissing, ref oMissing, 
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, 
                ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            Word.Bookmark bm = Doc.Bookmarks.get_Item(ref bmRenewalYear);
            Word.Range rng = bm.Range;
            rng.Text = string.Format("Sept {0} to Sept {1}", DateTime.Now.Year, DateTime.Now.Year + 1);
            Doc.MailMerge.OpenDataSource("C:\\Program Files (x86)\\PFGA Membership\\MembershipDB.accdb", ref oMissing, ref oMissing, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oConn, ref oMissing,
                 ref oMissing, ref oMissing, ref oMissing);

           */ 
        }

        private void welcomeLettersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            /*
            Word.Application App;
            Word.Document Doc;
            MembershipTableAdapters.PendingTableAdapter taPending = new PFGA_Membership.MembershipTableAdapters.PendingTableAdapter();
            Membership.PendingDataTable tbPending = new Membership.PendingDataTable();

            //OBJECT OF MISSING "NULL VALUE"
            Object oMissing = System.Reflection.Missing.Value;
            Object fileName = "C:\\Program Files (x86)\\PFGA Membership\\NewMemberWelcomeLetter.doc";
            Object bmSection = "Section";
            object oSave = false;

            App = new Word.Application();
            App.Visible = true;

            taPending.Fill(tbPending);

            foreach (DataRow row in tbPending.Rows)
            {
                Doc = App.Documents.Open(ref fileName, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

                Word.Bookmark bm = Doc.Bookmarks.get_Item(ref bmSection);
                Word.Range rng = bm.Range;
                rng.Text = getSectionLabels(row["SectionFlag"].ToString());

                Doc.PrintOut(ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, 
                    ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, 
                    ref oMissing);
                Doc.Close(ref oSave, ref oMissing, ref oMissing);
            }
            App.Quit(ref oSave, ref oMissing, ref oMissing);
            */
        }

        private void makeCardsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();
            
            da.qryCards(thisYear());
            MessageBox.Show("Cards Table has been updated");
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
            BindGrid();
            MessageBox.Show("Members Updated");

        }

        private void toHalfYearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MembershipTableAdapters.QueriesTableAdapter da = new MembershipTableAdapters.QueriesTableAdapter();

            da.UpdatePendingToHalf();
            BindGrid();
            MessageBox.Show("Members Updated");
        }
       
    }
}