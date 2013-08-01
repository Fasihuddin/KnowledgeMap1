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

        //Get topic ID and courseID
        int topicId = Convert.ToInt32(Session["TopicID"]);
        int courseId = Convert.ToInt32(Session["CourseID"]);

        // Generate the drawing canvas
        Xcanvas = 1002;
        Ycanvas = 802;
        bmpDiagram = new Bitmap(Xcanvas, Ycanvas);
        g = Graphics.FromImage(bmpDiagram);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.HighQuality;

        //create canvas border
        Pen myPen = new Pen(Color.Black, 1);
        g.DrawRectangle(myPen, 1, 1, 1000, 800);

        // Generate the shape of a node
        Node = new GraphicsPath();
        Node.AddEllipse(1, 1, 74, 34);

        // Draw the parent node
        this.DrawNode(g, Node, Color.Red, (Xcanvas/2)-37, 10, "Topic");

        //get all nodes and its prerequisites.
        List<Node> allNodes = getAllNodes(topicId);
        assignNodePrereq(allNodes, topicId);

        //for each degree
        for (int i = 1; i <= maxDegree; i++)
        {
            List<Node> nodesPerDegree = new List<Node>();
            for (int j = 0; j < allNodes.Count; j++)
            {
                if (allNodes[j].Degree == i)
                {
                    nodesPerDegree.Add(allNodes[j]);
                }
            }
            if (i == 1)
            {
                DrawNodePerDegree(nodesPerDegree, allNodes, i);
            }
            else
            {
                drawOtherDegrees(nodesPerDegree, allNodes, i);
            }
        }

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
     * query database to get the Topic Name from topicID
     * */
    private String getTopicName(int topicId)
    {
        String topicName = "";

        //Get all nodes from Database based on each topic
        conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Name FROM Topic WHERE (Topic_Id = @topicID)", conStr);
            SqlParameter p1 = new SqlParameter("@topicID", topicId);
            cmd.Parameters.Add(p1);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                topicName = reader.GetString(0);
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
        finally
        {
            conStr.Close();
        }
        return topicName;
    }

    /*
     * This method is used to get all nodes from database.
     * It retrieve all nodes based on a topic id
     * */
    private List<Node> getAllNodes(int topicId)
    {
        //Create a list to hold all nodes from database
        List<Node> allNodes = new List<Node>();

        //Get all nodes from Database based on each topic
        conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name, Nodes.Description, NodeOnTopic.Degree " +
                             "FROM NodeOnTopic INNER JOIN Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id " +
                             "WHERE (NodeOnTopic.Topic_Id = @topicID)", conStr);
            SqlParameter p2 = new SqlParameter("@topicID", topicId);
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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            //close SQL connection
            conStr.Close();
        }
        return allNodes;
    }

    /*
     * This method is used to add the pre-requisites nodes to each node
     * Further, this method also assign the degree/level of hierarcy for each node in the bitmap
     * */
    private void assignNodePrereq(List<Node> allNodes, int topicId)
    {
        conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        for (int i = 0; i < allNodes.Count; i++)
        {
            try
            {
            
                conStr.Open();
                //get all pre-requisites courses and add those to node object
                SqlCommand cmd = new SqlCommand("SELECT Prereq_ID FROM Prerequisites " +
                                "WHERE (Node_ID = @nodeID) AND (Topic_ID = @topicID)", conStr);

                SqlParameter p1 = new SqlParameter("@nodeID", allNodes[i].NodeId);
                SqlParameter p2 = new SqlParameter("@topicID", topicId);
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                SqlDataReader reader2 = cmd.ExecuteReader();

                Console.WriteLine(allNodes[i].NodeId + "-" + allNodes[i].Name);

                while (reader2.Read())
                {
                    int prereqId = reader2.GetInt32(0);
                    allNodes[i].addPrerequisite(prereqId);
                }
                reader2.Close();
                conStr.Close();
            }// end try
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                conStr.Close();
            }
        } // end for
    }

    private void DrawNodePerDegree(List<Node> NodesPerDegree, List<Node> allNodes, int degree)
    {
        //Get no of nodes for each degree
        int noOfNodes = NodesPerDegree.Count; 

        if (noOfNodes <= 10) // maximum nodes per degree is 10
        {
            int leftNodes = 0;
            int rightNodes = 0;

            //Check whether the noOfNodes are even numbers. This is to specify the no of nodes in the 
            //left and right hand sides of the midCanvas
            if (noOfNodes % 2 != 0)
            {
                leftNodes = Math.DivRem(noOfNodes, 2, out rightNodes);
                if (rightNodes < 5)
                {
                    rightNodes = leftNodes + 1;
                }
                else
                {
                    leftNodes++;
                }
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
                startX -= 100; // the width between node is 100px
                this.DrawNode(g, Node, Color.Blue, startX, startY, NodesPerDegree[i].Name);

                //connecting node with its prerequisite nodes
                if (degree == 1)
                {
                    this.ConnectNode(g, 500, 70, startX + 36, startY, Color.Red);
                }
                else
                {
                    //get prerequisites nodes
                    foreach (int nodeId in NodesPerDegree[i].getPrereq)
                    {
                        foreach (Node n in allNodes)
                        {
                            if (n.NodeId == nodeId)
                            {
                                int x = n.StartX+36;
                                int y = n.StartY+55;
                                this.ConnectNode(g, x, y, startX + 36, startY, Color.Red);
                            }
                        }//end foreach2
                    }//end foreach1
                }

                //add the location of node to the list (for clicking feature)
                String nodeLoc = NodesPerDegree[i].NodeId.ToString()+";"+Convert.ToString(startX) + ";"+
                                Convert.ToString(startX + 74)+ ";"+ Convert.ToString(startY)+ ";"+
                                Convert.ToString(startY + 34);
                this.nodesLocation.Add(nodeLoc);
                NodesPerDegree[i].StartX = startX;
                NodesPerDegree[i].StartY = startY;
                NodesPerDegree[i].position = 0;
            }

            //Draw nodes on the right hand side of the parent nodes
            startX = (Xcanvas / 2) + 10;
            for (int i = leftNodes; i < NodesPerDegree.Count; i++)
            {
                this.DrawNode(g, Node, Color.Blue, startX, startY, NodesPerDegree[i].Name);

                //connecting node with its prerequisite nodes
                if (degree == 1)
                {
                    this.ConnectNode(g, 500, 70, startX + 36, startY, Color.Red);
                }
                else
                {
                    //get prerequisites nodes
                    foreach (int nodeId in NodesPerDegree[i].getPrereq)
                    {
                        foreach (Node n in allNodes)
                        {
                            if (n.NodeId == nodeId)
                            {
                                int x = n.StartX + 36;
                                int y = n.StartY + 55;
                                this.ConnectNode(g, x, y, startX + 36, startY, Color.Red);
                            }
                        }//end foreach2
                    }//end foreach1
                }
                
                //add the location of node to the list (for clicking feature)
                String nodeLoc = NodesPerDegree[i].NodeId.ToString() + ";" + Convert.ToString(startX) + ";" +
                                Convert.ToString(startX + 74) + ";" + Convert.ToString(startY) + ";" +
                                Convert.ToString(startY + 34);
                this.nodesLocation.Add(nodeLoc);
                NodesPerDegree[i].StartX = startX;
                NodesPerDegree[i].StartY = startY;
                NodesPerDegree[i].position = 1;
                startX += 100;// the width between node is 100px
            }
        }
        else
        {
            Console.WriteLine("Please increase the size of the canvas");
            
        }
    }

    private void drawOtherDegrees(List<Node> NodesPerDegree, List<Node> allNodes, int degree)
    {
         //Get no of nodes for each degree
        int noOfNodes = NodesPerDegree.Count;

        if (noOfNodes <= 10) // maximum nodes per degree is 10
        {
            assignNodePosition(NodesPerDegree, allNodes);
            int right = 0;
            int left = 1;

            int midCanvas = (Xcanvas / 2) + 10;
            int startY = degree * 100;

            foreach (Node n in NodesPerDegree)
            {
                int pos = n.position;
                if (pos == 0)
                {
                    int startX = midCanvas - (100 * left);

                    this.DrawNode(g, Node, Color.Blue, startX, startY, n.Name);

                    foreach (int nodeId in n.getPrereq)
                    {
                        foreach (Node u in allNodes)
                        {
                            if (u.NodeId == nodeId)
                            {
                                int x = u.StartX + 36;
                                int y = u.StartY + 55;
                                this.ConnectNode(g, x, y, startX + 36, startY, Color.Red);
                            }
                        }//end foreach2
                    }//end foreach1

                    //add the location of node to the list (for clicking feature)
                    String nodeLoc = n.NodeId.ToString() + ";" + Convert.ToString(startX) + ";" +
                                    Convert.ToString(startX + 74) + ";" + Convert.ToString(startY) + ";" +
                                    Convert.ToString(startY + 34);
                    this.nodesLocation.Add(nodeLoc);
                    n.StartX = startX;
                    n.StartY = startY;
                    n.position = 0;
                    left++;
                }
                else
                {
                    int startX = midCanvas + (right*100);
                    this.DrawNode(g, Node, Color.Blue, startX, startY, n.Name);

                    //get prerequisites nodes
                    foreach (int nodeId in n.getPrereq)
                    {
                        foreach (Node u in allNodes)
                        {
                            if (u.NodeId == nodeId)
                            {
                                int x = u.StartX + 36;
                                int y = u.StartY + 55;
                                this.ConnectNode(g, x, y, startX + 36, startY, Color.Red);
                            }
                        }//end foreach2
                    }//end foreach1

                    //add the location of node to the list (for clicking feature)
                    String nodeLoc = n.NodeId.ToString() + ";" + Convert.ToString(startX) + ";" +
                                    Convert.ToString(startX + 74) + ";" + Convert.ToString(startY) + ";" +
                                    Convert.ToString(startY + 34);
                    this.nodesLocation.Add(nodeLoc);
                    n.StartX = startX;
                    n.StartY = startY;
                    n.position = 1;
                    right++;
                }//end else
            }//end for each
        }//end if
    }

    private void assignNodePosition(List<Node> NodesPerDegree, List<Node> allNodes)
    {
        
        List<List<Node[]>> assignedNode = new List<List<Node[]>>();

        List<int> leftNodesPos = new List<int>();
        List<int> rightNodesPos = new List<int>();
        int right, left;
        int leftCounter = 0;
        int rightCounter = 0;

        for (int u=0; u<NodesPerDegree.Count;u++)
        {
            right = 0;
            left = 0;
            foreach (int i in NodesPerDegree[u].getPrereq)
            {
                foreach (Node k in allNodes)
                {
                    if (k.NodeId == i && k.position == 0)
                    {
                        left += 1;
                    }
                    else if (k.NodeId == i)
                    {
                        right+=1;
                    }
                }
            }
            if (left >= right && leftCounter <= 5)
            {
                NodesPerDegree[u].position = 0;
                NodesPerDegree[u].noLeft = left;
                leftNodesPos.Add(u);
                leftCounter++;

            }
            else if(rightCounter < 5)
            {
                NodesPerDegree[u].position = 1;
                NodesPerDegree[u].noLeft = left;
                rightNodesPos.Add(u);
                rightCounter++;
            }
            else if (rightCounter >= 5)
            {
                NodesPerDegree[u].position = 0;
                NodesPerDegree[u].noLeft = left;
                leftNodesPos.Add(u);
                leftCounter++;
            }
        }

        //check the no of prerequisites nodes in the left between left and right nodes
        //if left nodes have less prerequisites nodes in the left, swap with the right
        for (int i = 0; i < rightNodesPos.Count; i++) 
        {
            int noRight = NodesPerDegree[rightNodesPos[i]].noLeft;
            for (int j = 0; j < rightNodesPos.Count; j++)
            {
                int noLeft = NodesPerDegree[rightNodesPos[j]].noLeft;
                if (noRight > noLeft)
                {
                    NodesPerDegree[rightNodesPos[j]].position = 1;
                    NodesPerDegree[rightNodesPos[i]].position = 0;
                }
            }
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
       
        //Check the length of node name
        if(Name.Length >= 15){ // if the module has long name, write its name in 2 lines
            //first half of the name
            int midsize = Name.Length / 2;
            System.Drawing.SizeF size1 = graphics.MeasureString(Name.Substring(0,midsize), oFont);
            System.Drawing.RectangleF rect =
                new RectangleF(38 - (size1.Width / 2), 40, size1.Width, size1.Height);
            graphics.DrawString(Name.Substring(0, midsize), oFont,
                Brushes.Black, rect, sf);

            //second half of the name
            String secondHalf = Name.Substring(midsize, Name.Length - midsize);
            System.Drawing.SizeF size2 = graphics.MeasureString(secondHalf, oFont);
            System.Drawing.RectangleF rect2 =
                new RectangleF(38 - (size2.Width / 2), 50, size2.Width, size2.Height);
            graphics.DrawString(secondHalf, oFont,
                Brushes.Black, rect2, sf);
        }else{
           System.Drawing.SizeF size = graphics.MeasureString(Name, oFont);
           System.Drawing.RectangleF rect =
             new RectangleF(38 - (size.Width / 2), 40, size.Width, size.Height);
           graphics.DrawString(Name, oFont,
               Brushes.Black, rect, sf);
        }

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
        g.DrawLine(p, x1, y1+7, x2, y2);
    }

}