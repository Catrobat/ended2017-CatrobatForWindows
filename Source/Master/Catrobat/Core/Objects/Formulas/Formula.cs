using System;
using System.Xml.Linq;

namespace Catrobat.Core.Objects.Formulas
{
    public class Formula : DataObject
    {
        private FormulaTree _formulaTree;
        public FormulaTree FormulaTree
        {
            get { return _formulaTree; }
            set
            {
                if (_formulaTree == value)
                {
                    return;
                }

                _formulaTree = value;
                RaisePropertyChanged();
            }
        }

        public Formula() {}

        public Formula(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("formulaTree") != null)
                _formulaTree = new FormulaTree(xRoot.Element("formulaTree"));
        }

        internal override XElement CreateXML()
        {
            return _formulaTree.CreateXML();
        }

        public DataObject Copy()
        {
            var newFormula = new Formula();
            newFormula._formulaTree = _formulaTree.Copy() as FormulaTree;

            return newFormula;
        }
    }
}
