using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Tests.Misc
{
    public class ProjectGeneratorForScriptBrickCollectionTests : IProjectGenerator
    {
        public Project GenerateProject()
        {
            var project = new Project
            {
                ProjectHeader = new ProjectHeader
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 0,
                    ApplicationName = "Pocket Code",
                    ApplicationVersion = "0.0.1",
                    CatrobatLanguageVersion = "Win0.08",
                    DateTimeUpload = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Description = "",
                    DeviceName = "SampleDevice",
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    Platform = ServiceLocator.SystemInformationService.PlatformName,
                    PlatformVersion = ServiceLocator.SystemInformationService.PlatformVersion,
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    RemixOf = "",
                    ScreenHeight = 480,
                    ScreenWidth = 800,
                    Tags = "",
                    Url = "http://pocketcode.org/details/871",
                    UserHandle = ""
                }
            };

            project.ProjectHeader.SetProgramName("project");

            project.SpriteList = new SpriteList();

            var scripts1 = new ObservableCollection<Script>
            {
                new StartScript()
                { 
                    Bricks = 
                    new BrickList
                    {
                        Bricks = new ObservableCollection<Brick>
                        {
                            new SetCostumeBrick()
                        }
                    }
                }
            };

            var scripts2 = new ObservableCollection<Script>
            {
                new StartScript()
                { 
                    Bricks = 
                    new BrickList
                    {
                        Bricks = new ObservableCollection<Brick>
                        {
                            new SetCostumeBrick(),
                        }
                    }
                },
                new WhenScript()
                { 
                    Bricks = 
                    new BrickList
                    {
                        Bricks = new ObservableCollection<Brick>
                        {
                            new SetCostumeBrick(),
                            new WaitBrick(),
                            new SetCostumeBrick(),
                            new WaitBrick(),
                            new SetCostumeBrick()
                        }
                    }
                }
            };

            var object1 = new Sprite { Scripts = new ScriptList { Scripts = scripts1 } };
            var object2 = new Sprite { Scripts = new ScriptList { Scripts = scripts2 } };

            project.SpriteList = new SpriteList
            {
                Sprites = new ObservableCollection<Sprite>
                    {
                        object1, object2
                    }
            };

            return project;
        }
    }
}
