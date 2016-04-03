﻿using System;
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
        static int SHORT_WAIT=550;
        static int MEDIUM_WAIT=1500;
        static int LONG_WAIT=2500;
        static int CUT_Y_TOP=155;
        static int CUT_Y_DOWN=1020;
        static int CUT_X_LEN=158;
        static int[] CUT_X={650,813,977,1139};

        static int ADOBEPRO8_FIT_X=621;
        static int ADOBEPRO8_FIT_Y=103;
        static int ADOBEPRO8_CUT_X=805;
        static int ADOBEPRO8_CUT_Y=70;


        static int FOXIT7_X=975;
        static int FOXIT7_Y=645;
        static int FOXIT7_Y_LEN=40;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Console.WriteLine("extract begin");
            if(args.Length!=1){
                Console.WriteLine("usage: ScottUtil.ExtractScott.exe [pdf path]");
                return;
            }
            string folderpath = args[0];

            // read all page names
            string[] pageFiles = Directory.GetFiles(folderpath, "*.pdf", SearchOption.TopDirectoryOnly);
            

            // sub page ranges
            PointPair[] pps = CalcCutRanges();

            foreach (string pageFile in pageFiles)
            {
                string pageNumStr=GetPageNumStr(pageFile);

                string volNumStr=pageFile.Substring(pageFile.IndexOf("Volume.")+7,1);

                // make page folder dir
                string pageFolderName=string.Format("v{0}-{1}",volNumStr,pageNumStr);
                string pageFolderPath=folderpath+@"\"+pageFolderName;
                Directory.CreateDirectory(pageFolderPath);

                // move page into dir
                string newPagePath=pageFolderPath+@"\"+pageFolderName+".pdf";
                File.Copy(pageFile, newPagePath, true);

                Console.WriteLine(string.Format("process {0}", pageFolderName));

                // process every subpage
                int pageIdx = 1;
                foreach (PointPair pp in pps)
                {
                    // create sub page
                    string subPageFile = string.Format(@"{0}\{1}.pdf", pageFolderPath,pageIdx);
                    File.Copy(newPagePath, subPageFile, true);

                    ProcessInAdobePro8(subPageFile,pageIdx, pageFolderPath, pp);

                    ProcessInAdobeReader11(subPageFile,pageIdx, pageFolderPath);
                    
                    ProcessInFoxit7(subPageFile,pageIdx,pageFolderPath);

                    ++pageIdx;
                }
                
            }
            Console.WriteLine("extract end");
        }

        static PointPair[] CalcCutRanges(){
            PointPair[] pps = new PointPair[4];
            for(int i=0;i<pps.Length;++i){
                pps[i] = new PointPair(new Point(CUT_X[i], CUT_Y_TOP), new Point(CUT_X[i]+CUT_X_LEN, CUT_Y_DOWN));
            }
            return pps;
        }

        static void ProcessInFoxit7(string subPageFile, int pageIdx, string pageFolderPath){
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Foxit Software\Foxit Reader\FoxitReader.exe", subPageFile);
            new KmSim.Delay().Do(LONG_WAIT);

            // trigger save as 
            new KmSim.KeyPress("^+s").Do(MEDIUM_WAIT);

            // choose txt type
            new KmSim.MouseMoveTo(FOXIT7_X, FOXIT7_Y+FOXIT7_Y_LEN).Do(SHORT_WAIT);
            new KmSim.MouseLeftClick().Do(SHORT_WAIT);
            new KmSim.MouseMoveTo(FOXIT7_X, FOXIT7_Y+FOXIT7_Y_LEN*2).Do(SHORT_WAIT);
            new KmSim.MouseLeftClick().Do(SHORT_WAIT);
            new KmSim.MouseMoveTo(FOXIT7_X, FOXIT7_Y).Do(SHORT_WAIT);
            new KmSim.MouseLeftClick().Do(SHORT_WAIT);

            // input txt path
            new KmSim.KeyPress(string.Format(@"{0}\{1}.f.txt", pageFolderPath, pageIdx)).Do(SHORT_WAIT);
            new KmSim.KeyPress("{ENTER}").Do(MEDIUM_WAIT);

            ClosePdfApp();
        }

        static void ProcessInAdobeReader11(string subPageFile, int pageIdx, string pageFolderPath){
            OpenWithAdobeReader11(subPageFile);

            String[] keyList=new string[]{"%f","h","x"};
            foreach(string k in keyList){
                new KmSim.KeyPress(k).Do(SHORT_WAIT);
            }
            new KmSim.KeyPress(string.Format(@"{0}\{1}.a.txt", pageFolderPath, pageIdx)).Do(SHORT_WAIT);
            new KmSim.KeyPress("{ENTER}").Do(MEDIUM_WAIT);

            ClosePdfApp();
        }


        static void ProcessInAdobePro8(string subPageFile, int pageIdx, string pageFolderPath, PointPair pp){
            OpenWithAdobePro8(subPageFile);                    

            CutSubPage(subPageFile, pp);

            SaveSubPageInJpg(pageIdx, pageFolderPath);
            
            SaveSubPageInTiff(pageIdx, pageFolderPath);

            ClosePdfApp();
        }

        static void ClosePdfApp(){
            new KmSim.KeyPress("%{F4}").Do(MEDIUM_WAIT);
        }

        static string GetPageNumStr(string pageFile){
            // get pageg num
            string pageNumStr=pageFile.Substring(pageFile.LastIndexOf(") "));
            pageNumStr= pageNumStr.Substring(2, pageNumStr.IndexOf(".pdf")-2);
            // fill '0' before num
            string zeroPre="";
            for(int i=0;i<4-pageNumStr.Length;++i){
                zeroPre+="0";
            }
            return zeroPre+pageNumStr;
        }

        static void SaveSubPageInJpg(int pageIdx, string pageFolderPath){
            string[] keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{ENTER}"};
            foreach(string k in keyList){
                new KmSim.KeyPress(k).Do(SHORT_WAIT/2);
            }
            new KmSim.KeyPress(string.Format(@"{0}\{1}.jpg", pageFolderPath, pageIdx)).Do(SHORT_WAIT);
            new KmSim.KeyPress("{ENTER}").Do(MEDIUM_WAIT);
        }

        static void SaveSubPageInTiff(int pageIdx, string pageFolderPath){
            string[] keyList=new string[]{"%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{DOWN}","{DOWN}","{ENTER}"};
            foreach(string k in keyList){
                new KmSim.KeyPress(k).Do(SHORT_WAIT/2);
            }
            new KmSim.KeyPress(string.Format(@"{0}\{1}.tiff", pageFolderPath, pageIdx)).Do(SHORT_WAIT);
            new KmSim.KeyPress("{ENTER}").Do(MEDIUM_WAIT);
        }

        static void OpenWithAdobePro8(string pageFile){
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Adobe\Acrobat 8.0\Acrobat\Acrobat.exe", pageFile);
            new KmSim.Delay().Do(LONG_WAIT);
        }

        static void OpenWithAdobeReader11(string pageFile){
            System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Adobe\Reader 11.0\Reader\AcroRd32.exe", pageFile);
            new KmSim.Delay().Do(LONG_WAIT);
        }

        static void CutSubPage(string pageFile, PointPair pp){
            // fit page size
            new KmSim.MouseMoveTo(ADOBEPRO8_FIT_X , ADOBEPRO8_FIT_Y ).Do(SHORT_WAIT);
            new KmSim.MouseLeftClick().Do(SHORT_WAIT);

            // click the cut btn
            new KmSim.MouseMoveTo(ADOBEPRO8_CUT_X, ADOBEPRO8_CUT_Y).Do(SHORT_WAIT);
            new KmSim.MouseLeftClick().Do(SHORT_WAIT);

            // select a region
            new KmSim.MouseMoveTo(pp.TopLeft.X, pp.TopLeft.Y).Do(SHORT_WAIT/2);
            new KmSim.MouseLeftDown().Do(SHORT_WAIT/2);
            new KmSim.MouseMoveTo(pp.BottomRight.X, pp.BottomRight.Y).Do(SHORT_WAIT/2);
            new KmSim.MouseLeftUp().Do(SHORT_WAIT/2);
            
            // cut it
            new KmSim.KeyPress("{ENTER}").Do(SHORT_WAIT);
            new KmSim.KeyPress("{ENTER}").Do(SHORT_WAIT);

            // save it
            new KmSim.KeyPress("^s").Do(MEDIUM_WAIT);
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
