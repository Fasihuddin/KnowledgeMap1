<%@ Page Language="C#" AutoEventWireup="true" CodeFile="saveMap.aspx.cs" Inherits="saveMap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Save Knowledge Map</title>
    <style type="text/css">
        .auto-style1
        {
            height: 23px;
        }
    </style>


</head>
<body>
    <form id="form1" runat="server">
        <h1><strong>Save Knowledge Map</strong></h1>
        <table style="width:100%;">
            <tr>
                <td><img id="imgMap" src="CreateMap.aspx" runat="server" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Text="Save Knowledge Map" OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td class="auto-style1">
                    
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
    </form>
</body>
</html>
