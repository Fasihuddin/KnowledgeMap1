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
        //TEST ONLY - PLEASE DELETE
        Session["TopicID"] = 1;
        Session["CourseID"] = 1;
<<<<<<< HEAD
        int studentId = 1;
=======
<<<<<<< HEAD
        int studentId = 1;
=======
<<<<<<< HEAD
=======
        int studentId = 1;
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19

        Byte[] imageByte = drawKnowledgeMap();
        if (imageByte != null)
        {
            //Setting the canvas for the bitmap image
<<<<<<< HEAD
            Bitmap oCanvas = new Bitmap(1002, 802);
            //Declare a graphics to draw the image
            Graphics g = Graphics.FromImage(oCanvas);
=======
<<<<<<< HEAD
            Bitmap oCanvas = new Bitmap(1002, 802);
            //Declare a graphics to draw the image
            Graphics g = Graphics.FromImage(oCanvas);
=======
<<<<<<< HEAD
            Bitmap oCanvas = new Bitmap(1000, 700);
            //Declare a graphics to draw the image
            Graphics g = Graphics.FromImage(oCanvas);
            // draw the line on the perimeter of the canvas
            g.FillRectangle(Brushes.White, 1, 1, 678, 568);
=======
            Bitmap oCanvas = new Bitmap(1002, 802);
            //Declare a graphics to draw the image
            Graphics g = Graphics.FromImage(oCanvas);
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19

            // Draw the image using the data byte from database
            Bitmap objImage = new Bitmap(new System.IO.MemoryStream(imageByte));
            g.DrawImage(objImage, 0, 0);

            //draw region
<<<<<<< HEAD
            drawRegion(g, studentId);
=======
<<<<<<< HEAD
            drawRegion(g, studentId);
=======
<<<<<<< HEAD
            drawRegion(g);
=======
            drawRegion(g, studentId);
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19

            // Draw the canvas with the image and regions.
            Response.ContentType = "image/jpeg";
            oCanvas.Save(Response.OutputStream, ImageFormat.Jpeg);
            g.Dispose();
            oCanvas.Dispose();
            Response.End();
        }//end if
    }

    private Byte[] drawKnowledgeMap(){
        Byte[] imageByte = null;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //get image from topic table in the database
            SqlCommand cmd = new SqlCommand("SELECT img_data FROM Topic WHERE Topic_id = @topicID AND Course = @courseID", conStr);
            SqlParameter p1 = new SqlParameter("@topicID", Convert.ToInt32(Session["TopicID"]));
            SqlParameter p2 = new SqlParameter("@courseID", Convert.ToInt32(Session["CourseID"]));
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

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

<<<<<<< HEAD
    private void drawRegion(Graphics g, int studentId)
=======
<<<<<<< HEAD
    private void drawRegion(Graphics g, int studentId)
=======
<<<<<<< HEAD
    private void drawRegion(Graphics g)
=======
    private void drawRegion(Graphics g, int studentId)
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
    {
        GraphicsPath region = new GraphicsPath();
        List<int> nodeId = new List<int>();
        List<String> loc = new List<string>();
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);

        try
        {
            conStr.Open();
            //get image from topic table in the database
            SqlCommand cmd = new SqlCommand("SELECT Node_Id, NodeLocation FROM NodeOnCourse WHERE Topic_Id = @topicID AND Course_Id = @courseID", conStr);
            SqlParameter p1 = new SqlParameter("@topicID", Convert.ToInt32(Session["TopicID"]));
            SqlParameter p2 = new SqlParameter("@courseID", Convert.ToInt32(Session["CourseID"]));
            cmd.Parameters.Add(p1);
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
           
<<<<<<< HEAD
            //this variable will be used to store all passedNodes
            List<int> passedNodes = new List<int>();
=======
<<<<<<< HEAD
            //this variable will be used to store all passedNodes
            List<int> passedNodes = new List<int>();
=======
<<<<<<< HEAD
=======
            //this variable will be used to store all passedNodes
            List<int> passedNodes = new List<int>();
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19

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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
                    cmd = new SqlCommand("SELECT Is_complete from Test WHERE Node_Id = @nodeID", conStr);
                    p1 = new SqlParameter("@nodeID", nodeId[i]);
                    cmd.Parameters.Add(p1);
=======
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
                    cmd = new SqlCommand("SELECT Student_test.IsPassed FROM Student_test INNER JOIN TEST ON "+
                                        "Student_test.Test_Id = Test.Test_Id WHERE Test.Node_Id = @nodeID AND " +
                                        "Student_test.Student_Id = @studentID", conStr);
                    p1 = new SqlParameter("@nodeID", nodeId[i]);
                    p2 = new SqlParameter("@studentID", studentId);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        int test = reader.GetInt32(0);
                        if (test == 1)
                        {
                            isComplete = true;
<<<<<<< HEAD
                            passedNodes.Add(nodeId[i]);
=======
<<<<<<< HEAD
                            passedNodes.Add(nodeId[i]);
=======
<<<<<<< HEAD
=======
                            passedNodes.Add(nodeId[i]);
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
<<<<<<< HEAD
            }
=======
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
            }//end for

            //add all passed nodes to the session.
            Session["isPassed"] = passedNodes;
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
>>>>>>> 1e397fcbd8c876c0ba97a91b22b22f7fba66ec19
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
