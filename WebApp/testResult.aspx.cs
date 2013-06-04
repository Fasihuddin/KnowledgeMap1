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
            NodeIdLabel.Text = varNodeId;

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
            
            sqlQuery = "select q.text, ch.text, sa.IsRight, l.Strength_Level FROM Questions q, Choices ch, Student_answer sa, Student_test st, Strength l WHERE st.Student_Id=" + User.Identity.Name + " and sa.test_Id=" + varTestId + " and sa.Test_DateTime= '" + varDateTime + "' and sa.Question_Id= q.Question_Id and sa.Student_choice= ch.Choice_Id and sa.Test_Id= st.Test_Id and sa.Test_DateTime= st.Test_DateTime  and l.Question_Id= sa.Question_Id and l.Node_Id="+ varNodeId;
            cmd.CommandText = sqlQuery;
            reader = cmd.ExecuteReader();
            List<ResultQA> newItem = new List<ResultQA>();
            while (reader.Read())
            {
                string Question = reader.GetString(0);
                string Answer = reader.GetString(1);
                int isRight = reader.GetInt32(2);
                int weight = reader.GetInt32(3);

                if (isRight==1)
                    ansResult= "Right";
                else
                    ansResult ="Wrong";
                double temp = (double) weight / strengthTotal; 
                int varWeight = Convert.ToInt32(temp* 100);
                string strWeight = varWeight.ToString() + "%";
                newItem.Add(new ResultQA(Question,Answer,isRight,ansResult,strWeight));
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
                if (gvr.Cells[2].Text.Equals("1"))
                {
                    gvr.Cells[3].ForeColor = Color.Green;
                    gvr.Cells[3].BorderColor = Color.Black;
                }
                else
                {
                    gvr.Cells[3].ForeColor = Color.Red;
                    gvr.Cells[3].BorderColor = Color.Black;
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
}