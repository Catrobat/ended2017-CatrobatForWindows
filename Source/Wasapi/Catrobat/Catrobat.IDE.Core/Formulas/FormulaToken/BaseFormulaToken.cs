// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    public abstract partial class BaseFormulaToken
    {
        #region Implements IStringSerializable

        public abstract string Serialize();

        #endregion
    }
}
