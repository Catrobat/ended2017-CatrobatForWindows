using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Catrobat.IDE.WindowsPhone.Controls;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls;
using Catrobat.IDE.WindowsPhone.Controls.ListsViewControls.CatrobatListView;

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
                PlayCommand = _viewModel.StartPlayerCommand,
                DataContext = _viewModel
            };
            _commandBarActions.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;
            _commandBarActions.SetBinding(MultiModeEditorCommandBar.ModeProperty, 
                new Binding { Path = new PropertyPath("ActionsCommandBarMode"), Mode = BindingMode.TwoWay });



            _commandBarLooks = new MultiModeEditorCommandBar
            {
                TargetType = AppBarTargetType.Look,
                CopyCommand = _viewModel.CopyLookCommand,
                DeleteCommand = _viewModel.DeleteLookCommand,
                NewCommand = _viewModel.AddNewLookCommand,
                PlayCommand = _viewModel.StartPlayerCommand,
                DataContext = _viewModel
            };
            _commandBarLooks.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;
            _commandBarLooks.SetBinding(MultiModeEditorCommandBar.ModeProperty, 
                new Binding {  Path = new PropertyPath("LooksCommandBarMode"), Mode = BindingMode.TwoWay });

            _commandBarSounds = new MultiModeEditorCommandBar
            {
                TargetType = AppBarTargetType.Sound,
                CopyCommand = _viewModel.CopySoundCommand,
                DeleteCommand = _viewModel.DeleteSoundCommand,
                NewCommand = _viewModel.AddNewSoundCommand,
                PlayCommand = _viewModel.StartPlayerCommand,
                DataContext = _viewModel
            };
            _commandBarSounds.ModeChanged += MultiModeEditorCommandBar_OnModeChanged;
            _commandBarSounds.SetBinding(MultiModeEditorCommandBar.ModeProperty, 
                new Binding {  Path = new PropertyPath("SoundsCommandBarMode"), Mode = BindingMode.TwoWay});
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

            if (listView == null)
                return;

            Debug.Assert(listView != null, "listView != null");

            switch (mode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    listView.SelectionEnabled = false;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    listView.SelectionEnabled = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }
        }

        private void LookItem_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            var isClickEnabled = !ListViewLooks.SelectionEnabled;
            if (isClickEnabled)
            {
                if (_viewModel.EditLookCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditLookCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);

            }
        }

        private void SoundItem_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            var isClickEnabled = !ListViewSounds.SelectionEnabled;

            if (isClickEnabled)
                if (_viewModel.EditSoundCommand.CanExecute(((FrameworkElement)e.OriginalSource).DataContext))
                    _viewModel.EditSoundCommand.Execute(((FrameworkElement)e.OriginalSource).DataContext);
        }
    }
}
