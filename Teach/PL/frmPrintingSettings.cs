using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Drawing.Printing;

namespace Teach.PL
{
    public partial class frmPrintingSettings : DevExpress.XtraEditors.XtraForm
    {
        public frmPrintingSettings()
        {
            InitializeComponent();
        }

        private void frmPrintingSettings_Load(object sender, EventArgs e)
        {
            foreach (String strPrinter in PrinterSettings.InstalledPrinters)
            {
                cmbPrinters.Properties.Items.Add(strPrinter);
            }
            cmbPrinters.Text = Properties.Settings.Default.Printer;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Printer = cmbPrinters.Text;
            Properties.Settings.Default.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}