using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class StdShowTopicNode : System.Web.UI.Page, ICallbackEventHandler
{
    private String strMessage;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get course and topic ID from url address
            int courseID = Convert.ToInt32(Request.QueryString["cid"]);
            int topicId = Convert.ToInt32(Request.QueryString["tid"]);
            //add those IDs to a session
            Session["CourseTopicID"] = courseID + ";" + topicId;

           //fill topic labels
            setTopicLabels(topicId);
                
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
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private void setTopicLabels(int topicId)
    {
         //select all test versions for the node
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Name, Description FROM Topic WHERE Topic_id = @topicId", conStr);
            SqlParameter p1 = new SqlParameter("@topicId", topicId);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                lblTopicName.Text += " " + reader.GetString(0);
                lblDesc.Text += " " + reader.GetString(1);
            }
            //close sql connection
            reader.Close();
        }
        catch (Exception ex)
        {
            ex.ToString();

        }
        finally
        {
            conStr.Close();
        }
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
            strMessage = "";
        }
    }

}