using System.Linq;
using System.Reflection;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.XmlModelConvertion;
using Catrobat.IDE.Core.XmlModelConvertion.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Catrobat.IDE.Core.Models.CatrobatModels;

namespace Catrobat.IDE.Core.Tests.Tests.Data
{
    [TestClass]
    public class XmlModelConversionTests
    {
        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod, TestCategory("XmlModelConversion")]
        public void CheckIfAllXmlTypsHaveCorrespondingConvertersTest()
        {
            var assembly = typeof(IXmlModelConverter).GetTypeInfo().Assembly;
            var typesInAssemblies = assembly.DefinedTypes;

            var inAssemblies = typesInAssemblies as TypeInfo[] ?? typesInAssemblies.ToArray();

            var converterTypes = (from typeInfo in inAssemblies
                where typeInfo.ImplementedInterfaces.Contains(typeof(IXmlModelConverter)) &&
                typeInfo.IsAbstract == false
                select typeInfo.AsType()).ToList();

            var converterXmlObjectTypes = (from converterType in converterTypes
                let baseType = converterType.BaseType
                where baseType != null
                select baseType.GenericTypeArguments[0]).ToList();

            var xmlObjectTypes1 = (from typeInfo in inAssemblies
                where typeInfo.IsSubclassOf(typeof(XmlObjectNode)) &&
                typeInfo.IsAbstract == false
                select typeInfo.AsType()).ToList();

            var xmlObjectTypes = (from typeInfo in inAssemblies
                                  where typeInfo.IsSubclassOf(typeof(XmlObjectNode)) &&
                                  typeInfo.IsAbstract == false
                                  select typeInfo.AsType()).ToList();

            foreach (var xmlObjectType in xmlObjectTypes)
                Assert.IsTrue(converterXmlObjectTypes.Contains(xmlObjectType));
        }

        [TestMethod, TestCategory("XmlModelConversion")]
        public void CheckIfAllModelsHaveCorrespondingConvertersTest()
        {
            var assembly = typeof(IXmlModelConverter).GetTypeInfo().Assembly;
            var typesInAssemblies = assembly.DefinedTypes;

            var inAssemblies = typesInAssemblies as TypeInfo[] ?? typesInAssemblies.ToArray();

            var converterTypes = (from typeInfo in inAssemblies
                                  where typeInfo.ImplementedInterfaces.Contains(typeof(IXmlModelConverter)) &&
                                  typeInfo.IsAbstract == false
                                  select typeInfo.AsType()).ToList();

            var converterModelTypes = (from converterType in converterTypes
                                       let baseType = converterType.BaseType
                                       where baseType != null
                                       select baseType.GenericTypeArguments[1]).ToList();

            var modelTypes = (from typeInfo in inAssemblies
                              where typeInfo.IsSubclassOf(typeof(CatrobatModelBase)) &&
                              typeInfo.IsAbstract == false
                              select typeInfo.AsType()).ToList();

            foreach (var modelType in modelTypes)
                Assert.IsTrue(converterModelTypes.Contains(modelType));

            //TODO FormulaToken and FormulaNode Converters are still missing
        }


        [TestMethod, TestCategory("XmlModelConversion")]
        public void XmlModelConversionSimpleTest()
        {
            var programConverter = new ProgramConverter();

            var programGenerator = new TestProgramGeneratorReflection();
            var program1 = programGenerator.GenerateSimpleProgram();

            var xmlProgram = programConverter.Convert(program1);

            var program2 = programConverter.Convert(xmlProgram);

            Assert.IsTrue(program1.TestEquals(program2));
        }

        [TestMethod, TestCategory("XmlModelConversion")]
        public void XmlModelConversionReflectionTest()
        {
            var programConverter = new ProgramConverter();

            var programGenerator = new TestProgramGeneratorReflection();
            var program1 = programGenerator.GenerateProgram();

            var xmlProgram = programConverter.Convert(program1);

            var program2 = programConverter.Convert(xmlProgram);

            Assert.AreEqual(program1.BasePath, program2.BasePath);
            Assert.AreEqual(program1.BroadcastMessages.Count, program2.BroadcastMessages.Count);
            Assert.AreEqual(program1.Description, program2.Description);
            Assert.AreEqual(program1.Name, program2.Name);
            Assert.AreEqual(program1.UploadHeader.Uploaded, program2.UploadHeader.Uploaded);
            Assert.AreEqual(program1.UploadHeader.MediaLicense, program2.UploadHeader.MediaLicense);

            Assert.AreEqual(program1.GlobalVariables.Count, program2.GlobalVariables.Count);
            Assert.AreEqual(program1.GlobalVariables[0].Name, program2.GlobalVariables[0].Name);

            Assert.AreEqual(program1.Sprites.Count, program2.Sprites.Count);
            Assert.AreEqual(program1.Sprites[0].LocalVariables.Count, program2.Sprites[0].LocalVariables.Count);
            Assert.AreEqual(program1.Sprites[0].LocalVariables[0].Name, program2.Sprites[0].LocalVariables[0].Name);
            Assert.AreEqual(program1.Sprites[0].Looks.Count, program2.Sprites[0].Looks.Count);
            Assert.AreEqual(program1.Sprites[0].Looks[0].Name, program2.Sprites[0].Looks[0].Name);
            Assert.AreEqual(program1.Sprites[0].Sounds.Count, program2.Sprites[0].Sounds.Count);
            Assert.AreEqual(program1.Sprites[0].Sounds[0].Name, program2.Sprites[0].Sounds[0].Name);
            Assert.AreEqual(program1.Sprites[0].Scripts.Count, program2.Sprites[0].Scripts.Count);
            Assert.AreEqual(program1.Sprites[0].Scripts[0].Bricks.Count, program2.Sprites[0].Scripts[0].Bricks.Count);

            Assert.AreEqual(program1.Sprites[1].LocalVariables.Count, program2.Sprites[1].LocalVariables.Count);
            Assert.AreEqual(program1.Sprites[1].LocalVariables[0].Name, program2.Sprites[1].LocalVariables[0].Name);
            Assert.AreEqual(program1.Sprites[1].Looks.Count, program2.Sprites[1].Looks.Count);
            Assert.AreEqual(program1.Sprites[1].Looks[0].Name, program2.Sprites[1].Looks[0].Name);
            Assert.AreEqual(program1.Sprites[1].Sounds.Count, program2.Sprites[1].Sounds.Count);
            Assert.AreEqual(program1.Sprites[1].Sounds[0].Name, program2.Sprites[1].Sounds[0].Name);
            Assert.AreEqual(program1.Sprites[1].Scripts.Count, program2.Sprites[1].Scripts.Count);
            Assert.AreEqual(program1.Sprites[1].Scripts[0].Bricks.Count, program2.Sprites[1].Scripts[0].Bricks.Count);

            Assert.IsTrue(program1.TestEquals(program2));
        }
    }
}
