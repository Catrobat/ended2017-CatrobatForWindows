using System;
using System.Windows.Data;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Controls.Buttons;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class BoolPlayButtonStateConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        //var isPlaying = (bool)value;

        //return isPlaying ? PlayButtonState.Play : PlayButtonState.Pause;

        return null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      // Not Needed.
      return null;
    }
  }
}
