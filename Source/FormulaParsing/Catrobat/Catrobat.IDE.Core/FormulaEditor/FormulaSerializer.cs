using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public class FormulaSerializer
    {
        public static string EmptyChild = " ";

        public string Serialize(IFormulaTree formula)
        {
            return formula == null ? string.Empty : formula.Serialize();
        }
    }
}
