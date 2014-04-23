using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
{
    public class ForeverLoopEndBrick : LoopEndBrick
    {
        public ForeverLoopEndBrick() {}

        public ForeverLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXML(XElement xRoot)
        {
            base.LoadFromCommonXML(xRoot);
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndlessBrick");
            base.CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ForeverLoopEndBrick();

            if (_loopBeginBrickReference != null)
                newBrick.LoopBeginBrickReference = _loopBeginBrickReference.Copy() as LoopBeginBrickReference;

            return newBrick;
        }
    }
}