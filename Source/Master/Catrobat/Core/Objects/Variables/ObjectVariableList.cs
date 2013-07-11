using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class ObjectVariableList : DataObject
    {
        public ObservableCollection<ObjectVariableEntry> ObjectVariableEntries;

        public ObjectVariableList() {}

        public ObjectVariableList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>();
            foreach (XElement element in xRoot.Elements())
            {
                ObjectVariableEntries.Add(new ObjectVariableEntry(element));
            }
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("objectVariableList");

            foreach (ObjectVariableEntry entry in ObjectVariableEntries)
            {
                xRoot.Add(entry.CreateXML());
            }

            return xRoot;
        }
    }
}
