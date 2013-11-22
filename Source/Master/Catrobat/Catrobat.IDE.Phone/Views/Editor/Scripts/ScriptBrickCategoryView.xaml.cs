using System.ComponentModel;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Scripts
{
    public partial class ScriptBrickCategoryView : PhoneApplicationPage
    {
        private readonly ScriptBrickCategoryViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ScriptBrickCategoryViewModel;

        public ScriptBrickCategoryView()
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