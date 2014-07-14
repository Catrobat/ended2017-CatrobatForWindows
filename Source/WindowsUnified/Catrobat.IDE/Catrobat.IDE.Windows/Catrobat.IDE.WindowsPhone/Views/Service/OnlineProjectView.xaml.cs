using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Service;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Service
{
    public partial class OnlineProjectView : ViewPageBase
    {
        private readonly OnlineProjectViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).OnlineProjectViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public OnlineProjectView()
        {
            InitializeComponent();
        }
    }
}
