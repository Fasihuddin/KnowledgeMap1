using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;


public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            FillCourseList();
        }
    }
    
    //Method to fill the course list
    private void FillCourseList()
    {
        ListCourse.Items.Clear();
        List<Course> newItem = new List<Course>();
      
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand("SELECT Course_Id, Name, Code, Description FROM Course", conStr);
        // Try to open database and read information.
        try
        {
            conStr.Open();
            reader = cmd.ExecuteReader();
            // For each item, add the Course name to the displayed
            // list box text
            while (reader.Read())
            {
                int courseId = reader.GetInt32(0);
                String name = reader.GetString(1);
                String code = reader.GetString(2);
                String desc = reader.GetString(3);

                Course newCourse = new Course(courseId, name, code, desc);
                ListCourse.Items.Add(code + ", " + name);
                newItem.Add(newCourse);
                txtCourseDesc.Text = desc;
            }
            reader.Close();
            Session["CourseList"] = newItem;
        }
        catch (Exception err)
        {
            LblResults.Text = "Error reading list of Courses. ";
            LblResults.Text += err.Message;
        }
        finally
        {
            conStr.Close();
        }
    }
    protected void ListCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //make listbox, textbox and button visible
        lstTopic.Enabled = true;
        lstTopic.Items.Clear();
        txtTopicDesc.Text = "";
        BtnStart.Enabled = false;

        List<Topic> allTopics = new List<Topic>();
        int item = ListCourse.SelectedIndex;
        List<Course> newItem = (List<Course>) Session["CourseList"];
        Course selectedCourse = newItem[item];
        txtCourseDesc.Text = selectedCourse.description;

        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand("SELECT Topic.Topic_Id, Topic.Name, Topic.Description "+
                    "FROM Topic INNER JOIN TopicOnCourse ON Topic.Topic_Id = TopicOnCourse.TopicID WHERE TopicOnCourse.CourseID = @courseId", conStr);
        SqlParameter p1 = new SqlParameter("@courseId", selectedCourse.courseID);
        cmd.Parameters.Add(p1);
        // Try to open database and read information.
        try
        {
            conStr.Open();
            reader = cmd.ExecuteReader();
            // For each item, add the Course name to the displayed
            // list box text
            while (reader.Read())
            {
                int topicId = reader.GetInt32(0);
                String name = reader.GetString(1);
                String desc = reader.GetString(2);
                Topic newTopic = new Topic(topicId, name, desc);
                allTopics.Add(newTopic);
                lstTopic.Items.Add(name);
            }
            if (allTopics.Count > 0)
            {
                //assign the description of topic 1
                txtTopicDesc.Text = allTopics[0].description;
                BtnStart.Enabled = true;
            }

            reader.Close();
            Session["TopicList"] = allTopics;
        }catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            conStr.Close();
        }
    }
    protected void lstTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        int item = lstTopic.SelectedIndex;
        if (item >= 0)
        {
            List<Topic> newItem = (List<Topic>)Session["TopicList"];
            txtTopicDesc.Text = newItem[item].description;
        }
    }
    protected void BtnStart_Click(object sender, EventArgs e)
    {
        //get the topic and course ID
        List<Topic> allTopics = (List<Topic>)Session["TopicList"]; 
        List<Course> allCourses = (List<Course>)Session["CourseList"]; 
        int topicId = allTopics[lstTopic.SelectedIndex].topicId;
        int courseId = allCourses[ListCourse.SelectedIndex].courseID;

        //add topic and course id to the session to be passed to subsequent forms
        Session["CourseTopicID"] = courseId.ToString() + ";" + topicId.ToString();
        Response.Redirect("~/StdShowTopicMap.aspx");
    }
}


