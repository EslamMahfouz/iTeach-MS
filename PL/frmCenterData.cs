using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Security.AccessControl;

namespace Teach.PL
{
    public partial class frmCenterData : XtraForm
    {
        EDM.TeachEntities db = new EDM.TeachEntities();

        public frmCenterData()
        {
            InitializeComponent();
        }

        private void frmGymData_Load(object sender, EventArgs e)
        {
            try
            {
                var centerData = db.CenterDatas.Find(1);
                txtName.Text = centerData.Name;
                txtAddress.Text = centerData.Address;
                txtPhone1.Text = centerData.Phone1;
                txtPhone2.Text = centerData.Phone2;
                txtMail.Text = centerData.Mail;
                txtSubject.Text = centerData.Subject;
                txtMaxDegree.Text = Properties.Settings.Default.maxDegree.ToString();
                txtTotal.Text = Properties.Settings.Default.totalAttendance.ToString();

                byte[] img = centerData.Logo;
                MemoryStream ms = new MemoryStream(img);
                pBox.Image = Image.FromStream(ms);
                txtBackup.Text = Properties.Settings.Default.BackUpFolder;
            }
            catch
            {
                return;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = folderBrowserDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtBackup.Text = folderBrowserDialog1.SelectedPath;
                    Properties.Settings.Default.BackUpFolder = txtBackup.Text;
                    Properties.Settings.Default.Save();
                    File.SetAttributes(Properties.Settings.Default.BackUpFolder, File.GetAttributes(Properties.Settings.Default.BackUpFolder) & ~FileAttributes.ReadOnly);
                    DirectoryInfo dInfo = new DirectoryInfo(Properties.Settings.Default.BackUpFolder);
                    DirectorySecurity dSecurity = dInfo.GetAccessControl();
                    dSecurity.AddAccessRule(new FileSystemAccessRule("Everyone", FileSystemRights.FullControl, AccessControlType.Allow));
                    dInfo.SetAccessControl(dSecurity);
                }
            }
            catch
            {
                XtraMessageBox.Show("برجاء إختيار فولدر أخر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            MemoryStream ms = new MemoryStream();
            pBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            byte[] img = ms.ToArray();

            var cd = db.CenterDatas.Find(1);
            cd.Name = txtName.Text;
            cd.Address = txtAddress.Text;
            cd.Phone1 = txtPhone1.Text;
            cd.Phone2 = txtPhone2.Text;
            cd.Mail = txtMail.Text;
            cd.Logo = img;
            cd.Subject = txtSubject.Text;
            db.SaveChanges();
            Properties.Settings.Default.maxDegree = Convert.ToDouble(txtMaxDegree.Text);
            Properties.Settings.Default.totalAttendance = Convert.ToInt32(txtTotal.Text);
            Properties.Settings.Default.Save();
            XtraMessageBox.Show("تم حفظ التعديلات بنجاح", "تعديل", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }        
    }
}