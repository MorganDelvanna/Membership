using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmExtra : Form
    {

        /*
         * Bitfield Key
         * f1 = Archery          
         * f2 = HandGun 
         * f3 = Smallbore  
         * f4 = SCA              
         * f5 = Rifle            
         * f6 = Action Pistol    
         */

        private int _ID;
        private int _Promote;
        private BitField _Sections;
        private clsMembership mbr;

        private int ID
        {
            get 
            {
               return _ID; 
            }
            set 
            {
               _ID = value; 
            }
        }

        private int Promote
        {
            get
            {
                return _Promote;
            }
            set
            {
                _Promote = value;
            }
        }

        public bool IsExtra { get; set; }

        public frmExtra(int ID, int promoteID)
        {
            InitializeComponent();
            _ID = ID;
            _Promote = promoteID;
        }


        private void frmExtra_Load(object sender, EventArgs e)
        {
            ulong mask;
            List<KeyValuePair<int, string>> years = new List<KeyValuePair<int,string>>();

            try
            {

                mbr = new clsMembership(_ID, _Promote);

                MembershipTableAdapters.MembershipTypeTableAdapter daMemType = new PFGA_Membership.MembershipTableAdapters.MembershipTypeTableAdapter();
                Membership.MembershipTypeDataTable dtMemType = new Membership.MembershipTypeDataTable();
                daMemType.Fill(dtMemType);
                
                this.Text = String.Format("Card #: {0}", mbr.Card.ToString());
                last_NameTextBox.Text = mbr.LastName.ToString();
                first_NameTextBox.Text = mbr.FirstName.ToString();
                birth_DateDateTimePicker.Text = (mbr.BirthDate.ToString() == "1900-01-01" ? string.Empty : mbr.BirthDate.ToString());
                email_AddressTextBox.Text = mbr.Email.ToString();
                website_UsernamesTextBox.Text = mbr.UserName.ToString();
                palTextBox.Text = mbr.Pal.ToString();
                pal_Exp_DateDateTimePicker.Text = (mbr.PalExpDate.ToString() == "1900-01-01" ? string.Empty : mbr.PalExpDate.ToString());
                cboWalk.SelectedText = mbr.Walk.ToString();
                sectionFlagTextBox.Text = mbr.SectionFlag.ToString();
                txtNotes.Text = mbr.Notes;
                txtCardNumber.Text = mbr.Card.ToString();
                chkSwipe.Checked = mbr.SwipeCard;
                txtCell.Text = (mbr.Cell == null ? string.Empty : mbr.Cell.ToString());
                chkCardMade.Checked = mbr.CardMade;
                if (mbr.BadgeImage != null)
                { 
                    if (mbr.BadgeImage.Length > 0)
                    { 
                        Badge.Image = Image.FromStream(new MemoryStream(mbr.BadgeImage));
                    }
                }

                // New Extra Card, Hide Fields
                if (IsExtra || (_ID < 0 && _Promote > 0))
                {
                    this.Text = String.Format("Card #: {0}", mbr.Card.ToString());
                    last_NameTextBox.Text = mbr.LastName.ToString();
                    first_NameTextBox.Text = mbr.FirstName.ToString();
                    grpSection.Hide();                   
                    txtCardNumber.Show();
                    lblCardNumber.Show();
                }
                else if (_ID < 0 && _Promote < 0)
                {
                    txtCardNumber.Show();
                    lblCardNumber.Show();
                }

                if (ulong.TryParse(sectionFlagTextBox.Text.ToString(), out mask))
                {
                    _Sections.Mask = mask;
                }
                else
                {
                    _Sections.ClearField();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Loading Member Form", ex, true);
            }

            DisplaySections();
        }

        private void DisplaySections()
        {
            try
            {
                if (_Sections.AnyOn(BitField.Flag.f1)) //Archery
                {
                    chkArchery.Checked = true;
                }
                else
                {
                    chkArchery.Checked = false;
                }

                if (_Sections.AnyOn(BitField.Flag.f2)) //Handgun
                {
                    chkHandgun.Checked = true;
                }
                else
                {
                    chkHandgun.Checked = false;
                }

                if (_Sections.AnyOn(BitField.Flag.f3)) //Smallbore
                {
                    chkSmallbore.Checked = true;
                }
                else
                {
                    chkSmallbore.Checked = false;
                }

                if (_Sections.AnyOn(BitField.Flag.f4)) //SCA
                {
                    chkSCA.Checked = true;
                }
                else
                {
                    chkSCA.Checked = false;
                }

                if (_Sections.AnyOn(BitField.Flag.f5)) //Rifle
                {
                    chkRifle.Checked = true;
                }
                else
                {
                    chkRifle.Checked = false;
                }

                if (_Sections.AnyOn(BitField.Flag.f6)) //Action
                {
                    chkAction.Checked = true;
                }
                else
                {
                    chkAction.Checked = false;
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error setting Section Checkboxes", ex, true);
            }
        }

        private void chkArchery_CheckedChanged(object sender, EventArgs e)
        {
            if (chkArchery.Checked)
            {
                _Sections.SetOn(BitField.Flag.f1);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f1);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void chkHandgun_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHandgun.Checked)
            {
                _Sections.SetOn(BitField.Flag.f2);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f2);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void chkSmallbore_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSmallbore.Checked)
            {
                _Sections.SetOn(BitField.Flag.f3);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f3);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void chkSCA_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSCA.Checked)
            {
                _Sections.SetOn(BitField.Flag.f4);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f4);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void chkRifle_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRifle.Checked)
            {
                _Sections.SetOn(BitField.Flag.f5);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f5);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void chkAction_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAction.Checked)
            {
                _Sections.SetOn(BitField.Flag.f6);
            }
            else
            {
                _Sections.SetOff(BitField.Flag.f6);
            }
            sectionFlagTextBox.Text = _Sections.Mask.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if ((!IsExtra) && Promote == -1 && ID > 0)
                {
                    frmParent frm = (frmParent)this.ParentForm;
                    frm.showList();
                }
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to cancel", ex, true);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            double tmpPal;
            MemoryStream ms = new MemoryStream();

            try
            {
                mbr.LastName = last_NameTextBox.Text;
                mbr.FirstName = first_NameTextBox.Text;
                mbr.BirthDate = birth_DateDateTimePicker.Value;
                mbr.Email = email_AddressTextBox.Text;
                mbr.UserName = website_UsernamesTextBox.Text;
                if (!double.TryParse(palTextBox.Text, out tmpPal))
                {
                    tmpPal = 0;
                }
                mbr.Pal = tmpPal;
                mbr.PalExpDate = pal_Exp_DateDateTimePicker.Value;
                mbr.MemberTypeID = 11;
                mbr.Walk = cboWalk.Text;
                mbr.SectionFlag = _Sections.Mask;
                mbr.Card = int.Parse(txtCardNumber.Text);
                mbr.Notes = txtNotes.Text;
                mbr.SwipeCard = chkSwipe.Checked;
                mbr.Cell = txtCell.Text;
                mbr.CardMade = chkCardMade.Checked;
                if (Badge.Image != null)
                {
                    Badge.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    mbr.BadgeImage = ms.ToArray();
                    ms.Dispose();
                    string imagesFiled = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
                    if (!Directory.Exists(imagesFiled))
                    {
                        Directory.CreateDirectory(imagesFiled);
                    }
                    if (mbr.Card > 0)
                    {
                        Badge.Image.Save(string.Format(@"{0}\{1}.jpg", imagesFiled, mbr.Card.ToString()));
                    }
                }

                mbr.Save();

                frmMember frmM = (frmMember)Application.OpenForms[Application.OpenForms.Count - 2];
                frmM.MemberRecord.refreshCards();

                DataView dvExtraCards = new DataView(frmM.MemberRecord.ExtraCards);
                dvExtraCards.RowFilter = "Deleted = False AND Promote = False";
                frmM.dgExtraCards.DataSource = dvExtraCards;
                
                
                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to Save the Member", ex, true);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    mbr.BadgeImage = File.ReadAllBytes(openFileDialog1.FileName);
                    Badge.Image = Image.FromStream(new MemoryStream(mbr.BadgeImage));
                }
            }

        }

    }
}
