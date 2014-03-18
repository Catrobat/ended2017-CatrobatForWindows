using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
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
                .Concat(Child.Tokenize())
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

        #region Implements IFormulaSerialization

        protected abstract void SerializeToken(StringBuilder sb, bool isOpening);

        protected override void SerializeToken(StringBuilder sb)
        {
            // not used
            throw new NotImplementedException();
        }

        internal override void Serialize(StringBuilder sb)
        {
            SerializeToken(sb, true);
            sb.Append(Child == null ? " " : Child.Serialize());
            SerializeToken(sb, false);
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

        protected override void SerializeToken(StringBuilder sb, bool isOpening)
        {
            // TODO: translate
            sb.Append(isOpening ? "(" : ")");
        }

        protected override XmlFormulaTree ToXmlFormula(XmlFormulaTree child)
        {
            return XmlFormulaTreeFactory.CreateParenthesesNode(child);
        }
    }

    #endregion
}
