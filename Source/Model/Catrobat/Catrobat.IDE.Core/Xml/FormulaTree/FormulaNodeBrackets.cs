using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeBrackets
    {
    }

    #region Implementations

    partial class FormulaNodeParentheses
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateParenthesesNode(child);
        }
    }

    #endregion
}
