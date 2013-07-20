<%@ Page Title="Instructor Logout" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="InstLogout.aspx.cs" Inherits="InstLogout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p style="color: #FF0000">
        You are looged out
    </p>  <br />
    &nbsp;
    <asp:HyperLink ID="Login" runat="server" NavigateUrl="~/InstLogin.aspx">Login</asp:HyperLink>
</asp:Content>

