using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ScottUtil.MergeText
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
        }

        string SelectFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                return ofd.FileName;
            }
            else
            {
                return string.Empty;
            }
        }

        string begFile;
        private void button1_Click(object sender, EventArgs e)
        {
            begFile = SelectFile();
            if (begFile == string.Empty) return;
            this.textBox1.Text = begFile;
            this.textBox3.Text = (new FileInfo(begFile)).DirectoryName;
        }

        string endFile;
        private void button2_Click(object sender, EventArgs e)
        {
            endFile = SelectFile();
            if (endFile == string.Empty) return;
            this.textBox2.Text = endFile;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            begFile = this.textBox1.Text;
            endFile = this.textBox2.Text;
            MergeText(begFile, endFile, "");
            MessageBox.Show("File Merged!");
            
        }

        public void MergeText(string begFile, string endFile, string nation)
        {
            if (begFile == string.Empty || endFile == string.Empty || this.textBox3.Text == string.Empty) return;
            FileInfo fi = new FileInfo(begFile);
            string[] files = Directory.GetFiles(fi.DirectoryName, "*.txt");
            Array.Sort<string>(files);
            int i = -1, j = -1;
            for (int idx = 0; idx < files.Length; ++idx)
            {
                if (files[idx] == begFile)
                {
                    i = idx;
                }
                if (files[idx] == endFile)
                {
                    j = idx;
                }
            }
            if (j > i && i > -1)
            {
                string text = string.Empty;
                for (int idx = i; idx <= j; ++idx)
                {
                    text += File.ReadAllText(files[idx], Encoding.Default).Trim() + "\r\n";
                }

                FileStream fs = new FileStream(this.textBox3.Text, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(string.Format("<NationName> {0} </NationName> {1}",nation,text));
                sw.Close();
                fs.Close();

                for (int idx = i; idx <= j; ++idx)
                {
                    File.Delete(files[idx]);
                }
            }
        }
    }
}
