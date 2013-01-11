using System.Windows.Controls;
using Catrobat.IDEWindowsPhone7.Themes;
using IDEWindowsPhone7;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone7.Views.Main
{
  public partial class SettingsThemeChooser : PhoneApplicationPage
  {
    public SettingsThemeChooser()
    {
      InitializeComponent();
      lbxDesings.SelectedItem = (App.Current.Resources["ThemeChooser"] as ThemeChooser).SelectedTheme;
    }

    private void lbxDesings_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      (App.Current.Resources["ThemeChooser"] as ThemeChooser).SelectedTheme = ((sender as ListBox).SelectedItem as Theme);
    }

    private void lbxDesings_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.GoBack();
    }
  }
}