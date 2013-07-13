using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class ModifyModule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fill courses in the dropdownlist
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Course_Id, Name, Code FROM Course", conStr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read() && !reader.IsDBNull(0))
                {
                    ListItem lstItems = new ListItem();
                    lstItems.Value = Convert.ToString(reader.GetInt32(0));
                    lstItems.Text = reader.GetString(2) + " - " + reader.GetString(1);
                    this.ddlCourse.Items.Add(lstItems);
                }
                reader.Close();

                //get all topics of the first selected course in the dropdownlist 
                getTopicsFromDB();

                //fill all modules based on the selected topic in dropdownlist
                fillModules();
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
    protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //clear all items
        ddlTopic.Items.Clear();
        //pnlTest.Visible = false;

        //get all topics from DB
        getTopicsFromDB();

        //get all modules from DB
        fillModules();
    }

    /*
     * This function retrieves all modules from DB based on the selected Module in the dropdownlist.
     * */
    private void fillModules()
    {
        //clear all items
        this.ddlModule.Items.Clear();

        if (ddlTopic.SelectedIndex >= 0)
        {
            //fill topics
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name FROM NodeOnTopic INNER JOIN "
                         + "Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id WHERE (NodeOnTopic.Topic_Id = @topicID)", conStr);
                SqlParameter p1 = new SqlParameter("@topicID", this.ddlTopic.SelectedItem.Value);
                cmd.Parameters.Add(p1);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem lstItems = new ListItem();
                    lstItems.Value = Convert.ToString(reader.GetInt32(0));
                    lstItems.Text = reader.GetString(1);
                    this.ddlModule.Items.Add(lstItems);
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
        }//end if
    }// end function

    /*
    * This method is used to fill topics in the topics dropdownlist. Topics are obtained from DB
    * */
    private void getTopicsFromDB()
    {
        //fill topics
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("SELECT Topic.Topic_Id, Topic.Name FROM Course INNER JOIN " +
                     "TopicOnCourse ON Course.Course_Id = TopicOnCourse.CourseID INNER JOIN " +
                     "Topic ON TopicOnCourse.TopicID = Topic.Topic_Id WHERE Course_Id = @courseID", conStr);
            SqlParameter p1 = new SqlParameter("@courseID", ddlCourse.SelectedItem.Value);
            cmd.Parameters.Add(p1);
            SqlDataReader reader = cmd.ExecuteReader();

            bool hasData = false;
            while (reader.Read() && !reader.IsDBNull(0))
            {
                ListItem lstItems = new ListItem();
                lstItems.Value = Convert.ToString(reader.GetInt32(0));
                lstItems.Text = reader.GetString(1);
                this.ddlTopic.Items.Add(lstItems);
                hasData = true;
            }

            //disable module lists if no topic in the course
            if (hasData == false)
            {
                ddlModule.Enabled = false;
            }
            else
            {
                ddlModule.Enabled = true;
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


    protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillModules();
    }
    protected void btnModify_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;

        //insert module details
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("SELECT Name, Description FROM Nodes WHERE Node_Id = @nodeID", conStr);
            SqlParameter p1 = new SqlParameter("@nodeID", this.ddlModule.SelectedItem.Value);
            cmd.Parameters.Add(p1);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read() && !reader.IsDBNull(0))
            {
                txtNode.Text = reader.GetString(0);
                txtNodeDesc.Text = reader.GetString(1);
            }
            reader.Close();

            ////get all links of the node
            //cmd = new SqlCommand("SELECT Name, URL_Address FROM Materials WHERE Node = @nodeID", conStr);
            //p1 = new SqlParameter("@nodeID", this.ddlModule.SelectedItem.Value);
            //cmd.Parameters.Add(p1);
            //reader = cmd.ExecuteReader();

            //while (reader.Read() && !reader.IsDBNull(0))
            //{
            //    txtLinks.Text += reader.GetString(0) + ";" + reader.GetString(1) + Environment.NewLine;
            //}
            //reader.Close();
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
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
         //insert module details
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("UPDATE Nodes SET Name = @name, Description = @desc WHERE Node_Id = @nodeID", conStr);
            SqlParameter p1 = new SqlParameter("@name", txtNode.Text);
            SqlParameter p2 = new SqlParameter("@desc", txtNodeDesc.Text);
            SqlParameter p3 = new SqlParameter("@nodeID", this.ddlModule.SelectedItem.Value);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);

            int x = cmd.ExecuteNonQuery();
            fillModules();
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