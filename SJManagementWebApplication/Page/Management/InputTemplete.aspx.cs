using System;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using SJManagementWebApplication.Code;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace SJManagementWebApplication
{
    //템플릿: 클래스 네임 수정
    public partial class InputTemplete : System.Web.UI.Page
    {
        #region Init
        //sql부
        StringBuilder sbQuery = new StringBuilder();
        Dictionary<string, string> dCondition = new Dictionary<string, string>();
        SqlCommand scCmd = new SqlCommand();
        SqlDataSource sdsDatasource = new SqlDataSource();

        //입력폼 변경 여부
        public static String[] textArray = new string[8];
        public static bool isChanged = false;
        public static int iFocusedNum = 0;
        //입력 폼 내용
        string sSeq, sName, sInventor, sNum, sAppnum, sFill, sReg, sDiv;

        //팝업 버튼 클릭 이벤트
        protected void popupEventInit()
        {
            switch (pcPopup.getTag())
            {
                case NumDefine.OVERLAY:
                    pcPopup.CancelBtnEvent += new EventHandler(overlayCancel);
                    break;
                case NumDefine.DELETE:
                    pcPopup.OkBtnClick += new EventHandler(rowDeleteOk);
                    break;
                case NumDefine.FILE_DELETE:
                    pcPopup.OkBtnClick += new EventHandler(filelistDeleteOk);
                    break;
            }
        }

        //버튼 세팅+그리드뷰 바인딩+팝업 이벤트
        protected void Page_Init(object sender, EventArgs e)
        {
            string parameter = Request["__EVENTARGUMENT"];
            if (parameter == "param1")
                buttons(true, true, true);

            //템플릿: 그리드뷰 바인딩
            /*gdPatentManageInput.AutoGenerateColumns = true;
            sdsDatasource.SelectCommand = "SELECT * FROM [ipManage]";
            string sConnString = ConfigurationManager.ConnectionStrings["WEBAPPConnectionString"].ConnectionString;
            sdsDatasource.ConnectionString = sConnString;
            gdPatentManageInput.DataSource = sdsDatasource;
            gdPatentManageInput.DataBind();
            */
            popupEventInit();
        }
        //로드 시 실행(포스트백마다)
        public void Page_Load(object sender, EventArgs e)
        {
            //페이지 카운트
            spPagecount.Text = "/ " + gdPatentManageInput.PageCount;

            //세션에 로그인 후 이동할 페이지 이름 저장
            Session["prevPage"] = "Management/patentManagementInput";

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
            if (!IsPostBack)//포스트백 검사
            {
                //정렬
                //gdPatentManageInput.SortBy(gdPatentManageInput.Columns[9], ColumnSortOrder.Descending);
            }
        }
        //로드 해제 시
        protected void Page_Unload(object sender, EventArgs e)
        {
            //Print.debugWrite("page unload");
        }
        #endregion

        #region Management(Create,Update,Delete)
        //pager 이동
        protected void submitPager(object sender, EventArgs e)
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
        protected void FilterSortClear(object sender, EventArgs e)
        {
            FilterSortClear();
        }
        protected void FilterSortClear()
        {
            gdPatentManageInput.ClearSort();
            //재정렬
            //gdPatentManageInput.SortBy(gdPatentManageInput.Columns[9], ColumnSortOrder.Descending);
            gdPatentManageInput.FilterExpression = "";
            gdPatentManageInput.Settings.ShowGroupPanel = false;
            gdPatentManageInput.ResetToFirstPage();
            gdPatentManageInput.FocusedRowIndex = NumDefine.CLEAR;
        }
        //행 추가 세팅
        protected void addNewRow(object sender, EventArgs e)
        {
            //포커싱 설정/입력폼 비우기
            if (gdPatentManageInput.FocusedRowIndex == -1)
            {
                gdPatentManageInput.AddNewRow();
            }
        }
        //신규버튼 클릭 시
        protected void btnNew_Click(object sender, EventArgs e)
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
                gdPatentManageInput.FocusedRowIndex = -1;
                //버튼 세팅
                buttons(true, false, true);
            }
            else//추가 모드일 때
            {
                //취소버튼 -> 취소버튼 (추가->수정)
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;
                gdPatentManageInput.FocusedRowIndex = 0;
            }
        }

        //삭제버튼 클릭 시
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            //삭제 알림 팝업 on
            dAlert_delete.Style["visibility"] = "visible";
            dAlert_save.Style["visibility"] = "hidden";
            dAlert.Style["visibility"] = "hidden";

            //sql 초기화
            sbQuery.Clear();
            dCondition.Clear();
            scCmd.Dispose();

            //popup
            pcPopup.clearEvent(NumDefine.DELETE);
            pcPopup.setBtnText("삭제", "취소");
            pcPopup.showPopup(true, true, "삭제하시겠습니까?");

            //파일리스트 새로고침
            tbFileslabel.Focus();
        }
        //행 삭제: 확인
        protected void rowDeleteOk(object sender, EventArgs e)
        {/*
            //입력폼 내용 가져오기
            sSeq = tbSeqTxt.Text;
            try
            {
                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + tbSeqTxt.Text);
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
                    buttons(true, false, false);

                    //입력폼 클리어
                    gdPatentManageInput.FocusedRowIndex = -1;
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
            */
        }

        //저장버튼 클릭 시
        protected void btnSave_Click(object sender, EventArgs e)
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
                    case true: //업데이트 상태
                        if (isChanged || fuFileUpload1.HasFile)//바뀌거나 파일이 업로드되었을 때
                        {
                            isChanged = false;
                            try
                            {
                                /*
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

                                
                                 * scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);//스칼라 반환

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
                                    textArray[0] = sSeq;
                                    textArray[1] = sName;
                                    textArray[2] = sDiv;
                                    textArray[3] = sName;
                                    textArray[4] = sAppnum;
                                    textArray[5] = sInventor;
                                    textArray[6] = sFill;
                                    textArray[7] = sReg;
                                }
                                */
                                //파일업로드
                                upload();
                                //그리드뷰 업데이트
                                gdPatentManageInput.DataBind();
                                //파일리스트 업데이트
                                tbFileslabel.Focus();
                            }
                            catch (NullReferenceException le)
                            {
                                tbFileslabel.Focus();
                            }
                        }

                        else
                        {

                            dAlert_save.Style["visibility"] = "hidden";
                            //pcPopup
                            pcPopup.clearEvent(NumDefine.CLEAR);
                            pcPopup.showPopup(true, false, "수정사항이 없습니다.");
                        }
                        break;
                    case false: // 신규 버튼 클릭 후 상태
                        try
                        {

                            //postback으로 인해 미리 업로드, 취소 시 삭제
                            upload();

                            //메시지박스

                            //추가 쿼리
                            /*
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
                            */
                            sbQuery.Clear();
                            dCondition.Clear();


                            //총 개수 -1 에 포커스
                            gdPatentManageInput.DataBind(); //그리드뷰 업데이트
                            gdPatentManageInput.FocusedRowIndex = 0; //포커싱 업데이트
                        }
                        catch (NullReferenceException le)
                        {
                            tbFileslabel.Focus();
                        }
                        break;
                }
                //sql 초기화
                sbQuery.Clear();
                dCondition.Clear();
                scCmd.Dispose();

                //버튼상태 변경
                if (gdPatentManageInput.VisibleRowCount != 0)
                    buttons(true, true, true);
                else
                    buttons(true, false, true);
                //수정모드 변경
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;

                FilterSortClear();
            }
        }

        //버튼 활성화 설정
        protected void buttons(bool bA, bool bB, bool bC)
        {
            //추가
            if (bA)
                btnNew.Enabled = true;
            else
                btnNew.Enabled = false;
            //삭제
            if (bB)
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
            //저장
            if (bC)
                btnSave.Enabled = true;
            else
                btnSave.Enabled = false;
        }

        //입력 폼 데이터 세팅
        protected string[] arraySet(object sender)
        {
            string[] arr = new string[2];

            if (sender.Equals(tbNameTxt))
            {
                arr[0] = tbNameTxt.Text;
                arr[1] = 1.ToString();
            }
            else if (sender.Equals(cbDivTxt))
            {
                arr[0] = cbDivTxt.Text;
                arr[1] = 2.ToString();
            }
            else if (sender.Equals(tbNumTxt))
            {
                arr[0] = tbNumTxt.Text;
                arr[1] = 3.ToString();
            }
            else if (sender.Equals(tbAppnumTxt))
            {
                arr[0] = tbNumTxt.Text;
                arr[1] = 4.ToString();
            }
            else if (sender.Equals(tbInventorTxt))
            {
                arr[0] = tbInventorTxt.Text;
                arr[1] = 5.ToString();
            }
            else if (sender.Equals(deFilldate))
            {
                arr[0] = deFilldate.Text;
                arr[1] = 6.ToString();
            }
            else if (sender.Equals(deRegdate))
            {
                arr[0] = deRegdate.Text;
                arr[1] = 7.ToString();
            }
            return arr;
        }

        protected void cbDivTxt_ItemInserting(object sender, DevExpress.Web.Data.ASPxDataInsertingEventArgs e)
        {
            Print.debugWrite("인서팅!");
        }

        protected void gdPatentManageInput_CustomUnboundColumnData(object sender, DevExpress.Web.ASPxGridViewColumnDataEventArgs e)
        {
            if (e.Column.FieldName == "번호")
            {
                e.Value = string.Format("{0}", e.ListSourceRowIndex + 1);
            }
        }
        private void sqlClear()
        {
            sbQuery.Clear();
            dCondition.Clear();
            scCmd.Dispose();
        }
        //입력폼 데이터 변화 시(중복검사,수정여부 검사)
        protected void textChanged(object sender, EventArgs e)
        {
            bool addFlag = true;
            if (btnNew.ImageUrl.ToString().Equals(NumDefine.ADD_IMAGE))
            {
                addFlag = false; //수정 상태일 경우
            }
            int presentFocusedNum = gdPatentManageInput.FocusedRowIndex;
            if (!presentFocusedNum.Equals(iFocusedNum)) //포커스가 달라지면
            {
                for (int i = 0; i < textArray.Length; i++)
                {
                    textArray[i] = null; //초기화
                }
                isChanged = false;
                iFocusedNum = presentFocusedNum;
            }

            //저장 및 변경여부 검사 + 중복검사

            if (!addFlag) //수정 상태
            {
                string[] arr = new string[2];
                arr = arraySet(sender);

                if (!string.IsNullOrEmpty(textArray[int.Parse(arr[1])]))
                {
                    if (!textArray[int.Parse(arr[1])].Equals(arr[0]))
                    {
                        isChanged = true;
                    }
                }
                else
                {
                    Print.debugWrite("Txt 값", arr[0]);
                    textArray[int.Parse(arr[1])] = arr[0];
                }
            }

            else if (addFlag) //신규 등록 상태
            {
                sqlClear();
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
        protected void ShowOverlayPopup(string[] sOverlayData,string sType)
        {
            try
            {
                if (int.Parse(sOverlayData[0]) > 0)
                {
                    //pcPopup
                    pcPopup.clearEvent(NumDefine.OVERLAY);
                    if(sType.Equals("ip_name"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + tbNameTxt.Text + "\n[출원번호] " + sOverlayData[1] + "\n[특허번호] " + sOverlayData[2]);
                    else if(sType.Equals("ip_appnum"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + sOverlayData[1] + "\n[출원번호] " + tbAppnumTxt.Text + "\n[특허번호] " + sOverlayData[2]);
                    else if(sType.Equals("ip_num"))
                        pcPopup.showPopup(true, true, "다음의 항목이 중복 검색되었습니다. 계속 진행하시겠습니까?\n\n[특허명] " + sOverlayData[1] + "\n[출원번호] " + sOverlayData[2] + "\n[특허번호] " + tbNumTxt.Text);
                    //파일리스트 새로고침
                    tbFileslabel.Focus();
                }
            }
            catch (FormatException ee){ }
            catch (NullReferenceException aea) { }
        }    
        //중복검사에서 취소 클릭: 수정모드로 복귀
        protected void overlayCancel(object sender, EventArgs e)
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
        protected void btnViewfile_Click(object sender, EventArgs e)
        {
            if (gdPatentManageInput.FocusedRowIndex >= 0 && gdPatentManageInput.VisibleRowCount != 0)
            {
                //버튼세팅
                buttons(true, true, true);
                btnNew.ImageUrl = NumDefine.ADD_IMAGE;

                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + gdPatentManageInput.GetRowValues(gdPatentManageInput.FocusedRowIndex, "ip_seq").ToString()); //파일 폴더 경로
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
        protected void filelistDeletePopup(object sender, EventArgs e)
        {
            //pcPopup
            pcPopup.clearEvent(NumDefine.FILE_DELETE);
            pcPopup.showPopup(true, true, "삭제하시겠습니까?\n[" + tbFilename.Text.Substring(0, (tbFilename.Text.Length) - 5) + "]");

            //파일리스트 새로고침
            tbFileslabel.Focus();
        }
        //파일 삭제 팝업: 확인
        protected void filelistDeleteOk(object sender, EventArgs e)
        {
            //파일 삭제
            File.Delete(Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + tbSeqTxt.Text + "\\" + tbFilename.Text.Substring(0, (tbFilename.Text.Length) - 5));
            tbFilename.Text = "";
            var sbBuilder = new System.Text.StringBuilder();
            System.IO.DirectoryInfo diFolder = new System.IO.DirectoryInfo(@"" + Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + tbSeqTxt.Text);
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
        protected void upload()
        {/*
            //신규 추가일 때 seq 설정
            if (btnNew.ImageUrl.ToString().Equals(NumDefine.CANCEL_IMAGE))
            {
                sbQuery.Append("SELECT ISNULL(MAX(ip_seq),0)+1 ");
                sbQuery.Append("FROM ipManage");
                scCmd = Sj_StringBuilder.conditionFilter(sbQuery, dCondition);

                tbSeqTxt.Text = DbTask.data_Load_scalar(scCmd);
                sbQuery.Clear();
                dCondition.Clear();

                scCmd.Dispose();
            }
            //파일 가지고 있으면 추가
            if (fuFileUpload1.HasFile)
            {
                DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + tbSeqTxt.Text);
                //폴더 없을 시 추가
                if (diFolder.Exists == false)
                {
                    diFolder.Create();
                }
                foreach (HttpPostedFile uploadedFile in fuFileUpload1.PostedFiles)//파일 순차적으로 저장
                {
                    uploadedFile.SaveAs(Server.MapPath("~/") + "\\Content\\ipmanagepdf\\" + tbSeqTxt.Text + "\\" + uploadedFile.FileName.Replace('+', ' '));
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
            */
                        }
        #endregion
    }
}