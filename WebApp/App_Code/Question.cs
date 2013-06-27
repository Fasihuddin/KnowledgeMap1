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
    public int strength { get; set; }
    public int answer;

	public Question(String text, String choice1, String choice2, String choice3, String choice4, int strength, int answer)
	{
        this.text = text;
        this.strength = strength;
        this.answer = answer;
        this.choice1 = choice1;
        this.choice2 = choice2;
        this.choice3 = choice3;
        this.choice4 = choice4;
	}
}