using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.VersionConverter;
using Catrobat.IDE.Core.Xml.Converter;
using Catrobat.IDE.Core.Xml.XmlObjects;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;
using Catrobat.IDE.Tests.Extensions;
using Catrobat.IDE.Tests.Misc;
using Catrobat.IDE.Tests.SampleData;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Xml
{
    /// <summary>Tests <see cref="XmlFormulaConverter" />. </summary>
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests.Obsolete")]
        public void TestCatroidNodes()
        {
            const string directory = "FormulaEditor/";
            TestConvert<FormulaNodeAbs>(directory + "abs");
            TestConvert<FormulaNodeAccelerationX>(directory + "accelerationx");
            TestConvert<FormulaNodeAccelerationY>(directory + "accelerationy");
            TestConvert<FormulaNodeAccelerationZ>(directory + "accelerationz");
            TestConvert<FormulaNodeAdd>(directory + "add");
            TestConvert<FormulaNodeAnd>(directory + "and");
            TestConvert<FormulaNodeArccos>(directory + "arccos");
            TestConvert<FormulaNodeArcsin>(directory + "arcsin");
            TestConvert<FormulaNodeArctan>(directory + "arctan");
            TestConvert<FormulaNodeBrightness>(directory + "brightness");
            TestConvert<FormulaNodeCompass>(directory + "compass");
            TestConvert<FormulaNodeCos>(directory + "cos");
            TestConvert<FormulaNodeDivide>(directory + "divide");
            TestConvert<FormulaNodeEquals>(directory + "equals");
            TestConvert<FormulaNodeExp>(directory + "exp");
            TestConvert<FormulaNodeFalse>(directory + "false");
            TestConvert<FormulaNodeGlobalVariable>(directory + "globalvariable");
            TestConvert<FormulaNodeGreater>(directory + "greater");
            TestConvert<FormulaNodeGreaterEqual>(directory + "greaterequal");
            TestConvert<FormulaNodeInclinationX>(directory + "inclinationx");
            TestConvert<FormulaNodeInclinationY>(directory + "inclinationy");
            TestConvert<FormulaNodeLayer>(directory + "layer");
            TestConvert<FormulaNodeLess>(directory + "less");
            TestConvert<FormulaNodeLessEqual>(directory + "lessequal");
            TestConvert<FormulaNodeLn>(directory + "ln");
            TestConvert<FormulaNodeLocalVariable>(directory + "localvariable");
            TestConvert<FormulaNodeLog>(directory + "log");
            TestConvert<FormulaNodeLoudness>(directory + "loudness");
            TestConvert<FormulaNodeMax>(directory + "max");
            TestConvert<FormulaNodeMin>(directory + "min");
            TestConvert<FormulaNodeModulo>(directory + "mod");
            TestConvert<FormulaNodeMultiply>(directory + "multiply");
            TestConvert<FormulaNodeNegativeSign>(directory + "negativesign");
            TestConvert<FormulaNodeNot>(directory + "not");
            TestConvert<FormulaNodeNotEquals>(directory + "notequals");
            TestConvert<FormulaNodeNumber>(directory + "number");
            TestConvert<FormulaNodeTransparency>(directory + "transparency");
            TestConvert<FormulaNodeOr>(directory + "or");
            TestConvert<FormulaNodeParentheses>(directory + "parentheses");
            TestConvert<FormulaNodePi>(directory + "pi");
            TestConvert<FormulaNodePositionX>(directory + "positionx");
            TestConvert<FormulaNodePositionY>(directory + "positiony");
            TestConvert<FormulaNodeRandom>(directory + "random");
            TestConvert<FormulaNodeRotation>(directory + "rotation");
            TestConvert<FormulaNodeRound>(directory + "round");
            TestConvert<FormulaNodeSin>(directory + "sin");
            TestConvert<FormulaNodeSize>(directory + "size");
            TestConvert<FormulaNodeSqrt>(directory + "sqrt");
            TestConvert<FormulaNodeSubtract>(directory + "subtract");
            TestConvert<FormulaNodeTan>(directory + "tan");
            TestConvert<FormulaNodeTrue>(directory + "true");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests.Obsolete")]
        public void TestPocketCodeFormulas()
        {
            var documents = Enumerable.Range(1, 16)
                .Select(i => "Converter/091_Win091/PracticalTests/Test" + i + "Input")
                .Select(SampleLoader.LoadSampleXDocument);
            foreach (var document in documents)
            {
                CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
                var xml = document.ToString();

                var xmlProject = new XmlProject(xml);
                var project = new XmlProjectConverter().Convert(xmlProject);
                var xmlProject2 = new XmlProjectConverter().ConvertBack(project);

                var formulas = xmlProject.SpriteList.Sprites
                    .SelectMany(sprite => sprite.Scripts.Scripts
                        .SelectMany(script => script.Bricks.Bricks)
                        .SelectMany(brick => brick.GetType().GetProperties()
                            .Where(property => property.PropertyType == typeof (FormulaTree))
                            .Select(property => (FormulaTree) property.GetValue(brick))));
                var formulas2 = xmlProject2.SpriteList.Sprites
                    .SelectMany(sprite => sprite.Scripts.Scripts
                        .SelectMany(script => script.Bricks.Bricks)
                        .SelectMany(brick => brick.GetType().GetProperties()
                            .Where(property => property.PropertyType == typeof (FormulaTree))
                            .Select(property => (FormulaTree) property.GetValue(brick))));
                EnumerableAssert.AreTestEqual(formulas, formulas2);
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests")]
        public void TestXmlFormulaTreeNodes()
        {
            var localVariable = new LocalVariable
            {
                Name = "LocalVariable"
            };
            var globalVariable = new GlobalVariable
            {
                Name = "GlobalVariable"
            };
            var formulas = typeof (XmlFormulaTreeFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select(method => new XmlFormula
                {
                    FormulaTree = (XmlFormulaTree) method.Invoke(
                        obj: null,
                        parameters: method.GetParameters().Select(parameter =>
                        {
                            if (parameter.ParameterType == typeof (double)) return _random.Next();
                            if (parameter.ParameterType == typeof (XmlFormulaTree)) return XmlFormulaTreeFactory.CreateNumberNode(_random.Next());
                            if (parameter.ParameterType == typeof (LocalVariable)) return (object) localVariable;
                            if (parameter.ParameterType == typeof (GlobalVariable)) return (object) globalVariable;
                            Assert.Inconclusive();
                            return null;
                        }).ToArray())
                });
            TestConvert(
                converter: new XmlFormulaConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
                formulas: formulas);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas"), TestCategory("GatedTests")]
        public void TestFormulaTreeNodes()
        {
            var localVariable = new LocalVariable
            {
                Name = "LocalVariable"
            };
            var globalVariable = new GlobalVariable
            {
                Name = "GlobalVariable"
            };
            var formulas = typeof (FormulaTreeFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select(method => (FormulaTree) method.Invoke(
                    obj: null,
                    parameters: method.GetParameters().Select(parameter =>
                    {
                        if (parameter.ParameterType == typeof (double)) return _random.Next();
                        if (parameter.ParameterType == typeof (bool)) return _random.NextBool();
                        if (parameter.ParameterType == typeof (FormulaTree)) return FormulaTreeFactory.CreateNumberNode(_random.Next());
                        if (parameter.ParameterType == typeof (LocalVariable)) return (object) localVariable;
                        if (parameter.ParameterType == typeof (GlobalVariable)) return (object) globalVariable;
                        Assert.Inconclusive();
                        return null;
                    }).ToArray()));
            TestConvertBack(
                converter: new XmlFormulaConverter(Enumerable.Repeat(localVariable, 1), Enumerable.Repeat(globalVariable, 1)),
                formulas: formulas);
        }

        #region Helpers

        private void TestConvert<TExpected>(string path)
        {
            var document = SampleLoader.LoadSampleXDocument(path);
            CatrobatVersionConverter.ConvertVersions("0.91", "Win0.91", document);
            var xml = document.ToString();

            var xmlProject = new XmlProject(xml);
            var project = new XmlProjectConverter().Convert(xmlProject);

            var sprite = project.Sprites.Single();
            var brick = sprite.Scripts.SelectMany(script => script.Bricks).Single();
            var formula = brick.GetType().GetProperties()
                .Where(property => property.PropertyType == typeof (FormulaTree))
                .Select(property => (FormulaTree) property.GetValue(brick))
                .Single();
            Assert.IsInstanceOfType(formula, typeof (TExpected));
        }

        private static void TestConvert(XmlFormulaConverter converter, IEnumerable<XmlFormula> formulas)
        {
            foreach (var formula in formulas)
            {
                XmlFormulaTreeComparer.CompareFormulas(
                    actualFormula: formula.FormulaTree,
                    expectedFormula: converter.ConvertBack(converter.Convert(formula)).FormulaTree);
            }
        }

        private static void TestConvertBack(XmlFormulaConverter converter, IEnumerable<FormulaTree> formulas)
        {
            foreach (var formula in formulas)
            {
                ModelAssert.AreTestEqual(
                    actual: formula,
                    expected: converter.Convert(converter.ConvertBack(formula)));
            }
        }

        #endregion
    }
}
