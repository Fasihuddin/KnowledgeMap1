<%@ Page Title="Module Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StdNodePage.aspx.cs" Inherits="startTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   

    <style type="text/css">
        .auto-style1
        {
            text-decoration: underline;
        }
    </style>

   
    <div>
    
        <h3>Node Page</h3>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td><span class="auto-style1"><strong>Node Materials</strong></span><br />
                    <br />
                    &lt;&lt; Links for the node materials here &gt;&gt;<br />
                    <br />
                    <asp:GridView ID="grdLinks2" runat="server" CellPadding="4" GridLines="None" AutoGenerateColumns="False" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                 
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
                 
                        <EditRowStyle BackColor="#999999" />
                 
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
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
    </asp:Content>
