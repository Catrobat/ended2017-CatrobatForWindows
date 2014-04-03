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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNull()
        {
            Assert.AreEqual(string.Empty, _serializer.Serialize(null));
        }
        
        #region numbers

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNumber()
        {
            foreach (var value in new[] { 0, _random.NextDouble(), -_random.NextDouble() })
            {
                Assert.AreEqual(
                    expected: value.ToString(CultureInfo.CurrentCulture),
                    actual: _serializer.Serialize(FormulaTreeFactory.CreateNumberNode(value)));
            }
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestPi()
        {
            Assert.AreEqual(
                expected: "pi",
                actual: _serializer.Serialize(FormulaTreeFactory.CreatePiNode()));
        }

        #endregion

        #region arithmetic

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestAdd()
        {
            Assert.AreEqual(
                expected: "0+1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAddNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case +5 ? new class FormulaNodeSign?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSubtract()
        {
            Assert.AreEqual(
                expected: "0-1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSubtractNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case -5 ? new class FormulaNodeSign?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestMultiply()
        {
            Assert.AreEqual(
                expected: "0*1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMultiplyNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestDivide()
        {
            Assert.AreEqual(
                expected: "0/1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateDivideNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region relational operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestEquals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNotEquals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestLess()
        {
            Assert.AreEqual(
                expected: "0<1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestLessEqual()
        {
            Assert.AreEqual(
                expected: "0<=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestGreater()
        {
            Assert.AreEqual(
                expected: "0>1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestGreaterEqual()
        {
            Assert.AreEqual(
                expected: "0>=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region logic

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestTrue()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestFalse()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestAnd()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOr()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNot()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region min/max

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestMin()
        {
            Assert.AreEqual(
                expected: "min(0, 1)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMinNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestMax()
        {
            Assert.AreEqual(
                expected: "max(0, 1)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMaxNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region exponential function and logarithms

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestExp()
        {
            Assert.AreEqual(
                expected: "exp(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateExpNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestLog()
        {
            Assert.AreEqual(
                expected: "log(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLogNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestLn()
        {
            Assert.AreEqual(
                expected: "ln(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLnNode(_nodeZero)));
        }

        #endregion

        #region trigonometric functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSin()
        {
            Assert.AreEqual(
                expected: "sin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSinNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestCos()
        {
            Assert.AreEqual(
                expected: "cos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateCosNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestTan()
        {
            Assert.AreEqual(
                expected: "tan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateTanNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestArcsin()
        {
            Assert.AreEqual(
                expected: "arcsin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArcsinNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestArccos()
        {
            Assert.AreEqual(
                expected: "arccos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArccosNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestArcTan()
        {
            Assert.AreEqual(
                expected: "arctan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArctanNode(_nodeZero)));
        }


        #endregion

        #region miscellaneous functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSqrt()
        {
            Assert.AreEqual(
                expected: "sqrt(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSqrtNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestAbs()
        {
            Assert.AreEqual(
                expected: "abs(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAbsNode(_nodeZero)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestMod()
        {
            Assert.AreEqual(
                expected: "0 mod 1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateModuloNode(_nodeZero, _nodeOne)));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestRound()
        {
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestRandom()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSensors()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region object variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestObjectVariables()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region user variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestUserVariable()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestParentheses()
        {
            Assert.AreEqual(
                 expected: "(0)",
                 actual: _serializer.Serialize(FormulaTreeFactory.CreateParenthesesNode(_nodeZero)));
        }

        #endregion

    }
}
