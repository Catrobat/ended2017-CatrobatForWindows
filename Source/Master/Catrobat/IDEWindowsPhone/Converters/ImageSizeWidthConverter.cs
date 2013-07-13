using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Costumes;
using Catrobat.IDEWindowsPhone.ViewModel.Settings;
using IDEWindowsPhone;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class ImageSizeWidthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 0;

            if (!(value is ImageSize))
                return 0;

            var imageSize = (ImageSize)value;
            var imageDimention = parameter as ImageDimention;

            if (imageDimention == null)
                return 0;


            int imageWith = imageDimention.Width;
            int imageHeight = imageDimention.Height;

            int newWidth = ImageSizeEntry.GetNewImageWidth(imageSize, imageWith, imageHeight);

            return newWidth;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}