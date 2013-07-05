using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

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
                reorderListBoxScriptBricks.ScrollIntoView(_viewModel.SelectedBrick);
                _viewModel.SelectedBrick = null;
            }
        }

        private void LockPivotIfNoSpriteSelected()
        {
            var selectedSprite = _viewModel.SelectedSprite;

            if (selectedSprite == null)
            {
                if (pivotMain.Items.Contains(pivotScripts))
                {
                    if (_updatePivot)
                    {
                        pivotMain.Items.Remove(pivotScripts);
                        pivotMain.Items.Remove(pivotCostumes);
                        pivotMain.Items.Remove(pivotSounds);
                    }
                    else
                    {
                        _updatePivot = false;
                    }
                }
            }
            else
            {
                if (!pivotMain.Items.Contains(pivotScripts))
                {
                    try
                    {
                        pivotMain.Items.Add(pivotScripts);
                        pivotMain.Items.Add(pivotCostumes);
                        pivotMain.Items.Add(pivotSounds);
                    }
                    catch {}
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
                    _viewModel.SelectedSprite = selectedSprite;
                    LockPivotIfNoSpriteSelected();
                }
            }
        }

        private void reorderListBoxCostumes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Preventing selection
            reorderListBoxCostumes.SelectedIndex = -1;
        }

        private void reorderListBoxSounds_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Preventing selection
            reorderListBoxSounds.SelectedIndex = -1;
        }

        private void reorderListBoxScriptBricks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Preventing selection
            reorderListBoxScriptBricks.SelectedIndex = -1;
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