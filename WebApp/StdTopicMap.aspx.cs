using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;

public partial class StdTopicMap : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //get both course and topic id
            String data = Session["CourseTopicID"].ToString();
            string[] courseTopicID = new string[2];
            courseTopicID = data.Split(';');
            int courseId = Convert.ToInt32(courseTopicID[0]);
            int topicId = Convert.ToInt32(courseTopicID[1]);

            Byte[] imageByte = drawKnowledgeMap(topicId);
            if (imageByte != null)
            {
                //Setting the canvas for the bitmap image
                Bitmap oCanvas = new Bitmap(1002, 802);
                //Declare a graphics to draw the image
                Graphics g = Graphics.FromImage(oCanvas);

                // Draw the image using the data byte from database
                Bitmap objImage = new Bitmap(new System.IO.MemoryStream(imageByte));
                g.DrawImage(objImage, 0, 0);

                if (User.Identity.IsAuthenticated)
                {
                    int studentId = Convert.ToInt32(Session["StudentID"]);
                    //draw region
                    drawRegion(g, studentId, topicId);
                }
                else
                {
                    getNodesLoc(topicId);
                }

                // Draw the canvas with the image and regions.
                Response.ContentType = "image/jpeg";
                oCanvas.Save(Response.OutputStream, ImageFormat.Jpeg);
                g.Dispose();
                oCanvas.Dispose();
                Response.End();
            }//end if

        }
        catch (System.Threading.ThreadAbortException lException)
        {

        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    private Byte[] drawKnowledgeMap(int topicId){
        Byte[] imageByte = null;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //get image from topic table in the database
            SqlCommand cmd = new SqlCommand("SELECT img_data FROM Topic WHERE Topic_id = @topicID", conStr);
            SqlParameter p1 = new SqlParameter("@topicID", topicId);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                imageByte = (Byte[])reader["img_data"];
            }

            //close sql connection
            reader.Close();
            conStr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return imageByte;
    }

    private void drawRegion(Graphics g, int studentId, int topicId)
    {
        GraphicsPath region = new GraphicsPath();
        List<int> nodeId = new List<int>();
        List<String> loc = new List<string>();
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);

        try
        {
            conStr.Open();
            //get image from topic table in the database
            SqlCommand cmd = new SqlCommand("SELECT Node_Id, NodeLocation FROM NodeOnTopic WHERE Topic_Id = @topicID", conStr);
            SqlParameter p2 = new SqlParameter("@topicID", topicId);
            cmd.Parameters.Add(p2);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nodeId.Add(reader.GetInt32(0));
                loc.Add(reader.GetString(1));
            }
            //close sqlconnection
            reader.Close();
            conStr.Close();
           
            //this variable will be used to store all passedNodes
            List<int> passedNodes = new List<int>();

            //create regions
            for (int i=0; i<nodeId.Count;i++)
            {
                if (loc[i] != null)
                {
                    int x1, y1, x2, y2;
                    string[] recLoc = new string[4];
                    string allLoc = loc[i].ToString();
                    recLoc = allLoc.Split(';');
                    x1 = Convert.ToInt32(recLoc[0]);
                    x2 = Convert.ToInt32(recLoc[1]);
                    y1 = Convert.ToInt32(recLoc[2]);
                    y2 = Convert.ToInt32(recLoc[3]);

                    //check test completion for each node
                    bool isComplete = false;

                    //conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                    conStr.Open();
                    cmd = new SqlCommand("SELECT Student_test.IsPassed FROM Student_test INNER JOIN TEST ON "+
                                        "Student_test.Test_Id = Test.Test_Id WHERE Test.Node_Id = @nodeID AND " +
                                        "Student_test.Student_Id = @studentID", conStr);
                    SqlParameter p1 = new SqlParameter("@nodeID", nodeId[i]);
                    p2 = new SqlParameter("@studentID", studentId);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int test = reader.GetInt32(0);
                        if (test == 1)
                        {
                            isComplete = true;
                            passedNodes.Add(nodeId[i]);
                        }
                    }
                    reader.Close();
                    conStr.Close(); 

                    if (isComplete)
                    {
                        // Draw the node and fill it 
                        // with a gradient
                        System.Drawing.Brush oBrush =
                           new LinearGradientBrush(
                           new Rectangle(0, 0, 60, 90),
                           Color.White, Color.Green, 90, true);

                        Pen myPen = new Pen(Color.Black, 1);
                        g.DrawEllipse(myPen, x1 + 1, y1 + 1, 74, 34);
                        g.FillEllipse(oBrush, x1 + 1, y1 + 1, 74, 34);
                    }
                    else
                    {
                        Pen myPen = new Pen(Color.Black, 1);
                        g.DrawEllipse(myPen, x1 + 1, y1 + 1, 74, 34);
                    }


                    //add nodes and locations to the sessions
                    Session["AllNodes"] = nodeId;
                    Session["recLocation"] = loc;
                }
            }//end for

            //add all passed nodes to the session.
            Session["isPassed"] = passedNodes;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void getNodesLoc(int topicId){
        List<int> nodeId = new List<int>();
        List<String> loc = new List<string>();
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);

        try
        {
            conStr.Open();
            //get image from topic table in the database
            SqlCommand cmd = new SqlCommand("SELECT Node_Id, NodeLocation FROM NodeOnTopic WHERE Topic_Id = @topicID", conStr);
            SqlParameter p2 = new SqlParameter("@topicID", topicId);
            cmd.Parameters.Add(p2);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                nodeId.Add(reader.GetInt32(0));
                loc.Add(reader.GetString(1));
            }
            //close sqlconnection
            reader.Close();


            //add nodes and locations to the sessions
            Session["AllNodes"] = nodeId;
            Session["recLocation"] = loc;
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            conStr.Close();
        }

    }
}
