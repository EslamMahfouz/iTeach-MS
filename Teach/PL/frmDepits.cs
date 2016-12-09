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
using Teach.EDM;
using DevExpress.XtraEditors.Repository;

namespace Teach.PL
{
    public partial class frmDepits : XtraForm
    {
        TeachEntities db = new TeachEntities();

        public frmDepits()
        {
            InitializeComponent();
        }

        private void frmDepits_Load(object sender, EventArgs e)
        {
            RepositoryItemDateEdit riDateEdit = new RepositoryItemDateEdit();
            gridControl1.RepositoryItems.Add(riDateEdit);

            var std = (from x in db.tblAbsences
                       where x.paid == false
                       select new
                       {
                           الإسم = x.tblStudent.nameStudent,
                           المجموعة = x.tblStudent.tblGroup.nameGroup,
                           الصف = x.tblStudent.tblGroup.tblClass.nameClass,
                           المرحلة= x.tblStudent.tblGroup.tblClass.tblStage.nameStage,
                           رقم_ولي_الأمر = x.tblStudent.numberParent,
                           الشهر = x.date
                       }).ToList();
            gridControl1.DataSource = std;
            gridView1.PopulateColumns();
            gridView1.Columns["الشهر"].ColumnEdit = riDateEdit;
            riDateEdit.EditMask = "MM/yyyy";
            riDateEdit.Mask.UseMaskAsDisplayFormat = true;
            gridView1.Columns["الشهر"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
                e.Info.Kind = DevExpress.Utils.Drawing.IndicatorKind.Row;
            }
        }
    }
}