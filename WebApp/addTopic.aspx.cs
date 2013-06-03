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
        //TEST ONLY - PLEASE DELETE
        Session["maxCourseID"] = 3;

        if (!IsPostBack)
        {

        }
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
             p1 = new SqlParameter("@courseID", (int) Session["maxCourseID"]);
              p2 = new SqlParameter("@topicID", topicID);
             cmd.Parameters.Add(p1);
             cmd.Parameters.Add(p2);
             x = cmd.ExecuteNonQuery();

             //Assign topic ID to session
             Session["topicID"] = topicID;

             //Show success Alerts
             System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
             System.Web.HttpContext.Current.Response.Write("alert('A new topic has been created and added to the course. Next step is to add the nodes to the topic!')");
             System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
         }
         catch (Exception ex)
         {
             Console.WriteLine(ex.ToString());
             Response.Redirect("~/Error.aspx");
         }

        //enable node textboxes and button
        btnCreate.Enabled = false;
        btnAddNodes.Enabled = true;
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

            while (reader.Read())
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

    protected void btnShowMap_Click(object sender, EventArgs e)
    {

        Session["TopicID"] = 1;
        Session["CourseID"] = 1;
        Response.Redirect("~/saveMap.aspx");
    }
    protected void btnAddNodes_Click(object sender, EventArgs e)
    {
       ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('addNode.aspx?','AddNode','left=300, top=150,resizable=no,width=540,height=280');",true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {


        //enable show
    }
}