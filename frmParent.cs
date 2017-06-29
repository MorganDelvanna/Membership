using System;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmParent : Form
    {
#warning TODO: Web version of the membership App
#warning TODO: Updater needs to update the Database without the Data
#warning TODO: Cards and Labels for label maker
#warning TODO: Email for renewal letters, acceptance letters
#warning TODO: Pending Cards & Labels
#warning TODO: Pending Filter on list



        public frmParent()
        {
            InitializeComponent();
        }

        private void frmParent_Load(object sender, EventArgs e)
        {
            showList();
        }

        public void editMember(int memberId)
        {
            frmMember frm = new frmMember(memberId, -1);
            frm.MdiParent = this;
            frm.Show();
        }

        public void showList()
        {
            frmMemberList frm;
            frm = new frmMemberList();
            frm.MdiParent = this;
            frm.Show();
        }

        public void newMember()
        {
            frmMember frm = new frmMember(-1, -1);
            frm.MdiParent = this;
            frm.Show();
        }

        public void showLabels()
        {
            frmLabels frm = new frmLabels("Avery5161");
            frm.MdiParent = this;
            frm.Show();
        }

        public void showSummary()
        {
            frmLabels frm = new frmLabels("Summary");
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
