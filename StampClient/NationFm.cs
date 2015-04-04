using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class NationFm : Form
    {
        public NationFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;

            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.AllowUserToAddRows = false;
            this.Load += new EventHandler(NationFm_Load);
            this.dataGridView1.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(dataGridView1_DataBindingComplete);
            this.FormClosing += new FormClosingEventHandler(NationFm_FormClosing);
        }

        void NationFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.dt != null && this.dt.GetChanges() != null)
            {
                DialogResult ret = MessageBox.Show("离开前，请先保存数据", ":)", MessageBoxButtons.YesNo);
                e.Cancel = ret == System.Windows.Forms.DialogResult.Yes;
            }    
        }

        void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MainFm.HideColumns(this.dataGridView1, new string[] { "id" ,"desp_en","desp_cn","user_id"});
            MainFm.SetColumnTexts(this.dataGridView1,
                new string[] { "name_en", "name_cn" }, new string[] { "英文国名", "中文国名" });
            this.dataGridView1.Columns["name_en"].Width =
                this.dataGridView1.Columns["name_cn"].Width = 200;
        }

        DataTable dt;
        BindingSource bs;
        void NationFm_Load(object sender, EventArgs e)
        {
            try
            {
                DBUtil.Nation nation = new DBUtil.Nation();
                dt = nation.GetNations();
                bs = new BindingSource();
                bs.DataSource = dt;
                this.dataGridView1.DataSource = bs;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnRem_Click(object sender, EventArgs e)
        {
            if (this.dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("请选择一个国家");
                return;
            }

            if (DBUtil.User.CURRENT_USER_ID != 
                this.dataGridView1.SelectedCells[0].OwningRow.Cells["user_id"].Value.ToString())
            {
                MessageBox.Show("该国家非您添加，无法删除");
                return;
            }

            // has sets,prompt
            if ((new DBUtil.Nation()).HasSets(this.dataGridView1.SelectedCells[0].OwningRow.Cells["id"].Value))
            {
                MessageBox.Show("请先删除该国家下的邮票信息");
                return;
            }

            this.dataGridView1.Rows.Remove(this.dataGridView1.SelectedCells[0].OwningRow);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dt.Rows.Add(dr);
            DBUtil.Nation nation = new DBUtil.Nation();
            dr["id"] = nation.GetNextId();
            dr["user_id"] = DBUtil.User.CURRENT_USER_ID;
            this.bs.MoveLast();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.dataGridView1.EndEdit();
            this.bs.EndEdit();
            DataTable dt2 = dt.GetChanges(DataRowState.Added | DataRowState.Modified);
            if (dt2 != null)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    if(string.IsNullOrEmpty(dr["name_en"].ToString()) ||
                        string.IsNullOrEmpty(dr["name_cn"].ToString()))
                    {
                        MessageBox.Show("国家地区名称不能为空");
                        return;
                    }
                }
            }
            try
            {
                DBUtil.Nation nation = new DBUtil.Nation();
                nation.SaveToDB(dt);
                MessageBox.Show("保存成功");
            }
            catch (System.Exception ex)
            {
                string msg = ex.Message;
                if (ex.Message.IndexOf("唯一") != -1)
                {
                    msg = "新增国家地区已存在，请勿重复添加";
                }
                MessageBox.Show(msg);
            }
        }
    }
}
