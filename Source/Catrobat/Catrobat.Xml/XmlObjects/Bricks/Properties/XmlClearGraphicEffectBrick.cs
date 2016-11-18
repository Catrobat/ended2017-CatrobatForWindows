using System.Xml.Linq;
using Catrobat_Player.NativeComponent;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlClearGraphicEffectBrick : XmlBrick, IClearGraphicEffectBrick
    {
        public override bool Equals(System.Object obj)
        {
            XmlClearGraphicEffectBrick b = obj as XmlClearGraphicEffectBrick;
            if ((object)b == null)
                return false;

            return this.Equals(b);
        }

        public bool Equals(XmlClearGraphicEffectBrick b)
        {
            return this.Equals((XmlBrick)b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public XmlClearGraphicEffectBrick() {}

        public XmlClearGraphicEffectBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot) {}

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlClearGraphicEffectBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}
