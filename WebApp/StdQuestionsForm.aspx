<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StdQuestionsForm.aspx.cs" Inherits="StdQuestionsForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="DataList1" runat="server">
                        <FooterTemplate>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                        </FooterTemplate>
                        <HeaderTemplate>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Answer the following questions:"></asp:Label>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("QuestionID") %>'></asp:Label>
                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Question") %>'></asp:Label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            </asp:RadioButtonList>
                            <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                            </asp:CheckBoxList>
                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            <br />
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("AnswerType") %>' />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                </td>
            </tr>
        </table>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>
