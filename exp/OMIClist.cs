using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp
{
    public class OMIClist : IEnumerable<OMIC>
    {
        private List<OMIC> list;

        public OMIClist()
        {
            list = new List<OMIC>();
        }
        
        public IEnumerator<OMIC> GetEnumerator()
        {
            return list.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public int FindOMIC(string omic)
        {
            return list.FindIndex((a) => { return a.OMICid.Equals(omic); });
        }
        public bool RecordExists(string omic)
        {
            return FindOMIC(omic) >= 0;
        }
        public bool AddNewRecord(string omic)
        {
            if (FindOMIC(omic) >= 0)
            {
                return false;
            }

            OMIC om = new OMIC(omic);
            list.Add(om);

            return true;
        }
        public bool AddExistingRecord(OMIC omic)
        {
            string om = omic.OMICid;
            if (omic == null || String.IsNullOrEmpty(om) || FindOMIC(om) >= 0)
            {
                return false;
            }

            list.Add(omic);

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
        public OMIClist Filter(Predicate<OMIC> p)
        {
            List<OMIC> l = list.FindAll(p);
            OMIClist oml = new OMIClist();
            foreach (OMIC o in l) {
                oml.AddExistingRecord(o);
            }
            return oml;
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
