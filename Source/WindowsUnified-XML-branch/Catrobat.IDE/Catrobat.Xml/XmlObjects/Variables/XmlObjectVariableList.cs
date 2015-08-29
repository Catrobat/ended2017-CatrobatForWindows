using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlObjectVariableList : XmlObjectNode
    {
        public List<XmlObjectVariableEntry> ObjectVariableEntries;

        public XmlObjectVariableList() { ObjectVariableEntries = new List<XmlObjectVariableEntry>(); }

        public XmlObjectVariableList(XElement xElement)
        {
            ObjectVariableEntries = new List<XmlObjectVariableEntry>();
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                ObjectVariableEntries.Add(new XmlObjectVariableEntry(element));
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.XmlObjectVariableListType);

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
