using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetBrightnessBrick : XmlBrick
    {
        public XmlFormula Brightness { get; set; }

        public XmlSetBrightnessBrick()
        {
        }

        public XmlSetBrightnessBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            //Brightness = new XmlFormula(xRoot.Element("brightness"));
            Brightness = new XmlFormula(xRoot.Element(XmlConstants.Brightness));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("setBrightnessBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "setBrightnessBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetBrightnessBrickType);

            //var xVariable = new XElement("brightness");
            var xVariable = new XElement(XmlConstants.Brightness);
            xVariable.Add(Brightness.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Brightness != null)
                Brightness.LoadReference();
        }
    }
}