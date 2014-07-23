using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Scripts
{
    public partial class XmlStartScript : XmlScript
    {
        public XmlStartScript() {}

        public XmlStartScript(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("startScript");

            CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}