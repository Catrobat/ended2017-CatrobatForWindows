using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Objects.Bricks
{
    public class ForeverLoopEndBrick : LoopEndBrick
    {
        public ForeverLoopEndBrick() {}

        public ForeverLoopEndBrick(XElement xElement) : base(xElement) { }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("loopEndlessBrick");

            xRoot.Add(_loopBeginBrickReference.CreateXML());

            return xRoot;
        }
    }
}