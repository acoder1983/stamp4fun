using System;
using System.Data;
using System.Collections.Generic;
//
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.tableEditors.Width=Unit.Percentage(100.0);
            tableEditors.Style.Add("text-align", "center");
            int nameLineNum=6;
            int allLineNum = 12;
            DBUtil.User user = new DBUtil.User();
            DataTable dt = user.GetUsers();
            TableRow r=null;
            for (int i = 0; i < nameLineNum*allLineNum; ++i)
            {
                if (i % nameLineNum == 0)
                {
                    r = new TableRow();
                    tableEditors.Rows.Add(r);
                }
                TableCell c = new TableCell();
                c.Width = Unit.Percentage(100.0 / nameLineNum);
                if (i < dt.Rows.Count)
                {
                    c.Text = string.Format("{0} ({1})", dt.Rows[i]["username"], dt.Rows[i]["lognum"]);
                }
                else
                {
                    //c.Text = Guid.NewGuid().ToString().Substring(0, 6);
                }
                
                r.Cells.Add(c);
            }

            Table t = new Table();
            t.Style.Add("text-align", "center");
            t.Width = Unit.Percentage(100.0);
            r = new TableRow();
            t.Rows.Add(r);
            TableCell tc = new TableCell();
            tc.Text = "Always thanks June :)";
            r.Cells.Add(tc);
            tableEditors.Parent.Controls.Add(t);
        }
    }
}
