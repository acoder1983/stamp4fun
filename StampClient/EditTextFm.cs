using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class EditTextFm : Form
    {
        public bool IsOk=false;
        public bool NotNull = false;
        public EditTextFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.btnOk.Click += new EventHandler(btnOk_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);

            this.FormClosing += new FormClosingEventHandler(EditTextFm_FormClosing);
        }

        void EditTextFm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.NotNull)
            {
                if (string.IsNullOrEmpty(this.textBox.Text))
                {
                    MessageBox.Show(string.Format("必须输入{0}", this.Text));
                    e.Cancel = true;
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
