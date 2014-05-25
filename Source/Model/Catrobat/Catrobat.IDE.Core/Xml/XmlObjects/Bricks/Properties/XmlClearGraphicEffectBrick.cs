using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlClearGraphicEffectBrick : XmlBrick
    {
        public XmlClearGraphicEffectBrick() {}

        public XmlClearGraphicEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("clearGraphicEffectBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }

        public override XmlObject Copy()
        {
            var newBrick = new XmlClearGraphicEffectBrick();

            return newBrick;
        }

        public override bool Equals(XmlObject other)
        {
            var otherBrick = other as XmlClearGraphicEffectBrick;

            if (otherBrick == null)
                return false;

            return true;
        }
    }
}