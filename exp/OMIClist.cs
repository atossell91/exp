using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp
{
    public class OMIClist
    {
        private List<OMIC> list;

        public OMIClist()
        {
            list = new List<OMIC>();
        }
        
        public int FindOMIC(string omic)
        {
            return list.FindIndex((a) => { return a.OMICid.Equals(omic); });
        }
        public bool RecordExists(string omic)
        {
            return FindOMIC(omic) >= 0;
        }
        public bool AddRecord(string omic)
        {
            if (FindOMIC(omic) >= 0)
            {
                return false;
            }

            OMIC om = new OMIC(omic);
            list.Add(om);

            return true;
        }
        public bool RemoveRecord(string omic)
        {
            int index = FindOMIC(omic);

            if (index < 0)
            {
                return false;
            }

            list.RemoveAt(index);
            
            return true;
        }
        public OMIC GetRecord(string omic)
        {
            return list.Find((a)=> { return a.OMICid == omic; });
        }
        public int RecordCount()
        {
            return list.Count;
        }
        public List<OMIC> Filter(Predicate<OMIC> p)
        {
            return list.FindAll(p);
        }
        public string[] ListToString(string format, int colWidth)
        {
            int len = list.Count;
            string[] output = new string[len];
            for (int n =0; n < len; ++n)
            {
                OMIC o = list[n];
                string currentOM = o.OMICid;
                output[n] = o.ToString(format, colWidth);
            }
            return output;
        }
    }
}
