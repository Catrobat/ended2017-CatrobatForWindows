using System.Text;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;

namespace Catrobat.IDE.Core.Formulas
{
    public class FormulaSerializer
    {
        public const string EmptyChild = " ";

        public static string Serialize(FormulaTree formula)
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
