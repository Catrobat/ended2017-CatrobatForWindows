using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

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
            XmlParserTempProjectHelper.inObjectVarList = true;
            var xRoot = new XElement(XmlConstants.XmlObjectVariableListType);

            foreach (XmlObjectVariableEntry entry in ObjectVariableEntries)
            {
                xRoot.Add(entry.CreateXml());
            }

            XmlParserTempProjectHelper.inObjectVarList = false;
            return xRoot;
        }

        public override void LoadReference()
        {
            foreach(var entry in ObjectVariableEntries)
                entry.LoadReference();
        }
    }
}
