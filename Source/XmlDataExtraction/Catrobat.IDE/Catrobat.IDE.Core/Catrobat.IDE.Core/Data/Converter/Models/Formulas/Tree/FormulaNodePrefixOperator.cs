using Catrobat.Data.Xml.XmlObjects.Formulas;
// ReSharper disable once CheckNamespace


namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodePrefixOperator
    {
    }

    #region Implementations

    partial class FormulaNodeNot
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNotNode(child);
        }
    }

    partial class FormulaNodeNegativeSign
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateNegativeSignNode(child);
        }
    }

    #endregion
}
