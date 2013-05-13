<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addTopic.aspx.cs" Inherits="addTopic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
        .auto-style2
        {
            height: 23px;
            width: 229px;
        }
        .auto-style3
        {
            width: 229px;
        }
        .auto-style4
        {
            width: 104px;
        }
        .auto-style5
        {
            width: 104px;
            height: 23px;
        }
        .auto-style8
        {
            width: 172px;
        }
        .auto-style11
        {
            width: 17px;
        }
        .auto-style12
        {
            height: 23px;
            width: 17px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1 style="height: 33px">Add Topic<br />
        </h1>
            <table style="width: 53%;">
                <tr>
                    <td class="auto-style2">
                        <asp:Label ID="lblName" runat="server" Text="Topic Name:"></asp:Label>
                    </td>
                    <td class="auto-style1">
                        <asp:TextBox ID="txtTopic" runat="server" Width="319px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="Description:"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDescription" runat="server" Height="76px" TextMode="MultiLine" Width="328px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style2"></td>
                    <td class="auto-style1">
                        <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Create" />
                    </td>
                </tr>
            </table>

        <h3>Add New Node</h3>
        <table style="width: 101%;">
            <tr>
                <td class="auto-style4">Node Name: </td>
                <td>
                    <asp:TextBox ID="txtNode" runat="server" Width="322px"></asp:TextBox>
                </td>
                <td class="auto-style11">&nbsp;</td>
                <td class="auto-style8" rowspan="4">
                    <asp:GridView ID="grdNodes" runat="server">
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">Description: </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtNodeDesc" runat="server" Height="76px" TextMode="MultiLine" Width="328px"></asp:TextBox>
                </td>
                <td class="auto-style12">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">Pre-requisites nodes:</td>
                <td>
                    <asp:TextBox ID="txtPrereq" runat="server" Height="76px" TextMode="MultiLine" Width="328px"></asp:TextBox>
                </td>
                <td class="auto-style11">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="btnAddNewNode" runat="server" Text="Add Node" />
                </td>
                <td class="auto-style11">&nbsp;</td>
            </tr>
        </table>
        <br />
        <br />
        <asp:Button ID="btnShowMap" runat="server" OnClick="btnShowMap_Click" Text="Show Knowledge Map" />
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
