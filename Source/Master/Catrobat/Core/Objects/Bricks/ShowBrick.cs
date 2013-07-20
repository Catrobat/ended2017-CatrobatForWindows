using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ShowBrick : Brick
    {
        public ShowBrick() {}

        public ShowBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("showBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ShowBrick();

            return newBrick;
        }
    }
}