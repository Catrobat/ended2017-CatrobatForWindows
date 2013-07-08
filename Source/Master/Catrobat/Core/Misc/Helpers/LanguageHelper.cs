using System.Collections.ObjectModel;
using System.Globalization;

namespace Catrobat.Core.Misc.Helpers
{
    public static class LanguageHelper
    {
        private static ICulture _culture;

        public static void SetICulture(ICulture culture)
        {
            _culture = culture;
        }

        private static readonly string[] SupportedLanguageCodes =
        {
            "DE", "EN"
        };

        private static ObservableCollection<CultureInfo> _supportedLanguages;

        public static ObservableCollection<CultureInfo> SupportedLanguages
        {
            get
            {
                if (_supportedLanguages == null)
                {
                    _supportedLanguages = new ObservableCollection<CultureInfo>();

                    foreach (string languageCode in SupportedLanguageCodes)
                    {
                        var culture = new CultureInfo(languageCode);
                        if (culture.IsNeutralCulture)
                        {
                            _supportedLanguages.Add(culture);
                        }
                    }
                }

                return _supportedLanguages;
            }
        }

        public static string GetCurrentCultureLanguageCode()
        {
            return _culture.GetToLetterCultureColde();
        }
    }
}