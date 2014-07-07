using Catrobat.IDE.Core.Models.Formulas.Tokens;

namespace Catrobat.IDE.Core.Models.Formulas.Tree
{
    public partial interface IFormulaOperator : IFormulaToken
    {
        /// Defines operator precedence like * before +
        int Order { get; }
    }
}
