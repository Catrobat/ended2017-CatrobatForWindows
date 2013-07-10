using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Catrobat.IDEWindowsPhone.Views.Editor.Costumes;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Sprites;
using Catrobat.IDEWindowsPhone.Views.Main;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using SoundState = Microsoft.Xna.Framework.Audio.SoundState;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Windows.Navigation;

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class EditorView : PhoneApplicationPage
    {
        private readonly EditorViewModel _viewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

        private bool _updatePivot = true;
        private bool _isSpriteDragging = false;

        public EditorView()
        {
            InitializeComponent();
            LockPivotIfNoSpriteSelected();

            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "SelectedSprite")
                ReorderListBoxSprites.SelectedItem = _viewModel.SelectedSprite;
        }

        private void RegisterCollectionsChangedHandlers()
        {
            
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnBackKeyPress(e);
        }

        private void reorderListBoxScriptBricks_Loaded(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedBrick != null)
            {
                ReorderListBoxScriptBricks.ScrollIntoView(_viewModel.SelectedBrick);
                _viewModel.SelectedBrick = null;
            }
        }

        private void LockPivotIfNoSpriteSelected()
        {
            Sprite selectedSprite = _viewModel.SelectedSprite;

            if (selectedSprite == null)
            {
                if (PivotMain.Items.Contains(PivotScripts))
                {
                    if (_updatePivot)
                    {
                        PivotMain.Items.Remove(PivotScripts);
                        PivotMain.Items.Remove(PivotCostumes);
                        PivotMain.Items.Remove(PivotSounds);
                    }
                    else
                    {
                        _updatePivot = false;
                    }
                }
            }
            else
            {
                if (!PivotMain.Items.Contains(PivotScripts))
                {
                    try
                    {
                        PivotMain.Items.Add(PivotScripts);
                        PivotMain.Items.Add(PivotCostumes);
                        PivotMain.Items.Add(PivotSounds);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void reorderListBoxSprites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var reorderableListbox = (sender as ReorderListBox);
            var selectedSprite = reorderableListbox.SelectedItem as Sprite;

            if (reorderableListbox.IsDraging)
            {
                _isSpriteDragging = true;
                reorderableListbox.IsDraging = false;
            }
            else
            {
                if (_isSpriteDragging)
                {
                    _isSpriteDragging = false;
                }
                else
                {
                    if (selectedSprite != _viewModel.SelectedSprite)
                        _viewModel.SelectedSprite = selectedSprite;

                    LockPivotIfNoSpriteSelected();
                }
            }
        }


        private void reorderListBoxScriptBricks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = new ObservableCollection<DataObject>();
            var listBox = sender as ListBox;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var item in listBox.SelectedItems)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                list.Add(item as Script);
            }

            _viewModel.SelectedScripts = list;
        }

        private void reorderListBoxCostumes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = new ObservableCollection<Costume>();
            var listBox = sender as ListBox;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var item in listBox.SelectedItems)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                list.Add(item as Costume);
            }

            _viewModel.SelectedCostumes = list;
        }

        private void reorderListBoxSounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = new ObservableCollection<Sound>();
            var listBox = sender as ListBox;

            // ReSharper disable once PossibleNullReferenceException
            foreach (var item in listBox.SelectedItems)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                list.Add(item as Sound);
            }

            _viewModel.SelectedSounds = list;
        }


        private void buttonSoundPlay_Click(object sender, RoutedEventArgs e)
        {
            //PlayButton btnPlay = sender as PlayButton;

            //var parameter = new List<Object>();
            //parameter.Add(btnPlay.State);
            //parameter.Add(btnPlay.DataContext as Sound);

            //_viewModel.PlaySoundCommand.Execute(parameter);
        }
    }
}
