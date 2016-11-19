using System.Collections.Generic;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public class XmlProgramVariableList : XmlObjectNode
    {
        public List<XmlUserVariableReference> UserVariableReferences { get; set; }

        public XmlProgramVariableList()
        {
            UserVariableReferences = new List<XmlUserVariableReference>();
        }

        public XmlProgramVariableList(XElement xElement)
        {
            UserVariableReferences = new List<XmlUserVariableReference>();
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot == null)
                return;

            foreach (XElement element in xRoot.Elements())
            {
                UserVariableReferences.Add(new XmlUserVariableReference(element));
            }
        }

        internal override XElement CreateXml()
        {
            XmlParserTempProjectHelper.inProgramVarList = true;
            var xRoot = new XElement(XmlConstants.XmlProgramVariableListType);

            foreach (XmlUserVariableReference userVariableReference in UserVariableReferences)
            {
                xRoot.Add(userVariableReference.CreateXml());
            }

            XmlParserTempProjectHelper.inProgramVarList = false;
            return xRoot;
        }
    }
}
