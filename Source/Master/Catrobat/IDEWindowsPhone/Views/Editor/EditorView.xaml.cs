using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Editor;
using Catrobat.IDEWindowsPhone.Controls.Buttons;
using Catrobat.IDEWindowsPhone.Controls.ReorderableListbox;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
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

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class EditorView : PhoneApplicationPage
    {
        readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

        int _firstVisibleScriptBrickIndex;
        int _lastVisibleScriptBrickIndex;
        private bool _updatePivot = true;
        private bool _isSpriteDragging = false;

        public EditorView()
        {
            InitializeComponent();
            LockPivotIfNoSpriteSelected();
            _editorViewModel.OnAddedBroadcastMessage += OnStartAddBroadcastMessage;

            (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _editorViewModel.ResetViewModel();
        }

        #region improve?

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // TODO: maybe remove this 2 lines
            reorderListBoxSprites.ItemsSource = _editorViewModel.Sprites;
            reorderListBoxSprites.SelectedItem = _editorViewModel.SelectedSprite;

            // TODO: do this somewhere else
            if (_editorViewModel.SelectedBrick != null)
            {
                Sprite selectedSprite = _editorViewModel.SelectedSprite;
                DataObject newScriptBrick = _editorViewModel.SelectedBrick;

                ((ScriptBrickCollection)reorderListBoxScriptBricks.ItemsSource).AddScriptBrick(newScriptBrick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex);

                if (newScriptBrick is LoopBeginBrick)
                {
                    LoopEndBrick brick = new LoopEndBrick(((LoopBeginBrick)newScriptBrick).Sprite);
                    brick.LoopBeginBrick = (LoopBeginBrick)newScriptBrick;
                    ((LoopBeginBrick)newScriptBrick).LoopEndBrick = brick;
                    ((ScriptBrickCollection)reorderListBoxScriptBricks.ItemsSource).AddScriptBrick(brick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
                }

                reorderListBoxScriptBricks.UpdateLayout();
                reorderListBoxScriptBricks.ScrollIntoView(reorderListBoxScriptBricks.ItemContainerGenerator.ContainerFromItem(newScriptBrick));
                _editorViewModel.SelectedBrick = null;
            }
        }

        private void reorderListBoxScriptBricks_Loaded(object sender, RoutedEventArgs e)
        {
            if (_editorViewModel.SelectedBrick != null)
            {
                reorderListBoxScriptBricks.ScrollIntoView(_editorViewModel.SelectedBrick);
                _editorViewModel.SelectedBrick = null;
            }
        }

        private void reorderListBoxScriptBricks_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            object dragItem = ((ReorderListBox)sender).DragItem;

            if (dragItem is Script)
                e.Complete();
        }

        private void LockPivotIfNoSpriteSelected()
        {
            Sprite selectedSprite = _editorViewModel.SelectedSprite;

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
                    catch
                    {
                    }
                }
            }
        }

        private void reorderListBoxSprites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ReorderListBox reorderableListbox = (sender as ReorderListBox);
            Sprite selectedSprite = reorderableListbox.SelectedItem as Sprite;

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
                    _editorViewModel.SelectedSprite = selectedSprite;
                    LockPivotIfNoSpriteSelected();
                }
            }
        }

        private void reorderListBoxSprites_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            _updatePivot = false;
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

        private void OnStartAddBroadcastMessage()
        {

            Navigation.NavigateTo(typeof(NewBroadcastMessage));
        }

        #endregion

        private void menueMainMenue_Click(object sender, EventArgs e)
        {
            _editorViewModel.GoToMainMenueCommand.Execute(null);
        }

        private void menueProjectSettings_Click(object sender, EventArgs e)
        {
            _editorViewModel.ProjectSettingsCommand.Execute(null);
        }

        private void buttonSoundPlay_Click(object sender, RoutedEventArgs e)
        {
            PlayButton btnPlay = sender as PlayButton;

            var parameter = new List<Object>();
            parameter.Add(btnPlay.State);
            parameter.Add(btnPlay.DataContext as Sound);

            _editorViewModel.PlaySoundCommand.Execute(parameter);
        }


        #region Appbar

        private void changeAppbar()
        {
            string iconsPath = "/Content/Images/ApplicationBar/dark/";

            ApplicationBar = new ApplicationBar();

            //ApplicationBarIconButton undoButton = new ApplicationBarIconButton();
            //undoButton.IconUri = new Uri(iconsPath + "appbar.back.rest.png", UriKind.Relative);
            //undoButton.Text = "rückgängig";
            //ApplicationBar.Buttons.Add(undoButton);

            ApplicationBarIconButton addButton = new ApplicationBarIconButton();
            addButton.IconUri = new Uri(iconsPath + "appbar.add.rest.png", UriKind.Relative);

            if (pivotMain.SelectedItem == pivotSprites)
                addButton.Text = EditorResources.ButtonAddObject;
            else if (pivotMain.SelectedItem == pivotScripts)
                addButton.Text = EditorResources.ButtonAddScript;
            else if (pivotMain.SelectedItem == pivotCostumes)
                addButton.Text = EditorResources.ButtonAddCostume;
            else if (pivotMain.SelectedItem == pivotSounds)
                addButton.Text = EditorResources.ButtonAddSound;

            ApplicationBar.Buttons.Add(addButton);

            ApplicationBarIconButton playButton = new ApplicationBarIconButton();
            playButton.IconUri = new Uri(iconsPath + "appbar.transport.play.rest.png", UriKind.Relative);
            playButton.Text = EditorResources.ButtonPlayProject;
            ApplicationBar.Buttons.Add(playButton);

            //ApplicationBarIconButton redoButton = new ApplicationBarIconButton();
            //redoButton.IconUri = new Uri(iconsPath + "appbar.next.rest.png", UriKind.Relative);
            //redoButton.Text = "wiederherstellen";
            //ApplicationBar.Buttons.Add(redoButton);

            addButton.Click += appbarButtonAdd_Click;
            //undoButton.Click += new EventHandler(appbarButtonUndo_Click);
            playButton.Click += new EventHandler(appbarButtonPlay_Click);
            //redoButton.Click += new EventHandler(appbarButtonRedo_Click);

            ApplicationBarMenuItem menuItemMainMenue = new ApplicationBarMenuItem();
            menuItemMainMenue.Text = EditorResources.MenuStartSite;
            ApplicationBar.MenuItems.Add(menuItemMainMenue);
            menuItemMainMenue.Click += new EventHandler(menueMainMenue_Click);

            ApplicationBarMenuItem menuItemProjectSettings = new ApplicationBarMenuItem();
            menuItemProjectSettings.Text = EditorResources.MenuProjectSettings;
            ApplicationBar.MenuItems.Add(menuItemProjectSettings);
            menuItemProjectSettings.Click += new EventHandler(menueProjectSettings_Click);
        }

        private void appbarButtonAdd_Click(object sender, EventArgs e)
        {
            if (pivotMain.SelectedItem == pivotSprites)
            {
                _editorViewModel.AddNewSpriteCommand.Execute(null);
            }
            else if (pivotMain.SelectedItem == pivotScripts)
            {
                _firstVisibleScriptBrickIndex = reorderListBoxScriptBricks.FirstVisibleItemIndex;
                _lastVisibleScriptBrickIndex = reorderListBoxScriptBricks.LastVisibleItemIndex;

                _editorViewModel.AddNewScriptBrickCommand.Execute(null);
            }
            else if (pivotMain.SelectedItem == pivotCostumes)
            {
                _editorViewModel.AddNewCostumeCommand.Execute(null);
            }
            else if (pivotMain.SelectedItem == pivotSounds)
            {
                _editorViewModel.AddNewSoundCommand.Execute(null);
            }
        }

        private void LanguageChanged(object sender, PropertyChangedEventArgs e)
        {
            changeAppbar();
        }

        private void pivotMain_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            changeAppbar();
        }

        private void appbarButtonPlay_Click(object sender, EventArgs e)
        {
            _editorViewModel.StartPlayerCommand.Execute(null);
        }

        private void appbarButtonUndo_Click(object sender, EventArgs e)
        {
            _editorViewModel.UndoCommand.Execute(null);
        }

        private void appbarButtonRedo_Click(object sender, EventArgs e)
        {
            _editorViewModel.RedoCommand.Execute(null);
        }

        #endregion

    }
}
