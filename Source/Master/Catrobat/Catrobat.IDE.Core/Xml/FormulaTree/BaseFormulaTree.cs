using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class BaseFormulaTree
    {
        #region Implements IXmlFormulaConvertible

        public abstract XmlFormulaTree ToXmlFormula();

        #endregion
    }
}
