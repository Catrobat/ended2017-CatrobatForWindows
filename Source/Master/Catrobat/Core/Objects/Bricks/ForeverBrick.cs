using System.Xml.Linq;

namespace Catrobat.Core.Objects
{
    public class ForeverBrick : LoopBeginBrick
    {
        public ForeverBrick()
        {
        }

        public ForeverBrick(Sprite parent) : base(parent)
        {
        }

        public ForeverBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

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

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ForeverBrick(parent);

            return newBrick;
        }
    }
}