using System.Globalization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Phone.Services
{
    public class CultureServicePhone : ICultureService
    {
        public CultureInfo GetCulture()
        {
            return CultureInfo.CurrentUICulture;
        }

        public void SetCulture(CultureInfo culture)
        {
            CultureInfo.DefaultThreadCurrentUICulture = culture;
        }
    }
}