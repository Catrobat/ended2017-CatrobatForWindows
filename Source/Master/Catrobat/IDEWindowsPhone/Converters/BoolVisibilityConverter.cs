using System;
using System.Windows.Data;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class BoolVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return ((bool) value) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      // Not Needed.
      return null;
    }
  }
}
