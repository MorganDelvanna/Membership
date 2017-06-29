using System;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmLabels : Form
    {
        //Custom Button
        ToolStripLabel tsFindLabel = new ToolStripLabel("Find Labels:");
        ToolStripTextBox tsFindBox = new ToolStripTextBox("FindBox");
        ToolStripButton tsFindButton = new ToolStripButton("Find");
        int ReportID = 0;

        public frmLabels(string rpt)
        {

            InitializeComponent();
            /*
            foreach (Control control in crystalViewer.Controls)
            {
                if (control is System.Windows.Forms.ToolStrip)
                {
                    ((ToolStrip)control).Items.Add(tsFindLabel);
                    ((ToolStrip)control).Items.Add(tsFindBox);
                    ((ToolStrip)control).Items.Add(tsFindButton);
                    tsFindButton.Click += new EventHandler(tsFindButton_Click);
                }
            }
            */
            switch (rpt)
            {
                case "Avery5161":
                    Avery5161Label();
                    ReportID = 0;
                    break;
                default:
                    Avery5161Label();
                    ReportID = 1;
                    break;
            }
            
        }

        void tsFindButton_Click(object sender, EventArgs e)
        {
            string sTest = tsFindBox.Text;
            string sPattern = @"([\w]*, [\w]*);";
            StringBuilder sFilter = new StringBuilder("{} in [");

            try
            {
                if (sTest.LastIndexOf(';') < sTest.Length) { sTest = string.Concat(sTest, ";"); }
                Regex finder = new Regex(sPattern);
                foreach (Match found in finder.Matches(sTest))
                {
                    Group g = found.Groups[1];
                    foreach (Capture c in g.Captures)
                    {
                        sFilter.Append("\"");
                        sFilter.Append(c.ToString());
                        sFilter.Append("\",");
                    }
                }
                sFilter.Remove(sFilter.Length - 1, 1);
                sFilter.Append("]");

                switch (ReportID)
                {
                    case 0:
                        sFilter.Insert(1, "qryBackTrackMailing.Name");
                        break;
                    case 1:
                        sFilter.Insert(1, "");
                        break;
                    default:
                        sFilter.Insert(1, "");
                        break;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error applying filter", ex, true);
            }
            // crystalViewer.SelectionFormula = sFilter.ToString();
            // crystalViewer.RefreshReport();
        }

        private void crystalViewer_Load(object sender, EventArgs e)
        {
            
        }

        private void Avery5161Label()
        {
            /*
            Avery5161 report;
            MembershipTableAdapters.qryBackTrackMailingTableAdapter da = new PFGA_Membership.MembershipTableAdapters.qryBackTrackMailingTableAdapter();
            Membership.qryBackTrackMailingDataTable dt = new Membership.qryBackTrackMailingDataTable();

            da.Fill(dt);

            report = new Avery5161();
            report.SetDataSource((DataTable)dt);

            crystalViewer.ReportSource = report;
             * */
        }

        private void frmLabels_Load(object sender, EventArgs e)
        {

        }

        private void frmLabels_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmParent frm = (frmParent)this.ParentForm;
            frm.showList();
        }
    }
}
