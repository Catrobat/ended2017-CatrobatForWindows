using System.Globalization;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Windows.Globalization;
using Catrobat.IDE.Core.UI.PortableUI;
using Catrobat.IDE.Core.ViewModels;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class CultureServiceWindowsShared : ICultureService
    {

        public CultureInfo GetBestCulture()
        {
            // TODO: check available cultures and return the best choice

            return new CultureInfo(ApplicationLanguages.Languages[0]);
        }

        public CultureInfo GetCulture()
        {
            return new CultureInfo(ApplicationLanguages.PrimaryLanguageOverride);
        }

        public void SetCulture(CultureInfo culture)
        {
            ApplicationLanguages.PrimaryLanguageOverride = culture.TwoLetterISOLanguageName;
        }
    }
}