using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ForeverBrick : LoopBeginBrick
    {
        public ForeverBrick() {}

        public ForeverBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("foreverBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ForeverBrick();

            newBrick.LoopEndBrickReference = _loopEndBrickReference.Copy() as LoopEndBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}