// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    public partial class FormulaTokenParameterSeparator
    {
        #region Implements IStringSerializable

        public override string Serialize()
        {
            return ",";
        }

        #endregion
    }
}
