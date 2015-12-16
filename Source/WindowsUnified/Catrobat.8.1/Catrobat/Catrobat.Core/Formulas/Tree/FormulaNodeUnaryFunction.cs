using Catrobat.Core.Resources.Localization;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
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
            return AppResourcesHelper.Get("Formula_Function_Abs");
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
            return AppResourcesHelper.Get("Formula_Function_Sqrt");
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
            return AppResourcesHelper.Get("Formula_Function_Round");
        }
    }

    #endregion
}
