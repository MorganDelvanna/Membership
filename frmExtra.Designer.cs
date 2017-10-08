namespace PFGA_Membership
{
    partial class frmExtra
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label website_UsernamesLabel;
            System.Windows.Forms.Label pal_Exp_DateLabel;
            System.Windows.Forms.Label palLabel;
            System.Windows.Forms.Label birth_DateLabel;
            System.Windows.Forms.Label walkLabel;
            System.Windows.Forms.Label email_AddressLabel;
            System.Windows.Forms.Label first_NameLabel;
            System.Windows.Forms.Label last_NameLabel;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExtra));
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.membership = new PFGA_Membership.Membership();
            this.chkCardMade = new System.Windows.Forms.CheckBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.Badge = new System.Windows.Forms.PictureBox();
            this.txtCell = new System.Windows.Forms.TextBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtCardNumber = new System.Windows.Forms.TextBox();
            this.website_UsernamesTextBox = new System.Windows.Forms.TextBox();
            this.palTextBox = new System.Windows.Forms.TextBox();
            this.email_AddressTextBox = new System.Windows.Forms.TextBox();
            this.first_NameTextBox = new System.Windows.Forms.TextBox();
            this.last_NameTextBox = new System.Windows.Forms.TextBox();
            this.chkSwipe = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblCardNumber = new System.Windows.Forms.Label();
            this.grpSection = new System.Windows.Forms.GroupBox();
            this.chkAction = new System.Windows.Forms.CheckBox();
            this.chkRifle = new System.Windows.Forms.CheckBox();
            this.chkSCA = new System.Windows.Forms.CheckBox();
            this.chkSmallbore = new System.Windows.Forms.CheckBox();
            this.chkHandgun = new System.Windows.Forms.CheckBox();
            this.chkArchery = new System.Windows.Forms.CheckBox();
            this.sectionFlagTextBox = new System.Windows.Forms.TextBox();
            this.cboWalk = new System.Windows.Forms.ComboBox();
            this.pal_Exp_DateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.birth_DateDateTimePicker = new System.Windows.Forms.DateTimePicker();
            label4 = new System.Windows.Forms.Label();
            website_UsernamesLabel = new System.Windows.Forms.Label();
            pal_Exp_DateLabel = new System.Windows.Forms.Label();
            palLabel = new System.Windows.Forms.Label();
            birth_DateLabel = new System.Windows.Forms.Label();
            walkLabel = new System.Windows.Forms.Label();
            email_AddressLabel = new System.Windows.Forms.Label();
            first_NameLabel = new System.Windows.Forms.Label();
            last_NameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.membership)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Badge)).BeginInit();
            this.grpSection.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(652, 427);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 29;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(733, 427);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 30;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // membership
            // 
            this.membership.DataSetName = "Membership";
            this.membership.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // chkCardMade
            // 
            this.chkCardMade.AutoSize = true;
            this.chkCardMade.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkCardMade.Location = new System.Drawing.Point(723, 296);
            this.chkCardMade.Name = "chkCardMade";
            this.chkCardMade.Size = new System.Drawing.Size(81, 17);
            this.chkCardMade.TabIndex = 135;
            this.chkCardMade.Text = "Card Made:";
            this.chkCardMade.UseVisualStyleBackColor = true;
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(705, 85);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(75, 23);
            this.btnLoad.TabIndex = 134;
            this.btnLoad.Text = "Load Image";
            this.btnLoad.UseVisualStyleBackColor = true;
            // 
            // Badge
            // 
            this.Badge.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Badge.Location = new System.Drawing.Point(426, 85);
            this.Badge.Name = "Badge";
            this.Badge.Size = new System.Drawing.Size(268, 228);
            this.Badge.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Badge.TabIndex = 133;
            this.Badge.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(91, 119);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(27, 13);
            label4.TabIndex = 132;
            label4.Text = "Cell:";
            // 
            // txtCell
            // 
            this.txtCell.Location = new System.Drawing.Point(124, 116);
            this.txtCell.Name = "txtCell";
            this.txtCell.Size = new System.Drawing.Size(100, 20);
            this.txtCell.TabIndex = 115;
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(426, 12);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(378, 66);
            this.txtNotes.TabIndex = 128;
            // 
            // txtCardNumber
            // 
            this.txtCardNumber.Location = new System.Drawing.Point(124, 12);
            this.txtCardNumber.Name = "txtCardNumber";
            this.txtCardNumber.Size = new System.Drawing.Size(100, 20);
            this.txtCardNumber.TabIndex = 109;
            this.txtCardNumber.Visible = false;
            // 
            // website_UsernamesTextBox
            // 
            this.website_UsernamesTextBox.Location = new System.Drawing.Point(124, 168);
            this.website_UsernamesTextBox.Name = "website_UsernamesTextBox";
            this.website_UsernamesTextBox.Size = new System.Drawing.Size(100, 20);
            this.website_UsernamesTextBox.TabIndex = 117;
            // 
            // palTextBox
            // 
            this.palTextBox.Location = new System.Drawing.Point(124, 196);
            this.palTextBox.Name = "palTextBox";
            this.palTextBox.Size = new System.Drawing.Size(100, 20);
            this.palTextBox.TabIndex = 118;
            // 
            // email_AddressTextBox
            // 
            this.email_AddressTextBox.Location = new System.Drawing.Point(124, 142);
            this.email_AddressTextBox.Name = "email_AddressTextBox";
            this.email_AddressTextBox.Size = new System.Drawing.Size(200, 20);
            this.email_AddressTextBox.TabIndex = 116;
            // 
            // first_NameTextBox
            // 
            this.first_NameTextBox.Location = new System.Drawing.Point(124, 64);
            this.first_NameTextBox.Name = "first_NameTextBox";
            this.first_NameTextBox.Size = new System.Drawing.Size(200, 20);
            this.first_NameTextBox.TabIndex = 112;
            // 
            // last_NameTextBox
            // 
            this.last_NameTextBox.Location = new System.Drawing.Point(124, 38);
            this.last_NameTextBox.Name = "last_NameTextBox";
            this.last_NameTextBox.Size = new System.Drawing.Size(200, 20);
            this.last_NameTextBox.TabIndex = 110;
            // 
            // chkSwipe
            // 
            this.chkSwipe.AutoSize = true;
            this.chkSwipe.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkSwipe.Location = new System.Drawing.Point(80, 275);
            this.chkSwipe.Name = "chkSwipe";
            this.chkSwipe.Size = new System.Drawing.Size(58, 17);
            this.chkSwipe.TabIndex = 123;
            this.chkSwipe.Text = "Swipe:";
            this.chkSwipe.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.chkSwipe.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(382, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 131;
            this.label3.Text = "Notes:";
            // 
            // lblCardNumber
            // 
            this.lblCardNumber.AutoSize = true;
            this.lblCardNumber.Location = new System.Drawing.Point(46, 15);
            this.lblCardNumber.Name = "lblCardNumber";
            this.lblCardNumber.Size = new System.Drawing.Size(72, 13);
            this.lblCardNumber.TabIndex = 130;
            this.lblCardNumber.Text = "Card Number:";
            this.lblCardNumber.Visible = false;
            // 
            // grpSection
            // 
            this.grpSection.Controls.Add(this.chkAction);
            this.grpSection.Controls.Add(this.chkRifle);
            this.grpSection.Controls.Add(this.chkSCA);
            this.grpSection.Controls.Add(this.chkSmallbore);
            this.grpSection.Controls.Add(this.chkHandgun);
            this.grpSection.Controls.Add(this.chkArchery);
            this.grpSection.Controls.Add(this.sectionFlagTextBox);
            this.grpSection.Location = new System.Drawing.Point(21, 309);
            this.grpSection.Name = "grpSection";
            this.grpSection.Size = new System.Drawing.Size(233, 95);
            this.grpSection.TabIndex = 124;
            this.grpSection.TabStop = false;
            this.grpSection.Text = "Sections";
            // 
            // chkAction
            // 
            this.chkAction.AutoSize = true;
            this.chkAction.Location = new System.Drawing.Point(144, 74);
            this.chkAction.Name = "chkAction";
            this.chkAction.Size = new System.Drawing.Size(84, 17);
            this.chkAction.TabIndex = 23;
            this.chkAction.Text = "Action Pistol";
            this.chkAction.UseVisualStyleBackColor = true;
            // 
            // chkRifle
            // 
            this.chkRifle.AutoSize = true;
            this.chkRifle.Location = new System.Drawing.Point(144, 51);
            this.chkRifle.Name = "chkRifle";
            this.chkRifle.Size = new System.Drawing.Size(47, 17);
            this.chkRifle.TabIndex = 22;
            this.chkRifle.Text = "Rifle";
            this.chkRifle.UseVisualStyleBackColor = true;
            // 
            // chkSCA
            // 
            this.chkSCA.AutoSize = true;
            this.chkSCA.Location = new System.Drawing.Point(144, 28);
            this.chkSCA.Name = "chkSCA";
            this.chkSCA.Size = new System.Drawing.Size(47, 17);
            this.chkSCA.TabIndex = 21;
            this.chkSCA.Text = "SCA";
            this.chkSCA.UseVisualStyleBackColor = true;
            // 
            // chkSmallbore
            // 
            this.chkSmallbore.AutoSize = true;
            this.chkSmallbore.Location = new System.Drawing.Point(22, 74);
            this.chkSmallbore.Name = "chkSmallbore";
            this.chkSmallbore.Size = new System.Drawing.Size(72, 17);
            this.chkSmallbore.TabIndex = 20;
            this.chkSmallbore.Text = "Smallbore";
            this.chkSmallbore.UseVisualStyleBackColor = true;
            // 
            // chkHandgun
            // 
            this.chkHandgun.AutoSize = true;
            this.chkHandgun.Location = new System.Drawing.Point(22, 51);
            this.chkHandgun.Name = "chkHandgun";
            this.chkHandgun.Size = new System.Drawing.Size(70, 17);
            this.chkHandgun.TabIndex = 19;
            this.chkHandgun.Text = "Handgun";
            this.chkHandgun.UseVisualStyleBackColor = true;
            // 
            // chkArchery
            // 
            this.chkArchery.AutoSize = true;
            this.chkArchery.Location = new System.Drawing.Point(22, 28);
            this.chkArchery.Name = "chkArchery";
            this.chkArchery.Size = new System.Drawing.Size(62, 17);
            this.chkArchery.TabIndex = 18;
            this.chkArchery.Text = "Archery";
            this.chkArchery.UseVisualStyleBackColor = true;
            // 
            // sectionFlagTextBox
            // 
            this.sectionFlagTextBox.Location = new System.Drawing.Point(103, 19);
            this.sectionFlagTextBox.Name = "sectionFlagTextBox";
            this.sectionFlagTextBox.Size = new System.Drawing.Size(0, 20);
            this.sectionFlagTextBox.TabIndex = 32;
            // 
            // cboWalk
            // 
            this.cboWalk.FormattingEnabled = true;
            this.cboWalk.Items.AddRange(new object[] {
            "",
            "Done",
            "Need"});
            this.cboWalk.Location = new System.Drawing.Point(124, 248);
            this.cboWalk.Name = "cboWalk";
            this.cboWalk.Size = new System.Drawing.Size(121, 21);
            this.cboWalk.TabIndex = 122;
            // 
            // website_UsernamesLabel
            // 
            website_UsernamesLabel.AutoSize = true;
            website_UsernamesLabel.Location = new System.Drawing.Point(18, 171);
            website_UsernamesLabel.Name = "website_UsernamesLabel";
            website_UsernamesLabel.Size = new System.Drawing.Size(100, 13);
            website_UsernamesLabel.TabIndex = 129;
            website_UsernamesLabel.Text = "Website Username:";
            // 
            // pal_Exp_DateLabel
            // 
            pal_Exp_DateLabel.AutoSize = true;
            pal_Exp_DateLabel.Location = new System.Drawing.Point(46, 226);
            pal_Exp_DateLabel.Name = "pal_Exp_DateLabel";
            pal_Exp_DateLabel.Size = new System.Drawing.Size(72, 13);
            pal_Exp_DateLabel.TabIndex = 127;
            pal_Exp_DateLabel.Text = "Pal Exp Date:";
            // 
            // pal_Exp_DateDateTimePicker
            // 
            this.pal_Exp_DateDateTimePicker.CustomFormat = "yyyy-MMM-dd";
            this.pal_Exp_DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.pal_Exp_DateDateTimePicker.Location = new System.Drawing.Point(124, 222);
            this.pal_Exp_DateDateTimePicker.Name = "pal_Exp_DateDateTimePicker";
            this.pal_Exp_DateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.pal_Exp_DateDateTimePicker.TabIndex = 119;
            // 
            // palLabel
            // 
            palLabel.AutoSize = true;
            palLabel.Location = new System.Drawing.Point(93, 199);
            palLabel.Name = "palLabel";
            palLabel.Size = new System.Drawing.Size(25, 13);
            palLabel.TabIndex = 126;
            palLabel.Text = "Pal:";
            // 
            // birth_DateLabel
            // 
            birth_DateLabel.AutoSize = true;
            birth_DateLabel.Location = new System.Drawing.Point(61, 94);
            birth_DateLabel.Name = "birth_DateLabel";
            birth_DateLabel.Size = new System.Drawing.Size(57, 13);
            birth_DateLabel.TabIndex = 125;
            birth_DateLabel.Text = "Birth Date:";
            // 
            // birth_DateDateTimePicker
            // 
            this.birth_DateDateTimePicker.CustomFormat = "yyyy-MMM-dd";
            this.birth_DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.birth_DateDateTimePicker.Location = new System.Drawing.Point(124, 90);
            this.birth_DateDateTimePicker.Name = "birth_DateDateTimePicker";
            this.birth_DateDateTimePicker.Size = new System.Drawing.Size(200, 20);
            this.birth_DateDateTimePicker.TabIndex = 114;
            // 
            // walkLabel
            // 
            walkLabel.AutoSize = true;
            walkLabel.Location = new System.Drawing.Point(83, 251);
            walkLabel.Name = "walkLabel";
            walkLabel.Size = new System.Drawing.Size(35, 13);
            walkLabel.TabIndex = 121;
            walkLabel.Text = "Walk:";
            // 
            // email_AddressLabel
            // 
            email_AddressLabel.AutoSize = true;
            email_AddressLabel.Location = new System.Drawing.Point(42, 145);
            email_AddressLabel.Name = "email_AddressLabel";
            email_AddressLabel.Size = new System.Drawing.Size(76, 13);
            email_AddressLabel.TabIndex = 120;
            email_AddressLabel.Text = "Email Address:";
            // 
            // first_NameLabel
            // 
            first_NameLabel.AutoSize = true;
            first_NameLabel.Location = new System.Drawing.Point(58, 67);
            first_NameLabel.Name = "first_NameLabel";
            first_NameLabel.Size = new System.Drawing.Size(60, 13);
            first_NameLabel.TabIndex = 113;
            first_NameLabel.Text = "First Name:";
            // 
            // last_NameLabel
            // 
            last_NameLabel.AutoSize = true;
            last_NameLabel.Location = new System.Drawing.Point(57, 38);
            last_NameLabel.Name = "last_NameLabel";
            last_NameLabel.Size = new System.Drawing.Size(61, 13);
            last_NameLabel.TabIndex = 111;
            last_NameLabel.Text = "Last Name:";
            // 
            // frmExtra
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(827, 461);
            this.ControlBox = false;
            this.Controls.Add(this.chkCardMade);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.Badge);
            this.Controls.Add(label4);
            this.Controls.Add(this.txtCell);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.txtCardNumber);
            this.Controls.Add(this.website_UsernamesTextBox);
            this.Controls.Add(this.palTextBox);
            this.Controls.Add(this.email_AddressTextBox);
            this.Controls.Add(this.first_NameTextBox);
            this.Controls.Add(this.last_NameTextBox);
            this.Controls.Add(this.chkSwipe);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblCardNumber);
            this.Controls.Add(this.grpSection);
            this.Controls.Add(this.cboWalk);
            this.Controls.Add(website_UsernamesLabel);
            this.Controls.Add(pal_Exp_DateLabel);
            this.Controls.Add(this.pal_Exp_DateDateTimePicker);
            this.Controls.Add(palLabel);
            this.Controls.Add(birth_DateLabel);
            this.Controls.Add(this.birth_DateDateTimePicker);
            this.Controls.Add(walkLabel);
            this.Controls.Add(email_AddressLabel);
            this.Controls.Add(first_NameLabel);
            this.Controls.Add(last_NameLabel);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmExtra";
            this.Text = "frmExtra";
            this.Load += new System.EventHandler(this.frmExtra_Load);
            ((System.ComponentModel.ISupportInitialize)(this.membership)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Badge)).EndInit();
            this.grpSection.ResumeLayout(false);
            this.grpSection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private Membership membership;
        private System.Windows.Forms.CheckBox chkCardMade;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.PictureBox Badge;
        private System.Windows.Forms.TextBox txtCell;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.TextBox txtCardNumber;
        private System.Windows.Forms.TextBox website_UsernamesTextBox;
        private System.Windows.Forms.TextBox palTextBox;
        private System.Windows.Forms.TextBox email_AddressTextBox;
        private System.Windows.Forms.TextBox first_NameTextBox;
        private System.Windows.Forms.TextBox last_NameTextBox;
        private System.Windows.Forms.CheckBox chkSwipe;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCardNumber;
        private System.Windows.Forms.GroupBox grpSection;
        private System.Windows.Forms.CheckBox chkAction;
        private System.Windows.Forms.CheckBox chkRifle;
        private System.Windows.Forms.CheckBox chkSCA;
        private System.Windows.Forms.CheckBox chkSmallbore;
        private System.Windows.Forms.CheckBox chkHandgun;
        private System.Windows.Forms.CheckBox chkArchery;
        private System.Windows.Forms.TextBox sectionFlagTextBox;
        private System.Windows.Forms.ComboBox cboWalk;
        private System.Windows.Forms.DateTimePicker pal_Exp_DateDateTimePicker;
        private System.Windows.Forms.DateTimePicker birth_DateDateTimePicker;

    }
}