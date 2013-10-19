using System.Xml.Linq;

namespace Catrobat.Core.CatrobatObjects.Bricks
{
    public class RepeatLoopEndBrick : LoopEndBrick
    {
        public RepeatLoopEndBrick() {}

        public RepeatLoopEndBrick(XElement xElement) : base(xElement) { }
    }
}