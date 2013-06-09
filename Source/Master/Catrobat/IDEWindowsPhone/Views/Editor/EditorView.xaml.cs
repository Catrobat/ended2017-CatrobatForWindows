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
using Catrobat.IDEWindowsPhone.Views.Editor.Costumes;
using Catrobat.IDEWindowsPhone.Views.Editor.Scripts;
using Catrobat.IDEWindowsPhone.Views.Editor.Sounds;
using Catrobat.IDEWindowsPhone.Views.Editor.Sprites;
using IDEWindowsPhone;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;
using SoundState = Microsoft.Xna.Framework.Audio.SoundState;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
  public partial class EditorView : PhoneApplicationPage
  {
    readonly EditorViewModel _editorViewModel = ServiceLocator.Current.GetInstance<EditorViewModel>();

    int _firstVisibleScriptBrickIndex;
    int _lastVisibleScriptBrickIndex;
    private readonly SoundPlayer _soundPlayer;
    private Sound _sound;
    private bool _updatePivote = true;
    private bool _isSpriteDragging = false;

    public EditorView()
    {
      InitializeComponent();
      LockPivotIfNoSpriteSelected();
      _editorViewModel.OnAddedBroadcastMessage += OnStartAddBroadcastMessage;
      _soundPlayer = new SoundPlayer();
      _soundPlayer.SoundStateChanged += SoundPlayerStateChanged;

      (App.Current.Resources["LocalizedStrings"] as LocalizedStrings).PropertyChanged += LanguageChanged;
    }

    protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
    {
      _soundPlayer.Stop();
    }

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
          brick.LoopBeginBrick = (LoopBeginBrick) newScriptBrick;
          ((LoopBeginBrick)newScriptBrick).LoopEndBrick = brick;
          ((ScriptBrickCollection)reorderListBoxScriptBricks.ItemsSource).AddScriptBrick(brick, _firstVisibleScriptBrickIndex, _lastVisibleScriptBrickIndex + 1);
        }

        reorderListBoxScriptBricks.UpdateLayout();
        reorderListBoxScriptBricks.ScrollIntoView(reorderListBoxScriptBricks.ItemContainerGenerator.ContainerFromItem(newScriptBrick));
        _editorViewModel.SelectedBrick = null;
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
          if (_updatePivote)
          {
            pivotMain.Items.Remove(pivotScripts);
            pivotMain.Items.Remove(pivotCostumes);
            pivotMain.Items.Remove(pivotSounds);
          }
          else
          {
            _updatePivote = false;
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
      _updatePivote = false;
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


    private void menueMainMenue_Click(object sender, EventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Main/MainView.xaml", UriKind.Relative));
    }

    private void menueProjectSettings_Click(object sender, EventArgs e)
    {
      NavigationService.Navigate(new Uri("/Views/Editor/ProjectSettingsView.xaml", UriKind.Relative));
    }


    private void btnDeleteSprite_Click(object sender, RoutedEventArgs e)
    {
      Sprite sprite = ((Button)sender).DataContext as Sprite;
      string name = sprite.Name;

      MessageBoxResult result = MessageBox.Show(EditorResources.MessageBoxDeleteSpriteText1 + name + EditorResources.MessageBoxDeleteSpriteText2,
        EditorResources.MessageBoxDeleteSpriteHeader, MessageBoxButton.OKCancel);

      if (result == MessageBoxResult.OK)
        _editorViewModel.DeleteSpriteCommand.Execute(sprite);
    }

    private void btnEditSpriteName_Click(object sender, RoutedEventArgs e)
    {
      _editorViewModel.SelectedSprite = ((Button)sender).DataContext as Sprite;
      NavigationService.Navigate(new Uri("/Views/Editor/Sprites/ChangeSpriteName.xaml", UriKind.Relative));
    }

    private void btnCopySprite_Click(object sender, RoutedEventArgs e)
    {
      Sprite newSprite = (((Button)sender).DataContext as Sprite).Copy() as Sprite;
      CatrobatContext.GetContext().CurrentProject.SpriteList.Sprites.Add(newSprite);
    }


    private void btnDeleteSound_Click(object sender, RoutedEventArgs e)
    {
      Sound sound = ((Button)sender).DataContext as Sound;
      string name = sound.Name;

      MessageBoxResult result = MessageBox.Show(EditorResources.MessageBoxDeleteSoundText1 + name + EditorResources.MessageBoxDeleteSoundText2,
        EditorResources.MessageBoxDeleteSoundHeader, MessageBoxButton.OKCancel);
      if (result == MessageBoxResult.OK)
      {
        _editorViewModel.DeleteSoundCommand.Execute(sound);
      }
    }

    private void btnEditSoundName_Click(object sender, RoutedEventArgs e)
    {
      _editorViewModel.SelectedSound = ((Button)sender).DataContext as Sound;
      NavigationService.Navigate(new Uri("/Views/Editor/Sounds/ChangeSoundName.xaml", UriKind.Relative));
    }

    private void buttonSoundPlay_Click(object sender, RoutedEventArgs e)
    {
      PlayButton btnPlay = sender as PlayButton;
      

      if (btnPlay.State == PlayButtonState.Play)
      {
        if (_sound != btnPlay.DataContext as Sound)
        {
          _sound = btnPlay.DataContext as Sound;
          _soundPlayer.SetSound(_sound);
        }
        _soundPlayer.Play();
      }
      else
      {
        _soundPlayer.Pause();
      }
    }

    private void SoundPlayerStateChanged(Misc.SoundState soundState, Misc.SoundState newState)
    {
      if (newState == Misc.SoundState.Stopped)
        Dispatcher.BeginInvoke(() =>
        {
          reorderListBoxSounds.ItemsSource = null;
          reorderListBoxSounds.ItemsSource = _editorViewModel.Sounds;
        });
    }


    private void btnDeleteCostume_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      Costume costume = ((Button)sender).DataContext as Costume;
      string name = costume.Name;

      MessageBoxResult result = MessageBox.Show(EditorResources.MessageBoxDeleteCostumeText1 + name + EditorResources.MessageBoxDeleteCostumeText2,
        EditorResources.MessageBoxDeleteCostumeHeader, MessageBoxButton.OKCancel);

      if (result == MessageBoxResult.OK)
        _editorViewModel.DeleteCostumeCommand.Execute(costume);
    }

    private void btnEditCostumeName_Click(object sender, System.Windows.RoutedEventArgs e)
    {
      _editorViewModel.EditCostume = ((Button)sender).DataContext as Costume;
      NavigationService.Navigate(new Uri("/Views/Editor/Costumes/ChangeCostume.xaml", UriKind.Relative));
    }

    private void btnCopyCostume_Click(object sender, RoutedEventArgs e)
    {
      Costume costume = ((Button)sender).DataContext as Costume;
      _editorViewModel.CopyCostumeCommand.Execute(costume);
    }


    private void OnStartAddBroadcastMessage()
    {
      NavigationService.Navigate(new Uri("/Views/Editor/Scripts/NewBroadcastMessage.xaml", UriKind.Relative));
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

      addButton.Click += new EventHandler(appbarButtonAdd_Click);
      //undoButton.Click += new EventHandler(appbarButtonUndo_Click);
      playButton.Click += new EventHandler(appbarButtonPlay_Click);
      //redoButton.Click += new EventHandler(appbarButtonRedo_Click);

      ApplicationBarMenuItem menuItemMainMenue = new ApplicationBarMenuItem();
      menuItemMainMenue.Text = EditorResources.MenuStartSite;
      ApplicationBar.MenuItems.Add(menuItemMainMenue);
      menuItemMainMenue.Click += new EventHandler(menueMainMenue_Click);

      ApplicationBarMenuItem menuItemProjectSettingds = new ApplicationBarMenuItem();
      menuItemProjectSettingds.Text = EditorResources.MenuProjectSettings;
      ApplicationBar.MenuItems.Add(menuItemProjectSettingds);
      menuItemProjectSettingds.Click += new EventHandler(menueProjectSettings_Click);
    }

    private void appbarButtonAdd_Click(object sender, EventArgs e)
    {
      if (pivotMain.SelectedItem == pivotSprites)
      {
        NavigationService.Navigate(new Uri("/Views/Editor/Sprites/AddNewSprite.xaml", UriKind.Relative));
      }
      else if (pivotMain.SelectedItem == pivotScripts)
      {
        _firstVisibleScriptBrickIndex = reorderListBoxScriptBricks.FirstVisibleItemIndex;
        _lastVisibleScriptBrickIndex = reorderListBoxScriptBricks.LastVisibleItemIndex;

        NavigationService.Navigate(new Uri("/Views/Editor/Scripts/AddNewScript.xaml", UriKind.Relative));
      }
      else if (pivotMain.SelectedItem == pivotCostumes)
      {
        NavigationService.Navigate(new Uri("/Views/Editor/Costumes/AddNewCostume.xaml", UriKind.Relative));
      }
      else if (pivotMain.SelectedItem == pivotSounds)
      {
        NavigationService.Navigate(new Uri("/Views/Editor/Sounds/AddNewSound.xaml", UriKind.Relative));
      }
    }

    private void appbarButtonPlay_Click(object sender, EventArgs e)
    {
      NavigationService.Navigate(new Uri("/MetroCatPlayer;component/GamePage.xaml", UriKind.Relative));
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
