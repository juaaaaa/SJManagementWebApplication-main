<%-- 회원가입페이지(HTML) --%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="joinMembership.aspx.cs" Inherits="SJManagementWebApplication.joinMembership" %>
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

<body class="bdSign">
    <%-- 가입 폼 --%>
    <section class="fmSign">
        <%-- 타이틀 --%>
        <h1>JOIN</h1>
        <form runat="server">
            <%--팝업--%>
            <UC:Popup ID="pcPopup" runat="server" />
            <%-- 이름 --%>
            <label>USER NAME</label>
            <div class="dSignarea">
                <input id="inUserName" runat="server"/>
            </div>
            <%-- 닉네임 --%>
            <label>USER NICKNAME</label>
            <div class="dSignarea">
                <input id="inUserNickname" runat="server"/>
            </div>
            <%-- ID --%>
            <label>USER ID</label>
            <div class="dSignarea">
                <input id="inUserId" runat="server"/>
            </div>
            <%-- 비밀번호 --%>
            <label>PASSWORD</label>
            <div class="dSignarea" style="border: none; background: transparent;">
                <input id="inUserPw" runat="server"/>
            </div>
            <%-- 완료 버튼 --%>
            <div class="dBtnarea">
                <dx:ASPxButton ID="btnReg" runat="server" Text="가입완료" OnClick="regBtn_Click" Width="100%" Height="50px" Font-Size="16px"></dx:ASPxButton>
            </div>
        </form>
        <%-- 이전화면(Login) 링크 --%>
        <div class="dCaption" style="margin-top:10px;">
            <a href="Login.aspx">로그인 화면으로 돌아가기</a>
        </div>
    </section>
</body>
</html>
