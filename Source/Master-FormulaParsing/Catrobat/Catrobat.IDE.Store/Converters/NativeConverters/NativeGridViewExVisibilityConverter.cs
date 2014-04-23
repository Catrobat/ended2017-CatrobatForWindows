using System;
using Windows.UI.Xaml;

namespace Catrobat.IDE.Store.Converters.NativeConverters
{
    public class NativeGridViewExVisibilityConverter
    {
        public NativeGridViewExVisibilityConverter()
        {
            Opposite = false;
        }

        public bool Opposite { get; set; }

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var not = object.Equals(parameter, "not") || Opposite;
            if (value is bool && targetType == typeof(Visibility))
            {
                return ((bool)value) != not ? Visibility.Visible : Visibility.Collapsed;
            }
            if (value is Visibility && targetType.GetType() == typeof(Boolean))
            {
                return (((Visibility)value) == Visibility.Visible) != not;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

        #endregion
    }
}
