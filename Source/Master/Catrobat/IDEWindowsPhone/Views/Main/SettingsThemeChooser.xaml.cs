using System.Windows.Controls;
using Catrobat.IDEWindowsPhone.Themes;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Views.Main
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