using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Nxt
{
    public partial class XmlNxtMotorTurnAngleBrick : XmlBrick
    {
        public XmlFormula Degrees { get; set; }

        public string Motor { get; set; }

        public XmlNxtMotorTurnAngleBrick() {}

        public XmlNxtMotorTurnAngleBrick(XElement xElement) : base(xElement) {}

        public override void LoadFromXml(XElement xRoot)
        {
            Degrees = new XmlFormula(xRoot.Element("degrees"));
            Motor = xRoot.Element("motor").Value;
        }

        public override XElement CreateXml()
        {
            var xRoot = new XElement("legoNxtMotorTurnAngleBrick");

            var xVariable = new XElement("degrees");
            xVariable.Add(Degrees.CreateXml());
            xRoot.Add(xVariable);

             xRoot.Add(new XElement("motor")
            {
                Value = Motor
            });

            return xRoot;
        }

        public override void LoadReference()
        {
            if (Degrees != null)
                Degrees.LoadReference();
        }
    }
}