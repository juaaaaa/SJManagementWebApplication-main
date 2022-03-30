## SJWebApplication Guide >> ( '작업 순서' 검색 )

### @ .aspx.cs 파일보다 .aspx 파일 먼저 작업 할 것 @

### 1. Web.config ( connectionStrings )
	1-1. name = WEBAPPConnectionString

		* connectionStrings name 변경 할 것 ( WEBAPPConnectionString >> [NewDataBase]ConnectionString )
		* 사용하는 db명으로 변경 ( WEBAPP >> [NewDataBase] )
		* db연결시 사용하는 ID, Password 변경 >> 해당 안되면 수정X
		* ip 주소 변경 >> db 변경 시에만 수정


### 2. Root.master
	2-1. Root.master.cs

		(0) Page_Load(object sender, EventArgs e)
			* 프로그램 이름 변경
      
		(1) CreateMenuItem(string selectedMenu) >> 메뉴 초기화 작업
			* 쿼리문 master menu >> Table명 변경 및 조건 수정 및 필드명 수정
      
		(2) getDetailMenu()
			* 쿼리문 detail menu >> Table명 수정

### 3. Script.js
	3-1. gridview

		(0) $(document).ready
			* 경로 설정 >> NewNameInput.aspx 로 수정

		(1) OnGetRowValues_patent(sValues)
			* input.aspx 에서 설정한 InstanceName 으로 수정 (혹은 function 추가 할 것)

		(2) Manage_FocusedRowChanged(s, e)
			* 변경된 db 컬럼명(ex >> 'ip_name')으로 수정 (혹은 function 추가 할 것)
	

### 4. patentManagement (조회 페이지)
	4-1. patentManagement.aspx

		(1) ASPxPopupControl
			* Popup 조건 변경 시 >> Item 개수 수정 및 Caption, NullText 등 조건 수정
      
		(2) ASPxGridView
			* GridView Column 변경 시 >> Column 개수 수정 및 조건 수정

	4-2 patentManagement.aspx.cs
  
		(0) QueryFiltersInit()
		(1) PopupTextInit()
			* 바뀐 Popup Item ID에 맞춰 개수와 변수명을 수정

		(2) SetQuery_All()
			* 쿼리문 - 전체 조회 >> Web.config 에서 변경 한 connectionStrings 로 수정
			* 조회 쿼리문 수정
      
		(3) SetQuery_Search()
			* 쿼리문 - 검색 조회 >> 검색 쿼리문 수정
			* 검색 쿼리문 수정 >> NewTable 에 맞는 필드로 메서드 수정


### 5. patentManagementInput (수정 페이지)

	5-1. patentManagementInput .aspx

  		(1) UpdatePanel
			* Item 개수 변경 (NewTable Column 개수) 및 값 변경  (각 Item의 ID, Text 등)
			* .aspx 상단에 경로 수정해서 추가할 것 >> <%@ Register TagPrefix="UC" TagName="Popup" Src="~/Code/PopupWebUserControl.ascx" %>
			* 3-1 (gridview) 참고

		(2) ASPxGridView
			* GridView Column 변경 시 >> Column 개수 수정 및 조건 수정

	5-2. patentManagementInput .aspx.cs
  
		(1, 2, 3) Init
			* sFilePath, sPagePath 경로 수정
			* 팝업 이벤트 메서드 수정
			* 전체 조회 >> Web.config 에서 변경 한 connectionStrings 로 수정
      
		(4) RowDeleteOk(object sender, EventArgs e)
			* 쿼리문 - 삭제 >> Table명 변경 및 조건 수정 및 필드명 수정
      
		(5, 6) BtnSave_Click(object sender, EventArgs e)
			* 쿼리문 - 업데이트 >> Table 이름 변경 및 조건 수정 및 필드명 수정
			* 쿼리문 - 인서트 >> Table명 변경 및 조건 수정 및 필드명 수정
      
		(7) TextChanged(object sender, EventArgs e)
			* 쿼리문 - 중복검사 >> Table명 이름 변경 및 조건 수정 및 필드명 수정
      
		(8) ShowOverlayPopup(string[] sOverlayData, string sType)
			* 중복 팝업 내용 수정
      
		(9) BtnViewfile_Click(object sender, EventArgs e)
			* 파일 리스트 폴더 경로 수정 ( 기준 seq값 변경 )
      
		(10) Upload()
			* 쿼리 - 파일 업로드 >> Table명 변경 및 조건 수정 및 필드명 수정


### 6. DbConnect (db 연결)

	(1) database >> db 정보 변경
	
### 7. patentManagement & patentManagementInput 공통
	
	* 각 .aspx 내의 GridView ID 값 변경 할 것 >> 변경 후 - .aspx.cs 의 GridView 변수명 변경*
### 8. StyleSheet.css

	(0) PageContent_toolbarSearchPopup_PW-1
		* 팝업 위치를 조정하기 위해 변경한 ID [NewPopupId]로 left, top 변경 >> 예시와 같이 추가할 것
		ex ) !important 작성 X >> 적용 안될수도 있어요
		.PageContent_[NewPopupId]_PW-1{
			left: *px !important;
			top: *px !important;
		}
			
	(1) PageContent_toolbarSearchPopup_Company_popupFormLayout_co_name_CC
		* 팝업 내부 Textbox 위치를 조정하기 위해 변경한 ID [NewPopupId]와 팝업 내부 변경한 Caption ID [NewCaptionId]로 수정
		ex)
		.PageContent_[NewPopupId]_popupFormLayout_[NewCaptionId]_CC{
			padding: 0 0 0 *px !important;
		}
		or
		.PageContent_[NewPopupId]_popupFormLayout_[NewCaptionId]_CC{
			padding-left: *px !important;
		}
		
	(2) PageContent_toolbarSearchPopup_Product_popupFormLayout_pr_tradeEnd_CapC
		*AspxCalender 클래스 사용했을 경우 수정 >> 기간 끝부분 캡션을 제거하기 위해 추가 필요
### 기능
	(1) PopupSearchUserControl
		0) Numdefine.cs 파일 > //column search popup 주석 아래 부분에 추가하고자 하는 테이블명 혹은 구분값 등록 
		1) 모든 메서드는 각각의 search popup을 Switch - case 문으로 구분해서 사용
		2) #region SearchPopup init(User Control) 으로 묶인 region 내용 부분 수정
		3) SearchPopupEventInit()은 수정할 필요 X
		4) SearchPopup_PageIndexChanged() & SearchPopupInit() 메서드에 쿼리 추가하기
		5) AddGridColumn() 메서드에 아래와 같은 방식으로 그리드 설정
		 	ex)                     
				GridViewDataColumn gdCol = new GridViewDataColumn();
				gdCol = pcSearchPopup.SetGridColumn(gdCol, 0, "us_seq", "번호", 10, DefaultBoolean.False, DefaultBoolean.False, true, true, HorizontalAlign.NotSet);
				pcSearchPopup.AddGridColumn(gdCol);
		6) PcSearchGrid_FocusedRowChanged() 메서드에 입력폼에 바인딩 될 값 세팅
			ex)
                            tbPriceTxt.Text = pcSearchPopup.GetFocusValue("pr_price");
		7) 입력폼에서 돋보기 버튼 클릭 시 이벤트 설정, Numdefine 상수값만 바꿔주기
			ex)
				protected void BtnCoNameColumnSP_Click(object sender, EventArgs e)
				{
				    pcSearchPopup.setTag(NumDefine.COMPANY);
				    ViewState["isCallback"] = true;
				    bType = NumDefine.All_MODE;
				    pcSearchPopup.bType = bType;
				}
		8) BtnSearchBtn_Click() 메서드에서 검색 쿼리 세팅(Switch 구문만 수정)
		9) BtnSearchInner_cancel_Click() 매서드에서 취소 버튼 클릭 시 입력값 돌려놓음
			ex)
				tbCoNameTxt.Text = gdPatentManageInput.GetRowValues(gdPatentManageInput.FocusedRowIndex, "co_name").ToString();
		10) Page_Load()메서드에 SearchPopupEventInit() 추가
		11) Page_PreRender()메서드 생성 후 SearchPopupInit() 추가
			protected void Page_PreRender(object sender, EventArgs ea)
			{
			    //검색팝업 세팅
			    SearchPopupInit();
			}
	(2) 파일리스트(삭제)
		1) aspx 파일
			1-1) PDF파일 새로고침/삭제 버튼 (JS->C#)[숨김] >> 삭제
			1-2) 파일리스트 조회/삭제 수행 (C#->JS) >> 삭제
			1-3) dFilelistplace >> div 삭제
			1-4) PDF파일 추가(파일업로드) >> 삭제
		2) cs 파일
			1-1) CS 파일에서 #region Filelist 부분 다 삭제 ** 간혹 gridview의 포커스 이벤트가 btnview_Click 메서드로 되어있는 경우가 있는데 그 경우는 grid view의 focused 메서드를 바꿔주거나 없애줌
			1-2) tbFileslabel.Focus(); >> 삭제
			1-3) Upload(); >> 삭제
			1-4) 
				DirectoryInfo diFolder = new DirectoryInfo(@"" + Server.MapPath("~/") + sFilePath + tbSeqTxt.Text);
				diFolder.Delete(true);//폴더 파일 다 지움 >> 삭제
			1-5) PopupEventInit() 메서드의 
			
				case NumDefine.FILE_DELETE:
				    pcPopup.OkBtnClick += new EventHandler(FilelistDeleteOk);
				    break;
				    부분 삭제
				    
	(3) 이력 페이지 (날짜 검색이 있는 관리 페이지) - ViewState
		0) 작동원리
			   첫페이지(postback이 아닐떄) >> ViewState Default 값 설정(일주일 전, 오늘) >> ViewState 에 string 형식으로 저장
			>> DateTime 에 Text 설정 >> SetQuery_Search() 함수로 GridView Binding
			
			>> postback 발생 >> ViewState에 Datetime Text 저장 >> Page_Init(or Page_Load) 실행 >> 저장된 ViewState를 Datetime Text에 저장
			>> SetQuery_Search() 함수로 GridView Binding
		1) protected void SearchDate_ViewStateInit()
		{
		    ViewState["DateStart"] = DateTime.Today.AddDays(-7).ToShortDateString();
		    ViewState["DateEnd"] = DateTime.Today.ToShortDateString();
		} >> 페이지를 처음 열었을 때 - ViewState를 설정 해주기 위한 함수 (Page_Init 또는 Page_Load 안에 if(!isPostbak) 조건문을 걸어 추가
		2) protected void SearchDate_TextInit()
		{
		    faF_failureDateStart.Text = (string)ViewState["DateStart"];
		    faF_failureDateEnd.Text = (string)ViewState["DateEnd"];
		} >> postback 발생 시 - ViewState의 날짜(string 형식)를 저장하기 위한 함수 (Page_PreRender 안에 추가)
		3) protected void SetSearchDate_ViewState()
		{
		    if (string.IsNullOrEmpty((string)ViewState["DateStart"]) && string.IsNullOrEmpty((string)ViewState["DateEnd"]))
			SearchDate_ViewStateInit();
		    else
		    {
			ViewState["DateStart"] = faF_failureDateStart.Text;
			ViewState["DateEnd"] = faF_failureDateEnd.Text;
		    }
		} >> postback 발생 시 - SetQuery_Search()로 db에 검색하기전에 Datetime에 저장 하기 위한 함수 (검색 함수 이전에 추가)
