using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.ViewModels.Editor.Sprites;
using Catrobat.IDE.WindowsPhone.Controls.ActionsControls;
using Catrobat.IDE.WindowsShared.Common;

namespace Catrobat.IDE.WindowsPhone.Controls
{
    public enum AppBarTargetType { Object, Action, Look, Sound }

    public delegate void MultiModeEditorCommandBarModeChanged(MultiModeEditorCommandBarMode mode);

    public class MultiModeEditorCommandBar : CommandBar
    {
        #region private members

        private MultiModeEditorCommandBarMode _currentMode;
        private ICommand _deleteCommand;
        private ICommand _copyCommand;

        private readonly AppBarButton _playButton;

        // Normal mode
        private readonly AppBarButton _newButton;
        private readonly AppBarButton _selectButton;
        private readonly AppBarButton _reorderButton;
        

        // Reorder mode
        private readonly AppBarButton _finishedReorderingButton;

        // Select mode
        private readonly AppBarButton _deleteButton;
        private readonly AppBarButton _copyButton;
        private readonly AppBarButton _cancelSelectionButton;

        #endregion

        #region events

        public event MultiModeEditorCommandBarModeChanged ModeChanged;

        #endregion

        #region dependency properties

        public MultiModeEditorCommandBarMode Mode
        {
            get { return (MultiModeEditorCommandBarMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode",
            typeof(MultiModeEditorCommandBarMode), typeof(MultiModeEditorCommandBar),
            new PropertyMetadata(MultiModeEditorCommandBarMode.Normal, ModePropertyChanged));

        private static void ModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as MultiModeEditorCommandBar);
            if (instance == null) return;
            var newMode = (MultiModeEditorCommandBarMode)e.NewValue;
            var oldMode = (MultiModeEditorCommandBarMode)e.OldValue;

            if (newMode != oldMode)
                instance.SetMode(newMode);
        }







        public AppBarTargetType TargetType
        {
            get { return (AppBarTargetType)GetValue(TargetTypeProperty); }
            set { SetValue(TargetTypeProperty, value); }
        }

        public static readonly DependencyProperty TargetTypeProperty =
            DependencyProperty.Register("TargetType", typeof(AppBarTargetType), typeof(MultiModeEditorCommandBar), new PropertyMetadata(AppBarTargetType.Look, TargetTypeChanged));

        private static void TargetTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var instance = (d as MultiModeEditorCommandBar);
            if (instance == null) return;

            Debug.Assert(e.NewValue != null, "e.NewValue != null");

            instance.SetMode(MultiModeEditorCommandBarMode.Normal);
            instance.UpdateAddText((AppBarTargetType)e.NewValue);
        }

        public ICommand NewCommand
        {
            get { return (ICommand)GetValue(NewCommandProperty); }
            set { SetValue(NewCommandProperty, value); }
        }
        public static readonly DependencyProperty NewCommandProperty =
            DependencyProperty.Register("NewCommand", typeof(ICommand), typeof(MultiModeEditorCommandBar), new PropertyMetadata(null, NewCommandChanged));
        private static void NewCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MultiModeEditorCommandBar)d)._newButton.Command = e.NewValue as ICommand;
        }


        public ICommand DeleteCommand
        {
            get { return (ICommand)GetValue(DeleteCommandProperty); }
            set { SetValue(DeleteCommandProperty, value); }
        }
        public static readonly DependencyProperty DeleteCommandProperty =
            DependencyProperty.Register("DeleteCommand", typeof(ICommand), typeof(MultiModeEditorCommandBar), new PropertyMetadata(null, DeleteCommandChanged));
        private static void DeleteCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((MultiModeEditorCommandBar)d)._deleteCommand = e.NewValue as ICommand;
            ((MultiModeEditorCommandBar)d)._deleteButton.Command = e.NewValue as ICommand;
        }

        public ICommand CopyCommand
        {
            get { return (ICommand)GetValue(CopyCommandProperty); }
            set { SetValue(CopyCommandProperty, value); }
        }
        public static readonly DependencyProperty CopyCommandProperty =
            DependencyProperty.Register("CopyCommand", typeof(ICommand), typeof(MultiModeEditorCommandBar), new PropertyMetadata(null, CopyCommandChanged));
        private static void CopyCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //((MultiModeEditorCommandBar)d)._copyCommand = e.NewValue as ICommand;
            ((MultiModeEditorCommandBar)d)._copyButton.Command = e.NewValue as ICommand;
        }

        public ICommand PlayCommand
        {
            get { return (ICommand)GetValue(PlayCommandProperty); }
            set { SetValue(PlayCommandProperty, value); }
        }
        public static readonly DependencyProperty PlayCommandProperty =
            DependencyProperty.Register("PlayCommand", typeof(ICommand), typeof(MultiModeEditorCommandBar), new PropertyMetadata(null, PlayCommandChanged));
        private static void PlayCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((MultiModeEditorCommandBar)d)._playButton.Command = e.NewValue as ICommand;
        }

        #endregion

        public MultiModeEditorCommandBar()
        {
            this.RequestedTheme = ElementTheme.Dark;
            SolidColorBrush appBarBackgroundBrush = new SolidColorBrush();
            appBarBackgroundBrush.Color = Color.FromArgb(255, 25, 165, 184);
            this.Background = appBarBackgroundBrush;

            _playButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonPlayProgram,
                Icon = new SymbolIcon(Symbol.Play)
            };

            _newButton = new AppBarButton
            {
                // Label will be chosen in UpdateAddText(...)
                Icon = new SymbolIcon(Symbol.Add)
            };

            _selectButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonSelect,
                Icon = new SymbolIcon(Symbol.SelectAll)
            };

            _reorderButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonStartReordering,
                //Icon = new SymbolIcon(Symbol.Bullets)
                //Icon = new SymbolIcon(Symbol.ShowResults)
                Icon = new SymbolIcon(Symbol.Sort)
            };

            _selectButton.Click += (sender, args) => 
                SetMode(MultiModeEditorCommandBarMode.Select);
            _reorderButton.Click += (sender, args) => 
                SetMode(MultiModeEditorCommandBarMode.Reorder);


            _finishedReorderingButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonFinishedReordering,
                Icon = new SymbolIcon(Symbol.Accept)
            };

            _finishedReorderingButton.Click += (sender, args) =>
                SetMode(MultiModeEditorCommandBarMode.Normal);

            _deleteButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonDelete,
                Icon = new SymbolIcon(Symbol.Delete)
            };
            //_deleteButton.Command = new RelayCommand(() =>
            //{
            //    SetMode(MultiModeEditorCommandBarMode.Normal);

            //    if (_deleteCommand != null)
            //        _deleteCommand.Execute(null);
            //});

            _copyButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonCopy,
                Icon = new SymbolIcon(Symbol.Copy)
            };
            //_copyButton.Command = new RelayCommand(() =>
            //{
            //    SetMode(MultiModeEditorCommandBarMode.Normal);

            //    if (_copyCommand != null)
            //        _copyCommand.Execute(null);
            //});

            _cancelSelectionButton = new AppBarButton
            {
                Label = AppResources.Editor_ButtonClearSelection,
                Icon = new SymbolIcon(Symbol.ClearSelection)
            };

            _cancelSelectionButton.Click += (sender, args) =>
                SetMode(MultiModeEditorCommandBarMode.Normal);


            SetMode(MultiModeEditorCommandBarMode.Normal);
        }

        private bool _firstModeChanged = true;
        private void SetMode(MultiModeEditorCommandBarMode newMode)
        {
            switch (_currentMode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    PrimaryCommands.Remove(_newButton);
                    PrimaryCommands.Remove(_selectButton);
                    PrimaryCommands.Remove(_reorderButton);
                    PrimaryCommands.Remove(_playButton);
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    PrimaryCommands.Remove(_finishedReorderingButton);
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    PrimaryCommands.Remove(_deleteButton);
                    PrimaryCommands.Remove(_copyButton);
                    PrimaryCommands.Remove(_cancelSelectionButton);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            switch (newMode)
            {
                case MultiModeEditorCommandBarMode.Normal:
                    PrimaryCommands.Add(_newButton);
                    PrimaryCommands.Add(_selectButton);

                    if (TargetType == AppBarTargetType.Object)
                        PrimaryCommands.Add(_reorderButton);
                    
                    PrimaryCommands.Add(_playButton);
                    _playButton.IsEnabled = true;
                    break;
                case MultiModeEditorCommandBarMode.Reorder:
                    PrimaryCommands.Add(_finishedReorderingButton);
                    _playButton.IsEnabled = false;
                    break;
                case MultiModeEditorCommandBarMode.Select:
                    PrimaryCommands.Add(_deleteButton);
                    PrimaryCommands.Add(_copyButton);
                    PrimaryCommands.Add(_cancelSelectionButton);
                    _playButton.IsEnabled = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("newMode");
            }

            
            _currentMode = newMode;

            if (ModeChanged != null)
            {
                Mode = newMode;
                ModeChanged.Invoke(newMode);
            }  
        }

        private void UpdateAddText(AppBarTargetType targetType)
        {
            var text = "";

            switch (targetType)
            {
                case AppBarTargetType.Object:
                    text = AppResources.Editor_ButtonAddObject;
                    break;

                case AppBarTargetType.Action:
                    text = AppResources.Editor_ButtonAddScript;
                    break;

                case AppBarTargetType.Look:
                    text = AppResources.Editor_ButtonAddLook;
                    break;

                case AppBarTargetType.Sound:
                    text = AppResources.Editor_ButtonAddSound;
                    break;
            }

            _newButton.Label = text;
        }
    }
}
