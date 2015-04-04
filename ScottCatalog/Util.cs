using System;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.ScottCatalog
{
    public class Util
    {
        public static string ReadNextWord(string text, int begPos, ref int endPos)
        {
            return FetchNextWord(text, begPos, ref endPos, true);
        }

        public static string PeekNextWord(string text, int begPos)
        {
            int pos = 0;
            return FetchNextWord(text, begPos, ref pos, false);
        }

        static string FetchNextWord(string text, int begPos, ref int endPos, bool fetched)
        {
            string word = string.Empty;
            int beg = begPos;

            while (begPos < text.Length)
            {
                if (text[begPos] == ' ' ||
                    text[begPos] == '\r' ||
                    text[begPos] == '\n')
                {
                    if (begPos > beg)
                    {
                        break;
                    }
                    ++beg;

                }

                ++begPos;

            }

            if (fetched)
            {
                endPos = begPos;
            }

            word = text.Substring(beg, begPos - beg);

            return word.Trim();
        }

    }
}
