using System.Collections.ObjectModel;
using System.Globalization;

namespace Catrobat.Core.Services.Common
{
    public static class LanguageHelper
    {
        private static ObservableCollection<CultureInfo> _supportedLanguages;

        public static ObservableCollection<CultureInfo> SupportedLanguages
        {
            get
            {
                if (_supportedLanguages == null)
                {
                    _supportedLanguages = new ObservableCollection<CultureInfo>();

                    foreach (string languageCode in Constants.SupportedLanguageCodes)
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
    }
}