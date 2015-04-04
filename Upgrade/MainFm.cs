using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Stamp4Fun.Upgrade
{
    public partial class MainFm : Form
    {
        public MainFm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.Shown += new EventHandler(MainFm_Shown);
        }

        void MainFm_Shown(object sender, EventArgs e)
        {
            Thread t = new Thread(new ThreadStart(ThreadProc));
            t.Start();
        }

        void ThreadProc()
        {
            System.Threading.Thread.Sleep(1000);
            for (int i = 1; i < VerFiles.Length; ++i)
            {
                Util.DownloadWebFile(string.Format("{0}/{1}",
                    Program.WEB_VER_PATH, VerFiles[i]));
            }
            this.CloseForm();
        }

        private delegate void CloseCallBack();
        private void CloseForm()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.InvokeRequired)
            {
                CloseCallBack d = new CloseCallBack(this.Close);
                this.Invoke(d, null);
            }
            else
            {
                this.Close();
            }
        }

        public string[] VerFiles;


    }
}
