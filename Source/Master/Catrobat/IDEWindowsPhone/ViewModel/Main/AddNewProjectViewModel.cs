using System.Runtime.CompilerServices;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Annotations;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using GalaSoft.MvvmLight.Command;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
  public class AddNewProjectViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private readonly ICatrobatContext catrobatContext;

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
            RaisePropertyChanged();
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
        catrobatContext = CatrobatContext.GetContext();
    }

    private void Save()
    {
      CatrobatContext.GetContext().CurrentProject.Save();
      CatrobatContext.GetContext().CreateNewProject(_projectName);

      ProjectName = "";
    }

    #region PropertyChanged
    public event PropertyChangedEventHandler PropertyChanged;

    [NotifyPropertyChangedInvocator]
    protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
    {
      PropertyChangedEventHandler handler = PropertyChanged;
      if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
  }
}
