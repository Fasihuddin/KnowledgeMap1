<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h2>
        <br />
        Oops... Seems there is an error</h2>
    <form id="form1" runat="server">
    <div>
    
        Error Message:
        <asp:Label ID="lblError" runat="server" Text="Label"></asp:Label>
    
    </div>
    </form>
</body>
</html>
