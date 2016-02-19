using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.Xml.XmlObjects
{
    public partial class XmlSound : XmlObjectNode
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public XmlSound() {}

        public XmlSound(string name)
        {
            Name = name;
            FileName = FileNameGenerationHelper.Generate() + Name;
        }

        internal XmlSound(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            FileName = xRoot.Element(XmlConstants.FileName).Value;
            Name = xRoot.Element(XmlConstants.Name).Value;
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Sound);

            xRoot.Add(new XElement(XmlConstants.FileName, FileName));

            xRoot.Add(new XElement(XmlConstants.Name, Name));

            return xRoot;
        }
    }
}
