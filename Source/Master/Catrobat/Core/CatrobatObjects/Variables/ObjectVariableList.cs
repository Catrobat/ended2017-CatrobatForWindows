using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Variables
{
    public class ObjectVariableList : DataObject
    {
        public ObservableCollection<ObjectVariableEntry> ObjectVariableEntries;

        public ObjectVariableList() { ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>(); }

        public ObjectVariableList(XElement xElement)
        {
            ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>();
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot == null)
                return;

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
                if(entry.VariableList.UserVariables.Count > 0)
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
            var otherObjectVariableList = other as ObjectVariableList;

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
