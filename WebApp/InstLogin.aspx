<%@ Page Title="Instructor login" Language="C#" MasterPageFile="~/InstMasterPage.master" AutoEventWireup="true" CodeFile="InstLogin.aspx.cs" Inherits="InstLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Instructors Login</h3>
        <asp:Login ID="Login1" runat="server" DisplayRememberMe="False" Height="153px" OnAuthenticate="Login1_Authenticate" Width="335px" TitleText="">
        </asp:Login>
    </asp:Content>
