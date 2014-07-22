using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlTurnLeftBrick : XmlBrick
    {
        public XmlFormula Degrees { get; set; }

        public XmlTurnLeftBrick() {}

        public XmlTurnLeftBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            Degrees = new XmlFormula(xRoot.Element("degrees"));
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("turnLeftBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(Degrees.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Degrees != null)
                Degrees.LoadReference();
        }
    }
}