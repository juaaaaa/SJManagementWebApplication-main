using System;
using SJManagementWebApplication.Code;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using SJManagementWebApplication.Db;

namespace SJManagementWebApplication
{
    public partial class user : System.Web.UI.Page
    {
        #region Init
        //입력 폼
        static string sName, sphone, sid, sjob, sdepart; //1
        //sql부
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> dCondition = new Dictionary<string, string>();
        SqlCommand scCmd = new SqlCommand();

        //로드 시 실행
        protected void Page_Load(object sender, EventArgs e)
        {
            Print.debugWrite("sender:", sender.ToString(), e.ToString());
            //초기 화면
            if (!IsPostBack)
            {
                SetQuery_All();
                QueryFiltersInit();
            }
            //작동중 (Callback 되거나 검색 조건이 남아있을 경우
            if (IsCallback
                || (!string.IsNullOrEmpty(sName) || !string.IsNullOrEmpty(sphone) || !string.IsNullOrEmpty(sid)
                || !string.IsNullOrEmpty(sjob) || !string.IsNullOrEmpty(sdepart) ))
            {
                SetQuery_Search();
                GridPageInit();
            }
            else
                SetQuery_All();
            
            //summary 값 설정
            SmryManagement.Visible = true;
            SummaryInit();
            spPagecount.Text = "/ " + gdPatentManage.PageCount;
            Session["prevPage"] = "Management/patentManagement"; //세션에 로그인 후 이동할 페이지 이름 저장
        }
        //검색 결과 카운트
        protected void SummaryInit()
        {
            //GridView Row 개수 읽기
            int iRowCnt = gdPatentManage.VisibleRowCount;

            string pagerSmy = gdPatentManage.SummaryText.ToString();
            if (filteredTextMain.IsVisible())
                pagerSmy = "총 " + iRowCnt + "건의 검색결과가 있습니다."; // Line 206 >> SetPagerSummary 와 비교해서 수정 필요
            else
                pagerSmy = "총 " + iRowCnt + "건";

            //Pager Summary에 출력
            SmryManagement.Text = pagerSmy.Trim();
        }
        //sql 초기화 
        protected void SqlInit()
        {
            sbQuery.Clear();
            dCondition.Clear();
            scCmd.Dispose();
        }
        //GridView Page 초기화
        protected void GridPageInit()
        {
            gdPatentManage.ResetToFirstPage();
            gdPatentManage.FocusedRowIndex = NumDefine.CLEAR;
        }
        //검색 조건 초기화 (Popup에서 입력한 검색 조건)
        protected void QueryFiltersInit()
        {
            sName = string.Empty;
            sphone = string.Empty;
            sid = string.Empty;
            sjob = string.Empty;
            sdepart = string.Empty;

        }
        //PopupText Init
        protected void PopupTextInit()
        {
            ///작업 순서 3.
            ///에디트박스 초기화
            us_name.Text = string.Empty;
            us_phone.Text = string.Empty;
            //Null 오류 발생시 디버그 출력
            try
            {
                us_job.SelectedItem.Selected = false;
                us_depart.SelectedItem.Selected = false;
            }
            catch (NullReferenceException exception)
            {
                //Print.debugWrite("PopupInit Error" + ip_division.Caption.ToString() + " : ", exception.ToString());
            }
            us_id.Text = string.Empty;

        }
        #endregion

        #region Management(Filter & Pager)

        #region FIlter
        //전체보기 Button - Filter 초기화
        protected void OnFIlterClearBtnClick(object sender, EventArgs e)
        {
            //Toolbar 숨김 및 Filter 초기화
            gdPatentManage.ClearSort();
            gdPatentManage.FilterExpression = "";
            gdPatentManage.Settings.ShowGroupPanel = false;
            SetSummaryState(false, false, false);
            QueryFiltersInit();
            //GridView 새로고침
            SetQuery_All();
            
            //GridView Page & Pager 초기화
            GridPageInit();
            SummaryInit();
            spPagecount.Text = "/ " + gdPatentManage.PageCount;
        }

        //Toolbar Visible 상태 변경
        protected void SetSummaryState(bool bTextMainState, bool bTextState, bool bFilterLineState)
        {
            filteredTextMain.Visible = bTextMainState; //'검색조건'
            filteredText.Visible = bTextState;
            filterLine.Visible = bFilterLineState;
        }
        #endregion

        #region Popup
        //Popup 실행(헤더 검색) 버튼
        protected void OnSearchBtnClick(object source, EventArgs e)
        {
            PopupTextInit();
        }
        //Popup 내부 검색 버튼
        protected void OnPopupSearchBtnClick(object sender, EventArgs e)
        {
            //쿼리실행
            SetQuery_Search();
            //검색 summary
            SetPagerSummary(true);
            //GridView Page 초기화
            GridPageInit();

            //Pager 및 팝업 설정
            gdPatentManage.ClearSort();
            dLbSearchFilterText.Visible = true;
            spPagecount.Text = "/ " + gdPatentManage.PageCount;
        }

        #endregion

        #region Pager + Footer

        //pager 이동
        protected void OnSubmitPagerBtnClick(object sender, EventArgs e)
        {
            //범위 예외 처리
            try
            {
                if (!string.IsNullOrEmpty(tbPagerinput.Text))
                {
                    //0이하 값 입력시 오류 방지
                    if (int.Parse(tbPagerinput.Text) <= 0)
                        gdPatentManage.PageIndex = 0;
                    else
                        gdPatentManage.PageIndex = int.Parse(tbPagerinput.Text) - 1;
                    spPagecount.Text = "/ " + gdPatentManage.PageCount;
                    tbPagerinput.Text = "";
                }
            }
            catch (OverflowException eee)
            {
                gdPatentManage.PageIndex = 0;
            }
        }
        //Pager Summary 세팅(텍스트,설정값)
        protected void SetPagerSummary(bool summaryState)
        {
            int iRowCnt = gdPatentManage.VisibleRowCount;

            string pagerSmy = "총 " + iRowCnt + "건의 검색결과가 있습니다.";

            //Toolbar Visible 상태 변경
            SmryManagement.Text = pagerSmy.Trim();
            dLbSearchFilterText.Visible = summaryState;
            filteredTextMain.Visible = summaryState;
            filteredText.Visible = summaryState;
            filterLine.Visible = summaryState;
        }
        #endregion

        #region Query

        //작업 >> 1. 쿼리문 - 전체
        private void SetQuery_All()
        {
            //Query
            SqlDataSource sdsDatasource = new SqlDataSource();

            //작업순서 1번.  쿼리문(전체) 수정
            gdPatentManage.AutoGenerateColumns = true;
            string sConnString = ConfigurationManager.ConnectionStrings["STDataConnectionString"].ConnectionString;
            sdsDatasource.SelectCommand = "SELECT * FROM [US001] ORDER BY regDate DESC";
            sdsDatasource.ConnectionString = sConnString;
            gdPatentManage.DataSource = sdsDatasource;
            gdPatentManage.DataBind();

            //초기화
            GridPageInit();
            QueryFiltersInit();
            dCondition.Clear();
            SqlInit();

        }
        //작업 >> 2. 쿼리문 - 검색 조건 적용
        private void SetQuery_Search()
        {
            //Query
            //작업순서 2번.   쿼리문(조건) 수정.
            sbQuery.Append("SELECT * FROM US001 ");

            filteredText.Text = string.Empty;
            string sQueryText = string.Empty;
            string sFilterText = string.Empty;

            //직원명
            sName = us_name.Text.ToString().Trim();
            if (!string.IsNullOrEmpty(sName))
            {
                sQueryText += "us_name LIKE @name AND ";
                sFilterText += "직원명:" + sName;

                dCondition.Add("@name", "%" + sName + "%");
            }
            //연락처
            sphone = us_phone.Text.ToString().Trim();
            if (!string.IsNullOrEmpty(sphone))
            {
                if (!string.IsNullOrEmpty(sFilterText))
                    sFilterText += ", ";

                sQueryText += "us_phone LIKE @phone AND ";
                sFilterText += "연락처:" + sphone;

                dCondition.Add("@inventor", "%" + sphone + "%");
            }
            //ID
            sid = us_id.Text.ToString().Trim();
            if (!string.IsNullOrEmpty(sid))
            {
                if (!string.IsNullOrEmpty(sFilterText))
                    sFilterText += ", ";

                sQueryText += "us_id LIKE @id AND ";
                sFilterText += "ID:" + sid;

                dCondition.Add("@division", "%" + sid + "%");
            }

            //직급
            sjob = us_job.Text.ToString().Trim();
            if (!string.IsNullOrEmpty(sjob))
            {
                if (!string.IsNullOrEmpty(sFilterText))
                    sFilterText += ", ";

                sQueryText += "us_job LIKE @job AND ";
                sFilterText += "직급:" + sjob;

                dCondition.Add("@job", "%" + sjob + "%");
                ;
            }
            //소속
            sdepart = us_depart.Text.ToString().Trim();
            if (!string.IsNullOrEmpty(sdepart))
            {
                if (!string.IsNullOrEmpty(sFilterText))
                    sFilterText += ", ";

                sQueryText += "us_depart LIKE @appnum AND";
                sFilterText += "소속:" + sdepart;

                dCondition.Add("@depart", "%" + sdepart + "%");
            }

            //모두 만족하는 조건 설정
            sQueryText += " us_seq > 0 ";
            sbQuery.Append(" WHERE " + sQueryText);
            filteredText.Text = sFilterText;
            filteredText.ToolTip = sFilterText;

            scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);
            gdPatentManage.DataSource = DbTask.data_Load_Table_dt(scCmd);
            gdPatentManage.DataBind(); //그리드뷰 업데이트

            //sql 초기화
            SqlInit();
        }
        #endregion

        #endregion
    }
}