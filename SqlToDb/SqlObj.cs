using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlToDb
{
    class SqlObj
    {
        private static int INDEX=100;
        public string dbkey;
        public string key;
        public string value;
        public string comment;
        public string module;
        public string sys="iENR";
       

        public string getSql()
        {
            string val = value.Replace("'", "''").Replace("&gt;", ">").Replace("&lt;", "<");
            string val1 = "";
            for (int i = 0; i < 256; i++)
            {
                val1 += "1";
            }
           
            // DES.KeyArray= UTF8Encoding.UTF8.GetBytes("12345678901234567890123456789012");
            // val = DES.Encrypt(val);

            
            string module = key.Split(new string[]{"_"}, StringSplitOptions.None)[0];
            comment = null!=comment?comment.Replace("'", "''").Replace("&gt;", ">").Replace("&lt;", "<"):null;
            return "INSERT INTO MOB_SQL(SQL_KEY,SQL_TRANSATION,SQL_TYPE,SQL_MODULE,[SQL],SQL_MARK,SQL_SYS) VALUES('" + key+"','" + dbkey + "'" + ",2" + ",'"+module+"','" + val + "','" + comment + "','"+sys+"')";
        }
    }
}
