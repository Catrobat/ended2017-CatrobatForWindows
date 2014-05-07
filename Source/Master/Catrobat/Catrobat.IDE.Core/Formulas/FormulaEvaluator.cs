using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaEvaluator
    {
        public static double EvaluateNumber(IFormulaTree formula)
        {
            return formula == null ? 0 : formula.EvaluateNumber();
        }

        public static bool EvaluateLogic(IFormulaTree formula)
        {
            return formula != null && formula.EvaluateLogic();
        }

        public static object Evaluate(IFormulaTree formula)
        {
            if (formula == null) return null;

            return formula.IsNumber() ? (object)formula.EvaluateNumber() : formula.EvaluateLogic();
        }
    }
}
