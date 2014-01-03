using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaParserTests
    {
        private static readonly IEnumerable<UserVariable> UserVariables = new[]
        {
            new UserVariable { Name = "UserVariable1" }, 
            new UserVariable { Name = "UserVariable2" }
        };
        private readonly FormulaParser _parser = new FormulaParser(UserVariables, null);
        private readonly Random _random = new Random();
        private readonly IFormulaTree _nodeZero = FormulaTreeFactory.CreateNumberNode(0);
        private readonly IFormulaTree _nodeOne = FormulaTreeFactory.CreateNumberNode(1);

        [TestMethod]
        public void TestNullOrWhitespace()
        {
            foreach (var input in new[] { null, string.Empty, " ", "  " })
            {
                IEnumerable<string> parsingErrors;
                IFormulaTree formula;
                Assert.IsTrue(_parser.Parse(input, out formula, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.IsNull(formula);
            }
        }

        #region numbers

        [TestMethod]
        public void TestNumber()
        {
            foreach (var value in new[] { 0, _random.Next(), -_random.Next(), _random.NextDouble() })
            {
                var input = value.ToString(CultureInfo.CurrentCulture);
                var expected = FormulaTreeFactory.CreateNumberNode(value);
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestPi()
        {
            foreach (var input in new[] { "pi", "PI", "Pi" })
            {
                var expected = FormulaTreeFactory.CreatePiNode();
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void TestAdd()
        {
            var expected = FormulaTreeFactory.CreateAddNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0+1", "0 + 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
            Assert.Inconclusive("TODO: what to do in the case +5 ?");
        }

        [TestMethod]
        public void TestSubtract()
        {
            var expected = FormulaTreeFactory.CreateSubtractNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0-1", "0 - 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
            Assert.Inconclusive("TODO: what to do in the case -5 ?");
        }

        [TestMethod]
        public void TestMultiply()
        {
            var expected = FormulaTreeFactory.CreateMultiplyNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0*1", "0 * 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestDivide()
        {
            var expected = FormulaTreeFactory.CreateDivideNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0/1", "0 / 1", "0:1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region relational operators

        [TestMethod]
        public void TestEquals()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestLess()
        {
            var expected = FormulaTreeFactory.CreateLessNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0<1", "0 < 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestLessEqual()
        {
            var expected = FormulaTreeFactory.CreateLessEqualNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0<=1", "0 <= 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestGreater()
        {
            var expected = FormulaTreeFactory.CreateGreaterNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0>1", "0 > 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestGreaterEqual()
        {
            var expected = FormulaTreeFactory.CreateGreaterEqualNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0>=1", "0 >= 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region logic

        [TestMethod]
        public void TestTrue()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestFalse()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestAnd()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestOr()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestNot()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region min/max

        [TestMethod]
        public void TestMin()
        {
            var expected = FormulaTreeFactory.CreateMinNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "min{0,1}", "Min{0,1}", "min{0, 1}", "min(0, 1)", "min 0, 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestMax()
        {
            var expected = FormulaTreeFactory.CreateMaxNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "max{0,1}", "Max{0,1}", "max{0, 1}", "max(0, 1)", "max 0, 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region exponential function and logarithms

        [TestMethod]
        public void TestExp()
        {
            var expected = FormulaTreeFactory.CreateExpNode(_nodeZero);
            foreach (var input in new[] { "exp(0)", "Exp(0)", "exp 0", "e^0", "exp{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestLog()
        {
            var expected = FormulaTreeFactory.CreateLogNode(_nodeZero);
            foreach (var input in new[] { "log(0)", "Log(0)", "log 0" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestLn()
        {
            var expected = FormulaTreeFactory.CreateLnNode(_nodeZero);
            foreach (var input in new[] { "ln(0)", "ln(0)", "ln 0" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region trigonometric functions

        [TestMethod]
        public void TestSin()
        {
            var expected = FormulaTreeFactory.CreateSinNode(_nodeZero);
            foreach (var input in new[] { "sin(0)", "Sin(0)", "sin 0", "sin{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestCos()
        {
            var expected = FormulaTreeFactory.CreateCosNode(_nodeZero);
            foreach (var input in new[] { "cos(0)", "Cos(0)", "cos 0", "cos{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestTan()
        {
            var expected = FormulaTreeFactory.CreateTanNode(_nodeZero);
            foreach (var input in new[] { "tan(0)", "Tan(0)", "tan 0", "tan{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestArcsin()
        {
            var expected = FormulaTreeFactory.CreateArcsinNode(_nodeZero);
            foreach (var input in new[] { "arcsin(0)", "Arcsin(0)", "ArcSin(0)", "arcsin 0" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestArccos()
        {
            var expected = FormulaTreeFactory.CreateArccosNode(_nodeZero);
            foreach (var input in new[] { "arccos(0)", "Arccos(0)", "ArcCos(0)", "arccos 0" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestArcTan()
        {
            var expected = FormulaTreeFactory.CreateArctanNode(_nodeZero);
            foreach (var input in new[] { "arctan(0)", "Arctan(0)", "ArcTan(0)", "arctan 0" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

        #region miscellaneous functions

        [TestMethod]
        public void TestSqrt()
        {
            var expected = FormulaTreeFactory.CreateSqrtNode(_nodeZero);
            foreach (var input in new[] { "sqrt(0)", "Sqrt(0)", "sqrt 0", "sqrt{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestAbs()
        {
            var expected = FormulaTreeFactory.CreateAbsNode(_nodeZero);
            foreach (var input in new[] { "|0|", "abs(0)", "Abs(0)", "abs 0", "abs{0}" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestMod()
        {
            var expected = FormulaTreeFactory.CreateModNode(_nodeZero, _nodeOne);
            foreach (var input in new[] { "0 mod 1", "0 Mod 1" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        [TestMethod]
        public void TestRound()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestRandom()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region sensors

        [TestMethod]
        public void TestSensors()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region object variables

        [TestMethod]
        public void TestObjectVariables()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region user variables

        [TestMethod]
        public void TestUserVariable()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region brackets

        [TestMethod]
        public void TestParentheses()
        {
            var expected = FormulaTreeFactory.CreateParenthesesNode(_nodeZero);
            foreach (var input in new[] { "(0)", "( 0 )", "{0}", "[0]", "((0))", "(((0)))" })
            {
                IFormulaTree result;
                IEnumerable<string> parsingErrors;
                Assert.IsTrue(_parser.Parse(input, out result, out parsingErrors));
                Assert.IsFalse(parsingErrors.Any());
                Assert.AreEqual(expected, result);
            }
        }

        #endregion

    }
}
