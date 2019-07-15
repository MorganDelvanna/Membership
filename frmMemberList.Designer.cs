namespace PFGA_Membership
{
    partial class frmMemberList
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMemberList));
            this.dgMemberList = new System.Windows.Forms.DataGridView();
            this.colCard = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFirstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colMemberType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDateJoined = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colWalk = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPaid = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnWalk = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnEdit = new System.Windows.Forms.DataGridViewButtonColumn();
            this.chkGeneral = new System.Windows.Forms.CheckBox();
            this.chkWalk = new System.Windows.Forms.CheckBox();
            this.chkPaid = new System.Windows.Forms.CheckBox();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.membersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuaddNew = new System.Windows.Forms.ToolStripMenuItem();
            this.pendingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toFullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toHalfYearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mailingListEmailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pendingMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renewingMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.makeCardsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.updateMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewCardLIstToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cboMemberTypeFilter = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.membership = new PFGA_Membership.Membership();
            this.membershipTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.membershipTypeTableAdapter = new PFGA_Membership.MembershipTableAdapters.MembershipTypeTableAdapter();
            this.halfToFullToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgMemberList)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.membership)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.membershipTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // dgMemberList
            // 
            this.dgMemberList.AllowUserToAddRows = false;
            this.dgMemberList.AllowUserToDeleteRows = false;
            this.dgMemberList.AllowUserToOrderColumns = true;
            this.dgMemberList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgMemberList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMemberList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colCard,
            this.colFirstName,
            this.colLastName,
            this.colYear,
            this.colMemberType,
            this.colDateJoined,
            this.colWalk,
            this.colPaid,
            this.colID,
            this.btnWalk,
            this.btnEdit});
            this.dgMemberList.Location = new System.Drawing.Point(12, 72);
            this.dgMemberList.Name = "dgMemberList";
            this.dgMemberList.ReadOnly = true;
            this.dgMemberList.Size = new System.Drawing.Size(1009, 473);
            this.dgMemberList.TabIndex = 0;
            // 
            // colCard
            // 
            this.colCard.HeaderText = "Card";
            this.colCard.Name = "colCard";
            this.colCard.ReadOnly = true;
            // 
            // colFirstName
            // 
            this.colFirstName.HeaderText = "First Name";
            this.colFirstName.Name = "colFirstName";
            this.colFirstName.ReadOnly = true;
            // 
            // colLastName
            // 
            this.colLastName.HeaderText = "Last Name";
            this.colLastName.Name = "colLastName";
            this.colLastName.ReadOnly = true;
            // 
            // colYear
            // 
            this.colYear.HeaderText = "Membership Year";
            this.colYear.Name = "colYear";
            this.colYear.ReadOnly = true;
            // 
            // colMemberType
            // 
            this.colMemberType.HeaderText = "Membership Type";
            this.colMemberType.Name = "colMemberType";
            this.colMemberType.ReadOnly = true;
            // 
            // colDateJoined
            // 
            this.colDateJoined.HeaderText = "Date Joined";
            this.colDateJoined.Name = "colDateJoined";
            this.colDateJoined.ReadOnly = true;
            // 
            // colWalk
            // 
            this.colWalk.HeaderText = "Walk";
            this.colWalk.Name = "colWalk";
            this.colWalk.ReadOnly = true;
            // 
            // colPaid
            // 
            this.colPaid.HeaderText = "Paid";
            this.colPaid.Name = "colPaid";
            this.colPaid.ReadOnly = true;
            this.colPaid.Visible = false;
            // 
            // colID
            // 
            this.colID.HeaderText = "ID";
            this.colID.Name = "colID";
            this.colID.ReadOnly = true;
            this.colID.Visible = false;
            // 
            // btnWalk
            // 
            this.btnWalk.HeaderText = "";
            this.btnWalk.Name = "btnWalk";
            this.btnWalk.ReadOnly = true;
            this.btnWalk.Text = "Walk";
            this.btnWalk.UseColumnTextForButtonValue = true;
            this.btnWalk.Width = 50;
            // 
            // btnEdit
            // 
            this.btnEdit.HeaderText = "";
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.ReadOnly = true;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseColumnTextForButtonValue = true;
            this.btnEdit.Width = 50;
            // 
            // chkGeneral
            // 
            this.chkGeneral.AutoSize = true;
            this.chkGeneral.Location = new System.Drawing.Point(12, 27);
            this.chkGeneral.Name = "chkGeneral";
            this.chkGeneral.Size = new System.Drawing.Size(94, 17);
            this.chkGeneral.TabIndex = 2;
            this.chkGeneral.Text = "Show Inactive";
            this.chkGeneral.UseVisualStyleBackColor = true;
            this.chkGeneral.CheckedChanged += new System.EventHandler(this.chkGeneral_CheckedChanged);
            // 
            // chkWalk
            // 
            this.chkWalk.AutoSize = true;
            this.chkWalk.Location = new System.Drawing.Point(112, 27);
            this.chkWalk.Name = "chkWalk";
            this.chkWalk.Size = new System.Drawing.Size(124, 17);
            this.chkWalk.TabIndex = 3;
            this.chkWalk.Text = "Highlight Need Walk";
            this.chkWalk.UseVisualStyleBackColor = true;
            this.chkWalk.CheckedChanged += new System.EventHandler(this.chkWalk_CheckedChanged);
            // 
            // chkPaid
            // 
            this.chkPaid.AutoSize = true;
            this.chkPaid.Location = new System.Drawing.Point(242, 27);
            this.chkPaid.Name = "chkPaid";
            this.chkPaid.Size = new System.Drawing.Size(131, 17);
            this.chkPaid.TabIndex = 4;
            this.chkPaid.Text = "Highlight Haven\'t Paid";
            this.chkPaid.UseVisualStyleBackColor = true;
            this.chkPaid.CheckedChanged += new System.EventHandler(this.chkPaid_CheckedChanged);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(921, 27);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(100, 20);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextSearch_KeyPress);
            this.txtSearch.Leave += new System.EventHandler(this.FilterMemberList);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(871, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(44, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Search:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.membersToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.cardsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1033, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // membersToolStripMenuItem
            // 
            this.membersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuaddNew,
            this.pendingToolStripMenuItem,
            this.halfToFullToolStripMenuItem});
            this.membersToolStripMenuItem.Name = "membersToolStripMenuItem";
            this.membersToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.membersToolStripMenuItem.Text = "Members";
            // 
            // mnuaddNew
            // 
            this.mnuaddNew.Name = "mnuaddNew";
            this.mnuaddNew.Size = new System.Drawing.Size(152, 22);
            this.mnuaddNew.Text = "&Add New";
            this.mnuaddNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // pendingToolStripMenuItem
            // 
            this.pendingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toFullToolStripMenuItem,
            this.toHalfYearToolStripMenuItem});
            this.pendingToolStripMenuItem.Name = "pendingToolStripMenuItem";
            this.pendingToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.pendingToolStripMenuItem.Text = "Pending";
            // 
            // toFullToolStripMenuItem
            // 
            this.toFullToolStripMenuItem.Name = "toFullToolStripMenuItem";
            this.toFullToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.toFullToolStripMenuItem.Text = "To Full";
            this.toFullToolStripMenuItem.Click += new System.EventHandler(this.toFullToolStripMenuItem_Click);
            // 
            // toHalfYearToolStripMenuItem
            // 
            this.toHalfYearToolStripMenuItem.Name = "toHalfYearToolStripMenuItem";
            this.toHalfYearToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.toHalfYearToolStripMenuItem.Text = "To Half-Year";
            this.toHalfYearToolStripMenuItem.Click += new System.EventHandler(this.toHalfYearToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportMembersToolStripMenuItem,
            this.mailingListEmailsToolStripMenuItem,
            this.pendingMembersToolStripMenuItem,
            this.renewingMembersToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // exportMembersToolStripMenuItem
            // 
            this.exportMembersToolStripMenuItem.Name = "exportMembersToolStripMenuItem";
            this.exportMembersToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.exportMembersToolStripMenuItem.Text = "Export Members";
            this.exportMembersToolStripMenuItem.Click += new System.EventHandler(this.exportMembersToolStripMenuItem_Click);
            // 
            // mailingListEmailsToolStripMenuItem
            // 
            this.mailingListEmailsToolStripMenuItem.Name = "mailingListEmailsToolStripMenuItem";
            this.mailingListEmailsToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.mailingListEmailsToolStripMenuItem.Text = "Mailing List Emails";
            this.mailingListEmailsToolStripMenuItem.Click += new System.EventHandler(this.mailingListEmailsToolStripMenuItem_Click);
            // 
            // pendingMembersToolStripMenuItem
            // 
            this.pendingMembersToolStripMenuItem.Name = "pendingMembersToolStripMenuItem";
            this.pendingMembersToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.pendingMembersToolStripMenuItem.Text = "Pending Members";
            this.pendingMembersToolStripMenuItem.Click += new System.EventHandler(this.pendingMembersToolStripMenuItem_Click);
            // 
            // renewingMembersToolStripMenuItem
            // 
            this.renewingMembersToolStripMenuItem.Name = "renewingMembersToolStripMenuItem";
            this.renewingMembersToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.renewingMembersToolStripMenuItem.Text = "Renewing Members";
            this.renewingMembersToolStripMenuItem.Click += new System.EventHandler(this.renewingMembersToolStripMenuItem_Click);
            // 
            // cardsToolStripMenuItem
            // 
            this.cardsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.makeCardsToolStripMenuItem,
            this.updateMembersToolStripMenuItem,
            this.viewCardLIstToolStripMenuItem});
            this.cardsToolStripMenuItem.Name = "cardsToolStripMenuItem";
            this.cardsToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.cardsToolStripMenuItem.Text = "Cards";
            // 
            // makeCardsToolStripMenuItem
            // 
            this.makeCardsToolStripMenuItem.Name = "makeCardsToolStripMenuItem";
            this.makeCardsToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.makeCardsToolStripMenuItem.Text = "Make Cards";
            this.makeCardsToolStripMenuItem.Click += new System.EventHandler(this.makeCardsToolStripMenuItem_Click);
            // 
            // updateMembersToolStripMenuItem
            // 
            this.updateMembersToolStripMenuItem.Name = "updateMembersToolStripMenuItem";
            this.updateMembersToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.updateMembersToolStripMenuItem.Text = "Update Members";
            this.updateMembersToolStripMenuItem.Click += new System.EventHandler(this.updateMembersToolStripMenuItem_Click);
            // 
            // viewCardLIstToolStripMenuItem
            // 
            this.viewCardLIstToolStripMenuItem.Name = "viewCardLIstToolStripMenuItem";
            this.viewCardLIstToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.viewCardLIstToolStripMenuItem.Text = "View Card LIst";
            this.viewCardLIstToolStripMenuItem.Click += new System.EventHandler(this.viewCardLIstToolStripMenuItem_Click);
            // 
            // cboMemberTypeFilter
            // 
            this.cboMemberTypeFilter.DataSource = this.membershipTypeBindingSource;
            this.cboMemberTypeFilter.DisplayMember = "Membership Type";
            this.cboMemberTypeFilter.FormattingEnabled = true;
            this.cboMemberTypeFilter.Location = new System.Drawing.Point(518, 25);
            this.cboMemberTypeFilter.Name = "cboMemberTypeFilter";
            this.cboMemberTypeFilter.Size = new System.Drawing.Size(147, 21);
            this.cboMemberTypeFilter.TabIndex = 11;
            this.cboMemberTypeFilter.SelectedIndexChanged += new System.EventHandler(this.cboMemberTypeFilter_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(396, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Membership Type Filter";
            // 
            // membership
            // 
            this.membership.DataSetName = "Membership";
            this.membership.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // membershipTypeBindingSource
            // 
            this.membershipTypeBindingSource.DataMember = "MembershipType";
            this.membershipTypeBindingSource.DataSource = this.membership;
            // 
            // membershipTypeTableAdapter
            // 
            this.membershipTypeTableAdapter.ClearBeforeFill = true;
            // 
            // halfToFullToolStripMenuItem
            // 
            this.halfToFullToolStripMenuItem.Name = "halfToFullToolStripMenuItem";
            this.halfToFullToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.halfToFullToolStripMenuItem.Text = "Half to Full";
            this.halfToFullToolStripMenuItem.Click += new System.EventHandler(this.halfToFullToolStripMenuItem_Click);
            // 
            // frmMemberList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 557);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMemberTypeFilter);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.chkPaid);
            this.Controls.Add(this.chkWalk);
            this.Controls.Add(this.chkGeneral);
            this.Controls.Add(this.dgMemberList);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMemberList";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Membership List";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMemberList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgMemberList)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.membership)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.membershipTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgMemberList;
        private System.Windows.Forms.CheckBox chkGeneral;
        private System.Windows.Forms.CheckBox chkWalk;
        private System.Windows.Forms.CheckBox chkPaid;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem membersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuaddNew;
        private System.Windows.Forms.ToolStripMenuItem exportMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mailingListEmailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem makeCardsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem updateMembersToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCard;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFirstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMemberType;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDateJoined;
        private System.Windows.Forms.DataGridViewTextBoxColumn colWalk;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPaid;
        private System.Windows.Forms.DataGridViewTextBoxColumn colID;
        private System.Windows.Forms.DataGridViewButtonColumn btnWalk;
        private System.Windows.Forms.DataGridViewButtonColumn btnEdit;
        private System.Windows.Forms.ToolStripMenuItem pendingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toFullToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toHalfYearToolStripMenuItem;
        private System.Windows.Forms.ComboBox cboMemberTypeFilter;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolStripMenuItem viewCardLIstToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pendingMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renewingMembersToolStripMenuItem;
        private Membership membership;
        private System.Windows.Forms.BindingSource membershipTypeBindingSource;
        private MembershipTableAdapters.MembershipTypeTableAdapter membershipTypeTableAdapter;
        private System.Windows.Forms.ToolStripMenuItem halfToFullToolStripMenuItem;

    }
}

