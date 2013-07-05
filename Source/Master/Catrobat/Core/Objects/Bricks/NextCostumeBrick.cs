using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class NextCostumeBrick : Brick
    {
        public NextCostumeBrick() {}

        public NextCostumeBrick(Sprite parent) : base(parent) {}

        public NextCostumeBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nextCostumeBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new NextCostumeBrick(parent);

            return newBrick;
        }
    }
}