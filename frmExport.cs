using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace PFGA_Membership
{
    public partial class frmExport : Form
    {
        public frmExport()
        {
            InitializeComponent();
        }

        private void bindGrid()
        {
            SqlConnection cnn;
            SqlCommand cmd;
            DataTable dtExport;
            SqlDataAdapter da;
            String expiryDate;


            String config = ConfigurationManager.ConnectionStrings["PFGA_Membership.Properties.Settings.PFGAMembershipConnectionString"].ToString();

            if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 9)
            {
                expiryDate = new DateTime(DateTime.Today.Year, 10, 31).ToString("yyyy-MM-dd hh:mm");
            }
            else
            {
                expiryDate = new DateTime(DateTime.Today.Year + 1, 10, 31).ToString("yyyy-MM-dd hh:mm");
            }

            try
            {
                cnn = new SqlConnection(config);
                cnn.Open();
                String qryExport = $@"SELECT [First Name] as firstName, [Last Name] as lastName, FORMAT(Card, '00000') AS userCode,
                    [First Name] + ' ' + [Last Name] AS cardName,  '26-bit' AS cardFormat, '' AS cardNumber, '' AS cardHex, 4 AS accessLevel, '' AS activationDate, 
                    '{expiryDate}' AS expiryDate, '' AS uniqueId FROM Members WHERE Members.MemberTypeID = 13 AND Archived = 0 AND cardMade = 0 AND Archived = 0";

                cmd = new SqlCommand(qryExport, cnn);
                dtExport = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtExport);
                DataView dv = new DataView(dtExport);
                dgExport.AutoGenerateColumns = false;
                dgExport.DataSource = dv;
                dgExport.Refresh();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to generate Export", ex, true);
            }
            

        }

        private void frmExport_Load(object sender, EventArgs e)
        {
            bindGrid();
        }

        private void btnCancel_Click(object sender, EventArgs e)
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), $@"Atrium Import {DateTime.Now.ToString("yyyyMMddhhmm")}.csv");
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(fileName, false))
                {
                    file.WriteLine("First_Name,Last_Name,User_Code,Card_Name,Card_Format,Card_Number,Card_Hex_Number,Access_Level_Id,Activation_Date,Expiration_Date,Unique_Id");
                    foreach (DataGridViewRow row in dgExport.Rows)
                    {          
                        if (row.Cells["cardNumber"].Value != null)
                        {
                            if (row.Cells["cardNumber"].Value.ToString().Length > 0)
                            {
                                file.WriteLine(String.Join(",", from DataGridViewCell c in row.Cells select c.Value));
                            }                            
                        }                        
                    }
                    file.Close();
                }

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
    }
}
