using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addCourse : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Get the maximum course ID (int) from database
            int maxCourseID = 0;
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT MAX(Course_Id) FROM Course", conStr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    maxCourseID = reader.GetInt32(0);
                }
                reader.Close();
                Session["CourseID"] = maxCourseID;
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
    protected void btnAddExisting_Click(object sender, EventArgs e)
    {
        btnAddNewTopics.Enabled = true;
        drpTopics.Enabled = true;
        txtTopicDesc.Enabled = true;
        btnAddTopicCourse.Enabled = true;

        //get list of topics from database
        List<Topic> allTopics = new List<Topic>();
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("SELECT Topic_Id, Name, Description FROM Topic", conStr);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int id = reader.GetInt32(0);
                string name = reader.GetString(1);
                string desc = reader.GetString(2);
                Topic newTopic = new Topic(id, name, desc);
                allTopics.Add(newTopic);
            }
            reader.Close();

            //bind to dropdown lists
            drpTopics.DataSource = allTopics;
            drpTopics.DataTextField = "name";
            drpTopics.DataValueField = "topicId";
            drpTopics.DataBind();

            txtTopicDesc.Text = allTopics[0].description;
            Session["allTopics"] = allTopics;
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
    protected void addTopicCourse_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("INSERT INTO TopicOnCourse VALUES (@courseID, @topicID)", conStr);
            SqlParameter p1 = new SqlParameter("@courseID", (int)Session["CourseID"]);
            SqlParameter p2 = new SqlParameter("@topicID", drpTopics.SelectedItem.Value);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

            int x = cmd.ExecuteNonQuery();

            //Show success Alerts
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
            System.Web.HttpContext.Current.Response.Write("alert('A topic has been added to the course. You may add other topics to the course')");
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
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
    protected void btnAddCourse_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("INSERT INTO Course VALUES (@courseID, @name, @code, @desc)", conStr);
            SqlParameter p1 = new SqlParameter("@courseID", (int)Session["CourseID"] + 1);
            SqlParameter p2 = new SqlParameter("@name", txtCourseName.Text);
            SqlParameter p3 = new SqlParameter("@code", txtCourseCode.Text);
            SqlParameter p4 = new SqlParameter("@desc", txtCourseDesc.Text);

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);

            int x = cmd.ExecuteNonQuery();

            Session["CourseID"] = (int) Session["CourseID"] + 1;

            //Show success Alerts
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
            System.Web.HttpContext.Current.Response.Write("alert('A course has been added to database.')");
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
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

        //Enable the topics button
        btnAddExisting.Enabled = true;
        btnAddNewTopics.Enabled = true;
        btnAddCourse.Enabled = false;
    }
    protected void drpTopics_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Topic> allTopics = (List<Topic>)Session["allTopics"];
        txtTopicDesc.Text = allTopics[drpTopics.SelectedIndex].description;
    }
    protected void btnAddNewTopics_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/addTopic.aspx");
    }
}