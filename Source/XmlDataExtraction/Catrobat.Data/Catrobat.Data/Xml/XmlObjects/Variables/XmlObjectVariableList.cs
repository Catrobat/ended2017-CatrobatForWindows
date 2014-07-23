using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Variables
{
    public class XmlObjectVariableList : XmlObject
    {
        public List<XmlObjectVariableEntry> ObjectVariableEntries;

        public XmlObjectVariableList() { ObjectVariableEntries = new List<XmlObjectVariableEntry>(); }

        public XmlObjectVariableList(XElement xElement)
        {
            ObjectVariableEntries = new List<XmlObjectVariableEntry>();
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                ObjectVariableEntries.Add(new XmlObjectVariableEntry(element));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("objectVariableList");

            foreach (XmlObjectVariableEntry entry in ObjectVariableEntries)
            {
                if(entry.VariableList.UserVariables.Count > 0)
                    xRoot.Add(entry.CreateXml());
            }

            return xRoot;
        }

        public override void LoadReference()
        {
            foreach(var entry in ObjectVariableEntries)
                entry.LoadReference();
        }
    }
}
