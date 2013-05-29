<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testResult.aspx.cs" Inherits="testResult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h3 style="text-align: center">Test result</h3>
    <form id="form1" runat="server">
    <div>
    
        <asp:LoginName ID="LoginName1" runat="server" />
    
    </div>
        <p>
            current user name is
            <asp:Label ID="testLbl" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            Test Id :<asp:Label ID="testIdLabel" runat="server"></asp:Label>
        </p>
        <p>
            Test Date/Time:
            <asp:Label ID="DateTimeLabel" runat="server"></asp:Label>
        </p>
        <p>
            Node Id:<asp:Label ID="NodeIdLabel" runat="server"></asp:Label>
        </p>
        <p>
            <asp:Label ID="LblErrMsg" runat="server"></asp:Label>
        </p>
        <p>
            your score is :
            <asp:Label ID="ResuleLabel" runat="server"></asp:Label>
        </p>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        </asp:GridView>
        <br />
    </form>
</body>
</html>
