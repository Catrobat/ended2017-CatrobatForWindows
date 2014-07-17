using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Annotations;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Utilities.Helpers;
using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;
using Catrobat.IDE.Core.ViewModels.Editor.Sounds;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using GalaSoft.MvvmLight.Messaging;


namespace Catrobat.IDE.Store.Views.Editor.Sprites
{
    public sealed partial class SpritesPresenter : Page, INotifyPropertyChanged
    {
        //private readonly ObservableDictionary _localViewModel = new ObservableDictionary();

        //public ObservableDictionary LocalViewModel
        //{
        //    get { return this._localViewModel; }
        //}

        public double ActionsHeight
        {
            get { return Window.Current.Bounds.Height - 350; }
        }

        public double ActionsInnerHeight
        {
            get { return ActionsHeight - 20; }
        }

        private readonly SpritesViewModel _spritesViewModel = ServiceLocator.GetInstance<SpritesViewModel>();
        private readonly SpriteEditorViewModel _spriteEditorViewModel = ServiceLocator.GetInstance<SpriteEditorViewModel>();
        private readonly FrameworkElement _appBarObjects;
        private readonly FrameworkElement _appBarActions;
        private readonly FrameworkElement _appBarLooks;
        private readonly FrameworkElement _appBarSounds;

        public SpritesPresenter()
        {
            this.InitializeComponent();

            FlipViewTabs.SelectedIndex = _spriteEditorViewModel.SelectedTabIndex;

            Window.Current.SizeChanged += WindowOnSizeChanged;

            if (ItemsControlAppBars.Items != null)
            {
                _appBarObjects = (FrameworkElement)ItemsControlAppBars.Items[0];
                _appBarActions = (FrameworkElement)ItemsControlAppBars.Items[1];
                _appBarLooks = (FrameworkElement)ItemsControlAppBars.Items[2];
                _appBarSounds = (FrameworkElement)ItemsControlAppBars.Items[3];
            }
            _spriteEditorViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }


        private void WindowOnSizeChanged(object sender, WindowSizeChangedEventArgs windowSizeChangedEventArgs)
        {
            // TODO: update size of GridView
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName ==
                PropertyHelper.GetPropertyName(() => _spriteEditorViewModel.SelectedSprite))
            {
                UpdateDataContext();
            }
        }

        private void UpdateDataContext()
        {
            if (_spriteEditorViewModel.SelectedSprite != null)
            {
                var source = new CollectionViewSource();
                source.Source = _spriteEditorViewModel.SelectedSprite.Scripts.Scripts;
                source.ItemsPath = new PropertyPath("Bricks.Bricks");
                source.IsSourceGrouped = true;
                GridViewActions.DataContext = source;
            }
        }

        private void RadioButtonTabs_Click(object sender, RoutedEventArgs e)
        {
            var radioButton = sender as RadioButton;

            if (radioButton != null)
            {
                int index = Convert.ToInt32(radioButton.Tag);
                _spriteEditorViewModel.SelectedTabIndex = index;
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
            if ((_spritesViewModel.SelectedSprite == null && _spriteEditorViewModel.Sprites.Count > 0) ||
                (_spritesViewModel.SelectedSprite != null && !_spriteEditorViewModel.Sprites.Contains(_spritesViewModel.SelectedSprite)
                && _spriteEditorViewModel.Sprites.Count > 0))
            {
                var message = new GenericMessage<Sprite>(_spriteEditorViewModel.Sprites[0]);
                Messenger.Default.Send(message, ViewModelMessagingToken.CurrentSpriteChangedListener);
            }

            base.OnNavigatedTo(e);
        }

        private void GridViewSprites_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.RemovedItems.Count > 0 && e.AddedItems.Count == 0)
                _spritesViewModel.SelectedSprite = (Sprite)e.RemovedItems[0];
        }

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



        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]

        public void RaisePropertyChanged<T>(Expression<Func<T>> selector)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyHelper.GetPropertyName(selector)));
            }
        }
        #endregion

        // creates new group in the data source, if end-user drags item to the new group placeholder
        private void MyGridView_BeforeDrop(object sender, GridViewSamples.Controls.BeforeDropItemsEventArgs e)
        {
            if (e.RequestCreateNewGroup)
            {
                //// create new group and re-assign datasource 
                //Group group = Group.GetNewGroup();
                //_groups.Insert(e.NewGroupIndex, group);
                //UpdateDataContext();
            }
        }

        // removes empty groups (except the last one)
        private void MyGridView_Drop(object sender, DragEventArgs e)
        {
            //bool needReset = false;
            //for (int i = _groups.Count - 1; i >= 0; i--)
            //{
            //    if (_groups[i].Items.Count == 0 && _groups.Count > 1)
            //    {
            //        _groups.RemoveAt(i);
            //        needReset = true;
            //    }
            //}
            //if (needReset)
            //{
            //    UpdateDataContext();
            //}
        }
    }
}
