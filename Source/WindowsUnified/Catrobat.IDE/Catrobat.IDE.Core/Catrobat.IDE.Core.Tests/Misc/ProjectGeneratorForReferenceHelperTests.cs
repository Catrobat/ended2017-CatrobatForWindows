using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Core.Tests.Misc
{
    public class ProjectGeneratorForReferenceHelperTests : ITestProjectGenerator
    {

        public Program GenerateProject()
        {
            var project = new Program
            {
                Name = "program1",
                Description = "",
                UploadHeader = new UploadHeader
                {
                    Uploaded = DateTime.Now,
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    RemixOf = "",
                    Tags = new ObservableCollection<string>(),
                    Url = "http://pocketcode.org/details/871",
                    UserId = ""
                }
            };

            AddSprites(project);

            return project;
        }

        private void AddSprites(Program project)
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


            var scripts1 = new ObservableCollection<Script>
            {
                new StartScript
                {
                    Bricks = new ObservableCollection<Brick>
                    {
                        new SetCostumeBrick {Value = looks1[0]},
                        new SetVariableBrick(), // TODO
                        new IfBrick(),
                        new IfBrick(),
                        new ElseBrick(),
                        new EndIfBrick(),
                        new ElseBrick(),
                        new EndIfBrick()
                    }
                }
            };

            ((IfBrick) scripts1[0].Bricks[2]).Else = (ElseBrick) scripts1[0].Bricks[6];
            ((IfBrick) scripts1[0].Bricks[2]).End = (EndIfBrick) scripts1[0].Bricks[7];

            ((IfBrick) scripts1[0].Bricks[3]).Else = (ElseBrick) scripts1[0].Bricks[4];
            ((IfBrick) scripts1[0].Bricks[3]).End = (EndIfBrick) scripts1[0].Bricks[5];

            ((ElseBrick) scripts1[0].Bricks[4]).Begin = (IfBrick) scripts1[0].Bricks[3];
            ((ElseBrick) scripts1[0].Bricks[4]).End = (EndIfBrick) scripts1[0].Bricks[5];

            ((EndIfBrick) scripts1[0].Bricks[5]).Begin = (IfBrick) scripts1[0].Bricks[3];
            ((EndIfBrick) scripts1[0].Bricks[5]).Else = (ElseBrick) scripts1[0].Bricks[4];

            ((ElseBrick) scripts1[0].Bricks[6]).Begin = (IfBrick) scripts1[0].Bricks[2];
            ((ElseBrick) scripts1[0].Bricks[6]).End = (EndIfBrick) scripts1[0].Bricks[7];

            ((EndIfBrick) scripts1[0].Bricks[7]).Begin = (IfBrick) scripts1[0].Bricks[2];
            ((EndIfBrick) scripts1[0].Bricks[7]).Else = (ElseBrick) scripts1[0].Bricks[6];

            var scripts2 = new ObservableCollection<Script>
            {
                new StartScript
                {
                    Bricks = new ObservableCollection<Brick>
                    {
                        new SetCostumeBrick {Value = looks2[0]},
                        new PlaySoundBrick {Value = sounds2[0]},
                        new LookAtBrick(),
                        new ForeverBrick(),
                        new EndForeverBrick(),
                        new RepeatBrick(),
                        new EndRepeatBrick()
                    }
                }
            };

            ((ForeverBrick) scripts2[0].Bricks[3]).End = (EndForeverBrick) scripts2[0].Bricks[4];
            ((EndForeverBrick) scripts2[0].Bricks[4]).Begin = (ForeverBrick) scripts2[0].Bricks[3];

            ((RepeatBrick) scripts2[0].Bricks[5]).End = (EndRepeatBrick) scripts2[0].Bricks[6];
            ((EndRepeatBrick) scripts2[0].Bricks[6]).Begin = (RepeatBrick) scripts2[0].Bricks[5];


            var object1 = new Sprite
            {
                Scripts = scripts1,
                Sounds = sounds1,
                Costumes = looks1,
                LocalVariables = new ObservableCollection<LocalVariable>
                {
                    new LocalVariable {Name = "Variable1"},
                }
            };

            var object2 = new Sprite
            {
                Scripts = scripts2,
                Sounds = sounds2,
                Costumes = looks2,
                LocalVariables = new ObservableCollection<LocalVariable>
                {
                    new LocalVariable {Name = "Variable1"},
                }
            };

            ((LookAtBrick)scripts2[0].Bricks[2]).Target = object2;

            project.Sprites = new ObservableCollection<Sprite>
            {
                object1,
                object2
            };


            project.GlobalVariables = new ObservableCollection<GlobalVariable>
            {
                new GlobalVariable {Name = "Variable2"}
            };
            ((SetVariableBrick) scripts1[0].Bricks[1]).Variable = object1.LocalVariables[0];
        }
    }
}
