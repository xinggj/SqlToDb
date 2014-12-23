using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SqlToDb
{
    public partial class SqlToDb : Form
    {
        private string fileName = null;
        public SqlToDb()
        {
            InitializeComponent();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (oFD_Import.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = oFD_Import.FileName;
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                IList<SqlObj> list = XmlParse.parseSql(doc);
                string sql = "Select * from mob_sql";
                System.Data.SqlClient.SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["connection"]);
                SqlCommand cmd = conn.CreateCommand();
                cmd.Connection = conn;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection.Open();
                cmd.CommandText = sql;
                
                //cmd.Transaction = cmd.Connection.BeginTransaction();
                DataSet ds = new DataSet();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                
                sda.Fill(ds);
                DataTable dt = ds.Tables[0];
                DataRow dr = dt.Rows[0];
                dt.Rows.RemoveAt(0);
                for (int i = 0; i < 2000;i++ )
                {
                    DataRow dr1 = dt.NewRow();
                    dr1[3] = 1;
                    dt.Rows.Add(dr1);
                }
                DateTime now = DateTime.Now;
                dt.TableName = "MOB_SQL";
                UpdateByDataSetForSQL(ds, "MOB_SQL", null);
               

                //    cmd.CommandText = sql;
                //for (int i = 0; i < 2000; i++)
                //{
                //    cmd.ExecuteNonQuery();
                //}

                TimeSpan ts = DateTime.Now - now;
                decimal d = ts.Seconds;
                foreach (SqlObj sobj in list)
                {
                    ToDB.Excute(sobj.getSql());
                }
            }
            
        }

        private void keyConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public static bool UpdateByDataSetForSQL(DataSet ds, string strTblName, string strConnection)
        {
            strConnection = System.Configuration.ConfigurationSettings.AppSettings["connection"];
            SqlConnection conn = new SqlConnection(strConnection);

            SqlDataAdapter myAdapter = new SqlDataAdapter();
            SqlCommand myCommand = new SqlCommand(("select * from " + strTblName), (SqlConnection)conn);
            myAdapter.SelectCommand = myCommand;
            SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(myAdapter);
            try
            {
                //				lock(this)            //处理并发情况(分布式情况)
                //				{
                myAdapter.Update(ds, strTblName);
                //				}
            }
            catch (Exception err)
            {
                conn.Close();
                return false;
            }
            return true;    //数据集的行状态在更新后会都变为: UnChange,在这次更新后客户端要用返回的ds
        }

    }
}
