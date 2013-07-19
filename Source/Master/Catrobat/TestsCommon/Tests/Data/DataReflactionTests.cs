using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Catrobat.Core.Objects;
using Catrobat.Core.Objects.Bricks;
using Catrobat.Core.Objects.Costumes;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Scripts;
using Catrobat.Core.Objects.Sounds;
using Catrobat.Core.Objects.Variables;
using Catrobat.Core.Storage;
using Catrobat.TestsCommon.Misc;
using Catrobat.TestsCommon.Misc.Storage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.Data
{
    [TestClass]
    public class DataReflactionTests
    {
        // Bricks that must be tested manually
        public List<Type> ExcludedBricks = new List<Type>
        {
            typeof(LoopBeginBrick),
            typeof(LoopEndBrick),
            typeof(ForeverBrick),
            typeof(RepeatBrick)
        };

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
            _rand = new Random(42);
        }

        [TestMethod]
        public void ReflectionWriteReadTest1()
        {
            const string savePath1 = "/ReflectionWriteReadTest1/project.xml";
            const string savePath2 = "/ReflectionWriteReadTest1/project.xml";

            var project1 = new Project
            {
                ProjectHeader = new ProjectHeader
                {
                    ApplicationBuildName = "",
                    ApplicationBuildNumber = 42,
                    ApplicationName = "app1",
                    ApplicationVersion = "1",
                    CatrobatLanguageVersion = 0.8f,
                    DateTimeUpload = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Description = "description1",
                    DeviceName = "SampleDevice",
                    MediaLicense = "",
                    Platform = "",
                    PlatformVersion = "",
                    ProgramLicense = "",
                    RemixOf = "",
                    ScreenHeight = 480,
                    ScreenWidth = 800,
                    Tags = "tag1",
                    Url = "",
                    UserHandle = "user1"
                }
            };
            project1.ProjectHeader.SetProgramName("project1");


            project1.SpriteList = new SpriteList();
            var sprites = new ObservableCollection<Sprite>();

            project1.SpriteList.Sprites = sprites;

            for (int i = 0; i < 2; i++)
            {
                var sprite = new Sprite
                {
                    Name= "Object" + i,
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
                    Name= "Look" + i,
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
                        FillDummyValues(brick, project1);
                        script.Bricks.Bricks.Add(brick);
                    }
                }
            }

            project1.VariableList = new VariableList
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

            project1.Save(savePath1);

            string xml1 = null;
            using (IStorage storage = new StorageTest())
            {
                xml1 = storage.ReadTextFile(savePath1);
            }

            var project2 = new Project(xml1);
            project2.Save(savePath2);
            
            string xml2 = null;
            using (IStorage storage = new StorageTest())
            {
                xml2 = storage.ReadTextFile(savePath1);
            }

            var reader1 = new StringReader(xml1);
            var document1 = new XDocument(reader1);

            var reader2 = new StringReader(xml2);
            var document2 = new XDocument(reader2);

            XmlDocumentCompare.Compare(document1, document2);
        }

        public void FillDummyValues(object o, Project project)
        {
            var type = o.GetType();

            foreach (var property in type.GetProperties())
            {
                if (property.CanWrite)
                {
                    var value = CreateDummyValue(property.PropertyType, project);
                    property.SetValue(o, value, null);
                }
            }
        }

        private static Random _rand;
        public object CreateDummyValue(Type type, Project project)
        {
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

            if (type == typeof(FormulaTree))
            {
                return new FormulaTree();
            }

            if (type == typeof(Formula))
            {
                var formula = new Formula
                {
                    FormulaTree = new FormulaTree()
                };
                return formula;
            }

            if (type == typeof(Sprite))
            {
                int index = _rand.Next(project.SpriteList.Sprites.Count - 1);
                return project.SpriteList.Sprites[index];
            }

            if (type == typeof(Costume))
            {
                int spriteIndex = _rand.Next(project.SpriteList.Sprites.Count - 1);
                var sprite = project.SpriteList.Sprites[spriteIndex];

                int costumeIndex = _rand.Next(sprite.Costumes.Costumes.Count - 1);
                var costume = sprite.Costumes.Costumes[spriteIndex];
                return costume;
            }

            if (type == typeof(Sound))
            {
                int spriteIndex = _rand.Next(project.SpriteList.Sprites.Count - 1);
                var sprite = project.SpriteList.Sprites[spriteIndex];

                int soundIndex = _rand.Next(sprite.Sounds.Sounds.Count - 1);
                var sound = sprite.Sounds.Sounds[spriteIndex];
                return sound;
            }

            if (type == typeof(UserVariableReference))
            {
                int spriteIndex = _rand.Next(project.SpriteList.Sprites.Count - 1);
                var sprite = project.SpriteList.Sprites[spriteIndex];
                return new UserVariableReference();
            }

            if (type == typeof(LoopBeginBrickRef))
            {
                int spriteIndex = _rand.Next(project.SpriteList.Sprites.Count - 1);
                var sprite = project.SpriteList.Sprites[spriteIndex];
                return new LoopBeginBrickRef()
                {
                    Class = "Forever",
                    LoopBeginBrick = new ForeverBrick()
                };
            }

            if (type == typeof(LoopEndBrickRef))
            {
                int spriteIndex = _rand.Next(project.SpriteList.Sprites.Count - 1);
                var sprite = project.SpriteList.Sprites[spriteIndex];
                return new LoopEndBrickRef()
                {
                    LoopEndBrick = new LoopEndBrick()
                };
            }

            return null;
        }
    }
}
