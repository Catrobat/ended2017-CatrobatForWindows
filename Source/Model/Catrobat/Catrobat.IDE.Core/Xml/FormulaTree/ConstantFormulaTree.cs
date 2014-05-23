using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class ConstantFormulaTree
    {
    }

    #region Implementations

    partial class FormulaNodeNumber
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateNumberNode(Value);
        }
    }

    partial class FormulaNodePi
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreatePiNode();
        }
    }

    partial class FormulaNodeTrue
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateTrueNode();
        }
    }

    partial class FormulaNodeFalse
    {
        public override XmlFormulaTree ToXmlFormula()
        {
            return XmlFormulaTreeFactory.CreateFalseNode();
        }
    }

    #endregion

}
