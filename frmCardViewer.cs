using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace PFGA_Membership
{
    public partial class frmCardViewer : Form
    {
        private ElementHost ctrlHost;
        private PFGA_Cards.CardViewer wpfCardViewer;

        public frmCardViewer()
        {
            InitializeComponent();
        }

        private void frmCardViewer_Load(object sender, EventArgs e)
        {
            MembershipTableAdapters.CardsTableAdapter daCards = new MembershipTableAdapters.CardsTableAdapter();
            Membership.CardsDataTable dtCards = new Membership.CardsDataTable();
            daCards.Fill(dtCards);

            ctrlHost = new ElementHost();
            ctrlHost.Dock = DockStyle.Fill;
            panel1.Controls.Add(ctrlHost);
            wpfCardViewer = new PFGA_Cards.CardViewer(dtCards);
            wpfCardViewer.InitializeComponent();
            ctrlHost.Child = wpfCardViewer;
            /*
            wpfCardViewer.OnButtonClick +=
                new PFGA_Cards.CardList.CardListEventHandler(
                avCardViewer_OnButtonClick);
             */
        }

        void avCardViewer_OnButtonClick(object sender, PFGA_Cards.CardListEventArgs args)
        {
            frmParent frm = (frmParent)this.ParentForm;
            frm.showList();
            this.Close();
            this.Dispose();
        }
    }
}
