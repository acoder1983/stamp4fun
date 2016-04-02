using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace ScottUtil.ExtractScott
{
    public partial class MainFm : Form, Comm.INofifyProgress
    {
        public MainFm()
        {
            InitializeComponent();
            this.Load += new EventHandler(MainFm_Load);
        }

        void MainFm_Load(object sender, EventArgs e)
        {
            if (this.tbScottFolder.Text.Length > 0)
            {
                btnExtract_Click(null, null);
                Close();
            }
        }

        struct PointPair
        {
            public Point TopLeft;
            public Point BottomRight;

            public PointPair(Point a, Point b)
            {
                this.TopLeft = a;
                this.BottomRight = b;
            }
        }

        public event ExtractFinishedHandler ExtractFinished;
        public delegate void ExtractFinishedHandler(ExtractFinishedArgs e);
        public class ExtractFinishedArgs
        {
            public string ScottFolderPath;
            public Comm.INofifyProgress Sender;
            public ExtractFinishedArgs(Comm.INofifyProgress sender, string path)
            {
                Sender = sender;
                ScottFolderPath = path;
            }
        }

        

        private void btnExtract_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(this.tbScottFolder.Text))
            {
                MessageBox.Show("请输入合法路径");
                return;
            }
            this.tbLog.Text = "";
            this.Hide();
            string folderpath = this.tbScottFolder.Text;
            
            this.tbLog.Text = string.Format("selected path: {0}\r\n", folderpath);
            // read all page names
            string[] pageFiles = Directory.GetFiles(folderpath, "*.pdf", SearchOption.TopDirectoryOnly);
            if (pageFiles.Length == 0)
            {
                this.tbLog.Text += "there are no pdf files";
                return;
            }
            Array.Sort<string>(pageFiles);
            
            // prcess every page
            PointPair[] pps = new PointPair[]{
                new PointPair(new Point(660, 167), new Point(810, 1003)),
                new PointPair(new Point(817, 167), new Point(967, 1003)),
                new PointPair(new Point(974, 167), new Point(1124, 1003)),
                new PointPair(new Point(1131, 167), new Point(1281, 1003))
            };

            string[] keyList;
            foreach (string pageFile in pageFiles)
            {
                // get pageg num
                string pageNumStr=pageFile.Substring(pageFile.LastIndexOf(") "));
                pageNumStr= pageNumStr.Substring(2, pageNumStr.IndexOf(".pdf")-2);
                // get vol num
                string volNumStr=pageFile.Substring(pageFile.IndexOf("Volume.")+7,1);
                // make page folder dir
                string pageFolderName=string.Format("v{0}-{1}",volNumStr,pageNumStr);
                string pageFolderPath=folderpath+@"\"+pageFolderName;
                Directory.CreateDirectory(pageFolderPath);
                // move pdf into dir
                string newPagePath=pageFolderPath+@"\"+pageFolderName+".pdf";
                File.Copy(pageFile, newPagePath, true);

                int i = 0;
                foreach (PointPair pp in pps)
                {
                    // copy a new page
                    string fooFile = string.Format(@"{0}\xxxyyyzzz.pdf", pageFolderPath);
                    File.Copy(newPagePath, fooFile, true);

                    // open it with adobe
                    System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Adobe\Acrobat 8.0\Acrobat\Acrobat.exe", fooFile);
                    new KmSim.Delay(1500).Do();

                    // fit the page size
                    new KmSim.MouseMoveTo(620, 100).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(350).Do();
                    
                    // click the cut btn
                    new KmSim.MouseMoveTo(805, 70).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(350).Do();

                    // select a region
                    new KmSim.MouseMoveTo(pp.TopLeft.X, pp.TopLeft.Y).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.MouseLeftDown().Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.MouseMoveTo(pp.BottomRight.X, pp.BottomRight.Y).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.MouseLeftUp().Do();
                    new KmSim.Delay(350).Do();
                    
                    // cut it
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(350).Do();

                    // save it
                    new KmSim.KeyPress("^s").Do();
                    new KmSim.Delay(350).Do();

                    ++i;

                    // save part pdf
                    File.Copy(fooFile, string.Format(@"{0}\{1}.pdf", pageFolderPath, i), true);
                    
                    // save it to txt
                    // keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{ENTER}"};
                    // foreach(string k in keyList){
                    //     new KmSim.KeyPress(k).Do();
                    //     new KmSim.Delay(350).Do();
                    // }
                    // new KmSim.KeyPress(string.Format(@"{0}\{1}.txt", pageFolderPath, i)).Do();
                    // new KmSim.Delay(350).Do();
                    // new KmSim.KeyPress("{ENTER}").Do();
                    // new KmSim.Delay(2000).Do();

                    // save it to tiff
                    keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{DOWN}","{DOWN}","{ENTER}"};
                    foreach(string k in keyList){
                        new KmSim.KeyPress(k).Do();
                        new KmSim.Delay(350).Do();
                    }
                    new KmSim.KeyPress(string.Format(@"{0}\{1}.tiff", pageFolderPath, i)).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(2000).Do();

                    // save it to jpg
                    keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{ENTER}"};
                    foreach(string k in keyList){
                        new KmSim.KeyPress(k).Do();
                        new KmSim.Delay(350).Do();
                    }
                    new KmSim.KeyPress(string.Format(@"{0}\{1}.jpg", pageFolderPath, i)).Do();
                    new KmSim.Delay(350).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(2000).Do();

                    // close the page
                    new KmSim.KeyPress("%{F4}").Do();
                    new KmSim.Delay(2000).Do();

                    File.Delete(fooFile);
                }
                
            }

            this.Show();

            if (this.ExtractFinished != null)
            {
                this.ExtractFinished(new ExtractFinishedArgs(this, folderpath));
            }

            this.tbLog.Text += "Finshed!";

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        public void Notify(string msg)
        {
            this.tbLog.Text += msg + "\r\n";
            this.tbLog.Refresh();
            this.tbLog.SelectionLength = this.tbLog.Text.Length;
            this.tbLog.ScrollToCaret(); 
        }
    }
}
