///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                          Knowledge Map Web Application for Research                                           //
//                                   Created by Ilung Pranata                                                    //
//                                  Date created: 27-April-2013                                                  //
//  This page accepts a test version ID from startTest.aspx form and retrieve the questions from database. Once  //
//  the questions are retrieved, it dynamically displays the questions for student to answer and save student    //
//  answers to the database.                                                                                     //

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class StdQuestionsForm : System.Web.UI.Page
{

    protected void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)  
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            //Get the questionID from the aspx label
            Label questionId = (Label) e.Item.FindControl("lblQuestionID");
            RadioButtonList rbl = (RadioButtonList) e.Item.FindControl("RadioButtonList1");

            //get all choices for the question
            DataSet ds = getQuestionChoices(questionId.Text);

            //bind choices to radio button lists
            rbl.Visible = true;
            rbl.DataSource = ds;
            rbl.DataTextField = "Text";
            rbl.DataValueField = "Choice_Id";
            rbl.DataBind();
        }
    }

    /*
     * Method used for getting all choices of the question. It asks for questionId to be supplied
     * */
    private DataSet getQuestionChoices(string questionId)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        DataSet ds = new DataSet();
        try
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Choices where Question = @id", conStr);
            SqlParameter p1 = new SqlParameter("@id", questionId);
            cmd.Parameters.Add(p1);
            SqlDataAdapter dtAdapt = new SqlDataAdapter();
            dtAdapt.SelectCommand = cmd;
            dtAdapt.Fill(ds, "choices");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return ds;
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        int testId = Convert.ToInt32(Request.QueryString["id"]);
        int noRight = 0;
        foreach (DataListItem item in DataList1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                int questionid = 0;
                int choiceid = 0;
                Label lbl = (Label) item.FindControl("lblQuestionID");
                questionid = Convert.ToInt32(lbl.Text);

                RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                choiceid = Convert.ToInt32(rbl.SelectedValue);

                noRight += SaveAnswer(questionid, choiceid, testId);
            }
        }

        //calculate score based on the number of questions student got it right
        double score = (double)noRight/(double)DataList1.Items.Count * 100;
        //saveTest
        SaveTest(testId, (int) Session["StudentId"], (int) score);


        DataList1.Visible = false;
        lblThanks.Text = "Thank you for answering the questions!";
    }

    private void SaveTest(int testId, int studentId, int score)
    {
        int isPassed = 0;
        if (score >= 50)
        {
            isPassed = 1;
        }

         SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
         try
         {
             conStr.Open();
             var today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
             //Get the answer for the question
             SqlCommand cmd = new SqlCommand("INSERT INTO Student_test VALUES(@studentId, @testId, @DateTime, @score, @isPassed)", conStr);
             SqlParameter p1 = new SqlParameter("@studentId", studentId);
             SqlParameter p2 = new SqlParameter("@testId", testId);
             SqlParameter p3 = new SqlParameter("@DateTime", today);
             SqlParameter p4 = new SqlParameter("@score", score);
             SqlParameter p5 = new SqlParameter("@isPassed", isPassed);

             cmd.Parameters.Add(p1);
             cmd.Parameters.Add(p2);
             cmd.Parameters.Add(p3);
             cmd.Parameters.Add(p4);
             cmd.Parameters.Add(p5);

             int x = cmd.ExecuteNonQuery();

         }
         catch (Exception ex)
         {
             ex.ToString();
         }
    }

    private int SaveAnswer(int qid, int cid, int testId)
    {
        int isRight = 0;
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        try
        {
            conStr.Open();
            //Get the answer for the question
            SqlCommand cmd = new SqlCommand("Select Choice_Id From Choices where Question = @qId and Status LIKE 'Y'", conStr);
            SqlParameter p1 = new SqlParameter("@qid", qid);
            cmd.Parameters.Add(p1);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //if student choice is similar with the answer of the question, student's answer is right
                if(cid == reader.GetInt32(0)){
                    isRight = 1;
                }
                
            }
            conStr.Close();

            conStr.Open();
            var today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //Now inserting the values to database
            cmd = new SqlCommand("insert into Student_answer(Test_Id,Test_DateTime, Question_Id,Student_choice,IsRight) values(@testId,@DateTime,@qid,@cid,@isRight)", conStr);
            SqlParameter p2 = new SqlParameter("@testid", testId);
            SqlParameter p3 = new SqlParameter("@qid", qid);
            SqlParameter p4 = new SqlParameter("@cid", cid);
            SqlParameter p5 = new SqlParameter("@isRight", isRight);
            SqlParameter p6 = new SqlParameter("@DateTime", today);

            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);

            cmd.ExecuteNonQuery();
            conStr.Close();
        }catch(Exception ex){
            Console.WriteLine(ex.ToString());
        }

        return isRight;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int testId = Convert.ToInt32(Request.QueryString["id"]);
            int studentId = (int) Session["StudentId"];
        }
        catch (Exception ex)
        {
            Response.Redirect("~/Error.aspx");
        }
    }
}