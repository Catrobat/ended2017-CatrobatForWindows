using System.Linq;
using System.Xml.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public class Formula : DataObject
    {
        private readonly XmlFormulaTreeConverter _converter = new XmlFormulaTreeConverter(Enumerable.Empty<UserVariable>(), null);

        private IFormulaTree _formulaTree2;
        public IFormulaTree FormulaTree2
        {
            get { return _formulaTree2; }
            set
            {
                if (_formulaTree2 == null && value == null) return;
                if (_formulaTree2 != null && _formulaTree2.Equals(value)) return;
                _formulaTree2 = value;
                FormulaTree = _converter.ConvertBack(_formulaTree2);
                RaisePropertyChanged();
            }
        }

        private XmlFormulaTree _formulaTree;
        public XmlFormulaTree FormulaTree
        {
            get { return _formulaTree; }
            set
            {
                if (_formulaTree == value)
                {
                    return;
                }

                _formulaTree = value;
                FormulaTree2 = _converter.Convert(_formulaTree);
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
                FormulaTree = new XmlFormulaTree(xRoot.Element("formulaTree"));
        }

        internal override XElement CreateXML()
        {
            return _formulaTree.CreateXML();
        }

        public DataObject Copy()
        {
            var newFormulaTree = _formulaTree.Copy() as XmlFormulaTree;
            return new Formula()
            {
                _formulaTree = newFormulaTree, 
                _formulaTree2 = _converter.Convert(newFormulaTree)
            };
        }

        public override bool Equals(DataObject other)
        {
            var otherFormula = other as Formula;

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
