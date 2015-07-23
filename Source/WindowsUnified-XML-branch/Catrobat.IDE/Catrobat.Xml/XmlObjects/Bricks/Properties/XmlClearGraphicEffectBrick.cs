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
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlClearGraphicEffectBrickType);

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}