<%@ Page Title="Topic Map" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="StdShowTopicMap.aspx.cs" Inherits="StdShowTopicNode" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript">
        function point_it(event) {
            //detect browser. firefox?
            var val = navigator.userAgent.toLowerCase();
            if (val.indexOf("firefox") > -1) {
                pos_x = event.clientX + document.body.scrollLeft + document.documentElement.scrollLeft - 205 - document.getElementById("pointer_div").offsetLeft;
                pos_y = event.clientY + document.body.scrollTop + document.documentElement.scrollTop - 420 - document.getElementById("pointer_div").offsetTop;
               
            } else {
                // Getting the x and y position of the mouse
                pos_x = event.offsetX ? (event.offsetX) : event.pageX - document.getElementById("pointer_div").offsetLeft;
                pos_y = event.offsetY ? (event.offsetY) : event.pageY - document.getElementById("pointer_div").offsetTop;
            }
            document.getElementById("imageCanvas").style.left = (pos_x - 1);
            document.getElementById("imageCanvas").style.top = (pos_y - 15);
            document.getElementById("imageCanvas").style.visibility = "visible";
            //Calling the server to pass the coordinate of the mouse click        
            CallServer(pos_x + ";" + pos_y, "Position of the mouse click");
        }
    
        // This function will be used to receive the information from the server after the mouse click
        function ReceiveServerData(arg) {
            if(arg.length <= 0)
            {
                // if there is no region label and message, show null
                document.getElementById('<%=lblMessage.ClientID%>').textContent = "You clicked at the wrong area. Please click on the Module only!";
            }
            else{
                // Declare an array and split the passed argument into region label and message
                document.getElementById('<%=lblMessage.ClientID%>').textContent = "Node: " + arg;
                document.location.href = "StdNodePage.aspx?nid=" + arg;
            }
        }

        </script>

    <style type="text/css">
        .auto-style1
        {
            width: 402px;
        }
        .auto-style2 {
            height: 36px;
        }
        </style>


   
    <div><h3>Topic Knowledge Map</h3>
            <table style="width:63%;">
                <tr>
                    <td><asp:Label ID="lblTopicName" runat="server" Font-Italic="True" Text="Topic: " Font-Bold="True" ForeColor="#6666FF"></asp:Label></td>
                    <td>
            <asp:TextBox ID="txtTopicName" runat="server" ReadOnly="True" Font-Italic="True" BorderWidth="0" BorderStyle="None" Rows="2" Width="556px" Font-Bold="True" ForeColor="#6666FF"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td valign="top" class="auto-style2">
            <asp:Label ID="lblDesc" runat="server" Font-Italic="True" Text="Description: " Font-Bold="True" ForeColor="#6666FF"></asp:Label>
                    </td>
                    <td class="auto-style2">
            <asp:TextBox ID="txtDesc" runat="server" ReadOnly="True" TextMode="MultiLine" Font-Italic="True" BorderWidth="0" BorderStyle="None" Rows="2" Width="556px" Font-Bold="True" ForeColor="#6666FF"></asp:TextBox>
                    </td>
                </tr>
                </table><br />
        <table style="width:100%;">
            <tr>
                <td>
                   <b>Legend:</b><br />
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#009933" Text="Green Module"></asp:Label>
&nbsp;- You have passed the test&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="#0033CC" Text="Blue Module"></asp:Label>
                    &nbsp;- You have NOT passed the test<br />
                    <asp:Label ID="lblMessage" runat="server" Text="Please click on the Module to view the materials and start the test." Font-Bold="True" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style1"><div id="pointer_div" onclick="point_it(event)"><img src="StdTopicMap.aspx" alt="image" id="imageCanvas" /></div>
                </td>
            </tr>
            </table>
    
    </div>
  
    </asp:Content>
