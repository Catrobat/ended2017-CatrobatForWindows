using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class StopAllSoundsBrick : Brick
    {
        public StopAllSoundsBrick()
        {
        }

        public StopAllSoundsBrick(Sprite parent) : base(parent)
        {
        }

        public StopAllSoundsBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal override void LoadFromXML(XElement xRoot)
        {
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("stopAllSoundsBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new StopAllSoundsBrick(parent);

            return newBrick;
        }
    }
}