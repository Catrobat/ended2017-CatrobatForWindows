using System;
using System.Globalization;
using System.Windows.Data;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class IntSoundInfoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ServiceLocator.Current.GetInstance<EditorViewModel>().Sounds.IndexOf((Sound) value);
            }
            catch
            {
                return 0;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return ServiceLocator.Current.GetInstance<EditorViewModel>().Sounds[(int) value];
            }
            catch
            {
                return null;
            }
        }
    }
}