using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Topic
/// </summary>
public class Topic
{
    public int topicId { get; set; }
    public String name { get; set; }
    public String description { get; set; }

	public Topic(int topicId, String name, String desc)
	{
        this.topicId = topicId;
        this.name = name;
        this.description = desc;
	}
}