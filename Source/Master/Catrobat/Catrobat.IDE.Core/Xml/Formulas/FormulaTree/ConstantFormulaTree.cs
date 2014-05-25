using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class ConstantFormulaTree
    {
    }

    #region Implementations

    partial class FormulaNodeNumber
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateNumberNode(Value);
        }
    }

    partial class FormulaNodePi
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreatePiNode();
        }
    }

    partial class FormulaNodeTrue
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateTrueNode();
        }
    }

    partial class FormulaNodeFalse
    {
        public override XmlFormulaTree ToXml()
        {
            return XmlFormulaTreeFactory.CreateFalseNode();
        }
    }

    #endregion
}
