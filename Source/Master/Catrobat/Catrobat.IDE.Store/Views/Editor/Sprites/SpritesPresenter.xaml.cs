using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.ViewModel.Editor.Costumes;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Catrobat.IDE.Core.ViewModel.Editor.Sounds;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Store.Views.Editor.Sounds;

namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpritesPresenter : Page
    {
        private readonly SpritesViewModel _spritesViewModel = ServiceLocator.GetInstance<SpritesViewModel>();
        private readonly SpriteEditorViewModel _spriteEditorViewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();
        private readonly FrameworkElement _appBarObjects;
        private readonly FrameworkElement _appBarActions;
        private readonly FrameworkElement _appBarLooks;
        private readonly FrameworkElement _appBarSounds;

        public SpritesPresenter()
        {
            this.InitializeComponent();

            if (ItemsControlAppBars.Items != null)
            {
                _appBarObjects = (FrameworkElement) ItemsControlAppBars.Items[0];
                _appBarActions = (FrameworkElement)ItemsControlAppBars.Items[1];
                _appBarLooks = (FrameworkElement)ItemsControlAppBars.Items[2];
                _appBarSounds = (FrameworkElement)ItemsControlAppBars.Items[3];
            }

            _spritesViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            //if (propertyChangedEventArgs.PropertyName ==
            //    PropertyHelper.GetPropertyName(() => _spritesViewModel.SelectedSprite))
            //{
            //    AppBarBottomn.IsOpen = _spritesViewModel.SelectedSprite != null;
            //}
        }

        private void RadioButtonTabs_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                int index = Convert.ToInt32(radioButton.Tag);
                FlipViewTabs.SelectedIndex = index;
            }
        }

        private void NewSpriteFlyout_OnOpen(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<AddNewSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void FlyoutChangeSprite_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<ChangeSpriteViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void NewLookFlyout_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<NewCostumeSourceSelectionViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }

        private void NewSoundFlyout_OnOpening(object sender, object e)
        {
            var viewModel = ServiceLocator.GetInstance<NewSoundSourceSelectionViewModel>();
            viewModel.NavigationObject = (Flyout)sender;
        }


        private void GridViewActions_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppBarBottomn.IsOpen = GridViewActions.SelectedItems.Count -
                e.RemovedItems.Count + e.AddedItems.Count > 0;

            if (ItemsControlAppBars.Items != null)
            {
                ItemsControlAppBars.Items.Remove(_appBarActions);
                ItemsControlAppBars.Items.Remove(_appBarLooks);
                ItemsControlAppBars.Items.Remove(_appBarSounds);
                ItemsControlAppBars.Items.Add(_appBarActions);
            }
        }

        private void GridViewLooks_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppBarBottomn.IsOpen = _spriteEditorViewModel.SelectedCostumes.Count - 
                e.RemovedItems.Count + e.AddedItems.Count > 0;

            if (ItemsControlAppBars.Items != null)
            {
                ItemsControlAppBars.Items.Remove(_appBarActions);
                ItemsControlAppBars.Items.Remove(_appBarLooks);
                ItemsControlAppBars.Items.Remove(_appBarSounds);
                ItemsControlAppBars.Items.Add(_appBarLooks);
            }
        }

        private void GridViewSounds_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AppBarBottomn.IsOpen = GridViewSounds.SelectedItems.Count -
                e.RemovedItems.Count + e.AddedItems.Count > 0;

            if (ItemsControlAppBars.Items != null)
            {
                ItemsControlAppBars.Items.Remove(_appBarActions);
                ItemsControlAppBars.Items.Remove(_appBarLooks);
                ItemsControlAppBars.Items.Remove(_appBarSounds);
                ItemsControlAppBars.Items.Add(_appBarSounds);
            }
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (_spritesViewModel.SelectedSprite == null && _spriteEditorViewModel.Sprites.Count > 0)
                _spritesViewModel.SelectedSprite = _spriteEditorViewModel.Sprites[0];

            base.OnNavigatedTo(e);
        }

        private void GridViewSprites_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0 && e.AddedItems.Count == 0)
                _spritesViewModel.SelectedSprite = (Sprite)e.RemovedItems[0];
        }

        //#region Drag operations

        //private void GridViewLooks_OnDragItemsStarting(object sender, DragItemsStartingEventArgs e)
        //{
        //    _spriteEditorViewModel.SelectedCostumes = new ObservableCollection<Costume>();

        //    foreach (Costume item in e.Items)
        //    {
        //        _spriteEditorViewModel.SelectedCostumes.Add(item);
        //    }
        //}

        //private void GridBasket_OnDragEnter(object sender, DragEventArgs e)
        //{
        //    GridDelete.Background = new SolidColorBrush(Colors.Green);
        //}

        //private void GridBasket_OnDragLeave(object sender, DragEventArgs e)
        //{
        //    GridDelete.Background = new SolidColorBrush(Colors.DarkSlateGray);
        //}

        //private void GridBasket_OnDrop(object sender, DragEventArgs e)
        //{
        //    var f = e.Data.GetView().AvailableFormats;

        //    GridDelete.Background = new SolidColorBrush(Colors.DarkSlateGray);

        //    switch (FlipViewTabs.SelectedIndex)
        //    {
        //        case 0:
        //            _spriteEditorViewModel.DeleteScriptBrickCommand.Execute(null);
        //            break;

        //        case 1:

        //            _spriteEditorViewModel.DeleteCostumeCommand.Execute(null);
        //            break;

        //        case 2:
        //            _spriteEditorViewModel.DeleteSoundCommand.Execute(null);
        //            break;
        //    }
        //}

        //#endregion

        private void FlipViewTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppBarBottomn == null)
                return;

            switch (((FlipView)sender).SelectedIndex)
            {
                case 0:
                    RadioButtonActions.IsChecked = true;
                    AppBarBottomn.IsOpen = _spriteEditorViewModel.SelectedActions.Count > 0;
                    break;

                case 1:
                    RadioButtonLooks.IsChecked = true;
                    AppBarBottomn.IsOpen = _spriteEditorViewModel.SelectedCostumes.Count > 0;
                    break;

                case 2:
                    RadioButtonSounds.IsChecked = true;
                    AppBarBottomn.IsOpen = _spriteEditorViewModel.SelectedSounds.Count > 0;
                    break;
            }
        }
    }
}
