<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchPopupWebUserControl.ascx.cs" Inherits="SJManagementWebApplication.Code.SearchPopupWebUserControl" %>


            <dx:ASPxPopupControl ID="toolbarSearchPopup" ClientInstanceName="toolbarSearchPopup" Width="600px" Height="110px" CloseAction="CloseButton" MaxWidth="900px" MaxHeight="300px"
                AllowDragging="true" AllowResize="true" ShowFooter="false" PopupElementID="ShowButtonSearch" PopupAnimationType="Slide" Modal="true" ShowHeader="true" ShowShadow="true"
                runat="server" ShowOnPageLoad="false" PopupHorizontalAlign="Center" PopupVerticalAlign="Below" EnableHierarchyRecreation="true" HeaderText="검색창" >
                <ContentCollection>
                    <dx:PopupControlContentControl ID="searchPopup" runat="server">
                        <dx:ASPxFormLayout runat="server" ID="popupFormLayout">
                            <SettingsAdaptivity AdaptivityMode="Off" SwitchToSingleColumnAtWindowInnerWidth="576" />
                            <Items>
                                <dx:LayoutGroup Caption="검색창" ColCount="2" GroupBoxDecoration="HeadingLine" Paddings-Padding="0" Paddings-PaddingTop="10" ShowCaption="False">
                                    <GroupBoxStyle>
                                        <Caption Font-Bold="true" Font-Size="16" CssClass="groupCaption" />
                                    </GroupBoxStyle>
                                    <Items>
                                        <dx:LayoutItem Caption="특허명">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="ip_name" Text=" " >
                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="발명자">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="ip_inventor" Text=" " >
                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="종류">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="ip_division" Text=" " >
                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="출원일자">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit runat="server" ID="ip_fillDate" Date=" ">
                                                        <ValidationSettings RequiredField-IsRequired="true" Display="Dynamic" />
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="특허번호">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="ip_num" Text="">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Empty" ShowCaption="False">
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="출원번호">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="ip_appnum" Text=" ">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="검색창" ShowCaption="False" HorizontalAlign="Right">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButton runat="server" BackgroundImage-ImageUrl="~/Content/Images/button_검색.svg" BackgroundImage-Repeat="NoRepeat" OnClick="SearchFromDB">
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
                <%-- Footer 부분 >> 주석처리 --%>
                <%--<FooterContentTemplate>
                    <div style="display: table; margin: 6px 6px 6px auto; visibility:hidden;">
                        <dx:ASPxButton ID="UpdateButton" runat="server" Text="Update Content" AutoPostBack="False" 
                            ClientSideEvents-Click="function(s, e) { toolbarSearchPopup.PerformCallback(); }"></dx:ASPxButton>
                    </div>
                </FooterContentTemplate>--%>
            </dx:ASPxPopupControl>