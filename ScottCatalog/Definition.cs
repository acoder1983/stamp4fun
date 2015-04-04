using System;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.ScottCatalog
{
    class StringArray
    {
        protected string[] array = new string[] { };

        public bool IsIn(string word)
        {
            for (int i = 0; i < this.array.Length; ++i)
            {
                if (word == this.array[i])
                {
                    return true;
                }
            }
            return false;
        }


    }

    class PicTypes : StringArray
    {
        public PicTypes()
        {
            this.array = new string[]{
            "A",  "AP", "CD", "D", "M", "OS", "R", "SP"
        };
        }
    }

    class SpecialStampTypes : StringArray
    {
        public SpecialStampTypes()
        {
            this.array = new string[]{
                "AR", "B", "C", "CB", "CBO", "CE", "CF", "CO", "CQ", "E", "EB", "EO", "EY", "F", "G", "GY", "H", 
                "I", "J", "M", "MC", "MR", "MQ", "N", "NB", "NC", "NE", "NO", "NJ", "NRA", 
                "O", "P", "Q", "QE", "QY", "RA", "RAC", "RAB", "RAJ", "S"
            };
        }
    }


    class Months : StringArray
    {
        public Months()
        {
            this.array = new string[]{
            "Jan", "Feb", "Mar", "Apr", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"
        };
        }
    }

    class PaperTypes : StringArray
    {
        public PaperTypes()
        {
            this.array = new string[]{
            "Wmk", "Unwmk","Wmkd", "Unwmkd"
        };
        }
    }

    class PrintTypes : StringArray
    {
        public PrintTypes()
        {
            this.array = new string[]{
            "Engr",  "Litho",  "Photo", "Typo", "Lithographed", "Photogravure", "Engraved", "Embossed", "Typographed"
            };
        }
    }


    class PerfTypes : StringArray
    {
        public PerfTypes()
        {
            this.array = new string[]{
                "Perf", "Imperf"
            };
        }
    }

    class AbbreWords : StringArray
    {
        public AbbreWords()
        {
            this.array = new string[]{
                "St.", "Pres.", "Intl.", "Cent.", "No.", "Assoc.", "Bicent."
            };
        }
    }
}