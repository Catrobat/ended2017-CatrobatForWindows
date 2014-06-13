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
            Brightness = new XmlFormula(xRoot.Element("brightness"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("setBrightnessBrick");

            var xVariable = new XElement("brightness");
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