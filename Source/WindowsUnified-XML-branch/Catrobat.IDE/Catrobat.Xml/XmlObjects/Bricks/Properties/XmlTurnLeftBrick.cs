using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlTurnLeftBrick : XmlBrick
    {
        public XmlFormula Degrees { get; set; }

        public XmlTurnLeftBrick() {}

        public XmlTurnLeftBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            //Degrees = new XmlFormula(xRoot.Element("degrees"));
            Degrees = new XmlFormula(xRoot.Element(XmlConstants.TurnLeftDegrees));
        }

        internal override XElement CreateXml()
        {
            //var xRoot = new XElement("turnLeftBrick");
            //var xRoot = new XElement("brick");
            //xRoot.SetAttributeValue("type", "turnLeftBrick");
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlTurnLeftBrickType);

            //var xVariable = new XElement("degrees");
            var xVariable = new XElement(XmlConstants.TurnLeftDegrees);
            xVariable.Add(Degrees.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Degrees != null)
                Degrees.LoadReference();
        }
    }
}