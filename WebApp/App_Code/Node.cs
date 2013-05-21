using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Node
/// </summary>
public class Node
{
    private int nodeId;
    private String name;
    private String description;
    private int topicId;
    private List<int> prereq;
    private int degree; // degree is used to show the hierarchy of the nodes (it is based on prerequisites)
<<<<<<< HEAD
    private int startX, startY;
    public int position { get; set; }
    public int noLeft { get; set;}
=======
<<<<<<< HEAD

=======
    private int startX, startY;
    public int position { get; set; }
    public int noLeft { get; set;}
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8

	public Node()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public Node(int nodeId, String name, string description, int topicId, int degree)
    {
        this.nodeId = nodeId;
        this.name = name;
        this.description = description;
        this.topicId = topicId;
        this.degree = degree;
        prereq = new List<int>();
    }

    public int NodeId
    {
        get { return nodeId; }
        set { nodeId = value; }
    }

    public String Name
    {
        get { return name; }
        set { name = value; }
    }

    public String Description
    {
        get { return description; }
        set { description = value; }
    }

    public int TopicId
    {
        get { return topicId; }
        set { topicId = value; }
    }

    public int Degree
    {
        get { return degree; }
        set { degree = value; }
    }

    public void addPrerequisite(int nodeId)
    {
        this.prereq.Add(nodeId);
    }

    public List<int> getPrereq
    {
        get { return prereq; }
    }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8

    public int StartY
    {
        get { return startY; }
        set { startY = value; }
    }

    public int StartX
    {
        get { return startX; }
        set { startX = value; }
    }
<<<<<<< HEAD
=======
>>>>>>> 21/05/2013
>>>>>>> 83df69e5b6af54df5e444f5c49d5720769c876c8
}