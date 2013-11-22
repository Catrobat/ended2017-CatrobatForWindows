using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class ComeToFrontBrick : Brick
    {
        public ComeToFrontBrick() {}

        public ComeToFrontBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("comeToFrontBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ComeToFrontBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ComeToFrontBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}