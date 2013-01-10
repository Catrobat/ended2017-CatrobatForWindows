using System;
using System.Windows.Data;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone7.Converters
{
  public class FloatStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return FormatHelper.ConvertFloat((float)value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return FormatHelper.ParseFloat((string)value);
      }
      catch (Exception)
      {
        return parameter;
      }
    }
  }
}
