using SJManagementWebApplication.Db;
using System;
using System.Diagnostics;
using SJManagementWebApplication.Model;
using System.Text;
using System.Collections.Generic;
using SJManagementWebApplication.Code;
using System.Data.SqlClient;
using DevExpress.Web;
using System.Web.UI.WebControls;
using DevExpress.Utils;
using System.Web.UI;

namespace SJManagementWebApplication
{
    public partial class joinMembership : System.Web.UI.Page
    {
        #region Init
        //sql부
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> dCondition = new Dictionary<string, string>();
        SqlCommand scCmd = new SqlCommand();

        protected void Page_Init(object sender, EventArgs e)
        {
            popupEventInit();
        }
        protected void popupEventInit()
        {
            if (pcPopup.getTag() == NumDefine.SUCCESS)
            {
                pcPopup.OkBtnClick += new EventHandler(joinOk);
            }
        }
        #endregion

        #region join(check)
        protected void regBtn_Click(object sender, EventArgs e)//가입 체크 및 진행
        {
            Print.debugWrite(inUserId.Value.ToString() + inUserPw.Value.ToString());
            //텍스트 통해 id/pw/이름/닉네임 불러와 변수선언
            string sId = inUserId.Value.ToString();
            string sPw = inUserPw.Value.ToString();
            string sName = inUserName.Value.ToString();
            string sNickname = inUserNickname.Value.ToString();

            //빈 항목 검사
            if (!string.IsNullOrEmpty(sId) && !string.IsNullOrEmpty(sPw) && !string.IsNullOrEmpty(sName) && !string.IsNullOrEmpty(sNickname))
            {
                //중복 유저검사
                sbQuery.Append("exec SP_DuplicateUser ");
                sbQuery.Append("@id, @nickname");

                dCondition.Add("@id", sId);
                dCondition.Add("@nickname", sNickname);

                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);

                sbQuery.Clear();
                dCondition.Clear();

                byte sResult = byte.Parse(DbTask.data_Load_scalar(scCmd));


                //중복 x
                if (sResult == NumDefine.NOT_EXIST)
                {
                    //db 작업
                    sbQuery.Append("exec SP_insert_User ");
                    sbQuery.Append("@id, @pw, @name, @nickname");

                    dCondition.Add("@id", sId);
                    dCondition.Add("@pw", sPw);
                    dCondition.Add("@name", sName);
                    dCondition.Add("@nickname", sNickname);

                    if (Sj_StringBuilder.FilterCheck(dCondition))
                    {
                        Print.debugWrite("성공");
                        scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);
                        if (DbTask.data_Load_CUD(scCmd))
                        {
                            //새 유저 정보 추가
                            pcPopup.clearEvent(NumDefine.SUCCESS);
                            pcPopup.showPopup(true, false, "회원 등록이 완료되었습니다.");
                        }
                        else
                        {
                            pcPopup.clearEvent(NumDefine.FAIL);
                            pcPopup.showPopup(true, false, "회원가입 실패");
                        }
                        sbQuery.Clear();
                        dCondition.Clear();
                        scCmd.Dispose();
                    }
                }
                //중복된 경우
                else
                {
                    pcPopup.clearEvent(NumDefine.FAIL);

                    switch (sResult)
                    {
                        case NumDefine.EXIST_ID:
                            pcPopup.showPopup(true, false, "아이디 중복");
                            break;
                        case NumDefine.EXIST_NAME:
                            pcPopup.showPopup(true, false, "닉네임 중복");
                            break;
                        case NumDefine.EXIST_ID_AND_NAME:
                            pcPopup.showPopup(true, false, "아이디와 닉네임 중복");
                            break;
                    }
                }
            }
            else
            {
                pcPopup.clearEvent(NumDefine.FAIL);
                pcPopup.showPopup(true, false, "빈 항목을 채워주세요.");
            }
        }

        protected void joinOk(object sender, EventArgs e)//가입 후 로그인페이지로 이동
        {
            //Login.aspx로 이동
            Response.Redirect(string.Format("Login.aspx?"), false);
            Context.ApplicationInstance.CompleteRequest();
        }
        #endregion
    }
}