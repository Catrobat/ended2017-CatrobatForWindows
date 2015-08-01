using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlLook : XmlObjectNode
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public XmlLook() { }

        public XmlLook(string name)
        {
            Name = name;
            FileName = FileNameGenerationHelper.Generate() + Name;
        }

        internal XmlLook(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            FileName = xRoot.Element(XmlConstants.FileName).Value;
            Name = xRoot.Attribute(XmlConstants.Name).Value;
            
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Look);

            xRoot.Add(new XElement(XmlConstants.FileName, FileName));

            xRoot.SetAttributeValue(XmlConstants.Name, Name.ToString());

            return xRoot;
        }
    }
}