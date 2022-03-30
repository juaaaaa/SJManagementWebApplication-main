<%-- 특허관리페이지(HTML) --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="userInput.aspx.cs" Inherits="SJManagementWebApplication.userInput" EnableEventValidation="false" SmartNavigation="true" %>

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
        <h1 id="h1MainTitle" class="h1Maintitle">사용자 등록</h1>
        <asp:ScriptManager ID="smScriptManager1" runat="server" />
        <%-- 조작 버튼부 --%>
        <dx:ASPxButton ID="btnSave" CssClass="lbButtonicon" Image-Url="~/Content/icon/button_save.svg" ClientInstanceName="btnSave" Font-Size="9pt" ForeColor="White" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" OnClick="BtnSave_Click"></dx:ASPxButton>
        <%-- 업데이트 패널1: 신규/취소/삭제(활성화) --%>
        <asp:UpdatePanel ID="upUpdatePanel1" runat="server">
            <ContentTemplate>
                <%-- 삭제버튼 --%>
                <dx:ASPxButton ID="btnDelete" CssClass="lbButtonicon" Image-Url="~/Content/icon/button_delete.svg" Font-Size="9pt" ForeColor="White" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" OnClick="BtnDelete_Click"></dx:ASPxButton>
                <%-- 신규버튼 --%>
                <dx:ASPxButton ID="btnNew" ClientInstanceName="btnNew" Image-Url="~/Content/icon/button_add.svg" CssClass="lbButtonicon" Font-Size="9pt" ForeColor="Black" AutoPostBack="false" runat="server" ClientIDMode="Static" Width="70px" BackColor="#FFFFFF" ClientSideEvents-Click="newBtnClick" OnClick="BtnNew_Click"></dx:ASPxButton>
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
    <div id="dAlert_delete" runat="server" class="dDataalert_delete" clientidmode="Static">
        <span class="spDataalerticon"><i id="iAlerticon_delete" class="fa fa-trash-o fa-5x"></i></span>
        <dx:ASPxLabel ID="lbAlerttext_delete" CssClass="lbAlerttext" ClientInstanceName="lbAlerttext_delete" runat="server" Text="삭제 완료"></dx:ASPxLabel>
    </div>
    <div id="dAlert_save" runat="server" class="dDataalert_save" clientidmode="Static">
        <span class="spDataalerticon"><i id="iAlerticon_save" class="fa fa-save fa-5x"></i></span>
        <dx:ASPxLabel ID="lbAlerttext_save" CssClass="lbAlerttext" ClientInstanceName="lbAlerttext_save" runat="server" Text="저장 완료"></dx:ASPxLabel>
    </div>
    <div id="dAlert" runat="server" class="dDataalert" clientidmode="Static">
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
                <asp:UpdatePanel ID="upUpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <%-- 직원명(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbNameLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="직원명"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbNameTxt" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" Text="" AutoPostBack="true" OnTextChanged="TextChanged" ClientInstanceName="tbNameTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        <%-- 연락처(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbphoneLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="연락처"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbphoneTxt" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" Text="" AutoPostBack="false" ClientInstanceName="tbphoneTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        <%-- ID(텍스트박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbidLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="ID"></dx:ASPxLabel>
                            <dx:ASPxTextBox ID="tbidLab" AutoCompleteType="Disabled" CssClass="tbInput" runat="server" Text="" AutoPostBack="true" OnTextChanged="TextChanged" ClientInstanceName="tbphoneTxt" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid"></dx:ASPxTextBox>
                        </div>
                        <%-- 직급(콤보박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbjobLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="직급"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cbjobTxt" runat="server" AutoPostBack="false" ClientInstanceName="cbjobTxt" Font-Size="12px" ButtonStyle-Font-Size="12px" ListBoxStyle-Font-Size="12px" ItemStyle-Font-Size="12px" HelpTextStyle-Font-Size="12px" CssClass="tbInput" AllowNull="true" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                                <Items>
                                    <dx:ListEditItem Value="책임연구원" Text="책임연구원" />
                                    <dx:ListEditItem Value="선임연구원" Text="선임연구원" />
                                    <dx:ListEditItem Value="연구원" Text="연구원" />
                                </Items>
                                <DropDownButton Image-Url="/Content/icon/icon_dropdown.svg" Image-Width="8px" Image-Height="12px"></DropDownButton>
                                <ButtonStyle BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="0px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1" Width="15" CssClass="btnstCombobox" BackColor="#f2f2f2"></ButtonStyle>
                            </dx:ASPxComboBox>
                        </div>
                        <%-- 소속(콤보박스) --%>
                        <div class="dInputForm3">
                            <dx:ASPxLabel ID="lbdepartLab" CssClass="lbColumn" runat="server" Font-Size="12px" Text="소속"></dx:ASPxLabel>
                            <dx:ASPxComboBox ID="cbdepartTxt" runat="server" AutoPostBack="false" ClientInstanceName="cbdepartTxt" Font-Size="12px" ButtonStyle-Font-Size="12px" ListBoxStyle-Font-Size="12px" ItemStyle-Font-Size="12px" HelpTextStyle-Font-Size="12px" CssClass="tbInput" AllowNull="true" FocusedStyle-Border-BorderWidth="1px" FocusedStyle-Border-BorderColor="#0071bc" FocusedStyle-Border-BorderStyle="Solid">
                                <Items>
                                    <dx:ListEditItem Value="ICT연구소" Text="ICT연구소" />
                                    <dx:ListEditItem Value="Seal 사업부" Text="Seal 사업부" />
                                    <dx:ListEditItem Value="기술연구소" Text="기술연구소" />
                                </Items>
                                <DropDownButton Image-Url="/Content/icon/icon_dropdown.svg" Image-Width="8px" Image-Height="12px"></DropDownButton>
                                <ButtonStyle BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="0px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1" Width="15" CssClass="btnstCombobox" BackColor="#f2f2f2"></ButtonStyle>
                            </dx:ASPxComboBox>
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
                <%-- 출원일자/등록일자 포스트백 방지--%>
                <asp:UpdatePanel ID="UpdatePanel1_1" runat="server" UpdateMode="Always">

                    <Triggers>
                        <%-- 업데이트 패널 트리거(이벤트) --%>
                        <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <%-- 업데이트 패널3: 그리드뷰/js<->c#조작부 --%>
    <asp:UpdatePanel ID="upUpdatePanel3" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <%--팝업--%>
            <UC:Popup ID="pcPopup" runat="server"></UC:Popup>
            <%-- 그리드뷰 생략된 글자 Hint(ellipsis - Tooltip) --%>
            <dx:ASPxHint ID="ASPxHint1" runat="server" TargetSelector="dx-ellipsis"></dx:ASPxHint>
            <%-- 그리드뷰 레이아웃--%>
            <div class="dGridviewLayout">
                <%-- PDF파일 새로고침/삭제 버튼 (JS->C#)[숨김] --%>
                <div class="dCstojs">
                    <dx:ASPxTextBox runat="server" ID="tbFileslabel" AutoPostBack="false" ClientInstanceName="tbFileslabel" Width="100%" ClientSideEvents-GotFocus="filelisttext"></dx:ASPxTextBox>
                    <dx:ASPxTextBox runat="server" ID="tbFilename" AutoPostBack="false" ClientInstanceName="tbFilename" Width="20%"></dx:ASPxTextBox>
                </div>
                <%-- 파일리스트 조회/삭제 수행 (C#->JS) --%>
                <div hidden="hidden">
                    <dx:ASPxButton ID="btnViewfile" CssClass="btnViewfile" runat="server" AutoPostBack="false" OnClick="BtnViewfile_Click"></dx:ASPxButton>
                    <dx:ASPxButton ID="btnPdfdel" CssClass="btnPdfdel" runat="server" AutoPostBack="false" OnClick="FilelistDeletePopup"></dx:ASPxButton>
                </div>
                <%-- 전체 보기/초기화 버튼 + 총개수 --%>
                <div id="dGridHeader">
                    <dx:ASPxButton runat="server" CssClass="btnFilterCancel" ID="btnFilterCancel" AutoPostBack="false" Image-Url="~/Content/icon/button_View_all.svg" Image-Width="75px" Image-Height="22px" ClientIDMode="Static" OnClick="FilterSortClear" BackColor="White" Border-BorderWidth="0" ForeColor="Black"></dx:ASPxButton>
                    <dx:ASPxLabel ID="lbFilterSummary" runat="server" Text="" Visible="true"></dx:ASPxLabel>
                </div>

                <%-- 사용자 등록 그리드뷰 --%>
                <dx:ASPxGridView ID="gdPatentManageInput" OnFocusedRowChanged="BtnViewfile_Click" EnableViewState="false" SettingsContextMenu-RowMenuItemVisibility-Refresh="false" SettingsLoadingPanel-Mode="Disabled" Font-Size="9pt" runat="server" ClientInstanceName="gdGridview" SettingsBehavior-AllowClientEventsOnLoad="true" AutoGenerateColumns="False" KeyFieldName="us_seq" CssClass="gdGridview" Width="100%">
                    <%-- 설정 --%>
                    <StylesExport Cell-Font-Names="Malgun Gothic"></StylesExport>
                    <SettingsResizing ColumnResizeMode="Control" Visualization="Postponed" />
                    <%-- 열 사이즈 조절 관련 --%>
                    <SettingsBehavior AllowEllipsisInText="true" AllowSort="true" AllowFocusedRow="true" AllowHeaderFilter="true" SortMode="Value" ProcessFocusedRowChangedOnServer="false" />
                    <%-- 권한(행동관련) --%>
                    <SettingsDataSecurity AllowEdit="True" AllowInsert="True" AllowDelete="True" AllowReadUnlistedFieldsFromClientApi="True"></SettingsDataSecurity>
                    <%-- 권한(데이터관련) --%>
                    <SettingsPopup>
                        <FilterControl AutoUpdatePosition="False">
                        </FilterControl>
                    </SettingsPopup>
                    <%-- 팝업 관련 --%>
                    <SettingsContextMenu Enabled="true" EnableRowMenu="False" EnableFooterMenu="False" ColumnMenuItemVisibility-ShowSearchPanel="false" ColumnMenuItemVisibility-ShowFilterRow="false" ColumnMenuItemVisibility-ShowFilterEditor="false" ColumnMenuItemVisibility-ShowFooter="false">
                        <RowMenuItemVisibility ExportMenu-Visible="true">
                            <GroupSummaryMenu SummaryAverage="false" SummaryMax="false" SummaryMin="false" SummarySum="false" />
                        </RowMenuItemVisibility>
                    </SettingsContextMenu>
                    <%-- 우클릭 메뉴 관련 --%>
                    <SettingsText ContextMenuSortAscending="오름차순 정렬" ContextMenuSortDescending="내림차순 정렬" ContextMenuClearSorting="정렬 해제" ContextMenuGroupByColumn="이 열로 그룹열기" ContextMenuUngroupColumn="그룹 해제" ContextMenuClearGrouping="전체 그룹 해제" ContextMenuFullExpand="전체 확장" ContextMenuFullCollapse="전체 접기" ContextMenuShowGroupPanel="그룹패널 열기" ContextMenuClearFilter="필터 해제" HeaderFilterShowBlanks="(빈칸)" HeaderFilterShowNonBlanks="(빈칸 아님)" HeaderFilterSelectAll="전체 선택" HeaderFilterShowAll="(필터 해제)" />
                    <%-- 한글화 관련 --%>
                    <SettingsSearchPanel Visible="False"></SettingsSearchPanel>
                    <%-- 검색 패널 --%>
                    <SettingsPager Mode="ShowPager" Position="Bottom" PageSize="15" AlwaysShowPager="true" FirstPageButton-Visible="true" LastPageButton-Visible="true" NextPageButton-Visible="true" PrevPageButton-Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" Summary-Visible="false">
                        <FirstPageButton Visible="true" Image-Url="/Content/icon/icon_keyboard_double_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_left_gray.svg" Image-Width="15px"></FirstPageButton>
                        <PrevPageButton Image-Url="/Content/icon/icon_keyboard_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_left_gray.svg" Image-Width="15px"></PrevPageButton>
                        <NextPageButton Image-Url="/Content/icon/icon_keyboard_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_right_gray.svg" Image-Width="15px"></NextPageButton>
                        <LastPageButton Visible="true" Image-Url="/Content/icon/icon_keyboard_double_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_right_gray.svg" Image-Width="15px"></LastPageButton>
                        <Summary Text="총 {2}건" />
                    </SettingsPager>
                    <%-- 페이지 관리 --%>
                    <Settings VerticalScrollableHeight="480" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="true" />
                    <%-- 일반 설정 --%>
                    <ClientSideEvents ContextMenuItemClick="setPagerTop" CallbackError="showLoadingPanelDemo" EndCallback="hideLoadingPanelDemo" FocusedRowChanged="Manage_FocusedRowChanged" ColumnSorting="sortandfocus" />
                    <%-- 클라이언트부 이벤트 --%>
                    <SettingsExport EnableClientSideExportAPI="true" Landscape="true" ExcelExportMode="DataAware" PaperKind="A3"></SettingsExport>
                    <%-- 내보내기 설정 --%>
                    <%-- 스타일 --%>
                    <StylesPager>
                        <CurrentPageNumber BackColor="White" ForeColor="#0071bc" Font-Bold="true" Font-Underline="false" Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px"></CurrentPageNumber>
                        <PageNumber Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px"></PageNumber>
                        <Button Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px" Paddings-PaddingTop="2.5px" Paddings-PaddingBottom="0px"></Button>
                        <DisabledButton Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px"></DisabledButton>
                        <Summary Paddings-PaddingLeft="0px" Paddings-PaddingTop="5px" HorizontalAlign="Right"></Summary>
                    </StylesPager>
                    <%-- 페이저 스타일 --%>
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
                    </Styles>
                    <%-- 스타일 --%>
                    <Images>
                        <HeaderFilter Height="15px" Width="14px">
                        </HeaderFilter>
                    </Images>
                    <%-- 이미지 --%>
                    <%-- 열 --%>
                    <Columns>
                        <%--<dx:GridViewDataTextColumn FieldName="번호" VisibleIndex="0" UnboundType="String" Width="40px"></dx:GridViewDataTextColumn>--%>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" Visible="false" SortIndex="0" Width="40px" SortOrder="None" Settings-AllowHeaderFilter="False" CellStyle-HorizontalAlign="Right" Settings-SortMode="Custom" FieldName="us_seq" Caption="번호" VisibleIndex="0" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_name" Settings-AllowHeaderFilter="False" Caption="직원명" Width="90px" VisibleIndex="1" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_phone" Settings-AllowHeaderFilter="False" Caption="연락처" Width="90px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" PropertiesTextEdit-DisplayFormatString="{0:000-0000-0000}"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_id" Settings-AllowHeaderFilter="False" Caption="ID" Width="90px" VisibleIndex="3" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" Settings-AllowHeaderFilter="True" FieldName="us_job" Caption="직급" Width="65px" VisibleIndex="4" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" Settings-AllowHeaderFilter="True" FieldName="us_depart" Caption="소속" Width="80px" VisibleIndex="6" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="regUserId" Settings-AllowHeaderFilter="False" Caption="등록자" Width="45px" VisibleIndex="8" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="regDate" Settings-AllowHeaderFilter="False" Caption="등록일시" Width="180px" VisibleIndex="9" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modUserId" Settings-AllowHeaderFilter="False" Caption="수정자" Width="45px" VisibleIndex="10" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modDate" Settings-AllowHeaderFilter="False" Caption="수정일시" Width="180px" VisibleIndex="11" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                    </Columns>
                    <%-- 그리드뷰 내부 ToolTip --%>
                    <ClientSideEvents EndCallback="function(s,e){
                        ASPxClientHint.Update();
                        }" />
                </dx:ASPxGridView>

                <%-- 입력한 페이지로 이동하는 pager (JS->C#) --%>
                <div runat="server" id="dPagernum" class="dPagernum">
                    <dx:ASPxTextBox Paddings-Padding="2px" runat="server" ID="tbPagerinput" Width="26" Font-Size="9pt" Style="display: inline-block;" ClientSideEvents-Init="setPagerTop" AutoCompleteType="Disabled">
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel runat="server" ID="spPagecount"></dx:ASPxLabel>
                    <dx:ASPxButton runat="server" ID="aSubmitpager" CssClass="aSubmitpager" AutoPostBack="false" BackgroundImage-ImageUrl="/Content/icon/icon_expand_circle_right_blue.svg" OnClick="BtnSubmitPager_Click" ClientIDMode="Static" BackColor="#ffffff"></dx:ASPxButton>
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
