using Catrobat.IDE.Core.Services;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.FormulaToken
{
    public partial class FormulaTokenDecimalSeparator
    {
        #region Implements IStringSerializable

        public override string Serialize()
        {
            return ServiceLocator.CultureService.GetCulture().NumberFormat.NumberDecimalSeparator;
        }

        #endregion
    }
}
