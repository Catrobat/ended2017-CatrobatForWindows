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

        public ObjectVariableList() { ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>(); }

        public ObjectVariableList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot == null)
            {
                ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>();
                return;
            }
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

        internal override void LoadReference()
        {
            foreach(var entry in ObjectVariableEntries)
                entry.LoadReference();
        }

        public DataObject Copy()
        {
            var newObjectVariableList = new ObjectVariableList();

            foreach(var entry in ObjectVariableEntries)
                newObjectVariableList.ObjectVariableEntries.Add(entry.Copy() as ObjectVariableEntry);

            return newObjectVariableList;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}
