using SJManagementWebApplication.Code;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SJManagementWebApplication.Db
{
    public class DbTask
    {
        public static String[,] data_Load(string query)
        {
            string[,] error = new string[1, 1];
            error[0, 0] = "error";
            try
            {
                lock (DbConnect.DBConn)
                {
                    if (!DbConnect.IsDBConnected)
                    {
                        error[0, 0] = "isnotconnected";
                        return error;
                    }
                    else
                    {
                        //DB 연결이 되고 난 후..

                        SqlDataAdapter adapter = new SqlDataAdapter(query, DbConnect.DBConn);

                        DataTable dt = new DataTable();
                        //string[] data;

                        int columnCount, rowCount;

                        string[,] datatest;

                        Print.debugWrite("debug", dt.Columns.Count.ToString());

                        try
                        {
                            adapter.Fill(dt);

                            columnCount = dt.Columns.Count;
                            rowCount = dt.Rows.Count;

                            datatest = new string[rowCount, columnCount];

                            Print.debugWrite("카운트값", rowCount.ToString(), columnCount.ToString());

                            for (int i = 0; i < rowCount; i++)
                            {
                                for (int j = 0; j < columnCount; j++)
                                {
                                    datatest[i, j] = Convert.ToString(dt.Rows[i][j]);
                                }
                            }

                            DbConnect.Close();

                            return datatest;
                        }
                        catch (Exception e)
                        {
                            DbConnect.Close();
                            return error;
                        }
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                return error;
            }
        }
        //스칼라 반환 함수
        public static string data_Load_scalar(SqlCommand query)
        {
            //string[,] error = new string[1, 1];
            string error = "Error";
            try
            {
                lock (DbConnect.DBConn)
                {
                    if (!DbConnect.IsDBConnected)
                    {
                        error = "connect" + error;
                        return error;
                    }
                    else
                    {
                        //DB 연결이 되고 난 후..
                        query.Connection = DbConnect.DBConn;
                        SqlDataReader reader = query.ExecuteReader();
                        string value = "";
                        while (reader.Read())
                        {
                            value = reader.GetValue(0).ToString();

                            Print.debugWrite("reader", value);
                        }
                        reader.Close();
                        DbConnect.Close();
                        return value;
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                DbConnect.Close();
                error = ane + error;
                return error;
            }
        }
        //테이블 반환 함수
        public static string[,] data_Load_Table(SqlCommand query)
        {
            string[,] error = new string[1, 1];
            string[,] tableData;
            //string error = "Error";
            try
            {
                lock (DbConnect.DBConn)
                {
                    if (!DbConnect.IsDBConnected)
                    {
                        error[0, 0] = "connect" + error;
                        return error;
                    }
                    else
                    {
                        //DB 연결이 되고 난 후..
                        query.Connection = DbConnect.DBConn;
                        SqlDataReader reader = query.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        int rowCount = dt.Rows.Count;
                        int columnCount = dt.Columns.Count;

                        tableData = new string[rowCount, columnCount];

                        Print.debugWrite("카운트값", rowCount.ToString(), columnCount.ToString());


                        for (int i = 0; i < rowCount; i++)
                        {
                            for (int j = 0; j < columnCount; j++)
                            {
                                tableData[i, j] = Convert.ToString(dt.Rows[i][j]);
                                Print.debugWrite(tableData[i,j], i.ToString(), j.ToString());
                            }
                        }
                        DbConnect.Close();
                        reader.Close();
                        return tableData;
                    }

                    //return value;
                }
            }
            catch (ArgumentNullException ane)
            {
                DbConnect.Close();
                return error;
            }
        }
        //테이블 반환 함수
        public static DataTable data_Load_Table_dt(SqlCommand query)
        {
            DataTable dt = new DataTable();
            try
            {
                lock (DbConnect.DBConn)
                {
                    if (!DbConnect.IsDBConnected)
                    {
                        return dt;
                    }
                    else
                    {
                        //DB 연결이 되고 난 후..
                        query.Connection = DbConnect.DBConn;
                        SqlDataReader reader = query.ExecuteReader();
                        
                        dt.Load(reader);
                        Print.debugWrite("dt 데이터",dt.ToString());

                        return dt;
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                DbConnect.Close();
                return dt;
            }
            catch (InvalidOperationException ioe)
            {
                DbConnect.Close();
                return dt;
            }
        }

        //UPDATE INSERT DELETE 함수
        public static Boolean data_Load_CUD(SqlCommand query)
        {
            bool isitok = false;
            //string isitok = "error";
            try
            {
                lock (DbConnect.DBConn)
                {
                    if (!DbConnect.IsDBConnected)
                    {
                        return isitok;
                    }
                    else
                    {
                        //DB 연결이 되고 난 후..
                        query.Connection = DbConnect.DBConn;
                        try
                        {
                            SqlDataReader reader = query.ExecuteReader();
                            reader.Close();

                            isitok = true;
                        }
                        catch (SqlException exception)
                        {
                            Print.debugWrite("debug", exception.Errors[0].ToString());
                        }
                        DbConnect.Close();
                        return isitok;
                    }
                }
            }
            catch (ArgumentNullException ane)
            {
                DbConnect.Close();
                return isitok;
            }
        }
    }
}