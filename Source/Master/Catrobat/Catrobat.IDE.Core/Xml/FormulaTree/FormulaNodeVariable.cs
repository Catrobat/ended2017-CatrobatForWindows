using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    partial class FormulaNodeVariable
    {
    }

    #region Implementations

    partial class FormulaNodeLocalVariable
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateLocalVariableNode(Variable);
        }
    }

    partial class FormulaNodeGlobalVariable
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateGlobalVariableNode(Variable);
        }
    }

    #endregion
}
