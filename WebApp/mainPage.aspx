<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mainPage.aspx.cs" Inherits="loginSuccess" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <asp:LoginName ID="LoginName1" runat="server" FormatString="Welcome {0}" />

    <form id="form1" runat="server">
        <asp:LoginStatus ID="LoginStatus1" runat="server" LogoutPageUrl="~/logout.aspx" LogoutAction="Redirect" />
        <h2>The mainpage</h2>
        <div>
        </div>
    </form>
</body>
</html>
