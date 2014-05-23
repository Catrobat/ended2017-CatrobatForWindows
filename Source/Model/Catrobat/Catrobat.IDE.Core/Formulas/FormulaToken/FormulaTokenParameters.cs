using System;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    internal abstract partial class FormulaTokenParameter
    {
        #region Implements IStringSerializable

        public override string Serialize()
        {
            // Not used
            throw new NotImplementedException();
        }

        #endregion
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
