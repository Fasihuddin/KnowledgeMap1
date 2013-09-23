using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

public partial class testResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        
        String sqlQuery;
        SqlCommand cmd = new SqlCommand();
        string varTestId;
        string varDateTime;
        //DateTime varDt;
        string varNodeId;
        int strengthTotal = 0; // to hold the total values of the questions strength
        int resultTotal = 0; // to hold the total of the strength of the right question
        int studentResult = 0; //to hold the result that the student get 
        SqlDataReader reader;
        int switchExp = 0;
       int updated =0;
        int rightOrWrong;
        string ansResult;
        string rightAns; // to hold the right answer of the question

        // Try to open database and read information.
        try
        {
            conStr.Open();
            // get the test id
            varTestId = Session["testID"].ToString();

            //get the test date and time
            varDateTime = Session["TestDateTime"].ToString();
              
             //varDt = DateTime.ParseExact(varDateTime, "dd/MM/yyyy hh:mm:ss tt",null);
             //string varDateTime2 = Convert.ToDateTime(varDateTime).ToString("yyyy/MM/dd hh:mm:ss tt");
            DateTimeLabel.Text = varDateTime;

            //get the node id
            sqlQuery = "SELECT Node_Id from Test where Test_Id=" + varTestId;
            cmd.CommandText = sqlQuery;
            cmd.Connection = conStr;
            varNodeId = cmd.ExecuteScalar().ToString();
            // get the module name
            sqlQuery.Remove(0);
            sqlQuery = "SELECT Name from Nodes where Node_Id=" + varNodeId;
            cmd.CommandText = sqlQuery;
            string node_name = cmd.ExecuteScalar().ToString();
            NodeIdLabel.Text = node_name;

            //reading the questions answers and thier strength
            sqlQuery.Remove(0);
            sqlQuery = " select sa.Question_Id, IsRight, Strength_Level from Student_answer sa, Strength sg, Student_test st where st.Student_Id=" + User.Identity.Name + " and sa.test_Id=" + varTestId + " and sa.Test_DateTime= '" + varDateTime + "' and sa.Question_Id= sg.Question_Id and sa.Test_Id= st.Test_Id and sa.Test_DateTime= st.Test_DateTime";
            cmd.CommandText = sqlQuery;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                strengthTotal+=(int)reader["Strength_Level"];
                switchExp = (int)reader["Strength_Level"];
                rightOrWrong = (int)reader["IsRight"];
                if(rightOrWrong== 1)
                {
                    switch (switchExp)
                    {
                        case 1: resultTotal += 1;
                            break;
                        case 2: resultTotal += 2;
                            break;
                        case 3: resultTotal += 3;
                            break;
                        case 4: resultTotal += 4;
                            break;
                        case 5: resultTotal += 5;
                            break;
                        default:
                            break;
                    }
                } //end of if
                else
                    resultTotal += 0;
            } //end of while
            reader.Close();
            double tempResult = (double)resultTotal/strengthTotal;
            studentResult = Convert.ToInt32(tempResult * 100);
            ResultLabel.Text = studentResult.ToString() + "%";

            // insert the score into the DB Student_test table
            sqlQuery.Remove(0);
            sqlQuery = "UPDATE Student_test SET Score='" + studentResult + "' where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime + "'";
            cmd.CommandText = sqlQuery;
            updated = cmd.ExecuteNonQuery(); 

            //insert whether student pass a test (pass if result >=50)
            sqlQuery.Remove(0);
            if (studentResult >= 50)
            {
                sqlQuery = "UPDATE Student_test SET IsPassed= 1 where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime + "'";
                ResultMsg.ForeColor = Color.Green;
                ResultMsg.Text = "Well done, you passed the test!!";
            }
            else
            {
                sqlQuery = "UPDATE Student_test SET IsPassed= 0 where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime + "'";
                ResultMsg.ForeColor = Color.Red;
                ResultMsg.Text = "Sorry, you didn't pass the test!!";
            }
            cmd.CommandText = sqlQuery;
            updated = cmd.ExecuteNonQuery(); 

            //show the student a report of his Q&A through grid view
            
            sqlQuery = "select q.text, ch.text, sa.IsRight, l.Strength_Level, q.Question_Id FROM Questions q, Choices ch, Student_answer sa, Student_test st, Strength l WHERE st.Student_Id=" + User.Identity.Name + " and sa.test_Id=" + varTestId + " and sa.Test_DateTime= '" + varDateTime + "' and sa.Question_Id= q.Question_Id and sa.Student_choice= ch.Choice_Id and sa.Test_Id= st.Test_Id and sa.Test_DateTime= st.Test_DateTime  and l.Question_Id= sa.Question_Id and l.Node_Id="+ varNodeId;
            cmd.CommandText = sqlQuery;
            reader = cmd.ExecuteReader();
            List<ResultQA> newItem = new List<ResultQA>();
            while (reader.Read())
            {
                string Question = reader.GetString(0);
                string Answer = reader.GetString(1);
                int isRight = reader.GetInt32(2);
                int weight = reader.GetInt32(3);
                int QuestionId = reader.GetInt32(4); //this to get the q id and use it to get the right answer

                if (isRight == 1)
                {
                    ansResult = "Right";
                    rightAns = Answer;
                }
                else
                {
                    ansResult = "Wrong";
                    rightAns = GetRightAnswer(QuestionId);

                }
                double temp = (double) weight / strengthTotal; 
                int varWeight = Convert.ToInt32(temp* 100);
                string strWeight = varWeight.ToString() + "%";
                newItem.Add(new ResultQA(Question,Answer,isRight,ansResult,strWeight,rightAns));
            }//end of while
            GridView1.DataSource = newItem;
            GridView1.DataBind();
                     

            //to hide the isRight col. in the grid view
            GridView1.HeaderRow.Cells[2].Visible = false;
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                gvr.Cells[2].Visible = false;
            }
            // to change the color of the cell
            foreach (GridViewRow gvr in GridView1.Rows)
            {
                //if (gvr.Cells[2].Text.Equals("1"))
                Label name = (Label)gvr.Cells[3].FindControl("AnswerResult"); // this is to get the value of the template field (right/wrong), AnswerResult is the ID of the label in template Item
                if(name.Text.Equals("Right"))
                {
                    gvr.Cells[3].ForeColor = Color.Green;
                    gvr.Cells[3].BorderColor = Color.Black;
                }
                else
                {
                    gvr.Cells[3].ForeColor = Color.Red;
                    gvr.Cells[3].BorderColor = Color.Black;
                    LinkButton showAnswer = (LinkButton)gvr.Cells[5].FindControl("ShowAns"); // this is to change the visibility of the linkButton, ShowAns is the ID of the linkButton
                    showAnswer.Visible = true;
                }

            }
        }// end of try
        catch (Exception err)
        {
            LblErrMsg.Text += err.Message;
        }
        finally
        {
            conStr.Close();
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StdCourseIntro.aspx");
    }

    public string GetRightAnswer(int QuestionId)
    {
        string rightAns="";
        string query = "select text from choices where Question= "+QuestionId+" and status ='Y'";
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(query, conStr);
        try
        {
            conStr.Open();
            rightAns = cmd.ExecuteScalar().ToString();

        }
        catch (Exception err)
        {
            LblErrMsg.Text += err.Message;
        }
        
        finally
        {
            conStr.Close();
        }
        return rightAns;
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        GridView1.Columns[6].HeaderText = "Right Answer";
            
        //Get the linkbutton that raised the event inside Gridview
        LinkButton btn = (LinkButton)sender;

        //Get the row that contains this button
        GridViewRow gridviewRow = (GridViewRow)btn.NamingContainer;

       // get the label control in the template field to change its property
        Label rightAns = (Label)gridviewRow.Cells[6].FindControl("RightAnswer");
        rightAns.Visible = true;
     
    }
}

 