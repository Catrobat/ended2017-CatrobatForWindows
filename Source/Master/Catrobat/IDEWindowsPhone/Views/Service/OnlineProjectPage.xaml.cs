using System;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources.Main;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Globalization;
using Catrobat.Core.Resources;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
  public partial class OnlineProjectPage : PhoneApplicationPage
  {
    public OnlineProjectPage()
    {
      InitializeComponent();
    }

    private void LocalizeApplicationBar()
    {
      ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = MainResources.OnlineProjectDownloadButton;
      ((ApplicationBarMenuItem)ApplicationBar.MenuItems[0]).Text = MainResources.OnlineProjectReportButton;
      ((ApplicationBarMenuItem)ApplicationBar.MenuItems[1]).Text = MainResources.OnlineProjectLicenseButton;
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      LocalizeApplicationBar();

      UploadedLabel.Text = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectUploadedBy, ((OnlineProjectHeader)DataContext).Uploaded);
      VersionLabel.Text = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectVersion, ((OnlineProjectHeader)DataContext).Version);
      ViewsLabel.Text = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectViews, ((OnlineProjectHeader)DataContext).Views);
      DownloadsLabel.Text = String.Format(CultureInfo.InvariantCulture, MainResources.OnlineProjectDownloads, ((OnlineProjectHeader)DataContext).Downloads);
      ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
    }

    private void Download_Click(object sender, EventArgs e)
    {
      (sender as ApplicationBarIconButton).IsEnabled = false;
      ServerCommunication.DownloadAndSaveProject(((OnlineProjectHeader)DataContext).DownloadUrl, ((OnlineProjectHeader)DataContext).ProjectName, DownloadCallback);

      MessageBox.Show(MainResources.DownloadQueueMessage,
              MainResources.MessageBoxInformation, MessageBoxButton.OK);

      NavigationService.GoBack();
    }

    private void DownloadCallback(string filename)
    {
      CatrobatContext.GetContext().UpdateLocalProjects();
      CatrobatContext.GetContext().SetCurrentProject(filename);

      Dispatcher.BeginInvoke(() =>
        {
          if(filename == "")
          {
            // TODO: show error message

          }
          else
          {
            if (ServerCommunication.NoDownloadsPending())
            {
              MessageBox.Show(MainResources.NoDownloadsPending,
                MainResources.MessageBoxInformation, MessageBoxButton.OK);
            }
          }
        });
    }

    private void Report_Click(object sender, EventArgs e)
    {
      // TODO: Implement.
    }

    private void License_Click(object sender, EventArgs e)
    {
      WebBrowserTask browser = new WebBrowserTask();
      browser.Uri = new Uri(ApplicationResources.ProjectLicenseUrl);
      browser.Show();
    }
  }
}