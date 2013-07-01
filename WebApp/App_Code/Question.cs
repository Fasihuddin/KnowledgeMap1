using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Question
/// </summary>
public class Question
{
    public String text { get; set; }
    public String choice1 { get; set; }
    public String choice2 { get; set; }
    public String choice3 { get; set; }
    public String choice4 { get; set; }
    public int choiceID1 { get; set; }
    public int choiceID2 { get; set; }
    public int choiceID3 { get; set; }
    public int choiceID4 { get; set; }

    public int strength { get; set; }
    public int answer;
    public byte[] imgData { get; set; }
    public String contentType { get; set; }
    public String imgName { get; set; }
    public int qId { get; set; }

	public Question(String text, String choice1, String choice2, String choice3, String choice4, 
        int strength, int answer, byte[] imgData, String contentType, String imgName)
	{
        this.text = text;
        this.strength = strength;
        this.answer = answer;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.choice3 = choice3;
        this.choice4 = choice4;
        this.imgData = imgData;
        this.contentType = contentType;
        this.imgName = imgName;
	}

    public Question(int qId, String text)
    {
        this.qId = qId;
        this.text = text;
    }
}