using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.IO;

public partial class CourseNode : System.Web.UI.Page
{
    SqlConnection conStr;
    Bitmap bmpDiagram;
    Graphics g;
    GraphicsPath Node;
    int Xcanvas;
    int Ycanvas;
    int maxDegree;
    List<String> nodesLocation; //this variable is used to store the location of all nodes

    private void Page_Load(object sender, System.EventArgs e)
    {
        //initial setup of maxDegree and nodesLocation
        maxDegree = 0;
        nodesLocation = new List<String>();

        // Generate the drawing canvas
        Xcanvas = 1000;
        Ycanvas = 700;
        bmpDiagram = new Bitmap(Xcanvas, Ycanvas);
        g = Graphics.FromImage(bmpDiagram);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.HighQuality;

        // Generate the shape of a node
        Node = new GraphicsPath();
        Node.AddEllipse(1, 1, 74, 34);

        // Draw the parent node
        this.DrawNode(g, Node, Color.Red, (Xcanvas/2)-37, 10, "Parent Node");

        List<Node> allNodes = getAllNodes(1, Convert.ToInt32(Session["TopicID"]));
        assignNodePrereq(allNodes, Convert.ToInt32(Session["CourseID"]));

        //for each degree
        for (int i = 1; i <= maxDegree; i++)
        {
            List<String> nodesPerDegree = new List<String>();
            List<int> nodesIdPerDegree = new List<int>();
            for (int j = 0; j < allNodes.Count; j++)
            {
                if (allNodes[j].Degree == i)
                {
                    nodesPerDegree.Add(Convert.ToString(allNodes[j].NodeId)+";"+allNodes[j].Name);
                    nodesIdPerDegree.Add(allNodes[j].NodeId);
                }
            }
            DrawNodePerDegree(nodesPerDegree, allNodes, i);
        }

        //TESTING - PLEASE DELETE
       // this.DrawNode(g, Node, Color.Blue, 500, 2 * 100, "Node test");
        //this.DrawNode(g, Node, Color.Blue, 500, 3 * 100, "Node test");

        //this.DrawPerson(g, Node, Color.Blue, 50, 170, "Node 1");
        //this.ConnectPerson(g, 80, 160, 230, 110,
        //   Color.Blue);
        //this.DrawPerson(g, Node, Color.Blue,
        //   150, 170, "Node 2");
        //this.ConnectPerson(g, 180, 160, 230, 110,
        //   Color.Blue);
        //this.DrawPerson(g, Node, Color.Green,
        //   250, 170, "Node 3");
        //this.ConnectPerson(g, 280, 160, 230, 110,
        //   Color.Green);
        //this.DrawPerson(g, Node, Color.Green,
        //   350, 170, "Node 4");
        //this.ConnectPerson(g, 380, 160, 230, 110,
        //   Color.Green);

        // Save the completed diagram into 
        // the output stream
        Response.ContentType = "image/jpeg";
        bmpDiagram.Save(Response.OutputStream, ImageFormat.Jpeg);

        //convert to memory byte and save it into a session
        MemoryStream memImage = new MemoryStream();
        bmpDiagram.Save(memImage, ImageFormat.Jpeg);
        byte[] Byte = memImage.ToArray();
        //Save the image data and nodes location to the session for database storing in saveMap.aspx
        Session["ImageData"] = Byte;
        Session["NodesLocation"] = this.nodesLocation;
        Response.End();
    }

    /*
     * This method is used to get all nodes from database.
     * It retrieve all nodes based on a topic id
     * */
    private List<Node> getAllNodes(int courseId, int topicId)
    {
        //Create a list to hold all nodes from database
        List<Node> allNodes = new List<Node>();

        //Get all nodes from Database based on each topic
        conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name, Nodes.Description, NodeOnCourse.Degree "+
                             "FROM NodeOnCourse INNER JOIN Nodes ON NodeOnCourse.Node_ID = Nodes.Node_Id "+
                             "WHERE (NodeOnCourse.Course_Id = @courseID) AND (NodeOnCourse.Topic_Id = @topicID)",conStr);
            SqlParameter p1 = new SqlParameter("@courseID", courseId);
            SqlParameter p2 = new SqlParameter("@topicID", topicId);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                int nodeId = reader.GetInt32(0);
                String name = reader.GetString(1);
                String desc = reader.GetString(2);
                int degree = reader.GetInt32(3);
                //Create a new node object
                Node newNode = new Node(nodeId, name, desc, topicId, degree);
                //add node object to a list of nodes
                allNodes.Add(newNode);

                //check for the maximum degree
                if (maxDegree < degree)
                {
                    maxDegree = degree;
                }
            }
            reader.Close();
            //close SQL connection
            conStr.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return allNodes;
    }

    /*
     * This method is used to add the pre-requisites nodes to each node
     * Further, this method also assign the degree/level of hierarcy for each node in the bitmap
     * */
    private void assignNodePrereq(List<Node> allNodes, int courseId)
    {
        conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            for (int i = 0; i < allNodes.Count; i++)
            {
                conStr.Open();
                //get all pre-requisites courses and add those to node object
                SqlCommand cmd = new SqlCommand("SELECT Prerequisites.Prereq_ID FROM NodeOnCourse INNER JOIN "+
                                "Prerequisites ON NodeOnCourse.Node_ID = Prerequisites.Node_ID AND NodeOnCourse.Course_ID "+
                                "= Prerequisites.Course_ID WHERE (Prerequisites.Node_ID = @nodeID) AND (Prerequisites.Course_ID = @courseID)",conStr);
                    
                SqlParameter p1 = new SqlParameter("@nodeID", allNodes[i].NodeId);
                SqlParameter p2 = new SqlParameter("@courseID", courseId);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                SqlDataReader reader2 = cmd.ExecuteReader();
                
                while (reader2.Read())
                {
                    int prereqId = reader2.GetInt32(0);
                    allNodes[i].addPrerequisite(prereqId);
                }
                reader2.Close();
                conStr.Close();
            } // end for

            /*
            //The following code is used for determining the degree/level of hierarchy
            //This degree of hierarchy is important for node representation in the bitmap
            for (int i = 0; i < allNodes.Count; i++)
            {
                int degree = 0;
                if (allNodes[i].getPrereq.Count > 0)
                {
                    foreach (int j in allNodes[i].getPrereq)
                    {
                        for (int k = 0; k < allNodes.Count; k++)
                        {
                            //look for the node object that contains the node id j
                            if (allNodes[k].NodeId == j)
                            {
                                int parentDegree = allNodes[k].Degree;
                                if (degree <= parentDegree)
                                {
                                    degree = parentDegree + 1;
                                }
                            }// end second if
                        }// end for
                    }// end for each
                }// end first if

                //assign degree
                allNodes[i].Degree = degree;
            
            }// end first for
             *  * */
        }// end try
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    private void DrawNodePerDegree(List<String> NodesPerDegree, List<Node> allNodes, int degree)
    {
        //Testing -- CHANGE
        int noOfNodes = NodesPerDegree.Count; //Get no of nodes from database for each degree

        if (noOfNodes <= 10)
        {
            int leftNodes = 0;
            int rightNodes = 0;
            //Check whether the noOfNodes are even numbers. This is to specify the no of nodes in the 
            //left and right hand sides of the midCanvas
            if (noOfNodes % 2 != 0)
            {
                leftNodes = Math.DivRem(noOfNodes, 2, out rightNodes);
                rightNodes = leftNodes + 1;
            }
            else
            {
                leftNodes = noOfNodes / 2;
                rightNodes = leftNodes;
            }

            int startX = (Xcanvas / 2) + 10;
            int startY = degree*100;
            //Draw nodes on the left hand side of the parent nodes
            for (int i = 0; i < leftNodes; i++)
            {
                //get node Info (id and name)
                String[] nodeInfo = NodesPerDegree[i].ToString().Split(';');
                startX -= 100; // the width between node is 100px
                this.DrawNode(g, Node, Color.Blue, startX, startY, nodeInfo[1]);
                this.ConnectNode(g, 500, 70, startX + 30, startY, Color.Red);

                //add the location of node to the list (for clicking feature)
                String nodeLoc = nodeInfo[0]+";"+Convert.ToString(startX) + ";"+
                                Convert.ToString(startX + 74)+ ";"+ Convert.ToString(startY)+ ";"+
                                Convert.ToString(startY + 34);
                this.nodesLocation.Add(nodeLoc);
            }

            //Draw nodes on the right hand side of the parent nodes
            startX = (Xcanvas / 2) + 10;
            for (int i = leftNodes; i < NodesPerDegree.Count; i++)
            {
                //get node Info (id and name)
                String[] nodeInfo = NodesPerDegree[i].ToString().Split(';');
                this.DrawNode(g, Node, Color.Blue, startX, startY, nodeInfo[1]);
                this.ConnectNode(g, 500, 70, startX + 30, startY, Color.Red);

                //add the location of node to the list (for clicking feature)
                String nodeLoc = nodeInfo[0] + ";" + Convert.ToString(startX) + ";" +
                                Convert.ToString(startX + 74) + ";" + Convert.ToString(startY) + ";" +
                                Convert.ToString(startY + 34);
                this.nodesLocation.Add(nodeLoc);

                startX += 100;// the width between node is 100px
            }
        }
        else
        {
            Console.WriteLine("Please increase the size of the canvas");
        }
    }

    private void DrawNode(Graphics graphics,
       GraphicsPath Shape, Color fill, float x,
       float y, string Name)
    {
        // Position the shape
        graphics.TranslateTransform(x, y);

        // Draw the node and fill it 
        // with a gradient
        System.Drawing.Brush oBrush =
           new LinearGradientBrush(
           new Rectangle(0, 0, 60, 90),
           Color.White, fill, 90, true);
        graphics.FillPath(oBrush, Shape);
        graphics.DrawPath(Pens.Black, Shape);

        // Draw the name
        StringFormat sf = new StringFormat();
        sf.Alignment = StringAlignment.Center;
        Font oFont = new Font("Tahoma", 10);
        System.Drawing.SizeF size =
           graphics.MeasureString(Name, oFont);
        System.Drawing.RectangleF rect =
           new RectangleF(38 - (size.Width / 2),
           40, size.Width, size.Height);
        graphics.DrawString(Name, oFont,
           Brushes.Black, rect, sf);

        // Rset the coordinate shift
        graphics.ResetTransform();
    }

    public void ConnectNode(Graphics g, float x1,
       float y1, float x2, float y2, Color lineColor)
    {
        // Draw a line with a custom arrow-head
        Pen p = new Pen(lineColor, 1);
        CustomLineCap myCap =
           new AdjustableArrowCap(5, 5, true);
        p.EndCap = LineCap.Custom;
        p.CustomEndCap = myCap;
        g.DrawLine(p, x1, y1, x2, y2);
    }

    

   

}