<%@ Page Language="C#" AutoEventWireup="true" CodeFile="index.aspx.cs" Inherits="index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Hello World!!<br />
        <br />
        <asp:Button ID="btnTest" runat="server" OnClick="btnTest_Click" Text="Button" />
        <br />
        <br />
        <asp:TextBox ID="txtResult" runat="server"></asp:TextBox>
        </div>
    </form>
</body>
</html>
