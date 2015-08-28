using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Variables
{
    public partial class XmlUserVariable : XmlObjectNode
    {
        public string Name { get; set; }

        public XmlUserVariable() {}

        public XmlUserVariable(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            Name = xRoot.Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.UserVariable, Name);

            return xRoot;
        }
    }
}
