<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CourseIntro.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Medium" Text="Select course"></asp:Label>
&nbsp;<asp:ListBox ID="ListCourse" runat="server" style="margin-top: 4px" Width="250px" Font-Size="Medium" Rows="1"></asp:ListBox>
        <br />
        <br />
        <asp:Label ID="LblResults" runat="server"></asp:Label>
        <br />
    
    </div>

    </form>
</body>
</html>
