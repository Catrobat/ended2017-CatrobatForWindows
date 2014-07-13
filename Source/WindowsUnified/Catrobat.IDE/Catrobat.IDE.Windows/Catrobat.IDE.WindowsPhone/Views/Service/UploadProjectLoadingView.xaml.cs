using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.Phone.UI.Input;

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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            ProgressBarProgress.IsIndeterminate = false;
        }

        private void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Handled = true;
        }
    }
}
