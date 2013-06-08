using System;
using System.Windows.Data;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class ActiveThemeBrushConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var settingsViewModel = (App.Current.Resources["Locator"] as ViewModelLocator).Settings;
      var theme = value as Theme;

      if (theme != null && settingsViewModel.ActiveTheme == value)
      {
        return App.Current.Resources["PhoneAccentBrush"];
      }
      else
      {
        return null;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      // Not Needed.
      return null;
    }
  }
}
