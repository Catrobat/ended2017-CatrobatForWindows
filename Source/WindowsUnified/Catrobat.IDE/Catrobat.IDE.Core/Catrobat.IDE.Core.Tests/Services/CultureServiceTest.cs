using System.Globalization;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Core.Tests.Services
{
    public class CultureServiceTest : ICultureService
    {
        private CultureInfo _culture;

        #region Implements ICultureService

        public CultureInfo GetBestCulture()
        {
            return new CultureInfo("en");
        }


        public CultureInfo GetCulture()
        {
            return _culture;
        }

        public void SetCulture(CultureInfo culture)
        {
            _culture = culture;
            AppResources.Culture = culture;
        }

        #endregion
    }
}
