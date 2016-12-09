namespace Teach.PL
{
    partial class frmPrintingSettings
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.cmbPrinters = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrinters.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.labelControl1.Location = new System.Drawing.Point(12, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(328, 24);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "برجاء إختيار الطابعة المستخدمة للطباعة";
            // 
            // cmbPrinters
            // 
            this.cmbPrinters.Location = new System.Drawing.Point(12, 42);
            this.cmbPrinters.Name = "cmbPrinters";
            this.cmbPrinters.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cmbPrinters.Properties.Appearance.Options.UseFont = true;
            this.cmbPrinters.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPrinters.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cmbPrinters.Size = new System.Drawing.Size(328, 30);
            this.cmbPrinters.TabIndex = 1;
            // 
            // btnSave
            // 
            this.btnSave.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.btnSave.Appearance.Options.UseFont = true;
            this.btnSave.Location = new System.Drawing.Point(150, 78);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(190, 56);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "حفظ";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmPrintingSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 143);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPrinters);
            this.Controls.Add(this.labelControl1);
            this.Name = "frmPrintingSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "إعدادات الطباعة";
            this.Load += new System.EventHandler(this.frmPrintingSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.cmbPrinters.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPrinters;
        private DevExpress.XtraEditors.SimpleButton btnSave;
    }
}