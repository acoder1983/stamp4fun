using System;
using System.Collections.Generic;

using System.Text;

namespace Stamp4Fun.ExtractPics
{
    class Util
    {
        public static string MakeupStr(string str, int width, string c, bool before)
        {
            string ret = string.Empty;
            if (str != null)
            {
                int len = str.Length;
                ret = str;
                for (int i = 0; i < width - len; ++i)
                {
                    if (before)
                    {
                        ret = c + ret;
                    }
                    else
                    {
                        ret = ret + c;
                    }
                }
            }
            return ret;
        }
    }
}
