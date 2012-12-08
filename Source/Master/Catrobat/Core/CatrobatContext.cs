using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using Catrobat.Core.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Storage;

namespace Catrobat.Core
{
    public delegate void ContextSaving();

    public sealed class CatrobatContext : ICatrobatContext, INotifyPropertyChanged
    {
        public static string LocalSettingsFilePath = "Settings/settings";
        public static string DefaultProjectPath = "Resources/default.catroid";
        public static string ProjectsPath = "Projects";
        public static string DefaultProjectName = "DefaultProject";

        public ContextSaving ContextSaving;
        private Project currentProject;
        private ObservableCollection<ProjectHeader> localProjects;


        private CatrobatContext()
        {
            bool firstTimeUse = !RestoreLocalSettings();

            if (Debugger.IsAttached)
            {
                var loader = new SampleProjectLoader();
                loader.LoadSampleProjects();
                UpdateLocalProjects();
            }

            if (firstTimeUse)
            {
                RestoreDefaultProject(DefaultProjectName);
                LocalSettings = new LocalSettings();
                LocalSettings.CurrentProjectName = CurrentProject.ProjectName;
            }
            else
            {
                InitializeLocalSettings();
            }
        }

        public LocalSettings LocalSettings { get; set; }

        public static CatrobatContext Instance
        {
            get { return Nested.instance; }
        }

        public Project CurrentProject
        {
            get { return currentProject; }
            set
            {
                if (currentProject == value)
                    return;

                currentProject = value;
                OnPropertyChanged("CurrentProject");
                OnPropertyChanged("LocalProjects");
                UpdateLocalProjects();
            }
        }

        public ObservableCollection<ProjectHeader> LocalProjects
        {
            get
            {
                if (localProjects == null)
                    UpdateLocalProjects();

                return localProjects;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetCurrentProject(string projectName)
        {
            if (currentProject != null && currentProject.ProjectName == projectName)
                return;

            if (currentProject != null)
                currentProject.Save();

            string projectCodeFile = ProjectsPath + "/" + projectName;

            try
            {
                using (IStorage storage = StorageSystem.GetStorage())
                {
                    string xml = storage.ReadTestFile(projectCodeFile + "/" + Project.ProjectCodePath);
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

            currentProject.DeviceName = DeviceInformationHelper.DeviceName;
            currentProject.Platform = "Windows Phone 7.5"; //TODO: get phone version
            currentProject.PlatformVersion = "7.1.1"; // TODO: ?
            currentProject.ApplicationVersionCode = 0; // TODO: ?
            currentProject.ApplicationVersionName = StaticApplicationSettings.CurrentApplicationVersionName;
            currentProject.ScreenHeight = DeviceInformationHelper.ScreenWidth;
            currentProject.ScreenHeight = DeviceInformationHelper.ScreenHeight;


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

            if (currentProject.ProjectName == projectName)
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

                string xml = storage.ReadTestFile(destinationPath + "/" + Project.ProjectCodePath);
                var newProject = new Project(xml);
                newProject.SetProjectName(newProjectName);
                newProject.Save();
            }

            UpdateLocalProjects();
        }

        public void UpdateLocalProjects()
        {
            if (Instance == null || Instance.CurrentProject == null)
                return;

            if (localProjects == null)
                localProjects = new ObservableCollection<ProjectHeader>();

            localProjects.Clear();

            using (IStorage storage = StorageSystem.GetStorage())
            {
                string[] projectNames = storage.GetDirectoryNames(ProjectsPath + "/*");

                foreach (string projectName in projectNames)
                {
                    if (projectName != Instance.CurrentProject.ProjectName)
                    {
                        byte[] projectScreenshot =
                            storage.LoadImage(ProjectsPath + "/" + projectName + "/" + Project.ScreenshotPath);
                        var projectHeader = new ProjectHeader
                            {
                                ProjectName = projectName,
                                Screenshot = projectScreenshot
                            };
                        localProjects.Add(projectHeader);
                    }
                }
            }
        }

        internal void StoreLocalSettings()
        {
            LocalSettings.CurrentProjectName = currentProject.ProjectName;

            if (ContextSaving != null)
                ContextSaving.Invoke();

            using (IStorage storage = StorageSystem.GetStorage())
            {
                storage.WriteSerializableObject(LocalSettingsFilePath, LocalSettings);

                //using (Stream fileStream = storage.OpenFile(LocalSettingsFilePath, StorageFileMode.Create, StorageFileAccess.ReadWrite))
                //{
                //  DataContractSerializer serializer = new DataContractSerializer(typeof(LocalSettings));
                //  serializer.WriteObject(fileStream, LocalSettings);
                //  fileStream.Close();
                //}
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
                        storage.ReadSerializableObject(LocalSettingsFilePath, typeof (LocalSettings));

                        //using (Stream fileStream = storage.OpenFile(LocalSettingsFilePath, StorageFileMode.Open, StorageFileAccess.ReadWrite))
                        //{
                        //  DataContractSerializer serializer = new DataContractSerializer(typeof(LocalSettings));
                        //  LocalSettings = (LocalSettings)serializer.ReadObject(fileStream);
                        //  fileStream.Close();
                        //}
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
            if (currentProject != null)
                currentProject.Save();

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
#if SILVERLIGHT
            Stream stream = Application.GetResourceStream(new Uri("/MetroCatData;component/" + DefaultProjectPath, UriKind.Relative)).Stream;
            CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(stream, projectCodeFile);
          #else
                    // TODO: implement me
#endif
                }

                string xml = storage.ReadTestFile(projectCodeFile + "/" + Project.ProjectCodePath);
                CurrentProject = new Project(xml);

                CurrentProject.SetProjectName(projectName);
            }
        }


        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(property));
        }

        private class Nested
        {
            internal static readonly CatrobatContext instance = new CatrobatContext();

            static Nested()
            {
            }
        }
    }
}