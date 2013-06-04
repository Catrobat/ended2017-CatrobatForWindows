using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
  public partial class PlayerLauncherView : PhoneApplicationPage
  {
    public static bool IsNavigateBack = true;

    public PlayerLauncherView()
    {
      InitializeComponent();
      Loaded += OnLoaded;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      if (IsNavigateBack)
        NavigationService.GoBack();

      base.OnNavigatedTo(e);
    }

    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
      IsNavigateBack = true;
      base.OnNavigatedFrom(e);
    }

    private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
    {
      var projectName = string.Empty;
      if (NavigationContext.QueryString.TryGetValue("ProjectName", out projectName))
      {
        if (!await PlayerLauncher.LaunchPlayer(projectName))
        {
          var message = new DialogMessage("No project with the name '" + projectName + "' was found.", ProjectNotFoundMessageResult)
          {
            Button = MessageBoxButton.OK,
            Caption = "Project not found"
          };

          Messenger.Default.Send(message);
        }
      }
    }

    private void ProjectNotFoundMessageResult(MessageBoxResult result)
    {
      NavigationService.GoBack();
    }
  }
}