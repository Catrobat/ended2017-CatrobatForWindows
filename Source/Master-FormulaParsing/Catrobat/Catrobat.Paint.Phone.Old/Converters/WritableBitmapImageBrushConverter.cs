using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Catrobat.Paint.Phone.Old.Converters
{
    public class WritableBitmapImageBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? (object) new SolidColorBrush {Color = Colors.Transparent} : new ImageBrush { ImageSource = value as WriteableBitmap };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //not needed!
            throw new NotImplementedException();
        }
    }
}
