using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlVariableList : XmlObject
    {
        public XmlObjectVariableList ObjectVariableList;

        public XmlProgramVariableList ProgramVariableList;

        public XmlVariableList()
        {
            ObjectVariableList = new XmlObjectVariableList();
            ProgramVariableList = new XmlProgramVariableList();
        }

        public XmlVariableList(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            ObjectVariableList = new XmlObjectVariableList(xRoot.Element("objectVariableList"));
            ProgramVariableList = new XmlProgramVariableList(xRoot.Element("programVariableList"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("variables");
            xRoot.Add(ObjectVariableList.CreateXml());
            xRoot.Add(ProgramVariableList.CreateXml());

            return xRoot;
        }

        internal override void LoadReference()
        {
            ObjectVariableList.LoadReference();
        }
    }
}
