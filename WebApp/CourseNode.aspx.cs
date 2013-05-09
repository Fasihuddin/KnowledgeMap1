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

public partial class CourseNode : System.Web.UI.Page
{
    private void Page_Load(object sender, System.EventArgs e)
    {
        // Generate the drawing canvas
        int Xcanvas = 1000;
        int Ycanvas = 700;
        Bitmap bmpDiagram = new Bitmap(Xcanvas, Ycanvas);
        Graphics g = Graphics.FromImage(bmpDiagram);
        g.Clear(Color.White);
        g.SmoothingMode = SmoothingMode.HighQuality;

        // Generate the shape of a node
        GraphicsPath Node = new GraphicsPath();
        Node.AddEllipse(1, 1, 74, 34);


       
        // Draw the parent node
        this.DrawNode(g, Node, Color.Red, (Xcanvas/2)-37, 10, "Parent Node");

        //Testing -- CHANGE
        int degree = 1; // get it from database based on pre-requisite nodes
        int noOfNodes = 7; //Get no of nodes from database for each degree

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

            //Draw nodes on the left hand side of the parent nodes
            for (int i = 0; i < leftNodes; i++)
            {
                startX -= 100;
                this.DrawNode(g, Node, Color.Blue, startX, degree * 100, "Node " + i);
                this.ConnectNode(g, 500, 70 , startX+30, (degree*100), Color.Red);
            }

            //Draw nodes on the right hand side of the parent nodes
            startX = (Xcanvas / 2) + 10;
            for (int i = 0; i < rightNodes; i++)
            {
                this.DrawNode(g, Node, Color.Blue, startX, degree * 100, "Node " + i);
                this.ConnectNode(g, 500, 70,startX + 30, (degree*100), Color.Red);
                startX += 100;
            }
        }
        else
        {
            Console.WriteLine("Please increase the size of the canvas");
        }
        
        //TESTING - PLEASE DELETE
        this.DrawNode(g, Node, Color.Blue, 500, 2 * 100, "Node test");
        this.DrawNode(g, Node, Color.Blue, 500, 3 * 100, "Node test");

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
        Response.End();
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