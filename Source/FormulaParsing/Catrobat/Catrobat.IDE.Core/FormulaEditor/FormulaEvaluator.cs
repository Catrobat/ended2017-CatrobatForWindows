using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.FormulaEditor
{
    class FormulaEvaluator
    {
        public double EvaluateNumber(IFormulaTree formula)
        {
            return formula == null ? 0 : formula.EvaluateNumber();
        }

        public bool EvaluateLogic(IFormulaTree formula)
        {
            return formula != null && formula.EvaluateLogic();
        }

        public object Evaluate(IFormulaTree formula)
        {
            if (formula == null) return null;

            try
            {
                return formula.EvaluateNumber();
            }
            catch (NotSupportedException)
            {
                return formula.EvaluateNumber();
            }
        }
    }
}
