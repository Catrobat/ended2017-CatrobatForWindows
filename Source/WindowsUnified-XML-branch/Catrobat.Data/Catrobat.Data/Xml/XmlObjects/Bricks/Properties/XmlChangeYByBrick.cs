using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeYByBrick : XmlBrick
    {
        public XmlFormula YMovement { get; set; }

        public XmlChangeYByBrick() {}

        public XmlChangeYByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            YMovement = new XmlFormula(xRoot.Element("yMovement"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeYByNBrick");

            var xVariable = new XElement("yMovement");
            xVariable.Add(YMovement.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (YMovement != null)
                YMovement.LoadReference();
        }
    }
}