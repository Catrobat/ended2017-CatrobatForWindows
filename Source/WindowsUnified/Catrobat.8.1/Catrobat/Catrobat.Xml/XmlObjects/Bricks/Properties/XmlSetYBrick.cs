using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetYBrick : XmlBrick
    {
        public XmlFormula YPosition { get; set; }

        public XmlSetYBrick() {}

        public XmlSetYBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                YPosition = new XmlFormula(xRoot, XmlConstants.YPosition);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetYBrickType);

            var xElement = YPosition.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.YPosition);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        public override void LoadReference()
        {
            if (YPosition != null)
                YPosition.LoadReference();
        }
    }
}
