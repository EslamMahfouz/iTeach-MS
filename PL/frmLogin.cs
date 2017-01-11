using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Teach.EDM;

namespace Teach.PL
{
    public partial class frmLogin : XtraForm
    {
        private TeachEntities db = new TeachEntities();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text;
            var password = txtPassword.Text;
            var log = (from x in db.tblUsers
                       where x.userName == userName && x.password == password
                       select x).ToList();

            if (log.Count > 0)
            {
                DialogResult = DialogResult.OK;
            }
            else
            {
                XtraMessageBox.Show("من فضلك أدخل بيانات صحيحة", "خطأ في البيانات", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }
    }
}
