using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes
{
    public partial class XmlNextCostumeBrick : XmlBrick
    {
        public XmlNextCostumeBrick() {}

        public XmlNextCostumeBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("nextLookBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlNextCostumeBrick();

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlNextCostumeBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}