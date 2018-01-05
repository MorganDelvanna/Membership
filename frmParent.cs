using System;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmParent : Form
    {
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

        public void showCardList()
        {
            frmCardList frm = new frmCardList();
            frm.MdiParent = this;
            frm.Show();
        }

        public void showCards()
        {
            frmCardViewer frm = new frmCardViewer();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
