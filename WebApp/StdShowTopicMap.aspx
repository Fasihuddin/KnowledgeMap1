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
<<<<<<< HEAD
            if(arg.length < 0)
            {
                // if there is no region label and message, show null
                document.form1.txtLabel.value = "Hello2";
            }
            else{
                // Declare an array and split the passed argument into region label and message
                document.form1.txtLabel.value = arg;
=======
            if(arg.length <= 0)
            {
                // if there is no region label and message, show null
                document.getElementById('<%=lblMessage.ClientID%>').textContent = "You clicked at the wrong area. Please click on the Module only!";
            }
            else{
                // Declare an array and split the passed argument into region label and message
                document.getElementById('<%=lblMessage.ClientID%>').textContent = "Node: " + arg;
                document.location.href = "StdNodePage.aspx?nid=" + arg;
>>>>>>> 21/05/2013
            }
        }

        </script>

<<<<<<< HEAD
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
=======
    <style type="text/css">
        .auto-style1
        {
            width: 402px;
        }
        .auto-style2
        {
            height: 61px;
        }
        .auto-style3
        {
            height: 17px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
    <div><h1>Topic Node</h1>
        <table style="width:100%;">
            <tr>
                <td class="auto-style1" rowspan="3"><div id="pointer_div" onclick="point_it(event)"><img src="StdTopicMap.aspx" alt="image" id="imageCanvas" /></div>
                </td>
                <td class="auto-style3"><asp:Label ID="lblMessage" runat="server" Text="Please click on the Module to view the materials and start the test." Font-Bold="True" ForeColor="Red"></asp:Label>
                    <br />
                    <br />
                    Legend:<br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#009933" Text="Green Module"></asp:Label>
&nbsp;- You have passed the test<br />
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0033CC" Text="Blue Module"></asp:Label>
&nbsp;- You have NOT passed the test</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
>>>>>>> 21/05/2013
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
