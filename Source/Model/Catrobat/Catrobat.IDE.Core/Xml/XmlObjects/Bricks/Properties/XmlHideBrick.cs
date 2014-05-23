using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
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

        public override XmlObject Copy()
        {
            var newBrick = new XmlHideBrick();

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlHideBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}