using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Stamp4Fun.ExtractScott
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            PageDlg dlg = new PageDlg();
            dlg.ShowDialog();
            Nation = dlg.textBox1.Text;
            if (string.IsNullOrEmpty(Nation) || string.IsNullOrEmpty(dlg.textBox2.Text) || 
                string.IsNullOrEmpty(dlg.textBox3.Text))
            {
                return;
            }
            for (int i = Convert.ToInt16(dlg.textBox2.Text); i <= Convert.ToInt16(dlg.textBox3.Text); ++i)
            {
                ExtractText.MainFm frm = new ExtractText.MainFm();
                frm.tbScottFolder.Text = string.Format("C:\\atest\\{0}\\{1}", Nation, i);
                frm.ExtractFinished += new ExtractText.MainFm.ExtractFinishedHandler(frm_ExtractFinished);
                frm.ShowDialog();
            }
            //Application.Run(frm);
        }
        static string Nation;
        static void frm_ExtractFinished(ExtractText.MainFm.ExtractFinishedArgs e)
        {
            string[] imageFiles = Directory.GetFiles(e.ScottFolderPath, "*.tiff");
            if (imageFiles.Length > 0)
            {
                ExtractPics.ExtractPicFromScottPage.Extract(imageFiles[0], e.Sender);    
            }

            string[] txtFiles = Directory.GetFiles(e.ScottFolderPath, "*.txt");
            if (txtFiles.Length > 0)
            {
                MergeText.MainFm frm = new MergeText.MainFm();
                frm.textBox3.Text = txtFiles[0].Substring(0, txtFiles[0].Length - 5) + "txt";
                frm.MergeText(txtFiles[0], txtFiles[txtFiles.Length - 1], Nation);
            }
        }
    }
}
