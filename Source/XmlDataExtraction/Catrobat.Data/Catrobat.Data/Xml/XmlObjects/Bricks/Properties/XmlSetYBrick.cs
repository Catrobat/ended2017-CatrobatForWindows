using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetYBrick : XmlBrick
    {
        public XmlFormula YPosition { get; set; }

        public XmlSetYBrick() {}

        public XmlSetYBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            YPosition = new XmlFormula(xRoot.Element("yPosition"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("setYBrick");

            var xVariable = new XElement("yPosition");
            xVariable.Add(YPosition.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}