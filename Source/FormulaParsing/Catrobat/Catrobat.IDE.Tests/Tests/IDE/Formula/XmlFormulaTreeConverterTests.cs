using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.XmlFormula;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class XmlFormulaTreeConverterTests
    {
        private readonly Random _random = new Random();

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {
            //  needed for SampleLoader.LoadSampleXDocument
            TestHelper.InitializeTests();
        }

        [TestMethod]
        public void TestCatroidNodes()
        {
            const string directory = "FormulaEditor/";
            TestXml<FormulaNodeAbs>(directory + "abs");
            TestXml<FormulaNodeAccelerationX>(directory + "accelerationx");
            TestXml<FormulaNodeAccelerationY>(directory + "accelerationy");
            TestXml<FormulaNodeAccelerationZ>(directory + "accelerationz");
            TestXml<FormulaNodeAdd>(directory + "add");
            TestXml<FormulaNodeAnd>(directory + "and");
            TestXml<FormulaNodeArccos>(directory + "arccos");
            TestXml<FormulaNodeArcsin>(directory + "arcsin");
            TestXml<FormulaNodeArctan>(directory + "arctan");
            TestXml<FormulaNodeBrightness>(directory + "brightness");
            TestXml<FormulaNodeCompass>(directory + "compass");
            TestXml<FormulaNodeCos>(directory + "cos");
            TestXml<FormulaNodeDivide>(directory + "divide");
            TestXml<FormulaNodeEquals>(directory + "equals");
            TestXml<FormulaNodeExp>(directory + "exp");
            TestXml<FormulaNodeFalse>(directory + "false");
            TestXml<FormulaNodeGlobalVariable>(directory + "globalvariable");
            TestXml<FormulaNodeGreater>(directory + "greater");
            TestXml<FormulaNodeGreaterEqual>(directory + "greaterequal");
            TestXml<FormulaNodeInclinationX>(directory + "inclinationx");
            TestXml<FormulaNodeInclinationY>(directory + "inclinationy");
            TestXml<FormulaNodeLayer>(directory + "layer");
            TestXml<FormulaNodeLess>(directory + "less");
            TestXml<FormulaNodeLessEqual>(directory + "lessequal");
            TestXml<FormulaNodeLn>(directory + "ln");
            TestXml<FormulaNodeLocalVariable>(directory + "localvariable");
            TestXml<FormulaNodeLog>(directory + "log");
            TestXml<FormulaNodeLoudness>(directory + "loudness");
            TestXml<FormulaNodeMax>(directory + "max");
            TestXml<FormulaNodeMin>(directory + "min");
            TestXml<FormulaNodeModulo>(directory + "mod");
            TestXml<FormulaNodeMultiply>(directory + "multiply");
            TestXml<FormulaNodeNegativeSign>(directory + "negativesign");
            TestXml<FormulaNodeNot>(directory + "not");
            TestXml<FormulaNodeNotEquals>(directory + "notequals");
            TestXml<FormulaNodeNumber>(directory + "number");
            TestXml<FormulaNodeTransparency>(directory + "transparency");
            TestXml<FormulaNodeOr>(directory + "or");
            TestXml<FormulaNodeParentheses>(directory + "parentheses");
            TestXml<FormulaNodePi>(directory + "pi");
            TestXml<FormulaNodePositionX>(directory + "positionx");
            TestXml<FormulaNodePositionY>(directory + "positiony");
            TestXml<FormulaNodeRandom>(directory + "random");
            TestXml<FormulaNodeRotation>(directory + "rotation");
            TestXml<FormulaNodeRound>(directory + "round");
            TestXml<FormulaNodeSin>(directory + "sin");
            TestXml<FormulaNodeSize>(directory + "size");
            TestXml<FormulaNodeSqrt>(directory + "sqrt");
            TestXml<FormulaNodeSubtract>(directory + "subtract");
            TestXml<FormulaNodeTan>(directory + "tan");
            TestXml<FormulaNodeTrue>(directory + "true");
        }

        [TestMethod]
        public void TestFormulaTreeNodes()
        {
            var localVariable = new UserVariable
            {
                Name = "LocalVariable"
            };
            var globalVariable = new UserVariable
            {
                Name = "GlobalVariable"
            };
            var nodes = typeof(FormulaTreeFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select(method => (IFormulaTree)method.Invoke(
                    obj: null,
                    parameters: method.GetParameters()
                        .Select(parameter =>
                        {
                            if (parameter.ParameterType == typeof(double)) return _random.Next();
                            if (parameter.ParameterType == typeof(bool)) return _random.NextBool();
                            if (parameter.ParameterType == typeof(IFormulaTree)) return FormulaTreeFactory.CreateNumberNode(_random.Next());
                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("local")) return (object)localVariable;
                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("global")) return (object)globalVariable;
                            Assert.Inconclusive();
                            return null;
                        })
                        .ToArray()));
            TestFormulas(
                converter: new XmlFormulaTreeConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
                formulas: nodes);
        }

        [TestMethod]
        public void TestXmlFormulaTreeNodes()
        {
            var localVariable = new UserVariable
            {
                Name = "LocalVariable"
            };
            var globalVariable = new UserVariable
            {
                Name = "GlobalVariable"
            };
            var nodes = typeof(XmlFormulaTreeFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select(method => (XmlFormulaTree)method.Invoke(
                    obj: null, 
                    parameters: method.GetParameters()
                        .Select(parameter =>
                        {
                            if (parameter.ParameterType == typeof(double)) return _random.Next();
                            if (parameter.ParameterType == typeof(XmlFormulaTree)) return XmlFormulaTreeFactory.CreateNumberNode(_random.Next());
                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("local")) return (object)localVariable;
                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("global")) return (object)globalVariable;
                            Assert.Inconclusive();
                            return null;
                        })
                        .ToArray()));
            TestFormulas(
                converter: new XmlFormulaTreeConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
                formulas: nodes);
        }

        [TestMethod]
        public void TestPocketCodeFormulas()
        {
            var documents = Enumerable.Range(1, 16)
                .Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input")
                .Select(SampleLoader.LoadSampleXDocument);
            foreach (var document in documents)
            {
                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
                var xml = document.ToString();
                var project = new Project(xml);
                var globalVariables = project.VariableList.ProgramVariableList.UserVariables;
                foreach (var sprite in project.SpriteList.Sprites)
                {
                    var localVariables = project.VariableList.ObjectVariableList.ObjectVariableEntries
                        .Where(entry => entry.Sprite == sprite)
                        .SelectMany(variable => variable.VariableList.UserVariables);
                    var formulas = sprite.Scripts.Scripts.SelectMany(script => script.Bricks.Bricks)
                        .SelectMany(brick => brick.GetType().GetProperties()
                            .Where(property => property.PropertyType == typeof(Core.CatrobatObjects.Formulas.Formula))
                            .Select(property => (Core.CatrobatObjects.Formulas.Formula) property.GetValue(brick)))
                        .Select(formula => formula.FormulaTree2);

                    TestFormulas(
                        converter: new XmlFormulaTreeConverter(localVariables, globalVariables),
                        formulas: formulas);
                }
            }
        }

        #region Helpers

        private void TestXml<TExpected>(string path)
        {
            var document = SampleLoader.LoadSampleXDocument(path);
            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
            var xml = document.ToString();
            var project = new Project(xml);
            var sprite = project.SpriteList.Sprites.Single();
            var brick = sprite.Scripts.Scripts.SelectMany(script => script.Bricks.Bricks).Single();
            var formula = brick.GetType().GetProperties()
                    .Where(property => property.PropertyType == typeof(Core.CatrobatObjects.Formulas.Formula))
                    .Select(property => (Core.CatrobatObjects.Formulas.Formula) property.GetValue(brick))
                    .Select(formula2 => formula2.FormulaTree2).Single();
            var localVariables = project.VariableList.ObjectVariableList.ObjectVariableEntries
                .Where(entry => entry.Sprite == sprite)
                .SelectMany(variable => variable.VariableList.UserVariables);
            var globalVariables = project.VariableList.ProgramVariableList.UserVariables;
            var converter = new XmlFormulaTreeConverter(localVariables, globalVariables);
            Assert.IsInstanceOfType(formula, typeof(TExpected));
        }

        private static void TestFormulas(XmlFormulaTreeConverter converter, IEnumerable<IFormulaTree> formulas)
        {
            foreach (var formula in formulas)
            {
                Assert.AreEqual(
                    actual: formula, 
                    expected: converter.Convert(converter.ConvertBack(formula)));
            }
        }

        private static void TestFormulas(XmlFormulaTreeConverter converter, IEnumerable<XmlFormulaTree> formulas)
        {
            foreach (var formula in formulas)
            {
                FormulaComparer.CompareFormulasGenerously(
                    actualFormula: formula, 
                    expectedFormula: converter.ConvertBack(converter.Convert(formula)));
            }
        }

        #endregion
    }
}
