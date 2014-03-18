using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class XmlFormulaTreeConverterTests
    {

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            //  needed for SampleLoader.LoadSampleXDocument
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void TestDefaultNodes()
        {
            var userVariables = new[] { new UserVariable { Name = "userVariable1" } };
            
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
                Select(Core.FormulaEditor.Editor.XmlFormulaTreeFactory.CreateDefaultNode);
            var sensorFormulas = EnumExtensions.AsEnumerable<SensorVariable>().
                Select(Core.FormulaEditor.Editor.XmlFormulaTreeFactory.CreateDefaultNode);
            var variableFormulas = EnumExtensions.AsEnumerable<ObjectVariable>().
                Select(Core.FormulaEditor.Editor.XmlFormulaTreeFactory.CreateDefaultNode).
                Concat(userVariables.Select(Core.CatrobatObjects.Formulas.XmlFormulaTreeFactory.CreateUserVariableNode));
            var bracketFormulas = Enumerable.Repeat(Core.CatrobatObjects.Formulas.XmlFormulaTreeFactory.CreateParenthesesNode(null), 1);

            TestXml(
                converter: new XmlFormulaTreeConverter(userVariables, null), 
                formulas: keyFormulas.Concat(sensorFormulas).Concat(variableFormulas).Concat(bracketFormulas));
            Assert.Inconclusive("Rewrite to use Core.CatrobatObjects.Formulas.XmlFormulaTreeFactory Members via reflection. ");
        }

        [TestMethod]
        public void TestPocketCodeFormulas()
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
                var userVariables = project.VariableList.ProgramVariableList.UserVariables.
                    Concat(project.VariableList.ObjectVariableList.ObjectVariableEntries.
                        SelectMany(variable => variable.VariableList.UserVariables)).
                    Distinct(variable => variable.Name);
                var objectVariable = project.VariableList.ObjectVariableList.ObjectVariableEntries.FirstOrDefault();
                TestXml(
                    converter: new XmlFormulaTreeConverter(userVariables, objectVariable), 
                    formulas: formulas);
            }
            Assert.Inconclusive("Add additional samples using all formula nodes. ");
        }

        private static void TestXml(XmlFormulaTreeConverter converter, IEnumerable<XmlFormulaTree> formulas)
        {
            foreach (var formula in formulas)
            {
                var convertedFormula = converter.ConvertBack(converter.Convert(formula));
                FormulaComparer.CompareFormulasGenerously(formula, convertedFormula);
            }
        }


    }
}
