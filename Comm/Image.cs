using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Stamp4Fun.Comm
{
    public class Image
    {
        public static Bitmap ShrinkImage(Bitmap img, int width, int height, Color bgColor)
        {
            Bitmap ret = null;
            if (img.Width == 0 || img.Height == 0)
            {
                ret = new Bitmap(0, 0);
                return ret;
            }
            // calc the smaller ratio
            double w = (double)width / img.Width;
            double h = (double)height / img.Height;
            double r = w > h ? h : w;
            ret = new Bitmap(width, height);
            Bitmap img2 = null;
            if (w > 1.0 && h > 1.0)
            {
                // pic smaller than rect
                img2 = img;
            }
            else
            {
                // one side is bigger that rect
                img2 = new Bitmap(img.GetThumbnailImage((int)(img.Width * r), (int)(img.Height * r), null, new IntPtr()));
            }
            int x = (width - img2.Width) / 2;
            int y = (height - img2.Height) / 2;
            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    if ((i >= x && i < (img2.Width + x)) &&
                        (j >= y && j < (img2.Height + y)))
                    {
                        ret.SetPixel(i, j, img2.GetPixel(i - x, j - y));
                    }
                    else
                    {
                        ret.SetPixel(i, j, bgColor);
                    }
                }
            }
            return ret;
        }

        public static byte[] ImageToBytes(System.Drawing.Bitmap img)
        {
            string tempName = Guid.NewGuid().ToString() + ".jpg";
            img.Save(tempName, System.Drawing.Imaging.ImageFormat.Jpeg);
            byte[] bytes = File.ReadAllBytes(tempName);
            File.Delete(tempName);
            return bytes;
        }
    }
}
