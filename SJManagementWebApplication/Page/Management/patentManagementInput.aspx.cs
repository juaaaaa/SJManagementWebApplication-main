using SJManagementWebApplication.Db;
using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web;
using System.Collections.Generic;
using SJManagementWebApplication.Code;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;

namespace SJManagementWebApplication
{
    public partial class patentManagementInput : System.Web.UI.Page
    {
        #region Init
        //sql부
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> dCondition = new Dictionary<string, string>();
        SqlCommand scCmd = new SqlCommand();
        SqlDataSource sdsDatasource = new SqlDataSource();

        //입력 폼 내용
        string sSeq, sName, sInventor, sNum, sAppnum, sFill, sReg, sDiv;

        //작업 순서 1 : 파일경로 설정, 현재 페이지 파일 경로 설정, 정렬 기준 컬럼 설정
        //파일 경로
        string sFilePath = "\\Content\\ipmanagepdf\\";
        //현재 페이지 파일 경로 설정
        string sPagePath = "Management/patentManagementInput";

        //작업 순서 2 : 팝업 이벤트 메서드 추가
        //팝업 버튼 클릭 이벤트
        protected void PopupEventInit()
        {
            switch (pcPopup.getTag())
            {
                case NumDefine.OVERLAY:
                    pcPopup.CancelBtnEvent += new EventHandler(OverlayCancel);
                    break;
                case NumDefine.DELETE:
                    pcPopup.OkBtnClick += new EventHandler(RowDeleteOk);
                    break;
                case NumDefine.FILE_DELETE:
                    pcPopup.OkBtnClick += new EventHandler(FilelistDeleteOk);
                    break;
            }
        }

        //버튼 세팅+그리드뷰 바인딩+팝업 이벤트
        protected void Page_Init(object sender, EventArgs e)
        {
            //작업 순서 3 : 초기 바인딩
            string parameter = Request["__EVENTARGUMENT"];
            if (parameter == "param1")
                Buttons(true, true);
            //바인딩
            gdPatentManageInput.AutoGenerateColumns = true;
            string sConnString = ConfigurationManager.ConnectionStrings["WEBAPPConnectionString"].ConnectionString;
            sdsDatasource.SelectCommand = "SELECT * FROM [ipManage] ORDER BY regDate DESC";
            sdsDatasource.ConnectionString = sConnString;
            gdPatentManageInput.DataSource = sdsDatasource;
            gdPatentManageInput.DataBind();

            PopupEventInit();
        }
        //로드 시 실행(포스트백마다)
        public void Page_Load(object sender, EventArgs e)
        {
            //페이지 카운트
            spPagecount.Text = "/ " + gdPatentManageInput.PageCount;

            //세션에 로그인 후 이동할 페이지 이름 저장
            Session["prevPage"] = sPagePath;

            //summary (행 개수)
            int i = gdPatentManageInput.VisibleRowCount;
            string pagerSmy = gdPatentManageInput.SummaryText.ToString();
            pagerSmy = "총 " + i + "건";
            lbFilterSummary.Text = pagerSmy.Trim();

            //상태 팝업 숨기기
            dAlert_delete.Style["visibility"] = "hidden";
            dAlert_save.Style["visibility"] = "hidden";
            dAlert.Style["visibility"] = "hidden";

            //파일리스트 호출
            tbFileslabel.Focus();
        }
        private void SqlClear()
        {
            sbQuery.Clear();
            dCondition.Clear();
            scCmd.Dispose();
        }
        #endregion

        #region Management(Create,Update,Delete)
        //pager 이동
        protected void BtnSubmitPager_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbPagerinput.Text))
                {
                    if (int.Parse(tbPagerinput.Text) <= 0)
                        gdPatentManageInput.PageIndex = 0;
                    else
                        gdPatentManageInput.PageIndex = int.Parse(tbPagerinput.Text) - 1;
                    spPagecount.Text = "/ " + gdPatentManageInput.PageCount;
                    tbPagerinput.Text = "";
                }
            }
            catch (OverflowException eee)
            {
                gdPatentManageInput.PageIndex = 0;
            }
        }
        //전체보기 버튼
        protected void BtnFilterSortClear(object sender, EventArgs e)
        {
            FilterSortClear();
        }
        protected void FilterSortClear()
        {
            gdPatentManageInput.ClearSort();
            gdPatentManageInput.FilterExpression = "";
            gdPatentManageInput.Settings.ShowGroupPanel = false;
            gdPatentManageInput.ResetToFirstPage();
            gdPatentManageInput.FocusedRowIndex = NumDefine.CLEAR;
        }
        //행 추가 세팅
        protected void AddNewRow(object sender, EventArgs e)
        {
            //포커싱 설정/입력폼 비우기
            if (gdPatentManageInput.FocusedRowIndex == NumDefine.NONE_FOCUSE)
            {
                gdPatentManageInput.AddNewRow();
            }
        }
        //신규버튼 클릭 시
        protected void BtnNew_Click(object sender, EventArgs e)
        {
            //신규 추가 팝업 on
            dAlert_delete.Style["visibility"] = "hidden";
            dAlert_save.Style["visibility"] = "hidden";
            dAlert.Style["visibility"] = "visible";

            //수정 모드일 때
            if (btnNew.ImageUrl.ToString().Equals(NumDefine.ADD_IMAGE))
            {
                //추가버튼 -> 취소버튼 (수정->추가)
                btnNew.ImageUrl = NumDefine.CANCEL_IMAGE;

                //포커싱 설정/입력폼 비우기
                gdPatentManageInput.FocusedRowIndex = NumDefine.NONE_FOCUSE;
                //버튼 세팅
                Buttons(false, true);
            }
            else//추가 모드일 때
            {
                //취소버튼 -> 취소버튼 (추가->수정)
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;
                gdPatentManageInput.FocusedRowIndex = 0;
            }
        }

        //삭제버튼 클릭 시
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            //삭제 알림 팝업 on
            dAlert_delete.Style["visibility"] = "visible";
            dAlert_save.Style["visibility"] = "hidden";
            dAlert.Style["visibility"] = "hidden";

            //sql 초기화
            SqlClear();

            //popup
            pcPopup.clearEvent(NumDefine.DELETE);
            pcPopup.setBtnText("삭제", "취소");
            pcPopup.showPopup(true, true, "삭제하시겠습니까?");

            //파일리스트 새로고침
            tbFileslabel.Focus();
        }
        //행 삭제: 확인
        protected void RowDeleteOk(object sender, EventArgs e)
        {
            //작업 순서 4 : 삭제 쿼리 작성
            //입력폼 내용 가져오기
            sSeq = tbSeqTxt.Text;
            try
            {
                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + sFilePath + tbSeqTxt.Text);
                diFolder.Delete(true);//폴더 파일 다 지움

                //삭제 쿼리
                sbQuery.Append("DELETE FROM ipManage ");
                sbQuery.Append("WHERE ip_seq = @seq");

                dCondition.Add("@seq", sSeq);

                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);//스칼라 반환

                if (!DbTask.data_Load_CUD(scCmd))
                {
                    //pcPopup
                    pcPopup.clearEvent(NumDefine.CLEAR);
                    pcPopup.showPopup(true, false, "삭제에 실패하였습니다.");
                    //파일리스트 새로고침
                    tbFileslabel.Focus();
                }

                if (gdPatentManageInput.VisibleRowCount == 1)//아무것도 없을 때
                {
                    //버튼세팅
                    Buttons(false, false);

                    //입력폼 클리어
                    gdPatentManageInput.FocusedRowIndex = NumDefine.NONE_FOCUSE;
                }
                else
                {
                    if (gdPatentManageInput.FocusedRowIndex == 0)//삭제 후 포커싱 처리
                        gdPatentManageInput.FocusedRowIndex = 0;
                    else
                        gdPatentManageInput.FocusedRowIndex = gdPatentManageInput.FocusedRowIndex - 1;

                }

                dAlert_save.Style["visibility"] = "visible";

                gdPatentManageInput.DataBind();
            }
            catch (NullReferenceException le)
            {
                tbFileslabel.Focus();
            }
            catch (DirectoryNotFoundException ae)
            {
                //pcPopup
                pcPopup.clearEvent(NumDefine.CLEAR);
                pcPopup.showPopup(true, false, "삭제에 실패하였습니다. (원인: DirectoryNotFoundException)");
                //파일리스트 새로고침
                tbFileslabel.Focus();
            }
        }

        //저장버튼 클릭 시
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //입력폼 부분 값 가져오기
            sSeq = tbSeqTxt.Text;
            sName = tbNameTxt.Text;
            sInventor = tbInventorTxt.Text;
            sNum = tbNumTxt.Text;
            sAppnum = tbAppnumTxt.Text;
            sFill = deFilldate.Text;
            sReg = deRegdate.Text;
            sDiv = cbDivTxt.Text;

            //입력 중 하나라도 미입력시 반환
            if (string.IsNullOrEmpty(sName) || string.IsNullOrEmpty(sInventor) || string.IsNullOrEmpty(sNum) || string.IsNullOrEmpty(sAppnum) || string.IsNullOrEmpty(sFill) || string.IsNullOrEmpty(sReg) || string.IsNullOrEmpty(sDiv))
            {
                //pcPopup
                pcPopup.clearEvent(NumDefine.CLEAR);
                pcPopup.showPopup(true, false, "필수입력 요소의 입력상태를 확인해주세요.");
                //파일리스트 새로고침+추가 모드 유지
                btnNew.ImageUrl = NumDefine.CANCEL_IMAGE;
                tbFileslabel.Focus();
            }
            else
            {
                //현재 상태(업데이트/추가)
                switch (btnNew.ImageUrl.ToString().Equals(NumDefine.ADD_IMAGE))//신규버튼 활성화 상태로 판단
                {
                    case true:
                        //작업 순서 5 : 업데이트 쿼리 작성
                        //업데이트 상태     
                        try
                        {
                            //업데이트 실행
                            sbQuery.Append("UPDATE ipManage SET ip_name = @name,ip_inventor = @inventor,ip_num = @num,ip_appnum = @appnum, ip_division = @div, ");
                            sbQuery.Append("ip_fillDate = @fill, ip_regDate = @reg, modUserId = @modid, modDate = GETDATE() ");
                            sbQuery.Append("WHERE ip_seq = @seq");

                            dCondition.Add("@seq", sSeq);
                            dCondition.Add("@name", sName);
                            dCondition.Add("@inventor", sInventor);
                            dCondition.Add("@num", sNum);
                            dCondition.Add("@appnum", sAppnum);
                            dCondition.Add("@div", sDiv);
                            dCondition.Add("@fill", sFill);
                            dCondition.Add("@reg", sReg);
                            dCondition.Add("@modid", "test"); //로그인 시스템 완성 후 수정

                            scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);//스칼라 반환

                            if (!DbTask.data_Load_CUD(scCmd))
                            {
                                //pcPopup
                                pcPopup.clearEvent(NumDefine.CLEAR);
                                pcPopup.showPopup(true, false, "저장에 실패하였습니다.");
                                //파일리스트 새로고침
                                tbFileslabel.Focus();
                            }
                            else
                            {
                                dAlert_save.Style["visibility"] = "visible";
                            }
                            //파일업로드
                            Upload();
                            //그리드뷰 업데이트
                            gdPatentManageInput.DataBind();
                            gdPatentManageInput.FocusedRowIndex = gdPatentManageInput.FocusedRowIndex;
                            //파일리스트 업데이트
                            tbFileslabel.Focus();

                        }
                        catch (NullReferenceException le)
                        {
                            tbFileslabel.Focus();
                        }
                        break;
                    case false:
                        //작업 순서 6 : 인서트 쿼리 작성
                        // 신규 버튼 클릭 후 상태
                        try
                        {
                            //postback으로 인해 미리 업로드, 취소 시 삭제
                            Upload();

                            //추가 쿼리
                            sbQuery.Append("INSERT INTO ipManage ");
                            sbQuery.Append("VALUES((select isnull(max(ip_seq),0) + 1 from ipManage),");
                            sbQuery.Append("@name, @inventor ,@num ,@appnum,@div,@fill,@reg,@regid,GETDATE(),null,null)");

                            dCondition.Add("@seq", sSeq);
                            dCondition.Add("@name", sName);
                            dCondition.Add("@inventor", sInventor);
                            dCondition.Add("@num", sNum);
                            dCondition.Add("@appnum", sAppnum);
                            dCondition.Add("@div", sDiv);
                            dCondition.Add("@fill", sFill);
                            dCondition.Add("@reg", sReg);
                            dCondition.Add("@regid", "test"); //로그인 시스템 완성 후 수정

                            scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);//스칼라 반환

                            if (!DbTask.data_Load_CUD(scCmd))
                            {
                                //pcPopup
                                pcPopup.clearEvent(NumDefine.CLEAR);
                                pcPopup.showPopup(true, false, "추가에 실패하였습니다.");
                                //파일리스트 새로고침
                                tbFileslabel.Focus();
                            }
                            SqlClear();

                            //총 개수 -1 에 포커스
                            gdPatentManageInput.DataBind(); //그리드뷰 업데이트

                            FilterSortClear();
                        }
                        catch (NullReferenceException le)
                        {
                            tbFileslabel.Focus();
                        }
                        break;
                }
                //sql 초기화
                SqlClear();

                //버튼상태 변경
                if (gdPatentManageInput.VisibleRowCount != 0)
                    Buttons(true, true);
                else
                    Buttons(false, true);
                //수정모드 변경
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;

            }
        }

        //버튼 활성화 설정
        protected void Buttons(bool bD, bool bS)
        {
            btnDelete.Enabled = bD;
            btnSave.Enabled = bS;
        }

        //입력폼 데이터 변화 시(중복검사,수정여부 검사)
        protected void TextChanged(object sender, EventArgs e)
        {
            //작업 순서 7 : 중복 체크 쿼리 작성
            if (btnNew.ImageUrl.ToString().Equals(NumDefine.CANCEL_IMAGE)) //신규 등록 상태
            {
                SqlClear();
                //중복검사 
                if (sender.Equals(tbNameTxt))
                {
                    sbQuery.Append("SELECT ip_appnum, ip_num FROM ipManage ");
                    sbQuery.Append("WHERE ip_name = @sText");

                    dCondition.Add("@sText", tbNameTxt.Text);

                    ShowOverlayPopup(OverlayCheck.CheckOverlay(sbQuery, dCondition, 2), "ip_name");
                }
                else if (sender.Equals(tbNumTxt))
                {
                    sbQuery.Append("SELECT ip_name, ip_appnum FROM ipManage ");
                    sbQuery.Append("WHERE ip_num = @sText");

                    dCondition.Add("@sText", tbNumTxt.Text);

                    ShowOverlayPopup(OverlayCheck.CheckOverlay(sbQuery, dCondition, 2), "ip_num");
                }
                else if (sender.Equals(tbAppnumTxt))
                {
                    sbQuery.Append("SELECT ip_name, ip_num FROM ipManage ");
                    sbQuery.Append("WHERE ip_appnum = @sText");

                    dCondition.Add("@sText", tbAppnumTxt.Text);

                    ShowOverlayPopup(OverlayCheck.CheckOverlay(sbQuery, dCondition, 2), "ip_appnum");
                }
            }
        }
        //중복 시 팝업 3종
        protected void ShowOverlayPopup(string[] sOverlayData, string sType)
        {
            //작업 순서 8 : 중복 체크 팝업 내용 작성
            try
            {
                if (int.Parse(sOverlayData[0]) > 0)
                {
                    //pcPopup
                    pcPopup.clearEvent(NumDefine.OVERLAY);
                    if (sType.Equals("ip_name"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + tbNameTxt.Text + "\n[출원번호] " + sOverlayData[1] + "\n[특허번호] " + sOverlayData[2]);
                    else if (sType.Equals("ip_appnum"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + sOverlayData[1] + "\n[출원번호] " + tbAppnumTxt.Text + "\n[특허번호] " + sOverlayData[2]);
                    else if (sType.Equals("ip_num"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + sOverlayData[1] + "\n[출원번호] " + sOverlayData[2] + "\n[특허번호] " + tbNumTxt.Text);
                    //파일리스트 새로고침
                    tbFileslabel.Focus();
                }
            }
            catch (FormatException ee) { }
            catch (NullReferenceException aea) { }
        }
        //중복검사에서 취소 클릭: 수정모드로 복귀
        protected void OverlayCancel(object sender, EventArgs e)
        {
            //오버레이 세팅
            dAlert_delete.Style["visibility"] = "hidden";
            dAlert_save.Style["visibility"] = "hidden";
            dAlert.Style["visibility"] = "visible";
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), null, "alertsetting(4)", true);

            //0번 행 포커스:수정모드
            gdPatentManageInput.FocusedRowIndex = 0;
            btnNew.ImageUrl = NumDefine.ADD_IMAGE;
        }
        #endregion

        #region filelist
        //파일리스트 갱신
        protected void BtnViewfile_Click(object sender, EventArgs e)
        {
            if (gdPatentManageInput.FocusedRowIndex >= 0 && gdPatentManageInput.VisibleRowCount != 0)
            {
                //작업 순서 9 : 파일 리스트 폴더 경로 설정(기준 seq 값 설정)
                //버튼세팅
                Buttons(true, true);
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;

                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + sFilePath + gdPatentManageInput.GetRowValues(gdPatentManageInput.FocusedRowIndex, "ip_seq").ToString()); //파일 폴더 경로
                //만약 폴더가 존재하지 않으면 생성
                if (diFolder.Exists == false)
                {
                    diFolder.Create();
                }
                var sbBuilder = new System.Text.StringBuilder();//문자열 더하기위한 StringBuilder 생성

                //파일리스트 갱신
                foreach (System.IO.FileInfo file in diFolder.GetFiles())
                {
                    //Print.debugWrite("파일명", file.Name);
                    sbBuilder.Append(file.Name + "/");
                }
                sbBuilder.Append(diFolder.GetFiles().Length);
                while (tbFileslabel.Text != sbBuilder.ToString())
                {
                    tbFileslabel.Text = sbBuilder.ToString();
                    tbFileslabel.Focus();
                }
            }
        }

        //파일 삭제 여부 팝업
        protected void FilelistDeletePopup(object sender, EventArgs e)
        {
            //pcPopup
            pcPopup.clearEvent(NumDefine.FILE_DELETE);
            pcPopup.showPopup(true, true, "삭제하시겠습니까?\n[" + tbFilename.Text.Substring(0, (tbFilename.Text.Length) - 5) + "]");

            //파일리스트 새로고침
            tbFileslabel.Focus();
        }
        //파일 삭제 팝업: 확인
        protected void FilelistDeleteOk(object sender, EventArgs e)
        {
            //파일 삭제
            File.Delete(Server.MapPath("~/") + sFilePath + tbSeqTxt.Text + "\\" + tbFilename.Text.Substring(0, (tbFilename.Text.Length) - 5));
            tbFilename.Text = "";
            var sbBuilder = new System.Text.StringBuilder();
            System.IO.DirectoryInfo diFolder = new System.IO.DirectoryInfo(@"" + Server.MapPath("~/") + sFilePath + tbSeqTxt.Text);
            foreach (System.IO.FileInfo file in diFolder.GetFiles())
            {
                sbBuilder.Append(file.Name + "/");
            }
            sbBuilder.Append(diFolder.GetFiles().Length);
            //파일리스트 갱신
            tbFileslabel.Text = sbBuilder.ToString();
            tbFileslabel.Focus();
        }

        //파일 업로드
        protected void Upload()
        {
            //작업 순서 10 : 파일 업로드 쿼리 작성
            //신규 추가일 때 seq 설정
            if (btnNew.ImageUrl.ToString().Equals(NumDefine.CANCEL_IMAGE))
            {
                sbQuery.Append("SELECT ISNULL(MAX(ip_seq),0)+1 ");
                sbQuery.Append("FROM ipManage");
                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);

                tbSeqTxt.Text = DbTask.data_Load_scalar(scCmd);

                SqlClear();
            }
            //파일 가지고 있으면 추가
            if (fuFileUpload1.HasFile)
            {
                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + sFilePath + tbSeqTxt.Text);
                //폴더 없을 시 추가
                if (diFolder.Exists == false)
                {
                    diFolder.Create();
                }
                foreach (HttpPostedFile uploadedFile in fuFileUpload1.PostedFiles)//파일 순차적으로 저장
                {
                    uploadedFile.SaveAs(Server.MapPath("~/") + sFilePath + tbSeqTxt.Text + "\\" + uploadedFile.FileName.Replace('+', ' '));
                }
                if (gdPatentManageInput.FocusedRowIndex >= 0)//업로드 후 갱신
                {
                    var sbBuilder = new System.Text.StringBuilder();
                    foreach (System.IO.FileInfo file in diFolder.GetFiles())
                    {
                        ////Print.debugWrite("파일명", file.Name);
                        sbBuilder.Append(file.Name + "/");
                    }
                    sbBuilder.Append(diFolder.GetFiles().Length);
                    //파일리스트 갱신
                    tbFileslabel.Text = sbBuilder.ToString();
                    tbFileslabel.Focus();
                }
            }
        }
        #endregion
    }
}