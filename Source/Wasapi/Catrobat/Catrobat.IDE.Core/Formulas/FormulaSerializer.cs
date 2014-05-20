using System.Text;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaSerializer
    {
        public const string EmptyChild = " ";

        public static string Serialize(IFormulaTree formula)
        {
            if (formula == null) return string.Empty;

            var sb = new StringBuilder();
            formula.Append(sb);
            return sb.ToString();
        }

        public static string Serialize(IFormulaToken token)
        {
            if (token == null) return string.Empty;

            return token.Serialize();
        }
    }
}
