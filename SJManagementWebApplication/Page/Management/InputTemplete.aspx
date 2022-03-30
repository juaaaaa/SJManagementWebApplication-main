<%-- 특허관리페이지(HTML) --%>

<%-- 템플릿2: 모호성 처리(inherits 변경) --%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="InputTemplete.aspx.cs" Inherits="SJManagementWebApplication.InputTemplete" EnableEventValidation="false" SmartNavigation="true"%>

<%@ Register TagPrefix="UC" TagName="Popup" Src="../../code/PopupWebUserControl.ascx" %>
<%-- 헤더+툴바 --%>
<asp:Content ID="cContent1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="cContent2" ContentPlaceHolderID="PageToolbar" runat="server">
</asp:Content>
<%-- 페이지 내부 컨텐츠 --%>

<asp:Content ID="cContent3" ContentPlaceHolderID="PageContent" runat="server">
    <%-- 상단 타이틀+메뉴+구분선 --%>
    <div id="dHeadermenu" class="dHeadermenu">
        <h1 id="h1MainTitle" class="h1Maintitle">입력페이지</h1>
        <asp:ScriptManager ID="smScriptManager1" runat="server"/>
        <%-- 조작 버튼부 --%>
        <dx:ASPxButton ID="btnSave" CssClass="lbButtonicon" Image-Url="~/Content/icon/button_save.svg" ClientInstanceName="btnSave" Font-Size="9pt" ForeColor="White" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" OnClick="btnSave_Click"></dx:ASPxButton>
        <%-- 업데이트 패널1: 신규/취소/삭제(활성화) --%>
        <asp:UpdatePanel ID="upUpdatePanel1" runat="server">
            <ContentTemplate>                
                <%-- 삭제버튼 --%>
                <dx:ASPxButton ID="btnDelete" CssClass="lbButtonicon" Image-Url="~/Content/icon/button_delete.svg" Font-Size="9pt" ForeColor="White" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" OnClick="btnDelete_Click"></dx:ASPxButton>
               <%-- 신규버튼 --%>
                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" Image-Url="~/Content/icon/button_add.svg" CssClass="lbButtonicon"  Font-Size="9pt" ForeColor="Black" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" ClientSideEvents-Click="newBtnClick" OnClick="btnNew_Click"></dx:ASPxButton><%--BackgroundImage-ImageUrl="/Content/icon/button_추가.svg"--%>
                <div id="dBtnline" class="dBtnline"></div>
                <%-- pdf,word,excel 출력버튼 --%>
                <dx:ASPxLabel ID="lbPdficon" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_pdf.svg" Font-Size="11px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickPdf"></dx:ASPxLabel>
                <dx:ASPxLabel ID="lbWordicon" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_docx.svg" Font-Size="11px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickWord"></dx:ASPxLabel>
                <dx:ASPxLabel ID="lbExcelicon" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_xlsx.svg" Font-Size="11px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickExcel"></dx:ASPxLabel>
            </ContentTemplate>
            <Triggers>
                <%-- 업데이트 패널 트리거(이벤트) --%>
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                <%--<asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Click" />--%>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <hr class="hrTitle" />

    <%-- 데이터 변경(추가/수정/삭제/취소) 알림 --%>
    <div id="dAlert_delete" runat="server" class="dDataalert_delete" ClientIDMode="Static">
        <span class="spDataalerticon"><i id="iAlerticon_delete" class="fa fa-trash-o fa-5x"></i></span>
        <dx:ASPxLabel ID="lbAlerttext_delete" CssClass="lbAlerttext" ClientInstanceName="lbAlerttext_delete" runat="server" Text="삭제 완료"></dx:ASPxLabel>
    </div>
    <div id="dAlert_save" runat="server" class="dDataalert_save" ClientIDMode="Static">
        <span class="spDataalerticon"><i id="iAlerticon_save" class="fa fa-save fa-5x"></i></span>
        <dx:ASPxLabel ID="lbAlerttext_save" CssClass="lbAlerttext" ClientInstanceName="lbAlerttext_save" runat="server" Text="저장 완료"></dx:ASPxLabel>
    </div>
    <div id="dAlert" runat="server" class="dDataalert" ClientIDMode="Static">
        <span class="spDataalerticon"><i id="iAlerticon" class="fa fa-times fa-5x"></i></span>
        <dx:ASPxLabel ID="lbAlerttext" CssClass="lbAlerttext" ClientInstanceName="lbAlerttext" runat="server" Text="취소"></dx:ASPxLabel>
    </div>
    
    <%-- 입력부 --%>
    <div id="dInputBox">
        <div class="dInputForm1">
            <%-- seq(텍스트박스)[숨김] --%>
            <div hidden="hidden">
                <dx:ASPxTextBox ID="tbSeqTxt" ClientInstanceName="tbSeqTxt" Text="" runat="server" Width="170px"></dx:ASPxTextBox>
            </div>
            <div id="dInputline1">
                <%--업데이트패널2: 값 변경 여부 확인용 --%>
                 <asp:UpdatePanel ID="upUpdatePanel2" runat="server" UpdateMode="Always"  >
                    <ContentTemplate>
                        <%-- 특허명(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbNameLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="텍스트1"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbNameTxt" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" Text="" AutoPostBack="true" OnTextChanged="textChanged" ClientInstanceName="tbNameTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        <%-- 종류(콤보박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbDivLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="콤보박스1"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cbDivTxt" runat="server" AutoPostBack="true" OnSelectedIndexChanged="textChanged" OnTextChanged="textChanged" ClientInstanceName="cbDivTxt" Font-Size="12px" ButtonStyle-Font-Size="12px" ListBoxStyle-Font-Size="12px" ItemStyle-Font-Size="12px" HelpTextStyle-Font-Size="12px" CssClass="tbInput" AllowNull="true" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                                <Items>
                                    <dx:ListEditItem Value="1" Text="1" />
                                    <dx:ListEditItem Value="2" Text="2" />
                                    <dx:ListEditItem Value="3" Text="3" />
                                </Items>
                                <DropDownButton Image-Url="/Content/icon/icon_dropdown.svg" Image-Width="8px" Image-Height="12px"></DropDownButton>
                                <ButtonStyle BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="0px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1" Width="15" CssClass="btnstCombobox" BackColor="#f2f2f2"></ButtonStyle>
                            </dx:ASPxComboBox>
                        </div>
                        <%-- 특허 번호(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbNumTxtLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="텍스트2"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbNumTxt" MaxLength="10" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" AutoPostBack="true" OnTextChanged="textChanged" Text="" ClientInstanceName="tbNumTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        <%-- 출원 번호(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbAppnumLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="텍스트3"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbAppnumTxt" MaxLength="15" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" AutoPostBack="true" OnTextChanged="textChanged" Text="" ClientInstanceName="tbAppnumTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        </ContentTemplate>
                    <Triggers>
                        <%-- 업데이트 패널 트리거(이벤트) --%>
                        <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div> 
            <div id="dInputline2">
                <%-- 발명자(텍스트박스) --%>
                <div class="dInputForm3">
                    <dx:ASPxLabel ID="lbInventorLab" CssClass="lbColumn" runat="server" Font-Size="9pt" Text="텍스트4"></dx:ASPxLabel>
                    <dx:ASPxTextBox ID="tbInventorTxt" MaxLength="9" AutoCompleteType="Disabled" CssClass="tbInput" OnTextChanged="textChanged" runat="server" Text="" ClientInstanceName="tbInventorTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                        <HelpTextSettings Position="Right"></HelpTextSettings>
                    </dx:ASPxTextBox>
                </div>
                <%-- 출원 일자(달력) --%>
                <div class="dInputForm3">
                    <dx:ASPxLabel ID="lbFilldateLab" CssClass="lbColumn" runat="server" Font-Size="9pt" Text="날짜1"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="deFilldate" runat="server" CssClass="tbInput" OnValueChanged="textChanged" AutoPostBack="true" ClientInstanceName="deFilldate" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                        <DropDownButton Image-Url="/Content/icon/icon_캘린더.svg" Image-Height="13px" Image-Width="13px"></DropDownButton>
                        <ButtonStyle Width="25px" BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="0px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1">
                            <HoverStyle BackColor="White"></HoverStyle>
                        </ButtonStyle>
                    </dx:ASPxDateEdit>
                </div>
                <%-- 등록 일자(달력) --%>
                <div class="dInputForm3">
                    <dx:ASPxLabel ID="lbRegdateLab" CssClass="lbColumn" runat="server" Font-Size="9pt" Text="날짜2"></dx:ASPxLabel>
                    <dx:ASPxDateEdit ID="deRegdate" AutoPostBack="true" CssClass="tbInput" runat="server" OnValueChanged="textChanged" ClientInstanceName="deRegdate" Font-Size="9pt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                        <DropDownButton Image-Url="/Content/icon/icon_캘린더.svg" Image-Height="13px" Image-Width="13px"></DropDownButton>
                        <ButtonStyle Font-Size="9pt" Width="25px" BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="0px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1">
                            <HoverStyle BackColor="White"></HoverStyle>
                        </ButtonStyle>
                    </dx:ASPxDateEdit>
                </div>
                <%-- PDF파일 추가(파일업로드) --%>
                <div class="dInputForm3">
                    <dx:ASPxLabel ID="lbPdfLab" CssClass="lbColumn" ClientVisible="true" ClientInstanceName="lbPdfLab" runat="server" ClientIDMode="Static" Font-Size="9pt" Text="파일1"></dx:ASPxLabel>
                    <asp:FileUpload ID="fuFileUpload1" CssClass="tbInput" runat="server" ClientIDMode="Static" accept="application/pdf" AllowMultiple="true" />
                    <div class="dFileUpload">
                        <label for="fuFileUpload1"><i></i>파일 업로드</label>
                    </div>
                </div>
            </div>
            <%--
            <div id="dFilelistplace">
                <div id="dFilelistout" class="dFilelistout" runat="server" clientidmode="Static">
                    <%-- 파일리스트 배경
                    <div id="dFilelist">
                        <%-- 슬라이드 부(이동) 
                        <ul id="ulSlider"></ul>
                        <%-- 이전/이후 버튼
                        <div class="dBtns" id="dNext"></div>
                        <div class="dBtns" id="dPrevious"></div>
                        <%-- 파일리스트 페이지 매기기 
                        <div id="dPaginationwrap" class="dPaginationwrap" runat="server" clientidmode="Static">
                            <ul></ul>
                        </div>
                    </div>
                </div>
            </div>
            --%>
        </div>
    </div>

    <%-- 업데이트 패널3: 그리드뷰/js<->c#조작부 --%>
    <asp:UpdatePanel ID="upUpdatePanel3" runat="server" UpdateMode="Always"  >
        <ContentTemplate>
            <%--팝업--%>
            <UC:Popup ID="pcPopup" runat="server"></UC:Popup>
            <%-- 그리드뷰 레이아웃--%>
            <div class="dGridviewLayout">
                <%-- PDF파일 새로고침/삭제 버튼 (JS->C#)[숨김] --%>
                <div class="dCstojs">                 
                    <dx:ASPxTextBox runat="server" ID="tbFileslabel" AutoPostBack="false" ClientInstanceName="tbFileslabel" Width="100%" ClientSideEvents-GotFocus="filelisttext"></dx:ASPxTextBox>
                    <dx:ASPxTextBox runat="server" ID="tbFilename" AutoPostBack="false" ClientInstanceName="tbFilename" Width="20%"></dx:ASPxTextBox>
                </div>
                <%-- 파일리스트 조회/삭제 수행 (C#->JS) --%>
                <div hidden="hidden">
                    <dx:ASPxButton ID="btnViewfile" CssClass="btnViewfile" runat="server" AutoPostBack="false" OnClick="btnViewfile_Click"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnPdfdel" CssClass="btnPdfdel" runat="server" AutoPostBack="false" OnClick="filelistDeletePopup"></dx:ASPxButton>
                </div>
                <%-- 전체 보기/초기화 버튼 + 총개수 --%>
                <div id="dGridHeader">
                    <dx:ASPxButton runat="server" CssClass="btnFilterCancel" id="btnFilterCancel" AutoPostBack="false" Image-Url="~/Content/icon/button_View_all.svg" Image-Width="75px" Image-Height="22px" ClientIDMode="Static" OnClick="FilterSortClear" BackColor="White" Border-BorderWidth="0" ForeColor="Black"></dx:ASPxButton>
                    <dx:ASPxLabel ID="lbFilterSummary" runat="server" Text="" Visible="true"></dx:ASPxLabel>
                </div>

                <%-- 특허관리 등록 그리드뷰 --%>
                <dx:ASPxGridView ID="gdPatentManageInput"  OnFocusedRowChanged="btnViewfile_Click" EnableViewState="false" SettingsContextMenu-RowMenuItemVisibility-Refresh="false" SettingsLoadingPanel-Mode="Disabled" Font-Size="9pt" runat="server" ClientInstanceName="gdGridview" SettingsBehavior-AllowClientEventsOnLoad="true" AutoGenerateColumns="False" KeyFieldName="ip_seq" Cssclass="gdGridview" Width="100%"><%--OnCustomUnboundColumnData="gdPatentManageInput_CustomUnboundColumnData"--%>
                    <%-- 설정 --%>
                    <StylesExport Cell-Font-Names="Malgun Gothic"></StylesExport>
                    <SettingsResizing ColumnResizeMode="Control" Visualization="Postponed"/> <%-- 열 사이즈 조절 관련 --%>
                    <SettingsBehavior AllowEllipsisInText="true" AllowSort="true" AllowFocusedRow="true" AllowHeaderFilter="true" SortMode="Value" ProcessFocusedRowChangedOnServer="false" /> <%-- 권한(행동관련) --%>
                    <SettingsDataSecurity AllowEdit="True" AllowInsert="True" AllowDelete="True" AllowReadUnlistedFieldsFromClientApi="True"></SettingsDataSecurity> <%-- 권한(데이터관련) --%>
                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="False">
                        </FilterControl>
                    </SettingsPopup> <%-- 팝업 관련 --%>
                    <SettingsContextMenu Enabled="true" EnableRowMenu="False" EnableFooterMenu="False" ColumnMenuItemVisibility-ShowSearchPanel="false" ColumnMenuItemVisibility-ShowFilterRow="false" ColumnMenuItemVisibility-ShowFilterEditor="false" ColumnMenuItemVisibility-ShowFooter="false">
                        <RowMenuItemVisibility ExportMenu-Visible="true">
                            <GroupSummaryMenu SummaryAverage="false" SummaryMax="false" SummaryMin="false" SummarySum="false" />
                        </RowMenuItemVisibility>
                    </SettingsContextMenu> <%-- 우클릭 메뉴 관련 --%>
                    <SettingsText ContextMenuSortAscending="오름차순 정렬" ContextMenuSortDescending="내림차순 정렬" ContextMenuClearSorting="정렬 해제" ContextMenuGroupByColumn="이 열로 그룹열기" ContextMenuUngroupColumn="그룹 해제" ContextMenuClearGrouping="전체 그룹 해제" ContextMenuFullExpand="전체 확장" ContextMenuFullCollapse="전체 접기" ContextMenuShowGroupPanel="그룹패널 열기" ContextMenuClearFilter="필터 해제" HeaderFilterShowBlanks="(빈칸)" HeaderFilterShowNonBlanks="(빈칸 아님)" HeaderFilterSelectAll="전체 선택" HeaderFilterShowAll="(필터 해제)"/> <%-- 한글화 관련 --%>
                    <SettingsSearchPanel Visible="False"></SettingsSearchPanel> <%-- 검색 패널 --%>
                    <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="15" AlwaysShowPager="true" FirstPageButton-Visible="true" LastPageButton-Visible="true" NextPageButton-Visible="true" PrevPageButton-Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" Summary-Visible="false">
                        <FirstPageButton Visible="true" Image-Url="/Content/icon/icon_keyboard_double_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_left_gray.svg" Image-Width="15px"></FirstPageButton>
                        <PrevPageButton Image-Url="/Content/icon/icon_keyboard_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_left_gray.svg" Image-Width="15px"></PrevPageButton>
                        <NextPageButton Image-Url="/Content/icon/icon_keyboard_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_right_gray.svg" Image-Width="15px"></NextPageButton>
                        <LastPageButton Visible="true" Image-Url="/Content/icon/icon_keyboard_double_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_right_gray.svg" Image-Width="15px"></LastPageButton>
                        <Summary Text="총 {2}건" />
                    </SettingsPager> <%-- 페이지 관리 --%>
                    <Settings VerticalScrollableHeight="480" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="true" /> <%-- 일반 설정 --%>
                    <ClientSideEvents ContextMenuItemClick="setPagerTop" CallbackError="showLoadingPanelDemo" EndCallback="hideLoadingPanelDemo" FocusedRowChanged="Manage_FocusedRowChanged" ColumnSorting="sortandfocus" /> <%-- 클라이언트부 이벤트 --%>
                    <SettingsExport EnableClientSideExportAPI="True"></SettingsExport><%-- 내보내기 설정 --%>
                    <%-- 스타일 --%>
                    <StylesPager>
                        <CurrentPageNumber BackColor="White" ForeColor="#0071bc" Font-Bold="true" Font-Underline="false" Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px"></CurrentPageNumber>
                        <PageNumber Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px"></PageNumber>
                        <Button Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px" Paddings-PaddingTop="2.5px" Paddings-PaddingBottom="0px"></Button>
                        <DisabledButton Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px"></DisabledButton>
                        <Summary Paddings-PaddingLeft="0px" Paddings-PaddingTop="5px" HorizontalAlign="Right"></Summary>
                    </StylesPager><%-- 페이저 스타일 --%>
                    <Styles FocusedRow-BackColor="#4796ce">
                        <AlternatingRow BackColor="#EEEEEE" Enabled="False">
                        </AlternatingRow>
                        <Header BackColor="#f2f2f2" HorizontalAlign="Center" Paddings-PaddingTop="2px" Paddings-PaddingBottom="2px" Paddings-PaddingLeft="4px" Paddings-PaddingRight="4px"></Header>
                        <FocusedRow BackColor="#4796CE" ForeColor="#FFFFFF">
                        </FocusedRow>
                        <FocusedCell Font-Bold="true">
                        </FocusedCell>
                        <Cell Paddings-PaddingTop="5px" Paddings-PaddingLeft="5px" Border-BorderColor="#dedede" Border-BorderWidth="1px" Border-BorderStyle="Solid" BorderTop-BorderStyle="None" BorderBottom-BorderStyle="None">
                        </Cell>
                        <PagerTopPanel BackColor="White" HorizontalAlign="Left">
                        </PagerTopPanel>
                        <HeaderFilterItem Height="9px"></HeaderFilterItem>
                        <AlternatingRow BackColor="#F2F2F2" Enabled="True">
                        </AlternatingRow>
                    </Styles><%-- 스타일 --%>
                    <Images>
                        <HeaderFilter Height="15px" Width="14px">
                        </HeaderFilter>
                    </Images><%-- 이미지 --%>
                    <%-- 열 --%>
                    <Columns>
                        <%--<dx:GridViewDataTextColumn FieldName="번호" VisibleIndex="0" UnboundType="String" Width="40px"></dx:GridViewDataTextColumn>--%>  
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" Visible="false" SortIndex="0" Width="40px" SortOrder="None" Settings-AllowHeaderFilter="False" CellStyle-HorizontalAlign="Right" Settings-SortMode="Custom" FieldName="ip_seq" Caption="seq" VisibleIndex="0" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modDate" Settings-AllowHeaderFilter="False" Caption="열1" Width="180px" VisibleIndex="11" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modDate" Settings-AllowHeaderFilter="False" Caption="열2" Width="180px" VisibleIndex="11" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                    </Columns>
                </dx:ASPxGridView>

                 <%-- 입력한 페이지로 이동하는 pager (JS->C#) --%>
                <div runat="server" id="dPagernum" class="dPagernum">
                    <dx:ASPxTextBox Paddings-Padding="2px" runat="server" ID="tbPagerinput" Width="26" Font-Size="9pt" style="display:inline-block;" ClientSideEvents-Init="setPagerTop" AutoCompleteType="Disabled">
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel runat="server" ID="spPagecount"></dx:ASPxLabel>
                    <dx:ASPxButton runat="server" ID="aSubmitpager" CssClass="aSubmitpager" AutoPostBack="false" BackgroundImage-ImageUrl="/Content/icon/icon_expand_circle_right_blue.svg" OnClick="submitPager" ClientIDMode="Static" BackColor="#ffffff"></dx:ASPxButton>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <%-- 업데이트 패널 트리거(이벤트) --%>
            <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnViewfile" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnPdfdel" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
