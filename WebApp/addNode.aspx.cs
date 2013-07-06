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
        if (!IsPostBack && Session["newNodes"] == null)
        {
            List<Node> nodeLst = new List<Node>();
            Session["newNodes"] = nodeLst;
        }
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

            while (reader.Read() && !reader.IsDBNull(0))
            {
                nodeID = reader.GetInt32(0);
            }
            reader.Close();

            //increase the nodeID by 1 for adding new node
            nodeID += 1;
            //Insert new node to DB
            cmd = new SqlCommand("INSERT INTO Nodes VALUES (@nodeId, @name, @desc)", conStr);
            SqlParameter p1 = new SqlParameter("@nodeId", nodeID);
            SqlParameter p2 = new SqlParameter("@name", txtNode.Text);
            SqlParameter p3 = new SqlParameter("@desc", txtNodeDesc.Text);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            int x = cmd.ExecuteNonQuery();

            //add new node object to session of new nodes
            List<Node> nodeLst = (List<Node>)Session["newNodes"];
            nodeLst.Add(new Node(nodeID, txtNode.Text, txtNodeDesc.Text));
            Session["newNodes"] = nodeLst;

            //Insert links of the node to the database
            addMaterials(nodeID);

            //clear values in textboxes
            txtNode.Text = "";
            txtLinks.Text = "";
            txtNodeDesc.Text = "";

            //Show success Alerts
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
            System.Web.HttpContext.Current.Response.Write("alert('A new module has been added. Please add other modules , otherwise press Finish Adding Modules button.')");
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
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

    /*
     * This method is used to insert links of the node to the database
     * */
    private void addMaterials(int nodeID)
    {
        if (!txtLinks.Text.Equals(""))
        {
            int materialID = 0;
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();

                //Get the latest nodeID
                SqlCommand cmd = new SqlCommand("SELECT MAX(Material_Id) FROM Materials", conStr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() && !reader.IsDBNull(0))
                {
                    materialID = reader.GetInt32(0);
                }
                reader.Close();

                //increase materialID by 1 to add new record
                materialID += 1;

                //get textbox value per line
                string txt = txtLinks.Text;
                string[] lst = txt.Split(new Char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                int order = 1;
                foreach (string link in lst)
                {
                    //get name and the links
                    string[] requestLoc = new string[2];
                    if (link.IndexOf(";") != -1)
                    {
                        requestLoc = link.Split(';');
                    }
                    else
                    {
                        requestLoc[0] = "Material Link";
                        requestLoc[1] = link;
                    }

                    //Insert new link to DB
                    cmd = new SqlCommand("INSERT INTO Materials VALUES (@materialId, @name, @url, @order, @nodeId, @topicId)", conStr);
                    SqlParameter p1 = new SqlParameter("@materialId", materialID);
                    SqlParameter p2 = new SqlParameter("@name", requestLoc[0]);
                    SqlParameter p3 = new SqlParameter("@url", requestLoc[1]);
                    SqlParameter p4 = new SqlParameter("@order", order);
                    SqlParameter p5 = new SqlParameter("@nodeId", nodeID);
                    SqlParameter p6 = new SqlParameter("@topicId", Convert.ToInt32(Session["topicID"]));
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    cmd.Parameters.Add(p6);
                    int x = cmd.ExecuteNonQuery();

                    //increment materialID and order
                    materialID++;
                    order++;
                }

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
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        ClientScript.RegisterStartupScript(Page.GetType(), "AddNode", "<script>window.close();</script>");
    }
}