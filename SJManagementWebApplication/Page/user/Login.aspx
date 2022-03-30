<%-- 로그인페이지(HTML) --%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SJManagementWebApplication.Login" %>
<%@ Register TagPrefix="UC" TagName="Popup" Src="../../code/PopupWebUserControl.ascx" %>
<%-- 단독페이지 --%>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<%-- 헤더 --%>
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
    <link rel="stylesheet" type="text/css" href="~/Content/Layout.css" /> <%-- css파일 불러오기 --%>
    <title></title>
</head>
<%-- 배경 --%>
<body class="bdLogin">
    <%-- 배경 비디오 --%>
    <div class="dLogin-videoback">
        <video autoplay="autoplay" loop="loop" muted="muted" class="vidLogin">
                <source src="/Content/sjtech.mp4" type="video/mp4"/>
        </video>
    </div>
    <%-- 로그인 폼 --%>
    <section class="fmLogin">
        <%-- SJ테크 로고 --%>
        <div class="dLogin-logo">
            <img id="imgSjtech" src="../../Content/sjtech.png" class="imgSjtech"/>        
        </div>
            <form runat="server">
                <%--팝업--%>
                <UC:Popup ID="pcPopup" runat="server" />
                <%-- 타이틀 --%>
                <label class="lbLogintitle" style="font-size: 35px;">LOGIN</label>     
                <div>
                    <%-- ID --%>
                    <label>USER ID</label>
                    <div class="dLoginarea">
                        <input type="text" id="inTrialUserId" placeholder="아이디" runat="server"/>
                    </div>
                    <%-- 비밀번호 --%>
                    <label>PASSWORD</label>
                    <div class="dLoginarea">
                        <input type="password" id="inTrialUserPassword" placeholder="비밀번호" runat="server"/>
                    </div>
                    <%-- 로그인 버튼 --%>
                    <div class="dBtnarea">
                        <dx:ASPxButton ID="btnLogin" runat="server" Text="Login" OnClick="loginBtn_Click" Width="100%" Height="60px" Font-Size="Large" CssClass="btnLogin"></dx:ASPxButton>
                    </div>
                    <%-- 하단 링크 --%>
                    <div style="width:100%; margin-top:10px;">
                        <%-- 회원가입 링크 --%>
                        <div class="dCaption" style="float:left">
                            <a href="joinMembership.aspx" style="font-size:18px;">회원가입</a>
                        </div>
                        <%-- 비밀번호 찾기 링크 --%>
                        <div class="dCaption" style="float:right">
                            <a href="FrmSendEmailWithMicrosoftEmail.aspx" style="font-size:18px;">Forgot Password?</a>
                        </div>
                    </div>
                </div>
            </form>
        </section>
    </body>
    </html>
