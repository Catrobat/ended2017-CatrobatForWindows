using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.Core.CatrobatObjects;
using Windows.UI.Xaml.Controls;
//using Catrobat.IDE.Core.Utilities;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProgramView : ViewPageBase
    {
        private readonly OnlineProgramViewModel _viewModel =
            ServiceLocator.ViewModelLocator.OnlineProgramViewModel;

        public OnlineProgramView()
        {
            InitializeComponent();
            // register event of new download-manager
            //ServiceLocator.WebCommunicationService.DownloadProgressChanged += WebCommunicationService_DownloadProgressChanged;
        }

        //void WebCommunicationService_DownloadProgressChanged(object sender, ProgressEventArgs e)
        //{
        //    ProgressBarDownload.Value = e.Percent;
        //    ButtonStartDownload.Content = e.OperationGuid;
        //}

        private void ViewPageBase_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            _viewModel.OnLoadCommand.Execute((OnlineProgramHeader)DataContext);
        }
    }
}
