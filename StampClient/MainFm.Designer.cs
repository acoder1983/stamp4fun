namespace Stamp4Fun.StampClient
{
    partial class MainFm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNation = new System.Windows.Forms.ToolStripButton();
            this.btnTopic = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.gridSet = new System.Windows.Forms.DataGridView();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.btnAddSet = new System.Windows.Forms.ToolStripButton();
            this.btnDelSet = new System.Windows.Forms.ToolStripButton();
            this.gridTopic = new System.Windows.Forms.DataGridView();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.btnAddTopic = new System.Windows.Forms.ToolStripButton();
            this.btnDelTopic = new System.Windows.Forms.ToolStripButton();
            this.gridSetPic = new System.Windows.Forms.DataGridView();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.btnAddSetPic = new System.Windows.Forms.ToolStripButton();
            this.btnDelSetPic = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cboNations = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tbStampNo = new System.Windows.Forms.ToolStripTextBox();
            this.btnSearch = new System.Windows.Forms.ToolStripButton();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.gridStamp = new System.Windows.Forms.DataGridView();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.btnAddStamp = new System.Windows.Forms.ToolStripButton();
            this.btnDelStamp = new System.Windows.Forms.ToolStripButton();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.gridStampEx = new System.Windows.Forms.DataGridView();
            this.gridStampPic = new System.Windows.Forms.DataGridView();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.btnAddStampPic = new System.Windows.Forms.ToolStripButton();
            this.btnDelStampPic = new System.Windows.Forms.ToolStripButton();
            this.btnBatchAdd = new System.Windows.Forms.ToolStripButton();
            this.btnBatchDel = new System.Windows.Forms.ToolStripButton();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSet)).BeginInit();
            this.toolStrip3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopic)).BeginInit();
            this.toolStrip8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetPic)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStamp)).BeginInit();
            this.toolStrip5.SuspendLayout();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridStampEx)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStampPic)).BeginInit();
            this.toolStrip7.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSave,
            this.toolStripDropDownButton1,
            this.btnNation,
            this.btnTopic});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(597, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.Image")));
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(43, 24);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.loadToolStripMenuItem});
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(47, 24);
            this.toolStripDropDownButton1.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // loadToolStripMenuItem
            // 
            this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            this.loadToolStripMenuItem.Size = new System.Drawing.Size(118, 24);
            this.loadToolStripMenuItem.Text = "Load";
            // 
            // btnNation
            // 
            this.btnNation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnNation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNation.Name = "btnNation";
            this.btnNation.Size = new System.Drawing.Size(73, 24);
            this.btnNation.Text = "国家地区";
            // 
            // btnTopic
            // 
            this.btnTopic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnTopic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnTopic.Name = "btnTopic";
            this.btnTopic.Size = new System.Drawing.Size(73, 24);
            this.btnTopic.Text = "专题分类";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 27);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer1.Panel2.Controls.Add(this.statusStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(597, 527);
            this.splitContainer1.SplitterDistance = 228;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 28);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer5);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gridSetPic);
            this.splitContainer2.Panel2.Controls.Add(this.toolStrip4);
            this.splitContainer2.Size = new System.Drawing.Size(597, 200);
            this.splitContainer2.SplitterDistance = 443;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 0);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.gridSet);
            this.splitContainer5.Panel1.Controls.Add(this.toolStrip3);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.gridTopic);
            this.splitContainer5.Panel2.Controls.Add(this.toolStrip8);
            this.splitContainer5.Size = new System.Drawing.Size(443, 200);
            this.splitContainer5.SplitterDistance = 346;
            this.splitContainer5.TabIndex = 0;
            // 
            // gridSet
            // 
            this.gridSet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSet.Location = new System.Drawing.Point(0, 27);
            this.gridSet.Name = "gridSet";
            this.gridSet.RowTemplate.Height = 27;
            this.gridSet.Size = new System.Drawing.Size(346, 173);
            this.gridSet.TabIndex = 1;
            // 
            // toolStrip3
            // 
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSet,
            this.btnDelSet});
            this.toolStrip3.Location = new System.Drawing.Point(0, 0);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(346, 27);
            this.toolStrip3.TabIndex = 0;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // btnAddSet
            // 
            this.btnAddSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddSet.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSet.Image")));
            this.btnAddSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSet.Name = "btnAddSet";
            this.btnAddSet.Size = new System.Drawing.Size(103, 24);
            this.btnAddSet.Text = "新增整套信息";
            // 
            // btnDelSet
            // 
            this.btnDelSet.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelSet.Image = ((System.Drawing.Image)(resources.GetObject("btnDelSet.Image")));
            this.btnDelSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelSet.Name = "btnDelSet";
            this.btnDelSet.Size = new System.Drawing.Size(103, 24);
            this.btnDelSet.Text = "删除整套信息";
            // 
            // gridTopic
            // 
            this.gridTopic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridTopic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTopic.Location = new System.Drawing.Point(0, 27);
            this.gridTopic.Name = "gridTopic";
            this.gridTopic.RowTemplate.Height = 27;
            this.gridTopic.Size = new System.Drawing.Size(93, 173);
            this.gridTopic.TabIndex = 1;
            // 
            // toolStrip8
            // 
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddTopic,
            this.btnDelTopic});
            this.toolStrip8.Location = new System.Drawing.Point(0, 0);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.Size = new System.Drawing.Size(93, 27);
            this.toolStrip8.TabIndex = 0;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // btnAddTopic
            // 
            this.btnAddTopic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddTopic.Image = ((System.Drawing.Image)(resources.GetObject("btnAddTopic.Image")));
            this.btnAddTopic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddTopic.Name = "btnAddTopic";
            this.btnAddTopic.Size = new System.Drawing.Size(73, 24);
            this.btnAddTopic.Text = "设置专题";
            // 
            // btnDelTopic
            // 
            this.btnDelTopic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelTopic.Image = ((System.Drawing.Image)(resources.GetObject("btnDelTopic.Image")));
            this.btnDelTopic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelTopic.Name = "btnDelTopic";
            this.btnDelTopic.Size = new System.Drawing.Size(73, 24);
            this.btnDelTopic.Text = "删除专题";
            // 
            // gridSetPic
            // 
            this.gridSetPic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSetPic.Location = new System.Drawing.Point(0, 27);
            this.gridSetPic.Name = "gridSetPic";
            this.gridSetPic.RowTemplate.Height = 27;
            this.gridSetPic.Size = new System.Drawing.Size(150, 173);
            this.gridSetPic.TabIndex = 2;
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddSetPic,
            this.btnDelSetPic});
            this.toolStrip4.Location = new System.Drawing.Point(0, 0);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(150, 27);
            this.toolStrip4.TabIndex = 0;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // btnAddSetPic
            // 
            this.btnAddSetPic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddSetPic.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSetPic.Image")));
            this.btnAddSetPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddSetPic.Name = "btnAddSetPic";
            this.btnAddSetPic.Size = new System.Drawing.Size(73, 24);
            this.btnAddSetPic.Text = "设置图片";
            // 
            // btnDelSetPic
            // 
            this.btnDelSetPic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelSetPic.Image = ((System.Drawing.Image)(resources.GetObject("btnDelSetPic.Image")));
            this.btnDelSetPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelSetPic.Name = "btnDelSetPic";
            this.btnDelSetPic.Size = new System.Drawing.Size(73, 24);
            this.btnDelSetPic.Text = "删除图片";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.cboNations,
            this.toolStripLabel2,
            this.tbStampNo,
            this.btnSearch});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(597, 28);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(39, 25);
            this.toolStripLabel1.Text = "国家";
            // 
            // cboNations
            // 
            this.cboNations.Name = "cboNations";
            this.cboNations.Size = new System.Drawing.Size(121, 28);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 25);
            this.toolStripLabel2.Text = "邮票编号";
            // 
            // tbStampNo
            // 
            this.tbStampNo.Name = "tbStampNo";
            this.tbStampNo.Size = new System.Drawing.Size(100, 28);
            // 
            // btnSearch
            // 
            this.btnSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(43, 25);
            this.btnSearch.Text = "查询";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.gridStamp);
            this.splitContainer3.Panel1.Controls.Add(this.toolStrip5);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.splitContainer4);
            this.splitContainer3.Panel2.Controls.Add(this.toolStrip6);
            this.splitContainer3.Size = new System.Drawing.Size(597, 273);
            this.splitContainer3.SplitterDistance = 346;
            this.splitContainer3.TabIndex = 0;
            // 
            // gridStamp
            // 
            this.gridStamp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStamp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStamp.Location = new System.Drawing.Point(0, 27);
            this.gridStamp.Name = "gridStamp";
            this.gridStamp.RowTemplate.Height = 27;
            this.gridStamp.Size = new System.Drawing.Size(346, 246);
            this.gridStamp.TabIndex = 2;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStamp,
            this.btnDelStamp});
            this.toolStrip5.Location = new System.Drawing.Point(0, 0);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(346, 27);
            this.toolStrip5.TabIndex = 0;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // btnAddStamp
            // 
            this.btnAddStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddStamp.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStamp.Image")));
            this.btnAddStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStamp.Name = "btnAddStamp";
            this.btnAddStamp.Size = new System.Drawing.Size(103, 24);
            this.btnAddStamp.Text = "增加单枚邮票";
            // 
            // btnDelStamp
            // 
            this.btnDelStamp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelStamp.Image = ((System.Drawing.Image)(resources.GetObject("btnDelStamp.Image")));
            this.btnDelStamp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelStamp.Name = "btnDelStamp";
            this.btnDelStamp.Size = new System.Drawing.Size(103, 24);
            this.btnDelStamp.Text = "删除单枚邮票";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 25);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.gridStampEx);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.gridStampPic);
            this.splitContainer4.Panel2.Controls.Add(this.toolStrip7);
            this.splitContainer4.Size = new System.Drawing.Size(247, 248);
            this.splitContainer4.SplitterDistance = 49;
            this.splitContainer4.TabIndex = 1;
            // 
            // gridStampEx
            // 
            this.gridStampEx.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStampEx.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStampEx.Location = new System.Drawing.Point(0, 0);
            this.gridStampEx.Name = "gridStampEx";
            this.gridStampEx.RowTemplate.Height = 27;
            this.gridStampEx.Size = new System.Drawing.Size(247, 49);
            this.gridStampEx.TabIndex = 0;
            // 
            // gridStampPic
            // 
            this.gridStampPic.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridStampPic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridStampPic.Location = new System.Drawing.Point(0, 27);
            this.gridStampPic.Name = "gridStampPic";
            this.gridStampPic.RowTemplate.Height = 27;
            this.gridStampPic.Size = new System.Drawing.Size(247, 168);
            this.gridStampPic.TabIndex = 3;
            // 
            // toolStrip7
            // 
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAddStampPic,
            this.btnDelStampPic,
            this.btnBatchAdd,
            this.btnBatchDel});
            this.toolStrip7.Location = new System.Drawing.Point(0, 0);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.Size = new System.Drawing.Size(247, 27);
            this.toolStrip7.TabIndex = 2;
            this.toolStrip7.Text = "toolStrip7";
            // 
            // btnAddStampPic
            // 
            this.btnAddStampPic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnAddStampPic.Image = ((System.Drawing.Image)(resources.GetObject("btnAddStampPic.Image")));
            this.btnAddStampPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddStampPic.Name = "btnAddStampPic";
            this.btnAddStampPic.Size = new System.Drawing.Size(73, 24);
            this.btnAddStampPic.Text = "设置图片";
            // 
            // btnDelStampPic
            // 
            this.btnDelStampPic.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnDelStampPic.Image = ((System.Drawing.Image)(resources.GetObject("btnDelStampPic.Image")));
            this.btnDelStampPic.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDelStampPic.Name = "btnDelStampPic";
            this.btnDelStampPic.Size = new System.Drawing.Size(73, 24);
            this.btnDelStampPic.Text = "删除图片";
            // 
            // btnBatchAdd
            // 
            this.btnBatchAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBatchAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchAdd.Image")));
            this.btnBatchAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatchAdd.Name = "btnBatchAdd";
            this.btnBatchAdd.Size = new System.Drawing.Size(73, 24);
            this.btnBatchAdd.Text = "批量设置";
            this.btnBatchAdd.Visible = false;
            // 
            // btnBatchDel
            // 
            this.btnBatchDel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnBatchDel.Image = ((System.Drawing.Image)(resources.GetObject("btnBatchDel.Image")));
            this.btnBatchDel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBatchDel.Name = "btnBatchDel";
            this.btnBatchDel.Size = new System.Drawing.Size(73, 24);
            this.btnBatchDel.Text = "全部删除";
            this.btnBatchDel.Visible = false;
            // 
            // toolStrip6
            // 
            this.toolStrip6.Location = new System.Drawing.Point(0, 0);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(247, 25);
            this.toolStrip6.TabIndex = 0;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 273);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(597, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statLabel
            // 
            this.statLabel.Name = "statLabel";
            this.statLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // MainFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 554);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainFm";
            this.Text = "MainFm";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel1.PerformLayout();
            this.splitContainer5.Panel2.ResumeLayout(false);
            this.splitContainer5.Panel2.PerformLayout();
            this.splitContainer5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSet)).EndInit();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTopic)).EndInit();
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetPic)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel1.PerformLayout();
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            this.splitContainer3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStamp)).EndInit();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            this.splitContainer4.Panel2.PerformLayout();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridStampEx)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridStampPic)).EndInit();
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton btnTopic;
        private System.Windows.Forms.ToolStripButton btnNation;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cboNations;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox tbStampNo;
        private System.Windows.Forms.ToolStripButton btnSearch;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView gridSetPic;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.DataGridView gridStamp;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statLabel;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.DataGridView gridStampEx;
        private System.Windows.Forms.ToolStrip toolStrip7;
        private System.Windows.Forms.DataGridView gridStampPic;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.DataGridView gridSet;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.DataGridView gridTopic;
        private System.Windows.Forms.ToolStrip toolStrip8;
        private System.Windows.Forms.ToolStripButton btnDelTopic;
        private System.Windows.Forms.ToolStripButton btnAddTopic;
        private System.Windows.Forms.ToolStripButton btnDelSetPic;
        private System.Windows.Forms.ToolStripButton btnAddSetPic;
        private System.Windows.Forms.ToolStripButton btnAddStampPic;
        private System.Windows.Forms.ToolStripButton btnDelStampPic;
        private System.Windows.Forms.ToolStripButton btnBatchAdd;
        private System.Windows.Forms.ToolStripButton btnBatchDel;
        private System.Windows.Forms.ToolStripButton btnAddSet;
        private System.Windows.Forms.ToolStripButton btnDelSet;
        private System.Windows.Forms.ToolStripButton btnAddStamp;
        private System.Windows.Forms.ToolStripButton btnDelStamp;
    }
}