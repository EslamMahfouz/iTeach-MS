﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTabbedMdi;
using DevExpress.XtraEditors;
using System.IO;
using System.Data.SqlClient;
using Teach.EDM;
using DevExpress.XtraCharts;
using System.Globalization;

namespace Teach.PL
{
    public partial class frmMain : XtraForm
    {
        TeachEntities db = new TeachEntities();

        void addForm(XtraForm frm)
        {
            showCharts(false);

            List<Form> openForms = new List<Form>();
            foreach (Form f in Application.OpenForms)
                openForms.Add(f);

            foreach (Form f in openForms)
            {
                if (f.Name != "frmMain")
                    f.Close();
            }

            frm.MdiParent = this;
            frm.Show();
        }
        void fillCharts()
        {
            DateTime dt = DateTime.Now.Date;
            DateTime dtLast = dt.AddMonths(-1);
            int thisYear = dt.Year, lastYear = dtLast.Year, thisMonth = dt.Month, lastMonth = dtLast.Month;

            var paidThisMonth = (from x in db.tblAbsences
                                 where x.date.Year == thisYear && x.date.Month == thisMonth && x.paid == true
                                 select x).ToList();
            var notPaidThisMonth = (from x in db.tblAbsences
                                    where x.date.Year == thisYear && x.date.Month == thisMonth && x.paid == false
                                    select x).ToList();

            var paidLastMonth = (from x in db.tblAbsences
                                 where x.date.Year == lastYear && x.date.Month == lastMonth && x.paid == true
                                 select x).ToList();
            var notPaidLastMonth = (from x in db.tblAbsences
                                    where x.date.Year == lastYear && x.date.Month == lastMonth && x.paid == false
                                    select x).ToList();

            Series thisPaid = new Series("الشهر الحالي", ViewType.Bar);
            thisPaid.Points.Add(new SeriesPoint("عدد من دفع", paidThisMonth.Count));
            thisPaid.Points.Add(new SeriesPoint("عدد من لم يدفع", notPaidThisMonth.Count));

            Series lastPaid = new Series("الشهر السابق", ViewType.Bar);
            lastPaid.Points.Add(new SeriesPoint("عدد من دفع", paidLastMonth.Count));
            lastPaid.Points.Add(new SeriesPoint("عدد من لم يدفع", notPaidLastMonth.Count));

            chartPaid.Series.Add(thisPaid);
            chartPaid.Series.Add(lastPaid);

            string day = DateTime.Now.ToString("dddd", new CultureInfo("ar-EG"));
            var gps = (from x in db.tblRelations
                      where x.Day == day
                      select new {المجموعة = x.tblGroup.nameGroup, الساعة = x.Time }).ToList();
            gridControl1.DataSource = gps;
        }
        void showCharts(bool status)
        {
            chartPaid.Visible = status;
            gridControl1.Visible = status;
        }
       
        public frmMain()
        {
            InitializeComponent();
        }
        private void frmMain_Load(object sender, EventArgs e)
        {
            //var xfrm = new frmLogin();
            //xfrm.ShowDialog();

            if (Properties.Settings.Default.isPaid == false)
            {
                var _frm = new frmActivate();
                _frm.ShowDialog();
            }
            fillCharts();
        }

        private void btnPrintingSettings_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmPrintingSettings frm = new frmPrintingSettings();
            frm.ShowDialog();
        }
        private void btnCenterData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmCenterData frm = new frmCenterData();
            addForm(frm);
        }

        private void btnAddGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var frm = new frmAddGroup();
            frm.ShowDialog();
        }
        private void btnShowGroups_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var frm = new frmGroups();
            addForm(frm);
        }

        private void btnChangeGroup_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            var frm = new frmChangeGroup();
            frm.ShowDialog();
        }
        private void btnDepits_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            frmDepits frm = new frmDepits();
            addForm(frm);
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SqlConnection sqlconnection = new SqlConnection(@"Server=.\SQLEXPRESS; Database=master; Integrated Security=true");
                SqlCommand cmd;

                string combined = Path.Combine(Properties.Settings.Default.BackUpFolder, "TeachBackup.bak");
                File.Delete(combined);
                string query = "Backup Database Teach to Disk='" + combined + "'";
                cmd = new SqlCommand(query, sqlconnection);
                sqlconnection.Open();
                cmd.ExecuteNonQuery();
                sqlconnection.Close();
            }
            catch
            {
                return;
            }
        }

        private void xtraTabbedMdiManager1_SelectedPageChanged(object sender, EventArgs e)
        {
            showCharts(false);
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, MdiTabPageEventArgs e)
        {
            chartPaid.Series.RemoveAt(0);
            chartPaid.Series.RemoveAt(0);
            showCharts(true);
            fillCharts();
        }

    }
}


