using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlVariableList : XmlObjectNode
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
            ObjectVariableList = new XmlObjectVariableList(xRoot.Element(XmlConstants.XmlObjectVariableListType));
            ProgramVariableList = new XmlProgramVariableList(xRoot.Element(XmlConstants.XmlProgramVariableListType));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Variables);
            xRoot.Add(ObjectVariableList.CreateXml());
            xRoot.Add(ProgramVariableList.CreateXml());

            return xRoot;
        }

        public override void LoadReference()
        {
            ObjectVariableList.LoadReference();
        }
    }
}
