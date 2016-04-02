using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScottUtil.ExtractPics
{
    public partial class MainFm : Form, Comm.INofifyProgress
    {
        public MainFm()
        {
            InitializeComponent();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = string.Empty;
            OpenFileDialog odf = new OpenFileDialog();
            if (odf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ExtractPicFromScottPage.Extract(odf.FileName, this);
            }
        }

       
        public void Notify(string file)
        {
            this.textBox2.Text += file + "\r\n";
            this.textBox2.Refresh();
            this.textBox2.SelectionLength = this.textBox2.Text.Length;
            this.textBox2.ScrollToCaret();
        }
    }
}
