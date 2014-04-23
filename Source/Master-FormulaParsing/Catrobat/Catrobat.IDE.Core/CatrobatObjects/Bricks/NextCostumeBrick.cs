using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class NextCostumeBrick : Brick
    {
        public NextCostumeBrick() {}

        public NextCostumeBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("nextLookBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new NextCostumeBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as NextCostumeBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}