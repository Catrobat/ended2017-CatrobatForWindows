using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ComeToFrontBrick : Brick
    {
        public ComeToFrontBrick() {}

        public ComeToFrontBrick(Sprite parent) : base(parent) {}

        public ComeToFrontBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("comeToFrontBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ComeToFrontBrick(parent);

            return newBrick;
        }
    }
}