using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    abstract partial class FormulaNodeBrackets
    {
        #region Implements IFormulaTokenizer

        protected abstract IFormulaToken CreateToken(bool isOpening);

        protected override IFormulaToken CreateToken()
        {
            // not used
            throw new NotImplementedException();
        }

        public override IEnumerable<IFormulaToken> Tokenize()
        {
            return Enumerable.Repeat(CreateToken(true), 1)
                .Concat(Child == null ? FormulaTokenizer.EmptyChild : Child.Tokenize())
                .Concat(Enumerable.Repeat(CreateToken(false), 1));
        }

        #endregion

        #region Implements IFormulaEvaluation

        public override double EvaluateNumber()
        {
            return Child.EvaluateNumber();
        }

        public override bool EvaluateLogic()
        {
            return Child.EvaluateLogic();
        }

        #endregion

        #region Implements IStringSerializable

        /// <remarks>
        /// The corresponding tokens can be found here: <see cref="FormulaTokenBracket"/>. 
        /// </remarks>
        public override string Serialize()
        {
            // not used
            throw new NotImplementedException();
        }

        #endregion

        #region Implements IFormulaInterpreter

        public override bool IsNumber()
        {
            return IsNumberT1T();
        }

        #endregion
    }

    #region Implementations

    partial class FormulaNodeParentheses
    {
        protected override IFormulaToken CreateToken(bool isOpening)
        {
            return FormulaTokenFactory.CreateParenthesisToken(isOpening);
        }

        public override void Append(StringBuilder sb)
        {
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
    }

    #endregion
}
