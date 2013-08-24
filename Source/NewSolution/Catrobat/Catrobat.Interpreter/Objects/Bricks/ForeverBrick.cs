using System.Xml.Linq;

namespace Catrobat.Interpreter.Objects.Bricks
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

            if(_loopEndBrickReference != null)
                newBrick.LoopEndBrickReference = _loopEndBrickReference.Copy() as LoopEndBrickReference;

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            var otherBrick = other as ForeverBrick;

            if (otherBrick == null)
                return false;

            return LoopEndBrickReference.Equals(otherBrick.LoopEndBrickReference);
        }
    }
}