using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class ProjectImportView : PhoneApplicationPage
  {
    private ProjectImporter _importer;
    private string _importedProjectName;
    private bool _isWorking = false;

    public ProjectImportView()
    {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      Dispatcher.BeginInvoke(() =>
      {
        ContentPanel.Visibility = Visibility.Collapsed;
        LoadingPanel.Visibility = Visibility.Visible;
        CheckBoxMakeActive.IsChecked = true;
        ProgressBarLoading.IsIndeterminate = true;
      });

      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      // TODO: handle goback
      // clear imported projects


      base.OnNavigatedFrom(e);
    }

    private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      _isWorking = true;
      var fileToken = string.Empty;
      if (NavigationContext.QueryString.TryGetValue("fileToken", out fileToken))
      {
        _importer = new ProjectImporter();
        var projectHeader = await _importer.ImportProjects(fileToken);
        _importedProjectName = projectHeader.ProjectName;

        if (projectHeader != null)
        {
          TextBoxProjectName.Text = projectHeader.ProjectName;
          ImageProjectScreenshot.Source = projectHeader.Screenshot as ImageSource;
          ContentPanel.Visibility = Visibility.Visible;
          LoadingPanel.Visibility = Visibility.Collapsed;
          ProgressBarLoading.IsIndeterminate = false;
        }
        else
        {
          ShowErrorMessage();
        }
      }

      _isWorking = false;
    }

    private void ProjectNotValidMessageResult(MessageBoxResult obj)
    {
      NavigationService.Navigate(new Uri("/Views/Main/MainView.xaml", UriKind.Relative));
    }

    private void ButtonAdd_OnClick(object sender, RoutedEventArgs e)
    {
      if (_isWorking)
        return;

      _isWorking = true;

      Dispatcher.BeginInvoke(() =>
      {
        ButtonAdd.IsEnabled = false;
        ButtonCancel.IsEnabled = false;
        ContentPanel.Visibility = Visibility.Collapsed;
        LoadingPanel.Visibility = Visibility.Visible;
        ProgressBarLoading.IsIndeterminate = true;
      });

      var setActive = CheckBoxMakeActive.IsChecked != null && CheckBoxMakeActive.IsChecked.Value;

      var task = Task.Run(() =>
      {
        try
        {
          if (_importer != null)
            _importer.AcceptTempProject(setActive);

          Dispatcher.BeginInvoke(() => NavigationService.Navigate(new Uri("/Views/Main/MainView.xaml", UriKind.Relative)));
        }
        catch
        {
          ShowErrorMessage();
        }
      });

      _isWorking = false;
    }

    private void ShowErrorMessage()
    {
      Dispatcher.BeginInvoke(() =>
      {
        var message = new DialogMessage("Sorry! The project is not valid or not compatible with this version of Catrobat.", ProjectNotValidMessageResult)
        {
          Button = MessageBoxButton.OK,
          Caption = "Project can not be opened"
        };

        Messenger.Default.Send(message);
      });
    }

    private void ButtonDismiss_OnClick(object sender, RoutedEventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/MainView.xaml", UriKind.Relative));
    }
  }
}