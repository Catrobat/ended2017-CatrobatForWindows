using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Resources.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
{
    abstract partial class FormulaNodeInfixOperator
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return (LeftChild == null ? FormulaTokenizer.EmptyChild : LeftChild.Tokenize())
                .Concat(Enumerable.Repeat(CreateToken(), 1))
                .Concat(RightChild == null ? FormulaTokenizer.EmptyChild : RightChild.Tokenize());
        }

        #endregion

        #region Implements IStringBuilderSerializable

        public override void Append(StringBuilder sb)
        {
            if (LeftChild == null)
            {
                sb.Append(FormulaSerializer.EmptyChild);
            }
            else
            {
                LeftChild.Append(sb);
            }
            AppendToken(sb);
            if (RightChild == null)
            {
                sb.Append(FormulaSerializer.EmptyChild);
            }
            else
            {
                RightChild.Append(sb);
            }
        }

        protected virtual void AppendToken(StringBuilder sb)
        {
            sb.Append(Serialize());
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

        public override string Serialize()
        {
            return "+";
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

        public override string Serialize()
        {
            return "-";
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

        public override string Serialize()
        {
            return "*";
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

        public override string Serialize()
        {
            return "/";
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

    partial class FormulaNodePower
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateCaretToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Pow(LeftChild.EvaluateNumber(), RightChild.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "^";
        }

        public override bool IsNumber()
        {
            return IsNumberN2N();
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree leftChild, XmlFormulaTree rightChild)
        {
            return XmlFormulaTreeFactory.CreatePowerNode(leftChild, rightChild);
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

        public override string Serialize()
        {
            return "=";
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

        public override string Serialize()
        {
            return "≠";
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

        public override string Serialize()
        {
            return ">";
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

        public override string Serialize()
        {
            return "≥";
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

        public override string Serialize()
        {
            return "<";
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

        public override string Serialize()
        {
            return "≤";
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

        protected override void AppendToken(StringBuilder sb)
        {
            sb.Append(" ");
            base.AppendToken(sb);
            sb.Append(" ");
        }

        public override string Serialize()
        {
            return AppResources.Formula_Operator_And;
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

        protected override void AppendToken(StringBuilder sb)
        {
            sb.Append(" ");
            base.AppendToken(sb);
            sb.Append(" ");
        }

        public override string Serialize()
        {
            return AppResources.Formula_Operator_Or;
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

    partial class FormulaNodeModulo
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateModToken();
        }

        public override double EvaluateNumber()
        {
            return LeftChild.EvaluateNumber() % RightChild.EvaluateNumber();
        }

        protected override void AppendToken(StringBuilder sb)
        {
            sb.Append(" ");
            base.AppendToken(sb);
            sb.Append(" ");
        }

        public override string Serialize()
        {
            return AppResources.Formula_Operator_Mod;
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
