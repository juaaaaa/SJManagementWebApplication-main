using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using SJManagementWebApplication.Code;
using SJManagementWebApplication.Db;
using SJManagementWebApplication.Model;

namespace SJManagementWebApplication
{
    public partial class Login : System.Web.UI.Page
    {
        #region Init
        string sAuthorizedId = null;
        string sId = null;        

        public void Page_Load(object sender, EventArgs e)
        {

        }
        public void Page_Init(object sender, EventArgs e)
        {

        }
        #endregion

        #region login(check)
        protected void loginBtn_Click(object sender, EventArgs e)//로그인 진행(체크)
        {
            //db 작업
            StringBuilder sbQuery = new StringBuilder();
            sbQuery.Append("exec SP_Login_Check @id, @pw");

            var dCondition = new Dictionary<string, string>();
            dCondition.Add("@id", inTrialUserId.Value.ToString());
            dCondition.Add("@pw", inTrialUserPassword.Value.ToString());

            if (Sj_StringBuilder.FilterCheck(dCondition))
            {
                SqlCommand scCmd = new SqlCommand();
                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);//스칼라 반환

                sId = DbTask.data_Load_scalar(scCmd);

                try
                {
                    //로그인 확인
                    if (inTrialUserId.Value.ToString().Equals(sId))
                    {
                        sAuthorizedId = sId;

                        Print.debugWrite("login result", sId);

                        Print.debugWrite("test", "1", "2", "3");

                        saveSession(sAuthorizedId);
                    }
                    else//실패시
                    {
                        pcPopup.showPopup(true, false, "로그인에 실패하였습니다. 다시 시도해주세요.");

                        try
                        {
                            Response.Redirect(string.Format("Login.aspx?"), false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                        catch (Exception le)
                        {
                            pcPopup.showPopup(true, false, "에러 발생");
                        }
                    }
                }
                catch (Exception ea)
                {
                    pcPopup.showPopup(true, false, "로그인 과정 중 오류가 발생했습니다. 다시 시도해주세요.");
                }
            }
            else
            {
                Print.debugWrite("실패");
                pcPopup.showPopup(true, false, "로그인에 실패하였습니다. 다시 시도해주세요.");
            }


        }
        #endregion

        #region login-session
        protected void saveSession(string sId)//세션 저장 - AuthHelper 사용
        {
            //user 세션에 저장
            if (!AuthHelper.SignIn(sId))
            {
                //세션 저장 중 오류 발생
                pcPopup.showPopup(true, false, "로그인 과정 중 오류가 발생했습니다. 다시 시도해주세요.");
            }
            else
            {
                //AuthHelper에 세션이 저장 됐을 때
                if (AuthHelper.IsAuthenticated())
                {
                    //세션 정보 get(확인)
                    var uhUser = AuthHelper.GetLoggedInUserInfo();
                    var sUserName = string.Format("{0}", uhUser.UserId);
                    //메인페이지로 redirect(변경)
                    Response.Redirect(string.Format("../../Page/Management/patentManagement.aspx?"), false);
                    Context.ApplicationInstance.CompleteRequest();
                    Print.debugWrite("로그인 성공"+sUserName);
                }
            }
        }
        #endregion
    }
}