using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class XmlFormulaTreeConverterTests
    {

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void XmlFormulaTreeConverterTests_DefaultNodes()
        {
            var keyFormulas = EnumExtensions.AsEnumerable<FormulaEditorKey>().
                Except(new[]
                {
                    FormulaEditorKey.Delete, 
                    FormulaEditorKey.Undo, 
                    FormulaEditorKey.Redo, 
                    FormulaEditorKey.OpenBracket, 
                    FormulaEditorKey.CloseBracket, 
                    FormulaEditorKey.NumberDot
                }).
                Select(XmlFormulaTreeFactory.CreateDefaultNode);
            var sensorFormulas = EnumExtensions.AsEnumerable<SensorVariable>().
                Select(XmlFormulaTreeFactory.CreateDefaultNode);
            var variableFormulas = EnumExtensions.AsEnumerable<ObjectVariable>().
                Select(XmlFormulaTreeFactory.CreateDefaultNode).
                Concat(Enumerable.Repeat(XmlFormulaTreeFactory2.CreateUserVariableNode("variableName"), 1));
            var bracketFormulas = Enumerable.Repeat(XmlFormulaTreeFactory2.CreateParenthesesNode(), 1);
            TestXml(keyFormulas.Concat(sensorFormulas).Concat(variableFormulas).Concat(bracketFormulas));
            Assert.Inconclusive("Rewrite to use XmlFormulaTreeFactory2 Members via reflection. ");
        }

        [TestMethod]
        public void XmlFormulaTreeConverterTests_PocketCodeFormulas()
        {
            var documents = Enumerable.Range(1, 16).
                Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input").
                Select(SampleLoader.LoadSampleXDocument);
            foreach (var document in documents)
            {
                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
                var xml = document.ToString();
                var project = new Project(xml);
                var bricks = project.SpriteList.Sprites.
                    SelectMany(sprite => sprite.Scripts.Scripts).
                    SelectMany(script => script.Bricks.Bricks).ToList();
                var formulas = bricks.
                    SelectMany(brick => brick.GetType().GetProperties().
                        Where(property => property.PropertyType == typeof(Core.CatrobatObjects.Formulas.Formula)).
                        Select(property => property.GetValue(brick)).
                        Cast<Core.CatrobatObjects.Formulas.Formula>()).
                    Select(formula => formula.FormulaTree);
                TestXml(formulas);
            }
            Assert.Inconclusive("Add additional samples using all formula nodes. ");
        }

        private static void TestXml(IEnumerable<XmlFormulaTree> formulas)
        {
            var converter = new XmlFormulaTreeConverter();
            foreach (var formula in formulas)
            {
                var convertedFormula = converter.ConvertBack(converter.Convert(formula));
                FormulaComparer.CompareFormulasGenerously(formula, convertedFormula);
            }
        }


    }
}
