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
            //Name = xRoot.Element("name").Value;
            Name = xRoot.Element(XmlConstants.Name).Value;
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("userVariable");
            var xRoot = new XElement(XmlConstants.UserVariable);

            //xRoot.Add(new XElement("name", Name));
            xRoot.Add(new XElement(XmlConstants.Name, Name));

            return xRoot;
        }
    }
}
