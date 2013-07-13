using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

/// <summary>
/// Summary description for QuestionManager
/// </summary>
public class QuestionManager
{
	public QuestionManager()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    // Checking whether username is student or instructor
    public static String checkInstructor(string username)
    {
        string roles = "Student";
        //DATABASE CONNECTION TO CHECK
        string sqlstring = "SELECT Inst_Id FROM Instructors WHERE Inst_Id= '"+ username+"'";

        // create a connection with sqldatabase 
        SqlConnection conStr = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString);
        SqlDataReader reader;
        SqlCommand cmd = new SqlCommand(sqlstring, conStr);
        // open a connection with sqldatabase
        conStr.Open();
        reader = cmd.ExecuteReader();
       // if (!(reader.Read()) && reader.IsDBNull(0))
        if(reader.Read())
        {
            roles = "Instructor";   
        }
        return roles;
    }
}

// DO NOT DELETE THIS PART
public enum QuestionTypes
{
    CheckBox, //MultipleChoice questions
    SingleSelect //Select Answer questions
}

