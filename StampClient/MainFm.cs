using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Stamp4Fun.StampClient
{
    public partial class MainFm : Form, Comm.INofifyProgress
    {
        public string FileName;
        public MainFm()
        {
            InitializeComponent();
            this.btnBatchAdd.Visible = this.btnBatchDel.Visible = true;
            this.WindowState = FormWindowState.Maximized;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.gridSet.MultiSelect = false;
            this.gridSet.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridSet.SelectionChanged += new EventHandler(gridSet_SelectionChanged);

            this.gridStamp.MultiSelect = false;
            this.gridStamp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.gridStamp.SelectionChanged += new EventHandler(gridStamp_SelectionChanged);
            this.gridStamp.RowPostPaint += new DataGridViewRowPostPaintEventHandler(gridStamp_RowPostPaint);

            this.gridSetPic.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(OnDataBindingComplete);
            this.gridStampPic.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(OnDataBindingComplete);

            this.Load += new EventHandler(MainFm_Load);
            this.FormClosing += new FormClosingEventHandler(MainFm_FormClosing);
            this.KeyDown += new KeyEventHandler(MainFm_KeyDown);
            this.btnNation.Click += new EventHandler(btnNation_Click);
            this.btnTopic.Click += new EventHandler(btnTopic_Click);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.btnAddTopic.Click += new EventHandler(btnAddTopic_Click);
            this.btnDelTopic.Click += new EventHandler(btnDelTopic_Click);
            this.btnAddSetPic.Click += new EventHandler(btnAddSetPic_Click);
            this.btnAddStampPic.Click += new EventHandler(btnAddStampPic_Click);
            this.btnDelSetPic.Click += new EventHandler(btnDelSetPic_Click);
            this.btnDelStampPic.Click += new EventHandler(btnDelStampPic_Click);
            this.btnBatchAdd.Click += new EventHandler(btnBatchAdd_Click);
            this.btnBatchDel.Click += new EventHandler(btnBatchDel_Click);
            this.btnAddSet.Click += new EventHandler(btnAddSet_Click);
            this.btnDelSet.Click += new EventHandler(btnDelSet_Click);
            this.btnAddStamp.Click += new EventHandler(btnAddStamp_Click);
            this.btnDelStamp.Click += new EventHandler(btnDelStamp_Click);

            this.cboNations.DropDown += new EventHandler(cboNations_DropDown);
            this.cboNations.KeyDown += new KeyEventHandler(cboNations_KeyDown);
        }

        void cboNations_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;    
        }

        void cboNations_DropDown(object sender, EventArgs e)
        {
            // load nations
            DBUtil.Nation nation = new DBUtil.Nation();
            dtNation = nation.GetNations();
            this.cboNations.Items.Clear();
            this.cboNations.Text = string.Empty;
            foreach (DataRow dr in dtNation.Rows)
            {
                this.cboNations.Items.Add(dr["name_cn"]);
            }    
        }

        void btnDelStamp_Click(object sender, EventArgs e)
        {
            if (this.gridStamp.SelectedCells.Count == 0)
            {
                MessageBox.Show("请先选择一枚邮票");
                return;
            }

            if (DBUtil.User.CURRENT_USER_ID !=
                this.gridStamp.SelectedCells[0].OwningRow.Cells["user_id"].Value.ToString())
            {
                MessageBox.Show("此枚邮票非您添加，无法删除");
                return;
            }

            if (this.gridStampPic.Rows.Count > 0)
            {
                MessageBox.Show("请先删除邮票的图片");
                return;
            }

            ((DataRowView)this.gridStamp.SelectedCells[0].OwningRow.DataBoundItem).Row.Delete();

        }

        void btnAddStamp_Click(object sender, EventArgs e)
        {
            // must have a set
            if (this.gridSet.Rows.Count == 0)
            {
                MessageBox.Show("请先增加整套信息");
                return;
            }
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            DataRow dr = this.ds.Tables[stamp.TABLE_NAME].NewRow();
            dr["id"] = Comm.Util.GenGuid();
            dr["set_id"] = this.gridSet.SelectedCells[0].OwningRow.Cells["id"].Value;
            dr["user_id"] = DBUtil.User.CURRENT_USER_ID;
            dr["cv_old"] = dr["cv_new"] = 0.0;
            // increment no
            if (this.gridStamp.Rows.Count > 0)
            {
                try
                {
                    dr["no"] = (Convert.ToInt32(this.gridStamp.Rows[
                        this.gridStamp.Rows.Count - 1].Cells["no"].Value) + 1).ToString();
                }
                catch
                {
                }
            }
            this.ds.Tables[stamp.TABLE_NAME].Rows.Add(dr);
        }

        void btnDelSet_Click(object sender, EventArgs e)
        {
            if (this.gridSet.SelectedCells.Count == 0)
            {
                MessageBox.Show("请先选择一套邮票");
                return;
            }

            if (DBUtil.User.CURRENT_USER_ID != 
                this.gridSet.SelectedCells[0].OwningRow.Cells["user_id"].Value.ToString())
            {
                MessageBox.Show("该套邮票非您添加，无法删除");
                return;
            }

            if (this.gridStamp.Rows.Count > 0)
            {
                MessageBox.Show("请先删除该套的邮票");
                return;
            }

            if (this.gridSetPic.Rows.Count > 0)
            {
                MessageBox.Show("请先删除整套邮票的图片");
                return;
            }
            ((DataRowView)this.gridSet.SelectedCells[0].OwningRow.DataBoundItem).Row.Delete();

        }

        void btnAddSet_Click(object sender, EventArgs e)
        {
            // if data changed, prompt
            if (this.ds.HasChanges())
            {
                MessageBox.Show("请先保存修改的数据");
                return;
            }
            // must choose country first
            if (string.IsNullOrEmpty(this.cboNations.Text))
            {
                MessageBox.Show("请先选择国家");
                return;
            }
            DBUtil.Set set = new DBUtil.Set();
            this.ds.Clear();
            this.ds.AcceptChanges();

            // add new set
            DataRow dr = this.ds.Tables[set.TABLE_NAME].NewRow();
            this.ds.Tables[set.TABLE_NAME].Rows.Add(dr);
            dr["id"] = Comm.Util.GenGuid();
            dr["nation_id"] = (new DBUtil.Nation()).GetNationIdByNameCn(
                this.cboNations.Text);
            dr["user_id"] = DBUtil.User.CURRENT_USER_ID;
            this.gridSet.Rows[this.gridSet.Rows.Count - 1].Cells["year"].Selected = true;
        }

        void btnBatchDel_Click(object sender, EventArgs e)
        {
            DBUtil.Picture pic = new DBUtil.Picture();
            DBUtil.Stamp stamp=new DBUtil.Stamp();
            Collection<DataRow> delRows = new Collection<DataRow>();
            foreach (DataRow dr in ds.Tables[stamp.TABLE_NAME].Rows)
            {
                if (string.IsNullOrEmpty(dr["pic_id"].ToString())) continue;
                DataRow[] rows = ds.Tables[pic.TABLE_NAME].Select(
                    string.Format("id='{0}'",dr["pic_id"]), null, DataViewRowState.CurrentRows);
                if (rows.Length > 0)
                {
                    if (rows[0]["user_id"].ToString() != DBUtil.User.CURRENT_USER_ID)
                    {
                        MessageBox.Show("有图片非您添加，无法全部删除");
                        return;
                    }
                    delRows.Add(rows[0]);
                }
            }
            
            foreach (DataRow dr in delRows)
            {
                dr.Delete();
            }

            foreach (DataGridViewRow row in this.gridStamp.Rows)
            {
                row.Cells["pic_id"].Value = DBNull.Value;
            }
        }

        void gridStamp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                DataGridView grid = sender as DataGridView;
                //添加行号 
                SolidBrush v_SolidBrush = new SolidBrush(grid.RowHeadersDefaultCellStyle.ForeColor);
                int v_LineNo = 0;
                v_LineNo = e.RowIndex + 1;

                string v_Line = v_LineNo.ToString();

                e.Graphics.DrawString(v_Line, e.InheritedRowStyle.Font, v_SolidBrush, 
                    e.RowBounds.Location.X + 15, e.RowBounds.Location.Y + 5);

            }
            catch (Exception ex)
            {
                MessageBox.Show("添加行号时发生错误，错误信息：" + ex.Message, "操作失败");
            }
        
        }

        void btnBatchAdd_Click(object sender, EventArgs e)
        {
            if (this.gridStamp.Rows.Count == 0)
            {
                MessageBox.Show("当前没有邮票信息");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                PicNaviDlg dlg = new PicNaviDlg();
                dlg.ImageFiles = ofd.FileNames;
                dlg.ShowDialog();
                int cnt = dlg.ImageFiles.Length > gridStamp.Rows.Count ? 
                    gridStamp.Rows.Count : dlg.ImageFiles.Length;
                DBUtil.Picture pic = new DBUtil.Picture();
                for (int i = 0; i < cnt; ++i )
                {
                    string file = dlg.ImageFiles[i];
                    if ((new FileInfo(file)).Length > 500 * 1024)
                    {
                        MessageBox.Show(string.Format("图片{0}大于500k, 无法导入", file));
                    }
                    else
                    {
                        try
                        {
                            gridStamp.Rows[i].Cells["no"].Selected = true;
                            Image img = Image.FromFile(file);
                            DataRow dr;
                            if (gridStampPic.Rows.Count == 0)
                            {
                                dr = ds.Tables[pic.TABLE_NAME].NewRow();
                                ds.Tables[pic.TABLE_NAME].Rows.Add(dr);
                                dr["id"] = Comm.Util.GenGuid();
                                dr["user_id"] = DBUtil.User.CURRENT_USER_ID;

                                // add set pic info
                                gridStamp.SelectedCells[0].OwningRow.Cells["pic_id"].Value = dr["id"];
                                ((DataView)bs[pic.TABLE_NAME + "stamp"].DataSource).RowFilter =
                                    string.Format("id='{0}'", dr["id"]);
                            }
                            else
                            {
                                dr = ((DataView)bs[pic.TABLE_NAME + "stamp"].DataSource)[0].Row;
                            }

                            dr["image_large"] = File.ReadAllBytes(file);
                            dr["image_small"] = Stamp4Fun.Comm.Image.ImageToBytes(
                                Stamp4Fun.Comm.Image.ShrinkImage(new Bitmap(img), 200, 200, System.Drawing.Color.Black));

                        }
                        catch (System.Exception)
                        {
                            MessageBox.Show(string.Format("{0}不是合法图片格式", file));
                        }
                    }
                }
            }   
        }

        void OnDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid.Rows.Count == 1)
                grid.Rows[0].Height = 200;
            grid.Columns["image_small"].Width = 200;
        }

        void btnDelStampPic_Click(object sender, EventArgs e)
        {
            if (this.gridStampPic.Rows.Count == 0)
            {
                MessageBox.Show("当前没有图片");
                return;
            }

            if (DBUtil.User.CURRENT_USER_ID != this.gridStampPic.Rows[0].Cells["user_id"].Value.ToString())
            {
                MessageBox.Show("该图片非您添加，无法删除");
                return;
            }

            ((DataRowView)this.gridStampPic.Rows[0].DataBoundItem).Row.Delete();
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            ((DataRowView)bs[stamp.TABLE_NAME].Current)["pic_id"] = DBNull.Value;
        }

        void btnDelSetPic_Click(object sender, EventArgs e)
        {
            if (this.gridSetPic.Rows.Count == 0)
            {
                MessageBox.Show("当前没有图片");
                return;
            }

            if (DBUtil.User.CURRENT_USER_ID != this.gridSetPic.Rows[0].Cells["user_id"].Value.ToString())
            {
                MessageBox.Show("该图片非您添加，无法删除");
                return;
            }

            ((DataRowView)this.gridSetPic.Rows[0].DataBoundItem).Row.Delete();
            ((DataRowView)this.gridSet.Rows[0].DataBoundItem).Row["pic_id"] = DBNull.Value;

        }

        void btnAddStampPic_Click(object sender, EventArgs e)
        {
            if (this.gridStamp.SelectedCells.Count == 0)
            {
                MessageBox.Show("当前没有邮票信息");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if ((new FileInfo(ofd.FileName)).Length > 500 * 1024)
                {
                    MessageBox.Show("图片不能大于500k");
                }
                else
                {
                    try
                    {
                        Image img = Image.FromFile(ofd.FileName);
                        DataRow dr;
                        DBUtil.Picture pic = new DBUtil.Picture();
                        if (gridStampPic.Rows.Count == 0)
                        {
                            dr = ds.Tables[pic.TABLE_NAME].NewRow();
                            ds.Tables[pic.TABLE_NAME].Rows.Add(dr);
                            dr["id"] = Comm.Util.GenGuid();
                            dr["user_id"] = DBUtil.User.CURRENT_USER_ID;

                            // add set pic info
                            gridStamp.SelectedCells[0].OwningRow.Cells["pic_id"].Value = dr["id"];
                            ((DataView)bs[pic.TABLE_NAME + "stamp"].DataSource).RowFilter =
                                string.Format("id='{0}'", dr["id"]);
                        }
                        else
                        {
                            dr = ((DataView)bs[pic.TABLE_NAME + "stamp"].DataSource)[0].Row;
                        }

                        dr["image_large"] = File.ReadAllBytes(ofd.FileName);
                        dr["image_small"] = Stamp4Fun.Comm.Image.ImageToBytes(
                            Stamp4Fun.Comm.Image.ShrinkImage(new Bitmap(img), 200, 200, System.Drawing.Color.Black));

                    }
                    catch (System.Exception)
                    {
                        MessageBox.Show("不是合法图片格式");
                    }
                }

            }
            
        }

        void btnAddSetPic_Click(object sender, EventArgs e)
        {
            if(this.gridSet.SelectedCells.Count == 0)
            {
                MessageBox.Show("当前没有邮票信息");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if((new FileInfo(ofd.FileName)).Length > 500*1024)
                {
                    MessageBox.Show("图片不能大于500k");
                }
                else
                {
                    try
                    {
                        Image img = Image.FromFile(ofd.FileName);
                        DataRow dr;
                        DBUtil.Picture pic = new DBUtil.Picture();
                        if (gridSetPic.Rows.Count == 0)
                        {
                            dr = ds.Tables[pic.TABLE_NAME].NewRow();
                            ds.Tables[pic.TABLE_NAME].Rows.Add(dr);
                            dr["id"] = Comm.Util.GenGuid();
                            dr["user_id"] = DBUtil.User.CURRENT_USER_ID;
                            // add set pic info
                            gridSet.SelectedCells[0].OwningRow.Cells["pic_id"].Value = dr["id"];
                            ((DataView)bs[pic.TABLE_NAME + "set"].DataSource).RowFilter =
                                string.Format("id='{0}'", dr["id"]);
                        }
                        else
                        {
                            dr = ((DataView)bs[pic.TABLE_NAME + "set"].DataSource)[0].Row;
                        }
                        
                        dr["image_large"] = File.ReadAllBytes(ofd.FileName);
                        dr["image_small"] = Stamp4Fun.Comm.Image.ImageToBytes(
                            Stamp4Fun.Comm.Image.ShrinkImage(new Bitmap(img), 200, 200, System.Drawing.Color.Black));
                        
                     }
                    catch (System.Exception)
                    {
                        MessageBox.Show("不是合法图片格式");
                    }
                }
                   
            }
            
        }

        void MainFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.ds != null && this.ds.GetChanges() != null)
            {
                DialogResult ret = MessageBox.Show("离开前，请先保存数据", ":)", MessageBoxButtons.YesNo);
                e.Cancel = ret == System.Windows.Forms.DialogResult.Yes;
            }
        }

        void btnDelTopic_Click(object sender, EventArgs e)
        {
            if (this.gridTopic.SelectedCells.Count == 0)
            {
                MessageBox.Show("当前没有选中专题信息");
                return;
            }
            ((DataRowView)this.gridTopic.SelectedCells[0].OwningRow.DataBoundItem).Row.Delete();
        }

        void btnAddTopic_Click(object sender, EventArgs e)
        {
            if(this.gridSet.SelectedCells.Count == 0)
            {
                MessageBox.Show("请先增加邮票信息");
                return;
            }
            SelectTopicFm frm = new SelectTopicFm();
            Collection<int> selTopics = new Collection<int>();
            DBUtil.Topic2Elem t2e = new DBUtil.Topic2Elem();
            foreach (DataRowView drv in bs[t2e.TABLE_NAME])
            {
                selTopics.Add(Convert.ToInt32(drv["topic_id"]));
            }

            frm.SetTopicIds(selTopics);
            frm.ShowDialog();
            if (frm.IsOk)
            {
                Collection<DataRow> delRows = new Collection<DataRow>();
                foreach (DataRowView drv in bs[t2e.TABLE_NAME])
                {
                    delRows.Add(drv.Row);
                }
                foreach (DataRow dr in delRows)
                {
                    dr.Delete();
                }

                DBUtil.Topic topic = new DBUtil.Topic();
            
                foreach (int id in frm.GetSelectTopicIds())
                {
                    DataRow dr = ds.Tables[t2e.TABLE_NAME].NewRow();
                    ds.Tables[t2e.TABLE_NAME].Rows.Add(dr);
                    dr["topic_id"] = id;
                    dr["elem_id"] = this.gridSet.SelectedCells[0].OwningRow.Cells["id"].Value;
                    dr["topic_name"] = topic.GetTopicFullName(id);
                }
            }
        }

        void btnSearch_Click(object sender, EventArgs e)
        {
            if (this.ds != null && this.ds.GetChanges() != null)
            {
                DialogResult ret = MessageBox.Show("查询前，请先保存数据", ":)", MessageBoxButtons.YesNo);
                if (ret == System.Windows.Forms.DialogResult.Yes)
                    return;
            }

            if (this.cboNations.SelectedItem == null ||
                string.IsNullOrEmpty(this.tbStampNo.Text))
            {
                MessageBox.Show("请输入国家名称和邮票编号");
                return;
            }

            try
            {
                DBUtil.Set set = new DBUtil.Set();
                DataTable dtSetId = set.GetSetIdData(string.Format(
                    "nation_id={0} and stamp_no='{1}'", dtNation.Select(string.Format(
                    "name_cn='{0}'", this.cboNations.SelectedItem))[0]["id"], this.tbStampNo.Text));
                if (dtSetId.Rows.Count == 0)
                {
                    MessageBox.Show("没有符合条件的数据");
                }
                else
                {
                    Cursor.Current = Cursors.WaitCursor;
                    LoadData(dtSetId.Rows[0]["set_id"]);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        void LoadData(object setId)
        {
            ds = new DataSet();
            DBUtil.Set set = new DBUtil.Set();
            ds.Tables.Add(set.GetSetById(setId));
            DBUtil.Topic2Elem t2e = new DBUtil.Topic2Elem();
            ds.Tables.Add(t2e.GetElemTopics(setId));
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            ds.Tables.Add(stamp.GetStampBySetId(setId));
            DBUtil.StampEx se = new DBUtil.StampEx();
            ds.Tables.Add(se.GetStampExBySetId(setId));

            DBUtil.Picture pic = new DBUtil.Picture();
            string picIds = string.Empty;
            foreach (DataRow dr in ds.Tables[stamp.TABLE_NAME].Rows)
            {
                if (!(dr["pic_id"] is DBNull))
                    picIds += string.Format("'{0}',", dr["pic_id"]);
            }
            if (ds.Tables[set.TABLE_NAME].Rows.Count > 0 && 
                !(ds.Tables[set.TABLE_NAME].Rows[0]["pic_id"] is DBNull))
            {
                picIds += string.Format("'{0}'", ds.Tables[set.TABLE_NAME].Rows[0]["pic_id"]);
            }
            else
            {
                if (picIds.Length > 0)
                {
                    picIds = picIds.Substring(0, picIds.Length - 1);
                }
            }
            string cond = "1!=1";
            if (picIds.Length > 0)
            {
                cond = string.Format("id in ({0})", picIds);
            }
            
            ds.Tables.Add(pic.GetPicByCondOnClntLoad(cond));

            // add fk
            ds.Relations.Add(ds.Tables[set.TABLE_NAME].Columns["id"], 
                ds.Tables[stamp.TABLE_NAME].Columns["set_id"]);
            ds.Relations.Add(ds.Tables[set.TABLE_NAME].Columns["id"],
                ds.Tables[t2e.TABLE_NAME].Columns["elem_id"]);
            ds.Relations.Add(ds.Tables[stamp.TABLE_NAME].Columns["id"],
                ds.Tables[se.TABLE_NAME].Columns["stamp_id"]);
            
            bs[set.TABLE_NAME].DataSource = ds.Tables[set.TABLE_NAME];
            gridSet.DataSource = bs[set.TABLE_NAME];
            this.InitGridSet();
        }

        void MainFm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                btnSave_Click(null, null);    
            }
            
        }

        void btnTopic_Click(object sender, EventArgs e)
        {
            TopicFm frm = new TopicFm();
            frm.ShowDialog();
        }

        void btnNation_Click(object sender, EventArgs e)
        {
            NationFm frm = new NationFm();
            frm.ShowDialog();
            this.cboNations.Text = string.Empty;
        }

        void MainFm_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("欢迎  {0}", LogonFm.LOGON_USER);
            Dictionary<string, bool> menuTexts = new Dictionary<string, bool>();
            menuTexts.Add("File", true);
            //menuTexts.Add("Nation", true);
            for (int i = 0; i < this.toolStrip1.Items.Count; ++i)
            {
                if (menuTexts.ContainsKey(this.toolStrip1.Items[i].Text))
                {
                    this.toolStrip1.Items[i].Visible = LogonFm.LOGON_USER == "acoder1983";
                }
            }

            DBUtil.Topic2Elem t2e = new DBUtil.Topic2Elem();
            DBUtil.Set set = new DBUtil.Set();
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            DBUtil.StampEx se = new DBUtil.StampEx();
            DBUtil.Picture pic = new DBUtil.Picture();
            string[] tabNames = new string[]{set.TABLE_NAME, t2e.TABLE_NAME, 
                pic.TABLE_NAME+"set", pic.TABLE_NAME+"stamp",
                stamp.TABLE_NAME, se.TABLE_NAME};
            foreach (string tab in tabNames)
            {
                bs.Add(tab, new BindingSource());
            }
        
            grids = new DataGridView[] { 
                this.gridSet, this.gridTopic, this.gridSetPic, this.gridStamp, this.gridStampEx, this.gridStampPic };
            foreach (DataGridView g in grids)
            {
                g.AllowUserToAddRows = false;
                g.ScrollBars = ScrollBars.Both;
                g.SelectionMode = DataGridViewSelectionMode.CellSelect;
                g.CellBeginEdit += new DataGridViewCellCancelEventHandler(OnCellBeginEdit);
            }

            // load schema
            LoadData(-1);
        }

        void OnCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            Dictionary<DataGridView, int> despLenDict = new Dictionary<DataGridView, int>();
            despLenDict.Add(this.gridSet, 1000);
            despLenDict.Add(this.gridStamp, 200);
            despLenDict.Add(this.gridStampEx, 100);
            despLenDict.Add(this.gridSetPic, 60);
            despLenDict.Add(this.gridStampPic, 60);
            
            switch (grid.SelectedCells[0].OwningColumn.Name.ToLower())
            {
                case "desp_en":
                case "desp_cn":
                case "desp":
                    EditTextFm frm = new EditTextFm();
                    frm.Text = "说明";
                    frm.Width = 400;
                    frm.Height = 300;
                    frm.textBox.Text = Convert.ToString(grid.SelectedCells[0].Value);
                    frm.textBox.MaxLength = despLenDict[grid];
                    frm.ShowDialog();
                    if (frm.IsOk)
                    {
                        grid.SelectedCells[0].Value = frm.textBox.Text;
                    }
                    e.Cancel = true;
                    break;
                case "print_desp":
                    DBUtil.PrintType print = new DBUtil.PrintType();
                    EditGridSelectItem(grid, "print_id", print.GetTopObjects());
                    e.Cancel = true;
                    break;
                case "paper_desp":
                    EditGridSelectItem(grid, "paper_id", DBUtil.Misc.GetPaperTypes());
                    e.Cancel = true;
                    break;
                case "month_desp":
                    EditGridSelectItem(grid, "month_id", DBUtil.Misc.GetMonths());
                    e.Cancel = true;
                    break;
                case "topic_name":
                    e.Cancel = true;
                    break;
            }

        }

        DataTable dtNation;
        DataGridView[] grids;

        void EditGridSelectItem(DataGridView grid, string col_id, DataTable dt)
        {
            SelectItemFm frm = new SelectItemFm();
            frm.Text = grid.SelectedCells[0].OwningColumn.HeaderText;
            frm.ckList.SelectionMode = SelectionMode.One;
            foreach (DataRow dr in dt.Rows)
            {
                frm.ckList.Items.Add(dr["desp"],
                    dr["desp"].ToString() == grid.SelectedCells[0].Value.ToString());
            }
            frm.ShowDialog();
            if (frm.IsOk)
            {
                if (frm.ckList.CheckedItems.Count == 1)
                {
                    grid.SelectedCells[0].Value = frm.ckList.GetItemText(frm.ckList.CheckedItems[0]);
                    grid.SelectedCells[0].OwningRow.Cells[col_id].Value =
                        dt.Select(string.Format("desp='{0}'", grid.SelectedCells[0].Value))[0]["id"];
                }
                else
                {
                    grid.SelectedCells[0].Value = null;
                    grid.SelectedCells[0].OwningRow.Cells[col_id].Value = DBNull.Value;
                }
            }

            grid.EndEdit();
        }

        void gridStamp_SelectionChanged(object sender, EventArgs e)
        {
            if (this.gridStamp.SelectedCells.Count == 0) return;
            DBUtil.StampEx se = new DBUtil.StampEx();
            bs[se.TABLE_NAME].DataSource = ds.Tables[se.TABLE_NAME];
            bs[se.TABLE_NAME].Filter = string.Format(
                "stamp_id='{0}'", this.gridStamp.SelectedCells[0].OwningRow.Cells["id"].Value);
            this.gridStampEx.DataSource = bs[se.TABLE_NAME];
            HideColumns(this.gridStampEx, new string[] { 
                "id", "stamp_id","user_id","set_id", "desp_en"});
            SetColumnTexts(this.gridStampEx, new string[] { 
                "no", "desp_en","desp_cn"},
                new string[] { "编号", "英文说明", "中文说明" });

            DBUtil.Picture pic = new DBUtil.Picture();
            DataView dvPic = new DataView();
            dvPic.Table = ds.Tables[pic.TABLE_NAME];
            bs[pic.TABLE_NAME + "stamp"].DataSource = dvPic;
            bs[pic.TABLE_NAME + "stamp"].Filter = string.Format(
                "id='{0}'", this.gridStamp.SelectedCells[0].OwningRow.Cells["pic_id"].Value);
            this.gridStampPic.DataSource = bs[pic.TABLE_NAME + "stamp"];
            HideColumns(this.gridStampPic, new string[] { 
                "id", "image_large","user_id", "desp_en", "no"});
            SetColumnTexts(this.gridStampPic, new string[] { 
                "no","image_small", "desp_en","desp_cn"},
                new string[] { "编号", "图片", "英文说明", "中文说明" });
        }

        void gridSet_SelectionChanged(object sender, EventArgs e)
        {
            if (this.gridSet.SelectedCells.Count == 0) return;
            DBUtil.Stamp stamp = new DBUtil.Stamp();
            bs[stamp.TABLE_NAME].DataSource = ds.Tables[stamp.TABLE_NAME];
            this.gridStamp.DataSource = bs[stamp.TABLE_NAME];
            bs[stamp.TABLE_NAME].Filter = string.Format(
                "set_id='{0}'", this.gridSet.SelectedCells[0].OwningRow.Cells["id"].Value);
            HideColumns(this.gridStamp, new string[] { 
                "id", "pic_id", "set_id","user_id","nation_id" ,"pic_no"});
            SetColumnTexts(this.gridStamp, new string[] { 
                "no", "pic_no", "denom", "color", "cv_new", "cv_old", "desp"},
                new string[] { "斯科特编号", "图片编号", "面值", "颜色", "价格(新)", "价格(旧)", "中文说明" });
            this.gridStamp.Columns["no"].Width = 150;

            DBUtil.Topic2Elem t2e = new DBUtil.Topic2Elem();
            bs[t2e.TABLE_NAME].DataSource = ds.Tables[t2e.TABLE_NAME];
            this.gridTopic.DataSource = bs[t2e.TABLE_NAME];
            bs[t2e.TABLE_NAME].Filter = string.Format(
                "elem_id='{0}'", this.gridSet.SelectedCells[0].OwningRow.Cells["id"].Value);
            HideColumns(this.gridTopic, new string[] { 
                "elem_id", "topic_id"});
            SetColumnTexts(this.gridTopic, new string[] { 
                "topic_name"}, new string[] { "专题分类" });

            DataView dvPic = new DataView();
            DBUtil.Picture pic = new DBUtil.Picture();
            dvPic.Table = ds.Tables[pic.TABLE_NAME];
            bs[pic.TABLE_NAME + "set"].DataSource = dvPic;
            bs[pic.TABLE_NAME + "set"].Filter = string.Format(
                "id='{0}'", this.gridSet.SelectedCells[0].OwningRow.Cells["pic_id"].Value);
            this.gridSetPic.DataSource = bs[pic.TABLE_NAME + "set"];
            HideColumns(this.gridSetPic, new string[] { 
                "id", "image_large", "no","desp_en","user_id"});
            SetColumnTexts(this.gridSetPic, new string[] { 
                "image_small", "desp_cn"},
                new string[] { "图片", "中文说明" });

            if (bs[pic.TABLE_NAME + "set"].Count == 1)
                this.gridSetPic.Rows[0].Height = 200;
            this.gridSetPic.Columns["image_small"].Width = 200;
        }

        OpenFileDialog odf = new OpenFileDialog();
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (odf.ShowDialog() == DialogResult.OK)
            {
                this.FileName = odf.FileName;
                btnLoad_Click(null, null);
            }
        }

        DataSet ds;
        Dictionary<string, BindingSource> bs = new Dictionary<string, BindingSource>();

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (this.FileName == string.Empty)
            {
                return;
            }

            ScottCatalog.Catalogue c = new ScottCatalog.Catalogue(this.FileName);
            Dictionary<string, bool> words = c.HasSpecialWords();
            if (words.Count > 0)
            {
                string spe = string.Empty;
                foreach (string wd in words.Keys)
                {
                    spe += string.Format("< {0} > ", wd);
                }
                DialogResult ret = MessageBox.Show(string.Format("文本存在 {0} ，是否检查格式", spe), "?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (ret == System.Windows.Forms.DialogResult.Yes)
                {
                    System.Diagnostics.Process.Start("notepad.exe", this.FileName);
                    return;
                }
            }
            c.Build(this);
            this.statLabel.Text = "";
            if (string.IsNullOrEmpty(c.Nation.Name))
            {
                MessageBox.Show("没有设置国家");
                return;
            }
            ds = DBUtil.Catalog.ToDataSet(c, DBUtil.User.CURRENT_USER_ID);
            DBUtil.Set set = new DBUtil.Set();
            bs[set.TABLE_NAME].DataSource = ds.Tables[set.TABLE_NAME];
            this.gridSet.DataSource = bs[set.TABLE_NAME];
            this.InitGridSet();
        }

        void InitGridSet()
        {
            HideColumns(this.gridSet, new string[] { 
                "id", "nation_id", "month_id", "print_id", "paper_id", "type_id" ,"pic_id","user_id", "paper_desp", "desp_en"});
            SetColumnTexts(this.gridSet, new string[] { 
                "year", "month_desp", "day", "print_desp", "paper_desp", "perf","desp_en","desp_cn" },
                new string[] { "年", "月", "日", "印刷类型", "纸张类型", "齿孔", "英文说明", "中文说明" });
            this.gridSet.Columns["month_desp"].Width = 50;
            this.gridSet.Columns["day"].Width = 50;
        }

        internal static void HideColumns(DataGridView grid, string[] cols)
        {
            foreach (string col in cols)
            {
                grid.Columns[col].Visible = false;
            }
        }

        internal static void SetColumnTexts(DataGridView grid, string[] cols, string[] texts)
        {
            for (int i = 0; i < cols.Length; ++i)
            {
                grid.Columns[cols[i]].HeaderText = texts[i];
            }
        }

        void LoadImagePaths()
        {
            string[] files = Directory.GetFiles(
                new FileInfo(this.FileName).DirectoryName, "*.jpg");
            int idx = 0;
            DBUtil.Picture pic = new DBUtil.Picture();
            ds.Tables[pic.TABLE_NAME].Columns.Add("file");
            foreach (DataRow dr in ds.Tables[pic.TABLE_NAME].Rows)
            {
                dr["file"] = files[idx++];
            }
        }

        void AdjustData()
        {
            // 
        }

        public void Notify(string msg)
        {
            this.statLabel.Text = msg;
            this.statusStrip1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            foreach (DataGridView grd in grids)
            {
                grd.EndEdit();
            }
            foreach (BindingSource b in bs.Values)
            {
                b.EndEdit();
            }

            Cursor.Current = Cursors.WaitCursor;
            
            try
            {
                for (int i = 0; i < this.gridSet.Rows.Count; ++i)
                {
                    this.gridSet.Rows[i].Cells["year"].Selected = true;
                    if (!ScottCatalog.Catalogue.IsValidStampYear(
                        this.gridSet.Rows[i].Cells["year"].Value.ToString()))
                    {
                        throw new Exception("请输入合法年份");
                    }
                    if (this.gridStamp.Rows.Count == 0)
                    {
                        throw new Exception("整套至少包含一枚邮票信息");
                    }
                    for (int j = 0; j < this.gridStamp.Rows.Count; ++j)
                    {
                        if(string.IsNullOrEmpty(this.gridStamp.Rows[j].Cells["no"].Value.ToString()))
                        {
                            throw new Exception("请输入邮票的斯科特编号");
                        }
                        if(((DataRowView)this.gridStamp.Rows[j].DataBoundItem).Row.RowState == DataRowState.Added &&
                            (new DBUtil.Stamp()).IsStampExist(
                            this.gridStamp.Rows[j].Cells["no"].Value,
                            this.gridSet.Rows[i].Cells["nation_id"].Value))
                        {
                            throw new Exception(string.Format("斯科特编号{0}已存在，请勿添加",
                                this.gridStamp.Rows[j].Cells["no"].Value));
                        }
                    }
                }

                DBUtil.Catalog.SaveToDB(ds);
                MessageBox.Show("保存成功");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnOpen_Click(null, null);
        }

       
    }
}
