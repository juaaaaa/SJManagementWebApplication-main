<%-- 특허관리페이지(HTML) --%>

<%@ Page Title="" Language="C#" MasterPageFile="~/Root.master" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="SJManagementWebApplication.user"
    EnableEventValidation="false" ResponseEncoding="utf-8" %>

<%@ Register TagPrefix="UC" TagName="Popup" Src="../../code/PopupWebUserControl.ascx" %>
<%-- 헤더+툴바 --%>
<asp:Content ID="cContent1" ContentPlaceHolderID="Head" runat="server">
</asp:Content>
<asp:Content ID="cContent2" ContentPlaceHolderID="PageToolbar" runat="server">
</asp:Content>
<%-- 페이지 내부 컨텐츠 --%>
<asp:Content ID="cContent3" ContentPlaceHolderID="PageContent" runat="server">
    <link rel="stylesheet" href="../../Content/gdManagentStyleSheet.css" />
    <%-- 상단 타이틀+구분선 --%>
    <div style="width: 868px; height: 40px;">
        <h1 id="h1MainTitle" class="h1Maintitle">사용자 관리</h1>
        <asp:ScriptManager ID="smScriptManager1" runat="server" />
        <%-- 상단 Header Button --%>
        <asp:UpdatePanel ID="updatePanelHead" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <%-- Pdf 출력 버튼 --%>
                <dx:ASPxLabel ID="slbPdficon" Cursor="hand" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_pdf.svg" Font-Size="9px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickPdf"></dx:ASPxLabel>
                <%-- Word 출력 버튼 --%>
                <dx:ASPxLabel ID="lbWordicon" Cursor="hand" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_docx.svg" Font-Size="9px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickWord"></dx:ASPxLabel>
                <%-- XLSX 출력 버튼 --%>
                <dx:ASPxLabel ID="slbExcelicon" Cursor="auto" CssClass="lbButtonicon" runat="server" ClientIDMode="Static" BackgroundImage-ImageUrl="/Content/icon/button_xlsx.svg" Font-Size="9px" Width="19px" Height="19px" ClientSideEvents-Click="OnExportClickExcel"></dx:ASPxLabel>
                <div id="dBtnline" style="height: 10px; width: 9px; border-left: 1px solid #B3B3B3; float: right; margin-top: 17.5px;"></div>
                <%-- 검색 버튼 --%>
                <dx:ASPxButton AutoPostBack="false" runat="server" ID="slbButtonicon" CssClass="slbButtonicon" BackgroundImage-ImageUrl="~/Content/icon/button_search.svg" BackgroundImage-Repeat="NoRepeat" Width="75px" Height="22px" OnClick="OnSearchBtnClick" UseSubmitBehavior="false"
                    ClientSideEvents-Click="function (s, e) {toolbarSearchPopup.Show(); }">
                </dx:ASPxButton>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <hr class="hrTitle" style="margin: 0px;" />
    <%-- 업데이트 패널1: 그리드뷰/js<->c#조작부 --%>
    <asp:UpdatePanel ID="upUpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <%--검색 팝업--%>
            <dx:ASPxPopupControl ID="toolbarSearchPopup" CssClass="popupSearchCSS" ClientInstanceName="toolbarSearchPopup" CloseAction="OuterMouseClick" MaxWidth="600px" AutoUpdatePosition="false"
                AllowDragging="false" EnableViewState ="false" AllowResize="false" ShowFooter="false" PopupElementID="ShowButtonSearch" PopupAnimationType="Fade" Modal="true" ShowHeader="false" ShowShadow="true"
                runat="server" ShowOnPageLoad="false" EnableHierarchyRecreation="true" HeaderText="검색창" CloseAnimationType="Fade">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="searchPopup" runat="server">
                        <dx:ASPxFormLayout runat="server" ID="popupFormLayout">
                            <SettingsAdaptivity AdaptivityMode="Off" />
                            <Items>
                                <%-- 팝업 검색 조건 --%>
                                <dx:LayoutGroup Caption="검색창" ColCount="2" GroupBoxDecoration="HeadingLine" ShowCaption="False">
                                    <GroupBoxStyle>
                                        <Caption Font-Bold="true" Font-Size="16" CssClass="groupCaption" />
                                    </GroupBoxStyle>
                                    <Items>
                                        <%-- 직원명(텍스트박스) --%>
                                        <dx:LayoutItem>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="us_name" Caption="직원명" NullText="직원명" AutoCompleteType="Disabled">
                                                        <ValidationSettings Display="Dynamic" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <%-- 연락처(텍스트박스) --%>
                                        <dx:LayoutItem>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="us_phone" Caption="연락처" NullText="연락처" AutoCompleteType="Disabled" MaxLength="9">
                                                        <ValidationSettings Display="Dynamic" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <%-- ID(텍스트박스) --%>
                                        <dx:LayoutItem>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="us_id" Caption="ID " NullText="ID" AutoCompleteType="Disabled" MaxLength="10">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <%-- 직급(콤보박스) --%>
                                        <dx:LayoutItem>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="us_job" Caption="직급 " TextField="" ClientInstanceName="cbDivTxt" SelectInputTextOnClick="true" ButtonStyle-CssClass="divisonBtn">
                                                        <Items>
                                                            <dx:ListEditItem Text="책임연구원" Value="1" />
                                                            <dx:ListEditItem Text="선임연구원" Value="2" />
                                                            <dx:ListEditItem Text="연구원" Value="3" />
                                                        </Items>
                                                        <DropDownButton Image-Url="~/Content/icon/icon_dropdown.svg" Image-Width="8px" Image-Height="12px"></DropDownButton>
                                                        <ButtonStyle BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="2px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1" Width="8" CssClass="btnstCombobox" BackColor="#FFFFFF"></ButtonStyle>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <%-- 소속(콤보박스) --%>
                                        <dx:LayoutItem>
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox runat="server" ID="us_depart" Caption="소속" TextField="" ClientInstanceName="cbDivTxt" SelectInputTextOnClick="true" ButtonStyle-CssClass="divisonBtn">
                                                        <Items>
                                                            <dx:ListEditItem Text="ICT연구소" Value="1" />
                                                            <dx:ListEditItem Text="Seal 사업부" Value="2" />
                                                            <dx:ListEditItem Text="기술 연구소" Value="3" />
                                                        </Items>
                                                        <DropDownButton Image-Url="~/Content/icon/icon_dropdown.svg" Image-Width="8px" Image-Height="12px"></DropDownButton>
                                                        <ButtonStyle BorderLeft-BorderColor="#B3B3B3" Paddings-Padding="2px" BorderLeft-BorderStyle="Solid" BorderLeft-BorderWidth="1" Width="8" CssClass="btnstCombobox" BackColor="#FFFFFF"></ButtonStyle>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <%-- 검색 버튼 --%>
                                        <dx:LayoutItem Caption="검색창" ShowCaption="False" HorizontalAlign="Right" VerticalAlign="Middle">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButton ID="popupSearchInnerBtn" ClientInstanceName="popupSearchInnerBtn" runat="server" UseSubmitBehavior="true" BackgroundImage-ImageUrl="~/Content/icon/button_search_200px.svg" BackgroundImage-Repeat="NoRepeat" OnClick="OnPopupSearchBtnClick" Height="21px" Width="200px" ClientSideEvents-Click="function (s, e) { toolbarSearchPopup.Hide(); }">
                                                    </dx:ASPxButton>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <%-- 전체보기 버튼 --%>
            <div id="dGridHeader">
                <dx:ASPxButton runat="server" CssClass="btnFilterCancel2" ID="btnFilterCancel2" AutoPostBack="false" Image-Url="~/Content/icon/button_View_all.svg" Image-Width="75px" Image-Height="22px" ClientIDMode="Static" OnClick="OnFIlterClearBtnClick" BackColor="White" Border-BorderWidth="0" ForeColor="Black"></dx:ASPxButton>
                <%-- Toolbar -> 검색조건 및 수량 --%>
                <div runat="server" id="dLbSearchFilterText" class="dLbSearchFilterText">
                    <dx:ASPxLabel ID="filteredTextMain" runat="server" Text="검색조건 :" Visible="false" Font-Bold="true"></dx:ASPxLabel>
                    <div id="filteredTextdiv" style="display: inline-block;">
                        <dx:ASPxLabel ID="filteredText" runat="server" Text="" Visible="false" AllowEllipsisInText="true" ToolTip="">
                        </dx:ASPxLabel>
                    </div>
                    <div style="display: inline-block;">
                        <dx:ASPxLabel ID="filterLine" CssClass="lbFilterLine" runat="server" BorderRight-BorderStyle="Solid" BorderRight-BorderWidth="1px" Visible="false" BorderRight-BorderColor="#B3B3B3"></dx:ASPxLabel>
                        <dx:ASPxLabel ID="SmryManagement" CssClass="SmryManagement" runat="server" Text="" Visible="true"></dx:ASPxLabel>
                    </div>
                </div>
            </div>
            <%-- 그리드뷰 레이아웃 --%>
            <div class="dGridviewLayout" style="width: 868px;">
                <%-- 그리드뷰 생략된 글자 Hint(ellipsis - Tooltip) --%>
                <dx:ASPxHint ID="ASPxHint2" runat="server" TargetSelector="dx-ellipsis"></dx:ASPxHint>
                <%-- 특허관리 관련 그리드뷰 --%>
                <dx:ASPxGridView ID="gdPatentManage" runat="server" ClientInstanceName="gdGridview" SettingsExport-EnableClientSideExportAPI="true" AutoGenerateColumns="False"
                    KeyFieldName="us_seq" CssClass="gdGridview" Width="100%" ><%--OnCustomUnboundColumnData="gdPatentManage_CustomUnboundColumnData"--%>
                    <%-- 그리드뷰 커스텀 --%>
                    <StylesExport Cell-Font-Names="Malgun Gothic"></StylesExport>
                    <SettingsExport EnableClientSideExportAPI="true" Landscape="true" ExcelExportMode="DataAware" PaperKind="A3"></SettingsExport>
                    <%-- 문서 Export 권한 --%>
                    <SettingsResizing ColumnResizeMode="Control" Visualization="Postponed" />
                    <%--그리드뷰 크기 변경 권한 설정--%>
                    <SettingsBehavior AllowEllipsisInText="true" AllowGroup="true" AllowSort="true" AllowFocusedRow="true" AllowSelectSingleRowOnly="true" />
                    <%-- 권한 설정 --%>
                    <Settings VerticalScrollableHeight="450" VerticalScrollBarMode="Auto" HorizontalScrollBarMode="Auto" ShowHeaderFilterButton="false" GridLines="Vertical" ShowFilterBar="Hidden" />
                    <%-- 기타 설정 --%>
                    <SettingsFilterControl ViewMode="VisualAndText"></SettingsFilterControl>
                    <%--필터 설정--%>
                    <%-- Pager 설정 >> footer --%>
                    <SettingsPager NumericButtonCount="10" Mode="ShowPager" Position="Bottom" PageSize="15" AlwaysShowPager="true" FirstPageButton-Visible="true" LastPageButton-Visible="true" NextPageButton-Visible="true" PrevPageButton-Visible="true" ShowDisabledButtons="true" ShowNumericButtons="true" Summary-Visible="false">
                        <FirstPageButton Image-Url="/Content/icon/icon_keyboard_double_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_left_gray.svg" Image-Width="15px"></FirstPageButton>
                        <PrevPageButton Image-Url="/Content/icon/icon_keyboard_arrow_left_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_left_gray.svg" Image-Width="15px"></PrevPageButton>
                        <NextPageButton Image-Url="/Content/icon/icon_keyboard_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_arrow_right_gray.svg" Image-Width="15px"></NextPageButton>
                        <LastPageButton Image-Url="/Content/icon/icon_keyboard_double_arrow_right_blue.svg" Image-UrlDisabled="/Content/icon/icon_keyboard_double_arrow_right_gray.svg" Image-Width="15px"></LastPageButton>
                        <Summary Text="총 {2}건" Position="Left" />
                    </SettingsPager>
                    <%-- Pager Style --%>
                    <StylesPager Button-HorizontalAlign="Center">
                        <CurrentPageNumber BackColor="White" ForeColor="#0071bc" Font-Bold="true" Font-Underline="false" Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px"></CurrentPageNumber>
                        <PageNumber Paddings-PaddingLeft="5px" Paddings-PaddingRight="5px" Paddings-PaddingTop="5px"></PageNumber>
                        <Button Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px" Paddings-PaddingTop="2.5px" Paddings-PaddingBottom="0px"></Button>
                        <DisabledButton Paddings-PaddingLeft="2px" Paddings-PaddingRight="2px"></DisabledButton>
                        <Summary Paddings-PaddingLeft="0px" Paddings-PaddingTop="5px" HorizontalAlign="Center"></Summary>
                    </StylesPager>
                    <%-- GridView Style --%>
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
                    <%-- 전체 보기/초기화 버튼 --%>
                    <%-- 전체 보기/초기화 버튼 --%>
                    <SettingsContextMenu Enabled="true" EnableRowMenu="False" EnableFooterMenu="False" ColumnMenuItemVisibility-ShowSearchPanel="false" ColumnMenuItemVisibility-ShowFilterRow="false" ColumnMenuItemVisibility-ShowFilterEditor="false" ColumnMenuItemVisibility-ShowFooter="false">
                        <RowMenuItemVisibility ExportMenu-Visible="true">
                            <GroupSummaryMenu SummaryAverage="false" SummaryMax="false" SummaryMin="false" SummarySum="false" />
                        </RowMenuItemVisibility>
                    </SettingsContextMenu>
                    <SettingsText ContextMenuSortAscending="오름차순 정렬" ContextMenuSortDescending="내림차순 정렬" ContextMenuClearSorting="정렬 해제" ContextMenuGroupByColumn="이 열로 그룹열기" ContextMenuShowGroupPanel="그룹패널 열기" ContextMenuClearFilter="필터 해제" />
                    <%-- 열 --%>
                    <Columns>
                        <%--<dx:GridViewDataTextColumn FieldName="번호" VisibleIndex="0" Width="40px" UnboundType="String"></dx:GridViewDataTextColumn>--%>  
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" SortIndex="0" Visible="false" Width="40px" SortOrder="None" MinWidth="60" CellStyle-HorizontalAlign="Right" Settings-SortMode="Custom" FieldName="us_seq" Caption="번호" VisibleIndex="0" ReadOnly="True"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_name" Settings-AllowHeaderFilter="False" Caption="직원명" MinWidth="70" VisibleIndex="1" CellStyle-HorizontalAlign="Left" CellRowSpan="1"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_phone" Settings-AllowHeaderFilter="True" Caption="연락처" MinWidth="90" VisibleIndex="2" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_id" Settings-AllowHeaderFilter="False" Caption="ID" MinWidth="95" VisibleIndex="3" CellStyle-HorizontalAlign="Left" PropertiesTextEdit-DisplayFormatString="{0:yyyy/MM/dd}"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_job" Settings-AllowHeaderFilter="False" Caption="직급" MinWidth="95" VisibleIndex="4" CellStyle-HorizontalAlign="Left" UnboundType="DateTime" PropertiesTextEdit-DisplayFormatString="{0:yyyy/MM/dd}"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="us_depart" Settings-AllowHeaderFilter="True" Caption="소속" MinWidth="105" VisibleIndex="5" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="regUserId" Settings-AllowHeaderFilter="False" Caption="등록자" MinWidth="70" VisibleIndex="8" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="regDate" Settings-AllowHeaderFilter="False" Caption="등록일시" MinWidth="180" VisibleIndex="9" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modUserId" Settings-AllowHeaderFilter="False" Caption="수정자" MinWidth="70" VisibleIndex="10" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Settings-AllowSort="True" FieldName="modDate" Settings-AllowHeaderFilter="False" Caption="수정일시" MinWidth="180" VisibleIndex="11" CellStyle-HorizontalAlign="Left"></dx:GridViewDataTextColumn>
                    </Columns>
                    <%-- 그리드뷰 내부 ToolTip --%>
                    <ClientSideEvents EndCallback="function(s,e){
                        ASPxClientHint.Update();
                        }" />
                </dx:ASPxGridView>

                <%-- 입력한 페이지로 이동하는 pager (JS->C#) --%>
                <div runat="server" id="dPagernum" class="dPagernum">
                    <dx:ASPxTextBox Paddings-Padding="2px" runat="server" ID="tbPagerinput" Width="26" Font-Size="9pt" Style="display: inline-block; vertical-align: middle;" AutoCompleteType="Disabled">
                    </dx:ASPxTextBox>
                    <dx:ASPxLabel runat="server" ID="spPagecount" Style="vertical-align: baseline;"></dx:ASPxLabel>
                    <dx:ASPxButton runat="server" ID="aSubmitpager2" CssClass="aSubmitpager" BackgroundImage-ImageUrl="/Content/icon/icon_expand_circle_right_blue.svg" OnClick="OnSubmitPagerBtnClick" ClientIDMode="Static" BackColor="#ffffff"></dx:ASPxButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
