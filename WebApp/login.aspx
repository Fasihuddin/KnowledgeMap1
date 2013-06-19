<%@ Page Title="Login" Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" MasterPageFile="~/MasterPage.master" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <h3>Student Login </h3>
        <p>
            <asp:Login ID="Login1" runat="server" DestinationPageUrl="~/StdCourseIntro.aspx" Height="130px" Width="401px" OnAuthenticate="Login1_Authenticate" DisplayRememberMe="False">
            </asp:Login>
        </p>
  </asp:Content>
