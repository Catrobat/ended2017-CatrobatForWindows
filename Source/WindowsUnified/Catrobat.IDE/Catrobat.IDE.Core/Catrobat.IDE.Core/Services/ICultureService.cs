using System.Globalization;

namespace Catrobat.IDE.Core.Services
{
    public interface ICultureService
    {
        CultureInfo GetBestCulture();

        CultureInfo GetCulture();

        void SetCulture(CultureInfo culture);
    }
}