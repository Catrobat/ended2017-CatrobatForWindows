using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;
using Catrobat.IDE.Core.Services;
using System.Collections.ObjectModel;

namespace Catrobat.IDE.Core
{
    public sealed class CatrobatContextDesign : CatrobatContextBase
    {
        #region Private members

        private Project _currentProject;

        #endregion

        #region Properties

        public Project CurrentProject
        {
            get { return _currentProject; }
            set
            {
                if (ReferenceEquals(_currentProject, value)) return;
                _currentProject = value;

                ServiceLocator.DispatcherService.RunOnMainThread(() => 
                    RaisePropertyChanged(() => CurrentProject));
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
            var catCostume = new Costume {Name = "Cat"};
            CurrentProject = new Project
            {
                Name = "Project 1 with very very very very very long name",
                Description = "Dies ist eine Test Anwendung.",
                UploadHeader = new UploadHeader
                {
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    RemixOf = "",
                    Tags = new ObservableCollection<string>(),
                    Url = "http://pocketcode.org/details/871",
                    UserId = "Username"
                },
                Sprites = new ObservableCollection<Sprite>
                {
                    new Sprite
                    {
                        Name = "Object 1",
                        Costumes = new ObservableCollection<Costume> {catCostume},
                        Sounds = new ObservableCollection<Sound> {new Sound {Name = "Miau Sound"}},
                        LocalVariables = new ObservableCollection<LocalVariable>
                        {
                            new LocalVariable {Name = "LocalVariable 1"},
                            new LocalVariable {Name = "LocalVariable 2"}
                        },
                        Scripts = new ObservableCollection<Script>
                        {
                            new StartScript
                            {
                                Bricks = new ObservableCollection<Brick>
                                {
                                    new SetCostumeBrick
                                    {
                                        Value = catCostume,
                                    }
                                }
                            }
                        }
                    }
                },
                GlobalVariables = new ObservableCollection<GlobalVariable>
                {
                    new GlobalVariable {Name = "GlobalVariable 1"},
                    new GlobalVariable {Name = "GlobalVariable 2"}
                }
            };
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
    }
}