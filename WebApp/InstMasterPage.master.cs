using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;


public partial class InstMasterPage : System.Web.UI.MasterPage
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
                SqlCommand cmd = new SqlCommand("select Name from Instructors where Inst_Id='" + Page.User.Identity.Name + "'", conStr);
                string username = cmd.ExecuteScalar().ToString();
                // If User is Authenticated then show the user name
                if (Page.User.Identity.IsAuthenticated)
                {
                    UserLabel.Text = "Welcome " + username + "  ";
                    //show logout link
                    LoginStatus1.Visible = true;
                }
            }
            else
            {
                UserLabel.Text = "Welcome guest";
                //show login link
                LoginLink.Visible = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    protected void LoginStatus1_LoggingOut(object sender, LoginCancelEventArgs e)
    {

    }
}

