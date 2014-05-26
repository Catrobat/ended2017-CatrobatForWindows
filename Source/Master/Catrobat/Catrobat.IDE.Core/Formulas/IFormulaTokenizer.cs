using System.Collections.Generic;
using Catrobat.IDE.Core.Models.Formulas.Tokens;

namespace Catrobat.IDE.Core.Formulas
{
    public interface IFormulaTokenizer
    {
        IEnumerable<IFormulaToken> Tokenize();
    }
}
