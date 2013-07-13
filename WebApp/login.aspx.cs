using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web.Security;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


public partial class login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //redirect to instructor login page if the return url is one of the instructor's pages
        if (!string.IsNullOrEmpty(Request.QueryString["returnUrl"]))
        {
            if ((Request.QueryString["returnUrl"].Contains("addCourse")) || (Request.QueryString["returnUrl"].Contains("addModuleTest")) || (Request.QueryString["returnUrl"].Contains("addNode"))
                || (Request.QueryString["returnUrl"].Contains("addNodesPrereq")) || (Request.QueryString["returnUrl"].Contains("addTopic")) || (Request.QueryString["returnUrl"].Contains("CreateMap"))
                || (Request.QueryString["returnUrl"].Contains("saveMap")))
                Response.Redirect("~/InstLogin.aspx");
        }
        try
        {
            // Check if the user is already loged in or not
            if ((Session["Check"] != null) && (Convert.ToBoolean(Session["Check"]) == true))
            {
                // If User is Authenticated then moved to a main page
                if (User.Identity.IsAuthenticated)
                    Response.Redirect("~/StdCourseIntro.aspx");
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

            // redirect to course intro if the requested page is restriced or if it is logout
            if (string.Equals(Request.QueryString["returnUrl"], "/WebApp/StdQuestionsForm.aspx") || string.Equals(Request.QueryString["returnUrl"], "/WebApp/testResult.aspx") )
            {
                //setAutCookie is used to enable redirecting to specific page
                FormsAuthentication.SetAuthCookie(Login1.UserName, false);
                Response.Redirect("~/StdCourseIntro.aspx");
            }
        }
        else
            // If user faild to provide valid user name and password
            e.Authenticated = false;
    }

    // Function name Authentication which will get check the user_name and passwrod from sql database then return a value true or false
    protected Boolean Authentication(string username, string password)
    {
        string sqlstring;
        sqlstring = "SELECT Student_Id FROM Students WHERE Student_Id='" + username + "' AND Password ='" + password + "'";

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
            int studentId = reader.GetInt32(0);
            Session["StudentID"] = studentId;
            return true;
        }
        else
        {
            return false;
        }

    }
}
