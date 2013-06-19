<%@ Page Title="Course Intro" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StdCourseIntro.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">   
     <style type="text/css">
        .auto-style1
        {
            width: 164px;
        }
        .auto-style2
        {
            width: 164px;
            height: 79px;
        }
        .auto-style3
        {
            height: 79px;
        }
        .auto-style4
        {
            width: 164px;
            height: 31px;
        }
        .auto-style5
        {
            height: 31px;
        }
        .auto-style6
        {
            color: #FF0000;
        }
        .auto-style7
        {
            width: 164px;
            height: 32px;
        }
        .auto-style8
        {
            height: 32px;
        }
        .auto-style9
        {
            width: 164px;
            height: 39px;
        }
        .auto-style10
        {
            height: 39px;
        }
    </style>

    <div>
    
        <h3>Course Selection</h3>
        <span class="auto-style6">Please select the course that you want to access the contents. Then, select the Topic of the course.</span><br />
&nbsp;<br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style7">
    
        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="Small" Text="Please select course"></asp:Label>
&nbsp;&nbsp;</td>
                <td class="auto-style8"><asp:ListBox ID="ListCourse" runat="server" style="margin-top: 4px" Width="250px" Font-Size="Medium" Rows="1" AutoPostBack="True" OnSelectedIndexChanged="ListCourse_SelectedIndexChanged"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Course Description: </td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtCourseDesc" runat="server" Height="68px" TextMode="MultiLine" Width="360px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">Select Topic:</td>
                <td class="auto-style5">
                    <asp:ListBox ID="lstTopic" runat="server" AutoPostBack="True" Enabled="False" Height="16px" OnSelectedIndexChanged="lstTopic_SelectedIndexChanged" Rows="1" Width="247px"></asp:ListBox>
&nbsp;&nbsp;&nbsp;&nbsp; </td>
            </tr>
            <tr>
                <td class="auto-style9">Topic Description: </td>
                <td class="auto-style10">
                    <asp:TextBox ID="txtTopicDesc" runat="server" Height="68px" TextMode="MultiLine" Width="360px" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">&nbsp;</td>
                <td>
                    <asp:Button ID="BtnStart" runat="server" Enabled="False" Text="Start Topic" Width="111px" OnClick="BtnStart_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <asp:Label ID="LblResults" runat="server"></asp:Label>
        <br />
    
    </div>

    </asp:Content>
