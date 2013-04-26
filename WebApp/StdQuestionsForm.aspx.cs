using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class StdQuestionsForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DataList1_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            HiddenField ansType = e.Item.FindControl("HiddenField1");
            Label questionId = e.Item.FindControl("lblQuestionID");
            RadioButtonList rbl = e.Item.FindControl("RadioButtonList1");
            CheckBoxList cbl = e.Item.FindControl("CheckBoxList1");
            TextBox txt = e.Item.FindControl("TextBox1");
            //DataSet ds = 

        }
    }


}