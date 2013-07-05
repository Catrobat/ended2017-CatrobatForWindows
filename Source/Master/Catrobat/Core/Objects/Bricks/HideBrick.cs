using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class HideBrick : Brick
    {
        public HideBrick() {}

        public HideBrick(Sprite parent) : base(parent) {}

        public HideBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("hideBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new HideBrick(parent);

            return newBrick;
        }
    }
}