using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StdShowTopicNode : System.Web.UI.Page, ICallbackEventHandler
{
    private String strMessage;

    protected void Page_Load(object sender, EventArgs e)
    {

        // This is the AJAX code will get the call back event from the javascript function.
        // the arguments passed by the javascript function will be gotten from this code.
        ClientScriptManager cm = Page.ClientScript;
        String cbReference = cm.GetCallbackEventReference(this, "arg",
            "ReceiveServerData", "");
        String callbackScript = "function CallServer(arg, context) {" +
            cbReference + "; }";
        cm.RegisterStartupScript(this.GetType(),
            "CallServer", callbackScript, true);
    }

    // This is the inherits code from callback event interface.
    public string GetCallbackResult()
    {
        // Returing the message from server side to the client side (javascript).
        return strMessage;
    }

    // This is the inherits code from callback event interface.
    public void RaiseCallbackEvent(string eventArgument)
    {
        // Define variables to store the clicked coordinates and each region coordinates
        int x, y, x1, y1, x2, y2;
        // boolean variable to be used test whether clicked coordinates inside the region.
        bool regTest = false;
        int arrayLoc = -1;
        // getting the clicked location from Javascript function
        string[] requestLoc = new string[2];
        requestLoc = eventArgument.Split(';');

        //only for IE
        if (requestLoc[0].IndexOf(".") != -1 )
        {
            requestLoc[0] = requestLoc[0].Substring(0, requestLoc[0].IndexOf("."));
        }
        
        if (requestLoc[1].IndexOf(".") != -1)
        {
            requestLoc[1] = requestLoc[1].Substring(0, requestLoc[1].IndexOf("."));
        }

        x = Convert.ToInt32(requestLoc[0]);
        y = Convert.ToInt32(requestLoc[1]);

        //Check whether there is any region defined by the image
        if (Session["recLocation"] != null)
        {
            //Getting all region locations of a image from the database
            List<String> allRecLoc = (List<String>)Session["recLocation"];

            // it will check the mouse click coordinate with each region location
            for (int i = 0; i < allRecLoc.Count; i++)
            {
                // Get the region locations
                string[] recLoc = new string[4];
                recLoc = allRecLoc[i].ToString().Split(';');
                x1 = Convert.ToInt32(recLoc[0]);
                x2 = Convert.ToInt32(recLoc[1]);
                y1 = Convert.ToInt32(recLoc[2]);
                y2 = Convert.ToInt32(recLoc[3]);

                //Checking the mouse click coordinate with each region location.
                if ((x >= x1) & (x <= x2))
                {
                    if ((y >= y1) & (y <= y2))
                    {
                        // Set the boolean true and get the array location of the region
                        regTest = true;
                        arrayLoc = i;
                    }
                }
            }

            if (regTest == true)
            {
                // Get the region label and region message
                List<int> allNodes = (List<int>)Session["AllNodes"];
                strMessage = allNodes[arrayLoc].ToString();
            }
            else
            {
                strMessage = "";
            }
        }//end if
        else
        {
            strMessage = "Error - NoRegion";
        }
    }

}