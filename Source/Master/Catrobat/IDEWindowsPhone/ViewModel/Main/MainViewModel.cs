using Catrobat.Core;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Themes;
using Catrobat.IDEWindowsPhone.Views.Main;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Media;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.ViewModel.Main
{
  public class MainViewModel : ViewModelBase, INotifyPropertyChanged
  {
    private readonly ICatrobatContext _catrobatContext;
    public new event PropertyChangedEventHandler PropertyChanged;

    public CatrobatContext Context { get; set; }

    public Project CurrentProject { get { return _catrobatContext.CurrentProject; } }

    public ProjectHeader CurrentProjectHeader { get { return _catrobatContext.CurrentProject.Header; } }

    public ProjectHeader PinProjectHeader { get; set; }

    public ImageSource CurrentProjectScreenshot
    {
      get
      {
        return CurrentProject.ProjectScreenshot as ImageSource;
      }
    }

    public ObservableCollection<ProjectHeader> LocalProjects { get { return _catrobatContext.LocalProjects; } }

    public OnlineProjectHeader SelectedOnlineProject { get; set; }

    private readonly ObservableCollection<OnlineProjectHeader> _onlineProjects = new ObservableCollection<OnlineProjectHeader>();

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
        return StaticApplicationSettings.CurrentApplicationVersion.ToString();
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

        CatrobatContext.GetContext().CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;
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
      Context = new CatrobatContext();
      Context.PropertyChanged += CatrobatContextPropertyChanged;
      Context.CurrentProject.PropertyChanged += CurrentProjectPropertyChanged;

      var themeChooser = Application.Current.Resources["ThemeChooser"] as ThemeChooser;
      if (themeChooser != null)
        themeChooser.PropertyChanged += ThemeChooserPropertyChanged;

      // Commands
      DeleteLocalProjectCommand = new RelayCommand<string>(DeleteLocalProject);
      CopyLocalProjectCommand = new RelayCommand<string>(CopyLocalProject);
      PinLocalProjectCommand = new RelayCommand<ProjectHeader>(PinLocalProject);
      LazyLoadOnlineProjectsCommand = new RelayCommand(LazyLoadOnlineProjects);
      SetCurrentProjectCommand = new RelayCommand<string>(SetCurrentProject);

      if (IsInDesignMode)
        _catrobatContext = new CatrobatContextDesign();
      else
        _catrobatContext = Context;
    }

    private void LoadOnlineProjectsCallback(List<OnlineProjectHeader> projects, bool append)
    {
      if (!append)
      {
        _onlineProjects.Clear();
      }

      IsLoadingOnlineProjects = false;

      foreach (OnlineProjectHeader header in projects)
      {
        _onlineProjects.Add(header);
      }
    }

    public bool _isLoadingOnlineProjects = false;
    public bool IsLoadingOnlineProjects 
    {
      get { return _isLoadingOnlineProjects; }
      set { _isLoadingOnlineProjects = value; RaisePropertyChanged("IsLoadingOnlineProjects"); }
    }
    public void LoadOnlineProjects(bool append)
    {
      if (!IsLoadingOnlineProjects)
      {
        IsLoadingOnlineProjects = true;
        ServerCommunication.LoadOnlineProjects(append, _filterText, _onlineProjects.Count, LoadOnlineProjectsCallback);
      }
      
    }

    private MessageBoxResult _dialogResult;

    public RelayCommand<string> DeleteLocalProjectCommand
    {
      get;
      private set;
    }

    private void DeleteProductMessageCallback(MessageBoxResult result)
    {
      _dialogResult = result;

      if (_dialogResult == MessageBoxResult.OK)
      {
        CatrobatContext.GetContext().DeleteProject(_deleteProductName);
        _deleteProductName = null;
      }
    }

    private string _deleteProductName;
    private void DeleteLocalProject(string projectName)
    {
      _deleteProductName = projectName;

      var message = new DialogMessage(String.Format(MainResources.MainDeleteProjectDialogMessage, projectName), DeleteProductMessageCallback)
      {
        Button = MessageBoxButton.OKCancel,
        Caption = MainResources.MainDeleteProjectDialogTitle
      };

      Messenger.Default.Send(message);
    }

    public RelayCommand<string> CopyLocalProjectCommand
    {
      get;
      private set;
    }

    private void CopyProductMessageCallback(MessageBoxResult result)
    {

      _dialogResult = result;

      if (_dialogResult == MessageBoxResult.OK)
      {
        CatrobatContext.GetContext().CopyProject(_copyProductName);
        _copyProductName = null;
      }
    }

    private string _copyProductName;
    private void CopyLocalProject(string projectName)
    {
      _copyProductName = projectName;

      var message = new DialogMessage(String.Format(MainResources.MainCopyProjectDialogMessage, projectName), CopyProductMessageCallback)
      {
        Button = MessageBoxButton.OKCancel,
        Caption = MainResources.MainCopyProjectDialogTitle
      };

      Messenger.Default.Send(message);
    }

    public RelayCommand<ProjectHeader> PinLocalProjectCommand
    {
      get;
      private set;
    }

    private void PinLocalProject(ProjectHeader project)
    {
      PinProjectHeader = project;
      Navigation.NavigateTo(typeof(TileGeneratorView));
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
      CatrobatContext.GetContext().SetCurrentProject(projectName);
    }

    // This is a fix to the bug that the overwritten RaisePropertyChanged is not working properly
    protected override void RaisePropertyChanged(string propertyName)
    {
      if(PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
