using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlIfOnEdgeBounceBrick : XmlBrick
    {
        public override bool Equals(System.Object obj)
        {
            XmlIfOnEdgeBounceBrick b = obj as XmlIfOnEdgeBounceBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlIfOnEdgeBounceBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

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
