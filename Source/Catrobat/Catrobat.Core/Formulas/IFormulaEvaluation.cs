namespace Catrobat.IDE.Core.Formulas
{
    public interface IFormulaEvaluation
    {
        bool EvaluateLogic();

        double EvaluateNumber();
    }
}
