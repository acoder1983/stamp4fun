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
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // for(int i=0;i<1000000;++i){
            //     new KmSim.Delay(1000).Do();
            //     Console.WriteLine(DateTime.Now.ToString());
            // }
            Console.WriteLine("extract begin");
            new KmSim.Delay(10000).Do();
            string folderpath = args[0];

            // read all page names
            string[] pageFiles = Directory.GetFiles(folderpath, "*.pdf", SearchOption.TopDirectoryOnly);
            Array.Sort<string>(pageFiles);
            
            // prcess every page
            PointPair[] pps = new PointPair[]{
                new PointPair(new Point(650, 155), new Point(808, 1020)),
                new PointPair(new Point(813, 155), new Point(971, 1020)),
                new PointPair(new Point(977, 155), new Point(1135, 1020)),
                new PointPair(new Point(1139, 155), new Point(1297, 1020))
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
                    System.Diagnostics.Process.Start(@"C:\Program Files\Adobe\Acrobat 8.0\Acrobat\Acrobat.exe", fooFile);
                    new KmSim.Delay(2500).Do();

                    // fit the page size
                    new KmSim.MouseMoveTo(586, 87).Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(550).Do();
                    
                    // click the cut btn
                    new KmSim.MouseMoveTo(729, 56).Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(550).Do();

                    // select a region
                    new KmSim.MouseMoveTo(pp.TopLeft.X, pp.TopLeft.Y).Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.MouseLeftDown().Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.MouseMoveTo(pp.BottomRight.X, pp.BottomRight.Y).Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.MouseLeftUp().Do();
                    new KmSim.Delay(550).Do();
                    
                    // cut it
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(550).Do();

                    // save it
                    new KmSim.KeyPress("^s").Do();
                    new KmSim.Delay(550).Do();

                    ++i;

                    // save part pdf
                    File.Copy(fooFile, string.Format(@"{0}\{1}.pdf", pageFolderPath, i), true);
                    

                    // save it to jpg
                    keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{ENTER}"};
                    foreach(string k in keyList){
                        new KmSim.KeyPress(k).Do();
                        new KmSim.Delay(550).Do();
                    }
                    new KmSim.KeyPress(string.Format(@"{0}\{1}.jpg", pageFolderPath, i)).Do();
                    new KmSim.Delay(550).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(2000).Do();

                    // close the page
                    new KmSim.KeyPress("%{F4}").Do();
                    new KmSim.Delay(5000).Do();

                    File.Delete(fooFile);
                }
                
            }
            Console.WriteLine("extract end");
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
}
