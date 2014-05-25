using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodePrefixOperator
    {
    }

    #region Implementations

    partial class FormulaNodeNot
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNotNode(child);
        }
    }

    partial class FormulaNodeNegativeSign
    {
        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNegativeSignNode(child);
        }
    }

    #endregion
}
