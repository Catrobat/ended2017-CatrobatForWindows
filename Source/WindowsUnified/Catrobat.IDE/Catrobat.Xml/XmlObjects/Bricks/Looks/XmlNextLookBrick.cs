using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Looks
{
    public partial class XmlNextLookBrick : XmlBrick
    {
        public XmlNextLookBrick() {}

        public XmlNextLookBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlNextLookBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}