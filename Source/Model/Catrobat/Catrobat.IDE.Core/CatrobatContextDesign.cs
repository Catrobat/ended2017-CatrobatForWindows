using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Xml;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Bricks.Costumes;
using Catrobat.IDE.Core.Xml.XmlObjects.Scripts;
using Catrobat.IDE.Core.Xml.XmlObjects.Variables;

namespace Catrobat.IDE.Core
{
    public sealed class CatrobatContextDesign : CatrobatContextBase
    {
        #region Private members

        private XmlProject _currentProject;
        private Project _currentProject2;

        #endregion

        #region Properties

        [Obsolete("Use CurrentProject2 instead. ")]
        public XmlProject CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (_currentProject == value) return;

                _currentProject = value;
                _currentProject2 = new XmlProjectConverter().Convert(_currentProject);
                RaisePropertyChanged(() => CurrentProject);
            }
        }

        public Project CurrentProject2
        {
            get { return _currentProject2; }
            set
            {
                if (ReferenceEquals(_currentProject2, value)) return;
                _currentProject2 = value;
                RaisePropertyChanged(() => CurrentProject2);
            }
        }

        public ObservableCollection<ProjectDummyHeader> LocalProjects { get; private set; }
        public ObservableCollection<OnlineProjectHeader> OnlineProjects { get; private set; }

        #endregion

        public CatrobatContextDesign()
        {
            InitCurrentProject();
            InitLocalProjects();
            InitOnlineProjects();

            LocalSettings = new LocalSettings
            {
                CurrentLanguageString = "en",
                CurrentProjectName = "DefaultProject",
                CurrentThemeIndex = 0,
                CurrentToken = "DummyToken",
                CurrentUserEmail = "dummy@somedomain.com"
            };
        }

        private void InitCurrentProject()
        {
            var project = new XmlProject
            {
                ProjectHeader = new XmlProjectHeader(false)
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 0,
                    ApplicationName = "Pocket Code",
                    ApplicationVersion = "0.8.2",
                    CatrobatLanguageVersion = "Win0.8",
                    DateTimeUpload = "",
                    Description = "Dies ist eine Test Anwendung.",
                    DeviceName = "Device 1",
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    Platform = "Windows Phone",
                    PlatformVersion = "8.0",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    ProgramName = "Project 1 with very very very very very long name",
                    RemixOf = "",
                    ScreenHeight = 1280,
                    ScreenWidth = 720,
                    Tags = "",
                    Url = "http://pocketcode.org/details/871",
                    UserHandle = "Username"
                }
            };

            project.SpriteList = new XmlSpriteList();

            var sprite = new XmlSprite { Name = "Object 1" };

            var costumes = new XmlCostumeList
                {
                    Costumes = new ObservableCollection<XmlCostume>{new XmlCostume{Name = "Cat"}}
                };
            sprite.Costumes = costumes;

            var sounds = new XmlSoundList
                {
                    Sounds = new ObservableCollection<XmlSound> {new XmlSound {Name = "Miau Sound"}}
                };
            sprite.Sounds = sounds;

            var scripts = new XmlScriptList();

            var script = new XmlStartScript();

            var setCostumeBrick = new XmlSetCostumeBrick
                {
                    Costume = sprite.Costumes.Costumes[0],
                };

            script.Bricks.Bricks.Add(setCostumeBrick);
            scripts.Scripts.Add(script);
            sprite.Scripts = scripts;
            project.SpriteList.Sprites.Add(sprite);

            project.VariableList = new XmlVariableList();

            var programVariableList = new XmlProgramVariableList();
            var globalVariables = new ObservableCollection<XmlUserVariable>
            { 
                new XmlUserVariable { Name = "GlobalVariable 1" }, 
                new XmlUserVariable { Name = "GlobalVariable 2" }
            };
            programVariableList.UserVariables = globalVariables;

            var objectVariableList = new XmlObjectVariableList();
            var entries = new ObservableCollection<XmlObjectVariableEntry>
                {
                    new XmlObjectVariableEntry
                    {
                        XmlSpriteReference = new XmlSpriteReference{Sprite = sprite},
                        VariableList = new XmlUserVariableList{UserVariables = new ObservableCollection<XmlUserVariable>
                        {
                            new XmlUserVariable {Name = "LocalVariable 1"},
                            new XmlUserVariable {Name = "LocalVariable 2"}
                        }}
                    }
                };
            objectVariableList.ObjectVariableEntries = entries;

            project.VariableList.ObjectVariableList = objectVariableList;
            project.VariableList.ProgramVariableList = programVariableList;

            CurrentProject = project;
        }

        private void InitLocalProjects()
        {
            LocalProjects = new ObservableCollection<ProjectDummyHeader>();

            // TODO: add sample images

            var project1 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 2 with very very very very very long name"
            };

            var project2 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 3"
            };

            var project3 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 4"
            };


            LocalProjects.Add(project1);
            LocalProjects.Add(project2);
            LocalProjects.Add(project3);
        }

        private void InitOnlineProjects()
        {
            OnlineProjects = new ObservableCollection<OnlineProjectHeader>();

            var project1 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 1 with very very very very very long name",
                Description = "That is a nice description of Project 1. That is a nice description of Project 1. That is a nice description of Project 1"
            };

            var project2 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 2",
                Description = "That is a nice description of Project 2."
            };

            var project3 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 3",
                Description = "That is a nice description of Project 3."
            };

            OnlineProjects.Add(project1);
            OnlineProjects.Add(project2);
            OnlineProjects.Add(project3);
        }

        private void InitVariables()
        {
            //TODO: init variables
        }
    }
}