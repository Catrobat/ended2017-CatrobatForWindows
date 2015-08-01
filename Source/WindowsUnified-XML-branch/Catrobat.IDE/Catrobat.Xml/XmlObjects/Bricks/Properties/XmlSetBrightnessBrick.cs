using System.Xml.Linq;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Properties
{
    public partial class XmlSetBrightnessBrick : XmlBrick
    {
        public XmlFormula Brightness { get; set; }

        public XmlSetBrightnessBrick()
        {
        }

        public XmlSetBrightnessBrick(XElement xElement) : base(xElement)
        {
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot != null)
            {
                Brightness = XmlFormulaTreeFactory.getFormula(xRoot, XmlConstants.Brightness);
            }
        }

        internal override XElement CreateXml()
        {
            var xRoot = new XElement(XmlConstants.Brick);
            xRoot.SetAttributeValue(XmlConstants.Type, XmlConstants.XmlSetBrightnessBrickType);

            var xElement = Brightness.CreateXml();
            xElement.SetAttributeValue(XmlConstants.Category, XmlConstants.Brightness);

            var xFormulalist = new XElement(XmlConstants.FormulaList);
            xFormulalist.Add(xElement);

            xRoot.Add(xFormulalist);

            return xRoot;
        }

        internal override void LoadReference()
        {
            if (Brightness != null)
                Brightness.LoadReference();
        }
    }
}