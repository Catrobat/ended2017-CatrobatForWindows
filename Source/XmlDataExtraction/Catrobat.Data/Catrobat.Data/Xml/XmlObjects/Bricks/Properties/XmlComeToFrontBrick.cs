using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlComeToFrontBrick : XmlBrick
    {
        public XmlComeToFrontBrick() {}

        public XmlComeToFrontBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot) {}

        public override XElement CreateXml()
        {
            var xRoot = new XElement("comeToFrontBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}