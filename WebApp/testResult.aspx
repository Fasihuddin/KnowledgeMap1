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
            <asp:Label ID="ResultLabel" runat="server"></asp:Label>
        </p>
        <br />
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
    </form>
</body>
</html>
