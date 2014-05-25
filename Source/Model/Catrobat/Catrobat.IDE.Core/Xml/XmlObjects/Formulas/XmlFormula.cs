using System.Xml.Linq;

namespace Catrobat.IDE.Core.Xml.XmlObjects.Formulas
{
    public partial class XmlFormula : XmlObject
    {
        private XmlFormulaTree _formulaTree;
        public XmlFormulaTree FormulaTree
        {
            get { return _formulaTree; }
            set
            {
                if (_formulaTree == value) return;
                _formulaTree = value;
                RaisePropertyChanged();
            }
        }

        public XmlFormula()
        {
        }

        public XmlFormula(XElement xElement)
        {
            LoadFromXml(xElement);
        }

        internal override void LoadFromXml(XElement xRoot)
        {
            if (xRoot.Element("formulaTree") != null)
            {
                _formulaTree = new XmlFormulaTree(xRoot.Element("formulaTree"));
            }
        }

        internal override XElement CreateXml()
        {
            return (_formulaTree ?? new XmlFormulaTree()).CreateXml();
        }

        public XmlObject Copy()
        {
            return new XmlFormula
            {
                _formulaTree = (_formulaTree == null ? null : (XmlFormulaTree) _formulaTree.Copy())
            };
        }

        public override bool Equals(XmlObject other)
        {
            var otherFormula = other as XmlFormula;

            if (otherFormula == null)
                return false;

            if (FormulaTree != null && otherFormula.FormulaTree != null)
                return FormulaTree.Equals(otherFormula.FormulaTree);

            if (!(FormulaTree == null && otherFormula.FormulaTree == null))
                return false;

            return true;
         }
    }
}
