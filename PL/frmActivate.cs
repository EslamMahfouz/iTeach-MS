using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Net.NetworkInformation;

namespace Teach.PL
{
    public partial class frmActivate : DevExpress.XtraEditors.XtraForm
    {
        public static string GetMACAddress()
        {
            var nics = NetworkInterface.GetAllNetworkInterfaces();
            var sMacAddress = string.Empty;
            foreach (NetworkInterface adapter in nics)
            {
                if (sMacAddress == String.Empty)
                {
                    sMacAddress = adapter.GetPhysicalAddress().ToString();
                }
            }
            return sMacAddress;
        }

        private static int CalculateHash(string serial)
        {
            var hashedValue = 0;
            for (var i = 0; i < serial.Length; i++)
            {
                hashedValue += (int)Convert.ToChar(serial[i]);
                hashedValue *= 4;
            }
            return hashedValue;
        }
        private string calc()
        {
            return CalculateHash(txtSerial.Text).ToString();
        }

        public frmActivate()
        {
            InitializeComponent();
            txtSerial.Text = GetMACAddress();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            try
            {
                var check = calc();
                if (Convert.ToInt32(txtCode.Text) == Convert.ToInt32(check))
                {
                    Properties.Settings.Default.isPaid = true;
                    Properties.Settings.Default.Save();
                    XtraMessageBox.Show("تم التفعيل بنجاح", "التفعيل", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    XtraMessageBox.Show("من فضلك أدخل رقم سيريال صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCode.Text = string.Empty;
                }
            }
            catch
            {
                XtraMessageBox.Show("من فضلك أدخل رقم سيريال صحيح", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }
    }
}
