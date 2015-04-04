using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun.UI_Comm
{
    public class Util
    {
        public static Label CreateBlankRowLabel()
        {
            Label blankRow = new Label();
            blankRow.Width = Unit.Percentage(100.0);
            return blankRow;
        }

        public static Label CreateBetweenLabel(double percent)
        {
            Label lblTbAndBtn = new Label();
            lblTbAndBtn.Width = Unit.Percentage(percent);
            return lblTbAndBtn;
        }

        public static string GetWebRootFilePath()
        {
            SiteMaster sm = new SiteMaster();
            System.IO.FileInfo fi = new System.IO.FileInfo(sm.GetType().Assembly.Location);
            return fi.DirectoryName.Substring(0, fi.DirectoryName.Length - 3);
        }

        public static string GetGuidString()
        {
            return Guid.NewGuid().ToString().Replace("-", "");
        }

        public static string GetWebRootHttpPath(string url)
        {
            // if localhost, find the third /
            // else find the stamp/
            string ret = string.Empty;
            int pos = url.IndexOf("localhost");
            if ( pos != -1)
            {
                pos = url.IndexOf("/", pos);
                ret = url.Substring(0, pos);
            }
            else
            {
                string root = "/stamp";
                pos = url.IndexOf(root);
                ret = url.Substring(0, pos + root.Length); 
            }
            return ret;
        }

        public static void AddClickOnLink(WebControl c, string url)
        {
            c.Attributes.Add("onclick", string.Format("window.open('{0}')", url));
        }
    }
}