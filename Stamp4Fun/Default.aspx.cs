using System;
using System.Collections.Generic;
//
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.IsPostBack) return;
            
            Control c = this.LoadControl("UI_Main/Main.ascx");
            SiteMaster sm = this.Master as SiteMaster;
            sm.MainPlaceHolder.Controls.Add(c);
        }
    }
}
