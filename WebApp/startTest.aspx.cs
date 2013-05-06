///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                          Knowledge Map Web Application for Research                                           //
//                                   Created by Ilung Pranata                                                    //
//                                  Date created: 06-May-2013                                                    //
//  This page is used by student to select the node. Once node is selected, the page lists the contents of the   //
//  node (links, educational materials, etc) and also it randomly picks a test version from database for student //

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class startTest : System.Web.UI.Page
{
    SqlConnection conStr;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            //set label to non-visible
            lblTestId.Visible = false;

            conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);

            DataTable dt = new DataTable();
            try
            {
                conStr.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Nodes", conStr);
                dt.Load(cmd.ExecuteReader());
                conStr.Close();

                //bind nodes to dropdownlist
                this.ddlNodeList.DataSource = dt;
                this.ddlNodeList.DataTextField = "Node_Id";
                this.ddlNodeList.DataValueField = "Node_Id";
                this.ddlNodeList.DataBind();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
    protected void ddlNodeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblTestId.Visible = true;
        List<int> testId = new List<int>();
        int finalTestId;

        int nodeId = Convert.ToInt32(this.ddlNodeList.SelectedItem.Value);
        try
        {
            conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Test_Id FROM Test WHERE Node_Id = @nodeId", conStr);
            SqlParameter p1 = new SqlParameter("@nodeId", nodeId);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                testId.Add(Convert.ToInt32(reader[0]));
            }
            //close sql connection
            conStr.Close();

            //get random test ID 
            if (testId.Count > 1)
            {
                Random rand = new Random();
                finalTestId = testId[rand.Next(0, testId.Count)];
                lblTestId.Text = finalTestId.ToString();
            }
            else if(testId.Count == 0)
            {
                finalTestId = -1;
                lblTestId.Text = "No test versions created for this node";
            }
            else
            {
                finalTestId = testId[0];
                lblTestId.Text = finalTestId.ToString();
            }


            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }
    protected void btnStartTest_Click(object sender, EventArgs e)
    {
        int testId = Convert.ToInt32(lblTestId.Text);
        Response.Redirect("~/StdQuestionsForm.aspx?id=" + testId);
    }
}