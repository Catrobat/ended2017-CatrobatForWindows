using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;

namespace Catrobat.IDE.Core.FormulaEditor
{
    public interface IFormulaTokenizer
    {
        IEnumerable<IFormulaToken> Tokenize();
    }
}
