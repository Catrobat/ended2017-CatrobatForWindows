using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using Catrobat.Core;
using Catrobat.Core.Objects;

namespace Catrobat.IDEWindowsPhone.Converters
{
    public class InActiveProjectVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var header = value as ProjectHeader;

            if (header != null && header.ProjectName != CatrobatContext.GetContext().CurrentProject.ProjectName)
            {
                CatrobatContext.GetContext().CurrentProject.Header = header;
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Not Needed.
            return null;
        }
    }
}