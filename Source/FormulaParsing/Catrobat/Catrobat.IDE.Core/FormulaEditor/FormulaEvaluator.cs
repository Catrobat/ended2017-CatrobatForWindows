using System;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Services;

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

            return formula.IsNumber() ? (object) formula.EvaluateNumber() : formula.EvaluateLogic();
        }
    }
}
