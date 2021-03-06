﻿///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

            try
            {
                //process image
                Image img = (Image)e.Item.FindControl("imgCanvas");
                int qsId = Convert.ToInt32(questionId.Text);
                string imgByte = getImageByte(qsId);
                if (!imgByte.Equals(""))
                {
                    img.ImageUrl = imgByte;
                }
                else
                {
                    img.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }

    private string getImageByte(int questionId)
    {
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        byte[] picture = null;
        try
        {
            conStr.Open();
            SqlCommand cmd = new SqlCommand("SELECT imgData FROM Questions where Question_Id = @id", conStr);
            SqlParameter p1 = new SqlParameter("@id", questionId);
            cmd.Parameters.Add(p1);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                picture = (byte[])reader[0];
                if (picture.Length <= 0)
                {
                    picture = null;
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
            conStr.Close();
        }

        string returnData ="";
        if (picture != null && picture.Length > 0)
        {
            returnData = "data:image/jpg;base64," + Convert.ToBase64String(picture);
        }

        return returnData;
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
        // This is to check whethre all questions are answered
        bool allanswer=true;
        
        foreach (DataListItem item in DataList1.Items)
        {
            if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
            {
                
                RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                if(!(rbl.SelectedItem !=null))
                {
                    lblThanks.ForeColor = System.Drawing.Color.Red;
                    lblThanks.Visible = true;
                    lblThanks.Text = " You need to answer all questions. ";
                    allanswer = false;
                    return;
                }
            }
        }
        if (allanswer == true)
        {
            //Ilung starts here
            int testId = Convert.ToInt32(Request.QueryString["id"]);
            //saveTest
            SaveTest(testId, (int)Session["StudentId"]);

            //save student answers
            foreach (DataListItem item in DataList1.Items)
            {
                if (item.ItemType == ListItemType.Item | item.ItemType == ListItemType.AlternatingItem)
                {
                    int questionid = 0;
                    int choiceid = 0;
                    Label lbl = (Label)item.FindControl("lblQuestionID");
                    questionid = Convert.ToInt32(lbl.Text);

                    RadioButtonList rbl = (RadioButtonList)item.FindControl("RadioButtonList1");
                    choiceid = Convert.ToInt32(rbl.SelectedValue);
                    SaveAnswer(questionid, choiceid, testId);
                }
            }

            DataList1.Visible = false;
            lblThanks.Text = "Thank you for answering the questions!";

            //to get to student result
            Response.Redirect("~/testResult.aspx");
        }//end of if
    }

    private void SaveTest(int testId, int studentId)
    {
         SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
         try
         {
             conStr.Open();
             var today = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
             //Get the answer for the question
             SqlCommand cmd = new SqlCommand("INSERT INTO Student_test VALUES(@studentId, @testId, @DateTime, '', '')", conStr);
             SqlParameter p1 = new SqlParameter("@studentId", studentId);
             SqlParameter p2 = new SqlParameter("@testId", testId);
             SqlParameter p3 = new SqlParameter("@DateTime", today);

             cmd.Parameters.Add(p1);
             cmd.Parameters.Add(p2);
             cmd.Parameters.Add(p3);

             int x = cmd.ExecuteNonQuery();
             Session["TestDateTime"] = today;
         }
         catch (Exception ex)
         {
             ex.ToString();
         }
         finally
         {
             conStr.Close();
         }
    }

    private void SaveAnswer(int qid, int cid, int testId)
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
                if (cid == reader.GetInt32(0))
                {
                    isRight = 1;
                }

            }
            reader.Close();

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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            int testId = Convert.ToInt32(Request.QueryString["id"]);
            if (Session["StudentID"] != null)
            {
                int studentId = (int)Session["StudentID"];
            }
            else
            {
                lblThanks.Text = "You are not login! Please login before continuing with the test";
                DataList1.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            Response.Redirect("~/Error.aspx");
        }
    }
}