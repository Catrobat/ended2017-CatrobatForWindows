using System;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Objects;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Globalization;

namespace Catrobat.IDEWindowsPhone7.Views.Main
{
  public partial class OnlineProjectPage : PhoneApplicationPage
  {
    public OnlineProjectPage()
    {
      InitializeComponent();
    }

    private void LocalizeApplicationBar()
    {
      ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).Text = MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectDownloadButton;
      ((ApplicationBarMenuItem)ApplicationBar.MenuItems[0]).Text = MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectReportButton;
      ((ApplicationBarMenuItem)ApplicationBar.MenuItems[1]).Text = MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectLicenseButton;
    }

    private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
    {
      LocalizeApplicationBar();

      UploadedLabel.Text = String.Format(CultureInfo.InvariantCulture, MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectUploadedBy, ((OnlineProjectHeader)DataContext).Uploaded);
      VersionLabel.Text = String.Format(CultureInfo.InvariantCulture, MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectVersion, ((OnlineProjectHeader)DataContext).Version);
      ViewsLabel.Text = String.Format(CultureInfo.InvariantCulture, MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectViews, ((OnlineProjectHeader)DataContext).Views);
      DownloadsLabel.Text = String.Format(CultureInfo.InvariantCulture, MetroCatIDE.Content.Resources.Main.MainResources.OnlineProjectDownloads, ((OnlineProjectHeader)DataContext).Downloads);
      ((ApplicationBarIconButton)ApplicationBar.Buttons[0]).IsEnabled = true;
    }

    private void Download_Click(object sender, EventArgs e)
    {
      (sender as ApplicationBarIconButton).IsEnabled = false;
      ServerCommunication.downloadAndSaveProject(((OnlineProjectHeader)DataContext).DownloadUrl, downloadCallback);

      MessageBox.Show(MetroCatIDE.Content.Resources.Main.MainResources.DownloadQueueMessage,
              MetroCatIDE.Content.Resources.Main.MainResources.MessageBoxInformation, MessageBoxButton.OK);

      NavigationService.GoBack();
    }

    private void downloadCallback(string filename)
    {
      CatrobatContext.Instance.UpdateLocalProjects();
      CatrobatContext.Instance.SetCurrentProject(filename);

      Dispatcher.BeginInvoke(() =>
        {
          if (ServerCommunication.NoDownloadsPending())
          {
            MessageBox.Show(MetroCatIDE.Content.Resources.Main.MainResources.NoDownloadsPending,
              MetroCatIDE.Content.Resources.Main.MainResources.MessageBoxInformation, MessageBoxButton.OK);
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
      browser.Uri = new Uri(MetroCatIDE.Content.Resources.ApplicationResources.ProjectLicenseUrl);
      browser.Show();
    }
  }
}