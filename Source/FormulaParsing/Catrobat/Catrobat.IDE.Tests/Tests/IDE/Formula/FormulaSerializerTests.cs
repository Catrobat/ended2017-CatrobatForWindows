using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.FormulaEditor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaSerializerTests
    {
        private readonly FormulaSerializer _serializer = new FormulaSerializer();
        private readonly Random _random = new Random();
        private readonly IFormulaTree _nodeZero = FormulaTreeFactory.CreateNumberNode(0);
        private readonly IFormulaTree _nodeOne = FormulaTreeFactory.CreateNumberNode(1);

        [TestMethod]
        public void TestNull()
        {
            Assert.AreEqual(string.Empty, _serializer.Serialize(null));
        }
        
        #region numbers

        [TestMethod]
        public void TestNumber()
        {
            foreach (var value in new[] { 0, _random.NextDouble(), -_random.NextDouble() })
            {
                Assert.AreEqual(
                    expected: value.ToString(CultureInfo.CurrentCulture),
                    actual: _serializer.Serialize(FormulaTreeFactory.CreateNumberNode(value)));
            }
        }

        [TestMethod]
        public void TestPi()
        {
            Assert.AreEqual(
                expected: "pi",
                actual: _serializer.Serialize(FormulaTreeFactory.CreatePiNode()));
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void TestAdd()
        {
            Assert.AreEqual(
                expected: "0+1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAddNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case +5 ? new class FormulaNodeSign?");
        }

        [TestMethod]
        public void TestSubtract()
        {
            Assert.AreEqual(
                expected: "0-1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSubtractNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case -5 ? new class FormulaNodeSign?");
        }

        [TestMethod]
        public void TestMultiply()
        {
            Assert.AreEqual(
                expected: "0*1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMultiplyNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestDivide()
        {
            Assert.AreEqual(
                expected: "0/1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateDivideNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region relational operators

        [TestMethod]
        public void TestEquals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestNotEquals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestLess()
        {
            Assert.AreEqual(
                expected: "0<1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestLessEqual()
        {
            Assert.AreEqual(
                expected: "0<=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestGreater()
        {
            Assert.AreEqual(
                expected: "0>1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestGreaterEqual()
        {
            Assert.AreEqual(
                expected: "0>=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
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
            Assert.AreEqual(
                expected: "min(0, 1)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMinNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void TestMax()
        {
            Assert.AreEqual(
                expected: "max(0, 1)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMaxNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region exponential function and logarithms

        [TestMethod]
        public void TestExp()
        {
            Assert.AreEqual(
                expected: "exp(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateExpNode(_nodeZero)));
        }

        [TestMethod]
        public void TestLog()
        {
            Assert.AreEqual(
                expected: "log(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLogNode(_nodeZero)));
        }

        [TestMethod]
        public void TestLn()
        {
            Assert.AreEqual(
                expected: "ln(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLnNode(_nodeZero)));
        }

        #endregion

        #region trigonometric functions

        [TestMethod]
        public void TestSin()
        {
            Assert.AreEqual(
                expected: "sin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSinNode(_nodeZero)));
        }

        [TestMethod]
        public void TestCos()
        {
            Assert.AreEqual(
                expected: "cos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateCosNode(_nodeZero)));
        }

        [TestMethod]
        public void TestTan()
        {
            Assert.AreEqual(
                expected: "tan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateTanNode(_nodeZero)));
        }

        [TestMethod]
        public void TestArcsin()
        {
            Assert.AreEqual(
                expected: "arcsin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArcsinNode(_nodeZero)));
        }

        [TestMethod]
        public void TestArccos()
        {
            Assert.AreEqual(
                expected: "arccos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArccosNode(_nodeZero)));
        }

        [TestMethod]
        public void TestArcTan()
        {
            Assert.AreEqual(
                expected: "arctan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArctanNode(_nodeZero)));
        }


        #endregion

        #region miscellaneous functions

        [TestMethod]
        public void TestSqrt()
        {
            Assert.AreEqual(
                expected: "sqrt(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSqrtNode(_nodeZero)));
        }

        [TestMethod]
        public void TestAbs()
        {
            Assert.AreEqual(
                expected: "abs(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAbsNode(_nodeZero)));
        }

        [TestMethod]
        public void TestMod()
        {
            Assert.AreEqual(
                expected: "0 mod 1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateModNode(_nodeZero, _nodeOne)));
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
            Assert.AreEqual(
                 expected: "(0)",
                 actual: _serializer.Serialize(FormulaTreeFactory.CreateParenthesesNode(_nodeZero)));
        }

        #endregion

    }
}
