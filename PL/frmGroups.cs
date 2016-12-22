using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Teach.BL;
using Teach.Reports;
using DevExpress.XtraReports.UI;
using Teach.EDM;
using System.Globalization;

namespace Teach.PL
{
    public partial class frmGroups : XtraForm
    {
        TeachEntities db = new TeachEntities();
        clsFill f = new clsFill(); clsGet g = new clsGet(); clsAdd a = new clsAdd();
        clsAbsence absence = new clsAbsence();

        private int idGroup, lastNum;
        private bool isNew = false;
        private void fillAbsence()
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;

            var stds = from x in db.tblStudents
                      where x.idGroup == idGroup
                      select x;

            foreach (var item in stds)
            {
                var abs = from x in db.tblAbsences
                          where x.idStudent == item.idStudent && x.date.Year == year && x.date.Month == month
                          select x;
                if (abs.ToList().Count == 0)
                {
                    a.addAbsence(item.idStudent);
                }
            }
        }
        private void fillGrids()
        {
            try
            {
                lastNum = 0;
                var date = Convert.ToDateTime(dateEdit1.EditValue);

                var dates = from x in db.tblRelations
                            where x.idGroup == idGroup
                            select new { م = x.idRelation, اليوم = x.Day, الساعة = x.Time };
                gridControl1.DataSource = dates.ToList();
                gridView5.Columns["م"].Visible = false;

                getStudentsOfGroupTableAdapter.Fill(dsGetStudentsofGroup.getStudentsOfGroup, idGroup);
                getAbsenceOfGroupTableAdapter.Fill(dsGetAbsenceOfGroup.getAbsenceOfGroup, idGroup, date);
                gridView2.Columns["colرقمالكشف1"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
            }
            catch
            {
                return;
            }
        }

        public frmGroups()
        {
            InitializeComponent();
            dateEdit1.EditValue = DateTime.Now.Date;
            dtMonth.EditValue = DateTime.Now.Date;
        }
        private void frmGroups_Load(object sender, EventArgs e)
        {
            f.fillStages(cmbStage);
            cmbStage.EditValue = 1;
        }
        private void xtraTabControl1_SelectedPageChanging(object sender, DevExpress.XtraTab.TabPageChangingEventArgs e)
        {
            fillAbsence();
            fillGrids();
        }

        private void cmbStage_EditValueChanged(object sender, EventArgs e)
        {
            var idStage = Convert.ToInt32(cmbStage.EditValue);
            g.getClasses(idStage, cmbClass);
            cmbClass.EditValue = 2;
        }
        private void cmbClass_EditValueChanged(object sender, EventArgs e)
        {
            var idClass = Convert.ToInt32(cmbClass.EditValue);
            g.getGroups(idClass, cmbGroup);
        }
        private void cmbGroup_EditValueChanged(object sender, EventArgs e)
        {
            idGroup = Convert.ToInt32(cmbGroup.EditValue);
            fillAbsence();
            fillGrids();
        }
        private void dateEdit1_EditValueChanged(object sender, EventArgs e)
        {
            fillGrids();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            groupControl1.Enabled = true;
        }
        private void btnAddDate_Click(object sender, EventArgs e)
        {
            groupControl2.Visible = true;
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var day = cmbDays.Text;
                var hour = teTime.Text;
                tblRelation r = new tblRelation()
                {
                    idGroup = idGroup,
                    Day = day,
                    Time = hour,
                };
                db.tblRelations.Add(r);
                db.SaveChanges();
                XtraMessageBox.Show("تم إضافة المعاد", "إضافة", MessageBoxButtons.OK, MessageBoxIcon.Information);
                groupControl2.Visible = false;
                groupControl1.Enabled = false;
                cmbGroup_EditValueChanged(sender, e);
            }
            catch
            {
                XtraMessageBox.Show("برجاء ملء جميع الحقول", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("تأكيد حذف المجموعة؟", "حذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var group = db.tblGroups.Find(idGroup);
                db.tblGroups.Remove(group);
                db.SaveChanges();
                XtraMessageBox.Show("تم الحذف بنجاح", "حذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                gridControl1.DataSource = null;
                cmbClass_EditValueChanged(sender, e);
            }
        }
        private void btnDeleteDate_Click(object sender, EventArgs e)
        {
            try
            {
                int idRelation = Convert.ToInt32(gridView5.GetFocusedRowCellValue("م"));
                var relation = db.tblRelations.Find(idRelation);
                db.tblRelations.Remove(relation);
                db.SaveChanges();
                cmbGroup_EditValueChanged(sender, e);
            }
            catch
            {
                return;
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (!isNew)
            {
                var idStudent = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colم1));
                var nameStudent = gridView2.GetFocusedRowCellValue(colالإسم).ToString();
                var addressStudent = gridView2.GetFocusedRowCellValue(colالعنوان).ToString();
                var schoolStudent = gridView2.GetFocusedRowCellValue(colالمدرسة).ToString();
                var numberParent = gridView2.GetFocusedRowCellValue(colرقموليالأمر).ToString();
                var notes = gridView2.GetFocusedRowCellValue(colملاحظات).ToString();
                var numInLog = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colرقمالكشف1));

                var std = db.tblStudents.Find(idStudent);
                std.nameStudent = nameStudent;
                std.addressStudent = addressStudent;
                std.schoolStudent = schoolStudent;
                std.numberParent = numberParent;
                std.notes = notes;
                std.numInLog = numInLog;
                db.SaveChanges();
            }
        }
        private void gridView2_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
                if (isNew)
                {
                    var nameStudent = gridView2.GetFocusedRowCellValue(colالإسم).ToString();
                    var addressStudent = gridView2.GetFocusedRowCellValue(colالعنوان).ToString();
                    var schoolStudent = gridView2.GetFocusedRowCellValue(colالمدرسة).ToString();
                    var numberParent = gridView2.GetFocusedRowCellValue(colرقموليالأمر).ToString();
                    var notes = gridView2.GetFocusedRowCellValue(colملاحظات).ToString();
                    var numInLog = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colرقمالكشف1));

                    if (nameStudent == "")
                    {
                        XtraMessageBox.Show("برجاء كتابة إسم الطالب ", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    for (int i = 0; i < gridView2.DataRowCount - 1; i++)
                    {
                        int temp = Convert.ToInt32(gridView2.GetRowCellValue(i, colرقمالكشف1));
                        if (temp == Convert.ToInt32(numInLog))
                        {
                            XtraMessageBox.Show("هذا الرقم موجود في الكشف بالفعل", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    tblStudent std = new tblStudent()
                    {
                        idGroup = idGroup,
                        nameStudent = nameStudent,
                        addressStudent = addressStudent,
                        schoolStudent = schoolStudent,
                        numberParent = numberParent,
                        notes = notes,
                        numInLog = numInLog
                    };
                    db.tblStudents.Add(std);
                    db.SaveChanges();
                    isNew = false;
                }
        }
        private void gridControl2_EmbeddedNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            if (e.Button.ButtonType == NavigatorButtonType.Append)
            {
                isNew = true;
                for (int i = 0; i < gridView2.RowCount; i++)
                {
                    int temp = Convert.ToInt32(gridView2.GetRowCellValue(i, colرقمالكشف1));
                    if (temp > lastNum)
                        lastNum = temp;
                }
            }
            else if (e.Button.ButtonType == NavigatorButtonType.Remove)
            {
                if (XtraMessageBox.Show("تأكيد الحذف؟", "سؤال", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        var idStudent = Convert.ToInt32(gridView2.GetFocusedRowCellValue(colم1));
                        var std = db.tblStudents.Find(idStudent);
                        db.tblStudents.Remove(std);
                        db.SaveChanges();
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }
        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            view.SetRowCellValue(e.RowHandle, colرقمالكشف1, lastNum + 1);
        }

        private void gridView3_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            bool paid = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(colالدفع));
            var idStudent = Convert.ToInt32(gridView3.GetFocusedRowCellValue(colم));
            var date = Convert.ToDateTime(dateEdit1.EditValue);
            int yaer = date.Year, month = date.Month;

            var day1 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col1));
            var day2 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col2));
            var day3 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col3));
            var day4 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col4));
            var day5 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col5));
            var day6 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col6));
            var day7 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col7));
            var day8 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col8));
            var day9 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col9));
            var day10 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col10));
            var day11 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col11));
            var day12 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col12));
            var day13 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col13));
            var day14 = Convert.ToBoolean(gridView3.GetFocusedRowCellValue(col14));
            if (e.Column.Name == "colالدفع" && Convert.ToBoolean(gridView3.GetFocusedRowCellValue(colالدفع)) == true)
            {
                if (XtraMessageBox.Show("هل تريد تأكيد الدفع؟", "الدفع", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    paid = true;
                else
                {
                    fillGrids();
                    return;
                }
            }
            if (e.Column.Name == "colالدفع" && Convert.ToBoolean(gridView3.GetFocusedRowCellValue(colالدفع)) == false)
            {
                if (XtraMessageBox.Show("هل تريد تأكيد إلغاء الدفع؟", "إلغاء دفع", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    paid = false;
                else
                {
                    fillGrids();
                    return;
                }
            }
            var exam = Convert.ToDouble(gridView3.GetFocusedRowCellValue(colالإمتحان));


            var absence = from x in db.tblAbsences
                       where x.date.Year == yaer && x.date.Month == month && x.idStudent == idStudent
                       select x;
            foreach (var abs in absence)
            {
                abs.day1 = day1;
                abs.day2 = day2;
                abs.day3 = day3;
                abs.day4 = day4;
                abs.day5 = day5;
                abs.day6 = day6;
                abs.day7 = day7;
                abs.day8 = day8;
                abs.day9 = day9;
                abs.day10 = day10;
                abs.day11 = day11;
                abs.day12 = day12;
                abs.day13 = day13;
                abs.day14 = day14;
                abs.paid = paid;
                abs.exam = exam;
                break;
            }
            db.SaveChanges();
        }
        
        private void btnReport_Click(object sender, EventArgs e)
        {
            try
            {
                var report = new repMonthly();
                var attend = 0;

                var idStudent = Convert.ToInt32(gridView3.GetFocusedRowCellValue(colم));
                var date = Convert.ToDateTime(dateEdit1.EditValue);
                var cd = db.CenterDatas.Find(1);
                report.Name = cd.Name;
                report.address.Value = cd.Address;
                report.Phone1.Value = cd.Phone1;
                report.Phone2.Value = cd.Phone2;
                report.subject.Value = cd.Subject;
                DateTime month = Convert.ToDateTime(dateEdit1.EditValue);
                report.month.Value = month.ToString("MMMM، yyyy", new CultureInfo("ar-EG"));
                var dt = absence.repMonthly(idStudent, date);

                report.paramDegree.Value = dt.Rows[0]["exam"].ToString();
                report.paramMaxDegree.Value = Properties.Settings.Default.maxDegree;
                report.paramName.Value = gridView3.GetFocusedRowCellValue(colالطالب).ToString();
                if (Convert.ToBoolean(dt.Rows[0]["paid"]))
                    report.paramPaid.Value = "تم الدفع";
                else
                    report.paramPaid.Value = "لم يتم الدفع";
                for (var i = 2; i < 16; i++)
                {
                    if (Convert.ToBoolean(dt.Rows[0][i]))
                    {
                        attend++;
                    }
                }
                report.paramAttend.Value = attend;
                report.paramTotal.Value = 14;
                report.ShowPreview();
            }
            catch
            {
                return;
            }
        }
        private void btnRepGroup_Click(object sender, EventArgs e)
        {
            try
            {
                var report = new repMonthly();
                var date = Convert.ToDateTime(dateEdit1.EditValue);
                var cd = db.CenterDatas.Find(1);
                report.Name = cd.Name;
                report.address.Value = cd.Address;
                report.Phone1.Value = cd.Phone1;
                report.Phone2.Value = cd.Phone2;
                report.subject.Value = cd.Subject;
                DateTime month = Convert.ToDateTime(dateEdit1.EditValue);
                report.month.Value = month.ToString("MMMM، yyyy", new CultureInfo("ar-EG"));

                for (int i = 0; i < gridView3.RowCount; i++)
                {
                    var attend = 0;
                    var idStudent = Convert.ToInt32(gridView3.GetRowCellValue(i, colم));
                    var dt = absence.repMonthly(idStudent, date);
                    report.paramDegree.Value = dt.Rows[0]["exam"].ToString();
                    report.paramMaxDegree.Value = Properties.Settings.Default.maxDegree;
                    report.paramName.Value = gridView3.GetRowCellValue(i, colالطالب).ToString();
                    if (Convert.ToBoolean(dt.Rows[0]["paid"]))
                        report.paramPaid.Value = "تم الدفع";
                    else
                        report.paramPaid.Value = "لم يتم الدفع";
                    for (var x = 2; x < 16; x++)
                    {
                        if (Convert.ToBoolean(dt.Rows[0][x]))
                        {
                            attend++;
                        }
                    }
                    report.paramAttend.Value = attend;
                    report.paramTotal.Value = 14;
                    report.CreateDocument();
                    report.Print(Properties.Settings.Default.Printer);
                }
            }
            catch
            {
                return;
            }
        }

        private void btnGrd_Click(object sender, EventArgs e)
        {
            if (txtPrice.Text == string.Empty)
            {
                XtraMessageBox.Show("أدخل سعر الشهر", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtPrice.Focus();
                return;
            }
            var idGroup = Convert.ToInt32(cmbGroup.EditValue);
            var date = Convert.ToDateTime(dtMonth.EditValue);

            txtPaid.Text = absence.getPaid(idGroup, date).Rows.Count.ToString();
            txtNotPaid.Text = absence.getNotPaid(idGroup, date).Rows.Count.ToString();

            txtDone.Text = (Convert.ToInt32(txtPaid.Text) * Convert.ToInt32(txtPrice.Text)).ToString();
            txtStill.Text = (Convert.ToInt32(txtNotPaid.Text) * Convert.ToInt32(txtPrice.Text)).ToString();

            txtTotal.Text = (Convert.ToInt32(txtDone.Text) + Convert.ToInt32(txtStill.Text)).ToString();
        }
    }
}
