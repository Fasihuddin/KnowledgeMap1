using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class testResult : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        testLbl.Text = User.Identity.Name;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        
        String sqlQuery;
        SqlCommand cmd = new SqlCommand();
        string varTestId;
        string varDateTime;
        DateTime varDt;
        string varNodeId;
        int strengthTotal = 0; // to hold the total values of the questions strength
        int resultTotal = 0; // to hold the total of the strength of the right question
        int studentResult = 0; //to hold the result that the student get 
        SqlDataReader reader;
        int switchExp = 0;
       int updated =0;
        int rightOrWrong;

        // Try to open database and read information.
        try
        {
            conStr.Open();
            // get the test id
            sqlQuery = "SELECT test_Id from Student_test WHERE Student_Id="+ User.Identity.Name;
            cmd.CommandText = sqlQuery;
            cmd.Connection = conStr;
            varTestId = cmd.ExecuteScalar().ToString();
            testIdLabel.Text = varTestId;

            //int testID = (int) Session["testID"];
           
            //get the test date and time
            sqlQuery.Remove(0); //remove the previous sql query
            sqlQuery = "SELECT test_DateTime from Student_test where test_Id =" + varTestId + " and student_Id=" + User.Identity.Name;
            cmd.CommandText = sqlQuery;
            varDateTime = cmd.ExecuteScalar().ToString();
          
             varDt = DateTime.ParseExact(varDateTime, "dd/MM/yyyy hh:mm:ss tt",null);
             string varDateTime2 = Convert.ToDateTime(varDateTime).ToString("yyyy/MM/dd hh:mm:ss tt");
            DateTimeLabel.Text = varDateTime;

            //get the node id
            sqlQuery.Remove(0);
            sqlQuery = "SELECT Node_Id from Test where Test_Id=" + varTestId;
            cmd.CommandText = sqlQuery;
            varNodeId = cmd.ExecuteScalar().ToString();
            NodeIdLabel.Text = varNodeId;

            //reading the questions answers and thier strength
            sqlQuery.Remove(0);
            sqlQuery ="select sa.Question_Id, IsRight, Strength_Level from Student_answer sa, Strength sg where sa.Student_Id=" + User.Identity.Name+" and sa.test_Id="+ varTestId +" and sa.TestDateTime= '"+varDateTime2+"' and sa.Question_Id= sg.Question_Id";
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
            studentResult = (resultTotal / strengthTotal) * 100;
            ResuleLabel.Text = studentResult.ToString() + "%";

            // insert the score into the DB Student_test table
            sqlQuery.Remove(0);
            sqlQuery = "UPDATE Student_test SET Score='" + studentResult + "' where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime2 + "'";
            cmd.CommandText = sqlQuery;
            updated = cmd.ExecuteNonQuery(); 

            //insert whether student pass a test (pass if result >=50)
            sqlQuery.Remove(0);
            if (studentResult >= 50)
                sqlQuery = "UPDATE Student_test SET IsPassed= 1 where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime2 + "'";
            else
                sqlQuery = "UPDATE Student_test SET IsPassed= 0 where Student_Id =" + User.Identity.Name + "and Test_Id=" + varTestId + "and Test_DateTime= '" + varDateTime2 + "'";
            cmd.CommandText = sqlQuery;
            updated = cmd.ExecuteNonQuery(); 
            
            
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