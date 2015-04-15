using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlHideBrick : XmlBrick
    {
        public XmlHideBrick() {}

        public XmlHideBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("hideBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}