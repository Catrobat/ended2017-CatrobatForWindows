using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
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
