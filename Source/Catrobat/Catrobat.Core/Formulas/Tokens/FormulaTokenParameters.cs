using System;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    internal abstract partial class FormulaTokenParameter
    {
        public override string Serialize()
        {
            // Not used
            throw new NotImplementedException();
        }
    }

    #region Implementations

    internal partial class FormulaTokenUnaryParameter
    {
    }

    internal partial class FormulaTokenBinaryParameter
    {
    }
    
    #endregion
}
