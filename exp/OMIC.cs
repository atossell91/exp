using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp
{
    public class OMIC
    {
        public class StickerCounts
        {
            public int Partial;
            public int Other;
        }

        public string OMICid;

        public StickerCounts StickersOut;
        public StickerCounts StickersReturned;
        
        public int UnitCount; //Box/combo count
        public char LoadStatus; //Partial or complete
        public string DoorID;
        public float TotalWeight;
        public string Notes;

        public OMIC(string omic)
        {
            OMICid = omic;
            StickersOut = new StickerCounts();
            StickersOut.Partial = 0;
            StickersOut.Other = 0;

            StickersReturned = new StickerCounts();
            StickersReturned.Partial = 0;
            StickersReturned.Other = 0;

            LoadStatus = 'N';

            DoorID = "NONE";// string.Empty;
            Notes = string.Empty;
        }

        public override string ToString()
        {
            string s = " ";
            return OMICid + s + StickersOut.Partial + s + StickersOut.Other + s + StickersReturned.Partial + s + StickersReturned.Other + s + UnitCount + s + LoadStatus + s + DoorID + s + TotalWeight;
        }
        public string ToString(string format, int colWidth)
        {
            Dictionary<string, object> dict = new Dictionary<string, object> {
                {"OM", OMICid},
                {"OP", StickersOut.Partial},
                {"OO", StickersOut.Other},
                {"IP", StickersReturned.Partial},
                {"IO", StickersReturned.Other},
                {"UC", UnitCount},
                {"LS", LoadStatus},
                {"DI", DoorID},
                {"TW", TotalWeight}
            };
            string output = format;
            output = output.Replace(" ", "~");
            foreach (string key in dict.Keys)
            {
                output = output.Replace(key, dict[key].ToString()).PadLeft(colWidth);
            }
            output = output.Replace("~", "");
            return output;
        }
    }
}
