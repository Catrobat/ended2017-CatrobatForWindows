using System;
using System.Windows.Data;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class SecondStringMillisecondConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return FormatHelper.ConvertDouble(((int)value) / 1000f);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return (int)Math.Round(FormatHelper.ParseDouble((string)value) * 1000);
      }
      catch (Exception)
      {
        return parameter;
      }
    }
  }
}
