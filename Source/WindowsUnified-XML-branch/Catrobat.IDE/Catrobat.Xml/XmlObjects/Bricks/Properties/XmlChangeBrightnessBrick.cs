using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeBrightnessBrick : XmlBrick
    {
        public XmlFormula ChangeBrightness { get; set; }

        public XmlChangeBrightnessBrick() { }

        public XmlChangeBrightnessBrick(XElement xElement) : base(xElement) { }

        internal override void LoadFromXml(XElement xRoot)
        {
           ChangeBrightness = new XmlFormula(xRoot.Element(XmlConstants.ChangeBrightness));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeBrightnessBrickType);

            var xVariable = new XElement(XmlConstants.ChangeBrightness);
            xVariable.Add(ChangeBrightness.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (ChangeBrightness != null)
                ChangeBrightness.LoadReference();
        }
    }
}
