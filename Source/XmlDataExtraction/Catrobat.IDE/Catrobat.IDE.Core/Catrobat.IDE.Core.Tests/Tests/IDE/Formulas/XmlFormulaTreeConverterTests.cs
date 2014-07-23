using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Formula = Catrobat.IDE.Core.ViewModels.Editor.Formula;

//namespace Catrobat.IDE.Core.Tests.Tests.IDE.Formulas
//{
//    /// <summary>Tests <see cref="XmlFormulaTreeConverter" />. </summary>
//    [TestClass]
//    public class XmlFormulaTreeConverterTests
//    {
//        private readonly Random _random = new Random();

//        [ClassInitialize]
//        public static void TestClassInitialize(TestContext testContext)
//        {
//            //  needed for SampleLoader.LoadSampleXDocument
//            TestHelper.InitializeTests();
//        }

//        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests.Obsolete")]
//        public void TestCatroidNodes()
//        {
//            const string directory = "FormulaEditor/";
//            TestConvert<FormulaNodeAbs>(directory + "abs");
//            TestConvert<FormulaNodeAccelerationX>(directory + "accelerationx");
//            TestConvert<FormulaNodeAccelerationY>(directory + "accelerationy");
//            TestConvert<FormulaNodeAccelerationZ>(directory + "accelerationz");
//            TestConvert<FormulaNodeAdd>(directory + "add");
//            TestConvert<FormulaNodeAnd>(directory + "and");
//            TestConvert<FormulaNodeArccos>(directory + "arccos");
//            TestConvert<FormulaNodeArcsin>(directory + "arcsin");
//            TestConvert<FormulaNodeArctan>(directory + "arctan");
//            TestConvert<FormulaNodeBrightness>(directory + "brightness");
//            TestConvert<FormulaNodeCompass>(directory + "compass");
//            TestConvert<FormulaNodeCos>(directory + "cos");
//            TestConvert<FormulaNodeDivide>(directory + "divide");
//            TestConvert<FormulaNodeEquals>(directory + "equals");
//            TestConvert<FormulaNodeExp>(directory + "exp");
//            TestConvert<FormulaNodeFalse>(directory + "false");
//            TestConvert<FormulaNodeGlobalVariable>(directory + "globalvariable");
//            TestConvert<FormulaNodeGreater>(directory + "greater");
//            TestConvert<FormulaNodeGreaterEqual>(directory + "greaterequal");
//            TestConvert<FormulaNodeInclinationX>(directory + "inclinationx");
//            TestConvert<FormulaNodeInclinationY>(directory + "inclinationy");
//            TestConvert<FormulaNodeLayer>(directory + "layer");
//            TestConvert<FormulaNodeLess>(directory + "less");
//            TestConvert<FormulaNodeLessEqual>(directory + "lessequal");
//            TestConvert<FormulaNodeLn>(directory + "ln");
//            TestConvert<FormulaNodeLocalVariable>(directory + "localvariable");
//            TestConvert<FormulaNodeLog>(directory + "log");
//            TestConvert<FormulaNodeLoudness>(directory + "loudness");
//            TestConvert<FormulaNodeMax>(directory + "max");
//            TestConvert<FormulaNodeMin>(directory + "min");
//            TestConvert<FormulaNodeModulo>(directory + "mod");
//            TestConvert<FormulaNodeMultiply>(directory + "multiply");
//            TestConvert<FormulaNodeNegativeSign>(directory + "negativesign");
//            TestConvert<FormulaNodeNot>(directory + "not");
//            TestConvert<FormulaNodeNotEquals>(directory + "notequals");
//            TestConvert<FormulaNodeNumber>(directory + "number");
//            TestConvert<FormulaNodeTransparency>(directory + "transparency");
//            TestConvert<FormulaNodeOr>(directory + "or");
//            TestConvert<FormulaNodeParentheses>(directory + "parentheses");
//            TestConvert<FormulaNodePi>(directory + "pi");
//            TestConvert<FormulaNodePositionX>(directory + "positionx");
//            TestConvert<FormulaNodePositionY>(directory + "positiony");
//            TestConvert<FormulaNodeRandom>(directory + "random");
//            TestConvert<FormulaNodeRotation>(directory + "rotation");
//            TestConvert<FormulaNodeRound>(directory + "round");
//            TestConvert<FormulaNodeSin>(directory + "sin");
//            TestConvert<FormulaNodeSize>(directory + "size");
//            TestConvert<FormulaNodeSqrt>(directory + "sqrt");
//            TestConvert<FormulaNodeSubtract>(directory + "subtract");
//            TestConvert<FormulaNodeTan>(directory + "tan");
//            TestConvert<FormulaNodeTrue>(directory + "true");
//        }

//        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests")]
//        public void TestXmlFormulaTreeNodes()
//        {
//            var localVariable = new UserVariable
//            {
//                Name = "LocalVariable"
//            };
//            var globalVariable = new UserVariable
//            {
//                Name = "GlobalVariable"
//            };
//            var nodes = typeof(XmlFormulaTreeFactory).GetMethods()
//                .Where(method => method.IsStatic)
//                .Select(method => (XmlFormulaTree)method.Invoke(
//                    obj: null,
//                    parameters: method.GetParameters()
//                        .Select(parameter =>
//                        {
//                            if (parameter.ParameterType == typeof(double)) return _random.Next();
//                            if (parameter.ParameterType == typeof(XmlFormulaTree)) return XmlFormulaTreeFactory.CreateNumberNode(_random.Next());
//                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("local")) return (object)localVariable;
//                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("global")) return (object)globalVariable;
//                            Assert.Inconclusive();
//                            return null;
//                        })
//                        .ToArray()));
//            TestConvert(
//                converter: new XmlFormulaTreeConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
//                formulas: nodes);
//        }

//        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests.Obsolete")]
//        public void TestPocketCodeFormulas()
//        {
//            var documents = Enumerable.Range(1, 16)
//                .Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input")
//                .Select(SampleLoader.LoadSampleXDocument);
//            foreach (var document in documents)
//            {
//                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
//                var xml = document.ToString();

//                // XmlFormulaTreeConverter.Convert() is called here
//                var project = new Project(xml);

//                var globalVariables = project.VariableList.ProgramVariableList.UserVariables;
//                foreach (var sprite in project.SpriteList.Sprites)
//                {
//                    // access to foreach variable in closure
//                    var sprite2 = sprite;
//                    var localVariables = project.VariableList.ObjectVariableList.ObjectVariableEntries
//                        .Where(entry => entry.Sprite == sprite2)
//                        .SelectMany(variable => variable.VariableList.UserVariables);
//                    var formulas = sprite.Scripts.Scripts.SelectMany(script => script.Bricks.Bricks)
//                        .SelectMany(brick => brick.GetType().GetProperties()
//                            .Where(property => property.PropertyType == typeof(Formula))
//                            .Select(property => (Formula)property.GetValue(brick)))
//                        .Select(formula => formula.FormulaTree2);

//                    TestConvertBack(
//                        converter: new XmlFormulaTreeConverter(localVariables, globalVariables),
//                        formulas: formulas);
//                }
//            }
//        }

//        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests")]
//        public void TestFormulaTreeNodes()
//        {
//            var localVariable = new UserVariable
//            {
//                Name = "LocalVariable"
//            };
//            var globalVariable = new UserVariable
//            {
//                Name = "GlobalVariable"
//            };
//            var nodes = typeof(FormulaTreeFactory).GetMethods()
//                .Where(method => method.IsStatic)
//                .Select(method => (IFormulaTree)method.Invoke(
//                    obj: null,
//                    parameters: method.GetParameters()
//                        .Select(parameter =>
//                        {
//                            if (parameter.ParameterType == typeof(double)) return _random.Next();
//                            if (parameter.ParameterType == typeof(bool)) return _random.NextBool();
//                            if (parameter.ParameterType == typeof(IFormulaTree)) return FormulaTreeFactory.CreateNumberNode(_random.Next());
//                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("local")) return (object)localVariable;
//                            if (parameter.ParameterType == typeof(UserVariable) && method.Name.ToLower().Contains("global")) return (object)globalVariable;
//                            Assert.Inconclusive();
//                            return null;
//                        })
//                        .ToArray()));
//            TestConvertBack(
//                converter: new XmlFormulaTreeConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
//                formulas: nodes);
//        }

//        #region Helpers

//        private void TestConvert<TExpected>(string path)
//        {
//            var document = SampleLoader.LoadSampleXDocument(path);
//            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
//            var xml = document.ToString();

//            // XmlFormulaTreeConverter.Convert() is called here
//            var project = new Project(xml);

//            var sprite = project.SpriteList.Sprites.Single();
//            var brick = sprite.Scripts.Scripts.SelectMany(script => script.Bricks.Bricks).Single();
//            var formula = brick.GetType().GetProperties()
//                    .Where(property => property.PropertyType == typeof(Formula))
//                    .Select(property => (Formula) property.GetValue(brick))
//                    .Select(formula2 => formula2.FormulaTree2).Single();
//            Assert.IsInstanceOfType(formula, typeof(TExpected));
//        }

//        private static void TestConvert(XmlFormulaTreeConverter converter, IEnumerable<XmlFormulaTree> formulas)
//        {
//            foreach (var formula in formulas)
//            {
//                XmlFormulaTreeComparer.CompareFormulas(
//                    actualFormula: formula,
//                    expectedFormula: converter.ConvertBack(converter.Convert(formula)));
//            }
//        }

//        private static void TestConvertBack(XmlFormulaTreeConverter converter, IEnumerable<IFormulaTree> formulas)
//        {
//            foreach (var formula in formulas)
//            {
//                Assert.AreEqual(
//                    actual: formula, 
//                    expected: converter.Convert(converter.ConvertBack(formula)));
//            }
//        }

//        #endregion
//    }
//}
