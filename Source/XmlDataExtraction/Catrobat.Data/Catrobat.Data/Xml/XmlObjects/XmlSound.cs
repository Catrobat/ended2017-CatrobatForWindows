using System.Xml.Linq;
using Catrobat.Data.Utilities.Helpers;

namespace Catrobat.Data.Xml.XmlObjects
{
    public partial class XmlSound : XmlObject
    {
        public string FileName { get; set; }

        public string Name { get; set; }

        public XmlSound() {}

        public XmlSound(string name)
        {
            Name = name;
            FileName = FileNameGenerationHelper.Generate() + "_" +  Name;
        }

        public XmlSound(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            FileName = xRoot.Element("fileName").Value;
            Name = xRoot.Element("name").Value;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("sound");

            xRoot.Add(new XElement("fileName", FileName));

            xRoot.Add(new XElement("name", Name));

            return xRoot;
        }
    }
}