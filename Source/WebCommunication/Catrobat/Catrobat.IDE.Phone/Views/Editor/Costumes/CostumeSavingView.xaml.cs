using System.ComponentModel;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }
    }
}