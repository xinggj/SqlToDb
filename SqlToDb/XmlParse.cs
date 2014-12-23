using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SqlToDb
{
    class XmlParse
    {
        public static IList<SqlObj> parseSql(XmlDocument doc)
        {
            List<SqlObj> list = new List<SqlObj>();

            XmlNodeList xnl = doc.GetElementsByTagName("sqls")[0].ChildNodes;
            if (xnl.Count > 0)
            {
                string comment = null;
                foreach (XmlNode item in xnl)
                {
                    if (item.NodeType == XmlNodeType.Comment)
                    {
                        if (null == comment)
                            comment = item.Value;
                        continue;
                    }

                    list.Add(new SqlObj() { dbkey = item.Attributes["dbkey"].Value, key = item.Attributes["key"].Value, value = item.Attributes["value"].Value.Replace("&gt;", ">").Replace("&lt;", "<"), comment=comment });
                    comment = null;
                }
            }

            return list;
        }

        public IList<MsgObj> parseMsg(XmlDocument doc)
        {
            List<MsgObj> list = new List<MsgObj>();

            return list;
        }
    }
}
