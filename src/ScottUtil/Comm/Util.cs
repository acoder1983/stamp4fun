using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ScottUtil.Comm
{
    public interface INofifyProgress
    {
        void Notify(string msg);
    }

    public class Util
    {
        public static string GenGuid()
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper();
        }


        public static bool IsNumber(string wd)
        {
            try
            {
                Convert.ToDouble(wd);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

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

        public static string EncodeUrl(string url)
        {
            return url.Replace("(", "$1").Replace(")", "$2").Replace("'", "$3");
        }

        public static string DecodeUrl(string url)
        {
            return url.Replace("$1", "(").Replace("$2", ")").Replace("$3", "'") ;
        }
    }
}
