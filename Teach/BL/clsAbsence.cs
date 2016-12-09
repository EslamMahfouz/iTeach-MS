using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;

namespace Teach.BL
{
    internal class clsAbsence
    {
        public DataTable getPaid(int idGroup, DateTime date)
        {
            var DAL = new DAL.DataAccessLayer();
            var dt = new DataTable();
            var param = new SqlParameter[2];

            param[0] = new SqlParameter("@idGroup", SqlDbType.Int);
            param[0].Value = idGroup;

            param[1] = new SqlParameter("@date", SqlDbType.Date);
            param[1].Value = date;

            dt = DAL.SelectData("getPaid", param);
            DAL.Close();
            return dt;
        }
        public DataTable getNotPaid(int idGroup, DateTime date)
        {
            var DAL = new DAL.DataAccessLayer();
            var dt = new DataTable();
            var param = new SqlParameter[2];

            param[0] = new SqlParameter("@idGroup", SqlDbType.Int);
            param[0].Value = idGroup;

            param[1] = new SqlParameter("@date", SqlDbType.Date);
            param[1].Value = date;

            dt = DAL.SelectData("getNotPaid", param);
            DAL.Close();
            return dt;
        }
        public DataTable repMonthly(int idStudent, DateTime date)
        {
            var DAL = new DAL.DataAccessLayer();
            var dt = new DataTable();
            var param = new SqlParameter[2];

            param[0] = new SqlParameter("@idStudent", SqlDbType.Int);
            param[0].Value = idStudent;

            param[1] = new SqlParameter("@date", SqlDbType.Date);
            param[1].Value = date;

            dt = DAL.SelectData("repMonthly", param);
            DAL.Close();
            return dt;
        }
    }
}
