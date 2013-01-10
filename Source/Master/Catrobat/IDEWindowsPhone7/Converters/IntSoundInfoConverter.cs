using System;
using System.Windows.Data;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDEWindowsPhone7.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone7.Converters
{
  public class IntSoundInfoConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return ServiceLocator.Current.GetInstance<EditorViewModel>().Sounds.IndexOf((Sound)value);
      }
      catch (Exception e)
      {
        return 0;
      }
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return ServiceLocator.Current.GetInstance<EditorViewModel>().Sounds[(int)value];
      }
      catch (Exception e)
      {
        return null;
      }
    }
  }
}
