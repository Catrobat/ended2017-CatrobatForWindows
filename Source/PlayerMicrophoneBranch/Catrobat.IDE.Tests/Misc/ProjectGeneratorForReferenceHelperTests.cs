using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Tests.Misc
{
    public class ProjectGeneratorForReferenceHelperTests : ITestProjectGenerator
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

            project.ProjectHeader.SetProgramName("program1");

            AddSprites(project);

            return project;
        }

        private void AddSprites(Project project)
        {
            var looks1 = new ObservableCollection<Costume>
            {
                new Costume{ Name="background", FileName = "5A71C6F41035979503BA294F78A09336_background" }
            };

            var sounds1 = new ObservableCollection<Sound>
            {
                /* No Sound here */
            };



            var looks2 = new ObservableCollection<Costume>
            {
                new Costume{ Name="normalCat", FileName = "34A109A82231694B6FE09C216B390570_normalCat" },
                new Costume{ Name="banzaiCat", FileName = "395CD6389BD601812BDB299934A0CCB4_banzaiCat" },
                new Costume{ Name="cheshireCat", FileName = "B4497E87AC34B1329DD9B14C08EEAFF0_cheshireCat" }
            };

            var sounds2 = new ObservableCollection<Sound>
            {
                new Sound{ Name = "StarlightGameOver", FileName = "FD7B62A7B84AAE3F11F76868D0C2159A_StarlightGameOver"}
            };


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
                            new SetCostumeBrick{ Costume = looks1[0]},
                            new SetVariableBrick(), // TODO
                            new IfLogicBeginBrick(),
                            new IfLogicBeginBrick(),
                            new IfLogicElseBrick(),
                            new IfLogicEndBrick(),
                            new IfLogicElseBrick(),
                            new IfLogicEndBrick()
                        }
                    }
                }
            };

            ((IfLogicBeginBrick) scripts1[0].Bricks.Bricks[2]).IfLogicElseBrick =
                (IfLogicElseBrick) scripts1[0].Bricks.Bricks[6];
            ((IfLogicBeginBrick)scripts1[0].Bricks.Bricks[2]).IfLogicEndBrick =
                (IfLogicEndBrick)scripts1[0].Bricks.Bricks[7];

            ((IfLogicBeginBrick)scripts1[0].Bricks.Bricks[3]).IfLogicElseBrick =
                (IfLogicElseBrick)scripts1[0].Bricks.Bricks[4];
            ((IfLogicBeginBrick)scripts1[0].Bricks.Bricks[3]).IfLogicEndBrick =
                (IfLogicEndBrick)scripts1[0].Bricks.Bricks[5];

            ((IfLogicElseBrick)scripts1[0].Bricks.Bricks[4]).IfLogicBeginBrick =
                (IfLogicBeginBrick)scripts1[0].Bricks.Bricks[3];
            ((IfLogicElseBrick)scripts1[0].Bricks.Bricks[4]).IfLogicEndBrick =
                (IfLogicEndBrick)scripts1[0].Bricks.Bricks[5];

            ((IfLogicEndBrick)scripts1[0].Bricks.Bricks[5]).IfLogicBeginBrick =
                (IfLogicBeginBrick)scripts1[0].Bricks.Bricks[3];
            ((IfLogicEndBrick)scripts1[0].Bricks.Bricks[5]).IfLogicElseBrick =
                (IfLogicElseBrick)scripts1[0].Bricks.Bricks[4];

            ((IfLogicElseBrick)scripts1[0].Bricks.Bricks[6]).IfLogicBeginBrick =
                     (IfLogicBeginBrick)scripts1[0].Bricks.Bricks[2];
            ((IfLogicElseBrick)scripts1[0].Bricks.Bricks[6]).IfLogicEndBrick =
                (IfLogicEndBrick)scripts1[0].Bricks.Bricks[7];

            ((IfLogicEndBrick)scripts1[0].Bricks.Bricks[7]).IfLogicBeginBrick =
                (IfLogicBeginBrick)scripts1[0].Bricks.Bricks[2];
            ((IfLogicEndBrick)scripts1[0].Bricks.Bricks[7]).IfLogicElseBrick =
                (IfLogicElseBrick)scripts1[0].Bricks.Bricks[6];


            var scripts2 = new ObservableCollection<Script>
            {
                new StartScript()
                { 
                    Bricks = 
                    new BrickList
                    {
                        Bricks = new ObservableCollection<Brick>
                        {
                            new SetCostumeBrick{Costume = looks2[0]},
                            new PlaySoundBrick{Sound = sounds2[0]},
                            new PointToBrick(),
                            new ForeverBrick(),
                            new ForeverLoopEndBrick(),
                            new RepeatBrick(),
                            new RepeatLoopEndBrick()
                        }
                    }
                }
            };

            ((ForeverBrick)scripts2[0].Bricks.Bricks[3]).LoopEndBrick =
                (ForeverLoopEndBrick)scripts2[0].Bricks.Bricks[4];
            ((ForeverLoopEndBrick)scripts2[0].Bricks.Bricks[4]).LoopBeginBrick =
                (ForeverBrick)scripts2[0].Bricks.Bricks[3];

            ((RepeatBrick)scripts2[0].Bricks.Bricks[5]).LoopEndBrick =
                (RepeatLoopEndBrick)scripts2[0].Bricks.Bricks[6];
            ((RepeatLoopEndBrick)scripts2[0].Bricks.Bricks[6]).LoopBeginBrick =
                (RepeatBrick)scripts2[0].Bricks.Bricks[5];


            var object1 = new Sprite
            {
                Scripts = new ScriptList { Scripts = scripts1 },
                Sounds = new SoundList { Sounds = sounds1 },
                Costumes = new CostumeList { Costumes = looks1 }
            };

            var object2 = new Sprite
            {
                Scripts = new ScriptList { Scripts = scripts2 },
                Sounds = new SoundList { Sounds = sounds2 },
                Costumes = new CostumeList { Costumes = looks2 }
            };

            ((PointToBrick)scripts2[0].Bricks.Bricks[2]).PointedSprite = object2;

            project.SpriteList = new SpriteList
            {
                Sprites = new ObservableCollection<Sprite>
                    {
                        object1, object2
                    }
            };


            var programVariables = new ObservableCollection<UserVariable>
            {
                new UserVariable{Name = "Variable2"}
            };

            var objectVariables = new ObservableCollection<ObjectVariableEntry>
            {
                new ObjectVariableEntry
                {
                    Sprite = object1,
                    VariableList = new UserVariableList{UserVariables = new ObservableCollection<UserVariable>
                    {
                        new UserVariable{Name = "Variable1"},
                    }}
                },
                new ObjectVariableEntry
                {
                    Sprite = object2,
                    VariableList = new UserVariableList{UserVariables = new ObservableCollection<UserVariable>
                    {
                        new UserVariable{Name = "Variable1"},
                    }}
                }
            };

            project.VariableList.ObjectVariableList = new ObjectVariableList { ObjectVariableEntries = objectVariables };
            project.VariableList.ProgramVariableList = new ProgramVariableList{ UserVariables = programVariables };

            ((SetVariableBrick)scripts1[0].Bricks.Bricks[1]).UserVariable = 
                objectVariables[0].VariableList.UserVariables[0];
        }
    }
}
