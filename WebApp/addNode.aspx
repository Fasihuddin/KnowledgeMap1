<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addNode.aspx.cs" Inherits="addNode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style4
        {
        }
        .auto-style5
        {
            width: 171px;
            height: 23px;
        }
        .auto-style1
        {
            height: 23px;
            width: 378px;
        }
        .auto-style14
        {
            width: 481px;
        }
        .auto-style15
        {
            width: 378px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1>Add New Modules</h1>
        <p>Add new module in this form. Please note that maximum length of module name is 30 characters.</p>
        <table class="auto-style14">
            <tr>
                <td class="auto-style4">Module Name: </td>
                <td class="auto-style15">
                    <asp:TextBox ID="txtNode" runat="server" Width="322px" OnTextChanged="txtNode_TextChanged" MaxLength="30"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNode" ErrorMessage="* Node name is required" ForeColor="Red" ValidationGroup="NodeVal"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Description: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtNodeDesc" runat="server" Height="76px" TextMode="MultiLine" Width="328px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td class="auto-style15">
                    <asp:Button ID="btnAddNewNode" runat="server" Text="Add Node" OnClick="btnAddNewNode_Click" ValidationGroup="NodeVal" />
                &nbsp;
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Finish Adding Nodes" Width="141px" />
                </td>
            </tr>
            <tr>
                <td class="auto-style4" colspan="2">
                    <asp:Label ID="lblMessage" runat="server" Font-Bold="True" Font-Italic="True" Text="* A new node has been added. Please add other nodes , otherwise press &quot;Finish Adding Nodes&quot; button." Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
