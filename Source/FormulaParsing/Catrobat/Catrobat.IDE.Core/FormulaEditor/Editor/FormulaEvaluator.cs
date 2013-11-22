using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using System;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    class FormulaEvaluator
    {

        public object Evaluate(IFormulaTree formula)
        {
            if (formula == null) return null;

            var type = formula.GetType();
            if (type == typeof(FormulaNodeNumber)) return Evaluate((FormulaNodeNumber)formula);
            if (type == typeof(FormulaNodePi)) return Evaluate((FormulaNodePi)formula);
            if (type == typeof(FormulaNodeTrue)) return Evaluate((FormulaNodeTrue)formula);
            if (type == typeof(FormulaNodeFalse)) return Evaluate((FormulaNodeFalse)formula);
            if (type == typeof(FormulaNodeParentheses)) return Evaluate((FormulaNodeParentheses)formula);
            throw new NotImplementedException();
        }

        #region numbers

        private double Evaluate(FormulaNodeNumber formula)
        {
            return formula.Value;
        }

        private double Evaluate(FormulaNodePi formula)
        {
            return Math.PI;
        }

        #endregion

        #region arithmetic


        #endregion

        #region relational operators

        private bool Evaluate(FormulaNodeEquals formula)
        {
            throw new NotImplementedException();
        }

        private bool Evaluate(FormulaNodeLess formula, out bool result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region logic

        private bool Evaluate(FormulaNodeTrue formula)
        {
            return true;
        }

        private bool Evaluate(FormulaNodeFalse formula)
        {
            return false;
        }

        #endregion

        #region min/max


        #endregion

        #region exponential function and logarithms


        #endregion

        #region trigonometric functions

 
        #endregion

        #region miscellaneous functions


        #endregion

        #region sensors



        #endregion

        #region object variables


        #endregion

        #region user variables

        private object Evaluate(FormulaNodeUserVariable formula)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region brackets

        private object Evaluate(FormulaNodeParentheses formula)
        {
            return Evaluate(formula.Child);
        }

        #endregion

    }
}
