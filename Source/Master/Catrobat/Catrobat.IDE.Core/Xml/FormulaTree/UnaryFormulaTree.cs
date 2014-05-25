using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class UnaryFormulaTree
    {
        #region Implements IXmlFormulaConvertible

        protected abstract XmlFormulaTree ToXmlFormula(XmlFormulaTree child);

        public override XmlFormulaTree ToXmlFormula()
        {
            return ToXmlFormula(Child == null ? null : Child.ToXmlFormula());
        }

        #endregion
    }
}
