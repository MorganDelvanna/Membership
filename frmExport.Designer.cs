namespace PFGA_Membership
{
    partial class frmExport
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
            this.dgExport = new System.Windows.Forms.DataGridView();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.firstName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lastName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CardName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardFormat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cardHex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.accessLevel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.activationDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expiryDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.uniqueId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgExport)).BeginInit();
            this.SuspendLayout();
            // 
            // dgExport
            // 
            this.dgExport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgExport.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.firstName,
            this.lastName,
            this.userCode,
            this.CardName,
            this.cardFormat,
            this.cardNumber,
            this.cardHex,
            this.accessLevel,
            this.activationDate,
            this.expiryDate,
            this.uniqueId});
            this.dgExport.Location = new System.Drawing.Point(27, 33);
            this.dgExport.Name = "dgExport";
            this.dgExport.RowHeadersWidth = 40;
            this.dgExport.Size = new System.Drawing.Size(745, 370);
            this.dgExport.TabIndex = 0;
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(616, 414);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(697, 414);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // firstName
            // 
            this.firstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.firstName.DataPropertyName = "firstName";
            this.firstName.HeaderText = "First Name";
            this.firstName.Name = "firstName";
            this.firstName.ReadOnly = true;
            this.firstName.Width = 82;
            // 
            // lastName
            // 
            this.lastName.DataPropertyName = "lastName";
            this.lastName.HeaderText = "Last Name";
            this.lastName.Name = "lastName";
            this.lastName.ReadOnly = true;
            // 
            // userCode
            // 
            this.userCode.DataPropertyName = "userCode";
            this.userCode.HeaderText = "User Code";
            this.userCode.Name = "userCode";
            this.userCode.ReadOnly = true;
            // 
            // CardName
            // 
            this.CardName.DataPropertyName = "cardName";
            this.CardName.HeaderText = "Card Name";
            this.CardName.Name = "CardName";
            this.CardName.Visible = false;
            // 
            // cardFormat
            // 
            this.cardFormat.DataPropertyName = "cardFormat";
            this.cardFormat.HeaderText = "Card Format";
            this.cardFormat.Name = "cardFormat";
            this.cardFormat.Visible = false;
            // 
            // cardNumber
            // 
            this.cardNumber.DataPropertyName = "cardNumber";
            this.cardNumber.HeaderText = "Swipe Card Number";
            this.cardNumber.Name = "cardNumber";
            // 
            // cardHex
            // 
            this.cardHex.DataPropertyName = "cardHex";
            this.cardHex.HeaderText = "Card Hex";
            this.cardHex.Name = "cardHex";
            this.cardHex.Visible = false;
            // 
            // accessLevel
            // 
            this.accessLevel.DataPropertyName = "accessLevel";
            this.accessLevel.HeaderText = "Access Level";
            this.accessLevel.Name = "accessLevel";
            this.accessLevel.Visible = false;
            // 
            // activationDate
            // 
            this.activationDate.DataPropertyName = "activationDate";
            this.activationDate.HeaderText = "Activation Date";
            this.activationDate.Name = "activationDate";
            this.activationDate.Visible = false;
            // 
            // expiryDate
            // 
            this.expiryDate.DataPropertyName = "expiryDate";
            this.expiryDate.HeaderText = "Expiry Date";
            this.expiryDate.Name = "expiryDate";
            this.expiryDate.Visible = false;
            // 
            // uniqueId
            // 
            this.uniqueId.DataPropertyName = "uniqueId";
            this.uniqueId.HeaderText = "Unique Id";
            this.uniqueId.Name = "uniqueId";
            this.uniqueId.Visible = false;
            // 
            // frmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.dgExport);
            this.Name = "frmExport";
            this.Text = "frmExport";
            this.Load += new System.EventHandler(this.frmExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgExport)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgExport;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn firstName;
        private System.Windows.Forms.DataGridViewTextBoxColumn lastName;
        private System.Windows.Forms.DataGridViewTextBoxColumn userCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn CardName;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardFormat;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn cardHex;
        private System.Windows.Forms.DataGridViewTextBoxColumn accessLevel;
        private System.Windows.Forms.DataGridViewTextBoxColumn activationDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn expiryDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn uniqueId;
    }
}