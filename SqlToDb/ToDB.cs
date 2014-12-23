using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SqlToDb
{
    class ToDB
    {
        public static SqlCommand Open()
        {
            System.Data.SqlClient.SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
            SqlCommand cmd = conn.CreateCommand();
            cmd.Connection=conn;
            cmd.CommandType = System.Data.CommandType.Text;
         
            return cmd;
        }

        public static int Excute(string sql)
        {
            SqlCommand cmd = Open();
            cmd.Connection.Open();
            cmd.CommandText = sql;
            cmd.Transaction = cmd.Connection.BeginTransaction();
            try
            {
                int i = cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                return i;
            }
            catch(Exception e)
            {
                cmd.Transaction.Rollback();
            }
            finally
            {
                cmd.Connection.Close();
                cmd.Dispose();
            }
            return 0;

        }

    }
}
