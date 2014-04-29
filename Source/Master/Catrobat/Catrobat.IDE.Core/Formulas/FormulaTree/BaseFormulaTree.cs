using System;
using System.Collections.Generic;
using System.Text;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaTree
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

        #region Implements IFormulaInterpreter

        public abstract bool IsNumber();

        #endregion

        #region Implements IXmlFormulaConvertible

        public abstract XmlFormulaTree ToXmlFormula();

        #endregion

        #region Implements IStringBuilderSerializable

        public abstract void Append(StringBuilder sb);

        #endregion

        #region Implements IStringSerializable

        public abstract string Serialize();

        #endregion
    }
}
