using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeYByBrick : XmlBrick
    {
        public XmlFormula YMovement { get; set; }

        public XmlChangeYByBrick() {}

        public XmlChangeYByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            YMovement = new XmlFormula(xRoot.Element(XmlConstants.YPositionChange));
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeYByBrickType);

            var xVariable = new XElement(XmlConstants.YPositionChange);
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