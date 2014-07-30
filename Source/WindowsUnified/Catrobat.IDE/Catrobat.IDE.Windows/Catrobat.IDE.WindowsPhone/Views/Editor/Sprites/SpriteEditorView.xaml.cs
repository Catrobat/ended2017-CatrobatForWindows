using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Windows.UI.Xaml;
using Catrobat.IDE.WindowsPhone.Controls;
using Catrobat.IDE.WindowsShared.Services;

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
            InitializeComponent();

            CreateCommandBars();

            BottomAppBar = _commandBarActions;
            //CommandBarMain = (CommandBar)Resources["AppBarActions"];
        }

        private void CreateCommandBars()
        {
            _commandBarActions = new MultiModeEditorCommandBar
            {
                CopyCommand = _viewModel.CopyScriptBrickCommand,
                DeleteCommand = _viewModel.DeleteScriptBrickCommand,
                NewCommand = _viewModel.AddNewScriptBrickCommand,
                PlayCommand = _viewModel.StartPlayerCommand,
            };
            _commandBarActions.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;

            _commandBarLooks = new MultiModeEditorCommandBar
            {
                CopyCommand = _viewModel.CopyCostumeCommand,
                DeleteCommand = _viewModel.DeleteCostumeCommand,
                NewCommand = _viewModel.AddNewCostumeCommand,
                PlayCommand = _viewModel.StartPlayerCommand
            };
            _commandBarLooks.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;

            _commandBarSounds = new MultiModeEditorCommandBar
            {
                CopyCommand = _viewModel.CopySoundCommand,
                DeleteCommand = _viewModel.DeleteSoundCommand,
                NewCommand = _viewModel.AddNewSoundCommand,
                PlayCommand = _viewModel.StartPlayerCommand
            };
            _commandBarSounds.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //((SoundPlayerServiceWindowsShared) ServiceLocator.SoundPlayerService).
            //    SetMediaElement(MediaElementSound);

            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            //ServiceLocator.SoundPlayerService.Clear();

            base.OnNavigatedFrom(e);
        }

        //private void PlayPauseButtonSound_OnPlayStateChanged(PlayPauseButton button, 
        //    PlayPauseButtonState state)
        //{
        //    var sound = (Sound)button.DataContext;

        //    if (state == PlayPauseButtonState.Play)
        //    {
        //        _viewModel.PlaySoundCommand.Execute(sound);
        //    }
        //    else
        //    {
        //        _viewModel.StopSoundCommand.Execute(sound);
        //    }
        //}

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
            ListView listView = null;

            if (PivotMain.SelectedItem == PivotActions)
                listView = (ListView)this.FindName("ListViewActions");
            else if (PivotMain.SelectedItem == PivotLooks)
                listView = (ListView)this.FindName("ListViewLooks");
            else if (PivotMain.SelectedItem == PivotSounds)
                listView = (ListView)this.FindName("ListViewSounds");

            Debug.Assert(listView != null, "listView != null");

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
    }
}
