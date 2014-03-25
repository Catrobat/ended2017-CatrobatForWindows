using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.FormulaEditor;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class FormulaNodeInfixOperator
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return LeftChild.Tokenize()
                .Concat(Enumerable.Repeat(CreateToken(), 1))
                .Concat(RightChild.Tokenize());
        }

        #endregion

        #region Implements IFormulaSerialization

        internal override void Serialize(StringBuilder sb)
        {
            var leftChild = LeftChild as BaseFormulaTree;
            if (leftChild == null) sb.Append(FormulaSerializer.EmptyChild); else leftChild.Serialize(sb);
            SerializeToken(sb);
            var rightChild = RightChild as BaseFormulaTree;
            if (rightChild == null) sb.Append(FormulaSerializer.EmptyChild); else rightChild.Serialize(sb);
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeAdd
    {
        protected override IFormulaToken CreateToken() 
        {
            return FormulaTokenFactory.CreatePlusToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() + RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("+");
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateAddNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeSubtract
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMinusToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() - RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("-");
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateSubtractNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeMultiply
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateMultiplyToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() * RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("*");
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateMultiplyNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeDivide
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateDivideToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() / RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("/");
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateDivideNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeEquals
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateEqualsToken();
        }

        public override bool EvaluateLogic()
        {
            try
            {
                return Math.Abs(LeftChild.EvaluateNumber() - RightChild.EvaluateNumber()) <= double.Epsilon;
            }
            catch (NotSupportedException)
            {
                return LeftChild.EvaluateLogic() == RightChild.EvaluateLogic();
            }
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("=");
        }

        public override bool IsNumber()
        {
            return IsNumberT2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateEqualsNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeNotEquals
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateNotEqualsToken();
        }

        public override bool EvaluateLogic()
        {
            try
            {
                return Math.Abs(LeftChild.EvaluateNumber() - RightChild.EvaluateNumber()) > double.Epsilon;
            }
            catch (NotSupportedException)
            {
                return LeftChild.EvaluateLogic() != RightChild.EvaluateLogic();
            }
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("≠");
        }

        public override bool IsNumber()
        {
            return IsNumberT2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateNotEqualsNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeGreater
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateGreaterToken();
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateNumber() > RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append(">");
        }

        public override bool IsNumber()
        {
            return IsNumberN2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateGreaterNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeGreaterEqual
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateGreaterEqualToken();
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateNumber() >= RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("≥");
        }

        public override bool IsNumber()
        {
            return IsNumberN2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateGreaterEqualNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeLess
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLessToken();
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateNumber() < RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("<");
        }

        public override bool IsNumber()
        {
            return IsNumberN2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateLessNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeLessEqual
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLessEqualToken();
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateNumber() <= RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append("≤");
        }

        public override bool IsNumber()
        {
            return IsNumberN2L();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateLessEqualNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeAnd
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAndToken();
        }

        public override double EvaluateNumber()
        {
            return ((long) LeftChild.EvaluateNumber()) & ((long) RightChild.EvaluateNumber());
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateLogic() && RightChild.EvaluateLogic();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append(" And ");
        }

        public override bool IsNumber()
        {
            return IsNumberT2T();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateAndNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeOr
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateOrToken();
        }

        public override double EvaluateNumber()
        {
            return ((long)LeftChild.EvaluateNumber()) | ((long)RightChild.EvaluateNumber());
        }

        public override bool EvaluateLogic()
        {
            return LeftChild.EvaluateLogic() || RightChild.EvaluateLogic();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            // TODO: translate
            sb.Append(" Or ");
        }

        public override bool IsNumber()
        {
            return IsNumberT2T();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateOrNode(leftChild, rightChild);
        }
    }

    partial class FormulaNodeMod
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateModToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() % RightChild.EvaluateNumber();
        }

        protected override void SerializeToken(StringBuilder sb)
        {
            sb.Append(" mod ");
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreateModNode(leftChild, rightChild);
        }
    }

    #endregion

}
