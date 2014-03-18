using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Bricks;
using Catrobat.IDE.Core.CatrobatObjects.Costumes;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Scripts;
using Catrobat.IDE.Core.CatrobatObjects.Sounds;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System.IO;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.Tests.Misc
{
    public class ProjectGeneratorReflection : ITestProjectGenerator
    {
        // Bricks that must be tested manually
        private static readonly List<Type> ExcludedBricks = new List<Type>
        {
            typeof(ForeverBrick),
            typeof(ForeverLoopEndBrick),
            typeof(RepeatBrick),
            typeof(RepeatLoopEndBrick),
            typeof(IfLogicBeginBrick),
            typeof(IfLogicElseBrick),
            typeof(IfLogicEndBrick),
            typeof(EmptyDummyBrick)
        };

        private static readonly List<Type> ExcludedScripts = new List<Type>
        {
            typeof(EmptyDummyBrick)
        };

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

            XmlParserTempProjectHelper.Project = project;

            project.ProjectHeader.SetProgramName("project1");

            project.SpriteList = new SpriteList();
            var sprites = new ObservableCollection<Sprite>();

            project.SpriteList.Sprites = sprites;

            for (int i = 0; i < 2; i++)
            {
                var sprite = new Sprite
                {
                    Name = "Object" + i,
                };

                sprite.Costumes = new CostumeList();
                sprite.Sounds = new SoundList();
                sprites.Add(sprite);
            }

            for (int i = 0; i < 6; i++)
            {
                var sprite = sprites[i % 2];
                sprite.Costumes = new CostumeList();
                sprite.Costumes.Costumes.Add(GenerateCostume(i, project));
            }

            for (int i = 0; i < 6; i++)
            {
                var sprite = sprites[i % 2];
                sprite.Sounds = new SoundList();
                sprite.Sounds.Sounds.Add(GenerateSound(i, project));
            }


            AddUserVariables(project);


            foreach (var sprite in sprites)
            {
                var scripts = ReflectionHelper.GetInstances<Script>(ExcludedScripts);

                foreach (var script in scripts)
                {
                    FillDummyValues(script, project, sprite);
                    sprite.Scripts.Scripts.Add(script);

                    var bricks = ReflectionHelper.GetInstances<Brick>(ExcludedBricks);
                    foreach (var brick in bricks)
                    {
                        FillDummyValues(brick, project, sprite);
                        script.Bricks.Bricks.Add(brick);
                    }
                    AddLoopBricks(script.Bricks.Bricks);
                    AddIfLogicBricks(script.Bricks.Bricks);
                }
            }

            project.LoadReference();
            project.LoadBroadcastMessages();

            return project;
        }

        private void FillDummyValues(object o, Project project, Sprite sprite)
        {
            var type = o.GetType();

            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = CreateDummyValue(property.PropertyType, project, sprite);
                    property.SetValue(o, value, null);
                }
            }
        }

        private Random _rand;
        private object CreateDummyValue(Type type, Project project, Sprite sprite)
        {
            _rand = new Random(42);

            if (type == typeof(bool))
            {
                return true;
            }

            if (type == typeof(int))
            {
                return 42;
            }

            if (type == typeof(string))
            {
                return "SomeString";
            }

            if (type == typeof(double))
            {
                return 42.42;
            }

            if (type == typeof (WhenScript.WhenScriptAction))
            {
                return WhenScript.WhenScriptAction.Tapped;
            }

            if (type == typeof(Formula))
            {
                return GenerateFormula();
            }

            if (type == typeof(Sprite))
            {
                var sprites = project.SpriteList.Sprites;
                return sprites[_rand.Next(0, sprites.Count - 1)];
            }

            if (type == typeof(Costume))
            {
                var costumes = sprite.Costumes.Costumes;
                return costumes[_rand.Next(0, costumes.Count - 1)];
            }

            if (type == typeof(Sound))
            {
                var sounds = sprite.Sounds.Sounds;
                return sounds[_rand.Next(0, sounds.Count - 1)];
            }

            if (type == typeof(UserVariable))
            {
                foreach (var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
                    if (entry.Sprite == sprite)
                    {
                        var userVariables = entry.VariableList.UserVariables;
                        return userVariables[_rand.Next(0, userVariables.Count - 1)];
                    }
            }

            if (type == typeof(IfLogicBeginBrick))
            {
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicBeginBrick)
                            return brick;
            }

            if (type == typeof(IfLogicElseBrick))
            {
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicElseBrick)
                            return brick;
            }

            if (type == typeof(IfLogicEndBrick))
            {
                foreach (var script in sprite.Scripts.Scripts)
                    foreach (var brick in script.Bricks.Bricks)
                        if (brick is IfLogicEndBrick)
                            return brick;
            }

            if (type == typeof (ScriptList))
                return new ScriptList();

            if (type == typeof (BrickList))
                return new BrickList();

            return null;
        }

        private Formula GenerateFormula()
        {
            var formula = new Formula
            {
                FormulaTree = new FormulaTree
                {
                    LeftChild = new FormulaTree
                    {
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MINUS"
                        },
                        RightChild = new FormulaTree
                        {
                            LeftChild = new FormulaTree
                            {
                                VariableType = "FUNCTION",
                                VariableValue = "0"
                            },
                            VariableType = "USER_VARIABLE",
                            VariableValue = "LocalTestVariable1"
                        },
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "0"
                    },
                    VariableType = "NUMBER",
                    VariableValue = "6"
                }
            };
            return formula;
        }

        private Costume GenerateCostume(int index, Project project)
        {
            var costume = new Costume
             {
                 FileName = "FileName" + index,
                 Name = "Look" + index,
             };

            var absoluteFileName = Path.Combine(project.BasePath, Project.ImagesPath, costume.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                var fileStream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write);
                fileStream.Close();
            }

            return costume;
        }

        private Sound GenerateSound(int index, Project project)
        {
            var sound = new Sound
                {
                    FileName = "FileName" + index,
                    Name = "Sound" + index,
                };

            var absoluteFileName = Path.Combine(project.BasePath, Project.SoundsPath, sound.FileName);

            using (var storage = StorageSystem.GetStorage())
            {
                var fileStream = storage.OpenFile(absoluteFileName, StorageFileMode.Create, StorageFileAccess.Write);
                fileStream.Close();
            }

            return sound;
        }

        private void AddUserVariables(Project project)
        {
            project.VariableList = new VariableList
                {
                    ObjectVariableList = new ObjectVariableList
                        {
                            ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>()
                        },
                    ProgramVariableList = new ProgramVariableList
                        {
                            UserVariables = new ObservableCollection<UserVariable>()
                        }
                };

            foreach (var sprite in project.SpriteList.Sprites)
            {
                var entry = new ObjectVariableEntry
                    {
                        Sprite = sprite,
                        VariableList = new UserVariableList
                            {
                                UserVariables = new ObservableCollection<UserVariable>
                                    {
                                        new UserVariable
                                            {
                                                Name = "LocalTestVariable"
                                            }
                                    }
                            }
                    };

                project.VariableList.ObjectVariableList.ObjectVariableEntries.Add(entry);
            }

            for (int i = 0; i < 3; i++)
            {
                project.VariableList.ProgramVariableList.UserVariables.Add(new UserVariable
                    {
                        Name = "GlobalTestVariable" + i
                    });
            }

        }

        private void AddLoopBricks(ObservableCollection<Brick> bricks)
        {
            var foreverBrick = new ForeverBrick();
            bricks.Add(foreverBrick);

            var repeatBrick = new RepeatBrick
                {
                    TimesToRepeat = new Formula
                        {
                            FormulaTree = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "2"
                                }
                        }
                };
            bricks.Add(repeatBrick);

            var loopEndBrickForever = new ForeverLoopEndBrick();
            bricks.Add(loopEndBrickForever);

            var loopEndBrickRepeat = new RepeatLoopEndBrick();
            bricks.Add(loopEndBrickRepeat);

            foreverBrick.LoopEndBrick = loopEndBrickForever;
            repeatBrick.LoopEndBrick = loopEndBrickRepeat;
            loopEndBrickForever.LoopBeginBrick = foreverBrick;
            loopEndBrickRepeat.LoopBeginBrick = repeatBrick;
        }

        private void AddIfLogicBricks(ObservableCollection<Brick> bricks)
        {
            var ifLogicBeginBrick = new IfLogicBeginBrick
            {
                IfCondition = new Formula
                {
                    FormulaTree = new FormulaTree
                    {
                        VariableType = "BOOL",
                        VariableValue = "1"
                    }
                }
            };
            bricks.Add(ifLogicBeginBrick);

            var ifLogicElseBrick = new IfLogicElseBrick();
            bricks.Add(ifLogicElseBrick);

            var ifLogicEndBrick = new IfLogicEndBrick();
            bricks.Add(ifLogicEndBrick);


            ifLogicBeginBrick.IfLogicElseBrick = ifLogicElseBrick;
            ifLogicBeginBrick.IfLogicEndBrick = ifLogicEndBrick;

            ifLogicElseBrick.IfLogicBeginBrick = ifLogicBeginBrick;
            ifLogicElseBrick.IfLogicEndBrick = ifLogicEndBrick;

            ifLogicEndBrick.IfLogicBeginBrick = ifLogicBeginBrick;
            ifLogicEndBrick.IfLogicElseBrick = ifLogicElseBrick;
        }

    }
}
