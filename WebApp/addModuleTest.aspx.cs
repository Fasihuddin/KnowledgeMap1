///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//                          Knowledge Map Web Application for Research                                           //
//                                   Created by Ilung Pranata                                                    //
//                                  Date created: 25-June-2013                                                   //
// This form is used to create/edit test questions. It also creates test versions for the node                   //

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

public partial class addModuleTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //fill courses
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Course_Id, Name, Code FROM Course", conStr);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ListItem lstItems = new ListItem();
                    lstItems.Value = Convert.ToString(reader.GetInt32(0));
                    lstItems.Text = reader.GetString(2) + " - " + reader.GetString(1);
                    this.ddlCourse.Items.Add(lstItems);
                }
                reader.Close();
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

        private void FillEmployeeGrid()
        {
            List<Question> testQs = (List<Question>)Session["testQuestion"];
            gvEG.DataSource = testQs;
            gvEG.DataBind();
        }
        
        protected void gvEG_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("emptyInsert") || e.CommandName.Equals("Insert"))
            {
                try
                {
                    GridViewRow emptyRow;
                    if (e.CommandName.Equals("Insert"))
                    {
                        emptyRow = gvEG.FooterRow;
                    }
                    else
                    {
                        emptyRow = gvEG.Controls[0].Controls[0] as GridViewRow;
                    }

                    //storing picture
                    byte[] imageData;
                    String contentType;
                    String imgName;
                    FileUpload fuPic = (FileUpload)emptyRow.FindControl("fuPicture");
                    if (fuPic.HasFile)
                    {
                        imageData = fuPic.FileBytes;
                        contentType = fuPic.PostedFile.ContentType;
                        imgName = fuPic.PostedFile.FileName;
                    }
                    else
                    {
                        contentType = "None";
                        imageData = new byte[0];
                        imgName = "No Picture";
                    }

                    //storing answers
                    String text = Convert.ToString(((TextBox)emptyRow.FindControl("txtQuestionText")).Text);
                    String choice1 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer1")).Text));
                    String choice2 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer2")).Text));
                    String choice3 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer3")).Text));
                    String choice4 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer4")).Text));

                    int strength = Convert.ToInt32(((DropDownList) emptyRow.FindControl("ddlStrength")).SelectedValue);

                    RadioButton rd1 = (RadioButton)emptyRow.FindControl("rdbAnswer1");
                    RadioButton rd2 = (RadioButton)emptyRow.FindControl("rdbAnswer2");
                    RadioButton rd3 = (RadioButton)emptyRow.FindControl("rdbAnswer3");
                    RadioButton rd4 = (RadioButton)emptyRow.FindControl("rdbAnswer4");

                    int answer = 4;
                    if (rd1.Checked)
                    {
                        answer = 1;
                    }
                    else if (rd2.Checked)
                    {
                        answer = 2;
                    }
                    else if (rd3.Checked)
                    {
                        answer = 3;
                    }
                    //create question object
                    Question qs = new Question(text, choice1, choice2, choice3, choice4, strength, answer, imageData, contentType, imgName);

                    //call session to get the existing test questions
                    List<Question> testQs = (List<Question>)Session["testQuestion"];
                    //check if data exists in database - used in updating existing records
                    if (testQs.Count > 0 && testQs[0].qId > 0)
                    {
                        List<Question> newQuestions = (List<Question>)Session["newQuestions"];
                        newQuestions.Add(qs);
                    }
                    //add to session
                    testQs.Add(qs);
                    Session["testQuestion"] = testQs;

                    //fill the gridview
                    FillEmployeeGrid();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
        }
        protected void gvEG_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEG.EditIndex = e.NewEditIndex;
            FillEmployeeGrid();

            //get existing questions
                List<Question> testQs = (List<Question>)Session["testQuestion"];
                
                GridViewRow emptyRow = gvEG.Rows[e.NewEditIndex];
                RadioButton rd1 = (RadioButton)emptyRow.FindControl("rdbAnswer1");
                RadioButton rd2 = (RadioButton)emptyRow.FindControl("rdbAnswer2");
                RadioButton rd3 = (RadioButton)emptyRow.FindControl("rdbAnswer3");
                RadioButton rd4 = (RadioButton)emptyRow.FindControl("rdbAnswer4");

                //set answer radiobutton
                int ans = testQs[e.NewEditIndex].answer;
                if (ans == 1)
                {
                    rd1.Checked = true;
                }
                else if (ans == 2)
                {
                    rd2.Checked = true;
                }
                else if (ans == 3)
                {
                    rd3.Checked = true;
                }
                else
                {
                    rd4.Checked = true;
                }

        }
        
        protected void gvEG_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            //add to session
            List<Question> testQs = (List<Question>)Session["testQuestion"];

            //check if data exists in database
            if (testQs[0].qId > 0)
            {
                List<int> deleteID = (List<int>) Session["DeleteQsID"];
                deleteID.Add(testQs[e.RowIndex].qId);
            }
            testQs.RemoveAt(e.RowIndex);
            Session["testQuestion"] = testQs;
            FillEmployeeGrid();
        }

        protected void gvEG_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                List<Question> testQs = (List<Question>)Session["testQuestion"];

                //storing questions and answers
                GridViewRow emptyRow = gvEG.Rows[e.RowIndex];
                String text = Convert.ToString(((TextBox)emptyRow.FindControl("txtQuestionText")).Text);
                String choice1 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer1")).Text));
                String choice2 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer2")).Text));
                String choice3 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer3")).Text));
                String choice4 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer4")).Text));

                //storing picture
                byte[] imageData;
                String contentType;
                String imgName;
                FileUpload fuPic = (FileUpload)emptyRow.FindControl("fuPicture");
                if (fuPic.HasFile)
                {
                    imageData = fuPic.FileBytes;
                    contentType = fuPic.PostedFile.ContentType;
                    imgName = fuPic.PostedFile.FileName;
                }
                else
                {
                    List<Question> testQs2 = (List<Question>)Session["testQuestion"];
                    contentType = testQs2[e.RowIndex].contentType;
                    imageData = testQs2[e.RowIndex].imgData;
                    imgName = testQs2[e.RowIndex].imgName;
                }

                //storing strength
                int strength = Convert.ToInt32(((DropDownList)emptyRow.FindControl("ddlStrength")).SelectedValue);

                //storing answer
                RadioButton rd1 = (RadioButton)emptyRow.FindControl("rdbAnswer1");
                RadioButton rd2 = (RadioButton)emptyRow.FindControl("rdbAnswer2");
                RadioButton rd3 = (RadioButton)emptyRow.FindControl("rdbAnswer3");
                RadioButton rd4 = (RadioButton)emptyRow.FindControl("rdbAnswer4");

                int answer = 4;
                if (rd1.Checked)
                {
                    answer = 1;
                }
                else if (rd2.Checked)
                {
                    answer = 2;
                }
                else if (rd3.Checked)
                {
                    answer = 3;
                }

                //update question object
                testQs[e.RowIndex].text = text;
                testQs[e.RowIndex].choice1 = choice1;
                testQs[e.RowIndex].choice2 = choice2;
                testQs[e.RowIndex].choice3 = choice3;
                testQs[e.RowIndex].choice4 = choice4;
                testQs[e.RowIndex].strength = strength;
                testQs[e.RowIndex].answer = answer;
                testQs[e.RowIndex].imgData = imageData;
                testQs[e.RowIndex].contentType = contentType;
                testQs[e.RowIndex].imgName = imgName;

                //create question object
                //Question qs = new Question(text, choice1, choice2, choice3, choice4, strength, answer,imageData,contentType, imgName);

                //add to session
                Session["testQuestion"] = testQs;

                gvEG.EditIndex = -1;
                FillEmployeeGrid();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        protected void btnTest_Click(object sender, EventArgs e)
        {
            if (ddlModule.SelectedIndex >=0 && ddlTopic.SelectedItem.Value != null && ddlModule.SelectedIndex >=0)
            {
                //get existing questions from database
                List<Question> qsItem = getQuestionsOfModule();
                if (qsItem.Count > 0)
                {
                    foreach (Question qs in qsItem)
                    {
                         SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                         try
                         {
                             conStr.Open();
                             //Get the choices and answer for the questions

                             //Get the answer for the question
                             SqlCommand cmd = new SqlCommand("SELECT Questions.img_contenttype, Questions.imgData, Questions.imgName, Strength.Strength_Level " +
                                                "FROM Questions INNER JOIN Strength ON Questions.Question_Id = Strength.Question_Id "+
                                                "WHERE (Strength.Node_Id = @nodeID) AND (Strength.Question_Id = @qsID)", conStr);
                             SqlParameter p1 = new SqlParameter("@qsID", qs.qId);
                             SqlParameter p2 = new SqlParameter("@nodeID", Convert.ToInt32(ddlModule.SelectedItem.Value));
                             cmd.Parameters.Add(p1); 
                             cmd.Parameters.Add(p2);
                             SqlDataReader reader = cmd.ExecuteReader();

                             while (reader.Read())
                             {
                                 qs.contentType = reader.GetString(0);
                                 if (reader["imgData"] == null)
                                 {
                                     qs.imgData = (Byte[])reader["imgData"];
                                 }
                                 else
                                 {
                                     qs.imgData = new byte[0];
                                 }
                                 qs.imgName = reader.GetString(2);
                                 qs.strength = reader.GetInt32(3);
                             }

                             reader.Close();

                             //get choices and answer
                             //Get the answer for the question
                             cmd = new SqlCommand("SELECT Text, Status, Choice_Id " +
                                                "FROM Choices WHERE (Question = @qsID)", conStr);
                             p1 = new SqlParameter("@qsID", qs.qId);
                             cmd.Parameters.Add(p1);
                             reader = cmd.ExecuteReader();

                             String[] Choices = new String[4];
                             int[] choiceIDs = new int[4];
                             int count = 0;
                             while (reader.Read() && count < 4)
                             {
                                 Choices[count] = reader.GetString(0);
                                 String ans = reader.GetString(1);
                                 if(ans.Equals("Y"))
                                 {
                                     qs.answer = count+1;
                                 }
                                 choiceIDs[count] = reader.GetInt32(2);
                                 count++;
                             }
                             reader.Close();
                             //add choices
                             qs.choice1 = Choices[0];
                             qs.choice2 = Choices[1];
                             qs.choice3 = Choices[2];
                             qs.choice4 = Choices[3];
                             //add choiceIDs
                             qs.choiceID1 = choiceIDs[0];
                             qs.choiceID2 = choiceIDs[1];
                             qs.choiceID3 = choiceIDs[2];
                             qs.choiceID4 = choiceIDs[3];
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

                         //store all values
                         Session["testQuestion"] = qsItem;
                         Session["DeleteQsID"] = new List<int>();
                         Session["newQuestions"] = new List<Question>();
                         btnSaveTest.Enabled = false;
                         ddlVersions.Enabled = false;
                         btnUpdateQs.Enabled = true;
                        //fill data to gridview
                         FillEmployeeGrid();
                    }//end foreach
                }//end if
                else
                {
                    //for gridview
                    this.gvEG.DataSource = new List<object>();
                    gvEG.DataBind();

                    //set the viewstate to false
                    ViewState["isExisting"] = false; 
                    //store all values
                    Session["testQuestion"] = new List<Question>();

                    btnSaveTest.Enabled = true;
                    ddlVersions.Enabled = true;
                    btnUpdateQs.Enabled = false;
                }
                pnlTest.Visible = true;
            }
        }

        private List<Question> getQuestionsOfModule()
        {
            List<Question> qsItems = new List<Question>();
             //fill topics
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT DISTINCT Questions.Question_Id, Questions.Text " +
                         "FROM Nodes INNER JOIN Test ON Nodes.Node_Id = Test.Node_Id INNER JOIN " +
                         "Test_questions ON Test.Test_Id = Test_questions.Test_Id INNER JOIN "+
                         "Questions ON Test_questions.Question_Id = Questions.Question_Id "+
                         "WHERE (Nodes.Node_Id = @nodeID)", conStr);
                SqlParameter p1 = new SqlParameter("@nodeID", Convert.ToInt32(ddlModule.SelectedItem.Value));
                cmd.Parameters.Add(p1);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Question qs = new Question(reader.GetInt32(0), reader.GetString(1));
                    qsItems.Add(qs);
                }
                reader.Close();
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

            return qsItems;
        }


        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear all items
            ddlTopic.Items.Clear();
            pnlTest.Visible = false;

            //fill topics
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                //Get the answer for the question
                SqlCommand cmd = new SqlCommand("SELECT Topic.Topic_Id, Topic.Name FROM Course INNER JOIN "+
                         "TopicOnCourse ON Course.Course_Id = TopicOnCourse.CourseID INNER JOIN "+
                         "Topic ON TopicOnCourse.TopicID = Topic.Topic_Id WHERE Course_Id = @courseID", conStr);
                SqlParameter p1 = new SqlParameter("@courseID", ddlCourse.SelectedItem.Value);
                cmd.Parameters.Add(p1);
                SqlDataReader reader = cmd.ExecuteReader();

                bool hasData = false;
                while (reader.Read())
                {
                    ListItem lstItems = new ListItem();
                    lstItems.Value = Convert.ToString(reader.GetInt32(0));
                    lstItems.Text = reader.GetString(1);
                    this.ddlTopic.Items.Add(lstItems);
                    hasData = true;
                }

                //disable module lists if no topic in the course
                if (hasData == false)
                {
                    ddlModule.Enabled = false;
                }
                else
                {
                    ddlModule.Enabled = true;
                }

                reader.Close();
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

            fillModules();
        }
        protected void ddlTopic_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillModules();
        }

        private void fillModules()
        {
            //clear all items
            this.ddlModule.Items.Clear();

            if (ddlTopic.SelectedIndex >= 0)
            {
                //fill topics
                SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                try
                {
                    conStr.Open();
                    //Get the answer for the question
                    SqlCommand cmd = new SqlCommand("SELECT Nodes.Node_Id, Nodes.Name FROM NodeOnTopic INNER JOIN "
                             + "Nodes ON NodeOnTopic.Node_Id = Nodes.Node_Id WHERE (NodeOnTopic.Topic_Id = @topicID)", conStr);
                    SqlParameter p1 = new SqlParameter("@topicID", this.ddlTopic.SelectedItem.Value);
                    cmd.Parameters.Add(p1);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        ListItem lstItems = new ListItem();
                        lstItems.Value = Convert.ToString(reader.GetInt32(0));
                        lstItems.Text = reader.GetString(1);
                        this.ddlModule.Items.Add(lstItems);
                    }
                    reader.Close();
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
            }//end if
        }// end function

        protected void btnSaveTest_Click(object sender, EventArgs e)
        {

            List<Question> testQs = (List<Question>)Session["testQuestion"];


            if (testQs != null)
            {
                //save test questions
                saveQuestions(testQs);

                //create test versions
                generateTest(testQs);

                btnSaveTest.Enabled = false;
                //Show success Alerts
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
                System.Web.HttpContext.Current.Response.Write("alert('Success! Test questions added.')");
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
            }
            else
            {
                //Show success Alerts
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
                System.Web.HttpContext.Current.Response.Write("alert('No Test questions added. Please add the test questions.')");
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>");

            }
           
        }

        private void generateTest(List<Question> testQs)
        {
            int totalVer = Convert.ToInt32(ddlVersions.SelectedItem.Value);
            //save the first version of test
            List<int> qsID = (List<int>)Session["allQsID"];
            saveTest(qsID);

            //create other versions of test
            for (int i = 1; i < totalVer; i++)
            {
                //create temp list to hold the new random order questions
                List<int> tempQs = new List<int>();
                while (qsID.Count > 0)
                {
                    Random rand = new Random();
                    int no = rand.Next(qsID.Count);
                    tempQs.Add(qsID[no]);
                    qsID.RemoveAt(no);
                }
                saveTest(tempQs);
                qsID = (List<int>)Session["allQsID"];
            }

        }

   
    /*
     * This method is used to create test version and assign test to both module and questions
     * */
        private void saveTest(List<int> qsItem)
        {
             SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
             try
             {
                 conStr.Open();
                 int maxTestID = 0;
                 //get the largest test ID from DB
                 SqlCommand cmd = new SqlCommand("SELECT MAX(Test_Id) FROM Test", conStr);
                 SqlDataReader reader = cmd.ExecuteReader();

                 while (reader.Read())
                 {
                     maxTestID = reader.GetInt32(0);
                 }
                 reader.Close();

                 //save test
                 int moduleID = Convert.ToInt32(ddlModule.SelectedItem.Value);
                 cmd = new SqlCommand("INSERT INTO Test VALUES(@testID, @nodeID)", conStr);
                 SqlParameter p1 = new SqlParameter("@testID", maxTestID + 1);
                 SqlParameter p2 = new SqlParameter("@nodeID", moduleID);
                 cmd.Parameters.Add(p1);
                 cmd.Parameters.Add(p2);

                 int x = cmd.ExecuteNonQuery();

                 //assign test to questions
                 for (int i = 0; i < qsItem.Count; i++)
                 {
                     cmd = new SqlCommand("INSERT INTO Test_questions VALUES(@testID, @qsId, @order)", conStr);
                     p1 = new SqlParameter("@testID", maxTestID + 1);
                     p2 = new SqlParameter("@qsID", qsItem[i]);
                     SqlParameter p3 = new SqlParameter("@order", i+1);
                     cmd.Parameters.Add(p1);
                     cmd.Parameters.Add(p2);
                     cmd.Parameters.Add(p3);

                     x = cmd.ExecuteNonQuery();
                 }

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

        /*
         * This method is used to save the test questions
         * */
        private void saveQuestions(List<Question> testQs)
        {
                SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                try
                {
                    conStr.Open();
                    int maxQsID = 0;
                    //get the largest question ID from DB
                    SqlCommand cmd = new SqlCommand("SELECT MAX(Question_Id) FROM Questions", conStr);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        maxQsID = reader.GetInt32(0);
                    }
                    reader.Close();

                    //save questions to database
                    List<int> qsID = new List<int>(); //used for using questions ID to generate test versions
                    foreach (Question q in testQs)
                    {
                        //Insert questions to DB
                        cmd = new SqlCommand("INSERT INTO Questions(Question_Id, Text, img_contenttype, imgData, imgName) " +
                            "VALUES(@qID, @qText, @imgType, @imgData, @imgName)", conStr);
                        SqlParameter p1 = new SqlParameter("@qID", maxQsID + 1);
                        SqlParameter p2 = new SqlParameter("@qText", q.text);
                        SqlParameter p3 = new SqlParameter("@imgType", q.contentType);
                        SqlParameter p4 = new SqlParameter("@imgData", SqlDbType.Image);
                        p4.Value = q.imgData;
                        SqlParameter p5 = new SqlParameter("@imgName", q.imgName);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        cmd.Parameters.Add(p5);
                        int x = cmd.ExecuteNonQuery();

                        //save strength
                        cmd = new SqlCommand("INSERT INTO Strength VALUES(@nodeID, @qID, @strength)", conStr);
                        p1 = new SqlParameter("@nodeID", Convert.ToInt32(ddlModule.SelectedItem.Value));
                        p2 = new SqlParameter("@qID", maxQsID + 1);
                        p3 = new SqlParameter("@strength", q.strength);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        x = cmd.ExecuteNonQuery();

                        //save choice 1
                        //get max choiceID
                        int maxChoiceID = 0;
                        //get the largest test ID from DB
                        cmd = new SqlCommand("SELECT MAX(Choice_Id) FROM Choices", conStr);
                        reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            maxChoiceID = reader.GetInt32(0);
                        }
                        reader.Close();

                        Char[] answers = new Char[5];
                        answers[0] = Convert.ToChar("X");
                        for (int i =1; i<answers.Length; i++)
                        {
                            if (i == q.answer)
                            {
                                answers[i] = Convert.ToChar("Y");
                            }
                            else
                            {
                                answers[i] = Convert.ToChar("N");
                            }
                        }

                        //choice 1
                        cmd = new SqlCommand("INSERT INTO Choices VALUES(@ChoiceID, @text, @status, @qID)", conStr);
                        p1 = new SqlParameter("@ChoiceID", maxChoiceID+1);
                        p2 = new SqlParameter("@text", q.choice1);
                        p3 = new SqlParameter("@status", answers[1]);
                        p4 = new SqlParameter("@qID", maxQsID + 1);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        x = cmd.ExecuteNonQuery();

                        //choice 2
                        cmd = new SqlCommand("INSERT INTO Choices VALUES(@ChoiceID, @text, @status, @qID)", conStr);
                        p1 = new SqlParameter("@ChoiceID", maxChoiceID + 2);
                        p2 = new SqlParameter("@text", q.choice2);
                        p3 = new SqlParameter("@status", answers[2]);
                        p4 = new SqlParameter("@qID", maxQsID + 1);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        x = cmd.ExecuteNonQuery();

                        //choice 3
                        cmd = new SqlCommand("INSERT INTO Choices VALUES(@ChoiceID, @text, @status, @qID)", conStr);
                        p1 = new SqlParameter("@ChoiceID", maxChoiceID + 3);
                        p2 = new SqlParameter("@text", q.choice3);
                        p3 = new SqlParameter("@status", answers[3]);
                        p4 = new SqlParameter("@qID", maxQsID + 1);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        x = cmd.ExecuteNonQuery();

                        //choice 4
                        cmd = new SqlCommand("INSERT INTO Choices VALUES(@ChoiceID, @text, @status, @qID)", conStr);
                        p1 = new SqlParameter("@ChoiceID", maxChoiceID + 4);
                        p2 = new SqlParameter("@text", q.choice4);
                        p3 = new SqlParameter("@status", answers[4]);
                        p4 = new SqlParameter("@qID", maxQsID + 1);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        cmd.Parameters.Add(p4);
                        x = cmd.ExecuteNonQuery();

                        //increase the qsID
                        qsID.Add(maxQsID + 1);
                        maxQsID++; 
                    }
                    //add qsID to a session
                    Session["allQsID"] = qsID;
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

        protected void btnUpdateQs_Click(object sender, EventArgs e)
        {
            
            //The following code would update the changes to the DB
            List<Question> testQs = (List<Question>)Session["testQuestion"];
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
            try
            {
                conStr.Open();
                foreach (Question q in testQs)
                {
                    //update questions in questions table
                    SqlCommand cmd = new SqlCommand("UPDATE Questions SET Text = @qText, img_contenttype = @imgType, imgData = @imgData, "+
                                    "imgName = @imgName WHERE (Question_Id = @qID)", conStr);
                    SqlParameter p1 = new SqlParameter("@qID", q.qId);
                    SqlParameter p2 = new SqlParameter("@qText", q.text);
                    SqlParameter p3 = new SqlParameter("@imgType", q.contentType);
                    SqlParameter p4 = new SqlParameter("@imgData", SqlDbType.Image);
                    p4.Value = q.imgData;
                    SqlParameter p5 = new SqlParameter("@imgName", q.imgName);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    cmd.Parameters.Add(p4);
                    cmd.Parameters.Add(p5);
                    int x = cmd.ExecuteNonQuery();

                    //update strength in strength table
                    cmd = new SqlCommand("UPDATE Strength SET Strength_Level = @strength WHERE "+
                        "Node_Id = @nodeID AND Question_Id = @qID", conStr);
                    p1 = new SqlParameter("@nodeID", Convert.ToInt32(ddlModule.SelectedItem.Value));
                    p2 = new SqlParameter("@qID", q.qId);
                    p3 = new SqlParameter("@strength", q.strength);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    x = cmd.ExecuteNonQuery();

                    //update choices and answer in Choices table
                    Char[] answers = new Char[5];
                    answers[0] = Convert.ToChar("X");
                    for (int i = 1; i < answers.Length; i++)
                    {
                        if (i == q.answer)
                        {
                            answers[i] = Convert.ToChar("Y");
                        }
                        else
                        {
                            answers[i] = Convert.ToChar("N");
                        }
                    }

                    //choice 1
                    cmd = new SqlCommand("UPDATE Choices SET Text = @text, Status = @status WHERE Choice_Id = @ChoiceID", conStr);
                    p1 = new SqlParameter("@ChoiceID", q.choiceID1);
                    p2 = new SqlParameter("@text", q.choice1);
                    p3 = new SqlParameter("@status", answers[1]);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    x = cmd.ExecuteNonQuery();

                    //choice 2
                    cmd = new SqlCommand("UPDATE Choices SET Text = @text, Status = @status WHERE Choice_Id = @ChoiceID", conStr);
                    p1 = new SqlParameter("@ChoiceID", q.choiceID2);
                    p2 = new SqlParameter("@text", q.choice2);
                    p3 = new SqlParameter("@status", answers[2]);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    x = cmd.ExecuteNonQuery();

                    //choice 3
                    cmd = new SqlCommand("UPDATE Choices SET Text = @text, Status = @status WHERE Choice_Id = @ChoiceID", conStr);
                    p1 = new SqlParameter("@ChoiceID", q.choiceID3);
                    p2 = new SqlParameter("@text", q.choice3);
                    p3 = new SqlParameter("@status", answers[3]);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    x = cmd.ExecuteNonQuery();

                    //choice 4
                    cmd = new SqlCommand("UPDATE Choices SET Text = @text, Status = @status WHERE Choice_Id = @ChoiceID", conStr);
                    p1 = new SqlParameter("@ChoiceID", q.choiceID4);
                    p2 = new SqlParameter("@text", q.choice4);
                    p3 = new SqlParameter("@status", answers[4]);
                    cmd.Parameters.Add(p1);
                    cmd.Parameters.Add(p2);
                    cmd.Parameters.Add(p3);
                    x = cmd.ExecuteNonQuery();
                }

                //delete the removed questions in DB
                removeQuestionsFromDB();

                //add new questions
                List<Question> newQuestions = (List<Question>)Session["newQuestions"];
                if (newQuestions.Count > 0)
                {
                    saveQuestions(newQuestions);
                    assignQstoTest();
                }

                //Show success Alerts
                System.Web.HttpContext.Current.Response.Write("<SCRIPT LANGUAGE='JavaScript'>");
                System.Web.HttpContext.Current.Response.Write("alert('Success! All Test questions updated.')");
                System.Web.HttpContext.Current.Response.Write("</SCRIPT>");
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

        private void assignQstoTest()
        {
            //add qsID to a session
            List<int> allNewQuestions = (List<int>) Session["allQsID"];
            SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);

            try
            {
                conStr.Open();
                //get all test versions of the module
                List<int> testVersions = new List<int>();
                SqlCommand cmd = new SqlCommand("SELECT Test_Id FROM Test WHERE Node_Id = @nodeID", conStr);
                SqlParameter p1 = new SqlParameter("@nodeID", Convert.ToInt32(ddlModule.SelectedItem.Value));
                cmd.Parameters.Add(p1);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    testVersions.Add(reader.GetInt32(0));
                }
                reader.Close();

                //assign new questions to each test versions
                foreach(int i in testVersions)
                {
                    //get max question_Order
                    int qs_order = 1;
                    cmd = new SqlCommand("SELECT MAX(Question_order) FROM Test_questions WHERE Test_Id = @testId", conStr);
                    p1 = new SqlParameter("@testId", i);
                    cmd.Parameters.Add(p1);
                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(0))
                        {
                            qs_order += reader.GetInt32(0);
                        }
                    }
                    reader.Close();

                    //insert new questions to the test questions
                    foreach (int j in allNewQuestions)
                    {
                        cmd = new SqlCommand("INSERT INTO Test_questions VALUES(@testID, @qsID, @order)", conStr);
                        p1 = new SqlParameter("@testID", i);
                        SqlParameter p2 = new SqlParameter("@qsID", j);
                        SqlParameter p3 = new SqlParameter("@order", qs_order);
                        cmd.Parameters.Add(p1);
                        cmd.Parameters.Add(p2);
                        cmd.Parameters.Add(p3);
                        int x = cmd.ExecuteNonQuery();
                        qs_order ++;
                    }
                }
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

        private void removeQuestionsFromDB()
        {
            //delete data
            List<int> removeIds = (List<int>)Session["DeleteQsID"];
            if (removeIds.Count > 0)
            {
                SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
                try
                {
                    conStr.Open();
                    foreach (int i in removeIds)
                    {
                        //remove choices
                        SqlCommand cmd = new SqlCommand("DELETE FROM Choices WHERE Question = @qsID", conStr);
                        SqlParameter p1 = new SqlParameter("@qsID", i);
                        cmd.Parameters.Add(p1);
                        int x = cmd.ExecuteNonQuery();

                        //remove Test_Questions
                        cmd = new SqlCommand("DELETE FROM Test_questions WHERE Question_Id = @qsID", conStr);
                        p1 = new SqlParameter("@qsID", i);
                        cmd.Parameters.Add(p1);
                        x = cmd.ExecuteNonQuery();

                        //remove questions
                        cmd = new SqlCommand("DELETE FROM Questions WHERE Question_Id = @qsID", conStr);
                        p1 = new SqlParameter("@qsID", i);
                        cmd.Parameters.Add(p1);
                        x = cmd.ExecuteNonQuery();

                    }// end foreach
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
            }//end if
        }
}