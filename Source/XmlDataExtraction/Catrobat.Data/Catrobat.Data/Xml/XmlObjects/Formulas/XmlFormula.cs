using System.Xml.Linq;

namespace Catrobat.Data.Xml.XmlObjects.Formulas
{
    public partial class XmlFormula : XmlObject
    {
        public XmlFormulaTree FormulaTree { get; set; }

        public XmlFormula()
        {
        }

        public XmlFormula(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        public override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("formulaTree") != null)
            {
                FormulaTree = new XmlFormulaTree(xRoot.Element("formulaTree"));
            }
        }

        public override XElement CreateXml()
        {
            return (FormulaTree ?? new XmlFormulaTree()).CreateXml();
        }
    }
}
