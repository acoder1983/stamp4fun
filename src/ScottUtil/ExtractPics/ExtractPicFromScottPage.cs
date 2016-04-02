using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Collections.Generic;
using System.Text;

namespace ScottUtil.ExtractPics
{
    public class ExtractPicFromScottPage
    {
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

        static PointPair[] pps = new PointPair[]{
                new PointPair(new Point(100, 130), new Point(555, 2650)),
                new PointPair(new Point(570, 130), new Point(1025, 2650)),
                new PointPair(new Point(1040, 130), new Point(1495, 2650)),
                new PointPair(new Point(1510, 130), new Point(1965, 2650))
            };

        static Point pp = new Point(0, 0);
        static Size LastPicSize = new Size(0, 0);

        struct PicPos
        {
            public Point Pos;
            public Size Size;
            public PicPos(Point p, Size s)
            {
                Pos = p;
                Size = s;
            }
        }

        static Collection<PicPos> PicPosSet = new Collection<PicPos>();
        static bool IsInLastPic(Point p)
        {
            foreach (PicPos pp in PicPosSet)
            {
                if (p.X >= pp.Pos.X && (p.X - pp.Pos.X) < pp.Size.Width &&
                           p.Y >= pp.Pos.Y && (p.Y - pp.Pos.Y) < pp.Size.Height)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Extract(string file, Comm.INofifyProgress notify)
        {
            PicPosSet.Clear();
            Bitmap img = new Bitmap(Image.FromFile(file));
            int picNo = 0;
            System.IO.FileInfo fi = new System.IO.FileInfo(file);
            foreach (PointPair pp in pps)
            {
                for (int i = pp.TopLeft.Y; i < pp.BottomRight.Y; i++)
                {
                    int cntX = 0;
                    for (int j = pp.TopLeft.X; j < pp.BottomRight.X; ++j)
                    {
                        // if black, cnt++
                        Color c = img.GetPixel(j, i);
                        if (c.R < 50 && c.G < 50 && c.B < 50)
                        {
                            ++cntX;

                        }
                        else
                        {
                            if (cntX > 50)
                            {
                                // else if cntX > 50, continue search Y
                                int orgX = j - cntX;
                                int cntY = 0;
                                for (int y = i; y < pp.BottomRight.Y; ++y)
                                {
                                    c = img.GetPixel(orgX, y);
                                    if (c.R < 50 && c.G < 50 && c.B < 50)
                                    {
                                        cntY++;
                                    }
                                    else
                                    {
                                        if (cntY > 50)
                                        {
                                            // the pic width is cntX
                                            // the pic height is cntY
                                            int orgY = y - cntY;
                                            if (!IsInLastPic(new Point(orgX, orgY)))
                                            {
                                                Bitmap bmp = new Bitmap(cntX, cntY);
                                                for (y = 0; y < cntY; ++y)
                                                {
                                                    for (int x = 0; x < cntX; ++x)
                                                    {
                                                        bmp.SetPixel(x, y, img.GetPixel(orgX + x, orgY + y));

                                                    }
                                                }
                                                string picPath = string.Format("{1}/{0}.jpg",
                                                    Util.MakeupStr((++picNo).ToString(), 4, "0", true), fi.DirectoryName);
                                                bmp.Save(picPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                                                if (notify != null)
                                                {
                                                    notify.Notify(picPath);
                                                }
                                                PicPosSet.Add(new PicPos(new Point(orgX, orgY), new Size(cntX, cntY)));
                                            }

                                        }

                                        cntX = 0;
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                cntX = 0;
                            }
                        }


                    }
                }
            }

            // switch the same line pics
            for (int i = 0; i < PicPosSet.Count-1; ++i)
            {
                if (PicPosSet[i].Pos.X > PicPosSet[i + 1].Pos.X + PicPosSet[i+1].Size.Width)
                {
                    if (PicPosSet[i].Pos.Y + PicPosSet[i].Size.Height >= PicPosSet[i + 1].Pos.Y + PicPosSet[i+1].Size.Height)
                    {
                        System.IO.File.Copy(string.Format("{1}/{0}.jpg",
                            Util.MakeupStr((i+1).ToString(), 4, "0", true), fi.DirectoryName), "xxx.jpg", true);
                        System.IO.File.Copy(string.Format("{1}/{0}.jpg",
                            Util.MakeupStr((i+2).ToString(), 4, "0", true), fi.DirectoryName),
                            string.Format("{1}/{0}.jpg",
                            Util.MakeupStr((i+1).ToString(), 4, "0", true), fi.DirectoryName), true);
                        System.IO.File.Copy("xxx.jpg", string.Format("{1}/{0}.jpg",
                                                    Util.MakeupStr((i+2).ToString(), 4, "0", true), fi.DirectoryName), true);
                        System.IO.File.Delete("xxx.jpg");
                        ++i;
                    }
                }
            }
            notify.Notify("Extract Pics Finished");
        }
    }
}
