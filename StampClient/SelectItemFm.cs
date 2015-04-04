using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class SelectItemFm : Form
    {
        internal bool IsOk=false;
        public SelectItemFm()
        {
            InitializeComponent();
            this.ckList.CheckOnClick = true;
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.ckList.ItemCheck += new ItemCheckEventHandler(ckList_ItemCheck);    
        }

        void ckList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                foreach (int idx in this.ckList.CheckedIndices)
                {
                    if (idx != e.Index)
                    {
                        this.ckList.SetItemCheckState(idx, CheckState.Unchecked);
                    }
                }
            }
        }

 
        void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void btnOk_Click(object sender, EventArgs e)
        {
            this.IsOk = true;
            this.Close();
        }


    }
}
