using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ClearGraphicEffectBrick : Brick
    {
        public ClearGraphicEffectBrick() {}

        public ClearGraphicEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXML(XElement xRoot) {}

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("clearGraphicEffectBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy()
        {
            var newBrick = new ClearGraphicEffectBrick();

            return newBrick;
        }

        public override bool Equals(DataObject other)
        {
            throw new System.NotImplementedException();
        }
    }
}