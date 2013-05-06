<%@ Page Language="C#" AutoEventWireup="true" CodeFile="startTest.aspx.cs" Inherits="startTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Start Test Page<br />
        <br />
        <table style="width:100%;">
            <tr>
                <td><span class="auto-style1"><strong>Node Materials</strong></span><br />
                    <br />
                    &lt;&lt; Links for the node materials here &gt;&gt;</td>
                <td><span class="auto-style1"><strong>Start Test<br />
                    </strong></span>
                    <br />
                    Please select the node: <asp:DropDownList ID="ddlNodeList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNodeList_SelectedIndexChanged">
                    </asp:DropDownList>
                    <br />
                    <br />
                    Your computer generated Test ID is:&nbsp; <asp:Label ID="lblTestId" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnStartTest" runat="server" OnClick="btnStartTest_Click" Text="Start Test" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
