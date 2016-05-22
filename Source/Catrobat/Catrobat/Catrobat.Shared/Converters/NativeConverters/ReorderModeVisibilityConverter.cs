using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Catrobat.IDE.WindowsShared.Converters.NativeConverters
{
    public class ReorderModeVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var reorderModel = (ListViewReorderMode)value;

            return reorderModel == ListViewReorderMode.Disabled ? 
                Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            // not used
            return null;
        }
    }
}
