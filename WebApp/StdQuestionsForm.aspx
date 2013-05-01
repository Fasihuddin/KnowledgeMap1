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
                <td>Questions To Answer:<br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:DataList ID="DataList1" width="80%" runat="server" DataSourceID="SqlDataSource" onitemdatabound="DataList1_ItemDataBound" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" >
                        <AlternatingItemStyle BackColor="#DCDCDC" />
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <FooterTemplate>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </FooterTemplate>
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Answer the following questions:"></asp:Label>
                        </HeaderTemplate>
                        <ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <ItemTemplate>
                            <asp:Label ID="lblQuestionID" runat="server" Text='<%# Eval("Question_Id") %>'></asp:Label>
                            .&nbsp;
                            <asp:Label ID="lblQuestion" runat="server" Text='<%# Eval("Text") %>'></asp:Label>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            </asp:RadioButtonList>
                            &nbsp;
                        </ItemTemplate>
                        <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblThanks" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
    
    </div>
        <asp:SqlDataSource ID="SqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:connString %>"
                 SelectCommand="SELECT Questions.Question_Id, Text 
                                FROM Test_questions JOIN Questions ON Test_questions.Question_Id = Questions.Question_Id WHERE (Test_questions.Test_Id = @TestID)">
         
            <SelectParameters>
                <asp:QueryStringParameter Name="TestID" QueryStringField="id" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
    </form>
</body>
</html>
