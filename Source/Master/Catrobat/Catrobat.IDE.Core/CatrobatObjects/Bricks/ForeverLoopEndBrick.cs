using System.ComponentModel;
using System.Xml.Linq;
using Catrobat.IDE.Core.Utilities.Helpers;

namespace Catrobat.IDE.Core.CatrobatObjects.Bricks
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