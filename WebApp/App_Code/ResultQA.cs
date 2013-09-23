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
    public string AnswerResult { get; set; }
    public string QuestionWeight { get; set; }
    public string RightAnswer { get; set;}

	public ResultQA(string Qs, string Ans, int Right, string Result, string weight, string rightAns)
	{
        this.Question = Qs;
        this.Answer = Ans;
        this.IsRight = Right;
        this.AnswerResult = Result;
        this.QuestionWeight = weight;
        this.RightAnswer = rightAns;

	}
}