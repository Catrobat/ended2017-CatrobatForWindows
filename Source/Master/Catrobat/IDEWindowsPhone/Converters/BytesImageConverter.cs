using System;
using System.IO;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Catrobat.Core.Misc.Helpers;
using Catrobat.IDEWindowsPhone.Misc.Storage;

namespace Catrobat.IDEWindowsPhone.Converters
{
  public class BytesImageConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var byteImage = value as Byte[];

      return ImageToBytesHelper.ConvertByteToImage(byteImage);
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      var bitmapImage = value as BitmapImage;

      return ImageToBytesHelper.ConvertImageToBytes(bitmapImage);
    }
  }
}
