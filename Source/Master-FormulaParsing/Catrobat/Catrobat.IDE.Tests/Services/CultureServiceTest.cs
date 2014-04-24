using System.Globalization;
using System.Threading;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Tests.Services
{
    public class CultureServiceTest : ICultureService
    {
        public CultureInfo GetCulture()
        {
            return Thread.CurrentThread.CurrentUICulture;
        }

        public void SetCulture(CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
        }
    }
}
