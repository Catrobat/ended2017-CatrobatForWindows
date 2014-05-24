using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlObjectVariableList : XmlObject
    {
        public ObservableCollection<XmlObjectVariableEntry> ObjectVariableEntries;

        public XmlObjectVariableList() { ObjectVariableEntries = new ObservableCollection<XmlObjectVariableEntry>(); }

        public XmlObjectVariableList(XElement xElement)
        {
            ObjectVariableEntries = new ObservableCollection<XmlObjectVariableEntry>();
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
            var xRoot = new XElement("objectVariableList");

            foreach (XmlObjectVariableEntry entry in ObjectVariableEntries)
            {
                if(entry.VariableList.UserVariables.Count > 0)
                    xRoot.Add(entry.CreateXml());
            }

            return xRoot;
        }

        internal override void LoadReference()
        {
            foreach(var entry in ObjectVariableEntries)
                entry.LoadReference();
        }

        public XmlObject Copy()
        {
            var newObjectVariableList = new XmlObjectVariableList();

            foreach(var entry in ObjectVariableEntries)
                newObjectVariableList.ObjectVariableEntries.Add(entry.Copy() as XmlObjectVariableEntry);

            return newObjectVariableList;
        }

        public override bool Equals(XmlObject other)
        {
            var otherObjectVariableList = other as XmlObjectVariableList;

            if (otherObjectVariableList == null)
                return false;

            var count = ObjectVariableEntries.Count;
            var otherCount = otherObjectVariableList.ObjectVariableEntries.Count;

            if (count != otherCount)
                return false;

            for (int i = 0; i < count; i++)
                if (!ObjectVariableEntries[i].Equals(otherObjectVariableList.ObjectVariableEntries[i]))
                    return false;

            return true;
        }
    }
}
