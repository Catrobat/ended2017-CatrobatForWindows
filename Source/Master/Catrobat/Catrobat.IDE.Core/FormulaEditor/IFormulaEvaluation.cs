namespace Catrobat.IDE.Core.FormulaEditor
{
    public interface IFormulaEvaluation
    {
        bool EvaluateLogic();

        double EvaluateNumber();
    }
}
