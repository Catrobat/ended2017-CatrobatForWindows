using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects;

namespace Catrobat.IDE.Core.Xml.Variables
{
    public class XmlVariableList : DataObject
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
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            ObjectVariableList = new XmlObjectVariableList(xRoot.Element("objectVariableList"));
            ProgramVariableList = new XmlProgramVariableList(xRoot.Element("programVariableList"));
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
            var newVariableList = new XmlVariableList();
            newVariableList.ObjectVariableList = ObjectVariableList.Copy() as XmlObjectVariableList;
            newVariableList.ProgramVariableList = ProgramVariableList.Copy() as XmlProgramVariableList;

            return newVariableList;
        }

        public override bool Equals(DataObject other)
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
