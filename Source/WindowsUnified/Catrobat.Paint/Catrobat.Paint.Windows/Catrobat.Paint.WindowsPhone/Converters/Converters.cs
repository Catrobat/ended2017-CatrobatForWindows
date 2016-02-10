using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Catrobat.Paint.WindowsPhone.Converters
{
    public class CameraIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int cam = (int)value;
            switch (cam)
            {
                case 0:
                    return new Uri("ms-appx:/Assets/AppBar/FrontCam.png");
                case 1:
                    return new Uri("ms-appx:/Assets/AppBar/BackCam.png");
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ToolSettingsIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility vis = (Visibility)value;
            if (vis == Visibility.Visible)
                return new Uri("ms-appx:/Assets/AppBar/dark/appbar.cancel.rest.png");
            else
                return new Uri("ms-appx:/Assets/ColorPicker/icon_menu_strokes.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }

    public class ToolSettingsTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility vis = (Visibility)value;
            if (vis == Visibility.Visible)
                return "Schließen";
            else
                return "Pinselstärke";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
