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

        protected void gvEG_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");
                if (ddlDepartment != null)
                {
                 //   ddlDepartment.DataSource = new mainSQL().getDepartmentList();
                    ddlDepartment.DataBind();
                    ddlDepartment.SelectedValue = gvEG.DataKeys[e.Row.RowIndex].Values[1].ToString();
                }
            }
            if (e.Row.RowType == DataControlRowType.EmptyDataRow)
            {
                DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");
                if (ddlDepartment != null)
                {
                //    ddlDepartment.DataSource = new mainSQL().getDepartmentList();
                    ddlDepartment.DataBind();
                }
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                DropDownList ddlDepartment = (DropDownList)e.Row.FindControl("ddlDepartment");
             //   ddlDepartment.DataSource = new mainSQL().getDepartmentList(); ;
                ddlDepartment.DataBind();
            }
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
                    Question qs = new Question(text, choice1, choice2, choice3, choice4, strength, answer);

                    //add to session
                    List<Question> testQs = (List<Question>) Session["testQuestion"];
                    testQs.Add(qs);
                    Session["testQuestion"] = testQs;
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
            testQs.RemoveAt(e.RowIndex);
            Session["testQuestion"] = testQs;
            FillEmployeeGrid();
        }

        protected void gvEG_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridViewRow emptyRow = gvEG.Rows[e.RowIndex];
                String text = Convert.ToString(((TextBox)emptyRow.FindControl("txtQuestionText")).Text);
                String choice1 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer1")).Text));
                String choice2 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer2")).Text));
                String choice3 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer3")).Text));
                String choice4 = (Convert.ToString(((TextBox)emptyRow.FindControl("txtAnswer4")).Text));

                int strength = Convert.ToInt32(((DropDownList)emptyRow.FindControl("ddlStrength")).SelectedValue);

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
                Question qs = new Question(text, choice1, choice2, choice3, choice4, strength, answer);

                //add to session
                List<Question> testQs = (List<Question>)Session["testQuestion"];
                testQs.Insert(e.RowIndex, qs);
                testQs.RemoveAt(e.RowIndex + 1);
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
                //for gridview
                this.gvEG.DataSource = new List<object>();
                gvEG.DataBind();

                //store all values
                ViewState["courseID"] = ddlCourse.SelectedItem.Value;
                ViewState["topicID"] = this.ddlTopic.SelectedItem.Value;
                ViewState["nodeID"] = this.ddlModule.SelectedItem.Value;
                Session["testQuestion"] = new List<Question>();

                pnlTest.Visible = true;
            }
        }

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            //clear all items
            ddlTopic.Items.Clear();

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

}