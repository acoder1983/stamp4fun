namespace Stamp4Fun.StampClient
{
    partial class TopicFm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TopicFm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnEdit = new System.Windows.Forms.ToolStripDropDownButton();
            this.btnAdd1 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnAdd2 = new System.Windows.Forms.ToolStripMenuItem();
            this.btnEditName = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnMove = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnExp = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.topicTree = new Stamp4Fun.StampClient.TopicTree();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnEdit,
            this.btnMove,
            this.btnSave,
            this.btnExp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(268, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnEdit
            // 
            this.btnEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAdd1,
            this.btnAdd2,
            this.btnEditName,
            this.btnRem});
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(52, 24);
            this.btnEdit.Text = "编辑";
            // 
            // btnAdd1
            // 
            this.btnAdd1.Name = "btnAdd1";
            this.btnAdd1.Size = new System.Drawing.Size(168, 24);
            this.btnAdd1.Text = "增加顶级专题";
            this.btnAdd1.Click += new System.EventHandler(this.btnAdd1_Click);
            // 
            // btnAdd2
            // 
            this.btnAdd2.Name = "btnAdd2";
            this.btnAdd2.Size = new System.Drawing.Size(168, 24);
            this.btnAdd2.Text = "增加下级专题";
            this.btnAdd2.Click += new System.EventHandler(this.btnAdd2_Click);
            // 
            // btnEditName
            // 
            this.btnEditName.Name = "btnEditName";
            this.btnEditName.Size = new System.Drawing.Size(168, 24);
            this.btnEditName.Text = "修改专题名称";
            this.btnEditName.Click += new System.EventHandler(this.btnEditName_Click_1);
            // 
            // btnRem
            // 
            this.btnRem.Name = "btnRem";
            this.btnRem.Size = new System.Drawing.Size(168, 24);
            this.btnRem.Text = "删除专题";
            this.btnRem.Click += new System.EventHandler(this.btnRem_Click);
            // 
            // btnMove
            // 
            this.btnMove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(55, 24);
            this.btnMove.Text = "Move";
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(43, 24);
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnExp
            // 
            this.btnExp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExp.Name = "btnExp";
            this.btnExp.Size = new System.Drawing.Size(61, 24);
            this.btnExp.Text = "Export";
            this.btnExp.Click += new System.EventHandler(this.btnExp_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButton3);
            this.panel1.Controls.Add(this.radioButton2);
            this.panel1.Controls.Add(this.radioButton1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 338);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(268, 38);
            this.panel1.TabIndex = 2;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(154, 7);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(68, 19);
            this.radioButton3.TabIndex = 2;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "Child";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(88, 7);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(60, 19);
            this.radioButton2.TabIndex = 1;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Next";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(13, 7);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(60, 19);
            this.radioButton1.TabIndex = 0;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Prev";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // topicTree
            // 
            this.topicTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topicTree.Location = new System.Drawing.Point(0, 27);
            this.topicTree.Name = "topicTree";
            this.topicTree.Size = new System.Drawing.Size(268, 311);
            this.topicTree.TabIndex = 3;
            // 
            // TopicFm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(268, 376);
            this.Controls.Add(this.topicTree);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TopicFm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "专题分类";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripDropDownButton btnEdit;
        private System.Windows.Forms.ToolStripMenuItem btnAdd1;
        private System.Windows.Forms.ToolStripMenuItem btnAdd2;
        private System.Windows.Forms.ToolStripMenuItem btnRem;
        private System.Windows.Forms.ToolStripButton btnMove;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.ToolStripButton btnExp;
        private TopicTree topicTree;
        private System.Windows.Forms.ToolStripMenuItem btnEditName;
    }
}