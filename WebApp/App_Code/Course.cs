using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Course
/// </summary>
public class Course
{
    public int courseID {get; set;}
    public String name {get; set;}
    public String code {get; set;}
    public String description { get; set; }

	public Course(int courseID, String name, String code, String desc)
	{
        this.courseID = courseID;
        this.name = name;
        this.code = code;
        this.description = desc;
	}
}