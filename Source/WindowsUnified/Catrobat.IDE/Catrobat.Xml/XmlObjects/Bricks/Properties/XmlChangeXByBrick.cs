using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlChangeXByBrick : XmlBrick
    {
        public XmlFormula XMovement { get; set; }

        public XmlChangeXByBrick() {}

        public XmlChangeXByBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                XMovement = new XmlFormula(xRoot, XmlConstants.XPositionChange);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlChangeXByBrickType);

            var xElement = XMovement.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.XPositionChange);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (XMovement != null)
                XMovement.LoadReference();
        }
    }
}