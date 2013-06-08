using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addNodesPrereq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int topicID = 3;//Convert.ToInt32(Session["topicID"]);
        List<Node> tn = new List<Node>();   
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name, Nodes.Description FROM NodeOnTopic INNER JOIN"+
                        " Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id WHERE (NodeOnTopic.Topic_Id = @topicId)", conStr);
            SqlParameter p1 = new SqlParameter("@topicId", topicID);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                tn.Add(new Node(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
            }
            reader.Close();

            //bind to gridview
            GridView1.DataSource = tn;
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
            Response.Redirect("~/Error.aspx");
        }
        finally
        {
            conStr.Close();
        }
    }

    protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
    {
        if (e.CommandName == "AddPrerequisites")
        {
            // Retrieve the row index stored in the 
            // CommandArgument property.
            int index = Convert.ToInt32(e.CommandArgument);

            // Retrieve the row that contains the button 
            // from the Rows collection.
            GridViewRow row = GridView1.Rows[index];

            
            // Add code here to add the item to the shopping cart.
        }

    }
}