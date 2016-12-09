using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Teach.BL;
using Teach.EDM;
using System.Data;

namespace Teach.PL
{
    public partial class frmAddGroup : XtraForm
    {
        TeachEntities db = new TeachEntities();

        private clsFill f = new clsFill();
        private clsGet g = new clsGet();
        DataTable dt;


        private void ClrBoxs()
        {
            txtGroup.Text = "";
            txtMax.Text = "";
            btnAdd.Enabled = true;
            gridControl1.DataSource = null;
        }
        private Boolean isExist(string nameGroup)
        {
            var group = (from x in db.tblGroups
                        where x.nameGroup == nameGroup
                        select x).ToList();
            if (group.Count == 0)
                return false;
            else
                return true;
        }
        public frmAddGroup()
        {
            InitializeComponent();
            dt = new DataTable();
            dt.Columns.Add("اليوم");
            dt.Columns.Add("الساعة");
        }

        private void frmAddGroup_Load(object sender, EventArgs e)
        {
            f.fillStages(cmbStage);
            cmbStage.EditValue = 1;
        }

        private void cmbStage_EditValueChanged(object sender, EventArgs e)
        {
            int stageID = Convert.ToInt32(cmbStage.EditValue);
            g.getClasses(stageID, cmbClass);
            cmbClass.EditValue = 2;
        }

        private void btnAddToGrid_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["اليوم"] = cmbDays.Text;
            dr["الساعة"] = teTime.Text;
            dt.Rows.Add(dr);
            gridControl1.DataSource = dt;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!valName.Validate())
            {
                return;
            }
            var nameGroup = txtGroup.Text;
            if (txtMax.Text == "")
                txtMax.Text = "0";
            var maxNumber = Convert.ToInt32(txtMax.Text);
            var idClass = Convert.ToInt32(cmbClass.EditValue);

            if (isExist(nameGroup))
            {
                XtraMessageBox.Show("توجد مجموعة بهذا الإسم", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (dt.Rows.Count == 0)
            {
                XtraMessageBox.Show("يجب علي الأقل إختيار معاد واحد", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            tblGroup g = new tblGroup()
            {
                nameGroup = nameGroup,
                maxNumber = maxNumber,
                idClass = idClass
            };
            db.tblGroups.Add(g);

            foreach (DataRow dr in dt.Rows)
            {
                string day = dr["اليوم"].ToString();
                string hour = dr["الساعة"].ToString();
                tblRelation r = new tblRelation()
                {
                    idGroup = g.idGroup,
                    Day  = day,
                    Time = hour,
                };
                db.tblRelations.Add(r);
            }

            db.SaveChanges();
            XtraMessageBox.Show("تمت إضافة المجموعة بنجاح", "إضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
            ClrBoxs();
        }

    }
}
