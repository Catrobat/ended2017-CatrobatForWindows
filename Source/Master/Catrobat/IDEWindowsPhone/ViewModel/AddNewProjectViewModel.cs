using Catrobat.Core;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
  public class AddNewProjectViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private readonly ICatrobatContext catrobatContext;
    public new event PropertyChangedEventHandler PropertyChanged;

    private string _projectName;

    public string ProjectName
    {
      get
      {
        return _projectName;
      }
      set
      {
        if (_projectName != value)
        {
          _projectName = value;

          if (this.PropertyChanged != null)
          {
            PropertyChanged(this, new PropertyChangedEventArgs("ProjectName"));
          }
        }
      }
    }

    public RelayCommand SaveCommand
    {
      get;
      private set;
    }

    public AddNewProjectViewModel()
    {
      // Commands
      SaveCommand = new RelayCommand(Save);

      if (IsInDesignMode)
        catrobatContext = new CatrobatContextDesign();
      else
        catrobatContext = CatrobatContext.Instance;
    }

    private void Save()
    {
      CatrobatContext.Instance.CurrentProject.Save();
      CatrobatContext.Instance.CreateNewProject(_projectName);

      ProjectName = "";
    }
  }
}
