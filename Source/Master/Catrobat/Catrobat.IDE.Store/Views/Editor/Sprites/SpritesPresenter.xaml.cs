using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpritesPresenter : Page
    {
        private readonly SpritesViewModel _spritesViewModel = ServiceLocator.GetInstance<SpritesViewModel>();
        private readonly SpriteEditorViewModel _spriteEditorViewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();

        public SpritesPresenter()
        {
            this.InitializeComponent();
            _spritesViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertyHelper.GetPropertyName(() => _spritesViewModel.SelectedSprite))
                AppBarBottomn.IsOpen = _spritesViewModel.SelectedSprite != null;
        }

        private void FlyoutNew_OnOpen(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void RadioButtonTabs_Click(object sender, RoutedEventArgs e)
        {
            var radBtn = sender as RadioButton;

            if (radBtn != null)
            {
                int index = Convert.ToInt32(radBtn.Tag);

                FlipViewTabs.SelectedIndex = index;
            }
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
                _spriteEditorViewModel.SelectedActions.Count - e.RemovedItems.Count > 0 ||
                _spriteEditorViewModel.SelectedCostumes.Count > 0 ||
                _spriteEditorViewModel.SelectedSounds.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }

        private void GridViewLooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool showAppbar =
                _spriteEditorViewModel.SelectedActions.Count > 0 ||
                _spriteEditorViewModel.SelectedCostumes.Count - e.RemovedItems.Count > 0 ||
                _spriteEditorViewModel.SelectedSounds.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }

        private void GridViewSounds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool showAppbar =
                _spriteEditorViewModel.SelectedActions.Count > 0 ||
                _spriteEditorViewModel.SelectedCostumes.Count > 0 ||
                _spriteEditorViewModel.SelectedSounds.Count - e.RemovedItems.Count > 0 ||
                e.AddedItems.Count > 0;

            AppBarBottomn.IsOpen = showAppbar;
        }

        private void TileGeneratorFlyout_OnOpened(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ChangeCostumeViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void NewAction_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ScriptBrickCategoryViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void ButtonGoBack_OnClick(object sender, RoutedEventArgs e)
        {
            // TODO: maybe reset ViewModel

            ServiceLocator.NavigationService.NavigateBack();
        }
    }
}
