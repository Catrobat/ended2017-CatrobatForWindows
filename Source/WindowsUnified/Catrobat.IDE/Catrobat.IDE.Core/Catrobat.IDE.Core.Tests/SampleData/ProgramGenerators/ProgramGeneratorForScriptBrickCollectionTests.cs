using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators
{
    public class ProgramGeneratorForScriptBrickCollectionTests : ITestProgramGenerator
    {
        public Program GenerateProgram()
        {
            return new Program
            {
                Name = "project",
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
                },
                Sprites = new ObservableCollection<Sprite>
                {
                    new Sprite
                    {
                        Scripts = new ObservableCollection<Script>
                        {
                            new StartScript
                            {
                                Bricks = new ObservableCollection<Brick>
                                {
                                    new SetLookBrick()
                                }
                            }
                        }
                    },
                    new Sprite
                    {
                        Scripts = new ObservableCollection<Script>
                        {
                            new StartScript
                            {
                                Bricks = new ObservableCollection<Brick>
                                {
                                    new SetLookBrick(),
                                }
                            },
                            new TappedScript
                            {
                                Bricks = new ObservableCollection<Brick>
                                {
                                    new SetLookBrick(),
                                    new DelayBrick(),
                                    new SetLookBrick(),
                                    new DelayBrick(),
                                    new SetLookBrick()
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}
