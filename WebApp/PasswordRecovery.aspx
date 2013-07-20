<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PasswordRecovery.aspx.cs" Inherits="PasswordRecovery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Password Recovery</h3>
    <br/>
    <br />
    
    <table style="width: 100%">
        <tr>
            <td style="width: 6px">Username</td>
            <td> <asp:TextBox ID="UsernameTxt" runat="server"></asp:TextBox>
               &nbsp&nbsp<asp:Button ID="Button1" runat="server" Text="Next" Width="70px" OnClick="Button1_Click" />
            &nbsp;
                <asp:Label ID="noUserLbl" runat="server" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UsernameTxt" ErrorMessage="Username must be entered" ForeColor="Red"></asp:RequiredFieldValidator>
            &nbsp;
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="UsernameTxt" ErrorMessage="Please enter numbers only (eg. 313131)" Font-Size="X-Small" ForeColor="Red" ValidationExpression="[0-9]*"></asp:RegularExpressionValidator>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Answer your security question to recover your password" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
    <asp:Label ID="SecurityQs" runat="server" Text="SecurityQS" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="width: 6px; text-align: right;">
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="AnswerTxt" ErrorMessage="*" Font-Size="Large" ForeColor="Red" Visible="False"></asp:RequiredFieldValidator>
            </td>
            <td>
    <asp:TextBox ID="AnswerTxt" runat="server" Visible="False"></asp:TextBox>
                &nbsp&nbsp <asp:Button ID="Button2" runat="server" Text="Recover my password" Width="150px" OnClick="Button2_Click" Visible="False" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="MsgLbl" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

