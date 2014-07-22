using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Variables
{
    public partial class XmlUserVariable : XmlObject
    {
        public string Name { get; set; }

        public XmlUserVariable() {}

        public XmlUserVariable(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            Name = xRoot.Element("name").Value;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("userVariable");

            xRoot.Add(new XElement("name", Name));

            return xRoot;
        }
    }
}
