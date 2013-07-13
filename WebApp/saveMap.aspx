<%@ Page Language="C#" AutoEventWireup="true" CodeFile="saveMap.aspx.cs" Inherits="saveMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Knowledge Map</title>
    <style type="text/css">
        .auto-style1
        {
            text-decoration: underline;
        }
        .auto-style2 {
            background-color: #99CCFF;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <h3 style="background-position: 100% 70%; width:410px; height:30px; color:#00111A; font:28px/28px Georgia,Times, serif; background-image: url('../images/border2.gif'); background-repeat: no-repeat; background-attachment: scroll;" class="auto-style2">Save Knowledge Map</h3>
        <table style="width:100%;">
            <tr>
                <td>* Please review the computer-generated knowledge map below. This knowledge map will be used by students<br />
&nbsp;&nbsp; in the course. <strong>Don&#39;t forget to </strong><span class="auto-style1"><strong>Save</strong></span><strong> the knowledge map!!</strong><br />
                    * You may add a new topic to the course through a button that would appear after the knowledge map is saved.
                    <br />
                </td>
            </tr>
            <tr>
                <td><asp:Button ID="btnSave" runat="server" Text="Save Knowledge Map" OnClick="btnSave_Click" />
                    &nbsp;
                    <asp:Button ID="btnAddTopic" runat="server" Text="Add Another Topic" Width="185px" Visible="False" OnClick="btnAddTopic_Click" />

                &nbsp;&nbsp;
                    <asp:Button ID="btnFinish" runat="server" OnClick="btnFinish_Click" Text="Finish and Go to Main Page" Visible="False" Width="198px" />

                </td>
            </tr>
            <tr>
                <td>
                    <img id="imgMap" src="CreateMap.aspx" runat="server" />
                </td>
            </tr>
            </table>
        <br />
    </form>
</body>
</html>
