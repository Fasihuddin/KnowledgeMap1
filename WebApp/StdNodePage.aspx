<%@ Page Title="Module Page" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StdNodePage.aspx.cs" Inherits="startTest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   

    <style type="text/css">
        .auto-style1
        {
            text-decoration: underline;
        }
        .auto-style2
        {
            width: 503px;
        }
        .auto-style3 {
            font-size: medium;
        }
    </style>

   
    <div>
    
        <h3>Module Learning Materials</h3>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style2"><strong><span class="auto-style3">Module</span>:
                    <asp:Label ID="NodeLbl" runat="server" Font-Size="Small" Font-Italic="True"></asp:Label>
                    </strong><br />
                    <br />
                    &lt;&lt; Following are some links for different learning materials &gt;&gt;<br />
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
                                    <asp:HyperLink ID="hyperCtl" runat="server" NavigateUrl='<%# Eval("URL_Address") %>' Text='<%# Eval("URL_Address") %>' Target="_blank"></asp:HyperLink>
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

                    <br /><asp:Button ID="Back" runat="server" Text="Back to knowledge map" OnClientClick="JavaScript:window.history.back(1);return false;" />
                </td>
                
                <td><span class="auto-style1"><strong>Start Test</strong></span><br />
                    <br />
                    Computer-generated Test ID is:&nbsp; <asp:Label ID="lblTestId" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <asp:Button ID="btnStartTest" runat="server" OnClick="btnStartTest_Click" Text="Start Test" Enabled="False" />
                </td>
            </tr>
        </table>
    
    </div>
    </asp:Content>
