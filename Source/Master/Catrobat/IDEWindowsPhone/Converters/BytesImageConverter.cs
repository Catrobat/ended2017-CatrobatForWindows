using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Catrobat.Core.Misc.Helpers;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class BytesImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var byteImage = value as Byte[];

      if (byteImage == null)
        return null;

      using (var ms = new MemoryStream(byteImage))
      {
        var bi = new BitmapImage
          {
            CreateOptions = BitmapCreateOptions.None
          };

        bi.SetSource(ms);

        return bi;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var bitmapImage = value as BitmapImage;

      if (bitmapImage == null)
        return null;

      var encoding = parameter as string;

      switch(encoding)
      {
        case "png":
          throw new NotImplementedException();

        default:
          using (var ms = new MemoryStream())
          {
            var btmMap = new WriteableBitmap(bitmapImage.PixelWidth, bitmapImage.PixelHeight);

            btmMap.SaveJpeg(ms, bitmapImage.PixelWidth, bitmapImage.PixelHeight, 0, 100);

            return ms.ToArray();
          }
      }
    }
  }
}
