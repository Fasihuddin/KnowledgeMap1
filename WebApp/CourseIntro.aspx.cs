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
                
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand("SELECT Course_Id, Name, Code FROM Course", conStr);
        // Try to open database and read information.
        try
        {
            conStr.Open();
            reader = cmd.ExecuteReader();
            // For each item, add the Course name to the displayed
            // list box text
            while (reader.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = reader["Code"] + ", " + reader["Name"];
                newItem.Value = reader["Course_Id"].ToString();
                ListCourse.Items.Add(newItem);
            }
            reader.Close();
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
}


