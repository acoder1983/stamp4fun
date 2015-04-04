using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;

using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.ExtractText
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
            string fooFile = string.Format("{0}/xxxyyyzzz.pdf", folderpath);
            if (File.Exists(fooFile))
            {
                File.Delete(fooFile);
            }
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
                new PointPair(new Point(372, 159), new Point(471, 700)),
                new PointPair(new Point(474, 159), new Point(573, 700)),
                new PointPair(new Point(576, 159), new Point(675, 700)),
                new PointPair(new Point(678, 159), new Point(777, 700))
            };

            foreach (string pageFile in pageFiles)
            {
                int i = 0;
                foreach (PointPair pp in pps)
                {
                    // copy a new page
                    File.Copy(pageFile, fooFile, true);

                    // open it with adobe
                    System.Diagnostics.Process.Start(fooFile);
                    new KmSim.Delay(5000).Do();

                    // fit the page size
                    new KmSim.MouseMoveTo(434, 105).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(1000).Do();
                    
                    // click the page proc menu
                    new KmSim.KeyPress("%v").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("t").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("p").Do();
                    new KmSim.Delay(2000).Do();
                    
                    // click the cut btn
                    new KmSim.MouseMoveTo(1190, 385).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(500).Do();

                    // select a region
                    new KmSim.MouseMoveTo(pp.TopLeft.X, pp.TopLeft.Y).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftDown().Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseMoveTo(pp.BottomRight.X, pp.BottomRight.Y).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftUp().Do();
                    new KmSim.Delay(1000).Do();
                    
                    // cut it
                    new KmSim.MouseMoveTo(
                        (pp.TopLeft.X + pp.BottomRight.X) / 2,
                        (pp.TopLeft.Y + pp.BottomRight.Y) / 2).Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.MouseLeftDoubleClick().Do();
                    new KmSim.Delay(2000).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(1000).Do();

                    // save it
                    new KmSim.KeyPress("^s").Do();
                    new KmSim.Delay(1000).Do();
                    
                    // save it to txt
                    new KmSim.KeyPress("%f").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("h").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("m").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("c").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress(string.Format(@"{2}\{0}.{1}.txt", pageFile.Substring(
                        pageFile.LastIndexOf(") ")+2), ++i, folderpath)).Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(8000).Do();

                    // close the page
                    new KmSim.KeyPress("%{F4}").Do();
                    new KmSim.Delay(3000).Do();
                }

                // open it with adobe
                System.Diagnostics.Process.Start(pageFile);
                new KmSim.Delay(5000).Do();

                // save it to tiff
                new KmSim.KeyPress("%f").Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress("h").Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress("i").Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress("t").Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress(string.Format(@"{1}\{0}.tiff", pageFile.Substring(
                    pageFile.LastIndexOf(") ") + 2), folderpath)).Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress("{ENTER}").Do();
                new KmSim.Delay(8000).Do();
                // close the page
                new KmSim.KeyPress("%{F4}").Do();
                new KmSim.Delay(3000).Do();
            }
            File.Delete(fooFile);

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
