using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlComeToFrontBrick : XmlBrick
    {
        public XmlComeToFrontBrick() {}

        public XmlComeToFrontBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("comeToFrontBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "comeToFrontBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlComeToFrontBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}