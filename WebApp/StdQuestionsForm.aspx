<%@ Page Title="Test" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StdQuestionsForm.aspx.cs" Inherits="StdQuestionsForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>Questions To Answer:<br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="DataList1" width="80%" runat="server" DataSourceID="SqlDataSource" 
                        CellPadding="4" onItemDataBound="DataList1_ItemDataBound" ForeColor="#333333">
                        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                        <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                        <FooterTemplate>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </FooterTemplate>
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Answer the following questions:"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <ItemTemplate>
                            <asp:Image ID="imgCanvas" runat="server" Width="50%" /><br />
                            <asp:Label ID="lblQuestionOrder" runat="server" Text='<%# Eval("Question_order") %>'></asp:Label>
                            .&nbsp;
                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            </asp:RadioButtonList>
                            <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("Question_Id") %>' Visible="False"></asp:Label>
                            &nbsp;
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblThanks" runat="server"></asp:Label>
        <br />
        <br />
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                 SelectCommand="SELECT Test_questions.Question_order, Text, Test_questions.Question_Id
                                FROM Test_questions JOIN Questions ON Test_questions.Question_Id = Questions.Question_Id WHERE (Test_questions.Test_Id = 12) ORDER BY Test_questions.Question_order">
         
            <SelectParameters>
                <asp:QueryStringParameter Name="TestID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </asp:Content>
