<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addNodesPrereq.aspx.cs" Inherits="addNodesPrereq" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1><strong>Add Nodes Prerequisites</strong></h1>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="NodeId" HeaderText="ID" />
                <asp:BoundField DataField="Name" HeaderText="Name" />
                <asp:ButtonField ButtonType="Button" Text="AddPrerequisites" />
            </Columns>
        </asp:GridView>
    
    </div>
    </form>
</body>
</html>
