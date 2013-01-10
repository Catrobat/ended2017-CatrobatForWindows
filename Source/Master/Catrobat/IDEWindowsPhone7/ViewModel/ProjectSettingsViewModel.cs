using Catrobat.Core;
using Catrobat.Core.Objects;
using GalaSoft.MvvmLight;

namespace Catrobat.IDEWindowsPhone7.ViewModel
{
  public class ProjectSettingsViewModel : ViewModelBase
  {
    private readonly ICatrobatContext catrobatContext;
    private Sprite selectedSprite; // TODO (Memory): remove this and use Messageing instead 

    public Project CurrentProject { get { return catrobatContext.CurrentProject; } }

    public string ProjectName
    {
      get
      {
        return catrobatContext.CurrentProject.ProjectName;
      }

      set
      {
        catrobatContext.CurrentProject.ProjectName = value;
        RaisePropertyChanged("ProjectName");
      }
    }

    public string Title { get { return CurrentProject.ProjectName; } }


    public ProjectSettingsViewModel()
    {
      if (IsInDesignMode)
        catrobatContext = new CatrobatContextDesign();
      else
        catrobatContext = CatrobatContext.Instance;

      if(catrobatContext.CurrentProject.SpriteList.Sprites.Count > 0)
        selectedSprite = catrobatContext.CurrentProject.SpriteList.Sprites[0];
    }

    ////public override void Cleanup()
    ////{
    ////    // Clean up if needed

    ////    base.Cleanup();
    ////}
  }
}