<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StdNodePage.aspx.cs" Inherits="startTest" %>

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
    
        <h1>Node Page</h1>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td><span class="auto-style1"><strong>Node Materials</strong></span><br />
                    <br />
                    &lt;&lt; Links for the node materials here &gt;&gt;<br />
                    <br />
                    <asp:GridView ID="grdLinks2" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                 
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name" />
                            <asp:TemplateField HeaderText="Links">
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hyperCtl" runat="server" NavigateUrl='<%# Eval("URL_Address") %>' Text='<%# Eval("URL_Address") %>'></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                 
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                </td>
                <td><span class="auto-style1"><strong>Start Test</strong></span><br />
                    <br />
                    Your computer-generated Test ID is:&nbsp; <asp:Label ID="lblTestId" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnStartTest" runat="server" OnClick="btnStartTest_Click" Text="Start Test" Enabled="False" />
                </td>
            </tr>
        </table>
    
    </div>
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back to Topic Map" />
    </form>
</body>
</html>
