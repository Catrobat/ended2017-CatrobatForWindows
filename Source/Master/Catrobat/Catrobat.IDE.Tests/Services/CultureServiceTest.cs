using System.Globalization;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Tests.Services
{
    public class CultureServiceTest : ICultureService
    {
        private CultureInfo _culture;

        #region Implements ICultureService

        public CultureInfo GetCulture()
        {
            return _culture;
        }

        public void SetCulture(CultureInfo culture)
        {
            _culture = culture;
        }

        #endregion
    }
}
