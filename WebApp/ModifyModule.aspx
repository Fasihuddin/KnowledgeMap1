<%@ Page Title="" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="ModifyModule.aspx.cs" Inherits="ModifyModule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        Module update</h3>
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
    <asp:Label ID="lblMessage" runat="server" Font-Italic="True" ForeColor="Red" Text="* Module information has been updated." Visible="False"></asp:Label>
    <br />
    <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            &nbsp;<table style="width: 100%;">
                <tr>
                    <td style="width: 160px">Module Name: </td>
                    <td style="width: 271px">
                        <asp:TextBox ID="txtNode" runat="server" MaxLength="30" Width="343px"></asp:TextBox>
                    </td>
                    <td><strong>Edit Material Links:</strong></td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 160px">Description: </td>
                    <td style="width: 271px">
                        <asp:TextBox ID="txtNodeDesc" runat="server" Height="124px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                    </td>
                    <td rowspan="2" style="vertical-align:text-top">
                        <asp:GridView ID="GridView1" runat="server" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Material_Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
                                <asp:BoundField DataField="Material_Id" HeaderText="Material_Id" ReadOnly="True" SortExpression="Material_Id" />
                                <asp:BoundField DataField="URL_Address" HeaderText="URL_Address" SortExpression="URL_Address" />
                                <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>" DeleteCommand="DELETE FROM [Materials] WHERE [Material_Id] = @Material_Id" InsertCommand="INSERT INTO [Materials] ([Material_Id], [URL_Address], [Name]) VALUES (@Material_Id, @URL_Address, @Name)" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [Material_Id], [URL_Address], [Name] FROM [Materials] WHERE ([Node] = @Node)" UpdateCommand="UPDATE [Materials] SET [URL_Address] = @URL_Address, [Name] = @Name WHERE [Material_Id] = @Material_Id">
                            <DeleteParameters>
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                            </DeleteParameters>
                            <InsertParameters>
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                                <asp:Parameter Name="URL_Address" Type="String" />
                                <asp:Parameter Name="Name" Type="String" />
                            </InsertParameters>
                            <SelectParameters>
                                <asp:ControlParameter ControlID="ddlModule" Name="Node" PropertyName="SelectedValue" Type="Int32" />
                            </SelectParameters>
                            <UpdateParameters>
                                <asp:Parameter Name="URL_Address" Type="String" />
                                <asp:Parameter Name="Name" Type="String" />
                                <asp:Parameter Name="Material_Id" Type="Int32" />
                            </UpdateParameters>
                        </asp:SqlDataSource>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 160px">Material Links:<br /> <em>(One link per line)<br /> Format: name;link<br />
                        <br />
                        For Example:<br /> Search engine;http://www.google.com</em></td>
                    <td style="width: 271px">
                        <asp:TextBox ID="txtLinks" runat="server" Height="146px" TextMode="MultiLine" Width="348px"></asp:TextBox>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 160px">&nbsp;</td>
                    <td style="width: 271px">
                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" Width="125px" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            
        </asp:Panel>

    <div style="float:right;">


    </div>
        </asp:Content>

