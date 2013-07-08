using System.Collections.ObjectModel;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
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

        public ObservableCollection<ProjectHeader> LocalProjects { get; private set; }

        private void InitCurrentProject()
        {
            var project = new Project
            {
                ApplicationVersionCode = 1,
                ApplicationVersionName = "Version 1",
                DeviceName = "Device1",
                ProjectName = "Project1",
                ScreenHeight = 800,
                ScreenWidth = 480,
                PlatformVersion = "7.1.1", //TODO: Real version
                Platform = "Windows Phone 7.5" //TODO
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

            CurrentProject = project;
        }

        private void InitLocalProjects()
        {
            LocalProjects = new ObservableCollection<ProjectHeader>();

            var project1 = new ProjectHeader
            {
                ProjectName = "Local Project 1"
            };

            var project2 = new ProjectHeader
            {
                ProjectName = "Local Project 2"
            };

            var project3 = new ProjectHeader
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