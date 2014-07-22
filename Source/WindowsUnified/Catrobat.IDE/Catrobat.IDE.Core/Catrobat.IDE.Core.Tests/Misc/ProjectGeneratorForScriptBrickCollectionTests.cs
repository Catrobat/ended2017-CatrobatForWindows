using System;
using System.Collections.ObjectModel;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Bricks;
using Catrobat.IDE.Core.Models.Scripts;

namespace Catrobat.IDE.Core.Tests.Misc
{
    public class ProjectGeneratorForScriptBrickCollectionTests : ITestProjectGenerator
    {
        public Project GenerateProject()
        {
            return new Project
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
                                    new SetCostumeBrick()
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
                                    new SetCostumeBrick(),
                                }
                            },
                            new TappedScript
                            {
                                Bricks = new ObservableCollection<Brick>
                                {
                                    new SetCostumeBrick(),
                                    new DelayBrick(),
                                    new SetCostumeBrick(),
                                    new DelayBrick(),
                                    new SetCostumeBrick()
                                }
                            }
                        }
                    }
                },
            };
        }
    }
}
