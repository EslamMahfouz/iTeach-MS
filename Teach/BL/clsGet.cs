using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using Teach.EDM;

namespace Teach.BL
{
    class clsGet
    {
        TeachEntities db = new TeachEntities();

        public void getClasses(int stageID, LookUpEdit x)
        {
            var classes = from z in db.tblClasses
                          where z.idStage == stageID
                          select new { م = z.idClass, الصف = z.nameClass };
            x.Properties.DataSource = classes.ToList();
            x.Properties.PopulateColumns();
            x.Properties.DisplayMember = "الصف";
            x.Properties.ValueMember = "م";
            x.Properties.Columns["م"].Visible = false;
        }

        public void getGroups(int classID, LookUpEdit x)
        {
            var groups = from z in db.tblGroups
                          where z.idClass == classID
                          select new { م = z.idGroup, المجموعة = z.nameGroup };
            x.Properties.DataSource = groups.ToList();
            x.Properties.PopulateColumns();
            x.Properties.DisplayMember = "المجموعة";
            x.Properties.ValueMember = "م";
            x.Properties.Columns["م"].Visible = false;
        }

    }
}
