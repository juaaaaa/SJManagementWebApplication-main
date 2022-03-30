using System.Diagnostics;
using System.Text;

namespace SJManagementWebApplication.Code
{
    public class Print
    {
        public static void debugWrite(string disting, string msg)
        {
            Debug.Write("\n" + disting + " : "+ msg + "\n");
        }
        public static void debugWrite(string msg)
        {
            Debug.Write("\n" + msg + "\n");
        }
        public static void debugWrite(string disting, params string[] msgs)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\n");
            sb.Append(disting);
            sb.Append(": ");
            for(int i = 0; i < msgs.Length; i++)
            {
                sb.Append(msgs[i]);
                sb.Append(" / ");
            }
            sb.Append("\n");

            Debug.Write(sb.ToString());
        }
    }
}