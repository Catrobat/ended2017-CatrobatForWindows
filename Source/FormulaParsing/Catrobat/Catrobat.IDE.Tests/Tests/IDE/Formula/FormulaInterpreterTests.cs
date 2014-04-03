using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaToken;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.FormulaEditor;
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
        public void TestNullOrEmpty()
        {
            IFormulaToken formula;
            ParsingError parsingError;
            Assert.AreEqual(null, _interpreter.Interpret(null, out parsingError));
            Assert.IsNull(parsingError);
            Assert.AreEqual(null, _interpreter.Interpret(Enumerable.Empty<IFormulaToken>(), out parsingError));
            Assert.IsNotNull(parsingError);
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
            TestDoubleInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateLessNode, FormulaTokenFactory.CreateLessToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateLessEqualNode, FormulaTokenFactory.CreateLessEqualToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterNode, FormulaTokenFactory.CreateGreaterToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterEqualNode, FormulaTokenFactory.CreateGreaterEqualToken);
        }


        [TestMethod]
        public void TestLogic()
        {
            TestConstant(FormulaTreeFactory.CreateTrueNode, FormulaTokenFactory.CreateTrueToken);
            TestConstant(FormulaTreeFactory.CreateFalseNode, FormulaTokenFactory.CreateFalseToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            TestDoublePrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
            TestBoolPrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
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
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateAbsNode, FormulaTokenFactory.CreateAbsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateModuloNode, FormulaTokenFactory.CreateModToken);
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
            var x = _random.NextDouble();
            TestInterpreter(
                expected:
                    FormulaTreeFactory.CreateParenthesesNode(
                        FormulaTreeFactory.CreateNumberNode(x)), 
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateNumberToken(x), 
                    FormulaTokenFactory.CreateParenthesisToken(false), 
                });
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestOperatorOrder()
        {
            var x = _random.NextDouble();
            var y = _random.NextDouble();
            var z = _random.NextDouble();
            TestInterpreter(
                expected:
                    FormulaTreeFactory.CreateAddNode(
                        FormulaTreeFactory.CreateNumberNode(x),
                        FormulaTreeFactory.CreateMultiplyNode(
                            FormulaTreeFactory.CreateNumberNode(y), 
                            FormulaTreeFactory.CreateNumberNode(z))),
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateNumberToken(x), 
                    FormulaTokenFactory.CreatePlusToken(), 
                    FormulaTokenFactory.CreateNumberToken(y), 
                    FormulaTokenFactory.CreateMultiplyToken(), 
                    FormulaTokenFactory.CreateNumberToken(z), 
                });
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestWrongParameter()
        {
            ParsingError parsingError;
            Assert.IsNull(_interpreter.Interpret(
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateNumberToken(0), 
                    FormulaTokenFactory.CreateArgumentSeparatorToken(), 
                    FormulaTokenFactory.CreateNumberToken(0), 
                    FormulaTokenFactory.CreateParenthesisToken(false)
                },
                parsingError: out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestSemanticError()
        {
            ParsingError parsingError;
            Assert.IsNull(_interpreter.Interpret(
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateTrueToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(false)
                }, 
                parsingError:out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.Inconclusive();
        }

        #region Helpers

        private void TestInterpreter(IFormulaTree expected, IEnumerable<IFormulaToken> tokens)
        {
            ParsingError parsingError;
            Assert.AreEqual(expected, _interpreter.Interpret(tokens, out parsingError));
            Assert.IsNull(parsingError);
        }

        private void TestConstant(Func<ConstantFormulaTree> expected, Func<IFormulaToken> token)
        {
            TestInterpreter(
                expected: expected.Invoke(),
                tokens: new[] {token.Invoke()});
        }

        private void TestBoolPrefixOperator(Func<IFormulaTree, FormulaNodePrefixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.NextBool();
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateTruthValueToken(x) });
        }

        private void TestDoublePrefixOperator(Func<IFormulaTree, FormulaNodePrefixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateDigitToken(x) });
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

        #endregion
    }
}
