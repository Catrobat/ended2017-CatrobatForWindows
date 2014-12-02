using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class ConstantFormulaTree
    {
    }

    #region Implementations

    partial class FormulaNodeNumber
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateNumberNode(Value);
        }
    }

    partial class FormulaNodePi
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreatePiNode();
        }
    }

    partial class FormulaNodeTrue
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateTrueNode();
        }
    }

    partial class FormulaNodeFalse
    {
        protected internal override XmlFormulaTree ToXmlObject2()
        {
            return XmlFormulaTreeFactory.CreateFalseNode();
        }
    }

    #endregion
}
