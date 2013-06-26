using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addNodesPrereq : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session["topicID"] = 3; // DELETE LATER~!!!! TEST ONLY

            int topicID = Convert.ToInt32(Session["topicID"]);
            List<Node> tn = new List<Node>();
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name, Nodes.Description FROM NodeOnTopic INNER JOIN" +
                            " Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id WHERE (NodeOnTopic.Topic_Id = @topicId)", conStr);
                SqlParameter p1 = new SqlParameter("@topicId", topicID);
                cmd.Parameters.Add(p1);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tn.Add(new Node(reader.GetInt32(0), reader.GetString(1), reader.GetString(2)));
                    
                }
                reader.Close();

                //bind to gridview
                GridView1.DataSource = tn;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                ex.ToString();
                Response.Redirect("~/Error.aspx");
            }
            finally
            {
                conStr.Close();
            }
        }
    }

    protected void GridView1_RowCommand(object sender,GridViewCommandEventArgs e)
    {
        if (e.CommandName == "btnAdd")
        {
            // Retrieve the row index stored in the 
            // CommandArgument property.
            int index = Convert.ToInt32(e.CommandArgument);
            //get Node ID
            int nodeID = Convert.ToInt32(GridView1.Rows[index].Cells[0].Text);
            ViewState["nodeID"] = nodeID;
            ViewState["row"] = index;

            //clear all items in listview and make panel visible
            lstModules.Items.Clear();
            Panel2.Visible = true;

            lblModNo.Text = nodeID.ToString();

            //assign all modules to the listview
            for(int i=0; i<GridView1.Rows.Count; i++)
            {
                int node = Convert.ToInt32(GridView1.Rows[i].Cells[0].Text);
                if(node != nodeID){
                    ListItem lstItems = new ListItem();
                    lstItems.Value = Convert.ToString(node);
                    lstItems.Text = GridView1.Rows[i].Cells[1].Text;
                    lstModules.Items.Add(lstItems);
                }
            }// end for

        }//end if

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        
        List<int> parentNodes = new List<int>();
        List<String> prereqList = new List<String>();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            String prereq = GridView1.Rows[i].Cells[2].Text;
            if (!prereq.Equals(""))
            {
                prereqList.Add(GridView1.Rows[i].Cells[0].Text + ";" + prereq);
            }
            else
            {
                int node = Convert.ToInt32(GridView1.Rows[i].Cells[0].Text);
                //add parent nodes
                parentNodes.Add(node);

                //set the degree of parent node
                setDegree(1, node);
            }//end else
        }// end for

        //set degree for all other nodes
        int counter = 1;
        int degree = 2;
        int startingIndex = 0;

        //Algorithm for adding degree for each module
        do
        {
            counter = 0;
            int lengthParentNode = parentNodes.Count;
                List<int> temp = new List<int>();
                foreach (String val in prereqList)
                {
                    string[] requestLoc = new string[2];
                    requestLoc = val.Split(';');
                    for (int k = 1; k < requestLoc.Length; k++)
                    {
                        int tempNo = -1;
                        for (int i = startingIndex; i < parentNodes.Count; i++)
                        {
                            if (!requestLoc[k].Equals("") && Convert.ToInt32(requestLoc[k]) == parentNodes[i])
                            {
                                setDegree(degree, Convert.ToInt32(requestLoc[0]));
                                if (Convert.ToInt32(requestLoc[0]) != tempNo)
                                {
                                    temp.Add(Convert.ToInt32(requestLoc[0]));
                                    tempNo = Convert.ToInt32(requestLoc[0]);
                                }
                                counter = 1;
                            }//end if
                        }//end inner for
                    }//end outer for
                }// end foreach


                parentNodes.AddRange(temp);
                startingIndex = lengthParentNode;
                degree++;
            
        } while (counter != 0); //end while


        //open saveMap.aspx to generate the knowledge map
        Response.Redirect("~/saveMap.aspx");
    }

    private void setDegree(int degree, int nodeId)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("UPDATE NodeOnTopic SET Degree = @degree WHERE Node_Id = @nodeId AND Topic_ID= @topicId", conStr);
            SqlParameter p1 = new SqlParameter("@nodeId", nodeId);
            SqlParameter p2 = new SqlParameter("@topicId", Convert.ToInt32(Session["topicID"]));
            SqlParameter p3 = new SqlParameter("@degree", degree);
            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            int x = cmd.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            conStr.Close();
        }

    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();

            for (int i = lstModules.Items.Count - 1; i >= 0; i--)
            {
                if (lstModules.Items[i].Selected == true)
                {

                    //Insert new topic to Topic DB
                    SqlCommand cmd = new SqlCommand("INSERT INTO Prerequisites VALUES (@nodeId, @topicId, @PrereqId)", conStr);
                    SqlParameter p1 = new SqlParameter("@nodeId", Convert.ToInt32(ViewState["nodeID"]));
                    SqlParameter p2 = new SqlParameter("@topicId", Convert.ToInt32(Session["topicID"]));
                    SqlParameter p3 = new SqlParameter("@PrereqId", Convert.ToInt32(lstModules.Items[i].Value));
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    int x = cmd.ExecuteNonQuery();

                    int row = Convert.ToInt32(ViewState["row"]);
                    //add prerequisite modules to the gridview column 2
                    GridView1.Rows[row].Cells[2].Text += lstModules.Items[i].Value+";";
                }
            }

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Response.Redirect("~/Error.aspx");
        }
        finally
        {
            conStr.Close();
        }
        //make panel become invisible
        Panel2.Visible = false;
    }
}