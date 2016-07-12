using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class UploadProgramLoadingView : ViewPageBase
    {
        private readonly UploadProgramLoadingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).UploadProgramLoadingViewModel;

        

        public UploadProgramLoadingView()
        {
            InitializeComponent();
        }
    }
}
