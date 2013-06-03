<%@ Page Language="C#" AutoEventWireup="true" CodeFile="addCourse.aspx.cs" Inherits="addCourse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            color: #000000;
        }
        .auto-style2
        {
            width: 348px;
        }
        .auto-style4
        {
            width: 148px;
        }
        .auto-style5
        {
            width: 146px;
        }
        .auto-style6
        {
            width: 146px;
            height: 30px;
        }
        .auto-style7
        {
            width: 348px;
            height: 30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <h1><strong>Add Course</strong></h1>
        <table style="width: 92%;">
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label1" runat="server" Text="Course Code:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCourseCode" runat="server" MaxLength="10" style="margin-left: 0px" Width="152px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCourseCode" ErrorMessage="* Please input the course code" ForeColor="#FF3300" ValidationGroup="Course"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label2" runat="server" Text="Course Name:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCourseName" runat="server" MaxLength="150" Width="274px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCourseName" ErrorMessage="* Please input the course name" ForeColor="#FF3300" ValidationGroup="Course"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="Label3" runat="server" Text="Course Description:"></asp:Label>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="txtCourseDesc" runat="server" Height="60px" TextMode="MultiLine" Width="274px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style6"></td>
                <td class="auto-style7">
                    <asp:Button ID="btnAddCourse" runat="server" OnClick="btnAddCourse_Click" Text="Add Course" ValidationGroup="Course" />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" colspan="2">Please assign topics to the newly created course.
                    <br />
                    * If you want to assign the existing topics to the course, click <em><strong>Assign Existing Topics</strong></em> button.
                    <br />
                    * If you want to add new topics to the course, click <em><strong>Add New Topics</strong></em> button<br />
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:Button ID="btnAddExisting" runat="server" Enabled="False" OnClick="btnAddExisting_Click" Text="Assign Existing Topics" Width="161px" />
                </td>
                <td class="auto-style2">
                    <asp:Button ID="btnAddNewTopics" runat="server" Enabled="False" Text="Add New Topics" Width="196px" OnClick="btnAddNewTopics_Click" />
                </td>
            </tr>
        </table>
        <br />
        <h2>Assign Existing Topics</h2>
        <table style="width:100%;">
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label4" runat="server" Text="Select Topic:"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpTopics" runat="server" Height="16px" Width="360px" AutoPostBack="True" OnSelectedIndexChanged="drpTopics_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="Label6" runat="server" Text="Topic Description:"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtTopicDesc" runat="server" Enabled="False" Height="87px" TextMode="MultiLine" Width="355px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">&nbsp;</td>
                <td>
                    <asp:Button ID="btnAddTopicCourse" runat="server" Enabled="False" OnClick="addTopicCourse_Click" Text="Add Topic to Course" Width="196px" />
                </td>
            </tr>
        </table>
        <br />
    
    </div>
    </form>
</body>
</html>
