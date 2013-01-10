using System.Xml.Linq;

namespace Catrobat.Core.Objects.Bricks
{
    public class ClearGraphicEffectBrick : Brick
    {
        public ClearGraphicEffectBrick()
        {
        }

        public ClearGraphicEffectBrick(Sprite parent) : base(parent)
        {
        }

        public ClearGraphicEffectBrick(XElement xElement, Sprite parent) : base(xElement, parent)
        {
        }

        internal override void LoadFromXML(XElement xRoot)
        {
        }

        internal override XElement CreateXML()
        {
            var xRoot = new XElement("clearGraphicEffectBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override DataObject Copy(Sprite parent)
        {
            var newBrick = new ClearGraphicEffectBrick(parent);

            return newBrick;
        }
    }
}