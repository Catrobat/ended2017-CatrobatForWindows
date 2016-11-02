using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Catrobat.IDE.Core.UI.Converters
{
  public class BoolToVisibilityInvertedConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, string language)
    {
      if (value != null)
      {
        return ((bool)value)
          ? Visibility.Collapsed
          : Visibility.Visible;
      }

      return Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
      throw new NotImplementedException();
    }
  }
}
