using System;
using System.Windows.Data;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Objects;

namespace Catrobat.IDEWindowsPhone7.Converters
{
  public class ActiveProjectVisibilityConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      ProjectHeader header = value as ProjectHeader;

      if (header.ProjectName == CatrobatContext.Instance.CurrentProject.ProjectName)
      {
        CatrobatContext.Instance.CurrentProject.Header = header;
        return Visibility.Visible;
      }
      else
      {
        return Visibility.Collapsed;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      // Not Needed.
      return null;
    }
  }
}
