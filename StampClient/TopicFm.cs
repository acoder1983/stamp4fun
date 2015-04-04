using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class TopicFm : Form
    {
        public TopicFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.topicTree.treeView.LabelEdit = true;
            this.Load += new EventHandler(TopicFm_Load);
            this.FormClosing += new FormClosingEventHandler(TopicFm_FormClosing);
            this.topicTree.treeView.AfterLabelEdit += new NodeLabelEditEventHandler(treeView1_AfterLabelEdit);


            RadioCheckHandler = new System.EventHandler(this.OnRadioChecked);
            rbs = new RadioButton[] { this.radioButton1, this.radioButton2, this.radioButton3 };
            this.panel1.Visible = false;
        }

        void TopicFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dtSource.GetChanges() != null)
            {
                DialogResult ret = MessageBox.Show("离开前，请先保存数据", ":)", MessageBoxButtons.YesNo);
                e.Cancel = ret == System.Windows.Forms.DialogResult.Yes;
            }
        }

        EventHandler RadioCheckHandler;
        RadioButton[] rbs;
        void AddRadioCheckHandler()
        {
            for (int i = 0; i < rbs.Length; ++i)
            {
                rbs[i].CheckedChanged += RadioCheckHandler;
            }
        }

        void RemRadioCheckHandler()
        {
            for (int i = 0; i < rbs.Length; ++i)
            {
                rbs[i].CheckedChanged -= RadioCheckHandler;
            }
        }

        void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            ((DataRow)e.Node.Tag)["name"] = e.Label;
        }

        public DataTable dtSource;
        void TopicFm_Load(object sender, EventArgs e)
        {
            Dictionary<string, bool> menuTexts = new Dictionary<string, bool>();
            menuTexts.Add("Move", true);
            menuTexts.Add("Export", true);    
            for (int i = 0; i < this.toolStrip1.Items.Count; ++i)
            {
                if (menuTexts.ContainsKey(this.toolStrip1.Items[i].Text))
                {
                    this.toolStrip1.Items[i].Visible = LogonFm.LOGON_USER == "acoder1983";
                }
            }
            dtSource = this.topicTree.dtSource;
        }

        private void btnAdd1_Click(object sender, EventArgs e)
        {
            TreeNodeCollection nodes = this.topicTree.treeView.Nodes;
            EditTextFm frm = ShowEditTextForm(null);
            if (frm.IsOk)
            {
                TreeNode n = AddNode(nodes, null, frm.textBox.Text);
            }
        }

        TreeNode AddNode(TreeNodeCollection nodes, TreeNode pNode, string text)
        {
            // calc xh
            int maxXh;
            if (nodes.Count == 0)
            {
                maxXh = 0;
            }
            else
            {
                DataRow dr = nodes[nodes.Count - 1].Tag as DataRow;
                maxXh = Convert.ToInt32(dr["xh"]);
            }
            // get pid
            object pid = DBNull.Value;
            if (pNode != null)
            {
                DataRow dr = pNode.Tag as DataRow;
                pid = dr["id"];
            }

            TreeNode n = new TreeNode();
            n.Text = text;
            DataRow drNew = dtSource.NewRow();
            dtSource.Rows.Add(drNew);
            drNew["pid"] = pid;
            DBUtil.Topic topic = new DBUtil.Topic();
            int maxIdDB = topic.GetMaxId();
            maxIdDB = maxIdDB == 0 ? 500 : maxIdDB; // start from 500
            drNew["id"] = maxId = maxId > maxIdDB ? maxId + 1 : maxIdDB + 1;
            drNew["xh"] = ++maxXh;
            drNew["name"] = text;
            drNew["user_id"] = DBUtil.User.CURRENT_USER_ID;
            n.Tag = drNew;
            nodes.Add(n);

            return n;
        }

        int maxId = -1;

        private void btnAdd2_Click(object sender, EventArgs e)
        {
            TreeNodeCollection nodes;
            TreeNode pNode = null;
            if (this.topicTree.treeView.SelectedNode == null)
            {
                MessageBox.Show("请先选择一个专题");
                return;
            }
            nodes = this.topicTree.treeView.SelectedNode.Nodes;
            pNode = this.topicTree.treeView.SelectedNode;
            EditTextFm frm=ShowEditTextForm(null);
            if (frm.IsOk)
            {
                TreeNode n = AddNode(nodes, pNode, frm.textBox.Text);
                this.topicTree.treeView.SelectedNode.Expand();
                this.topicTree.treeView.SelectedNode = n;
            }
        }

        private void btnRem_Click(object sender, EventArgs e)
        {
            if (this.topicTree.treeView.SelectedNode == null)
            {
                MessageBox.Show("请先选择一个专题");
                return;
            }
            // delete nodes recursively
            DataRow dr = this.topicTree.treeView.SelectedNode.Tag as DataRow;
            // judge id
            if (dr["user_id"].ToString() != DBUtil.User.CURRENT_USER_ID)
            {
                MessageBox.Show("该专题非您创建，无法删除");
                return;
            }
            dr.Delete();
            DeleteNodeData(this.topicTree.treeView.SelectedNode.Nodes);
            this.topicTree.treeView.SelectedNode.Nodes.Remove(this.topicTree.treeView.SelectedNode);
        }

        void DeleteNodeData(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Nodes.Count == 0)
                {
                    DataRow dr = n.Tag as DataRow;
                    dr.Delete();
                }
                else
                {
                    DeleteNodeData(n.Nodes);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DBUtil.Topic topic = new DBUtil.Topic();
                topic.SaveToDB(dtSource);
                MessageBox.Show("保存成功");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        TreeNode selNode = null;
        private void btnMove_Click(object sender, EventArgs e)
        {
            if (this.topicTree.treeView.SelectedNode == null)
            {
                MessageBox.Show("请选择节点");
                return;
            }
            selNode = this.topicTree.treeView.SelectedNode;
            this.RemRadioCheckHandler();
            foreach (RadioButton r in rbs)
            {
                r.Checked = false;
            }
            this.AddRadioCheckHandler();
            this.panel1.Visible = true;

        }

        private void OnRadioChecked(object sender, EventArgs e)
        {
            if (this.topicTree.treeView.SelectedNode == null ||
                this.topicTree.treeView.SelectedNode == selNode)
            {
                MessageBox.Show("请选择节点");
            }
            else
            {
                // if selnode is sec node's parent, forbid it
                bool isParent = false;
                TreeNode n = this.topicTree.treeView.SelectedNode;
                while (n.Parent != null)
                {
                    if (selNode == n.Parent)
                    {
                        isParent = true;
                        break;
                    }
                    n = n.Parent;
                }
                if (isParent)
                {
                    MessageBox.Show("不能把父节点移动到子节点");
                }
                else
                {
                    RadioButton rb = sender as RadioButton;
                    TreeNodeCollection nodes;
                    if (selNode.Parent == null)
                    {
                        nodes = this.topicTree.treeView.Nodes;
                    }
                    else
                    {
                        nodes = selNode.Parent.Nodes;
                    }
                    nodes.Remove(selNode);
                    if (this.topicTree.treeView.SelectedNode.Parent == null)
                    {
                        nodes = this.topicTree.treeView.Nodes;
                    }
                    else
                    {
                        nodes = this.topicTree.treeView.SelectedNode.Parent.Nodes;
                    }

                    switch (rb.Text)
                    {
                        case "Prev":
                            nodes.Insert(this.topicTree.treeView.SelectedNode.Index, selNode);

                            break;
                        case "Next":
                            nodes.Insert(this.topicTree.treeView.SelectedNode.Index + 1, selNode);
                            break;
                        case "Child":
                            //LoadChildNodes(this.treeView1.SelectedNode);
                            this.topicTree.treeView.SelectedNode.Nodes.Insert(
                                this.topicTree.treeView.SelectedNode.Nodes.Count, selNode);
                            this.topicTree.treeView.SelectedNode.Expand();
                            break;
                        default:
                            System.Diagnostics.Debug.Assert(false);
                            break;
                    }

                    // edit data
                    DataRow dr = selNode.Tag as DataRow;
                    if (selNode.Parent == null)
                    {
                        dr["pid"] = DBNull.Value;
                        nodes = this.topicTree.treeView.Nodes;
                    }
                    else
                    {
                        dr["pid"] = ((DataRow)selNode.Parent.Tag)["id"];
                        nodes = selNode.Parent.Nodes;
                    }

                    for (int i = 0; i < nodes.Count; ++i)
                    {
                        ((DataRow)nodes[i].Tag)["xh"] = i+1;
                    }
                }


            }

            this.panel1.Visible = false;
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            string file = "export.txt";
            FileStream fs = new FileStream(file, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            DBUtil.Topic topic = new DBUtil.Topic();
            DataTable dt = topic.GetTopObjects();
            WriteTopics(sw, dt, 0);
            sw.Close();
            fs.Close();
            System.Diagnostics.Process.Start("notepad.exe", "export.txt");
        }

        void WriteTopics(StreamWriter sw, DataTable dt, int indent)
        {
            string spaces = "";
            for (int i = 0; i < indent; ++i)
            {
                spaces += "|----";
            }
            DBUtil.Topic topic = new DBUtil.Topic();
            foreach (DataRow dr in dt.Rows)
            {
                sw.WriteLine(spaces + dr["name"].ToString());
                WriteTopics(sw, topic.GetChildObjects(dr["id"]), indent + 1);
            }
        }

        private void btnEditName_Click_1(object sender, EventArgs e)
        {
            if (this.topicTree.treeView.SelectedNode == null)
            {
                MessageBox.Show("请先选择一个专题");
                return;
            }

            EditTextFm frm = ShowEditTextForm(this.topicTree.treeView.SelectedNode.Text);
            if (frm.IsOk)
            {
                this.topicTree.treeView.SelectedNode.Text = frm.textBox.Text;
                ((DataRow)this.topicTree.treeView.SelectedNode.Tag)["name"] = frm.textBox.Text;
            }
        }

        EditTextFm ShowEditTextForm(string text)
        {
            EditTextFm frm = new EditTextFm();
            frm.Text = "专题名称";
            frm.textBox.Multiline = false;
            frm.textBox.Text = text;
            frm.NotNull = true;
            frm.StartPosition = FormStartPosition.CenterParent;
            frm.ShowDialog();
            return frm;
        }

    }

}
