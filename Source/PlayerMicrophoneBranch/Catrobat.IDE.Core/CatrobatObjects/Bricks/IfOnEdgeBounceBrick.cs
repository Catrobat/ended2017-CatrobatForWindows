using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class IfOnEdgeBounceBrick : Brick
    {
        public IfOnEdgeBounceBrick() {}

        public IfOnEdgeBounceBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("ifOnEdgeBounceBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new IfOnEdgeBounceBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as IfOnEdgeBounceBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}