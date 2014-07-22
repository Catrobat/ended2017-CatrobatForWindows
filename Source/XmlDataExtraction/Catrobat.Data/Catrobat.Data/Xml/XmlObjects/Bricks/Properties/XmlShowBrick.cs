using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlShowBrick : XmlBrick
    {
        public XmlShowBrick() {}

        public XmlShowBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot) {}

        public override XElement CreateXml()
        {
            var xRoot = new XElement("showBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}