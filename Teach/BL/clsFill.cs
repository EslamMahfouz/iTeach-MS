using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using Teach.EDM;

namespace Teach.BL
{
    internal class clsFill
    {
        private TeachEntities db = new TeachEntities();

        public void fillStages(LookUpEdit x)
        {
            var stages = from z in db.tblStages
                        select new  { م = z.idStage, المرحلة = z.nameStage };

            x.Properties.DataSource = stages.ToList();
            x.Properties.PopulateColumns();
            x.Properties.DisplayMember = "المرحلة";
            x.Properties.ValueMember = "م";
            x.Properties.Columns["م"].Visible = false;
        }

        public void fillStudents(SearchLookUpEdit x)
        {
            var students = from z in db.tblStudents
                         select new { م = z.idStudent, الإسم = z.nameStudent, المرحلة = z.tblGroup.tblClass.tblStage.nameStage, الصف = z.tblGroup.tblClass.nameClass, المجموعة = z.tblGroup.nameGroup,  العنوان = z.addressStudent, هاتف_ولي_الأمر = z.numberParent };

            x.Properties.DataSource = students.ToList();
            x.Properties.PopulateViewColumns();
            x.Properties.DisplayMember = "الإسم";
            x.Properties.ValueMember = "م";
            x.Properties.View.Columns["م"].Visible = false;
        }

    }
}
