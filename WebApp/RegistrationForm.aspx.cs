using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class RegistrationForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
    SqlCommand cmd;

    protected void Submit_Click(object sender, EventArgs e)
    {
        int added = 0;
       // bool usernameExists = false;
        bool idExists = false;
        try
        {
            conStr.Open();

            // create a command to check if the id exists

            using (SqlCommand cmd = new SqlCommand("select count(*) from Students where Student_Id = @Student_Id", conStr))
            {
                cmd.Parameters.AddWithValue("Student_Id", TBId.Text);
                idExists = (int)cmd.ExecuteScalar() > 0;
            }

            //// create a command to check if the username exists
            //using (SqlCommand cmd = new SqlCommand("select count(*) from Students where Username = @UserName", conStr))
            //{
            //    cmd.Parameters.AddWithValue("UserName", TBUsername.Text);
            //    usernameExists = (int)cmd.ExecuteScalar() > 0;
            //}

            // if username exists, show a message error
            if (idExists)
            {
                LblResults.ForeColor = System.Drawing.Color.Red;
                if (idExists)
                    LblResults.Text = "This student id is already registered.";
            }
            else
            {

                cmd = new SqlCommand("INSERT INTO Students VALUES ('" + TBId.Text + "','" + TBName.Text + "','" + TBEmail.Text + "','" + TBPassword.Text + "','" + TBSecQ.Text + "','" + TBSecA.Text + "')", conStr);
                added = cmd.ExecuteNonQuery(); // to get the number of affected record
                if (added > 0)
                {
                    LblResults.ForeColor = new System.Drawing.Color();
                    LblResults.Text = "You have successfuly registered";
                }
            }
        }
        catch (Exception err)
        {
            LblResults.Text = "Error creating an account. ";
            LblResults.Text += err.Message;
        }
        finally
        {
            conStr.Close();
        }
    }
}