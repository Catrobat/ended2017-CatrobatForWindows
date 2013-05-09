using System;
using System.Windows.Data;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone.Converters
{
  /// <summary>
  /// Replaces {0} through the current application version
  /// </summary>
  public class VersionConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      return String.Format((string)value, StaticApplicationSettings.CurrentApplicationVersion.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      // not needed
      return null;
    }
  }
}
