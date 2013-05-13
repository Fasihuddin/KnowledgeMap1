<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StdShowTopicMap.aspx.cs" Inherits="StdShowTopicNode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Topic Map</title>

    <script language="javascript" type="text/javascript">
        function point_it(event){
            // Getting the x and y position of the mouse
            ofsetLeft = document.getElementById("pointer_div").offsetLeft;
            ofsetTop = document.getElementById("pointer_div").offsetTop;
            pos_x = event.offsetX?(event.offsetX):event.pageX-document.getElementById("pointer_div").offsetLeft;
            pos_y = event.offsetY?(event.offsetY):event.pageY-document.getElementById("pointer_div").offsetTop;
            document.getElementById("imageCanvas").style.left = (pos_x-1) ;
            document.getElementById("imageCanvas").style.top = (pos_y-15) ;
            document.getElementById("imageCanvas").style.visibility = "visible" ;    
            //Calling the server to pass the coordinate of the mouse click        
            CallServer(pos_x+";"+pos_y, "Position of the mouse click");
        }
    
        // This function will be used to receive the information from the server after the mouse click
        function ReceiveServerData(arg) {
            if(arg.length < 0)
            {
                // if there is no region label and message, show null
                document.form1.txtLabel.value = "Hello2";
            }
            else{
                // Declare an array and split the passed argument into region label and message
                document.form1.txtLabel.value = arg;
            }
        }

        </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table style="width:100%;">
            <tr>
                <td><div id="pointer_div" onclick="point_it(event)"><img src="StdTopicMap.aspx" alt="image" id="imageCanvas" /></div>
                </td>
                <td></td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><asp:Label ID="lblMessage" runat="server" Text="lblMessage"></asp:Label>
                    <asp:TextBox ID="txtLabel" runat="server"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
