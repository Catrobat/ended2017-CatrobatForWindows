using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class HideBrick : Brick
    {
        public HideBrick() {}

        public HideBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("hideBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new HideBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as HideBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}