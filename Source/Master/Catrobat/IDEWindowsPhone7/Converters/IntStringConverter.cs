using System;
using System.Windows.Data;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone7.Converters
{
  public class IntStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return FormatHelper.ConvertInt((int)value);      
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return FormatHelper.ParseInt((string)value);
      }
      catch (Exception)
      {
        return parameter;
      }
    }
  }
}
