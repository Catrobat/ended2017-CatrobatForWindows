using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Variables
{
    public class Variables : DataObject
    {
        public ObjectVariableList ObjectVariableList;
        public ProgramVariableList ProgramVariableList;

        public Variables() {}

        public Variables(XElement xElement)
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
    }
}
