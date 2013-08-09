using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class VariableList : DataObject
    {
        public ObjectVariableList ObjectVariableList;
        public ProgramVariableList ProgramVariableList;

        public VariableList()
        {
        }

        public VariableList(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            ObjectVariableList = new ObjectVariableList(xRoot.Element("objectVariableList"));
            ProgramVariableList = new ProgramVariableList(xRoot.Element("programVariableList"));
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("variables");
            xRoot.Add(ObjectVariableList.CreateXML());
            xRoot.Add(ProgramVariableList.CreateXML());

            return xRoot;
        }

        internal override void LoadReference()
        {
            ObjectVariableList.LoadReference();
        }

        public DataObject Copy()
        {
            var newVariableList = new VariableList();
            newVariableList.ObjectVariableList = ObjectVariableList.Copy() as ObjectVariableList;
            newVariableList.ProgramVariableList = ProgramVariableList.Copy() as ProgramVariableList;

            return newVariableList;
        }

        public override bool Equals(DataObject other)
        {
            var otherVariableList = other as VariableList;
            
            if (otherVariableList == null)
                return false;

            if (ObjectVariableList != null && otherVariableList.ObjectVariableList != null)
            {
                if (!ObjectVariableList.Equals(otherVariableList.ObjectVariableList))
                    return false;
            }
            else if (!(ObjectVariableList == null && otherVariableList.ObjectVariableList == null))
                return false;

            if (ProgramVariableList != null && otherVariableList.ProgramVariableList != null)
            {
                if (!ProgramVariableList.Equals(otherVariableList.ProgramVariableList))
                    return false;
            }
            else if (!(ProgramVariableList == null && otherVariableList.ProgramVariableList == null))
                return false;

            return true;
        }
    }
}
