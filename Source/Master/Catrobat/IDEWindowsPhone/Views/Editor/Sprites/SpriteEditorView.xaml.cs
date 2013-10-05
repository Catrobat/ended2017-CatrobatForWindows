using System.ComponentModel;
using System.Windows;
using Catrobat.Core.Misc;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Sprites;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Sprites
{
    public partial class SpriteEditorView : PhoneApplicationPage
    {
        private readonly SpriteEditorViewModel _viewModel = ServiceLocator.Current.GetInstance<SpriteEditorViewModel>();

        public SpriteEditorView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
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
