using System;
using System.Collections.Generic;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree
{
    abstract partial class BaseFormulaTree
    {
        #region Implements IFormulaTokenizer

        protected abstract IFormulaToken CreateToken();

        public abstract IEnumerable<IFormulaToken> Tokenize();

        #endregion

        #region Implements IFormulaEvaluation

        public virtual bool EvaluateLogic()
        {
            throw new NotSupportedException();
        }

        public virtual double EvaluateNumber()
        {
            throw new NotSupportedException();
        }

        #endregion

        #region Implements IFormulaSerialization

        protected abstract void SerializeToken(StringBuilder sb);

        internal abstract void Serialize(StringBuilder sb);

        public string Serialize()
        {
            var sb = new StringBuilder();
            Serialize(sb);
            return sb.ToString();
        }

        #endregion

        #region Implements IFormulaInterpreter

        public abstract void ClearChildren();

        public abstract bool IsNumber();

        #endregion

        #region Implements IXmlFormulaConvertible

        public abstract XmlFormulaTree ToXmlFormula();

        #endregion
    }
}
