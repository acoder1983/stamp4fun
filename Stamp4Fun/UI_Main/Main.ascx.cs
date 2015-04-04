using System;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun.Main
{
    public partial class Main : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (this.IsPostBack) return;
            LoadUI();
        }

        Collection<TreeView> trees=new Collection<TreeView>();
        TextBox tbYear;
        TextBox tbStampNo;

        private void LoadUI()
        {
            Panel p = new Panel();
            //p.Height = Unit.Pixel(400);//Unit.Percentage(100.0);
            p.Width = Unit.Percentage(100.0);
            p.Style.Add("text-align", "center");

            Label lblCaption = new Label();
            lblCaption.Text = "外邮吧邮票目录";
            lblCaption.Width = Unit.Percentage(100.0);
            lblCaption.Font.Size = 20;
            lblCaption.Font.Bold = true;
            lblCaption.ForeColor = System.Drawing.Color.DarkCyan;

            Label lblYear = new Label();
            lblYear.Text = "年份";
            tbYear = new TextBox();
            Label lblStampNo = new Label();
            lblStampNo.Text = "斯科特邮票编号";
            tbStampNo = new TextBox();
            Button btnSearch = new Button();
            btnSearch.Width = Unit.Percentage(100.0 / 12);
            btnSearch.Text = "搜索一下";
            btnSearch.Click += new EventHandler(OnSearchClick);

            //p.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());
            //p.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());
            
            p.Controls.Add(lblCaption);
            p.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());
            p.Controls.Add(lblYear);
            p.Controls.Add(UI_Comm.Util.CreateBetweenLabel(1.0));
            p.Controls.Add(tbYear);
            p.Controls.Add(UI_Comm.Util.CreateBetweenLabel(1.0));
            p.Controls.Add(lblStampNo);
            p.Controls.Add(UI_Comm.Util.CreateBetweenLabel(1.0));
            p.Controls.Add(tbStampNo);
            p.Controls.Add(UI_Comm.Util.CreateBetweenLabel(1.0));
            p.Controls.Add(btnSearch);
            p.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());

            Table t = new Table();
            t.Width = Unit.Percentage(100.0);
            TableRow row = new TableRow();
            t.Rows.Add(row);
            row = new TableRow();
            t.Rows.Add(row);
            TableCell cell = new TableCell();
            cell.Width = Unit.Percentage(100.0 / 4);
            t.Rows[0].Cells.Add(cell);
            cell = new TableCell();
            cell.Width = Unit.Percentage(100.0 / 4);
            t.Rows[1].Cells.Add(cell);

            string[] captions = new string[]{"专题分类", "国家地区", "印刷方式"};
            DBUtil.ITreeObject[] objs = new DBUtil.ITreeObject[]{
                new DBUtil.Topic(), new DBUtil.Nation(), new DBUtil.PrintType()};
            for (int i = 0; i < captions.Length; ++i)
            {
                cell = new TableCell();
                cell.Width = Unit.Percentage(100.0 / 6);
                Label lbl = new Label();
                lbl.Text = captions[i];
                cell.Controls.Add(lbl);
                t.Rows[0].Cells.Add(cell);

                TreeView tree = new TreeView();
                trees.Add(tree);
                tree.ShowCheckBoxes = TreeNodeTypes.All;
                DataTable dt = objs[i].GetTopObjects();
                LoadChildNodes(dt, tree.Nodes, objs[i]);
                tree.CollapseAll();    
                cell = new TableCell();
                cell.Width = Unit.Percentage(100.0 / 6);
                cell.Controls.Add(tree);
                t.Rows[1].Cells.Add(cell);
                
            }

            cell = new TableCell();
            cell.Width = Unit.Percentage(100.0 / 4);
            t.Rows[0].Cells.Add(cell);
            cell = new TableCell();
            cell.Width = Unit.Percentage(100.0 / 4);
            t.Rows[1].Cells.Add(cell);
            t.GridLines = GridLines.Both;
            p.Controls.Add(t);
            this.Controls.Add(p);
        }

        void LoadChildNodes(DataTable dt, TreeNodeCollection nodes, DBUtil.ITreeObject obj)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode n = new TreeNode(dr[obj.NameCol].ToString());
                n.Value = dr["id"].ToString();
                nodes.Add(n);
                LoadChildNodes(obj.GetChildObjects(dr["id"]), n.ChildNodes, obj);
            }
        }

        void OnSearchClick(object sender, EventArgs e)
        {
            string cond = string.Empty;
            string[] idCols = new string[] { "topic_id", "nation_id", "print_id" };
            for (int i = 0; i < idCols.Length; ++i)
            {
                string subCond=string.Empty;
                GetTreeCondValues(trees[i].Nodes, ref subCond);
                if (subCond.Length > 0)
                {
                    cond += string.Format("({0} in ({1})) and ", idCols[i],
                        subCond.Substring(0, subCond.LastIndexOf(",")));
                }
            }
            if (!string.IsNullOrEmpty(this.tbYear.Text))
            {
                cond += string.Format("(year = '{0}') and ", this.tbYear.Text);
            }
            if (!string.IsNullOrEmpty(this.tbStampNo.Text))
            {
                cond += string.Format("(stamp_no = '{0}') and ", this.tbStampNo.Text);
            }
            if (cond.Length > 0)
            {
                cond = cond.Substring(0, cond.LastIndexOf("and") - 1);
                string webRoot = UI_Comm.Util.GetWebRootHttpPath(this.Request.Url.ToString());
                string url = string.Format("{0}/{1}?cond={2}", webRoot, "UI_Search/SearchResult.aspx", cond);
                Response.Redirect(Stamp4Fun.Comm.Util.EncodeUrl(url));
            }
        }

        void GetTreeCondValues(TreeNodeCollection nodes, ref string cond)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked)
                {
                    cond += string.Format("{0},", n.Value);
                }
                GetTreeCondValues(n.ChildNodes, ref cond);
            }

        }

        Table GetNationTable()
        {
            DBUtil.Nation nation = new DBUtil.Nation();
            DataTable dt = nation.GetNations();
            Table t = new Table();
            t.Width = Unit.Percentage(100.0);
            TableRow r = null;
            int lineNum = 6;
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                if (i % lineNum == 0)
                {
                    r = new TableRow();
                    t.Rows.Add(r);
                }
                TableCell c = new TableCell();
                c.Width = Unit.Percentage(100.0 / lineNum);
                LinkButton h = new LinkButton();
                string webRoot = UI_Comm.Util.GetWebRootHttpPath(Request.Url.ToString());
                
                h.Text = dt.Rows[i]["name_cn"].ToString();
                UI_Comm.Util.AddClickOnLink(h,
                    string.Format("{0}/UI_Search/SearchResult.aspx?nation_id={1}", 
                    webRoot, dt.Rows[i]["id"]));
                c.Controls.Add(h);
                r.Cells.Add(c);
                
            }
            return t;
        }

    }
}