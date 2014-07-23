using Catrobat.Data.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    partial class FormulaNodeVariable
    {
    }

    #region Implementations

    partial class FormulaNodeLocalVariable
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateLocalVariableNode(Variable);
        }
    }

    partial class FormulaNodeGlobalVariable
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateGlobalVariableNode(Variable);
        }
    }

    #endregion
}
