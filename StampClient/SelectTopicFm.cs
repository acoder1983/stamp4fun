using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace Stamp4Fun.StampClient
{
    public partial class SelectTopicFm : Form
    {
        public SelectTopicFm()
        {
            InitializeComponent();

            this.topicTree1.treeView.CheckBoxes = true;

            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.Load += new EventHandler(SelectTopicFm_Load);
        }

        void SelectTopicFm_Load(object sender, EventArgs e)
        {
            CheckTopics(this.topicTree1.treeView.Nodes);
        }

        void CheckTopics(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                DataRow dr = n.Tag as DataRow;
                foreach (int id in selectTopicIds)
                {
                    if (Convert.ToInt32(dr["id"]) == id)
                    {
                        n.Checked = true;
                        break;
                    }
                }
                CheckTopics(n.Nodes);
            }
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            FindSelectTopics();
            if (selectTopicIds.Count == 0)
            {
                MessageBox.Show("至少选择一项专题");
                return;
            }
            this.IsOk = true;
            this.Close();
        }

        void FindSelectTopics()
        {
            selectTopicIds = new Collection<int>();
            Collection<TreeNode> chNodes = new Collection<TreeNode>();
            GetCheckedNode(chNodes, this.topicTree1.treeView.Nodes);
            foreach (TreeNode n in chNodes)
            {
                if (!HasCheckedChildren(n.Nodes))
                {
                    selectTopicIds.Add(Convert.ToInt32(((DataRow)n.Tag)["id"]));
                }
            }
        }

        bool HasCheckedChildren(TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked || HasCheckedChildren(n.Nodes))
                    return true;
            }
            return false;
        }

        void GetCheckedNode(Collection<TreeNode> cNodes, TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                if (n.Checked)
                    cNodes.Add(n);
                GetCheckedNode(cNodes, n.Nodes);
            }
        }


        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();    
        }

        internal bool IsOk = false;

        Collection<int> selectTopicIds = new Collection<int>();

        internal void SetTopicIds(Collection<int> ids)
        {
            selectTopicIds = ids;
        }

        internal Collection<int> GetSelectTopicIds()
        {
            return selectTopicIds;
        }
    }
}
