using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
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
            throw new System.NotImplementedException();
        }
    }
}