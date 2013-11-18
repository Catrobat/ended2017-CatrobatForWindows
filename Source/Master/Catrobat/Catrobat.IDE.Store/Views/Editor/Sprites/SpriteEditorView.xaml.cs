using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Store.Controls.ListView;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpriteEditorView : Page
    {
        readonly SpriteEditorViewModel _viewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();

        public SpriteEditorView()
        {
            this.InitializeComponent();
        }

        private void FlyoutChangeName_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ChangeSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void NewLookFlyout_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<NewCostumeSourceSelectionViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void GridViewActions_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool showAppbar =
                _viewModel.SelectedActions.Count - e.RemovedItems.Count > 0 ||
                _viewModel.SelectedCostumes.Count > 0 ||
                _viewModel.SelectedSounds.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }

        private void GridViewLooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool showAppbar =
                _viewModel.SelectedActions.Count > 0 ||
                _viewModel.SelectedCostumes.Count - e.RemovedItems.Count > 0 ||
                _viewModel.SelectedSounds.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }

        private void GridViewSounds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool showAppbar =
                _viewModel.SelectedActions.Count > 0 ||
                _viewModel.SelectedCostumes.Count > 0 ||
                _viewModel.SelectedSounds.Count - e.RemovedItems.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }
    }
}
