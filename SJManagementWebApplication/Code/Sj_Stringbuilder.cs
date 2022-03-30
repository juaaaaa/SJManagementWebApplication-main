using System;
using System.Text;
using System.Data.SqlClient;
using SJManagementWebApplication.Db;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web.UI;

namespace SJManagementWebApplication.Code
{
    public class Sj_StringBuilder
    {
        public static SqlCommand conditionFilter(StringBuilder sb, Dictionary<string, string> condition)
        {
            SqlCommand sqSqlCommand = new SqlCommand(sb.ToString());
            foreach (string sKey in condition.Keys)
            {
                string sValue = condition[sKey];
                sqSqlCommand.Parameters.AddWithValue(sKey, sValue);
                Print.debugWrite(sKey, sValue);
            }
            return sqSqlCommand;
            //Debug.Write("table returns: "+DbTask.data_Load_Table(sqlCommand));
        }

        public static void conditionFilter(StringBuilder sb, Dictionary<string, int> condition)
        {

        }

        public static Boolean FilterCheck(Dictionary<string, string> condition)
        {
            string[] filters = { " ", " or ", " and ", "concat", "CONCAT", "substr", "SUBSTR", "ord", "ORD", "'", "\"", "(", ")", "--", "%", "CREATE", "create", "DROP", "drop", "ALTER", "alter", "WHERE", "where", "INSERT", "insert", "SELECT", "select", "UPDATE" };
            foreach (string sKey in condition.Keys)
            {
                string sValue = condition[sKey];
                foreach (String i in filters)
                {
                    if (sValue.Contains(i))
                    {
                        Print.debugWrite("포함", i);
                        return false;
                    }
                    else
                    {
                        Print.debugWrite("미포함", i);
                    }
                }
            }
            return true;
        }
    }
}