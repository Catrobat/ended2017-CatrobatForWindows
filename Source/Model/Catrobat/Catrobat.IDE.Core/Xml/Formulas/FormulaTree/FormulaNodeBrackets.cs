using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeBrackets
    {
    }

    #region Implementations

    partial class FormulaNodeParentheses
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateParenthesesNode(child);
        }
    }

    #endregion
}
