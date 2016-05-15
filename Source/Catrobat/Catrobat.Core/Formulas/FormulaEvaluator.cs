using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaEvaluator
    {
        public static double EvaluateNumber(FormulaTree formula)
        {
            return formula == null ? 0 : formula.EvaluateNumber();
        }

        public static bool EvaluateLogic(FormulaTree formula)
        {
            return formula != null && formula.EvaluateLogic();
        }

        public static object Evaluate(FormulaTree formula)
        {
            if (formula == null) return null;

            return formula.IsNumber() ? (object)formula.EvaluateNumber() : formula.EvaluateLogic();
        }
    }
}
