using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    partial class FormulaNodeVariable
    {
    }

    #region Implementations

    partial class FormulaNodeLocalVariable
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateLocalVariableNode(Variable);
        }
    }

    partial class FormulaNodeGlobalVariable
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateGlobalVariableNode(Variable);
        }
    }

    #endregion
}
