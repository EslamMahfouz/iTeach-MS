using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Teach.BL;
using Teach.EDM;

namespace Teach.PL
{
    public partial class frmChangeGroup : XtraForm
    {
        clsFill f = new clsFill();
        TeachEntities db = new TeachEntities();
        int idStudent;

        public frmChangeGroup()
        {
            InitializeComponent();
        }
        private void frmInfoStudent_Load(object sender, EventArgs e)
        {
            f.fillStudents(cmbStudents);
        }

        private void cmbStudents_EditValueChanged(object sender, EventArgs e)
        {
            idStudent = Convert.ToInt32(cmbStudents.EditValue);
            var std = db.tblStudents.Find(idStudent);
            var idClass = Convert.ToInt32(std.tblGroup.idClass);
            var gps = from x in db.tblGroups
                      where x.idClass == idClass
                      select new { م = x.idGroup, المجموعة = x.nameGroup };
            cmbGroups.Properties.DataSource = gps.ToList();
            cmbGroups.Properties.PopulateColumns();
            cmbGroups.Properties.DisplayMember = "المجموعة";
            cmbGroups.Properties.ValueMember = "م";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                idStudent = Convert.ToInt32(cmbStudents.EditValue);
                var std = db.tblStudents.Find(idStudent);
                std.idGroup = Convert.ToInt32(cmbGroups.EditValue);
                db.SaveChanges();
                XtraMessageBox.Show("تم تغيير المجموعة بنجاح", "تغيير المجموعة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            catch
            {
                return;
            }
        }
    }
}
