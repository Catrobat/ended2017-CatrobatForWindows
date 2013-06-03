using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using System.Collections.Generic;

namespace Catrobat.Core
{
  public delegate void ContextSaving();

  public sealed class CatrobatContext : ICatrobatContext, INotifyPropertyChanged
  {
    public static string PlayerActiveProjectZipPath = "ActivePlayerProject/ActiveProject.catrobat";
    public static string LocalSettingsFilePath = "Settings/settings";
    public static string DefaultProjectPath = "default.catroid";
    public static string ProjectsPath = "Projects";
    public static string DefaultProjectName = "DefaultProject";

    public ContextSaving ContextSaving;
    private Project _currentProject;
    private ObservableCollection<ProjectHeader> _localProjects;


    public CatrobatContext()
    {
      bool firstTimeUse = !RestoreLocalSettings();

      if (firstTimeUse)
      {
        if (Debugger.IsAttached)
        {
          var loader = new SampleProjectLoader();
          loader.LoadSampleProjects();
          UpdateLocalProjects();
        }

        RestoreDefaultProject(DefaultProjectName);
        LocalSettings = new LocalSettings {CurrentProjectName = CurrentProject.ProjectName};
      }
      else
      {
        InitializeLocalSettings();
      }
    }

    public static void SetContextHolder(IContextHolder holder)
    {
      _holder = holder;
    }

    private static IContextHolder _holder;
    public static CatrobatContext GetContext()
    {
      return _holder.GetContext();
    }

    public LocalSettings LocalSettings { get; set; }

    public string CurrentToken
    {
      get
      {
        return LocalSettings.CurrentToken;
      }

      set
      {
        if (LocalSettings.CurrentToken == value)
          return;

        LocalSettings.CurrentToken = value;
        this.OnPropertyChanged("CurrentToken");
      }
    }

    public string CurrentUserEmail
    {
      get
      {
        return LocalSettings.CurrentUserEmail;
      }

      set
      {
        if (LocalSettings.CurrentUserEmail == value)
          return;

        LocalSettings.CurrentUserEmail = value;
        this.OnPropertyChanged("CurrentUserEmail");
      }
    }

    public Project CurrentProject
    {
      get { return _currentProject; }
      set
      {
        if (_currentProject == value)
          return;

        _currentProject = value;
        OnPropertyChanged("CurrentProject");
        OnPropertyChanged("LocalProjects");
        UpdateLocalProjects();
      }
    }

    public ObservableCollection<ProjectHeader> LocalProjects
    {
      get
      {
        if (_localProjects == null)
          UpdateLocalProjects();

        return _localProjects;
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void SetCurrentProject(string projectName)
    {
      if (_currentProject != null && _currentProject.ProjectName == projectName)
        return;

      if (_currentProject != null)
        _currentProject.Save();

      string projectCodeFile = ProjectsPath + "/" + projectName;

      try
      {
        using (IStorage storage = StorageSystem.GetStorage())
        {
          string xml = storage.ReadTextFile(projectCodeFile + "/" + Project.ProjectCodePath);
          CurrentProject = new Project(xml);
        }
      }
      catch
      {
        throw new Exception("Project not exists");
      }
    }

    public void CreateNewProject(string projectName)
    {
      RestoreDefaultProject(projectName);

      _currentProject.DeviceName = DeviceInformationHelper.DeviceName;
      _currentProject.Platform = "Windows Phone 7.5"; //TODO: get phone version
      _currentProject.PlatformVersion = "7.1.1"; // TODO: ?
      _currentProject.ApplicationVersionCode = 0; // TODO: ?
      _currentProject.ApplicationVersionName = StaticApplicationSettings.CurrentApplicationVersionName;
      _currentProject.ScreenHeight = DeviceInformationHelper.ScreenWidth;
      _currentProject.ScreenHeight = DeviceInformationHelper.ScreenHeight;


      // TODO: set other project properties here
      CurrentProject.Save();
      UpdateLocalProjects();
    }

    public void DeleteProject(string projectName)
    {
      using (IStorage storage = StorageSystem.GetStorage())
      {
        storage.DeleteDirectory(ProjectsPath + "/" + projectName);
      }

      if (_currentProject.ProjectName == projectName)
      {
        RestoreDefaultProject(DefaultProjectName);
      }

      UpdateLocalProjects();
    }

    public void CopyProject(string projectName)
    {
      using (IStorage storage = StorageSystem.GetStorage())
      {
        string sourcePath = ProjectsPath + "/" + projectName;
        string newProjectName = projectName;
        string destinationPath = ProjectsPath + "/" + newProjectName;

        int counter = 1;
        while (storage.DirectoryExists(destinationPath))
        {
          newProjectName = projectName + counter;
          destinationPath = ProjectsPath + "/" + newProjectName;
          counter++;
        }

        storage.CopyDirectory(sourcePath, destinationPath);

        string xml = storage.ReadTextFile(destinationPath + "/" + Project.ProjectCodePath);
        var newProject = new Project(xml);
        newProject.SetProjectName(newProjectName);
        newProject.Save();
      }

      UpdateLocalProjects();
    }

    public void UpdateLocalProjects()
    {
      if (CurrentProject == null)
        return;

      if (_localProjects == null)
        _localProjects = new ObservableCollection<ProjectHeader>();

      _localProjects.Clear();

      using (IStorage storage = StorageSystem.GetStorage())
      {
        string[] projectNames = storage.GetDirectoryNames(ProjectsPath);

        var projects = new List<ProjectHeader>();

        foreach (string projectName in projectNames)
        {
          if (projectName != CurrentProject.ProjectName)
          {
            object projectScreenshot =  storage.LoadImage(ProjectsPath + "/" + projectName + "/" + Project.ScreenshotPath);
            var projectHeader = new ProjectHeader
            {
              ProjectName = projectName,
              Screenshot = projectScreenshot
            };
            projects.Add(projectHeader);
          }
        }
        projects.Sort();
        foreach (ProjectHeader header in projects)
          _localProjects.Add(header);
      }

    }

    internal void StoreLocalSettings()
    {
      LocalSettings.CurrentProjectName = _currentProject.ProjectName;

      if (ContextSaving != null)
        ContextSaving.Invoke();

      using (IStorage storage = StorageSystem.GetStorage())
      {
        storage.WriteSerializableObject(LocalSettingsFilePath, LocalSettings);
      }
    }

    internal bool RestoreLocalSettings()
    {
      try
      {
        using (IStorage storage = StorageSystem.GetStorage())
        {
          if (storage.FileExists(LocalSettingsFilePath))
          {
            LocalSettings = storage.ReadSerializableObject(LocalSettingsFilePath, typeof(LocalSettings)) as LocalSettings;
          }
          else
          {
            return false;
          }
        }
      }
      catch
      {
        return false;
      }

      return true;
    }

    public void Save()
    {
      if (_currentProject != null)
        _currentProject.Save();

      StoreLocalSettings();
    }

    private void InitializeLocalSettings()
    {
      SetCurrentProject(LocalSettings.CurrentProjectName);
    }

    internal void RestoreDefaultProject(string projectName)
    {
      using (IStorage storage = StorageSystem.GetStorage())
      {
        string projectCodeFile = ProjectsPath + "/" + projectName;

        if (!storage.FileExists(projectCodeFile))
        {
          using (var resourceLoader = ResourceLoader.CreateResourceLoader())
          {
            Stream stream = resourceLoader.OpenResourceStream(ResourceScope.Resources, DefaultProjectPath);
            CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectCodeFile);
            stream.Dispose();
          }
        }

        string xml = storage.ReadTextFile(projectCodeFile + "/" + Project.ProjectCodePath);
        CurrentProject = new Project(xml);

        CurrentProject.SetProjectName(projectName);
      }
    }

    public void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite)
    {
      foreach (Script script in selectedSprite.Scripts.Scripts)
        foreach (Brick brick in script.Bricks.Bricks)
          if (brick is SetCostumeBrick)
            (brick as SetCostumeBrick).UpdateReference();
    }

    public void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite)
    {
      foreach (Script script in selectedSprite.Scripts.Scripts)
        foreach (Brick brick in script.Bricks.Bricks)
          if (brick is PlaySoundBrick)
            (brick as PlaySoundBrick).UpdateReference();
    }

    public void CleanUpSpriteReferences(Sprite deletedSprite)
    {
      foreach (Sprite sprite in _currentProject.SpriteList.Sprites)
        foreach (Script script in sprite.Scripts.Scripts)
          foreach (Brick brick in script.Bricks.Bricks)
            if (brick is PointToBrick)
              (brick as PointToBrick).UpdateReference();
    }

    private void OnPropertyChanged(string property)
    {
      if (PropertyChanged != null)
        PropertyChanged(this, new PropertyChangedEventArgs(property));
    }
  }
}