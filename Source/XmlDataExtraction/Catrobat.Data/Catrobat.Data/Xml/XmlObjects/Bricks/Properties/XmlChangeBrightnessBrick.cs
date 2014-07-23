using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeBrightnessBrick : XmlBrick
    {
        public XmlFormula ChangeBrightness { get; set; }

        public XmlChangeBrightnessBrick() { }

        public XmlChangeBrightnessBrick(XElement xElement) : base(xElement) { }

        public override void LoadFromXml(XElement xRoot)
        {
            ChangeBrightness = new XmlFormula(xRoot.Element("changeBrightness"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("changeBrightnessByNBrick");

            var xVariable = new XElement("changeBrightness");
            xVariable.Add(ChangeBrightness.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (ChangeBrightness != null)
                ChangeBrightness.LoadReference();
        }
    }
}
