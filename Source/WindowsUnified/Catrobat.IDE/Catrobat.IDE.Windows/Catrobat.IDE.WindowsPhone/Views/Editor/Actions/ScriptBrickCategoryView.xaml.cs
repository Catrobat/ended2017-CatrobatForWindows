using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Actions;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Actions
{
    public partial class ScriptBrickCategoryView
    {
        private readonly ScriptBrickCategoryViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ScriptBrickCategoryViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public ScriptBrickCategoryView()
        {
            InitializeComponent();
        }
    }
}