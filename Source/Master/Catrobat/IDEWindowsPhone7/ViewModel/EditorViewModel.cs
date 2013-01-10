using Catrobat.Core;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Catrobat.IDEWindowsPhone7.Views.Editor.Scripts;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDEWindowsPhone7.ViewModel
{
  public class EditorViewModel : ViewModelBase
  {
    #region Commands

    public ICommand DeleteScriptBrickCommand
    {
      get;
      private set;
    }

    public ICommand CopyScriptBrickCommand
    {
      get;
      private set;
    }

    public ICommand StartAddBroadcastMessageCommand
    {
      get;
      private set;
    }

    public ICommand DeleteSoundCommand
    {
      get;
      private set;
    }

    public ICommand DeleteSpriteCommand
    {
      get;
      private set;
    }

    public ICommand DeleteCostumeCommand
    {
      get;
      private set;
    }

    public ICommand CopyCostumeCommand
    {
      get;
      private set;
    }

    public ICommand UndoCommand
    {
      get;
      private set;
    }

    public ICommand RedoCommand
    {
      get;
      private set;
    }

    public RelayCommand<object> NothingItemHackCommand
    {
      get;
      private set;
    }

    # endregion

    #region Actions

    private void DeleteScriptBrickAction(DataObject scriptBrick)
    {
      if (scriptBrick != null)
        ScriptBricks.Remove(scriptBrick);
    }

    private void CopyScriptBrickAction(DataObject scriptBrick)
    {
      if (scriptBrick != null)
      {
        if (scriptBrick is Script)
        {
          DataObject copy = (scriptBrick as Script).Copy((scriptBrick as Script).Sprite);
          ScriptBricks.Insert(ScriptBricks.ScriptIndexOf((Script)scriptBrick), copy);
        }

        if (scriptBrick is Brick)
        {
          DataObject copy = (scriptBrick as Brick).Copy((scriptBrick as Brick).Sprite);
          ScriptBricks.Insert(ScriptBricks.IndexOf(scriptBrick), copy);
        }
      }
    }

    private void AddBroadcastMessageAction(DataObject broadcastObject)
    {
      // TODO: change this
      this.broadcastObject = broadcastObject;

      if(OnAddedBroadcastMessage != null)
        OnAddedBroadcastMessage.Invoke();
    }

    public void AddBroadcastMessageAction(string message)
    {
      if (!catrobatContext.CurrentProject.BroadcastMessages.Contains(message))
      {
        catrobatContext.CurrentProject.BroadcastMessages.Add(message);
        RaisePropertyChanged("BroadcastMessages");
      }
    }

    private void DeleteSoundAction(Sound sound)
    {
      sound.Delete();

      SoundList soundList = SelectedSprite.Sounds;
      soundList.Sounds.Remove(sound);

      CatrobatContext.Instance.CleanUpSoundReferences(sound, sound.Sprite);
    }

    private void DeleteSpriteAction(Sprite sprite)
    {
      sprite.Delete();
      CatrobatContext.Instance.CurrentProject.SpriteList.Sprites.Remove(sprite);
      CatrobatContext.Instance.CleanUpSpriteReferences(sprite);
    }

    private void DeleteCostumeAction(Costume costume)
    {
      costume.Delete();

      CostumeList costumeList = SelectedSprite.Costumes;
      costumeList.Costumes.Remove(costume);

      CatrobatContext.Instance.CleanUpCostumeReferences(costume, costume.Sprite);
    }

    private void CopyCostumeAction(Costume costume)
    {
      Costume newCostume = costume.Copy(SelectedSprite) as Costume;

      CostumeList costumeList = SelectedSprite.Costumes;
      costumeList.Costumes.Add(newCostume);
    }

    private void UndoAction()
    {
      CurrentProject.Undo();
    }

    private void RedoAction()
    {
      CurrentProject.Redo();
    }

    private void NothingItemHackAction(object attachedObject)
    {
      // Pretty hack-y, but oh well...
      if (attachedObject is BroadcastScript)
      {
        ((BroadcastScript)attachedObject).ReceivedMessage = null;
      }
      else if (attachedObject is PointToBrick)
      {
        ((PointToBrick)attachedObject).PointedSprite = null;
      }
      else if (attachedObject is PlaySoundBrick)
      {
        ((PlaySoundBrick)attachedObject).Sound = null;
      }
      else if (attachedObject is SetCostumeBrick)
      {
        ((SetCostumeBrick)attachedObject).Costume = null;
      }
      else if (attachedObject is BroadcastBrick)
      {
        ((BroadcastBrick)attachedObject).BroadcastMessage = null;
      }
      else if (attachedObject is BroadcastWaitBrick)
      {
        ((BroadcastWaitBrick)attachedObject).BroadcastMessage = null;
      }
    }

    #endregion

    # region Events

    public delegate void AddedBroadcastMessageEvent();
    public AddedBroadcastMessageEvent OnAddedBroadcastMessage;

    # endregion

    # region Properties

    private DataObject broadcastObject;
    public DataObject BroadcastObject
    {
      get { return broadcastObject; }
    }

    private readonly ICatrobatContext catrobatContext;

    public Project CurrentProject
    {
      get
      {
        return catrobatContext.CurrentProject;
      }
    }

    public ObservableCollection<Sprite> Sprites
    {
      get
      {
        return CurrentProject.SpriteList.Sprites;
      }
    }

    private Sprite selectedSprite;
    public Sprite SelectedSprite
    {
      get
      {
        return selectedSprite;
      }
      set
      {
        selectedSprite = value;

        scriptBricks.Update(selectedSprite);

        RaisePropertyChanged("SelectedSprite");
        RaisePropertyChanged("Sounds");
        RaisePropertyChanged("Costumes");
      }
    }

    private ScriptBrickCollection scriptBricks;
    public ScriptBrickCollection ScriptBricks
    {
      get
      {
        return scriptBricks;
      }
    }

    public ObservableCollection<Sound> Sounds
    {
      get
      {
        if (selectedSprite != null)
          return selectedSprite.Sounds.Sounds;
        else
          return null;
      }
    }

    public ObservableCollection<Costume> Costumes
    {
      get
      {
        if (selectedSprite != null)
          return selectedSprite.CostumeList.Costumes;
        else
          return null;
      }
    }

    public ObservableCollection<string> BroadcastMessages
    {
      get
      {
        return catrobatContext.CurrentProject.BroadcastMessages;
      }
    }

    public BitmapImage CurrentProjectScreenshot { get { return CurrentProject.ProjectScreenshot; } }

    # endregion

    public EditorViewModel()
    {
      this.DeleteScriptBrickCommand = new RelayCommand<DataObject>(this.DeleteScriptBrickAction);
      this.CopyScriptBrickCommand = new RelayCommand<DataObject>(this.CopyScriptBrickAction);
      this.StartAddBroadcastMessageCommand = new RelayCommand<DataObject>(this.AddBroadcastMessageAction);

      this.DeleteSoundCommand = new RelayCommand<Sound>(this.DeleteSoundAction);
      this.DeleteSpriteCommand = new RelayCommand<Sprite>(this.DeleteSpriteAction);
      this.DeleteCostumeCommand = new RelayCommand<Costume>(this.DeleteCostumeAction);
      this.CopyCostumeCommand = new RelayCommand<Costume>(this.CopyCostumeAction);
      this.UndoCommand = new RelayCommand(this.UndoAction);
      this.RedoCommand = new RelayCommand(this.RedoAction);

      this.NothingItemHackCommand = new RelayCommand<object>(NothingItemHackAction);

      if (IsInDesignMode)
      {
        catrobatContext = new CatrobatContextDesign();
        selectedSprite = catrobatContext.CurrentProject.SpriteList.Sprites[0];
      }
      else
      {
        catrobatContext = CatrobatContext.Instance;
      }

      scriptBricks = new ScriptBrickCollection();
    }

    public override void Cleanup()
    {
      // Clean up if needed
      // TODO: set all references to null

      base.Cleanup();
    }
  }
}
