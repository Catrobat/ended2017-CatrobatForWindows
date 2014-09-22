using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.WindowsPhone.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class SpritesView
    {
        private readonly SpritesViewModel _viewModel =
            ServiceLocator.ViewModelLocator.SpritesViewModel;

        public SpritesView()
        {
            InitializeComponent();
            PageCacheMode = NavigationCacheMode.Enabled;
        }

        private void MultiModeEditorCommandBar_OnModeChanged(MultiModeEditorCommandBarMode mode)
        {
            var listView = ListViewSprites;

            if (listView == null) return;

            switch (mode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    listView.ReorderEnabled = false;
                    listView.SelectionMode = ListViewSelectionMode.None;
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    listView.SelectionMode = ListViewSelectionMode.None;
                    listView.ReorderEnabled = true;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionMode = ListViewSelectionMode.Multiple;
                    listView.ReorderEnabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void SpriteItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled = /*ListViewSprites.ReorderMode == ListViewReorderMode.Disabled &&*/
                                 ListViewSprites.SelectionMode == ListViewSelectionMode.None;

            if (isClickEnabled)
                if (_viewModel.EditSpriteCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditSpriteCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);
        }
    }
}
