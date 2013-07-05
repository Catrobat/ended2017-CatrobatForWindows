using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ShowBrick : Brick
    {
        public ShowBrick() {}

        public ShowBrick(Sprite parent) : base(parent) {}

        public ShowBrick(XElement xElement, Sprite parent) : base(xElement, parent) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("showBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ShowBrick(parent);

            return newBrick;
        }
    }
}