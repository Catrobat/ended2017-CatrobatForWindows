using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetSizeToBrick : XmlBrick
    {
        public XmlFormula Size { get; set; }

        public XmlSetSizeToBrick() {}

        public XmlSetSizeToBrick(XElement xElement) : base(xElement) {}

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Size = XmlFormulaListFactory.getFormula(xRoot, XmlConstants.Size);
            }
            
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetSizeToBrickType);

            var xElement = Size.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Size);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Size != null)
                Size.LoadReference();
        }
    }
}