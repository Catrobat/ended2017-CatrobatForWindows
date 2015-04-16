using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlIfOnEdgeBounceBrick : XmlBrick
    {
        public XmlIfOnEdgeBounceBrick() {}

        public XmlIfOnEdgeBounceBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("ifOnEdgeBounceBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}