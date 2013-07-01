<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addNodesPrereq.aspx.cs" Inherits="addNodesPrereq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1><strong>Add Prerequisite Modules</strong></h1>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" onRowCommand="GridView1_RowCommand">
            <Columns>
                <asp:BoundField DataField="NodeId" HeaderText="Module ID" />
                <asp:BoundField DataField="Name" HeaderText="Module Name" />
                <asp:BoundField HeaderText="Prerequisite Modules" />
                <asp:ButtonField ButtonType="Button" CommandName="btnAdd" Text="Add Prerequisite Modules" />
            </Columns>
        </asp:GridView>
    
        <br />
    
    </div>
        <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save Prerequisite Modules and Generate Knowledge Map" Width="377px" />
        <br />
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="343px" Visible="False">
            Please add all pre-requisite modules for module ID:
            <asp:Label ID="lblModNo" runat="server" Text="Label" Font-Bold="True" Font-Italic="True"></asp:Label>
            .
            <br />
            * Select all pre-requisite modules from list below (Press Ctrl + Mouse Click
            <br />
            &nbsp;&nbsp; for adding more than 1 module) and click &quot;Add&quot;.<br />
            <br />
            <asp:ListBox ID="lstModules" runat="server" Height="222px" SelectionMode="Multiple" Width="322px"></asp:ListBox>
            <br />
            <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Save Prerequisite Modules" />
            &nbsp;
        </asp:Panel>
    </form>
</body>
</html>
