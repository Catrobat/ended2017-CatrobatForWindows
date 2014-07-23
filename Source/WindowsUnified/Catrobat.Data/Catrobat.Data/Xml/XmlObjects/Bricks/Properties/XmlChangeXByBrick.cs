using System.Xml.Linq;
using Catrobat.Data.Xml.XmlObjects.Formulas;

namespace Catrobat.Data.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeXByBrick : XmlBrick
    {
        public XmlFormula XMovement { get; set; }

        public XmlChangeXByBrick() {}

        public XmlChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            XMovement = new XmlFormula(xRoot.Element("xMovement"));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement("changeXByNBrick");

            var xVariable = new XElement("xMovement");
            xVariable.Add(XMovement.CreateXml());
            xRoot.Add(xVariable);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (XMovement != null)
                XMovement.LoadReference();
        }
    }
}