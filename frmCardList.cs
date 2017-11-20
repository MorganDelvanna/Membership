using System;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace PFGA_Membership
{
    public partial class frmCardList : Form
    {
        private ElementHost ctrlHost;
        private PFGA_Cards.CardList wpfCardList;

        public frmCardList()
        {
            InitializeComponent();
        }

        private void frmCardList_Load(object sender, EventArgs e)
        {
            MembershipTableAdapters.CardsTableAdapter daCards = new MembershipTableAdapters.CardsTableAdapter();
            Membership.CardsDataTable dtCards = new Membership.CardsDataTable();
            daCards.Fill(dtCards);

            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            wpfCardList = new PFGA_Cards.CardList(dtCards);
            wpfCardList.InitializeComponent();
            ctrlHost.Child = wpfCardList;

            wpfCardList.OnButtonClick +=
                new PFGA_Cards.CardList.CardListEventHandler(
                avCardList_OnButtonClick);
        }

        void avCardList_OnButtonClick(object sender, PFGA_Cards.CardListEventArgs args)
        {
            frmParent frm = (frmParent)this.ParentForm;
            frm.showList();
            this.Close();
            this.Dispose();
        }
    }
}
