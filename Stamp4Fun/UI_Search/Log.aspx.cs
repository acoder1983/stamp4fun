using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun.UI_Search
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.GridView1.DataSource = DBUtil.Misc.GetRecentLogs();
            this.GridView1.DataBind();
        }
    }
}