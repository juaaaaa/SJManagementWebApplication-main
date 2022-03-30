using SJManagementWebApplication.Db;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace SJManagementWebApplication.Code
{
    public static class OverlayCheck
    {
        public static string[] CheckOverlay(StringBuilder sbQuery, Dictionary<string, string> dCondition, int returnNum)
        {
            SqlCommand scCmd = new SqlCommand();
            string[] sReturn = new string[returnNum+1];

            //조건을 갖췄을 때 중복검사 시행
            if (!(sbQuery == null) && !(dCondition == null))
            {
                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);

                String[,] sOverlap = DbTask.data_Load_Table(scCmd);
                sbQuery.Clear();
                dCondition.Clear();

                sReturn[0] = (sOverlap.Length/returnNum).ToString();//개수 카운트

                //반환 값 저장
                for (int i = 1; i <= returnNum; i++)
                {
                    sReturn[i] = Arraytostring(sOverlap, returnNum, i-1);
                }
            }
            return sReturn;
        }
        //string[,] -> string 타입변환
        static string Arraytostring(string[,] str, int returnNum, int arrNum)
        {
            int strLength = str.Length / returnNum;
            string strResult = "";
            for (int i = 0; i < strLength; i++)
            {
                Print.debugWrite("db불러오기" + i + "." + str[i, arrNum]);
                strResult += str[i, arrNum];
                if (i != strLength - 1)
                    strResult += ", ";
            }
            return strResult;
        }
    }
}