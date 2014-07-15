using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProjectLoadingView : ViewPageBase
    {
        private readonly UploadProjectLoadingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProjectLoadingViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public UploadProjectLoadingView()
        {
            InitializeComponent();
        }
    }
}
