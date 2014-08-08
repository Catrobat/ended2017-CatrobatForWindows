using System;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
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

            switch (mode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    listView.SelectionMode = ListViewSelectionMode.None;
                    listView.ReorderMode = ListViewReorderMode.Disabled;
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    listView.SelectionMode = ListViewSelectionMode.None;
                    listView.ReorderMode = ListViewReorderMode.Enabled;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionMode = ListViewSelectionMode.Multiple;
                    listView.ReorderMode = ListViewReorderMode.Disabled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void SpriteItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled = ListViewSprites.ReorderMode == ListViewReorderMode.Disabled &&
                                 ListViewSprites.SelectionMode == ListViewSelectionMode.None;

            if (isClickEnabled)
                if (_viewModel.EditSpriteCommand.CanExecute(((FrameworkElement)sender).DataContext))
                    _viewModel.EditSpriteCommand.Execute(((FrameworkElement)sender).DataContext);
        }
    }
}
