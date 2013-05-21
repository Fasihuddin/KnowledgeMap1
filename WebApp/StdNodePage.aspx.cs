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

    protected void Page_Load(object sender, EventArgs e)
    {
        //get node ID from url address
        String strNodeID = Request.QueryString["nid"];
        if (!IsPostBack && strNodeID != "" && strNodeID != null)
        {
            int nodeID = Convert.ToInt32(strNodeID);

            //call the method to bind material links to the gridview
            boundGridView(nodeID);

            //select randomTestID
            int testID = pickRandomTest(nodeID);
            Session["testID"] = testID;
        }
        else if (strNodeID == "" || strNodeID == null)
        {
            // Show Fail Javascript message
            System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
            System.Web.HttpContext.Current.Response.Write("alert('No Node ID has been Selected! Please select the nodeID first!')");
            System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
            Response.Redirect("~/StdShowTopicMap.aspx");
        }
       
    }

    /*
     * Binding material links from database to the gridview (based on Node ID)
     * */
    private void boundGridView(int nodeID)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        DataSet ds = new DataSet();
        try
        {
            // NEED TO ADD A PARAMETER Node ID. Use SESSION TO GET THE NODE ID!!!
            SqlCommand cmd = new SqlCommand("SELECT Name, URL_Address FROM Materials WHERE Node = @nodeID"+
                " ORDER BY Material_Order", conStr);
            SqlParameter p1 = new SqlParameter("@nodeID", nodeID);
            cmd.Parameters.Add(p1);
            SqlDataAdapter dtAdapt = new SqlDataAdapter();
            dtAdapt.SelectCommand = cmd;
            dtAdapt.Fill(ds, "links");

            DataTable dt = ds.Tables["links"];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        //bind to gridview
        grdLinks2.DataSource = ds;
        grdLinks2.DataBind();

    }

    private int pickRandomTest(int nodeID)
    {

        int testID = 0;
        List<int> allTests = new List<int>();
        try
        {
            //select all test versions for the node
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Test_Id FROM Test WHERE Node_Id = @nodeId", conStr);
            SqlParameter p1 = new SqlParameter("@nodeId", nodeID);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                allTests.Add(Convert.ToInt32(reader[0]));
            }
            //close sql connection
            reader.Close();
            conStr.Close();

            //get all passed nodes from the session. This session is filled in the stdTopicMap.aspx
            //Then check whether student has passed the node on any of its test versions
            List<int> passedNode = (List<int>)Session["isPassed"];
            int isPassed = 0;
            foreach (int i in passedNode)
            {
                if (i == nodeID)
                {
                    isPassed = 1;
                }
            }

            //generate random test no if student has not passed the test
            if (allTests.Count > 1 && isPassed == 0)
            {
                List<int> availableTest = new List<int>();
                conStr.Open();
                cmd = new SqlCommand("SELECT Test_Id FROM Test WHERE Test_Id NOT IN "+
                             "(SELECT Test.Test_Id FROM Test JOIN " +
                             "Student_test ON Test.Test_Id = Student_test.Test_Id " +
                             "WHERE Node_Id = @nodeId GROUP BY Test.Test_Id) AND Node_Id = @nodeId", conStr);
                p1 = new SqlParameter("@nodeId", nodeID);
                cmd.Parameters.Add(p1);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    availableTest.Add(Convert.ToInt32(reader[0]));
                }
                //close sql connection
                reader.Close();
                conStr.Close();

                if (availableTest.Count > 0)
                {
                    Random rand = new Random();
                    testID = availableTest[rand.Next(0, availableTest.Count)];
                    lblTestId.Text = testID.ToString();
                }
                else
                {
                    Random rand = new Random();
                    testID = allTests[rand.Next(0, allTests.Count)];
                    lblTestId.Text = testID.ToString();
                }
            }
            else if (isPassed == 1) // if student has passed the test, disable the start test button
            {
                testID = -1;
                lblTestId.Text = "You have passed the test for this node";
                btnStartTest.Enabled = false;
            }
            else //if test is not created for this node, disable the start test button
            {
                testID = -1;
                lblTestId.Text = "No test versions created for this node";
                btnStartTest.Enabled = false;
            }
        }//end try
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return testID;
    }

    protected void btnStartTest_Click(object sender, EventArgs e)
    {
        int testId = Convert.ToInt32(lblTestId.Text);
        Response.Redirect("~/StdQuestionsForm.aspx?id=" + testId);
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StdShowTopicMap.aspx");
    }
}