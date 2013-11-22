using System;
using System.Globalization;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.FormulaEditor.Editor;
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
        public void FormulaSerializerTests_Null()
        {
            Assert.AreEqual(null, _serializer.Serialize(null));
        }

        #region numbers

        [TestMethod]
        public void FormulaSerializerTests_Number()
        {
            foreach (var value in new[] { 0, _random.NextDouble(), -_random.NextDouble() })
            {
                Assert.AreEqual(
                    expected: value.ToString(CultureInfo.CurrentCulture),
                    actual: _serializer.Serialize(FormulaTreeFactory.CreateNumberNode(value)));
            }
        }

        [TestMethod]
        public void FormulaSerializerTests_Pi()
        {
            Assert.AreEqual(
                expected: "pi",
                actual: _serializer.Serialize(FormulaTreeFactory.CreatePiNode()));
        }

        #endregion

        #region arithmetic

        [TestMethod]
        public void FormulaSerializerTests_Add()
        {
            Assert.AreEqual(
                expected: "0+1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAddNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case +5 ? new class FormulaNodeSign?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Subtract()
        {
            Assert.AreEqual(
                expected: "0-1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSubtractNode(_nodeZero, _nodeOne)));

            Assert.Inconclusive("Loose or tight?");
            Assert.Inconclusive("TODO: what to do in the case -5 ? new class FormulaNodeSign?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Multiply()
        {
            Assert.AreEqual(
                expected: "0*1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMultiplyNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Divide()
        {
            Assert.AreEqual(
                expected: "0/1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateDivideNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region relational operators

        [TestMethod]
        public void FormulaSerializerTests_Equals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_NotEquals()
        {
            Assert.Inconclusive();
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Less()
        {
            Assert.AreEqual(
                expected: "0<1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_LessEqual()
        {
            Assert.AreEqual(
                expected: "0<=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLessEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Greater()
        {
            Assert.AreEqual(
                expected: "0>1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_GreaterEqual()
        {
            Assert.AreEqual(
                expected: "0>=1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateGreaterEqualNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region logic

        [TestMethod]
        public void FormulaSerializerTests_True()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void FormulaSerializerTests_False()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void FormulaSerializerTests_And()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void FormulaSerializerTests_Or()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void FormulaSerializerTests_Not()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region min/max

        [TestMethod]
        public void FormulaSerializerTests_Min()
        {
            Assert.AreEqual(
                expected: "min{0,1}",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMinNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        [TestMethod]
        public void FormulaSerializerTests_Max()
        {
            Assert.AreEqual(
                expected: "max{0,1}",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateMaxNode(_nodeZero, _nodeOne)));
            Assert.Inconclusive("Loose or tight?");
        }

        #endregion

        #region exponential function and logarithms

        [TestMethod]
        public void FormulaSerializerTests_Exp()
        {
            Assert.AreEqual(
                expected: "exp(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateExpNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Log()
        {
            Assert.AreEqual(
                expected: "log(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLogNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Ln()
        {
            Assert.AreEqual(
                expected: "ln(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateLnNode(_nodeZero)));
        }

        #endregion

        #region trigonometric functions

        [TestMethod]
        public void FormulaSerializerTests_Sin()
        {
            Assert.AreEqual(
                expected: "sin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSinNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Cos()
        {
            Assert.AreEqual(
                expected: "cos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateCosNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Tan()
        {
            Assert.AreEqual(
                expected: "tan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateTanNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Arcsin()
        {
            Assert.AreEqual(
                expected: "arcsin(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArcsinNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Arccos()
        {
            Assert.AreEqual(
                expected: "arccos(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArccosNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_ArcTan()
        {
            Assert.AreEqual(
                expected: "arctan(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateArctanNode(_nodeZero)));
        }


        #endregion

        #region miscellaneous functions

        [TestMethod]
        public void FormulaSerializerTests_Sqrt()
        {
            Assert.AreEqual(
                expected: "sqrt(0)",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateSqrtNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Abs()
        {
            Assert.AreEqual(
                expected: "|0|",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateAbsNode(_nodeZero)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Mod()
        {
            Assert.AreEqual(
                expected: "0 mod 1",
                actual: _serializer.Serialize(FormulaTreeFactory.CreateModNode(_nodeZero, _nodeOne)));
        }

        [TestMethod]
        public void FormulaSerializerTests_Round()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void FormulaSerializerTests_Random()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region sensors

        [TestMethod]
        public void FormulaSerializerTests_Sensors()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region object variables

        [TestMethod]
        public void FormulaSerializerTests_ObjectVariables()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region user variables

        [TestMethod]
        public void FormulaSerializerTests_UserVariable()
        {
            Assert.Inconclusive();
        }

        #endregion

        #region brackets

        [TestMethod]
        public void FormulaSerializerTests_Parentheses()
        {
            Assert.AreEqual(
                 expected: "(0)",
                 actual: _serializer.Serialize(FormulaTreeFactory.CreateParenthesesNode(_nodeZero)));
        }

        #endregion

    }
}
