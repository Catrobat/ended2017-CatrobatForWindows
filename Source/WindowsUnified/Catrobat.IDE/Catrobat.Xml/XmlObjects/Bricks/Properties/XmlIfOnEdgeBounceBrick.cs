using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlIfOnEdgeBounceBrick : XmlBrick
    {
        public XmlIfOnEdgeBounceBrick() {}

        public XmlIfOnEdgeBounceBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlIfOnEdgeBounceBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}