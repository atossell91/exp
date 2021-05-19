using Microsoft.VisualStudio.TestTools.UnitTesting;
using exp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exp.Tests
{
    [TestClass()]
    public class OMIClistTests
    {
        [TestMethod()]
        public void AddRecordTest()
        {
            string rec = "070499";
            OMIClist l = new OMIClist();
            Assert.AreEqual(0, l.RecordCount());
            l.AddRecord(rec);
            Assert.AreEqual(1, l.RecordCount());
            Assert.AreEqual(true, l.RecordExists(rec));
        }

        [TestMethod()]
        public void RemoveRecordTest()
        {
            string rec = "070584";
            OMIClist l = new OMIClist();
            l.AddRecord(rec);
            l.RemoveRecord(rec);
            Assert.AreEqual(0, l.RecordCount());
        }
        [TestMethod()]
        public void updateRecord()
        {
            string rec1 = "070121";
            string rec2 = "070122";
            string rec3 = "070123";
            OMIClist l = new OMIClist();
            l.AddRecord(rec1);
            l.AddRecord(rec2);
            l.AddRecord(rec3);

            OMIC o = l.GetRecord(rec2);
            string dId = "11;";
            o.DoorID = dId;

            Assert.AreEqual(l.GetRecord(rec2).DoorID, dId);
        }
    }
}