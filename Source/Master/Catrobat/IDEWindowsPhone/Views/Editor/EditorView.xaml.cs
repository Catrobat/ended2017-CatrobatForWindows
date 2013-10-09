using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Catrobat.Core;
using Catrobat.Core.Utilities;
using Catrobat.Core.Utilities.Helpers;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.CatrobatObjects.Bricks;
using Catrobat.Core.CatrobatObjects.Costumes;
using Catrobat.Core.CatrobatObjects.Scripts;
using Catrobat.Core.CatrobatObjects.Sounds;
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

        public EditorView()
        {
            InitializeComponent();
            LockPivotIfNoSpriteSelected(null);

            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == PropertyHelper.GetPropertyName(()=> _viewModel.SelectedSprite))
            {
                var selectedSprite = _viewModel.SelectedSprite;

                ReorderListBoxSprites.SelectedItem = selectedSprite;
                LockPivotIfNoSpriteSelected(selectedSprite);
            }
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

        private void LockPivotIfNoSpriteSelected(Sprite selectedSprite)
        {
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
    }
}
