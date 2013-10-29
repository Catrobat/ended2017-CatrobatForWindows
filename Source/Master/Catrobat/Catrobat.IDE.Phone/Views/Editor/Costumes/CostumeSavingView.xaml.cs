using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Costumes
{
    public partial class CostumeSavingView : PhoneApplicationPage
    {
        private readonly CostumeSavingViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).CostumeSavingViewModel;

        public CostumeSavingView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
        }
    }
}