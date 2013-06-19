using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
         try
        {
            // Check if the user is already loged in or not
            if ((Session["Check"] != null) && (Convert.ToBoolean(Session["Check"]) == true))
            {
                conStr.Open();
                SqlCommand cmd = new SqlCommand("select Name from Students where Student_Id="+ Page.User.Identity.Name , conStr);
                string username = cmd.ExecuteScalar().ToString();
                // If User is Authenticated then show the user name
                if (Page.User.Identity.IsAuthenticated)
                    UserLabel.Text = "Welcome "+username+"  ";
            }
            else
                UserLabel.Text = "Welcome guest";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
