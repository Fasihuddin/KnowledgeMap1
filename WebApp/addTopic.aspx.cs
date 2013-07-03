///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                          Knowledge Map Web Application for Research                                           //
//                                   Created by Ilung Pranata                                                    //
//                                  Date created: 11-May-2013                                                    //


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addTopic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        int topicID = getNextTopicID() + 1;

         SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
         try
         {
             conStr.Open();
             //Insert new topic to Topic DB
             SqlCommand cmd = new SqlCommand("INSERT INTO Topic VALUES (@topicId, @name, @desc, '', '')", conStr);
             SqlParameter p1 = new SqlParameter("@topicId", topicID);
             SqlParameter p2 = new SqlParameter("@name", txtTopic.Text);
             SqlParameter p3 = new SqlParameter("@desc", txtDescription.Text);
             cmd.Parameters.Add(p1);
             cmd.Parameters.Add(p2);
             cmd.Parameters.Add(p3);
             int x = cmd.ExecuteNonQuery();

             //Insert topicOnCourse
             cmd = new SqlCommand("INSERT INTO TopicOnCourse VALUES (@courseID, @topicID)", conStr);
             p1 = new SqlParameter("@courseID", (int) Session["CourseID"]);
              p2 = new SqlParameter("@topicID", topicID);
             cmd.Parameters.Add(p1);
             cmd.Parameters.Add(p2);
             x = cmd.ExecuteNonQuery();

             //Assign topic ID to session
             Session["topicID"] = topicID;

             //enable buttons
             btnAddNodes.Enabled = true;
             btnRefresh.Enabled = true;
             btnRight.Enabled = true;
             btnConfirmAssignment.Enabled = true;
             btnRemove.Enabled = true;
             btnCreate.Enabled = false;

             //fill the topics and modules
             addExistingTopic(true, topicID);

             //Show success Alerts
             System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
             System.Web.HttpContext.Current.Response.Write("alert('A new topic has been created and added to the course. Next step is to add modules to this topic!')");
             System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
         }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
             Response.Redirect("~/Error.aspx");
         }

        //enable node textboxes and button
        btnCreate.Enabled = false;
    }

    private int getNextTopicID()
    {
        //Get the maximum course ID (int) from database
        int maxTopicID = 0;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("SELECT MAX(Topic_Id) FROM Topic", conStr);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read() && !reader.IsDBNull(0))
            {
                maxTopicID = reader.GetInt32(0);
            }
            reader.Close();
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

        return maxTopicID;
    }

    private void addExistingTopic(bool refresh, int currentTopicID)
    {
        if (!Page.IsPostBack || refresh == true)
        {
            ddlTopic.Items.Clear(); //clear all items in dropdownlist
            if (Session["newNodes"] != null)
            {
                List<Node> newNodes = (List<Node>)Session["newNodes"];
                if (newNodes.Count > 0)
                {
                    ListItem newNode = new ListItem();
                    newNode.Value = "-2"; //add -2 to the value if there is new nodes
                    newNode.Text = "New Modules";
                    ddlTopic.Items.Add(newNode);
                }
            }

            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Topic_Id, name FROM Topic ORDER BY name", conStr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() && !reader.IsDBNull(0))
                {
                    int topicId = reader.GetInt32(0);
                    if (topicId != currentTopicID)
                    {
                        ListItem lstItems = new ListItem();
                        lstItems.Value = Convert.ToString(topicId);
                        lstItems.Text = reader.GetString(1);
                        ddlTopic.Items.Add(lstItems);
                    }
                }
                reader.Close();
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

            //fill in the modules for first item in the dropdown list
            fillExistingModules(Convert.ToInt32(ddlTopic.Items[0].Value));
        } 
    }

    protected void btnAddNodes_Click(object sender, EventArgs e)
    {
       ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('addNode.aspx?','AddNode','left=300, top=150,resizable=no,width=900,height=800');",true);
    }
   
    protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clear existing items
        lstExistingModules.Items.Clear();

        //get topicID
        int topicId = Convert.ToInt32(ddlTopic.SelectedItem.Value);
        fillExistingModules(topicId);
    }

    private void fillExistingModules(int topicId)
    {
        //if user selected the newly created module
        if (topicId == -2)
        {
            List<Node> newNodes = (List<Node>)Session["newNodes"];
            foreach (Node n in newNodes){
                ListItem lstItems = new ListItem();
                lstItems.Value = n.NodeId.ToString();
                lstItems.Text = n.Name;
                lstExistingModules.Items.Add(lstItems);
            }
        }
        else
        {
                //get all modules based on the selected topicId
                SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                try
                {
                    conStr.Open();
                    //Get the answer for the question
                    SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name FROM NodeOnTopic INNER JOIN" +
                                 " Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id WHERE (NodeOnTopic.Topic_Id = @topicId)", conStr);

                    SqlParameter p1 = new SqlParameter("@topicId", topicId);
                    cmd.Parameters.Add(p1);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ListItem lstItems = new ListItem();
                        lstItems.Value = Convert.ToString(reader.GetInt32(0));
                        lstItems.Text = reader.GetString(1);
                        lstExistingModules.Items.Add(lstItems);
                    }
                    reader.Close();
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
    }

    protected void btnRight_Click(object sender, EventArgs e)
    {
        for (int i = lstExistingModules.Items.Count - 1; i >= 0; i--)
        {
            if (lstExistingModules.Items[i].Selected == true)
            {
                lstModuleOnTopic.Items.Add(lstExistingModules.Items[i]);
            }
        }
    }
    protected void btnRemove_Click(object sender, EventArgs e)
    {
        if (lstModuleOnTopic.SelectedIndex >= 0)
        {
           lstModuleOnTopic.Items.Remove(lstModuleOnTopic.SelectedItem);
        }
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        lstExistingModules.Items.Clear();
        addExistingTopic(true, Convert.ToInt32(Session["topicID"]));
    }

    protected void btnConfirmAssignment_Click(object sender, EventArgs e)
    {
        if (lstModuleOnTopic.Items.Count > 0)
        {
            int topicId = Convert.ToInt32(Session["topicID"]);

            //get all modules based on the selected topicId
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                for(int i = 0; i<lstModuleOnTopic.Items.Count; i++){

                    int nodeId = Convert.ToInt32(lstModuleOnTopic.Items[i].Value);
                    //Get the answer for the question
                    SqlCommand cmd = new SqlCommand("INSERT INTO NodeOnTopic(Node_Id, Topic_Id) VALUES(@nodeID, @topicID)", conStr);

                    SqlParameter p1 = new SqlParameter("@nodeID", nodeId);
                    SqlParameter p2 = new SqlParameter("@topicID", topicId);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);

                    cmd.ExecuteNonQuery();
                }

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

            //open up node pre-requisite page
            Response.Redirect("~/addNodesPrereq.aspx");
        }
        else
        {
            //Show No modules Alerts
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
            System.Web.HttpContext.Current.Response.Write("alert('You have not assigned any module to the new topic. Please assign the modules')");
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
        }
    }
}