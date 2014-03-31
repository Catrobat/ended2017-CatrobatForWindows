using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.FormulaEditor;
using System;
using System.Xml.Linq;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas
{
    public class Formula : DataObject
    {
        #region Properties

        private XmlFormulaTree _xmlFormulaTree;

        // TODO: rename to _formulaTree
        private IFormulaTree _formulaTree2;
        // TODO: rename to FormulaTree
        public IFormulaTree FormulaTree2
        {
            get { return _formulaTree2; }
            set
            {
                if (_formulaTree2 == null && value == null) return;
                if (_formulaTree2 != null && _formulaTree2.Equals(value)) return;
                _formulaTree2 = value;
                RaisePropertyChanged();
            }
        }

        #endregion

        public Formula()
        {
        }

        public Formula(XElement xElement)
        {
            LoadFromXML(xElement);
        }

        internal override void LoadFromXML(XElement xRoot)
        {
            if (xRoot.Element("formulaTree") != null)
            {
                _xmlFormulaTree = new XmlFormulaTree(xRoot.Element("formulaTree"));
                // FormulaTree is converted afterwards (see LoadReference)
            }
        }

        internal override XElement CreateXML()
        {
            var converter = new XmlFormulaTreeConverter();
            return (converter.ConvertBack(_formulaTree2) ?? new XmlFormulaTree()).CreateXML();
        }

        [Obsolete("Use overload with converter instead. ", true)]
        internal new void LoadReference() { base.LoadReference(); }

        /// <summary>Converts <see cref="FormulaTree2"/> from <see cref="XmlFormulaTree" />. </summary>
        /// <param name="converter">The <see cref="XmlFormulaTreeConverter"/> holding the local and global variables. </param>
        internal void LoadReference(XmlFormulaTreeConverter converter)
        {
            FormulaTree2 = converter.Convert(_xmlFormulaTree);
        }

        public DataObject Copy()
        {
            return new Formula
            {
                _formulaTree2 = (IFormulaTree) _formulaTree2.Clone()
            };
        }

        public override bool Equals(DataObject other)
        {
            var otherFormula = other as Formula;

            if (otherFormula == null)
                return false;

            if (FormulaTree2 != null && otherFormula.FormulaTree2 != null)
                return FormulaTree2.Equals(otherFormula.FormulaTree2);

            if (!(FormulaTree2 == null && otherFormula.FormulaTree2 == null))
                return false;

            return true;
         }
    }
}
