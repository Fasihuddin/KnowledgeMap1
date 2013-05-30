using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ResultQA
/// </summary>
public class ResultQA
{
    public string Question {get;set;}
    public string Answer {get;set;}
    public int IsRight {get;set;}
    public string answerResult { get; set; }

	public ResultQA(string Qs, string Ans, int Right, string Result)
	{
        this.Question = Qs;
        this.Answer = Ans;
        this.IsRight = Right;
        this.answerResult = Result;
	}
}