using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Stamp4Fun.UI_Search
{
    public partial class SearchResult : System.Web.UI.Page
    {
        [Serializable]
        class QueryState
        {
            public DataTable dtSetId = null;
            public int curPageIdx = 0;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            SiteMaster sm = this.Master as SiteMaster;
            sm.MainPlaceHolder.ID = "MainHolderSearch";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!this.IsPostBack)
            {
                this.LoadUI();
            }
        }

        Panel pStampList = null;
    
        void LoadUI()
        {
            SiteMaster sm = this.Master as SiteMaster;
            //sm.MainPlaceHolder.ID = "MainHolderSearch";
            sm.MainPlaceHolder.Controls.Clear();
            bool isQsNotExist = false;
            DBUtil.Set set = new DBUtil.Set();
            if (isQsNotExist = (this.ViewState["QueryState"] == null))
            {
                qs = new QueryState();
                this.ViewState["QueryState"] = qs;
                string cond = Comm.Util.DecodeUrl(Request["cond"]);
                qs.dtSetId = set.GetSetIdData(cond);
                qs.curPageIdx = 1;
            }
            qs = this.ViewState["QueryState"] as QueryState;
            Panel pCond = new Panel();
            pCond.ID = "pCond";
            pCond.Width = Unit.Percentage(100.0);
            pCond.Style.Add("text-align", "center");

            Panel pResults = new Panel();
            pResults.ID = "pResults";
            Panel pNaviTop = new Panel();
            
            Panel pNaviBottom = new Panel();
            pStampList = new Panel();
            
            LoadCond(pCond);
            LoadNavi(pNaviTop);
            LoadNavi(pNaviBottom);
            pResults.Controls.Add(pNaviTop);
            pResults.Controls.Add(pStampList);
            pResults.Controls.Add(pNaviBottom);
            DisplayOnePage(qs.curPageIdx);
            
            
            sm.MainPlaceHolder.Controls.Add(pCond);
            sm.MainPlaceHolder.Controls.Add(pResults);

        }

        void SetControlId(ControlCollection cc)
        {
            foreach (Control c in cc)
            {
                //c.ID = c.n;
                SetControlId(c.Controls);
            }
        }

        DropDownList dlNation = new DropDownList();
        TextBox tbStampNo;

        void LoadCond(Panel pCond)
        {
            Label lNation = new Label();
            lNation.Text = "国家地区";
            
            dlNation.AutoPostBack = false;
            dlNation.Width = Unit.Percentage(16.0);
            /*
            Label lPrint = new Label();
            lPrint.Text = "印刷方式";
            DropDownList dlPrint = new DropDownList();
            dlPrint.AutoPostBack = false;
            dlPrint.Width = Unit.Percentage(10.0);
            Label lFrom = new Label();
            lFrom.Text = "年份从";
            TextBox tbFromYear = new TextBox();
            tbFromYear.Width = Unit.Percentage(10.0);
            Label lTo = new Label();
            lTo.Text = "到";
            TextBox tbToYear = new TextBox();
            tbToYear.Width = Unit.Percentage(10.0);
            */
            Label lStampNo = new Label();
            lStampNo.Text = "邮票编号";
            tbStampNo = new TextBox();
            tbStampNo.Width = Unit.Percentage(10.0);
            Button btnSearch = new Button();
            btnSearch.Text = "查询";
            btnSearch.Click += new EventHandler(btnSearch_Click);
            
            LoadNation(dlNation);
            //LoadPrintType(dlPrint);

            pCond.Width = Unit.Percentage(100.0);
            pCond.Controls.Add(lNation);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(dlNation);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(2.0));
            /*
            pCond.Controls.Add(lFrom);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(tbFromYear);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(lTo);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(tbToYear);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(2.0));
            pCond.Controls.Add(lPrint);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(dlPrint);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(2.0));
            */
            pCond.Controls.Add(lStampNo);
            pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(0.5));
            pCond.Controls.Add(tbStampNo);
            //pCond.Controls.Add(UI_Comm.Util.CreateBetweenLabel(2.0));
            pCond.Controls.Add(btnSearch);
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            DBUtil.Nation nation = new DBUtil.Nation();
            string cond = string.Empty;
            if (!string.IsNullOrEmpty(dlNation.SelectedValue))
            {
                cond += string.Format("nation_id={0} and ", dlNation.SelectedItem.Value);
            }
            if (!string.IsNullOrEmpty(tbStampNo.Text))
            {
                cond += string.Format("stamp_no='{0}' and ", tbStampNo.Text);
            }
            if (cond.Length > 0)
            {
                cond = cond.Substring(0, cond.LastIndexOf("and") - 1);
                string webRoot = UI_Comm.Util.GetWebRootHttpPath(this.Request.Url.ToString());
                Response.Redirect(string.Format("{0}/{1}?cond={2}", webRoot, "UI_Search/SearchResult.aspx", cond));
            }
        }

        void LoadNation(DropDownList ddl)
        {
            ddl.Items.Clear();
            DBUtil.Nation nation = new DBUtil.Nation();
            DataTable dtNation = nation.GetNations();
            
            foreach (DataRow dr in dtNation.Rows)
            {
                ListItem li = new ListItem(dr["name_cn"].ToString());
                li.Value = dr["id"].ToString();
                ddl.Items.Add(li);
            }

        }

        void LoadPrintType(DropDownList ddl)
        {
            DBUtil.PrintType pt = new DBUtil.PrintType();
            DataTable dtPrintType = pt.GetTopObjects();
            foreach (DataRow dr in  dtPrintType.Rows)
            {
                ddl.Items.Add(dr["desp"].ToString());
            }
        }

        const string TEMP_PIC_DIR = "TempPic";

        Collection<Label> lblPages = new Collection<Label>();
        
        void LoadNavi(Panel pNavi)
        {
            pNavi.Style.Add("text-align", "center");
            pNavi.Width = Unit.Percentage(100.0);

            pNavi.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());

            Button bMoveToFirst = new Button();
            bMoveToFirst.Text = "|<<";
            bMoveToFirst.Click += new EventHandler(bMoveToFirst_Click);
            Button bMoveToPrev = new Button();
            bMoveToPrev.Text = "<";
            bMoveToPrev.Click += new EventHandler(bMoveToPrev_Click);
            Label l1 = new Label();
            l1.Width = Unit.Percentage(100.0 / 8);
            Label  l3 = new Label();
            l3.Width = Unit.Percentage(100.0 / 10);
            l3.Text = string.Format("共 {0} 页", qs.dtSetId.Rows.Count == 0 ?
                    0 : qs.dtSetId.Rows.Count / PAGE_SET_NUM + 1);
            TextBox tPage = new TextBox();
            tPage.Text = qs.dtSetId.Rows.Count == 0 ? "0" : qs.curPageIdx.ToString();
            tPage.Width = Unit.Percentage(100.0 / 30);
            Button bGo = new Button();
            bGo.Text = "Go";
            Label l2 = new Label();
            l2.Width = Unit.Percentage(100.0 / 8);
            Button bMoveToNext = new Button();
            bMoveToNext.Text = ">";
            bMoveToNext.Click += new EventHandler(bMoveToNext_Click);
            Button bMoveToLast = new Button();
            bMoveToLast.Text = ">>|";
            bMoveToLast.Click += new EventHandler(bMoveToLast_Click);

            lblPages.Add(l3);

            pNavi.Controls.Add(bMoveToFirst);
            pNavi.Controls.Add(bMoveToPrev);
            pNavi.Controls.Add(l1);
            pNavi.Controls.Add(l3);
            pNavi.Controls.Add(tPage);
            pNavi.Controls.Add(bGo);
            pNavi.Controls.Add(l2);
            pNavi.Controls.Add(bMoveToNext);
            pNavi.Controls.Add(bMoveToLast);

            pNavi.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());

        }

        void bMoveToLast_Click(object sender, EventArgs e)
        {
            qs.curPageIdx = qs.dtSetId.Rows.Count / PAGE_SET_NUM + 1;
            //DisplayOnePage(qs.curPageIdx);
            LoadUI();
        }

        void bMoveToNext_Click(object sender, EventArgs e)
        {
            if (qs.curPageIdx == qs.dtSetId.Rows.Count / PAGE_SET_NUM + 1)
            {
                //DisplayOnePage(qs.curPageIdx);
                LoadUI();
            }
            else
            {
                ++qs.curPageIdx;
                //DisplayOnePage(++qs.curPageIdx);
                LoadUI();
            }
        }

        
        void bMoveToPrev_Click(object sender, EventArgs e)
        {
            if (qs.curPageIdx == 1)
            {
                //DisplayOnePage(qs.curPageIdx);
                LoadUI();
            }
            else
            {
                --qs.curPageIdx;
                //DisplayOnePage(--qs.curPageIdx);
                LoadUI();
            }
        }

        void bMoveToFirst_Click(object sender, EventArgs e)
        {
            qs.curPageIdx = 1;
            //DisplayOnePage(qs.curPageIdx);
            LoadUI();
        }

        QueryState qs = null;


        const int PAGE_SET_NUM = 5;

        DataTable GetOnePageSetId(int index)
        {
            DataTable dt = qs.dtSetId.Clone();
            if (qs.dtSetId.Rows.Count > 0)
            {
                for (int i = PAGE_SET_NUM * (index - 1); i < 
                    (PAGE_SET_NUM * index > qs.dtSetId.Rows.Count ? 
                    qs.dtSetId.Rows.Count : PAGE_SET_NUM * index); ++i)
                {
                    dt.ImportRow(qs.dtSetId.Rows[i]);
                }
            }
            return dt;
        }

        void DisplayOnePage(int index)
        {
            //pStampList.Controls.Clear();
            //pStampList = new Panel();
            DataTable dtIds = GetOnePageSetId(index);
            DBUtil.Set set = new DBUtil.Set();
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            DBUtil.Picture pic = new DBUtil.Picture();
            foreach (DataRow drSetId in dtIds.Rows)
            {
                DataTable dtSet = set.GetSetById(drSetId["set_id"]);
                if (dtSet.Rows.Count == 1)
                {
                    pStampList.Controls.Add(LoadSet(dtSet.Rows[0]));
                    DataTable dtStamps = stamp.GetStampBySetId(drSetId["set_id"]);
                    DataTable dtPics = pic.GetPicById(pic.GetPicIdByElemId(drSetId["set_id"]));
                    dtPics.Columns.Add(new DataColumn("stamp_id"));
                    foreach (DataRow dr in dtStamps.Rows)
                    {
                        pStampList.Controls.Add(LoadStamp(dr));
                        if (!(dr["pic_id"] is DBNull))
                        {
                            DataTable dt = pic.GetPicById(dr["pic_id"]);
                            dt.Columns.Add(new DataColumn("stamp_id"));
                            dt.Rows[0]["stamp_id"] = dr["no"];
                            dtPics.Merge(dt);
                        }
                    }
                    
                    // display set cv 
                    if (dtStamps.Rows.Count > 1)
                    {
                        pStampList.Controls.Add(LoadSetCv(dtStamps));
                    }
                    Table t = null;
                    for (int i=0; i< dtPics.Rows.Count; ++i)
                    {
                        if (i % PIC_LINE_NUM == 0)
                        {
                            t = new Table();
                            t.Rows.Add(new TableRow());
                            t.Rows.Add(new TableRow());
                            pStampList.Controls.Add(t);
                        }
                        LoadPic(dtPics.Rows[i], t);
                    }
                }

                pStampList.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());
                pStampList.Controls.Add(UI_Comm.Util.CreateBlankRowLabel());
            }
        }

        const int PIC_LINE_NUM = 5;
        const int PIC_SMALL_SIZE = 200;

        string GetPicName(DataRow dr, bool small)
        {
            // name format stamp_id+size+time
            string time = DBUtil.Misc.GetElemLastLogTime(dr["id"]);
            time = time.Replace(":", string.Empty).Replace("/", string.Empty).Replace(" ", string.Empty);
            return string.Format("{0}_{1}_{2}.jpg", dr["id"], small ? "small" : "large", time);
        }

        Table LoadPic(DataRow drPic, Table t)
        {
            t.Style.Add("text-align", "center");
            TableRow r = t.Rows[0];
            DirectoryInfo dirPic = new DirectoryInfo(Request.PhysicalApplicationPath + "/" + TEMP_PIC_DIR);
            if(!dirPic.Exists)
            {
                dirPic.Create();
            }
            
            // create temp pic
            string smallPicName = GetPicName(drPic, true);
            string smallPicPath = dirPic.FullName + "/" + smallPicName;
            if(!File.Exists(smallPicPath))
                DBUtil.Picture.SaveSmallPic(drPic, smallPicPath);
            string largePicName = GetPicName(drPic, false);
            string largePicPath = dirPic.FullName + "/" + largePicName;
            if (!File.Exists(largePicPath))
                DBUtil.Picture.SaveLargePic(drPic, largePicPath);
            
            TableCell c = new TableCell();
            ImageButton ib = new ImageButton();
            string webRoot = UI_Comm.Util.GetWebRootHttpPath(Request.Url.ToString());
            UI_Comm.Util.AddClickOnLink(ib, string.Format("{0}/{1}/{2}", 
                webRoot, TEMP_PIC_DIR, largePicName));
           
            ib.ImageUrl = string.Format("{0}/{1}/{2}", webRoot, TEMP_PIC_DIR, smallPicName);
            c.Controls.Add(ib);
            r.Cells.Add(c);
            // display text
            r = t.Rows[1];
            c = new TableCell();
            c.Width = Unit.Pixel(PIC_SMALL_SIZE);
            c.Text = string.Format("{0} {1}", drPic["stamp_id"], drPic["desp_cn"]);
            c.Style.Add("word-wrap", "break-word");
            c.Style.Add("word-break", "break-all");
            
            r.Cells.Add(c);

            // insert blanks
            //c = new TableCell();
            //c.Width = Unit.Pixel(5);
            //t.Rows[0].Cells.Add(c);
            //c = new TableCell();
            //c.Width = Unit.Pixel(5);
            //t.Rows[1].Cells.Add(c);
            return t;
        }

        void ImageBtn_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Write(string.Format("<script>window.open('http://localhost:44533/TempPic/424_s.jpg','_blank')</script>"));
            //LoadUI();
            Response.Redirect("http://localhost:44533/TempPic/424_s.jpg");
            ClientScript.RegisterStartupScript(this.GetType(), "message", 
                "<script language='javascript'>this.form.target='_self'</script>");
            //target 属性设置回_self，防止点其它按钮时，也在新窗口打开
        }

        Panel LoadSet(DataRow dr)
        {
            Panel p = new Panel();
            p.Width = Unit.Percentage(100.0);
            Table t = new Table();
            t.Width = Unit.Percentage(100.0);
            TableRow r = new TableRow();
            
            t.Rows.Add(r);
            TableCell c = null;
            string[] colShown = new string[] { "date", "print_desp", "perf" };
            if (dr.Table.Columns.IndexOf("date") == -1)
            {
                dr.Table.Columns.Add("date");
            }
            string date = dr["year"].ToString();
            if (!string.IsNullOrEmpty(dr["month_id"].ToString()))
            {
                date += "." + dr["month_desp"].ToString();
                if (!string.IsNullOrEmpty(dr["day"].ToString()))
                {
                    date += "." + dr["day"].ToString();
                }
            }
            double setCellPercent = 0.0;
            dr["date"] = date;
            for(int i=0;i<colShown.Length;++i)
            {
                c = new TableCell();
                r.Cells.Add(c);
                c.Width = Unit.Percentage(STAMP_COL_WIDTH[i]);
                c.Text = dr[colShown[i]].ToString();
                setCellPercent += STAMP_COL_WIDTH[i];
            }
            
            c = new TableCell();
            c.Width = Unit.Percentage(100.0 - setCellPercent);
            // display topics
            DBUtil.Topic2Elem t2e = new DBUtil.Topic2Elem();
            DataTable dt = t2e.GetElemTopics(dr["id"]);
            if (dt.Rows.Count > 0)
            {
                string topics = string.Empty;
                DBUtil.Topic topic = new DBUtil.Topic();
                foreach (DataRow row in dt.Rows)
                {
                    topics += topic.GetTopicFullName(Convert.ToInt32(row["topic_id"])) + " ";
                }
                c.Text = topics;
            }
            r.Cells.Add(c);
            p.Controls.Add(t);

            if (!(dr["desp_cn"] is DBNull))
            {
                t = new Table();
                r = new TableRow();
                t.Rows.Add(r);

                TableCell c2 = new TableCell();
                r.Cells.Add(c2);
                c2.Text = dr["desp_cn"].ToString();
                c2.Style.Add("word-wrap", "break-word");
                c2.Style.Add("word-break", "break-all");

                p.Controls.Add(t);
            }

            if (!(dr["desp_en"] is DBNull))
            {
                t = new Table();
                r = new TableRow();
                t.Rows.Add(r);

                TableCell c2 = new TableCell();
                r.Cells.Add(c2);
                c2.Text = dr["desp_en"].ToString();
                c2.Style.Add("word-wrap", "break-word");
                c2.Style.Add("word-break", "break-all");

                p.Controls.Add(t);
            }
            
            return p;
        }

        string[] STAMP_COL_SHOWN = new string[] { "no", "denom", "color", "cv_new", "cv_old" };
        double[] STAMP_COL_WIDTH = new double[] { 12.0, 10.0, 20.0, 10.0, 10.0 };

        Panel LoadStamp(DataRow dr)
        {
            Panel p = new Panel();
            p.Width = Unit.Percentage(100.0);
            Table t = new Table();
            t.Width = Unit.Percentage(100.0);
            TableRow r = new TableRow();
            t.Rows.Add(r);
            TableCell c = null;
            double stampCellPercent = 0.0;
            string[] colShown = STAMP_COL_SHOWN;
            
            for (int i = 0; i < colShown.Length; ++i)
            {
                c = new TableCell();
                r.Cells.Add(c);
                c.Width = Unit.Percentage(STAMP_COL_WIDTH[i]);
                c.Text = dr[colShown[i]].ToString();
                stampCellPercent += STAMP_COL_WIDTH[i];
            }
            c = new TableCell();
            c.Width = Unit.Percentage(100.0 - stampCellPercent);
            r.Cells.Add(c);
            if (!(dr["desp"] is DBNull))
            {
                c.Text = dr["desp"].ToString();
            }
            p.Controls.Add(t);
            return p;
        }

        Panel LoadSetCv(DataTable dtStamps)
        {
            Panel p = new Panel();
            p.Width = Unit.Percentage(100.0);
            Table t = new Table();
            t.Width = Unit.Percentage(100.0);
            TableRow r = new TableRow();
            t.Rows.Add(r);
            TableCell c = null;
            double stampCellPercent = 0.0;
            string[] colShown = STAMP_COL_SHOWN;
            int col_cv_new = -1;
            int col_cv_old = -1;
            for (int i = 0; i < colShown.Length; ++i)
            {
                c = new TableCell();
                r.Cells.Add(c);
                c.Width = Unit.Percentage(STAMP_COL_WIDTH[i]);
                if(colShown[i] == "cv_new")
                    col_cv_new=i;
                if(colShown[i] == "cv_old")
                    col_cv_old=i;
                stampCellPercent += STAMP_COL_WIDTH[i];
            }
            double cv_new = 0.0;
            double cv_old = 0.0;
            foreach (DataRow dr in dtStamps.Rows)
            {
                cv_new += Convert.ToDouble(dr["cv_new"]);
                cv_old += Convert.ToDouble(dr["cv_old"]);
            }
            r.Cells[col_cv_new-1].Text = "合计";
            r.Cells[col_cv_new].Text = cv_new.ToString();
            r.Cells[col_cv_old].Text = cv_old.ToString();
            c = new TableCell();
            c.Width = Unit.Percentage(100.0 - stampCellPercent);
            r.Cells.Add(c);
            p.Controls.Add(t);
            return p;
        }

    }
}