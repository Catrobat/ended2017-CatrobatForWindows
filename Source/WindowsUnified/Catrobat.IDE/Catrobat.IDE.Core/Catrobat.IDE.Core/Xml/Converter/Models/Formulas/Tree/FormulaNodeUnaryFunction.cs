using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodeUnaryFunction
    {
    }

    #region Implementations

    partial class FormulaNodeExp
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateExpNode(child);
        }
    }
    
    partial class FormulaNodeLog
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateLogNode(child);
        }
    }

    partial class FormulaNodeLn
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateLnNode(child);
        }
    }

    partial class FormulaNodeSin
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateSinNode(child);
        }
    }

    partial class FormulaNodeCos
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateCosNode(child);
        }
    }

    partial class FormulaNodeTan
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateTanNode(child);
        }
    }

    partial class FormulaNodeArcsin
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArcsinNode(child);
        }
    }

    partial class FormulaNodeArccos
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArccosNode(child);
        }
    }

    partial class FormulaNodeArctan
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArctanNode(child);
        }
    }

    partial class FormulaNodeAbs
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateAbsNode(child);
        }
    }

    partial class FormulaNodeSqrt
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateSqrtNode(child);
        }
    }

    partial class FormulaNodeRound
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateRoundNode(child);
        }
    }

    #endregion
}
