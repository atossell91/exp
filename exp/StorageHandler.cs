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
        
        public OMIClist Load(string filepath)
        {
            OMIClist list = new OMIClist();
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNodeList omicsList = doc.SelectNodes("/exports/Test");
            foreach (XmlNode omic in omicsList)
            {
                Debug.WriteLine(omic.Attributes[0].InnerText);
                //Debug.WriteLine("Node: " + node.Attributes[0].InnerText);
                XmlNode s = omic.SelectSingleNode("StickersOut/Other");
                Debug.WriteLine("Stickers: " + s.InnerText);
            }
            return list;
        }
    }
}
