using System;
using System.Linq;
using System.Reflection;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Misc;
using Catrobat.IDE.Core.Tests.SampleData.ProgramGenerators;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.XmlModelConvertion;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        public void CheckIfAllXmlTypsHaveCorrespondingConverters()
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

            var converterModelTypes = (from converterType in converterTypes
                                       let baseType = converterType.BaseType
                                       where baseType != null
                                       select baseType.GenericTypeArguments[1]).ToList();

            var xmlObjectTypes = (from typeInfo in inAssemblies
                where typeInfo.IsSubclassOf(typeof(XmlObject)) &&
                typeInfo.IsAbstract == false
                select typeInfo.AsType()).ToList();

            var modelTypes = (from typeInfo in inAssemblies
                where typeInfo.IsSubclassOf(typeof(Model)) && // TODO: introduce derivied type of Model that is only used for Model classes that have a corresponding XmlObject
                typeInfo.IsAbstract == false
                select typeInfo.AsType()).ToList();

            foreach (var xmlObjectType in xmlObjectTypes)
                Assert.IsTrue(converterXmlObjectTypes.Contains(xmlObjectType));

            foreach (var modelType in modelTypes)
                Assert.IsTrue(converterModelTypes.Contains(modelType));
        }
    }
}
