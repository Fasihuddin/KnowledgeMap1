using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class InstLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            // Check if the user is already loged in or not
            if ((Session["Check"] != null) && (Convert.ToBoolean(Session["Check"]) == true))
            {
                // If User is Authenticated then moved to a main page
                if (User.Identity.IsAuthenticated)
                    Response.Redirect("~/addCourse.aspx");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
    {
        Boolean blnresult;
        blnresult = false;

        // Pass UserName  and Password from login1 control to an authentication function which will check will check the user name and password from sql server.
        // Then will retrun a true or false value into blnresult variable
        blnresult = Authentication(Login1.UserName, Login1.Password);

        // If blnresult has a true value then authenticate user 
        if (blnresult == true)
        {
            // This is the actual statement which will authenticate the user
            e.Authenticated = true;
            // Store authentication mode in session variable 
            Session["Check"] = true;
        }
        else
            // If user faild to provide valid user name and password
            e.Authenticated = false;

    }

    // Function name Authentication which will get check the user_name and passwrod from sql database then return a value true or false
    protected Boolean Authentication(string username, string password)
    {
        string sqlstring;
        sqlstring = "SELECT Inst_Id FROM Instructors WHERE Inst_Id='" + username + "' AND Password ='" + password + "'";

        // create a connection with sqldatabase 
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand(sqlstring, conStr);
        // open a connection with sqldatabase
        conStr.Open();

        // execute sql command and store a return values in reader
        reader = cmd.ExecuteReader();

        // check if reader has any value then return true otherwise return false
        if (reader.Read())
        {
            string InstId = reader.GetString(0);
            Session["InstId"] = InstId;
            return true;
        }
        else
        {
            return false;
        }

    }
}
