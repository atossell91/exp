using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace exp
{
    public class StorageHandler
    {
        XmlElement CreateAndAppendElement(string name, string data, XmlElement parent, XmlDocument doc)
        {
            XmlElement elem = doc.CreateElement(name);
            elem.InnerText = data;
            parent.AppendChild(elem);
            return elem;
        }
        XmlElement OmicToXElem(OMIC om, XmlDocument doc)
        {
            XmlElement elOmic = doc.CreateElement("Test");
            elOmic.SetAttribute("OMIC", om.OMICid);

            XmlElement xml_StickOut = CreateAndAppendElement("StickersOut", "", elOmic, doc);
            CreateAndAppendElement("Other", om.StickersOut.Other.ToString(), xml_StickOut, doc);
            CreateAndAppendElement("Partial", om.StickersOut.Partial.ToString(), xml_StickOut, doc);

            XmlElement xml_StickIn = CreateAndAppendElement("StickersIn", "", elOmic, doc);
            CreateAndAppendElement("Other", om.StickersReturned.Other.ToString(), xml_StickIn, doc);
            CreateAndAppendElement("Partial", om.StickersReturned.Partial.ToString(), xml_StickIn, doc);

            CreateAndAppendElement("UnitCount", om.UnitCount.ToString(), elOmic, doc);
            CreateAndAppendElement("LoadStatus", om.LoadStatus.ToString(), elOmic, doc);
            CreateAndAppendElement("DoorID", om.DoorID.ToString(), elOmic, doc);
            CreateAndAppendElement("TotalWeight", om.TotalWeight.ToString(), elOmic, doc);

            return elOmic;
        }
        public void Save(string filepath, OMIClist data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<?xml version=\"1.0\" encoding=\"UTF-8\"?> <exports></exports>");

            foreach (OMIC om in data)
            {
                XmlElement elem = OmicToXElem(om, doc);
                doc.DocumentElement.AppendChild(elem);
            }
            doc.Save(filepath);
        }
        string ExtractValue(XmlNode node, string xpath)
        {
            return node.SelectSingleNode(xpath).InnerText;
        }
        void setIntValue(ref int value, string s)
        {
            if (!int.TryParse(s, out value))
            {
                Debug.WriteLine("Integer parse error");
                value = 0;
            }
        }
        void setCharValue(ref char value, string s)
        {
            if (!char.TryParse(s, out value))
            {
                Debug.WriteLine("Character parse error");
                value = char.MinValue;
            }
        }
        void setFloatValue(ref float value, string s)
        {
            if (!float.TryParse(s, out value))
            {
                Debug.WriteLine("Float parse error.");
                value = 0f;
            }
        }
        public OMIClist Load(string filepath)
        {
            OMIClist list = new OMIClist();
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNodeList omicsList = doc.SelectNodes("/exports/Test");
            foreach (XmlNode omic in omicsList)
            {
                string id = omic.Attributes.GetNamedItem("OMIC").InnerText;
                OMIC record = new OMIC(id);

                setIntValue(ref record.StickersOut.Other, ExtractValue(omic,"StickersOut/Other"));
                setIntValue(ref record.StickersOut.Partial, ExtractValue(omic,"StickersOut/Partial"));
                setIntValue(ref record.StickersReturned.Other, ExtractValue(omic,"StickersIn/Other"));
                setIntValue(ref record.StickersReturned.Partial, ExtractValue(omic,"StickersIn/Partial"));
                setIntValue(ref record.UnitCount, ExtractValue(omic,"UnitCount"));
                setCharValue(ref record.LoadStatus, ExtractValue(omic, "LoadStatus"));
                record.DoorID = ExtractValue(omic, "DoorID");
                setFloatValue(ref record.TotalWeight, ExtractValue(omic, "TotalWeight"));

                list.AddExistingRecord(record);
            }
            return list;
        }
    }
}
