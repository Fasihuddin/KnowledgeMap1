using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addNode : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAddNewNode_Click(object sender, EventArgs e)
    {
        int nodeID = 0;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();

            //Get the latest nodeID
            SqlCommand cmd = new SqlCommand("SELECT MAX(Node_Id) FROM Nodes", conStr);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                nodeID = reader.GetInt32(0) + 1;
            }
            reader.Close();
        
            //Insert new node to DB
            cmd = new SqlCommand("INSERT INTO Nodes VALUES (@nodeId, @name, @desc)", conStr);
            SqlParameter p1 = new SqlParameter("@nodeId", nodeID);
            SqlParameter p2 = new SqlParameter("@name", txtNode.Text);
            SqlParameter p3 = new SqlParameter("@desc", txtNodeDesc.Text);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            int x = cmd.ExecuteNonQuery();

            //Show success Alerts
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Response.Redirect("~/Error.aspx");
        }
        finally
        {
            conStr.Close();
        }
    }
    protected void txtNode_TextChanged(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "AddNode", "<script>window.close();</script>");
    }
}