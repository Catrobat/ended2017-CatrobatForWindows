using System.Collections.Generic;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;

namespace Catrobat.IDE.Core.Formulas
{
    public interface IFormulaTokenizer
    {
        IEnumerable<IFormulaToken> Tokenize();
    }
}
