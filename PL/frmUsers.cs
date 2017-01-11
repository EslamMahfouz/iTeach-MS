using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Teach.PL
{
    public partial class frmUsers : XtraForm
    {
        EDM.TeachEntities db = new EDM.TeachEntities();

        public frmUsers()
        {
            InitializeComponent();
        }


        private void frmUsers_Load(object sender, EventArgs e)
        {
            var user = db.tblUsers.Find(1);
            txtUsername.Text = user.userName;
            txtPassword.Text = user.password;
        }
            
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) || txtPassword.Text != txtConfirm.Text)
            {
                XtraMessageBox.Show("برجاء التأكد من البيانات", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var user = db.tblUsers.Find(1);
            user.userName = txtUsername.Text;
            user.password = txtPassword.Text;
            db.SaveChanges();
            XtraMessageBox.Show("تم الحفظ بنجاح", "حفظ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

    }
}
