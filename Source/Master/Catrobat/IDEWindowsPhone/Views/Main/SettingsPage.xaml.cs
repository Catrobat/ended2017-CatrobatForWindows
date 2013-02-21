using System;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDEWindowsPhone.Themes;
using IDEWindowsPhone;
using KBB.Mobile.Controls;
using Microsoft.Phone.Controls;
using System.Diagnostics;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class SettingsPage : PhoneApplicationPage
  {
    private MemoryMonitor _memoryMonitor;

    public SettingsPage()
    {
      InitializeComponent();

      #region DEBUG
      checkBoxShowMemory.Visibility = Debugger.IsAttached ? Visibility.Visible : Visibility.Collapsed;
      #endregion
    }

    private void stackPanelSettingsLanguage_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/SettingsLanguageChooser.xaml", UriKind.Relative));
    }

    private void stackPanelSettingsDesign_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/SettingsThemeChooser.xaml", UriKind.Relative));
    }

    private void stackPanelSettingsBricks_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/SettingsBrickCategories.xaml", UriKind.Relative));
    }

    #region DEBUG

    private void checkBoxShowMemory_Checked(object sender, RoutedEventArgs e)
    {
      if (_memoryMonitor == null)
      {
        _memoryMonitor = new MemoryMonitor(true, true);
      }
      else
      {
        _memoryMonitor.ShowVisualization = true;
      }
    }

    private void checkBoxShowMemory_Unchecked(object sender, RoutedEventArgs e)
    {
      if (_memoryMonitor != null)
      {
        _memoryMonitor.ShowVisualization = false;
      }
    }

    #endregion
  }
}