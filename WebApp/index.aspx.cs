using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class index : System.Web.UI.Page
{
    private int test = 10;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == true)
        {

        }
        else
        {
            Session["value"] = test;
        }
    }
    protected void btnTest_Click(object sender, EventArgs e)
    {
        test = Convert.ToInt32(Session["value"]);
        test += 20;
        Session["value"] = test;
        txtResult.Text = test.ToString();
    }
}