using System.ComponentModel;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Service;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Service
{
    public partial class UploadProjectLoadingView : PhoneApplicationPage
    {
        private readonly UploadProjectLoadingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectLoadingViewModel;

        public UploadProjectLoadingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = true;
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = false;
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}