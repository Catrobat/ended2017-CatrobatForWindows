using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.IDECommon.Resources.Main;
using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Threading;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDEWindowsPhone7.ViewModel
{
  public class UploadProjectViewModel : ViewModelBase, INotifyPropertyChanged
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

    private string _projectDescription;

    public string ProjectDescription
    {
      get
      {
        return _projectDescription;
      }
      set
      {
        if (_projectDescription != value)
        {
          _projectDescription = value;

          if (this.PropertyChanged != null)
          {
            PropertyChanged(this, new PropertyChangedEventArgs("ProjectDescription"));
          }
        }
      }
    }

    public RelayCommand UploadCommand
    {
      get;
      private set;
    }

    public UploadProjectViewModel()
    {
      // Commands
      UploadCommand = new RelayCommand(Upload);

      if (IsInDesignMode)
        catrobatContext = new CatrobatContextDesign();
      else
        catrobatContext = CatrobatContext.Instance;

      _projectName = catrobatContext.CurrentProject.ProjectName;
    }

    private void Upload()
    {
      catrobatContext.CurrentProject.ProjectName = _projectName;

      ServerCommunication.uploadProject(_projectName, _projectDescription,
        CatrobatContext.Instance.CurrentUserEmail,
        Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName,
        CatrobatContext.Instance.CurrentToken, UploadCallback);

      Messenger.Default.Send(new DialogMessage(MainResources.UploadQueueMessage, null)
      {
        Caption = MainResources.MessageBoxInformation,
        Button = System.Windows.MessageBoxButton.OK,
      });
    }

    private void UploadCallback(bool successful)
    {
      if (ServerCommunication.NoUploadsPending())
      {
        Messenger.Default.Send(new DialogMessage(MainResources.NoUploadsPending, null)
        {
          Caption = MainResources.MessageBoxInformation,
          Button = System.Windows.MessageBoxButton.OK,
        });
      }
    }
  }
}
