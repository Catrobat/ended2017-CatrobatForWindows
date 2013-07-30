using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class StopAllSoundsBrick : Brick
    {
        public StopAllSoundsBrick() {}

        public StopAllSoundsBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("stopAllSoundsBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new StopAllSoundsBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}