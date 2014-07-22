using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlClearGraphicEffectBrick : XmlBrick
    {
        public XmlClearGraphicEffectBrick() {}

        public XmlClearGraphicEffectBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot) {}

        public override XElement CreateXml()
        {
            var xRoot = new XElement("clearGraphicEffectBrick");

            //CreateCommonXML(xRoot);

            return xRoot;
        }
    }
}