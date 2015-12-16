using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Catrobat.Core.Resources.Localization;
using WinRTXamlToolkit.Controls;

namespace Catrobat.IDE.WindowsShared.Converters.NativeConverters
{
    public class SelectedBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (!(value is bool) || (!(bool)value))
                return new SolidColorBrush(Colors.Black); // TODO: get color for theme resources

            return new SolidColorBrush(Colors.Blue); // TODO: get color for theme resources
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not used
            return null;
        }
    }
}
