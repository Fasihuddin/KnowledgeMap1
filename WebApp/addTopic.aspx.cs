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

public partial class addTopic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {

    }
    protected void btnShowMap_Click(object sender, EventArgs e)
    {

        Session["TopicID"] = 1;
        Session["CourseID"] = 1;
        Response.Redirect("~/saveMap.aspx");
    }
}