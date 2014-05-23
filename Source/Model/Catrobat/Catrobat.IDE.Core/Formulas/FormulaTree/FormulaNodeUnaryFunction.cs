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
    abstract partial class FormulaNodeUnaryFunction
    {
        #region Implements IFormulaTokenizer

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(), 1)
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(true), 1))
                .Concat(Child == null ? FormulaTokenizer.EmptyChild : Child.Tokenize())
                .Concat(Enumerable.Repeat(FormulaTokenFactory.CreateParenthesisToken(false), 1));

        }

        #endregion

        #region Implements IStringBuilderSerializable

        public override void Append(StringBuilder sb)
        {
            sb.Append(Serialize());
            sb.Append("(");
            if (Child == null)
            {
                sb.Append(FormulaSerializer.EmptyChild);
            }
            else
            {
                Child.Append(sb);
            }
            sb.Append(")");
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberN1N();
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeExp
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateExpToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Exp(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "exp";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateExpNode(child);
        }
    }
    
    partial class FormulaNodeLog
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLogToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Log10(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "log";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateLogNode(child);
        }
    }

    partial class FormulaNodeLn
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateLnToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Log(Child.EvaluateNumber(), Math.E);
        }

        public override string Serialize()
        {
            return "ln";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateLnNode(child);
        }
    }

    partial class FormulaNodeSin
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateSinToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Sin(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "sin";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateSinNode(child);
        }
    }

    partial class FormulaNodeCos
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateCosToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Cos(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "cos";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateCosNode(child);
        }
    }

    partial class FormulaNodeTan
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateTanToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Tan(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "tan";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateTanNode(child);
        }
    }

    partial class FormulaNodeArcsin
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateArcsinToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Asin(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "arcsin";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArcsinNode(child);
        }
    }

    partial class FormulaNodeArccos
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateArccosToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Acos(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "arccos";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArccosNode(child);
        }
    }

    partial class FormulaNodeArctan
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateArctanToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Atan(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return "arctan";
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateArctanNode(child);
        }
    }

    partial class FormulaNodeAbs
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateAbsToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Abs(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return AppResources.Formula_Function_Abs;
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateAbsNode(child);
        }
    }

    partial class FormulaNodeSqrt
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateSqrtToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Sqrt(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return AppResources.Formula_Function_Sqrt;
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateSqrtNode(child);
        }
    }

    partial class FormulaNodeRound
    {
        protected override IFormulaToken CreateToken()
        {
            return FormulaTokenFactory.CreateRoundToken();
        }

        public override double EvaluateNumber()
        {
            return Math.Round(Child.EvaluateNumber());
        }

        public override string Serialize()
        {
            return AppResources.Formula_Function_Round;
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateRoundNode(child);
        }
    }

    #endregion
}
