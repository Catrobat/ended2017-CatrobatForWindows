using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using KBB.Mobile.Controls;
using Microsoft.Phone.Controls;
using System;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class MainView : PhoneApplicationPage
  {
    private readonly MainViewModel _mainViewModel = ServiceLocator.Current.GetInstance<MainViewModel>();

    private MemoryMonitor _memoryMonitor;

    public MainView()
    {
      InitializeComponent();

      #region DEBUG

      if (Debugger.IsAttached)
        checkBoxShowMemory.Visibility = Visibility.Visible;
      else
        checkBoxShowMemory.Visibility = Visibility.Collapsed;

      #endregion

      // Dirty but there is no way around this
      Messenger.Default.Register<DialogMessage>(
        this,
        msg => Dispatcher.BeginInvoke(() =>
          {
            var result = MessageBox.Show(
              msg.Content,
              msg.Caption,
              msg.Button);

            if (msg.Callback != null)
            {
              // Send callback
              msg.ProcessCallback(result);
            }
          }));
    }

    private void buttonPlayCurrentProject_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/MetroCatPlayer;component/GamePage.xaml", UriKind.Relative));
    }

    private void buttonEditCurrentProject_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Editor/EditorView.xaml", UriKind.Relative));
    }

    private void buttonCreateNewProject_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/AddNewProject.xaml", UriKind.Relative));
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

    private void OnlineProject_Tap(object sender, System.Windows.Input.GestureEventArgs e)
    {
      if (OnlineProjectListBox.SelectedItem != null)
      {
        NavigationService.Navigate(new Uri("/Views/Main/OnlineProjectPage.xaml", UriKind.Relative));
      }
    }

    private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      // Hack - needed because it won't update immediately without it!
      (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
    }

    private void panoramaMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
      if ((panoramaMain.SelectedItem == panoramaItemOnlineProjects) && (OnlineProjectListBox.Items.Count == 0))
      {
        // Load Data - this has to stay in code-behind
        _mainViewModel.LoadOnlineProjects(false);
      }
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

    private void buttonUploadCurrentProject_Click(object sender, RoutedEventArgs e)
    {
      // Determine which page to open
      ServerCommunication.checkToken(CatrobatContext.Instance.CurrentToken, CheckTokenEvent);
    }

    private void CheckTokenEvent(bool registered)
    {
      if (registered)
      {
        Action action = () => NavigationService.Navigate(new Uri("/Views/Main/UploadProjectPage.xaml", UriKind.Relative));
        Dispatcher.BeginInvoke(action);
      }
      else
      {
        Action action = () => NavigationService.Navigate(new Uri("/Views/Main/UploadProjectLoginPage.xaml", UriKind.Relative));
        Dispatcher.BeginInvoke(action);
      }
    }
  }
}