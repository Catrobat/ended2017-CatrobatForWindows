using System.Collections.Generic;

namespace Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken
{
    public abstract partial class FormulaTokenBrackets : IFormulaToken
    {
        public List<IFormulaToken> Children { get; set; } 
    }

    #region Imeplementations

    public partial class FormulaTokenParentheses : FormulaTokenBrackets
    {
    }

    #endregion
}
