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
                    listView.SelectionEnabled = false;
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    listView.SelectionEnabled = false;
                    listView.ReorderEnabled = true;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionEnabled = true;
                    listView.ReorderEnabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void SpriteItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled = !ListViewSprites.SelectionEnabled;

            if (isClickEnabled)
                if (_viewModel.EditSpriteCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditSpriteCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);
        }
    }
}
