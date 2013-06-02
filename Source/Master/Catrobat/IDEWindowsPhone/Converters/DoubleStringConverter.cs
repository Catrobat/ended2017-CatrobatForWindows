using System;
using System.Windows.Data;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class DoubleStringConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (parameter != null)
      {
        return ((double) value).ToString(parameter as String);
      }
      else
      {
        return FormatHelper.ConvertDouble((double)value);
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return FormatHelper.ParseDouble((string)value);
      }
      catch (Exception)
      {
        return parameter;
      }
    }
  }
}
