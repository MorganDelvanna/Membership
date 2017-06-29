namespace PFGA_Membership
{
    partial class frmLabels
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLabels));
            //this.crystalViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // crystalViewer
            //
            /*
            this.crystalViewer.ActiveViewIndex = -1;
            this.crystalViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.crystalViewer.DisplayGroupTree = false;
            this.crystalViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crystalViewer.Location = new System.Drawing.Point(0, 0);
            this.crystalViewer.Name = "crystalViewer";
            this.crystalViewer.SelectionFormula = "";
            this.crystalViewer.ShowGroupTreeButton = false;
            this.crystalViewer.Size = new System.Drawing.Size(682, 262);
            this.crystalViewer.TabIndex = 0;
            this.crystalViewer.ViewTimeSelectionFormula = "";
            this.crystalViewer.Load += new System.EventHandler(this.crystalViewer_Load);
             * */
            // 
            // frmLabels
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 262);
            //this.Controls.Add(this.crystalViewer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmLabels";
            this.Text = "frmLabels";
            this.Load += new System.EventHandler(this.frmLabels_Load);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmLabels_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        //private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalViewer;
    }
}