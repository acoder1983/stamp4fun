using System;
using System.Data;
using System.IO;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Stamp4Fun.ScottCatalog
{
    public class Catalogue
    {
        public Nation Nation = new Nation();
        public Collection<Set> Sets = new Collection<Set>();
        public Dictionary<string, Picture> PicDict = new Dictionary<string, Picture>();
        Dictionary<string, bool> PicRefDict = new Dictionary<string, bool>();

        const string LINE_RET = "\r\n";

        const string NATION_NAME_BEG = "<NationName>";
        const string NATION_NAME_END = "</NationName>";
        const string NATION_DESP_BEG = "<NationDesp>";
        const string NATION_DESP_END = "</NationDesp>";


        string Text = string.Empty;
        public string File;
        enum CataElement
        {
            Nation, Set, Stamp, Picture, Desp, TextEnd
        }

        public Catalogue(string file)
        {
            this.File = file;
            this.Text = System.IO.File.ReadAllText(file, Encoding.Default);
            // replace all the '—' and '.—' and '.”'
            this.Text = this.Text.Replace(".—", " ");
            this.Text = this.Text.Replace("—", " ");
            this.Text = this.Text.Replace(".”", ".”. ");
            this.Text = this.Text.Replace("Syncopated", "Syncopated. ");
            this.Text = this.Text.Replace("Anniv.", "Anniv");
            this.Text = this.Text.Replace("Cent.", "Cent");
        }

        int Position = 0;

        string ReadNextWord()
        {
            return Util.ReadNextWord(this.Text, this.Position, ref this.Position);
        }

        string PeekNextWord()
        {
            return Util.PeekNextWord(this.Text, this.Position);
        }

        public Dictionary<string, bool> HasSpecialWords()
        {
            this.Position = 0;
            Dictionary<string, bool> words = new Dictionary<string, bool>();
            string wd = this.ReadNextWord();
            while (wd != string.Empty)
            {
                if (wd == "Overprinted" || wd == "a." || wd == "Inscribed:" ||
                    wd == "Inscribed" || wd == "Surcharged" || wd == "Sheet")
                {
                    if (!words.ContainsKey(wd))
                        words.Add(wd, true);
                }
                wd = this.ReadNextWord();
            }
            return words;
        }

        CataElement PeekNextTwoWords(ref string wd1, ref string wd2)
        {
            int pos = this.Position;
            wd1 = this.ReadNextWord();
            wd2 = this.ReadNextWord();
            this.Position = pos;
            if (wd1 == string.Empty)
                return CataElement.TextEnd;
            string text = string.Empty;
            string num = string.Empty;
            if (wd1[0] == '<')
            {
                if (wd1 == NATION_NAME_BEG ||
                wd1 == NATION_DESP_BEG)
                {
                    return CataElement.Nation;
                }
                else
                {
                    return CataElement.Desp;
                }
            }
            else if (this.StartWithNum(wd1, ref num, ref text))
            {
                string text2 = string.Empty;
                string num2 = string.Empty;
                if (this.StartWithText(wd2, ref text2, ref num2) &&
                    this.IsPicType(text2))
                {
                    return CataElement.Stamp;
                }
                else
                {
                    if (IsValidStampYear(num) &&
                        (text == string.Empty || text[0] == ',' || text[0] == '-'))
                    {
                        if (this.IsWordInSet(wd2.Replace(".", string.Empty)))
                        {
                            return CataElement.Set;
                        }
                        else
                        {
                            this.ReadNextWord();// skip the year
                            string wd11 = string.Empty;
                            string wd22 = string.Empty;
                            if (this.PeekNextTwoWords(ref wd11, ref wd22) == CataElement.Stamp)
                            {
                                this.Position -= wd1.Length;
                                return CataElement.Set;
                            }
                            else
                            {
                                this.Position -= wd1.Length;
                                return CataElement.Desp;
                            }
                        }
                    }
                    else
                    {
                        return CataElement.Desp;
                    }
                }
            }
            else if (this.StartWithText(wd1, ref text, ref num))
            {
                if (num == string.Empty)
                {
                    return CataElement.Desp;
                }
                else
                {
                    if (this.IsSpecialStampType(text))
                    {
                        string text2 = string.Empty;
                        string num2 = string.Empty;
                        if (this.StartWithText(wd2, ref text2, ref num2) &&
                            (this.IsPicType(text2) && (text != text2 ||
                            (text[0] == 'O' && text2[0] == 'O') ||
                            (text[0] == 'M' && text2[0] == 'M'))))
                        {
                            return CataElement.Stamp;
                        }
                        else
                        {
                            return CataElement.Desp;
                        }
                    }
                    else
                    {
                        return CataElement.Desp;
                    }
                }
            }
            else
            {
                return CataElement.Desp;
            }

        }

        CataElement LastReadElem = CataElement.TextEnd;

        public void Build(Comm.INofifyProgress notify)
        {
            this.Position = 0;
            string word1 = string.Empty;
            string word2 = string.Empty;
            CataElement ce = CataElement.TextEnd;
            while ((ce = this.PeekNextTwoWords(ref word1, ref word2)) != CataElement.TextEnd)
            {
                string text = string.Empty;
                string num = string.Empty;

                switch (ce)
                {
                    case CataElement.Nation:
                        this.ReadNationInfo(word1);
                        break;
                    case CataElement.Set:
                        this.ReadSet(word1);
                        break;
                    case CataElement.Stamp:
                        this.ReadStamp(word1, word2);
                        break;
                    case CataElement.Desp:
                        this.ReadInfo(word1);
                        break;
                    default:
                        Debug.Assert(false);
                        break;
                }

                if (notify != null)
                {
                    notify.Notify(string.Format("processing {0}......", ce.ToString()));
                }
            }
        }

        void AddLineToDesp(string word)
        {
            Set set = null;
            if (this.Sets.Count == 0)
            {
                set = new Set();
                this.Sets.Add(set);
            }
            set = this.Sets[this.Sets.Count - 1];
            set.Desp += word + " " + this.ReadToLineEnd();
        }

        bool IsWordInSet(string word)
        {
            return (new Months()).IsIn(word) || (new PaperTypes()).IsIn(word) ||
                (new PrintTypes()).IsIn(word) || (new PerfTypes()).IsIn(word);
        }

        string ReadToLineEnd()
        {
            int beg = this.Position;
            int end = this.Text.IndexOf(LINE_RET, this.Position);
            if (end == -1)
            {
                end = this.Text.Length;
            }
            this.Position = end;
            return this.Text.Substring(beg, end - beg).Trim();
        }

        bool StartWithText(string word, ref string text, ref string num)
        {
            if (word.Length == 0 || char.IsDigit(word[0])) return false;
            text = num = string.Empty;
            for (int i = 1; i < word.Length; ++i)
            {
                if (char.IsDigit(word[i]))
                {
                    text = word.Substring(0, i);
                    num = word.Substring(i, word.Length - i);
                    break;
                }
            }
            if (num == string.Empty)
            {
                text = word;
            }
            return true;
        }

        bool StartWithNum(string word, ref string num, ref string text)
        {
            if (!char.IsDigit(word[0])) return false;
            text = num = string.Empty;
            for (int i = 1; i < word.Length; ++i)
            {
                if (!char.IsDigit(word[i]))
                {
                    num = word.Substring(0, i);
                    text = word.Substring(i, word.Length - i);
                    break;
                }
            }
            if (text == string.Empty)
            {
                num = word;
            }
            return true;
        }

        bool IsSpecialStampType(string word)
        {
            return (new SpecialStampTypes()).IsIn(word);
        }

        bool IsPicType(string word)
        {
            return (new PicTypes()).IsIn(word);
        }

        void ReadStamp(string stampLabel, string picLabel)
        {
            Set set = this.Sets[this.Sets.Count - 1];
            Stamp stamp = new Stamp(set);
            set.Stamps.Add(stamp);
            stamp.No = stampLabel;

            // the first stamp has pic obj
            stamp.PicNo = picLabel;
            if (!this.PicRefDict.ContainsKey(picLabel))
            {
                if (this.PicDict.ContainsKey(picLabel))
                {
                    stamp.Pic = this.PicDict[picLabel];
                }

                this.PicRefDict.Add(picLabel, true);
            }

            this.ReadNextWord(); // skip the stamplabel
            this.ReadNextWord(); // skip the picLabel
            stamp.Denom = this.ReadNextWord();
            string nextVal = this.PeekNextWord();
            if (nextVal == "on" ||
                nextVal == "+")
            {
                stamp.Denom += string.Format(" {0} ", nextVal);
                nextVal = this.ReadNextWord(); // skip the conn word
                nextVal = this.ReadNextWord();
                stamp.Denom += nextVal;
            }
            else if (nextVal == "Sheet")
            {
                int beg = this.Position - nextVal.Length;
                nextVal = this.ReadNextWord();
                if (nextVal == "of")
                {
                    nextVal = this.ReadNextWord();
                    stamp.Denom = this.Text.Substring(beg, this.Position - beg);
                }
            }

            nextVal = string.Empty;
            do
            {
                stamp.Color += nextVal + " ";
                nextVal = this.ReadNextWord();
            } while (!IsDecimal(nextVal));
            stamp.Color = stamp.Color.Trim();

            if (nextVal != "n/a")
                stamp.CvNew = ConvertToCv(nextVal);

            nextVal = this.ReadNextWord();
            if (nextVal != "n/a")
                stamp.CvUsed = ConvertToCv(nextVal);

            // read expand
            string ex = this.PeekNextWord();
            while (ex.Length >= 2 && ex[1] == '.')
            {
                StampExpand se = new StampExpand(stamp);
                stamp.StampExpands.Add(se);
                se.No = this.ReadNextWord();
                // read until cvnew and cvold
                int pos = this.Position;

                while (true)
                {
                    string wd = this.ReadNextWord();
                    if (wd.Replace(".", string.Empty) == "Perf")
                    {
                        this.ReadPerfValue();
                    }
                    else
                    {
                        if (Comm.Util.IsNumber(wd))
                        {
                            wd = this.ReadNextWord();
                            if (Comm.Util.IsNumber(wd))
                            {
                                break;
                            }
                        }
                    }
                }
                se.Desp = this.Text.Substring(pos, this.Position - pos);
                ex = this.PeekNextWord();
            }

            this.LastReadElem = CataElement.Stamp;
        }

        bool IsDecimal(string word)
        {
            try
            {
                Convert.ToDecimal(word);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        string ConvertToCv(string word)
        {
            if (word[0] == '.')
            {
                return Convert.ToDouble("0" + word).ToString();
            }
            else
            {
                return Convert.ToDouble(word.Replace(",", "")).ToString();
            }
        }

        void ReadSet(string word)
        {
            Set set = null;
            if (this.Sets.Count == 0 || this.Sets[this.Sets.Count - 1].Year != null)
            {
                set = new Set();
                this.Sets.Add(set);
            }
            set = this.Sets[this.Sets.Count - 1];

            word = this.ReadNextWord();
            set.Year = word.Replace(",", "");

            while ((word = this.ReadNextWord()) != string.Empty)
            {
                word = word.Replace(".", string.Empty);
                if ((new Months()).IsIn(word))
                {
                    set.Month = word;
                }
                else if (IsDay(word))
                {
                    if (set.Month != string.Empty && set.Day == string.Empty)
                    {
                        set.Day = word;
                    }
                    else
                    {
                        this.Position -= word.Length;
                        break;
                    }
                }
                else if ((new PrintTypes()).IsIn(word))
                {
                    set.Print = word;
                    word = this.PeekNextWord();
                    if (word == "and" || word == "&")
                    {
                        this.ReadNextWord();
                        set.Print += " " + this.ReadNextWord().Replace(".", string.Empty);
                    }
                }
                else if ((new PaperTypes()).IsIn(word))
                {
                    set.Paper = word;
                }
                else if ((new PerfTypes()).IsIn(word))
                {
                    if (word == "Perf")
                    {
                        set.Perf = this.ReadPerfValue();
                    }
                    else
                    {
                        set.Perf = word;
                    }
                }
                else if (word.IndexOf("/") != -1 || word.IndexOf("x") != -1)
                {
                    set.Perf += word;
                }
                else
                {
                    this.Position -= word.Length;
                    break;
                }
            }

            if (set.Perf != string.Empty)
            {
                int pos = set.Perf.IndexOf("/");
                if (pos != -1)
                {
                    set.Perf = set.Perf.Insert(pos - 1, "^");
                }
            }

            if (this.Sets.Count > 1 && set.Print == string.Empty)
            {
                set.Print = this.Sets[this.Sets.Count - 2].Print;
            }

            if (this.DespWaitToAdd != string.Empty)
            {
                set.Desp += this.DespWaitToAdd;
                this.DespWaitToAdd = string.Empty;
            }

            if (this.PrintWaitToAdd != string.Empty)
            {
                set.Print = this.PrintWaitToAdd;
                this.PrintWaitToAdd = string.Empty;
            }

            this.LastReadElem = CataElement.Set;
        }

        private string ReadPerfValue()
        {
            string perf = ReadNextWord();
            char lastCh = perf[perf.Length - 1];
            if (lastCh == '/')
            {
                // Perf. 131/ 2
                perf += ReadNextWord();
            }
            else if (lastCh == ';')
            {
                // Perf. 111/2; 14 (2m)
                perf += ReadNextWord();
                perf += ReadNextWord();
            }
            else if (lastCh == ',')
            {
                // Perf. 13x131/2, 131/2x13
                perf += ReadNextWord();
            }
            string nextWd = PeekNextWord();
            if (nextWd.IndexOf("/") != -1 || nextWd.IndexOf("x") != -1)
            {
                perf += nextWd;
                ReadNextWord();
            }
            return perf;
        }

        void ReadInfo(string word)
        {
            int beg = this.Position;
            this.ReadNextWord();
            bool bProc = false;
            while (word != string.Empty)
            {
                if (this.IsPic(word))
                {
                    Picture pic = new Picture();
                    pic.No = word;
                    pic.Desp = Text.Substring(beg,
                        this.Position - beg - word.Length).Replace(LINE_RET, "").Trim();

                    this.LastAddPic = pic;
                    this.PicDict.Add(word, pic);

                    this.LastReadElem = CataElement.Picture;

                    bProc = true;
                }
                else if (word == "Miniature")
                {
                    word = this.PeekNextWord();
                    if (word == "Sheet")
                    {
                        this.ReadNextWord();
                        this.ProcDespEnd(beg);
                        this.LastReadElem = CataElement.Desp;
                        bProc = true;
                        break;
                    }
                }
                else if (word == "Booklet")
                {
                    word = this.PeekNextWord();
                    if (word == "Stamps")
                    {
                        this.ReadNextWord();
                        this.ProcDespEnd(beg);
                        this.LastReadElem = CataElement.Desp;
                        bProc = true;
                        break;
                    }
                }
                else if (word == "Buff" || word == "Granite" || word == "Cream")
                {
                    word = this.PeekNextWord();
                    if (word == "Paper")
                    {
                        this.ReadNextWord();
                        this.ProcDespEnd(beg);
                        this.LastReadElem = CataElement.Desp;
                        bProc = true;
                        break;
                    }
                }
                else if (word == "Type" || word == "Types")
                {
                    string wd = word;
                    word = this.PeekNextWord();
                    if (word == "of")
                    {
                        this.ReadNextWord();
                        word = this.ReadNextWord();
                        if (word.Length >= 4 && IsValidStampYear(word.Substring(0, 4)))
                        {
                            this.DespWaitToAdd += " " + this.Text.Substring(beg, this.Position - beg) + ". ";
                            this.LastReadElem = CataElement.Desp;
                            bProc = true;
                            break;
                        }
                    }
                    else if (word == "Similar")
                    {
                        this.ReadNextWord();
                        // skip 'to' and 'pic_no'
                        this.ReadNextWord();
                        this.ReadNextWord();
                        this.LastReadElem = CataElement.Desp;
                        bProc = true;
                        break;
                    }
                }
                else if (word == "Common")
                {
                    word = this.PeekNextWord();
                    if (word == "Design")
                    {
                        word = this.ReadNextWord();
                        if (word == "Type")
                        {
                            this.DespWaitToAdd += " " + this.Text.Substring(beg, this.Position - beg) + ".";
                            bProc = true;
                            break;
                        }
                    }
                }
                else if (word == "Perf.")
                {
                    this.ReadPerfValue();
                    this.ProcDespEnd(beg);
                    bProc = true;
                    break;
                }
                else if (word == "Size:")
                {
                    word = this.ReadNextWord();
                    while (!word.EndsWith("mm") && !word.EndsWith("mm."))
                    {
                        word = this.ReadNextWord();
                    }
                    ProcDespEnd(beg);
                    bProc = true;
                }
                else if (word == "Nos.")
                {
                    int pos = this.Position;
                    string wd = word;
                    word = this.ReadNextWord();
                    if (word.IndexOf("-") != -1)
                    {
                        word = this.ReadNextWord();
                        if (word.StartsWith("("))
                        {
                            word = this.ReadNextWord();
                            if (Comm.Util.IsNumber(word))
                            {
                                word = this.ReadNextWord();
                                if (Comm.Util.IsNumber(word))
                                {
                                    bProc = true;
                                    this.LastReadElem = CataElement.Desp;
                                    break;
                                }
                            }
                        }
                    }
                    this.Position = pos;
                    this.ProcDespEnd(beg);
                    this.LastReadElem = CataElement.Desp;
                    bProc = true;
                    break;
                }
                else if (new PrintTypes().IsIn(word.Replace(".", string.Empty)))
                {
                    string wd = word;
                    word = this.PeekNextWord();
                    if (word == "and" || word == "&" || word == "or")
                    {
                        this.ReadNextWord();
                        word = this.ReadNextWord();
                        string print = Text.Substring(beg, this.Position - beg).Replace(LINE_RET, "").Trim();
                        if (this.LastReadElem == CataElement.Set ||
                            this.LastReadElem == CataElement.Picture ||
                            this.LastReadElem == CataElement.Desp ||
                            this.LastReadElem == CataElement.TextEnd)
                        {
                            this.PrintWaitToAdd = print;
                        }
                        else
                        {
                            ProcessTextInStamps(print);
                        }
                    }
                    else
                    {
                        ProcessTextInStamps(this.Text.Substring(beg, this.Position - beg));
                    }

                    this.LastReadElem = CataElement.Desp;
                    bProc = true;
                }
                else if (IsDespEnd(word) && !(this.IsWordAbbre(word)))
                {
                    ProcDespEnd(beg);
                    bProc = true;
                }
                if (bProc)
                {
                    break;
                }

                word = this.ReadNextWord();
            }

            if (!bProc)
            {
                Debug.Assert(false);
            }
        }

        private void ProcessTextInStamps(string text)
        {
            string wd1 = string.Empty;
            string wd2 = string.Empty;

            Set curSet = this.Sets[this.Sets.Count - 1];

            if (this.PeekNextTwoWords(ref wd1, ref wd2) == CataElement.Stamp)
            {
                curSet.Desp += string.Format(" After No.{0} {1}.",
                    curSet.Stamps[curSet.Stamps.Count - 1].No, text);
            }
            else
            {
                curSet.Desp += " " + text;
            }
        }

        private void ProcDespEnd(int beg)
        {
            string desp = Text.Substring(beg, this.Position - beg).Replace(LINE_RET, "").Trim();
            if (this.Sets.Count == 0 ||
                this.LastReadElem == CataElement.Picture ||
                this.DespWaitToAdd != string.Empty)
            {
                this.DespWaitToAdd += " " + desp;
            }
            else if (this.LastReadElem == CataElement.Stamp)
            {
                ProcessTextInStamps(desp);
            }
            else
            {
                Set curSet = this.Sets[this.Sets.Count - 1];
                curSet.Desp += " " + desp;
            }

            this.LastReadElem = CataElement.Desp;
        }

        string DespWaitToAdd = string.Empty;
        string PrintWaitToAdd = string.Empty;
        Picture LastAddPic = null;

        bool IsWordAbbre(string word)
        {
            return (new AbbreWords()).IsIn(word);
        }

        bool IsPicInSet(string picLabel, Set set)
        {
            foreach (Stamp s in set.Stamps)
            {
                if (s.PicNo == picLabel)
                {
                    return true;
                }
            }
            return false;
        }

        bool IsPic(string word)
        {
            string picType = string.Empty;
            string picNo = string.Empty;
            return this.StartWithText(word, ref picType, ref picNo) &&
                this.IsPicType(picType) && picNo.Length > 0 && char.IsDigit(picNo[0]);
        }

        bool IsDespEnd(string word)
        {
            return word[word.Length - 1] == '.';
        }

        public static bool IsValidStampYear(string word)
        {
            try
            {
                int yy = Convert.ToInt32(word);
                return yy > 1800 && yy < 2100;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        bool IsDay(string word)
        {
            try
            {
                int dd = Convert.ToInt32(word);
                return dd > 0 && dd < 32;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        void ReadNationInfo(string nationBeg)
        {
            string nationEnd = string.Empty;
            if (nationBeg == NATION_NAME_BEG)
            {
                nationEnd = NATION_NAME_END;
            }
            else
            {
                nationEnd = NATION_DESP_END;
            }

            this.ReadNextWord(); //skip the nationbeg
            int beg = this.Position;
            string word = string.Empty;
            while ((word = this.ReadNextWord()) != string.Empty)
            {
                if (word == nationEnd)
                {
                    int cnt = this.Position - beg - nationEnd.Length;
                    if (nationBeg == NATION_NAME_BEG)
                    {
                        this.Nation.Name = this.Text.Substring(beg, cnt).Trim();
                    }
                    else
                    {
                        this.Nation.Desp = this.Text.Substring(beg, cnt).Trim();
                    }
                    break;
                }
                if (word == string.Empty)
                {
                    Debug.Assert(false);
                }
            }
        }

        public override string ToString()
        {
            string text = string.Empty;
            text += string.Format("{0} \r\n\r\n{1}\r\n",
                this.Nation.Name, this.Nation.Desp);
            foreach (Picture p in this.PicDict.Values)
            {
                text += string.Format("{0}    {1}\r\n", p.No, p.Desp);
            }
            text += LINE_RET;
            foreach (Set s in this.Sets)
            {
                text += string.Format("{0} {1} {2} {3} {4} {5} {6}\r\n",
                    s.Year, s.Month, s.Day, s.Paper, s.Print, s.Perf, s.Desp);
                foreach (Stamp m in s.Stamps)
                {
                    text += string.Format("{0} {1} {2} {3} {4} {5}\r\n",
                        m.No, m.PicNo, m.Denom, m.Color, m.CvNew, m.CvUsed);
                    foreach (StampExpand ex in m.StampExpands)
                    {
                        text += string.Format("{0} {1}\r\n", ex.No, ex.Desp);
                    }

                }
                text += LINE_RET;
            }
            return text;
        }
    }

    public class Element
    {
        public object Id = null;
        public Element()
        {
            //Gen Unique Id
            Id = Comm.Util.GenGuid();
        }
    }

    public class Nation : Element
    {
        public string Name = string.Empty;
        public string Desp = string.Empty;
    }

    public class Set : Element
    {
        public string Year = string.Empty;
        public string Month = string.Empty;
        public string Day = string.Empty;

        public string Paper = string.Empty;
        public string Print = string.Empty;
        public string Perf = string.Empty;

        public string Desp = string.Empty;

        public Collection<Stamp> Stamps = new Collection<Stamp>();
    }

    public class Picture : Element
    {
        public string No = string.Empty;
        public string Desp = string.Empty;

    }

    public class StampExpand : Element
    {
        public Stamp Stamp = null;

        public string No = string.Empty;
        public string Desp = string.Empty;

        public StampExpand(Stamp s)
        {
            this.Stamp = s;
        }
    }

    public class Stamp : Element
    {
        public Set Set = null;
        public Picture Pic = null;

        public string No = string.Empty;
        public string PicNo = string.Empty;
        public string Denom = string.Empty;
        public string Color = string.Empty;
        public string CvNew = string.Empty;
        public string CvUsed = string.Empty;

        public Collection<StampExpand> StampExpands = new Collection<StampExpand>();
        public Stamp(Set set)
        {
            this.Set = set;
        }

    }
}