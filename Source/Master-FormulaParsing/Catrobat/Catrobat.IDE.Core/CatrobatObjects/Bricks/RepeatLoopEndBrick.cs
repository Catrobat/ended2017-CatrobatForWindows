using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class RepeatLoopEndBrick : LoopEndBrick
    {
        public RepeatLoopEndBrick() {}

        public RepeatLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new RepeatLoopEndBrick();

            if (_loopBeginBrickReference != null)
                newBrick.LoopBeginBrickReference = _loopBeginBrickReference.Copy() as LoopBeginBrickReference;

            return newBrick;
        }
    }
}