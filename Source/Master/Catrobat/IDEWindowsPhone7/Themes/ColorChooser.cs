using System.Windows;
using System.Windows.Media;

namespace Catrobat.IDEWindowsPhone7.Themes
{
  public class ColorChooser
  {
    private static bool dark = ((Visibility)Application.Current.Resources["PhoneDarkThemeVisibility"] == Visibility.Visible);
    private static Color darkColor = Colors.White;
    private static Color lightColor = Colors.Black;

    public SolidColorBrush WhiteDarkColor
    {
      get
      {
        if (dark)
          return new SolidColorBrush(darkColor);
        else
          return new SolidColorBrush(lightColor);
      }
    }
  }
}
