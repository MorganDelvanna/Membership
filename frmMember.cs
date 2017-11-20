using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PFGA_Membership
{
    public partial class frmMember : Form
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
        private BitField _Participation;
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

        public clsMembership MemberRecord
        {
            get
            {
                return mbr;
            }
            set
            {
                mbr = value;
            }
        }

        public bool IsExtra { get; set; }

        public frmMember(int ID, int promoteID)
        {
            InitializeComponent();
            _ID = ID;
            _Promote = promoteID;
        }


        private void frmMember_Load(object sender, EventArgs e)
        {
            ulong mask;
            ulong pMask;
            int curYear;
            List<KeyValuePair<int, string>> years = new List<KeyValuePair<int,string>>();

            try
            {

                mbr = new clsMembership(_ID, _Promote);

                MembershipTableAdapters.MembershipTypeTableAdapter daMemType = new PFGA_Membership.MembershipTableAdapters.MembershipTypeTableAdapter();
                Membership.MembershipTypeDataTable dtMemType = new Membership.MembershipTypeDataTable();
                daMemType.Fill(dtMemType);
                cboMemberType.DataSource = dtMemType;
                cboMemberType.DisplayMember = "Membership Type";
                cboMemberType.ValueMember = "MembershipTypeID";

                if (DateTime.Today.Month >= 1 && DateTime.Today.Month < 8)
                {
                    curYear = DateTime.Today.Year - 1;
                }
                else
                {
                    curYear = DateTime.Today.Year;
                }

                for(int c = curYear; c > 2008; c--)
                {
                    years.Add(new KeyValuePair<int, string>(c, string.Format("{0} - {1}", c, c + 1)));
                }
                cboPaid.DataSource = years;
                cboPaid.ValueMember="Key";
                cboPaid.DisplayMember="Value";

                MembershipTableAdapters.PaymentTypeTableAdapter daPayType = new MembershipTableAdapters.PaymentTypeTableAdapter();
                Membership.PaymentTypeDataTable dtPayType = new Membership.PaymentTypeDataTable();
                daPayType.Fill(dtPayType);
                RadioButton[] RadioButtons;
                int index = 0;
                RadioButtons = new RadioButton[dtPayType.Rows.Count];
                foreach(DataRow row in dtPayType)
                {
                    RadioButtons[index] = new RadioButton();
                    RadioButtons[index].Name = "chkPayment" + Convert.ToString(index);
                    RadioButtons[index].Text = row["PaymentType"].ToString();
                    RadioButtons[index].AutoSize = true;
                    RadioButtons[index].Tag = int.Parse(row["PaymentTypeId"].ToString());
                    RadioButtons[index].Location = new System.Drawing.Point((RadioButtons[index].Width * index) + 5, 15);
                    grpPayType.Controls.Add(RadioButtons[index]);
                    index++;
                }
                RadioButtons[0].Checked = true;

                this.Text = String.Format("Card #: {0}", mbr.Card.ToString());
                last_NameTextBox.Text = mbr.LastName.ToString();
                first_NameTextBox.Text = mbr.FirstName.ToString();
                birth_DateDateTimePicker.Text = (mbr.BirthDate.ToString() == "1900-01-01" ? string.Empty : mbr.BirthDate.ToString());
                addressTextBox.Text = mbr.Address.ToString();
                city__ProvTextBox.Text = mbr.CityProv.ToString();
                postalTextBox.Text = mbr.Postal.ToString();
                phoneTextBox.Text = mbr.Phone.ToString();
                email_AddressTextBox.Text = mbr.Email.ToString();
                website_UsernamesTextBox.Text = mbr.UserName.ToString();
                palTextBox.Text = mbr.Pal.ToString();
                pal_Exp_DateDateTimePicker.Text = (mbr.PalExpDate.ToString() == "1900-01-01" ? string.Empty : mbr.PalExpDate.ToString());
                cboMemberType.SelectedValue = mbr.MemberTypeID;
                date_JoinedDateTimePicker.Text = (mbr.JoinedDate.ToString() == "1900-01-01" ? string.Empty : mbr.JoinedDate.ToString());
                cboWalk.SelectedText = mbr.Walk.ToString();
                sectionFlagTextBox.Text = mbr.SectionFlag.ToString();
                noBackTrackCheckBox.Checked = mbr.NoBackTrack;
                noEmailingCheckBox.Checked = mbr.NoEmail;
                chkActive.Checked = mbr.Active;
                txtNotes.Text = mbr.Notes;
                txtCardNumber.Text = mbr.Card.ToString();
                txtSponsor.Text = mbr.Sponsor;
                chkSwipe.Checked = mbr.SwipeCard;
                chkExecutive.Checked = mbr.Executive;
                txtCell.Text = (mbr.Cell == null ? string.Empty : mbr.Cell.ToString());
                txtOther.Text = mbr.ParticipationOther;
                txtParticipateFlag.Text = mbr.Participation.ToString();
                chkCardMade.Checked = mbr.CardMade;
                if (mbr.BadgeImage != null)
                { 
                    if (mbr.BadgeImage.Length > 0)
                    { 
                        Badge.Image = Image.FromStream(new MemoryStream(mbr.BadgeImage));
                    }
                }
                  

                // Existing Member
                if (_ID > 0)
                {
                    #region PaidHistory
                    dgPaidHistory.AutoGenerateColumns = false;

                    DataGridViewTextBoxColumn dgcYearPaid = new DataGridViewTextBoxColumn();
                    dgcYearPaid.Name = "YearPaid";
                    dgcYearPaid.Visible = false;
                    dgcYearPaid.SortMode = DataGridViewColumnSortMode.Programmatic;
                    dgcYearPaid.DataPropertyName = "YearPaid";
                    dgPaidHistory.Columns.Add(dgcYearPaid);
                    dgPaidHistory.Columns["YearPaid"].Visible = false;

                    DataGridViewTextBoxColumn dgcMembershipYear = new DataGridViewTextBoxColumn();
                    dgcMembershipYear.Name = "MembershipYear";
                    dgcMembershipYear.HeaderText = "Year Paid";
                    dgcMembershipYear.DataPropertyName = "MembershipYear";
                    dgcMembershipYear.SortMode = DataGridViewColumnSortMode.Programmatic;
                    dgPaidHistory.Columns.Add(dgcMembershipYear);

                    // Create DeleteButton
                    DataGridViewButtonColumn dgcDelButton = new DataGridViewButtonColumn();
                    dgcDelButton.Name = "colDelete";
                    dgcDelButton.HeaderText = "";
                    dgcDelButton.Text = "û";
                    dgcDelButton.ToolTipText = "Delete Row";
                    dgcDelButton.UseColumnTextForButtonValue = true;
                    dgcDelButton.DefaultCellStyle.Font = new Font("WingDings", 14);
                    dgcDelButton.DefaultCellStyle.ForeColor = Color.Red;
                    dgcDelButton.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgcDelButton.Width = 20;
                    dgPaidHistory.Columns.Add(dgcDelButton);

                    DataView dvPaidHistory = new DataView(mbr.PaidHistory);
                    dvPaidHistory.Sort = "YearPaid DESC";
                    dvPaidHistory.RowFilter = "Deleted = False";

                    dgPaidHistory.DataSource = dvPaidHistory;
                    #endregion

                    #region Extra Cards
                    dgExtraCards.AutoGenerateColumns = false;

                    DataGridViewTextBoxColumn dgcID = new DataGridViewTextBoxColumn();
                    dgcID.Name = "ID";
                    dgcID.Visible = false;
                    dgcID.DataPropertyName = "ID";
                    dgExtraCards.Columns.Add(dgcID);

                    DataGridViewTextBoxColumn dgcName = new DataGridViewTextBoxColumn();
                    dgcName.Name = "Name";
                    dgcName.HeaderText = "Name";
                    dgcName.DataPropertyName = "Name";
                    dgcName.Width = 500;
                    dgExtraCards.Columns.Add(dgcName);

                    DataGridViewImageButtonDeleteColumn dgRemove = new DataGridViewImageButtonDeleteColumn();
                    dgRemove.Name = "colDelete";
                    dgRemove.HeaderText = "";
                    dgRemove.Text = "Delete";
                    dgRemove.ToolTipText = "";
                    dgRemove.UseColumnTextForButtonValue = true;
                    dgRemove.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgExtraCards.Columns.Add(dgRemove);

                    DataGridViewImageButtonPromoteColumn dgPromote = new DataGridViewImageButtonPromoteColumn();
                    dgPromote.Name = "colPromote";
                    dgPromote.HeaderText = "";
                    dgPromote.Text = "Promote to Member";
                    dgPromote.ToolTipText = "Promote";
                    dgPromote.UseColumnTextForButtonValue = true;
                    dgExtraCards.Columns.Add(dgPromote);

                    DataGridViewImageButtonEditColumn dgEdit = new DataGridViewImageButtonEditColumn();
                    dgEdit.Name = "colEdit";
                    dgEdit.HeaderText = "";
                    dgEdit.Text = "Edit";
                    dgEdit.ToolTipText = "Edit";
                    dgEdit.UseColumnTextForButtonValue = true;
                    dgExtraCards.Columns.Add(dgEdit);

                    DataView dvExtraCards = new DataView(mbr.ExtraCards);
                    dvExtraCards.RowFilter = "Deleted = False AND Promote = False";
                    dgExtraCards.DataSource = dvExtraCards;
                    #endregion
                }

                // New Extra Card, Hide Fields
                if (IsExtra || (_ID < 0 && _Promote > 0))
                {
                    this.Text = String.Format("Card #: {0}", mbr.Card.ToString());
                    last_NameTextBox.Text = mbr.LastName.ToString();
                    first_NameTextBox.Text = mbr.FirstName.ToString();
                    addressTextBox.Hide();
                    city__ProvTextBox.Hide();
                    postalTextBox.Hide();
                    phoneTextBox.Hide();
                    website_UsernamesTextBox.Hide();
                    date_JoinedDateTimePicker.Hide();
                    grpSection.Hide();
                    noBackTrackCheckBox.Hide();
                    noEmailingCheckBox.Hide();
                    chkActive.Hide();
                    tabControl1.TabPages.Remove(tabPaid);
                    tabControl1.TabPages.Remove(tabExtra);
                    grpParticipate.Hide();
                    txtCardNumber.Show();
                    lblCardNumber.Show();
                }
                else if (_ID < 0 && _Promote < 0)
                {
                    tabPaid.Hide();
                    tabExtra.Hide();
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

                if (ulong.TryParse(txtParticipateFlag.Text, out pMask))
                {
                    _Participation.Mask = pMask;
                }
                else
                {
                    _Participation.ClearField();
                }

                
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error Loading Member Form", ex, true);
            }

            DisplaySections();
            DisplayParticipation();

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

        private void DisplayParticipation()
        {
            try
            {
                if (_Participation.AnyOn(BitField.Flag.f1)) //Work Party
                {
                    chkWorkParty.Checked = true;
                }
                else
                {
                    chkWorkParty.Checked = false;
                }

                if (_Participation.AnyOn(BitField.Flag.f2)) // Events
                {
                    chkEvents.Checked = true;
                }
                else
                {
                    chkEvents.Checked = false;
                }

                if (_Participation.AnyOn(BitField.Flag.f3)) // Executive
                {
                    chkpExecutive.Checked = true;
                }
                else
                {
                    chkpExecutive.Checked = false;
                }

                if (_Participation.AnyOn(BitField.Flag.f4)) // Range Officer
                {
                    chkRO.Checked = true;
                }
                else
                {
                    chkRO.Checked = false;
                }

                if (_Participation.AnyOn(BitField.Flag.f5)) // TRaining Officer
                {
                    chkTO.Checked = true;
                }
                else
                {
                    chkTO.Checked = false;
                }

                if (_Participation.AnyOn(BitField.Flag.f6)) // Other
                {
                    chkOther.Checked = true;
                }
                else
                {
                    chkOther.Checked = false;
                }

            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error setting Participation Checkboxes", ex, true);
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
                mbr.Address = addressTextBox.Text;
                mbr.CityProv = city__ProvTextBox.Text;
                mbr.Postal = postalTextBox.Text;
                mbr.Phone = phoneTextBox.Text;
                mbr.Email = email_AddressTextBox.Text;
                mbr.UserName = website_UsernamesTextBox.Text;
                if (!double.TryParse(palTextBox.Text, out tmpPal))
                {
                    tmpPal = 0;
                }
                mbr.Pal = tmpPal;
                mbr.PalExpDate = pal_Exp_DateDateTimePicker.Value;
                mbr.MemberTypeID = int.Parse(cboMemberType.SelectedValue.ToString());
                mbr.JoinedDate = date_JoinedDateTimePicker.Value;
                mbr.Walk = cboWalk.Text;
                mbr.SectionFlag = _Sections.Mask;
                mbr.NoBackTrack = noBackTrackCheckBox.Checked;
                mbr.NoEmail = noEmailingCheckBox.Checked;
                mbr.Active = chkActive.Checked;
                mbr.Card = int.Parse(txtCardNumber.Text);
                mbr.Notes = txtNotes.Text;
                mbr.Sponsor = txtSponsor.Text;
                mbr.SwipeCard = chkSwipe.Checked;
                mbr.Executive = chkExecutive.Checked;
                mbr.Cell = txtCell.Text;
                mbr.ParticipationOther = txtOther.Text;
                mbr.Participation = _Participation.Mask;
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

                if ((!IsExtra) && ((Promote == -1 && ID > 0) || Application.OpenForms.Count == 2))
                {
                    frmParent frm = (frmParent)this.ParentForm;
                    frm.showList();
                }

                this.Close();
                this.Dispose();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to Save the Member", ex, true);
            }
        }

        private void dgPaidHistory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgPaidHistory.Columns["colDelete"].Index)
                {
                    if (MessageBox.Show("This will delete the History record permanently when you click Save",
                        "Delete Payment History", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        var searchTerm = string.Format("[YearPaid] = {0}", dgPaidHistory["YearPaid", e.RowIndex].Value.ToString());
                        mbr.PaidHistory.Select(searchTerm)
                             .ToList<DataRow>()
                             .ForEach(r =>
                             {
                                 r["Deleted"] = true;
                             });
                        dgPaidHistory.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to delete paid history", ex, true);
            }
        }

        private void btnAddPaid_Click(object sender, EventArgs e)
        {
            int curYear;

            try
            {
                curYear = int.Parse(cboPaid.SelectedValue.ToString());
                int payType = int.Parse(grpPayType.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked).Tag.ToString());

                DataRow[] search = mbr.PaidHistory.Select(String.Format("YearPaid={0}", curYear.ToString()));

                if (search.Length == 0)
                {
                    DataRow dr = mbr.PaidHistory.NewRow();
                    dr["YearPaid"] = curYear;
                    dr["MembershipYear"] = string.Format("{0} - {1}", curYear, curYear + 1);
                    dr["Deleted"] = false;
                    dr["NewRec"] = true;
                    dr["PaymentType"] = payType;
                    mbr.PaidHistory.Rows.Add(dr);
                    dgPaidHistory.Refresh();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to add a Paid Row", ex, true);
            }
        }

        private void dgExtraCards_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == dgExtraCards.Columns["colRemove"].Index)
                {
                    if (MessageBox.Show("This will delete the Extra Card permanently when you click Save",
                        "Delete Extra Card", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                    {
                        mbr.ExtraCards.Rows[e.RowIndex]["Deleted"] = true;
                    }
                }
                else if (e.ColumnIndex == dgExtraCards.Columns["colPromote"].Index)
                {
                    int ID = int.Parse(dgExtraCards["ID", e.RowIndex].Value.ToString());
                    frmMember frm = new frmMember(ID, mbr.ID);
                    frm.Show();
                }
                else if (e.ColumnIndex == dgExtraCards.Columns["colEdit"].Index)
                {
                    int ID = int.Parse(dgExtraCards["ID", e.RowIndex].Value.ToString());
                    frmMember frm = new frmMember(ID, -1);
                    frm.IsExtra = true;
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to modify an Extra Card", ex, true);
            }

            this.Refresh();
        }

        private void btnExtra_Click(object sender, EventArgs e)
        {
            try
            {
                frmExtra frm = new frmExtra(-1, mbr.ID);
                frm.Show();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("Error trying to add a new Extra cards", ex, true);
            }
        }

        private void chkWorkParty_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWorkParty.Checked)
            {
                _Participation.SetOn(BitField.Flag.f1);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f1);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void chkEvents_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEvents.Checked)
            {
                _Participation.SetOn(BitField.Flag.f2);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f2);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void chkpExecutive_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpExecutive.Checked)
            {
                _Participation.SetOn(BitField.Flag.f3);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f3);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void chkRO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO.Checked)
            {
                _Participation.SetOn(BitField.Flag.f4);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f4);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void chkTO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTO.Checked)
            {
                _Participation.SetOn(BitField.Flag.f5);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f5);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void chkOther_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOther.Checked)
            {
                _Participation.SetOn(BitField.Flag.f6);
            }
            else
            {
                _Participation.SetOff(BitField.Flag.f6);
            }
            txtParticipateFlag.Text = _Participation.Mask.ToString();
        }

        private void cboMemberType_SelectedIndexChanged(object sender, EventArgs e)
        {
  
            // If the applicant is denied
            if (cboMemberType.SelectedValue.ToString() == "14")
            {
                chkActive.Checked = false;
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
