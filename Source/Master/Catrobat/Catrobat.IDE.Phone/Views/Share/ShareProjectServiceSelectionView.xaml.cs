using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Share;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Share
{
    public partial class ShareProjectServiceSelectionView : PhoneApplicationPage
    {
        private readonly ShareProjectServiceSelectionViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ShareProjectServiceSelectionViewModel;

        public ShareProjectServiceSelectionView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}