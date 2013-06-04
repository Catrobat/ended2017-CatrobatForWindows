using Catrobat.Core.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class ScreenshotConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      if (value != null)
        return value;
      else
      {
        BitmapImage image = new BitmapImage();
        using (var loader = ResourceLoader.CreateResourceLoader())
        {
          var stream = loader.OpenResourceStream(ResourceScope.IdePhone, "Content/Images/Screenshot/NoScreenshot.png");
          image.SetSource(stream);
        }
        return image;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}
