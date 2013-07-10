using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;

namespace Catrobat.Core
{
    public sealed class CatrobatContextDesign : ICatrobatContext
    {
        public CatrobatContextDesign()
        {
            InitCurrentProject();
            InitLocalProjects();
            InitOnlineProjects();
        }

        public ObservableCollection<OnlineProjectHeader> OnlineProjects { get; private set; }

        public Project CurrentProject { get; set; }

        public ObservableCollection<ProjectDummyHeader> LocalProjects { get; private set; }

        private void InitCurrentProject()
        {
            var project = new Project();

            project.ProjectHeader = new ProjectHeader
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 0,
                    ApplicationName = "Pocket Code",
                    ApplicationVersion = "0.8.2",
                    CatrobatLanguageVersion = (float)0.8,
                    DateTimeUpload = "",
                    Description = "Dies ist eine Test Anwendung.",
                    DeviceName = "Device 1",
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    Platform = "Windows Phone",
                    PlatformVersion = "8.0",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    ProgramName = "Program Name",
                    RemixOf = "",
                    ScreenHeight = 1280,
                    ScreenWidth = 720,
                    Tags = "",
                    Url = "http://pocketcode.org/details/871",
                    UserHandle = "Username"
                };

            // TODO: implement other design data here

            var sprites = new SpriteList(project);
            var sprite = new Sprite(project);
            sprite.Name = "Object 1";

            sprite.Costumes = new CostumeList(sprite);
            var costume = new Costume("Cat", sprite);
            var image = new byte[0]; //new BitmapImage(new Uri(costume.FileName, UriKind.Relative)); // TODO: fix me
            //costume.Image = image;
            sprite.Costumes.Costumes.Add(costume);

            sprite.Sounds = new SoundList(sprite);
            var sound = new Sound("Miau_Sound", sprite);
            sprite.Sounds.Sounds.Add(sound);

            sprite.Scripts = new ScriptList(sprite);
            Script startScript = new StartScript(sprite);

            startScript.Bricks = new BrickList(sprite);
            var setCostumeBrick = new SetCostumeBrick(sprite);
            var costumeRef = new CostumeReference(sprite);
            costumeRef.Costume = costume;
            //setCostumeBrick.Costume = costumeRef;

            //TODO: Add more Bricks if you need them

            sprites.Sprites.Add(sprite);
            project.SpriteList = sprites;

            CurrentProject = project;
        }

        private void InitLocalProjects()
        {
            LocalProjects = new ObservableCollection<ProjectDummyHeader>();

            var project1 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 1"
            };

            var project2 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 2"
            };

            var project3 = new ProjectDummyHeader
            {
                ProjectName = "Local Project 3"
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
                ProjectName = "Online Project 1"
            };

            var project2 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 2"
            };

            var project3 = new OnlineProjectHeader
            {
                ProjectName = "Online Project 3"
            };

            OnlineProjects.Add(project1);
            OnlineProjects.Add(project2);
            OnlineProjects.Add(project3);
        }
    }
}