using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Windows.UI.Xaml;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class SpriteEditorView
    {
        private readonly SpriteEditorViewModel _viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).SpriteEditorViewModel;

        

        public SpriteEditorView()
        {
            InitializeComponent();
        }

        private void reorderListBoxScriptBricks_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedBrick != null)
            {
                ReorderListBoxScriptBricks.ScrollIntoView(_viewModel.SelectedBrick);
                _viewModel.SelectedBrick = null;
            }
        }
    }
}
