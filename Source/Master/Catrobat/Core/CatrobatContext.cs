using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core
{
    public delegate void ContextSaving();

    public sealed class CatrobatContext : ICatrobatContext, INotifyPropertyChanged
    {
        public const string PlayerActiveProjectZipPath = "ActivePlayerProject/ActiveProject.catrobat_from_ide";
        public const string LocalSettingsFilePath = "Settings/settings";
        public const string DefaultProjectPath = "default.catroid";
        public const string ProjectsPath = "Projects";
        public const string DefaultProjectName = "DefaultProject";
        public const string TempProjectImportZipPath = "Temp/ImportProjectZip";
        public const string TempProjectImportPath = "Temp/ImportProject";

        public const string TempPaintImagePath = "Temp/PaintImage";


        public ContextSaving ContextSaving;
        private Project _currentProject;
        private ObservableCollection<ProjectDummyHeader> _localProjects;


        public CatrobatContext()
        {
            var firstTimeUse = !RestoreLocalSettings();

            if (firstTimeUse)
            {
                if (Debugger.IsAttached)
                {
                    var loader = new SampleProjectLoader();
                    loader.LoadSampleProjects();
                    UpdateLocalProjects();
                }

                RestoreDefaultProject(DefaultProjectName);
                LocalSettings = new LocalSettings {CurrentProjectName = CurrentProject.ProjectHeader.ProgramName};
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

        public LocalSettings LocalSettings { get; private set; }

        public string CurrentToken
        {
            get { return LocalSettings.CurrentToken; }

            set
            {
                if (LocalSettings.CurrentToken == value)
                {
                    return;
                }

                LocalSettings.CurrentToken = value;
                OnPropertyChanged("CurrentToken");
            }
        }

        public string CurrentUserEmail
        {
            get { return LocalSettings.CurrentUserEmail; }

            set
            {
                if (LocalSettings.CurrentUserEmail == value)
                {
                    return;
                }

                LocalSettings.CurrentUserEmail = value;
                OnPropertyChanged("CurrentUserEmail");
            }
        }

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (_currentProject == value)
                {
                    return;
                }

                _currentProject = value;
                OnPropertyChanged("CurrentProject");
                OnPropertyChanged("LocalProjects");
                UpdateLocalProjects();
            }
        }

        public ObservableCollection<ProjectDummyHeader> LocalProjects
        {
            get
            {
                if (_localProjects == null)
                {
                    UpdateLocalProjects();
                }

                return _localProjects;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetCurrentProject(string projectName)
        {
            if (_currentProject != null && _currentProject.ProjectHeader.ProgramName == projectName)
            {
                return;
            }

            if (_currentProject != null)
            {
                _currentProject.Save();
            }

            var projectCodeFile = Path.Combine(ProjectsPath, projectName);

            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    var tempPath = Path.Combine(projectCodeFile, Project.ProjectCodePath);
                    var xml = storage.ReadTextFile(tempPath);
                    var newProject = new Project(xml);
                    CurrentProject = newProject;
                }
            }
            catch
            {
                throw new Exception("Project does not exist");
            }
        }

        public void CreateNewProject(string projectName)
        {
            RestoreDefaultProject(projectName);

            var projectHeader = new ProjectHeader
            {
                ApplicationBuildName = "",
                ApplicationBuildNumber = 0,
                ApplicationName = "Pocket Code",
                ApplicationVersion = "0.0.1",
                CatrobatLanguageVersion = (float)0.8,
                DateTimeUpload = "",
                Description = "",
                DeviceName = DeviceInformationHelper.DeviceName,
                MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                Platform = PlatformInformationHelper.GetPlatformName(),
                PlatformVersion = PlatformInformationHelper.GetPlatformVersion(),
                ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                ProgramName = "",
                RemixOf = "",
                ScreenHeight = DeviceInformationHelper.ScreenHeight,
                ScreenWidth = DeviceInformationHelper.ScreenWidth,
                Tags = "",
                Url = "http://pocketcode.org/details/871",
                UserHandle = ""
            };

            CurrentProject.Save();
            UpdateLocalProjects();
        }

        public void DeleteProject(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(ProjectsPath + "/" + projectName);
            }

            if (_currentProject.ProjectHeader.ProgramName == projectName)
            {
                RestoreDefaultProject(DefaultProjectName);
            }

            UpdateLocalProjects();
        }

        public void CopyProject(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var sourcePath = Path.Combine(ProjectsPath, projectName);
                var newProjectName = projectName;
                var destinationPath = Path.Combine(ProjectsPath, newProjectName);

                var counter = 1;
                while (storage.DirectoryExists(destinationPath))
                {
                    newProjectName = projectName + counter;
                    destinationPath = Path.Combine(ProjectsPath, newProjectName);
                    counter++;
                }

                storage.CopyDirectory(sourcePath, destinationPath);

                var tempXmlPath = Path.Combine(destinationPath, Project.ProjectCodePath);
                var xml = storage.ReadTextFile(tempXmlPath);
                var newProject = new Project(xml);
                newProject.SetSetProgramName(newProjectName);
                newProject.Save();
            }

            UpdateLocalProjects();
        }

        public void UpdateLocalProjects()
        {
            if (CurrentProject == null)
            {
                return;
            }

            if (_localProjects == null)
            {
                _localProjects = new ObservableCollection<ProjectDummyHeader>();
            }

            _localProjects.Clear();

            using (var storage = StorageSystem.GetStorage())
            {
                var projectNames = storage.GetDirectoryNames(ProjectsPath);

                var projects = new List<ProjectDummyHeader>();

                foreach (string projectName in projectNames)
                {
                    if (projectName != CurrentProject.ProjectHeader.ProgramName)
                    {
                        var screenshotPath = Path.Combine(ProjectsPath, projectName, Project.ScreenshotPath);
                        var projectScreenshot = storage.LoadImage(screenshotPath);
                        var projectHeader = new ProjectDummyHeader
                        {
                            ProjectName = projectName,
                            Screenshot = projectScreenshot
                        };
                        projects.Add(projectHeader);
                    }
                }
                projects.Sort();
                foreach (ProjectDummyHeader header in projects)
                {
                    _localProjects.Add(header);
                }
            }
        }

        internal void StoreLocalSettings()
        {
            LocalSettings.CurrentProjectName = _currentProject.ProjectHeader.ProgramName;

            if (ContextSaving != null)
            {
                ContextSaving.Invoke();
            }

            using (var storage = StorageSystem.GetStorage())
            {
                storage.WriteSerializableObject(LocalSettingsFilePath, LocalSettings);
            }
        }

        internal bool RestoreLocalSettings()
        {
            try
            {
                using (var storage = StorageSystem.GetStorage())
                {
                    if (storage.FileExists(LocalSettingsFilePath))
                    {
                        LocalSettings = storage.ReadSerializableObject(LocalSettingsFilePath, typeof (LocalSettings)) as LocalSettings;
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
            {
                _currentProject.Save();
            }

            StoreLocalSettings();
        }

        private void InitializeLocalSettings()
        {
            SetCurrentProject(LocalSettings.CurrentProjectName);
        }

        internal void RestoreDefaultProject(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                var projectCodeFile = Path.Combine(ProjectsPath, projectName);

                if (!storage.FileExists(projectCodeFile))
                {
                    using (var resourceLoader = ResourceLoader.CreateResourceLoader())
                    {
                        var stream = resourceLoader.OpenResourceStream(ResourceScope.Resources, DefaultProjectPath);
                        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectCodeFile);
                        stream.Dispose();
                    }
                }

                var tempXmlPath = Path.Combine(projectCodeFile, Project.ProjectCodePath);
                var xml = storage.ReadTextFile(tempXmlPath);
                CurrentProject = new Project(xml);

                CurrentProject.SetSetProgramName(projectName);
            }
        }

        public void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite)
        {
            ReferenceHelper.Project = CurrentProject;

            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is SetCostumeBrick)
                    {
                        (brick as SetCostumeBrick).CostumeReference = null;
                    }
                }
            }

            ReferenceHelper.Project = null;
        }

        public void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite)
        {
            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is PlaySoundBrick)
                    {
                        (brick as PlaySoundBrick).SoundReference = null;
                    }
                }
            }
        }

        public void CleanUpSpriteReferences(Sprite deletedSprite)
        {
            foreach (Sprite sprite in _currentProject.SpriteList.Sprites)
            {
                foreach (Script script in sprite.Scripts.Scripts)
                {
                    foreach (Brick brick in script.Bricks.Bricks)
                    {
                        if (brick is PointToBrick)
                        {
                            (brick as PointToBrick).PointedSpriteReference = null;
                        }
                    }
                }
            }
        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}