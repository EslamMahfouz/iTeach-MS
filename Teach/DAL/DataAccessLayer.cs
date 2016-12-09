using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using Teach.Properties;

namespace Teach.DAL
{
    internal class DataAccessLayer
    {
        private SqlConnection sqlconnection;
        public DataAccessLayer()
        {
            sqlconnection = new SqlConnection(Settings.Default.TeachConnectionString);
        }

        public void Open()
        {
            if (sqlconnection.State != ConnectionState.Open)
            {
                sqlconnection.Open();
            }
        }
        public void Close()
        {
            if (sqlconnection.State == ConnectionState.Open)
            {
                sqlconnection.Close();
            }
        }

        public DataTable SelectData(string stored_procedure, SqlParameter[] param)
        {
            var sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;
            if (param != null)
            {
                for (var i = 0; i < param.Length; i++)
                {
                    sqlcmd.Parameters.Add(param[i]);
                }
            }
            var da = new SqlDataAdapter(sqlcmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public void ExcuteCommand(string stored_procedure, SqlParameter[] param)
        {
            var sqlcmd = new SqlCommand();
            sqlcmd.CommandType = CommandType.StoredProcedure;
            sqlcmd.CommandText = stored_procedure;
            sqlcmd.Connection = sqlconnection;

            if (param != null)
            {
                sqlcmd.Parameters.AddRange(param);
            }
            sqlcmd.ExecuteNonQuery();
        }

        public static bool TestConnection()
        {
            var _return = true;

            using (var con = new SqlConnection(Settings.Default.TeachConnectionString))
            {
                try
                {
                    con.Open();
                }
                catch (Exception)
                {
                    _return = false;
                }
            }
            return _return;
        }
    }
}
