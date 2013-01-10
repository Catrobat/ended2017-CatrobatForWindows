using System;
using System.Windows.Data;
using Catrobat.Core.Objects.Costumes;
using Catrobat.IDEWindowsPhone7.ViewModel;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone7.Converters
{
  public class IntCostumeReferenceConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
      try
      {
        return ServiceLocator.Current.GetInstance<EditorViewModel>().Costumes.IndexOf((Costume)value);
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
        return ServiceLocator.Current.GetInstance<EditorViewModel>().Costumes[(int)value];
      }
      catch (Exception e)
      {
        return null;
      }
    }
  }
}
