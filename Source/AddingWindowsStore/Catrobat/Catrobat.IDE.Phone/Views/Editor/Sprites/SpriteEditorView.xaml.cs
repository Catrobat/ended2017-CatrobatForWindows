using System.Windows;
using Catrobat.IDE.Phone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Sprites
{
    public partial class SpriteEditorView : PhoneApplicationPage
    {
        private readonly SpriteEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<SpriteEditorViewModel>();

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
