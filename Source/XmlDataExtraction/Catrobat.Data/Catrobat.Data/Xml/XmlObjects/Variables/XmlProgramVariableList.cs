using System.Collections.Generic;
using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Variables
{
    public class XmlProgramVariableList : XmlObject
    {
        public List<XmlUserVariable> UserVariables;

        public XmlProgramVariableList()
        {
            UserVariables = new List<XmlUserVariable>();
        }

        public XmlProgramVariableList(XElement xElement)
        {
            UserVariables = new List<XmlUserVariable>();
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                UserVariables.Add(new XmlUserVariable(element));
            }
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("programVariableList");

            foreach (XmlUserVariable userVariable in UserVariables)
            {
                xRoot.Add(userVariable.CreateXml());
            }

            return xRoot;
        }
    }
}
