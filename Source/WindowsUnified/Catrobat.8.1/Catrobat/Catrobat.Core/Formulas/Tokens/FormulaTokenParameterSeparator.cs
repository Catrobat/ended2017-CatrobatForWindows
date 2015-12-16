// ReSharper disable once CheckNamespace
using Catrobat.IDE.Core.Services;
namespace Catrobat.IDE.Core.Models.Formulas.Tokens
{
    public partial class FormulaTokenParameterSeparator
    {
        public override string Serialize()
        {
            return ServiceLocator.CultureService.GetCulture().TextInfo.ListSeparator;
        }
    }
}
