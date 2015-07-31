using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    public partial class XmlFormula : XmlObjectNode
    {
        public XmlFormulaTree FormulaTree { get; set; }

        public XmlFormula()
        {
        }

        public XmlFormula(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            //TODO: notwendig? oder doch nur  if (xRoot.Element(XmlConstants.Formula) != null)
            if (xRoot.Element(XmlConstants.FormulaList).Element(XmlConstants.Formula) != null)
            {
                FormulaTree = new XmlFormulaTree(xRoot.Element(XmlConstants.Formula));
            }
        }

        internal override XElement CreateXml()
        {
            return (FormulaTree ?? new XmlFormulaTree()).CreateXml();
        }
    }
}
