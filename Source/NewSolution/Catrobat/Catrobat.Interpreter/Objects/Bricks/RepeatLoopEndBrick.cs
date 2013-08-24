using System.Xml.Linq;

namespace Catrobat.Interpreter.Objects.Bricks
{
    public class RepeatLoopEndBrick : LoopEndBrick
    {
        public RepeatLoopEndBrick() {}

        public RepeatLoopEndBrick(XElement xElement) : base(xElement) { }
    }
}