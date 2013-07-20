<%@ Page Title="Registration" Language="C#" AutoEventWireup="true" CodeFile="RegistrationForm.aspx.cs" Inherits="RegistrationForm" MasterPageFile="~/MasterPage.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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

    
    <div>
    
        <h3>Create an Account<br />
        </h3>
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
                   <br />
                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter numbers only (eg. 313131)" ControlToValidate="TBId" ForeColor="Red" ValidationExpression="[0-9]*"></asp:RegularExpressionValidator>
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
        <asp:Label ID="LblResults" runat="server" Font-Bold="True"></asp:Label>
        <br />
    
    </div>
   
    </asp:Content>