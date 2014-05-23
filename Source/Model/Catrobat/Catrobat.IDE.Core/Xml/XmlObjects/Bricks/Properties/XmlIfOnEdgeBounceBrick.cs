using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public class XmlIfOnEdgeBounceBrick : XmlBrick
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

        public override XmlObject Copy()
        {
            var newBrick = new XmlIfOnEdgeBounceBrick();

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlIfOnEdgeBounceBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}