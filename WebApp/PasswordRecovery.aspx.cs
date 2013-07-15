using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;

public partial class PasswordRecovery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string sqlstring;
        sqlstring = "SELECT Student_Id FROM Students WHERE Student_Id=" + UsernameTxt.Text;

        // create a connection with sqldatabase 
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand(sqlstring, conStr);
        try
        {

            conStr.Open();

            // execute sql command and store a return values in reader
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                sqlstring.Remove(0);
                sqlstring = "SELECT SecurityQuestion FROM Students WHERE Student_Id=" + UsernameTxt.Text;
                SqlCommand cmd1 = new SqlCommand(sqlstring, conStr);
                   string Question = cmd1.ExecuteScalar().ToString();
                SecurityQs.Text = Question;
                Label1.Visible = true;
                SecurityQs.Visible = true;
                AnswerTxt.Visible = true;
                Button2.Visible = true;
            }
            conStr.Close();
        }
        catch (Exception ex)
        {
            MsgLbl.Text=ex.ToString();
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {string sqlstring;
        sqlstring = "SELECT SecurityAnswer FROM Students WHERE Student_Id=" + UsernameTxt.Text;

        // create a connection with sqldatabase 
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlCommand cmd = new SqlCommand(sqlstring, conStr);
        try
        {
            conStr.Open();

            //get the security answer           
            string Answer = cmd.ExecuteScalar().ToString();

            //get the Email
            cmd.CommandText = "SELECT Email FROM Students WHERE Student_Id=" + UsernameTxt.Text;
            string Email = cmd.ExecuteScalar().ToString();

            if (Answer.Equals(AnswerTxt.Text))
            {
                //get the Email
                cmd.CommandText = "SELECT Password FROM Students WHERE Student_Id=" + UsernameTxt.Text;
                string Password = cmd.ExecuteScalar().ToString();

                MailMessage objMail = new MailMessage("open-calc@hotmail.com", Email, "Password Recovery", "Hello, Your password is " + Password);
                NetworkCredential objNC = new NetworkCredential("open-calc@hotmail.com", "calc2013");
                SmtpClient objsmtp = new SmtpClient("smtp.live.com", 587); // for hotmail
                objsmtp.EnableSsl = true;
                objsmtp.Credentials = objNC;
                objsmtp.Send(objMail);
                MsgLbl.ForeColor = System.Drawing.Color.Green;
                MsgLbl.Text = "Your password is submitted to your registered email (" + Email+")";
            }
            else
            {
                MsgLbl.ForeColor = System.Drawing.Color.Red;
                MsgLbl.Text = "Sorry!! Answer is not correct";
            }
        }
        catch (Exception ex)
        {
            MsgLbl.Text = ex.ToString();
        }
    }
}