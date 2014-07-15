using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Scripts
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