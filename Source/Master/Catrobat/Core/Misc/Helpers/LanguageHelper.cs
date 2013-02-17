using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Threading;

namespace Catrobat.Core.Misc.Helpers
{
  public class LanguageHelper
  {
    private static ICulture _culture;

    public static void SetICulture(ICulture culture)
    {
      _culture = culture;
    }

    private static string[] supportedLanguageCodes = 
    { 
      "DE","EN"
    };

    private static ObservableCollection<CultureInfo> supportedLanguages;
    public static ObservableCollection<CultureInfo> SupportedLanguages
    {
      get
      {
        if (supportedLanguages == null)
        {
          supportedLanguages = new ObservableCollection<CultureInfo>();

          foreach(string languageCode in supportedLanguageCodes)
          {
            CultureInfo culture = new CultureInfo(languageCode);
            if (culture.IsNeutralCulture)
            {
              supportedLanguages.Add(culture);
            }
          }
        }

        return supportedLanguages;
      }
    }

    public static string GetCurrentCultureLanguageCode()
    {
      return _culture.Get2LetterCultureColde();
    }
  }
}
