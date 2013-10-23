using System;
using System.Globalization;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class CultureServiceStore : ICultureService
    {
        public CultureInfo GetCulture()
        {
            return new CultureInfo(Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride);
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