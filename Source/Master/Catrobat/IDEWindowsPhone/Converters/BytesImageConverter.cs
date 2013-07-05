using System;
using System.Globalization;
using System.Windows.Data;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class BytesImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;

            //var byteImage = value as Byte[];

            //return ImageFormatHelper.ConvertByteToImage(byteImage);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;

            //var bitmapImage = value as BitmapImage;

            //return ImageFormatHelper.ConvertImageToBytes(bitmapImage);
        }
    }
}