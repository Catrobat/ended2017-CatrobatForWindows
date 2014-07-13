using System.Globalization;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Windows.Globalization;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class CultureServiceWindowsShared : ICultureService
    {
        public CultureInfo GetCulture()
        {
            return new CultureInfo(Windows.Globalization.ApplicationLanguages.Languages[0]);
        }

        public void SetCulture(CultureInfo culture)
        {
            Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = culture.TwoLetterISOLanguageName;
            var frame = Window.Current.Content as Frame;

            if (frame != null) 
                if (frame.Content != null) 
                    frame.Navigate(frame.Content.GetType());
        }
    }
}