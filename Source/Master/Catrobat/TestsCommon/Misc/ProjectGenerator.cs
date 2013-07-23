using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.TestsCommon.Misc
{
    public class ProjectGenerator
    {
        // Bricks that must be tested manually
        private static readonly List<Type> ExcludedBricks = new List<Type>
        {
            //typeof(LoopEndBrick),
            //typeof(ForeverBrick),
            //typeof(RepeatBrick)
        };

        public static Project GenerateProject()
        {
            var project = new Project
            {
                ProjectHeader = new ProjectHeader
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 0,
                    ApplicationName = "Pocket Code",
                    ApplicationVersion = "0.0.1",
                    CatrobatLanguageVersion = (float) 0.8,
                    DateTimeUpload = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Description = "",
                    DeviceName = "SampleDevice",
                    MediaLicense = "http://developer.catrobat.org/ccbysa_v3",
                    Platform = PlatformInformationHelper.GetPlatformName(),
                    PlatformVersion = PlatformInformationHelper.GetPlatformVersion(),
                    ProgramLicense = "http://developer.catrobat.org/agpl_v3",
                    RemixOf = "",
                    ScreenHeight = 480,
                    ScreenWidth = 800,
                    Tags = "",
                    Url = "http://pocketcode.org/details/871",
                    UserHandle = ""
                }
            };
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
                sprite.Costumes.Costumes.Add(new Costume
                {
                    FileName = "FileName" + i,
                    Name = "Look" + i,
                });
            }

            for (int i = 0; i < 6; i++)
            {
                var sprite = sprites[i % 2];
                sprite.Sounds = new SoundList();
                sprite.Sounds.Sounds.Add(new Sound
                {
                    FileName = "FileName" + i,
                    Name = "Sound" + i,
                });
            }


            project.VariableList = new VariableList
            {
                ObjectVariableList = new ObjectVariableList
                {
                    ObjectVariableEntries = new ObservableCollection<ObjectVariableEntry>()
                },
                ProgramVariableList = new ProgramVariableList()
                {
                    UserVariables = new ObservableCollection<UserVariable>()
                }
            };

            var count = 0;
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


            foreach (var sprite in sprites)
            {
                var scripts = ReflectionHelper.GetInstances<Script>();

                foreach (var script in scripts)
                {
                    sprite.Scripts = new ScriptList();
                    sprite.Scripts.Scripts.Add(script);

                    var bricks = ReflectionHelper.GetInstances<Brick>(ExcludedBricks);
                    foreach (var brick in bricks)
                    {
                        FillDummyValues(brick, project, sprite);
                        script.Bricks.Bricks.Add(brick);
                    }

                    AddLoopBricks(script.Bricks.Bricks);
                }
            }

            return project;
        }

        private static void FillDummyValues(object o, Project project, Sprite sprite)
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

        private static Random _rand ;
        private static object CreateDummyValue(Type type, Project project, Sprite sprite)
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

            if (type == typeof(Formula))
            {
                var formula = new Formula
                {
                    FormulaTree = new FormulaTree
                    {
                        LeftChild = new LeftChild
                        {
                            LeftChild = new LeftChild(),
                            RightChild = new RightChild(),
                            VariableType = "BOOL",
                            VariableValue = "1"
                        },
                        RightChild = new RightChild(),
                        VariableType = "NUMBER",
                        VariableValue = "6"
                    }
                };
                return formula;
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
                foreach(var entry in project.VariableList.ObjectVariableList.ObjectVariableEntries)
                    if (entry.Sprite == sprite)
                    {
                        var userVariables = entry.VariableList.UserVariables;
                        return userVariables[_rand.Next(0, userVariables.Count - 1)];
                    }
            }

            if (type == typeof(IfLogicBeginBrick))
            {
                foreach(var script in sprite.Scripts.Scripts)
                    foreach(var brick in script.Bricks.Bricks)
                        if(brick is IfLogicBeginBrick)
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

            return null;
        }

        private static void AddLoopBricks(ObservableCollection<Brick> bricks)
        {
            var foreverBrick = new ForeverBrick();
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
            var loopEndBrickForever = new LoopEndBrick();
            var loopEndBrickRepeat = new LoopEndBrick();

            foreverBrick.LoopEndBrick = loopEndBrickForever;
            repeatBrick.LoopEndBrick = loopEndBrickRepeat;
            loopEndBrickForever.LoopBeginBrick = foreverBrick;
            loopEndBrickRepeat.LoopBeginBrick = repeatBrick;

            bricks.Add(foreverBrick);
            bricks.Add(repeatBrick);
            bricks.Add(loopEndBrickForever);
            bricks.Add(loopEndBrickRepeat);
        }

    }
}
