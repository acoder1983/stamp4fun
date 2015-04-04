using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class TopicTree : UserControl
    {
        public TopicTree()
        {
            InitializeComponent();

            this.Load += new EventHandler(TopicTree_Load);
            this.btnSearch.Click += new EventHandler(btnSearch_Click);
            this.tbSearch.KeyDown += new KeyEventHandler(tbSearch_KeyDown);
        }

        void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                btnSearch_Click(null, null);
            }
        }

        void TopicTree_Load(object sender, EventArgs e)
        {
            if (this.DesignMode) return;
            DBUtil.Topic topic = new DBUtil.Topic();
            //dtSource = topic.GetTopObjects();
            //LoadChildNodes(dtSource, this.treeView.Nodes);
            dtAll = topic.GetAllObjects();
            dtSource = topic.GetTopObjects(dtAll);
            LoadChildNodes(dtSource, this.treeView.Nodes);
        }

        DataTable dtAll;
        void LoadChildNodes(DataTable dt, TreeNodeCollection nodes)
        {
            DataRow[] rows=dt.Select();
            DBUtil.Topic topic = new DBUtil.Topic();
            foreach (DataRow dr in rows)
            {
                if (dt != dtSource)
                {
                    dtSource.ImportRow(dr);
                }
                TreeNode n = new TreeNode(dr["name"].ToString());
                n.Tag = dtSource.Select(string.Format("id={0}",dr["id"]))[0];
                nodes.Add(n);
                LoadChildNodes(topic.GetChildObjects(dtAll, dr["id"]), n.Nodes);
            }
        }

        public DataTable dtSource;

        void tbSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                btnSearch_Click(null, null);
            }
        }

        int nodeFoundIdx = -1;
        void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.tbSearch.Text))
            {
                return;
            }
            // search in the tree
            TreeNode n = SearchNode(this.tbSearch.Text);

            // if found, expand all the parent
            if (n != null)
            {
                this.treeView.SelectedNode = n;
                while (n.Parent != null)
                {
                    n.Parent.Expand();
                    n = n.Parent;
                }
            }
            else
            {
                MessageBox.Show("已搜索到末尾");
                nodeFoundIdx = -1;
            }
        }

        void CopyTreeNodesToList(ref Collection<TreeNode> list, TreeNodeCollection nodes)
        {
            foreach (TreeNode n in nodes)
            {
                list.Add(n);
                CopyTreeNodesToList(ref list, n.Nodes);
            }
        }


        TreeNode SearchNode(string text)
        {
            Collection<TreeNode> nodeList = new Collection<TreeNode>();
            CopyTreeNodesToList(ref nodeList, this.treeView.Nodes);
            TreeNode n = null;
            for (int i = nodeFoundIdx + 1; i < nodeList.Count; ++i)
            {
                if (nodeList[i].Text.IndexOf(text) != -1)
                {
                    nodeFoundIdx = i;
                    n = nodeList[i];
                    break;
                }
            }
            return n;
        }

    }

    public class ToolStripSpringTextBox : ToolStripTextBox
    {
        public override Size GetPreferredSize(Size constrainingSize)
        {
            // Use the default size if the text box is on the overflow menu
            // or is on a vertical ToolStrip.
            if (IsOnOverflow || Owner.Orientation == Orientation.Vertical)
            {
                return DefaultSize;
            }

            // Declare a variable to store the total available width as 
            // it is calculated, starting with the display width of the 
            // owning ToolStrip.
            Int32 width = Owner.DisplayRectangle.Width;

            // Subtract the width of the overflow button if it is displayed. 
            if (Owner.OverflowButton.Visible)
            {
                width = width - Owner.OverflowButton.Width -
                    Owner.OverflowButton.Margin.Horizontal;
            }

            // Declare a variable to maintain a count of ToolStripSpringTextBox 
            // items currently displayed in the owning ToolStrip. 
            Int32 springBoxCount = 0;

            foreach (ToolStripItem item in Owner.Items)
            {
                // Ignore items on the overflow menu.
                if (item.IsOnOverflow) continue;

                if (item is ToolStripSpringTextBox)
                {
                    // For ToolStripSpringTextBox items, increment the count and 
                    // subtract the margin width from the total available width.
                    springBoxCount++;
                    width -= item.Margin.Horizontal;
                }
                else
                {
                    // For all other items, subtract the full width from the total
                    // available width.
                    width = width - item.Width - item.Margin.Horizontal;
                }
            }

            // If there are multiple ToolStripSpringTextBox items in the owning
            // ToolStrip, divide the total available width between them. 
            if (springBoxCount > 1) width /= springBoxCount;

            // If the available width is less than the default width, use the
            // default width, forcing one or more items onto the overflow menu.
            if (width < DefaultSize.Width) width = DefaultSize.Width;

            // Retrieve the preferred size from the base class, but change the
            // width to the calculated width. 
            Size size = base.GetPreferredSize(constrainingSize);
            size.Width = width;
            return size;
        }
    }
}
