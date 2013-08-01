using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.IDE.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using Microsoft.Practices.ServiceLocation;


namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class EditorView : PhoneApplicationPage
    {
        private readonly EditorViewModel _viewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

        private bool _updatePivot = true;
        private bool _isSpriteDragging;

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
            _viewModel.IsSpriteSelecting = true;

            var task = Task.Run(() =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    var reorderableListbox = (sender as ReorderListBox);
                    if (reorderableListbox != null)
                    {
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
                                if (selectedSprite != null && selectedSprite != _viewModel.SelectedSprite)
                                    _viewModel.SelectedSprite = selectedSprite;

                                LockPivotIfNoSpriteSelected();
                            }
                        }
                    }

                    _viewModel.IsSpriteSelecting = false;
                });
            });
        }
    }
}
