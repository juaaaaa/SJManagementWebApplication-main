using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SJManagementWebApplication.Db
{
    public class DbConnect
    {
        private static SqlConnection conn = null;
        public static string DBConnString { get; private set; }
        public static bool bDBConnCheck = false;
        private static int errorBoxCount = 0;

        public static string uid = "sa";
        public static string server = "175.123.253.185";
        public static string password = "sj1398134490";
        public static string database = "STData"; //작업 순서 1: DB 정보 변경

        public DbConnect() { }

        public static SqlConnection DBConn
        {
            get
            {
                if (!ConnectToDB())
                {
                    return null;
                }
                return conn;
            }
        }
        public static bool ConnectToDB()
        {
            if (conn == null)
            {
                string DBConnString = "SERVER=" + server + ";DATABASE=" + database + ";UID=" + uid + ";PASSWORD=" + password + ";";

                conn = new SqlConnection(DBConnString);
            }
            try
            {
                if (!IsDBConnected)
                {
                    conn.Open();

                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        bDBConnCheck = true;
                    }
                    else
                    {
                        bDBConnCheck = false;
                    }
                }
            }
            catch (SqlException e)
            {
                errorBoxCount++;
                if (errorBoxCount == 1)
                {

                }
                return false;
            }
            return true;
        }
        public static bool IsDBConnected
        {
            get
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    return false;
                }
                return true;
            }
        }
        public static void Close()
        {
            if (IsDBConnected)
            {
                DBConn.Close();
            }
        }
    }
}