<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopupWebUserControl.ascx.cs" Inherits="SJManagementWebApplication.Code.PopupWebUserControl_2" %>

<dx:ASPxPopupControl ID="pcPopup" runat="server" Width="500" CloseAction="CloseButton" CloseOnEscape="true" Modal="True"
    ClientInstanceName="pcPopup" CssClass="popup" HeaderImage-Url="~/Content/icon/main_logo.svg" HeaderImage-Height="25" CloseButtonImage-Url="~/Content/icon/button_close_obj.svg"
    HeaderText="" HeaderStyle-Paddings-Padding="0" HeaderStyle-Border-BorderStyle="None" HeaderStyle-CssClass="popupheader" PopupAnimationType="None" EnableViewState="False" AutoUpdatePosition="true" ShowCloseButton="true" CloseButtonStyle-CssClass="popupBtn" ShowHeader="true">
    <ContentStyle>
        <Border />
    </ContentStyle>
    <ContentCollection>
        <dx:PopupControlContentControl runat="server">
            <dx:ASPxPanel ID="Panel1" runat="server" DefaultButton="btOK">
                <PanelCollection>
                    <dx:PanelContent runat="server">
                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" Width="100%" Height="100%">
                            <Items>
                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxLabel runat="server" CssClass="popupText" ID="lbPopup" ClientInstanceName="lbPopup" Text="test" Font-Size="12px"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                                <dx:LayoutItem ShowCaption="False" Paddings-PaddingTop="19">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer>
                                            <dx:ASPxButton ID="btnOk" ClientInstanceName="btnOk" runat="server" Text="확인" Width="80px" AutoPostBack="true" CssClass="okBtn" Style="float: right; margin-right: 8px;" OnClick="add_OkClickEvent">
                                                <ClientSideEvents Click="function(s, e) { pcPopup.Hide(); if(btnOk.GetText() == '삭제') alertsetting(3);  }" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="btnCancel" ClientInstanceName="btnCancel" runat="server" Text="취소" Width="80px" AutoPostBack="False" CssClass="cancelBtn" Style="float: right; margin-right: 8px;" OnClick="add_CancelClickEvent">
                                                <ClientSideEvents Click="function(s, e) { pcPopup.Hide(); }" />
                                            </dx:ASPxButton>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>
                            </Items>
                        </dx:ASPxFormLayout>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ContentStyle>
        <Paddings PaddingBottom="5px" />
    </ContentStyle>
</dx:ASPxPopupControl>
