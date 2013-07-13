<%@ Page Title="" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="ModifyModule.aspx.cs" Inherits="ModifyModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        Module Page</p>
    <p>
        &nbsp;</p>
        <table style="width: 56%;">
            <tr>
                <td class="auto-style2">Select Course:</td>
                <td class="auto-style4">
                    <asp:DropDownList ID="ddlCourse" runat="server" Height="25px" Width="352px" AutoPostBack="True" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">Select Topic:</td>
                <td class="auto-style7">
                    <asp:DropDownList ID="ddlTopic" OnSelectedIndexChanged="ddlTopic_SelectedIndexChanged" runat="server" Height="25px" Width="352px" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">Select Module:</td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlModule" runat="server" Height="26px" Width="352px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">
                    <asp:Button ID="btnModify" runat="server" Text="Modify Modules" Width="230px" OnClick="btnModify_Click" />
                </td>
            </tr>
        </table>
    <br />
    <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            &nbsp;<table style="width: 100%;">
                <tr>
                    <td style="width: 160px">Module Name: </td>
                    <td style="width: 376px">
                        <asp:TextBox ID="txtNode" runat="server" MaxLength="30" Width="343px"></asp:TextBox>
                    </td>
                    <td>Material Links:</td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 160px; height: 156px">Description: </td>
                    <td style="width: 376px; height: 156px">
                        <asp:TextBox ID="txtNodeDesc" runat="server" Height="124px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                    </td>
                    <td rowspan="2">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Material_Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="Material_Id" HeaderText="Material_Id" ReadOnly="True" SortExpression="Material_Id" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                                <asp:BoundField DataField="URL_Address" HeaderText="URL_Address" SortExpression="URL_Address" />
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KnowledgeMapConnectionString %>" DeleteCommand="DELETE FROM [Materials] WHERE [Material_Id] = @Material_Id" InsertCommand="INSERT INTO [Materials] ([Material_Id], [Name], [URL_Address]) VALUES (@Material_Id, @Name, @URL_Address)" SelectCommand="SELECT [Material_Id], [Name], [URL_Address] FROM [Materials] WHERE ([Node] = @Node)" UpdateCommand="UPDATE [Materials] SET [Name] = @Name, [URL_Address] = @URL_Address WHERE [Material_Id] = @Material_Id">
                            <DeleteParameters>
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                                <asp:Parameter Name="Name" Type="String" />
                                <asp:Parameter Name="URL_Address" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlModule" DefaultValue="0" Name="Node" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="Name" Type="String" />
                                <asp:Parameter Name="URL_Address" Type="String" />
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 73px">Material Links:<br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td style="width: 160px">&nbsp;</td>
                    <td style="width: 376px">
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="125px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>

    <div style="float:right;">


    </div>
        </asp:Content>

