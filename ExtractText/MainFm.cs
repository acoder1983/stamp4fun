

namespace Stamp4Fun.ExtractText
{
    public partial class MainFm : Form, Comm.INofifyProgress
    {
        

        private void btnExtract_Click(object sender, EventArgs e)
        {
            
            foreach (string pageFile in pageFiles)
            {
                // get page num
                string pageNumStr=pageFile.Substring(pageFile.LastIndexOf(") "));
                pageNumStr= pageNumStr.Substring(2, pageNumStr.IndexOf(".pdf")-2);
                // get vol num
                string volNumStr=pageFile.Substring(pageFile.IndexOf("Volume.")+7,1);
                // make page folder dir
                string pageFolderName=string.Format("v{0}-{1}",volNumStr,pageNumStr);
                string pageFolderPath=folderpath+"/"+pageFolderName;
                Directory.CreateDirectory(pageFolderPath);
                // move pdf into dir
                string newPagePath=pageFolderPath+"/"+pageFolderName+".pdf";
                File.Copy(pageFile, newPagePath, true);

                int i = 0;
                foreach (PointPair pp in pps)
                {
                    // copy a new page
                    string fooFile = string.Format("{0}/xxxyyyzzz.pdf", pageFolderPath);
                    File.Copy(newPagePath, fooFile, true);

                    // open it with adobe
                    System.Diagnostics.Process.Start(fooFile);
                    new KmSim.Delay(3000).Do();

                    // fit the page size
                    new KmSim.MouseMoveTo(620, 100).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(1000).Do();
                    
                    // click the cut btn
                    new KmSim.MouseMoveTo(805, 70).Do();
                    new KmSim.Delay(500).Do();
                    new KmSim.MouseLeftClick().Do();
                    new KmSim.Delay(1000).Do();

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
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(1000).Do();

                    // save it
                    new KmSim.KeyPress("^s").Do();
                    new KmSim.Delay(1000).Do();
                    
                    // save it to txt
                    string[] keys=["%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{ENTER}"];
                    foreach(string k in keys){
                        new KmSim.KeyPress(k).Do();
                        new KmSim.Delay(500).Do();
                    }
                    ++i;
                    new KmSim.KeyPress(string.Format(@"{0}\{1}.txt", pageFolderPath, i)).Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(3000).Do();

                    // save it to jpg
                    string[] keys=["%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{ENTER}"];
                    foreach(string k in keys){
                        new KmSim.KeyPress(k).Do();
                        new KmSim.Delay(500).Do();
                    }
                    new KmSim.KeyPress(string.Format(@"{0}\{1}.jpg", pageFolderPath, i)).Do();
                    new KmSim.Delay(1000).Do();
                    new KmSim.KeyPress("{ENTER}").Do();
                    new KmSim.Delay(3000).Do();

                    // close the page
                    new KmSim.KeyPress("%{F4}").Do();
                    new KmSim.Delay(2000).Do();
                }

                // open it with adobe
                System.Diagnostics.Process.Start(pageFile);
                new KmSim.Delay(3000).Do();

                // save it to tiff
                string[] keys=["%f","t","{DOWN}","{DOWN}","{DOWN}","{DOWN}","{RIGHT}","{DOWN}","{DOWN}","{DOWN}","{ENTER}"];
                foreach(string k in keys){
                    new KmSim.KeyPress(k).Do();
                    new KmSim.Delay(500).Do();
                }
                new KmSim.KeyPress(string.Format(@"{0}\{1}.jpg", pageFolderPath, pageFolderName)).Do();
                new KmSim.Delay(1000).Do();
                new KmSim.KeyPress("{ENTER}").Do();
                new KmSim.Delay(5000).Do();
                // close the page
                new KmSim.KeyPress("%{F4}").Do();
                new KmSim.Delay(2000).Do();
            }
           

        }

      
    }
}
