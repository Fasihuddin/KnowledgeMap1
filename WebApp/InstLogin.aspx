<%@ Page Title="Instructor login" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="InstLogin.aspx.cs" Inherits="InstLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Instructors Login</h3>
        <br />
    Username&nbsp; <asp:TextBox ID="TxtUsername" runat="server" Width="120px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtUsername" ErrorMessage="Username is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br /><br />

    Password&nbsp;  <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" Width="120px"></asp:TextBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxtPassword" ErrorMessage="Password is required" ForeColor="Red"></asp:RequiredFieldValidator>
    <br /><br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" Width="67px" />

&nbsp; 

<asp:Label ID="LabelFailed" runat="server" ForeColor="Red"></asp:Label>
</asp:Content>
