<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QuestionForm.aspx.cs" Inherits="QuestionForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 173px;
        }
        .auto-style2
        {
            width: 173px;
            height: 113px;
        }
        .auto-style3
        {
            height: 113px;
        }
        .auto-style4
        {
            width: 173px;
            height: 39px;
        }
        .auto-style5
        {
            height: 39px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td class="auto-style4">Question Type: </td>
                <td class="auto-style5">
                    <asp:DropDownList ID="ddlQuestionType" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style2">Questions:</td>
                <td class="auto-style3">
                    <asp:TextBox ID="txtQuestion" runat="server" Height="90px" TextMode="MultiLine" Width="438px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style1">Answer choices: </td>
                <td>
                    <asp:TextBox ID="txtAnswerChoices" runat="server" Height="112px" TextMode="MultiLine" Width="438px"></asp:TextBox>
&nbsp;(Enter answer choices as comma separated)</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
