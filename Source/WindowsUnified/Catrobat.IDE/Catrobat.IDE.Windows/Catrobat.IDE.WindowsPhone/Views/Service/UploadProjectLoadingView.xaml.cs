using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.Phone.UI.Input;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProjectLoadingView : Page
    {
        private readonly UploadProjectLoadingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectLoadingViewModel;

        public UploadProjectLoadingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
            ProgressBarProgress.IsIndeterminate = true;
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            HardwareButtons.BackPressed -= HardwareButtons_BackPressed;
            ProgressBarProgress.IsIndeterminate = false;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Handled = true;
        }
    }
}
