using System;
using System.Collections.Generic;
//
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.NavigationMenu.MenuItemClick += new MenuEventHandler(NavigationMenu_MenuItemClick);
            this.NavigationMenu.Style.Add("color", "ffffff");
        }

        void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            string webRoot = UI_Comm.Util.GetWebRootHttpPath(this.Request.Url.ToString());
                    
            switch (e.Item.Text)
            {
                case "主页":
                    Response.Redirect(string.Format("{0}/Default.aspx", webRoot));
                    break;
                case "关于":
                    Response.Redirect(string.Format("{0}/UI_Main/About.aspx", webRoot));
                    break;
            }
        }

        public ContentPlaceHolder MainPlaceHolder
        {
            get
            {
                return this.MainContent;
            }
        }
    }
}
