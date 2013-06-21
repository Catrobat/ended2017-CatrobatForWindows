using System;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Globalization;
using Catrobat.Core.Resources;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Service;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class OnlineProjectView : PhoneApplicationPage
    {

        private readonly OnlineProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<OnlineProjectViewModel>();

        public OnlineProjectView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();
            base.OnNavigatedFrom(e);
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
            _viewModel.DownloadCommand.Execute(null);
        }

        private void Report_Click(object sender, EventArgs e)
        {
            _viewModel.ReportCommand.Execute(null);
        }

        private void License_Click(object sender, EventArgs e)
        {
            _viewModel.LicenseCommand.Execute(null);
        }
    }
}