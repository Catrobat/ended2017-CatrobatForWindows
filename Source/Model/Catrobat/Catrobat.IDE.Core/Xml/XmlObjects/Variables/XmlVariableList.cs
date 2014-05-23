using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlVariableList : XmlObject
    {
        /// <summary>Local variables</summary>
        public XmlObjectVariableList ObjectVariableList;

        /// <summary>Global variables</summary>
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

        public XmlObject Copy()
        {
            var newVariableList = new XmlVariableList();
            newVariableList.ObjectVariableList = ObjectVariableList.Copy() as XmlObjectVariableList;
            newVariableList.ProgramVariableList = ProgramVariableList.Copy() as XmlProgramVariableList;

            return newVariableList;
        }

        public override bool Equals(XmlObject other)
        {
            var otherVariableList = other as XmlVariableList;
            
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
