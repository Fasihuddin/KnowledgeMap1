<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addTopic.aspx.cs" Inherits="addTopic" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style2
        {
            height: 23px;
        }
        .auto-style3
        {
            width: 81px;
        }
        .auto-style13
        {
            height: 23px;
            width: 396px;
        }
        .auto-style14
        {
            width: 396px;
        }
        .auto-style15
        {
            width: 37px;
        }
        .auto-style16
        {
            width: 288px;
        }
        .auto-style17
        {
            width: 403px;
        }
        .auto-style18
        {
            text-decoration: underline;
        }
        .auto-style19
        {
            width: 288px;
            height: 23px;
        }
        .auto-style20
        {
            width: 37px;
            height: 23px;
        }
        .auto-style21
        {
            width: 403px;
            height: 23px;
        }
        .auto-style22
        {
            height: 23px;
            width: 81px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1 style="height: 33px">Add New Topic<br />
        </h1>
            <table style="width: 89%;">
                <tr>
                    <td class="auto-style22">
                        <asp:Label ID="lblName" runat="server" Text="Topic Name:"></asp:Label>
                    </td>
                    <td class="auto-style13">
                        <asp:TextBox ID="txtTopic" runat="server" Width="324px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style3">
                        <asp:Label ID="Label1" runat="server" Text="Description:"></asp:Label>
                    </td>
                    <td class="auto-style14">
                        <asp:TextBox ID="txtDescription" runat="server" Height="76px" TextMode="MultiLine" Width="328px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22"></td>
                    <td class="auto-style13">
                        <asp:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Create Topic" />
                    </td>
                </tr>
                <tr>
                    <td class="auto-style22">&nbsp;</td>
                    <td class="auto-style13">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style2" colspan="2">Please assign modules to the newly created topic. Steps are follow:<br />
                        1. Create new modules for the topic by pressing <em><strong>Add New Modules</strong></em> button.<br />
                        2. Press <strong><em>Refresh Modules</em></strong> button in the selection texbox to include the newly-created modules.<br />
                        3. Once all new modules are created, assign modules to the topic. You may also assign the existing modules from other course/topics to your newly created topic.<br />
                        4. Once modules have been assigned to topic, Press <strong><em>Confirm Modules Assignment</em></strong> button and then Press <strong><em>Show Knowledge Map</em></strong> button to preview the knowledge map for this topic.<br />
                    <br />
                    * Note: Press<em><strong> Refresh Modules</strong></em> button to add the new modules into the selection table<br />
                    </td>
                </tr>
            </table>

        <h3>
            <asp:Button ID="btnAddNodes" runat="server" OnClick="btnAddNodes_Click" Text="Add New Modules" />
&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnRefresh" runat="server" Text="Refresh Modules" />
        </h3>
        <h3 class="auto-style18">Assign Modules to Topic</h3>
        <table style="width: 84%;">
            <tr>
                <td class="auto-style19"><strong>Modules Selection Table:</strong></td>
                <td class="auto-style20"></td>
                <td class="auto-style21"><strong>Assigned Modules:</strong></td>
            </tr>
            <tr>
                <td class="auto-style16">
                    <asp:ListBox ID="ListBox1" runat="server" Height="167px" Width="281px"></asp:ListBox>
                </td>
                <td class="auto-style15">
                    <asp:Button ID="btnRight" runat="server" Text="&gt;&gt;" />
                </td>
                <td class="auto-style17">
                    <asp:ListBox ID="ListBox2" runat="server" Height="167px" Width="309px"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style16">&nbsp;</td>
                <td class="auto-style15">&nbsp;</td>
                <td class="auto-style17">
                    <asp:Button ID="btnConfirmAssignment" runat="server" OnClick="Button1_Click" Text="Confirm Modules Assignment" Width="193px" />
                </td>
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
