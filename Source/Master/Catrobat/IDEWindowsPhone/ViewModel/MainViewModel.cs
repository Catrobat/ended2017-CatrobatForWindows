using System.IO;
using System.Windows.Interop;
using Catrobat.Core;
using Catrobat.Core.Misc;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Themes;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.ComponentModel;
using System;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using IDEWindowsPhone;

namespace Catrobat.IDEWindowsPhone.ViewModel
{
  public class MainViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private readonly ICatrobatContext catrobatContext;
    public new event PropertyChangedEventHandler PropertyChanged;

    public CultureInfo CurrentCulture
    {
      get
      {
        return Thread.CurrentThread.CurrentCulture;
      }

      set
      {
        if (Thread.CurrentThread.CurrentCulture.Equals(value))
          return;

        Thread.CurrentThread.CurrentCulture = value;
        Thread.CurrentThread.CurrentUICulture = value;

        ((LocalizedStrings)App.Current.Resources["LocalizedStrings"]).Reset();
        RaisePropertyChanged("CurrentCulture");
        RaisePropertyChanged("CurrentCultureName");
      }
    }

    public string CurrentCultureName
    {
      get
      {
        return CurrentCulture.NativeName;
      }
    }

    public ObservableCollection<CultureInfo> AvailableCultures
    {
      get
      {
        return LanguageHelper.SupportedLanguages;
      }
    }

    public Project CurrentProject { get { return catrobatContext.CurrentProject; } }

    public BitmapImage CurrentProjectScreenshot
    {
      get
      {
        using (var memoryStream = new MemoryStream(CurrentProject.ProjectScreenshot,
          0, CurrentProject.ProjectScreenshot.Length))
        {
          var bitmapImage = new BitmapImage();
          bitmapImage.SetSource(memoryStream);
          return bitmapImage;
        }
      }
    }

    public ObservableCollection<ProjectHeader> LocalProjects { get { return catrobatContext.LocalProjects; } }

    public OnlineProjectHeader SelectedOnlineProject { get; set; }

    private ObservableCollection<OnlineProjectHeader> _onlineProjects = new ObservableCollection<OnlineProjectHeader>();

    public ObservableCollection<OnlineProjectHeader> OnlineProjects
    {
      get
      {
        return _onlineProjects;
      }
    }

    public string ApplicationVersion
    {
      get
      {
        return StaticApplicationSettings.CurrentApplicationVersionName;
      }
    }

    private string _filterText = "";

    public String FilterText
    {
      get
      {
        return _filterText;
      }
      set
      {
        if (_filterText != value)
        {
          _filterText = value;
          LoadOnlineProjects(false);

          if (this.PropertyChanged != null)
          {
            RaisePropertyChanged("FilterText");
          }

        }
      }
    }

    public void CatrobatContextPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "CurrentProject")
      {
        if (this.PropertyChanged != null)
        {
          RaisePropertyChanged("CurrentProject");
          RaisePropertyChanged("CurrentProjectScreenshot");
        }

        CatrobatContext.Instance.CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;
      }
    }

    public void CurrentProjectPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "ProjectScreenshot")
      {
        if (this.PropertyChanged != null)
        {
          RaisePropertyChanged("CurrentProjectScreenshot");
        }
      }
    }

    public void ThemeChooserPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (e.PropertyName == "SelectedTheme")
      {
        RaisePropertyChanged("SelectedTheme");
      }
    }

    public MainViewModel()
    {
      CatrobatContext.Instance.PropertyChanged += CatrobatContextPropertyChanged;
      CatrobatContext.Instance.CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;

      (App.Current.Resources["ThemeChooser"] as ThemeChooser).PropertyChanged += ThemeChooserPropertyChanged;

      // Commands
      DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProject);
      CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProject);
      LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjects);
      SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProject);

      if (IsInDesignMode)
        catrobatContext = new CatrobatContextDesign();
      else
        catrobatContext = CatrobatContext.Instance;
    }

    private void LoadOnlineProjectsCallback(List<OnlineProjectHeader> projects, bool append)
    {
      if (!append)
      {
        _onlineProjects.Clear();
      }

      foreach (OnlineProjectHeader header in projects)
      {
        _onlineProjects.Add(header);
      }
    }

    public void LoadOnlineProjects(bool append)
    {
      ServerCommunication.LoadOnlineProjects(append, _filterText, _onlineProjects.Count, LoadOnlineProjectsCallback);
    }

    public MessageBoxResult _dialogResult;

    public RelayCommand<string> DeleteLocalProjectCommand
    {
      get;
      private set;
    }

    private void DialogMessageCallback(MessageBoxResult result)
    {
      _dialogResult = result;
    }

    private void DeleteLocalProject(string projectName)
    {
      var message = new DialogMessage(String.Format(MainResources.MainDeleteProjectDialogMessage, projectName), DialogMessageCallback)
      {
        Button = MessageBoxButton.OKCancel,
        Caption = MainResources.MainDeleteProjectDialogTitle
      };

      Messenger.Default.Send(message);

      if (_dialogResult == MessageBoxResult.OK)
      {
        CatrobatContext.Instance.DeleteProject(projectName);
      }
    }

    public RelayCommand<string> CopyLocalProjectCommand
    {
      get;
      private set;
    }

    private void CopyLocalProject(string projectName)
    {
      var message = new DialogMessage(String.Format(MainResources.MainCopyProjectDialogMessage, projectName), DialogMessageCallback)
      {
        Button = MessageBoxButton.OKCancel,
        Caption = MainResources.MainCopyProjectDialogTitle
      };

      Messenger.Default.Send(message);

      if (_dialogResult == MessageBoxResult.OK)
      {
        CatrobatContext.Instance.CopyProject(projectName);
      }
    }

    public RelayCommand LazyLoadOnlineProjectsCommand
    {
      get;
      private set;
    }

    private void LazyLoadOnlineProjects()
    {
      LoadOnlineProjects(true);
    }

    public RelayCommand<string> SetCurrentProjectCommand
    {
      get;
      private set;
    }

    private void SetCurrentProject(string projectName)
    {
      CatrobatContext.Instance.SetCurrentProject(projectName);
    }

    // This is a fix to the bug that the overwritten RaisePropertyChanged is not working properly
    protected override void RaisePropertyChanged(string propertyName)
    {
      if(PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
