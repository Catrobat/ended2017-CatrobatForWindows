using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaInterpreterTests
    {
        private readonly FormulaInterpreter _interpreter = new FormulaInterpreter();
        private readonly Random _random = new Random();

        [TestMethod]
        public void TestEmpty()
        {
            TestInterpreter(
              expected: null,
              tokens: Enumerable.Empty<IFormulaToken>());
        }

        [TestMethod]
        public void TestNumber()
        {
            string parsingError;
            for (var digit = 0; digit <= 9; digit++)
            {
                TestInterpreter(
                    expected: FormulaTreeFactory.CreateNumberNode(digit), 
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(digit) });
                TestInterpreter(
                        expected: FormulaTreeFactory.CreateNumberNode(-digit),
                        tokens: new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreateDigitToken(digit) });
            }
            TestInterpreter(
                    expected: FormulaTreeFactory.CreateNumberNode(42),
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestConstant(FormulaTreeFactory.CreatePiNode, FormulaTokenFactory.CreatePiToken);
        }


        [TestMethod]
        public void TestArithmetic()
        {
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAddNode, FormulaTokenFactory.CreatePlusToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateSubtractNode, FormulaTokenFactory.CreateMinusToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateMultiplyNode, FormulaTokenFactory.CreateMultiplyToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateDivideNode, FormulaTokenFactory.CreateDivideToken);
        }

        [TestMethod]
        public void TestRelationalOperators()
        {
            TestBoolInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateLessNode, FormulaTokenFactory.CreateLessToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateLessEqualNode, FormulaTokenFactory.CreateLessEqualToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateGreaterNode, FormulaTokenFactory.CreateGreaterToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateGreaterEqualNode, FormulaTokenFactory.CreateGreaterEqualToken);
        }


        [TestMethod]
        public void TestLogic()
        {
            TestConstant(FormulaTreeFactory.CreateTrueNode, FormulaTokenFactory.CreateTrueToken);
            TestConstant(FormulaTreeFactory.CreateFalseNode, FormulaTokenFactory.CreateFalseToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            Assert.Inconclusive("Not");
        }

        [TestMethod]
        public void TestMinMax()
        {
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateMinNode, FormulaTokenFactory.CreateMinToken);
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateMaxNode, FormulaTokenFactory.CreateMaxToken);
        }


        [TestMethod]
        public void TestExponentialFunctionAndLogarithms()
        {
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateExpNode, FormulaTokenFactory.CreateExpToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateLogNode, FormulaTokenFactory.CreateLogToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateLnNode, FormulaTokenFactory.CreateLnToken);
        }

        [TestMethod]
        public void TestTrigonometricFunctions()
        {
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateSinNode, FormulaTokenFactory.CreateSinToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateCosNode, FormulaTokenFactory.CreateCosToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateTanNode, FormulaTokenFactory.CreateTanToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArcsinNode, FormulaTokenFactory.CreateArcsinToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArccosNode, FormulaTokenFactory.CreateArccosToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArctanNode, FormulaTokenFactory.CreateArctanToken);
        }

        [TestMethod]
        public void TestMiscellaneousFunctions()
        {
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateSqrtNode, FormulaTokenFactory.CreateSqrtToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateRoundNode, FormulaTokenFactory.CreateRoundToken);
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateRandomNode, FormulaTokenFactory.CreateRandomToken);
            Assert.Inconclusive("Abs");
        }

        [TestMethod]
        public void TestSensors()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestObjectVariables()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestUserVariable()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestBrackets()
        {
            Assert.Inconclusive();
        }

        private void TestInterpreter(IFormulaTree expected, IEnumerable<IFormulaToken> tokens)
        {
            string parsingError;
            Assert.AreEqual(expected, _interpreter.Interpret(tokens, out parsingError));
            Assert.IsNull(parsingError);
        }

        private void TestConstant(Func<ConstantFormulaTree> expected, Func<IFormulaToken> token)
        {
            TestInterpreter(
                expected: expected.Invoke(),
                tokens: new[] {token.Invoke()});
        }

        private void TestBoolInfixOperator(Func<IFormulaTree, IFormulaTree, FormulaNodeInfixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateTruthValueNode(x), FormulaTreeFactory.CreateTruthValueNode(y)),
                tokens: new[] { FormulaTokenFactory.CreateTruthValueToken(x), token.Invoke(), FormulaTokenFactory.CreateTruthValueToken(y) });
        }

        private void TestDoubleInfixOperator(Func<IFormulaTree, IFormulaTree, FormulaNodeInfixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            var y = _random.Next(0, 10);
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)),
                tokens: new[] {FormulaTokenFactory.CreateDigitToken(x), token.Invoke(), FormulaTokenFactory.CreateDigitToken(y)});
        }

        private void TestDoubleUnaryFunction(Func<IFormulaTree, FormulaNodeUnaryFunction> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateDigitToken(x) });
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateParenthesisToken(false) });
        }

        private void TestDoubleBinaryFunction(Func<IFormulaTree, IFormulaTree, FormulaNodeBinaryFunction> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            var y = _random.Next(0, 10);
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateArgumentSeparatorToken(), FormulaTokenFactory.CreateDigitToken(y), FormulaTokenFactory.CreateParenthesisToken(false) });
        }
    }
}
