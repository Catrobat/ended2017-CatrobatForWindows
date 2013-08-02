using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Catrobat.Core.Annotations;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core
{
    public delegate void ContextSaving();

    public sealed class CatrobatContext : CatrobatContextBase
    {
        public  CatrobatContext()
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

        public override void SetCurrentProject(string projectName)
        {
            if (CurrentProjectField != null && CurrentProjectField.ProjectHeader.ProgramName == projectName)
            {
                return;
            }

            if (CurrentProjectField != null)
            {
                CurrentProjectField.Save();
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

        public override void CreateNewProject(string projectName)
        {
            RestoreDefaultProject(projectName);

            CurrentProject.Save();
            UpdateLocalProjects();
        }

        public override void DeleteProject(string projectName)
        {
            using (var storage = StorageSystem.GetStorage())
            {
                storage.DeleteDirectory(ProjectsPath + "/" + projectName);
            }

            if (CurrentProjectField.ProjectHeader.ProgramName == projectName)
            {
                RestoreDefaultProject(DefaultProjectName);
            }

            UpdateLocalProjects();
        }

        public override void CopyProject(string projectName)
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
                newProject.SetProgramName(newProjectName);
                newProject.Save();
            }

            UpdateLocalProjects();
        }

        public override void UpdateLocalProjects()
        {
            if (CurrentProject == null)
            {
                return;
            }

            if (LocalProjectsField == null)
            {
                LocalProjectsField = new ObservableCollection<ProjectDummyHeader>();
            }

            LocalProjectsField.Clear();

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
                    LocalProjectsField.Add(header);
                }
            }
        }

        public override void StoreLocalSettings()
        {
            LocalSettings.CurrentProjectName = CurrentProjectField.ProjectHeader.ProgramName;

            if (ContextSaving != null)
            {
                ContextSaving.Invoke();
            }

            using (var storage = StorageSystem.GetStorage())
            {
                storage.WriteSerializableObject(LocalSettingsFilePath, LocalSettings);
            }
        }

        public override bool RestoreLocalSettings()
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

        public override void Save()
        {
            if (CurrentProjectField != null)
            {
                CurrentProjectField.Save();
            }

            StoreLocalSettings();
        }

        public override void InitializeLocalSettings()
        {
            SetCurrentProject(LocalSettings.CurrentProjectName);
        }

        public override void RestoreDefaultProject(string projectName)
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

                CurrentProject.SetProgramName(projectName);
            }
        }

        public override void CleanUpCostumeReferences(Costume deletedCostume, Sprite selectedSprite)
        {
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
        }

        public override void CleanUpSoundReferences(Sound deletedSound, Sprite selectedSprite)
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

        public override void CleanUpSpriteReferences(Sprite deletedSprite)
        {
            foreach (Sprite sprite in CurrentProjectField.SpriteList.Sprites)
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

        public override void CleanUpVariableReferences(UserVariable deletedUserVariable, Sprite selectedSprite)
        {
            foreach (Script script in selectedSprite.Scripts.Scripts)
            {
                foreach (Brick brick in script.Bricks.Bricks)
                {
                    if (brick is SetVariableBrick)
                    {
                        (brick as SetVariableBrick).UserVariableReference = null;
                    }
                    else if (brick is ChangeVariableBrick)
                    {
                        (brick as ChangeVariableBrick).UserVariableReference = null;
                    }
                }
            }
        }
    }
}