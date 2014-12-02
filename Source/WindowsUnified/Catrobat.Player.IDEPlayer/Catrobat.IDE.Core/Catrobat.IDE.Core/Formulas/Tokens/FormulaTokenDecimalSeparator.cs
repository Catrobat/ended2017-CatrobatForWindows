using Catrobat.IDE.Core.Services;

// ReSharper disable once CheckNamespace
namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    public partial class FormulaTokenDecimalSeparator
    {
        public override string Serialize()
        {
            return ServiceLocator.CultureService.GetCulture().NumberFormat.NumberDecimalSeparator;
        }
    }
}
