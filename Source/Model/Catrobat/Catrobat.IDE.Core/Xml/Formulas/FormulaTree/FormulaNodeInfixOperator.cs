using Catrobat.IDE.Core.Xml.Formulas;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeInfixOperator
    {
    }

    #region Implementations

    partial class FormulaNodeAdd
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateAddNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeSubtract
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateSubtractNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeMultiply
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateMultiplyNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeDivide
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateDivideNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodePower
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreatePowerNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeEquals
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateEqualsNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeNotEquals
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateNotEqualsNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeGreater
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateGreaterNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeGreaterEqual
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateGreaterEqualNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeLess
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateLessNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeLessEqual
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateLessEqualNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeAnd
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateAndNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeOr
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateOrNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeModulo
    {
        protected override XmlFormulaTree ToXml(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateModNode(leftChild, rightChild);
        }
    }

    #endregion

}
