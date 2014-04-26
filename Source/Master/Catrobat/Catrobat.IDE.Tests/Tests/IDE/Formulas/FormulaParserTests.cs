using System;
using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formulas
{
    [TestClass]
    public class FormulaParserTests
    {
        private readonly UserVariable[] _localVariables = 
        {
            new UserVariable { Name = "Variable1" }, 
            new UserVariable { Name = "Variable2" }
        };
        private readonly UserVariable[] _globalVariables = 
        {
            new UserVariable { Name = "Variable2" }, 
            new UserVariable { Name = "Variable3" }
        };
        private readonly Random _random = new Random();
        private readonly IFormulaTree _nodeZero = FormulaTreeFactory.CreateNumberNode(0);
        private readonly IFormulaTree _nodeOne = FormulaTreeFactory.CreateNumberNode(1);
        private readonly FormulaParser _parser;

        public FormulaParserTests()
        {
            _parser = new FormulaParser(_localVariables, _globalVariables);
        }

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
        }


        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNullOrWhitespace()
        {
            foreach (var input in new[] { null, string.Empty, " ", "  " })
            {
                ParsingError parsingError;
                IFormulaTree formula;
                Assert.IsTrue(_parser.Parse(input, out formula, out parsingError));
                Assert.IsNull(parsingError);
                Assert.IsNull(formula);
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestConstants()
        {
            foreach (var value in new[] { 0, _random.Next(), -_random.Next(), _random.NextDouble() })
            {
                TestParser(
                    expectedFormula: FormulaTreeFactory.CreateNumberNode(value), 
                    input: value.ToString("R", ServiceLocator.CultureService.GetCulture()));
            }
            foreach (var input in new[] { "pi", "PI", "Pi" })
            {
                TestParser(
                    expectedFormula: FormulaTreeFactory.CreatePiNode(), 
                    input: input);
            }
            Assert.Inconclusive("True");
            Assert.Inconclusive("False");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            foreach (var input in new[] { "0+1", "0 + 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateAddNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0-1", "0 - 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateSubtractNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "-pi" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateNegativeSignNode(FormulaTreeFactory.CreatePiNode()), input: input);
            }
            foreach (var input in new[] { "0*1", "0 * 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateMultiplyNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0/1", "0 / 1", "0:1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateDivideNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0<1", "0 < 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateLessNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0=1", "0 = 1", "0 == 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateEqualsNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0≠1", "0 ≠ 1", "0 <> 1", "0 != 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateNotEqualsNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0≤1", "0 ≤ 1", "0 <= 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateLessEqualNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0>1", "0 > 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateGreaterNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0≥1", "0 ≥ 1", "0 >= 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateGreaterEqualNode(_nodeZero, _nodeOne), input: input);
            }
            foreach (var input in new[] { "0 mod 1", "0 Mod 1" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateModuloNode(_nodeZero, _nodeOne), input: input);
            }
            Assert.Inconclusive("And");
            Assert.Inconclusive("Or");
            Assert.Inconclusive("Not");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctions()
        {
            foreach (var input in new[] { "exp(0)", "Exp(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateExpNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "log(0)", "Log(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateLogNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "ln(0)", "ln(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateLnNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "sin(0)", "Sin(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateSinNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "cos(0)", "Cos(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateCosNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "tan(0)", "Tan(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateTanNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "arcsin(0)", "Arcsin(0)", "ArcSin(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateArcsinNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "arccos(0)", "Arccos(0)", "ArcCos(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateArccosNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "arctan(0)", "Arctan(0)", "ArcTan(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateArctanNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "sqrt(0)", "Sqrt(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateSqrtNode(_nodeZero), input: input);
            }
            foreach (var input in new[] { "abs(0)", "Abs(0)" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateAbsNode(_nodeZero), input: input);
            }
            Assert.Inconclusive("Round");
            Assert.Inconclusive("Random");
            Assert.Inconclusive("Min/Max");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperties()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBrackets()
        {
            foreach (var input in new[] { "(0)", "( 0 )", "((0))", "(((0)))" })
            {
                TestParser(expectedFormula: FormulaTreeFactory.CreateParenthesesNode(_nodeZero), input: input);
            }
        }

        #region Helpers

        private void TestParser(IFormulaTree expectedFormula, string input)
        {
            ParsingError parsingError;
            IFormulaTree formula;
            Assert.IsTrue(_parser.Parse(input, out formula, out parsingError));
            Assert.IsNull(parsingError);
            Assert.AreEqual(expectedFormula, formula);
        }

        #endregion

    }
}
