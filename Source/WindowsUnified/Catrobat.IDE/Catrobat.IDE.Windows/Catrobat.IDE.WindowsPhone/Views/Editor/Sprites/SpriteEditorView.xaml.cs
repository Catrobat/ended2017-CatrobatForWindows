using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Windows.UI.Xaml;
using Catrobat.IDE.WindowsPhone.Controls;
using Catrobat.IDE.WindowsPhone.Controls.SoundControls;
using Catrobat.IDE.WindowsShared.Services;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls;
using System.Collections;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Sprites
{
    public partial class SpriteEditorView
    {
        private readonly SpriteEditorViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.SpriteEditorViewModel;

        private MultiModeEditorCommandBar _commandBarActions;
        private MultiModeEditorCommandBar _commandBarLooks;
        private MultiModeEditorCommandBar _commandBarSounds;


        public SpriteEditorView()
        {
            PageCacheMode = NavigationCacheMode.Enabled;
            InitializeComponent();

            CreateCommandBars();

            BottomAppBar = _commandBarActions;
            //CommandBarMain = (CommandBar)Resources["AppBarActions"];
        }

        private void CreateCommandBars()
        {
            _commandBarActions = new MultiModeEditorCommandBar
            {
                TargetType = AppBarTargetType.Action,
                CopyCommand = _viewModel.CopyScriptBrickCommand,
                DeleteCommand = _viewModel.DeleteScriptBrickCommand,
                NewCommand = _viewModel.AddNewScriptBrickCommand,
                PlayCommand = _viewModel.StartPlayerCommand
                
            };
            _commandBarActions.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;

            _commandBarLooks = new MultiModeEditorCommandBar
            {
                TargetType = AppBarTargetType.Look,
                CopyCommand = _viewModel.CopyLookCommand,
                DeleteCommand = _viewModel.DeleteLookCommand,
                NewCommand = _viewModel.AddNewLookCommand,
                PlayCommand = _viewModel.StartPlayerCommand
            };
            _commandBarLooks.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;

            _commandBarSounds = new MultiModeEditorCommandBar
            {
                TargetType = AppBarTargetType.Sound,
                CopyCommand = _viewModel.CopySoundCommand,
                DeleteCommand = _viewModel.DeleteSoundCommand,
                NewCommand = _viewModel.AddNewSoundCommand,
                PlayCommand = _viewModel.StartPlayerCommand
            };
            _commandBarSounds.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var playSoundButtonGroup = PlayPauseButtonGroupSounds;
            if (playSoundButtonGroup != null) playSoundButtonGroup.Stop();

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
        }

        //private void reorderListBoxScriptBricks_Loaded(object sender, RoutedEventArgs e)
        //{
        //    if (_viewModel.SelectedBrick != null)
        //    {
        //        ReorderListBoxScriptBricks.ScrollIntoView(_viewModel.SelectedBrick);
        //        _viewModel.SelectedBrick = null;
        //    }
        //}
        private void Pivot_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(PivotActions))
                BottomAppBar = _commandBarActions;
            else if (e.AddedItems.Contains(PivotLooks))
                BottomAppBar = _commandBarLooks;
            else if (e.AddedItems.Contains(PivotSounds))
                BottomAppBar = _commandBarSounds;

        }

        private void MultiModeEditorCommandBar_OnModeChanged(MultiModeEditorCommandBarMode mode)
        {
            CatrobatListView listView = null;

            if (PivotMain.SelectedItem == PivotActions)
                listView = ListViewActions;
            else
                if (PivotMain.SelectedItem == PivotLooks)
                    listView = ListViewLooks;    
                else if (PivotMain.SelectedItem == PivotSounds)
                    listView = ListViewSounds; 

            Debug.Assert(listView != null, "listView != null");

            switch (mode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    listView.SelectionMode = ListViewSelectionMode.None;
                    //listView.ReorderMode = ListViewReorderMode.Disabled;
                    (listView.SelectedItems as IList).Clear();
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    //listView.SelectionMode = ListViewSelectionMode.None;
                    //listView.ReorderMode = ListViewReorderMode.Enabled;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionMode = ListViewSelectionMode.Multiple;
                    //listView.ReorderMode = ListViewReorderMode.Disabled;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void LookItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled =/* ListViewLooks.ReorderEnabled == ListViewReorderMode.Disabled &&
                                 */ListViewLooks.SelectionMode == ListViewSelectionMode.None;
            if (isClickEnabled)
            {
                if (_viewModel.EditLookCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditLookCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);

            }
        }

        private void SoundItem_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var isClickEnabled = /*ListViewSounds.ReorderMode == ListViewReorderMode.Disabled &&*/
                                 ListViewSounds.SelectionMode == ListViewSelectionMode.None;

            if (isClickEnabled)
                if (_viewModel.EditSoundCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditSoundCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);
        }
    }
}
