<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RegistrationForm.aspx.cs" Inherits="RegistrationForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 172px;
            text-align: right;
        }
        .auto-style3 {
            width: 234px;
        }
        .auto-style4 {
            width: 172px;
            text-align: right;
            height: 26px;
        }
        .auto-style5 {
            width: 234px;
            height: 26px;
        }
        .auto-style6 {
            height: 26px;
        }
        .auto-style7 {
            text-align: center;
        }
        .auto-style8 {
            font-size: x-small;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h2 class="auto-style7">Registration Form<br />
        </h2>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Name</td>
                <td class="auto-style3">
                    <asp:TextBox ID="TBName" runat="server" MaxLength="50" Width="199px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorName" runat="server" ControlToValidate="TBName" ErrorMessage="Please Enter your name" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Student Id </td>
                <td class="auto-style3">
                    <asp:TextBox ID="TBId" runat="server" Width="200px"></asp:TextBox>
                    <br />
                    <span class="auto-style8">Enter your student Id (eg. 3131313)</span></td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorId" runat="server" ControlToValidate="TBId" ErrorMessage="Please enter your student Id" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Choose your username</td>
                <td class="auto-style3">
        <asp:TextBox ID="TBUsername" runat="server" MaxLength="20" style="margin-top: 0px" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUsername" runat="server" ControlToValidate="TBUsername" ErrorMessage="Please enter a username" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Create a password</td>
                <td class="auto-style3">
        <asp:TextBox ID="TBPassword" runat="server" MaxLength="10" TextMode="Password" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ControlToValidate="TBPassword" ErrorMessage="Please enter a password" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Confirm your password</td>
                <td class="auto-style5">
        <asp:TextBox ID="TBConfirm" runat="server" MaxLength="10" TextMode="Password" Width="200px"></asp:TextBox>
                </td>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorConfirm" runat="server" ControlToValidate="TBConfirm" ErrorMessage="Please re-enter the password" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ControlToCompare="TBPassword" ControlToValidate="TBConfirm" ErrorMessage="Password isn't match" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Your email</td>
                <td class="auto-style3">
        <asp:TextBox ID="TBEmail" runat="server" MaxLength="69" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TBEmail" ErrorMessage="Please enter your email" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="TBEmail" ErrorMessage="Please enter a valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Create a security question</td>
                <td class="auto-style3">
        <asp:TextBox ID="TBSecQ" runat="server" MaxLength="69" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecQ" runat="server" ControlToValidate="TBSecQ" ErrorMessage="Please enter a security question" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Security answer</td>
                <td class="auto-style3"> 
        <asp:TextBox ID="TBSecA" runat="server" MaxLength="69" Width="200px"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorSecA" runat="server" ControlToValidate="TBSecA" ErrorMessage="Please enter a security answer" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style3">
                    <asp:Button ID="Submit" runat="server" OnClick="Submit_Click" Text="Submit" />
&nbsp;&nbsp;&nbsp;
                    <input id="Reset1" type="reset" value="Start over" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:Label ID="LblResults" runat="server"></asp:Label>
        <br />
    
    </div>
    </form>
</body>
</html>
