using System.Xml.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Tests.Extensions;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Xml
{
    /// <summary>Tests <see cref="XmlProjectConverter" />. </summary>
    [TestClass]
    public class XmlProjectConverterTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            //  needed for SampleLoader.LoadSampleXDocument
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Xml"), TestCategory("GatedTests.Obsolete")]
        public void TestPocketCodeProjects()
        {
            var documents = Enumerable.Range(1, 16)
                .Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input")
                .Select(SampleLoader.LoadSampleXDocument);
            foreach (var document in documents)
            {
                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
                var xml = document.ToString();

                var project = new XmlProject(xml);
                TestConvert(new XmlProjectConverter(), project);
            }
        }

        #region Helpers

        private static void TestConvert(XmlProjectConverter converter, XmlProject project)
        {
            var tempProject = converter.Convert(project);
            ValidProject(tempProject);

            var actual = converter.ConvertBack(tempProject);

            var expectedDocument = project.CreateXML();

            var actualDocument = actual.CreateXML();

            var writer = new XmlStringWriter();
            expectedDocument.Save(writer, SaveOptions.None);
            var expectedXml = writer.GetStringBuilder().ToString();

            writer = new XmlStringWriter();
            actualDocument.Save(writer, SaveOptions.None);
            var actualXml = writer.GetStringBuilder().ToString();

            Assert.AreEqual(expectedXml, actualXml);
        }

        private static void ValidProject(Project project)
        {
            foreach (var sprite in project.Sprites)
            {
                var objects = sprite.Scripts
                    .SelectMany(script => Enumerable.Repeat<IBrick>(script, 1)
                        .Concat(script.Bricks))
                    .SelectMany(brick => Enumerable.Repeat((Model) brick, 1)
                        .Concat(brick.GetType().GetPropertiesValues<FormulaTree>(brick)
                            .SelectMany(tree => tree.AsEnumerable())))
                    .ToList();

                var broadcastMessages = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<BroadcastMessage>(obj))
                    .Where(message => message != null);
                foreach (var message in broadcastMessages)
                {
                    CollectionAssert.Contains(project.BroadcastMessages, message);
                }

                var variables = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<Variable>(obj))
                    .ToList();

                var localVariables = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<LocalVariable>(obj))
                    .Concat(variables.OfType<LocalVariable>())
                    .Where(variable => variable != null);
                foreach (var variable in localVariables)
                {
                    CollectionAssert.Contains(sprite.LocalVariables, variable);
                }

                var globalVariables = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<GlobalVariable>(obj))
                    .Concat(variables.OfType<GlobalVariable>())
                    .Where(variable => variable != null);
                foreach (var variable in globalVariables)
                {
                    CollectionAssert.Contains(project.GlobalVariables, variable);
                }

                var sounds = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<Sound>(obj))
                    .Where(sound => sound != null);
                foreach (var sound in sounds)
                {
                    CollectionAssert.Contains(sprite.Sounds, sound);
                }

                var costumes = objects
                    .SelectMany(obj => obj.GetType().GetPropertiesValues<Costume>(obj))
                    .Where(costume => costume != null);
                foreach (var costume in costumes)
                {
                    CollectionAssert.Contains(sprite.Costumes, costume);
                }
            }
        }

        #endregion
    }
}
