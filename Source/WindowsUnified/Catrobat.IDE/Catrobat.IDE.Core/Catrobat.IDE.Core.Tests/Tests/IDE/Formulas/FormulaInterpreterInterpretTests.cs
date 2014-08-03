using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Tests.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.IDE.Formulas
{
    /// <summary>Tests <see cref="FormulaInterpreter.Interpret" /> with valid tokens. </summary>
    [TestClass]
    public class FormulaInterpreterInterpretTests
    {
        private readonly Random _random = new Random();

        #region Constants

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestConstants()
        {
            for (var digit = 0; digit <= 9; digit++)
            {
                TestInterpret(
                    expected: FormulaTreeFactory.CreateNumberNode(digit), 
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(digit) });
                TestInterpret(
                        expected: FormulaTreeFactory.CreateNumberNode(-digit),
                        tokens: new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreateDigitToken(digit) });
            }
            TestInterpret(
                    expected: FormulaTreeFactory.CreateNumberNode(42),
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestInterpret(
                    expected: FormulaTreeFactory.CreateNumberNode(0.42),
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(0), FormulaTokenFactory.CreateDecimalSeparatorToken(), FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestConstant(FormulaTreeFactory.CreatePiNode, FormulaTokenFactory.CreatePiToken);
            TestConstant(FormulaTreeFactory.CreateTrueNode, FormulaTokenFactory.CreateTrueToken);
            TestConstant(FormulaTreeFactory.CreateFalseNode, FormulaTokenFactory.CreateFalseToken);
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperators()
        {
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAddNode, FormulaTokenFactory.CreatePlusToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateSubtractNode, FormulaTokenFactory.CreateMinusToken);
            TestInterpret(
                    expected: FormulaTreeFactory.CreateNegativeSignNode(FormulaTreeFactory.CreatePiNode()),
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateMinusToken(), FormulaTokenFactory.CreatePiToken() });
            TestDoubleInfixOperator(FormulaTreeFactory.CreateMultiplyNode, FormulaTokenFactory.CreateMultiplyToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateDivideNode, FormulaTokenFactory.CreateDivideToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreatePowerNode, FormulaTokenFactory.CreateCaretToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateEqualsNode, FormulaTokenFactory.CreateEqualsToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateNotEqualsNode, FormulaTokenFactory.CreateNotEqualsToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateLessNode, FormulaTokenFactory.CreateLessToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateLessEqualNode, FormulaTokenFactory.CreateLessEqualToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterNode, FormulaTokenFactory.CreateGreaterToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateGreaterEqualNode, FormulaTokenFactory.CreateGreaterEqualToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateAndNode, FormulaTokenFactory.CreateAndToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            TestBoolInfixOperator(FormulaTreeFactory.CreateOrNode, FormulaTokenFactory.CreateOrToken);
            TestDoublePrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
            TestBoolPrefixOperator(FormulaTreeFactory.CreateNotNode, FormulaTokenFactory.CreateNotToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateModuloNode, FormulaTokenFactory.CreateModToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperatorOrder()
        {
            var x = _random.Next();
            var y = _random.Next();
            var z = _random.Next();

            // shortcuts to improve readability
            var xToken = FormulaTokenFactory.CreateDigitToken(x);
            var yToken = FormulaTokenFactory.CreateDigitToken(y);
            var zToken = FormulaTokenFactory.CreateDigitToken(z);
            var plusToken = FormulaTokenFactory.CreatePlusToken();
            var multiplyToken = FormulaTokenFactory.CreateMultiplyToken();
            var equalsToken = FormulaTokenFactory.CreateEqualsToken();
            var lessToken = FormulaTokenFactory.CreateLessToken();
            var notToken = FormulaTokenFactory.CreateNotToken();
            var xNode = FormulaTreeFactory.CreateNumberNode(x);
            var yNode = FormulaTreeFactory.CreateNumberNode(y);
            var zNode = FormulaTreeFactory.CreateNumberNode(z);
            Func<FormulaTree, FormulaTree, FormulaTree> createAddNode = FormulaTreeFactory.CreateAddNode;
            Func<FormulaTree, FormulaTree, FormulaTree> createMultiplyNode = FormulaTreeFactory.CreateMultiplyNode;
            Func<FormulaTree, FormulaTree, FormulaTree> createEqualsNode = FormulaTreeFactory.CreateEqualsNode;
            Func<FormulaTree, FormulaTree, FormulaTree> createLessNode = FormulaTreeFactory.CreateLessNode;
            Func<FormulaTree, FormulaTree> createNotNode = FormulaTreeFactory.CreateNotNode;

            TestInterpret(
                expected: createAddNode(xNode, createMultiplyNode(yNode, zNode)),
                tokens: new IFormulaToken[] { xToken, plusToken, yToken, multiplyToken, zToken });
            TestInterpret(
                expected: createAddNode(createMultiplyNode(xNode, yNode), zNode),
                tokens: new IFormulaToken[] { xToken, multiplyToken, yToken, plusToken, zToken });
            TestInterpret(
                expected: createAddNode(xNode, createAddNode(yNode, zNode)),
                tokens: new IFormulaToken[] { xToken, plusToken, yToken, plusToken, zToken });
            TestInterpret(
                expected: createEqualsNode(createNotNode(xNode), yNode),
                tokens: new IFormulaToken[] { notToken, xToken, equalsToken, yToken });
            TestInterpret(
                expected: createEqualsNode(xNode, createNotNode(yNode)),
                tokens: new IFormulaToken[] { xToken, equalsToken, notToken, yToken });
            TestInterpret(
                expected: createNotNode(createLessNode(xNode, yNode)),
                tokens: new IFormulaToken[] { notToken, xToken, lessToken, yToken });
            TestInterpret(
                expected: createLessNode(xNode, createNotNode(yNode)),
                tokens: new IFormulaToken[] { xToken, lessToken, notToken, yToken });
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctions()
        {
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateExpNode, FormulaTokenFactory.CreateExpToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateLogNode, FormulaTokenFactory.CreateLogToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateLnNode, FormulaTokenFactory.CreateLnToken);
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateMinNode, FormulaTokenFactory.CreateMinToken);
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateMaxNode, FormulaTokenFactory.CreateMaxToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateSinNode, FormulaTokenFactory.CreateSinToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateCosNode, FormulaTokenFactory.CreateCosToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateTanNode, FormulaTokenFactory.CreateTanToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArcsinNode, FormulaTokenFactory.CreateArcsinToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArccosNode, FormulaTokenFactory.CreateArccosToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateArctanNode, FormulaTokenFactory.CreateArctanToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateSqrtNode, FormulaTokenFactory.CreateSqrtToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateAbsNode, FormulaTokenFactory.CreateAbsToken);
            TestDoubleUnaryFunction(FormulaTreeFactory.CreateRoundNode, FormulaTokenFactory.CreateRoundToken);
            TestDoubleBinaryFunction(FormulaTreeFactory.CreateRandomNode, FormulaTokenFactory.CreateRandomToken);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestSensors()
        {
            TestConstant(FormulaTreeFactory.CreateAccelerationXNode, FormulaTokenFactory.CreateAccelerationXToken);
            TestConstant(FormulaTreeFactory.CreateAccelerationYNode, FormulaTokenFactory.CreateAccelerationYToken);
            TestConstant(FormulaTreeFactory.CreateAccelerationZNode, FormulaTokenFactory.CreateAccelerationZToken);
            TestConstant(FormulaTreeFactory.CreateCompassNode, FormulaTokenFactory.CreateCompassToken);
            TestConstant(FormulaTreeFactory.CreateInclinationXNode, FormulaTokenFactory.CreateInclinationXToken);
            TestConstant(FormulaTreeFactory.CreateInclinationYNode, FormulaTokenFactory.CreateInclinationYToken);
            TestConstant(FormulaTreeFactory.CreateLoudnessNode, FormulaTokenFactory.CreateLoudnessToken);
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestProperties()
        {
            TestConstant(FormulaTreeFactory.CreateBrightnessNode, FormulaTokenFactory.CreateBrightnessToken);
            TestConstant(FormulaTreeFactory.CreateLayerNode, FormulaTokenFactory.CreateLayerToken);
            TestConstant(FormulaTreeFactory.CreatePositionXNode, FormulaTokenFactory.CreatePositionXToken);
            TestConstant(FormulaTreeFactory.CreatePositionYNode, FormulaTokenFactory.CreatePositionYToken);
            TestConstant(FormulaTreeFactory.CreateRotationNode, FormulaTokenFactory.CreateRotationToken);
            TestConstant(FormulaTreeFactory.CreateSizeNode, FormulaTokenFactory.CreateSizeToken);
            TestConstant(FormulaTreeFactory.CreateTransparencyNode, FormulaTokenFactory.CreateTransparencyToken);
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestVariables()
        {
            TestVariable<LocalVariable>(FormulaTreeFactory.CreateLocalVariableNode, FormulaTokenFactory.CreateLocalVariableToken);
            TestVariable<GlobalVariable>(FormulaTreeFactory.CreateGlobalVariableNode, FormulaTokenFactory.CreateGlobalVariableToken);
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBrackets()
        {
            var x = _random.Next();
            var y = _random.Next();
            var z = _random.Next();

            // shortcuts to improve readability
            var xToken = FormulaTokenFactory.CreateDigitToken(x);
            var yToken = FormulaTokenFactory.CreateDigitToken(y);
            var zToken = FormulaTokenFactory.CreateDigitToken(z);
            var openingToken = FormulaTokenFactory.CreateParenthesisToken(true);
            var closingToken = FormulaTokenFactory.CreateParenthesisToken(false);
            var xNode = FormulaTreeFactory.CreateNumberNode(x);
            var yNode = FormulaTreeFactory.CreateNumberNode(y);
            var zNode = FormulaTreeFactory.CreateNumberNode(z);
            Func<FormulaTree, FormulaTree> createParenthesesNode = FormulaTreeFactory.CreateParenthesesNode;

            // (x)
            TestInterpret(
                expected: createParenthesesNode(xNode),
                tokens: new IFormulaToken[] {openingToken, xToken, closingToken});

            // ((x))
            TestInterpret(
                expected: createParenthesesNode(xNode),
                tokens: new IFormulaToken[] {openingToken, openingToken, xToken, closingToken, closingToken});

            // (x+y)*z
            TestInterpret(
                expected: FormulaTreeFactory.CreateMultiplyNode(createParenthesesNode(FormulaTreeFactory.CreateAddNode(xNode, yNode)), zNode),
                tokens: new IFormulaToken[]
                {
                    openingToken, xToken, FormulaTokenFactory.CreatePlusToken(), yToken, closingToken, 
                    FormulaTokenFactory.CreateMultiplyToken(), zToken
                });
        }
        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestRedundantBrackets()
        {
            var x = _random.Next();
            var y = _random.Next();

            // shortcuts to improve readability
            var xToken = FormulaTokenFactory.CreateDigitToken(x);
            var yToken = FormulaTokenFactory.CreateDigitToken(y);
            var openingToken = FormulaTokenFactory.CreateParenthesisToken(true);
            var closingToken = FormulaTokenFactory.CreateParenthesisToken(false);
            var xNode = FormulaTreeFactory.CreateNumberNode(x);
            var yNode = FormulaTreeFactory.CreateNumberNode(y);
            Func<FormulaTree, FormulaTree> createParenthesesNode = FormulaTreeFactory.CreateParenthesesNode;

            // ((x))
            TestInterpret(
                expected: createParenthesesNode(xNode),
                tokens: new IFormulaToken[] { openingToken, openingToken, xToken, closingToken, closingToken });

            // sin((x))
            TestInterpret(
                expected: FormulaTreeFactory.CreateSinNode(xNode),
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    openingToken, openingToken, xToken, closingToken, closingToken
                });

            // min((x), (y))
            TestInterpret(
                expected: FormulaTreeFactory.CreateMinNode(xNode, yNode),
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateMinToken(), openingToken, 
                    openingToken, xToken, closingToken, FormulaTokenFactory.CreateParameterSeparatorToken(), 
                    openingToken, yToken, closingToken, closingToken
                });
        }


        #endregion

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void MonkeyTest()
        {
            const int iterations = 100000;
            const int minLength = 1;
            const int maxLength = 10;

            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var length = _random.Next(minLength, maxLength);
                var tokens = Enumerable.Range(1, length).Select(i => _random.NextFormulaToken()).ToList();
                ParsingError parsingError;
                var formula = FormulaInterpreter.Interpret(tokens, out parsingError);
                if (formula != null)
                {
                    Assert.IsTrue(formula.AsEnumerable().All(node => node != null));
                }
            }
        }

        #region Helpers

        private void TestInterpret(FormulaTree expected, IList<IFormulaToken> tokens)
        {
            ParsingError parsingError;
            ModelAssert.AreTestEqual(expected, FormulaInterpreter.Interpret(tokens, out parsingError));
            Assert.IsNull(parsingError);
        }

        private void TestConstant(Func<ConstantFormulaTree> expected, Func<IFormulaToken> token)
        {
            TestInterpret(
                expected: expected.Invoke(),
                tokens: new[] {token.Invoke()});
        }

        private void TestBoolPrefixOperator(Func<FormulaTree, FormulaNodePrefixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.NextBool();
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateTruthValueNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateTruthValueToken(x) });
        }

        private void TestDoublePrefixOperator(Func<FormulaTree, FormulaNodePrefixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateDigitToken(x) });
        }

        private void TestBoolInfixOperator(Func<FormulaTree, FormulaTree, FormulaNodeInfixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.NextBool();
            var y = _random.NextBool();
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateTruthValueNode(x), FormulaTreeFactory.CreateTruthValueNode(y)),
                tokens: new[] { FormulaTokenFactory.CreateTruthValueToken(x), token.Invoke(), FormulaTokenFactory.CreateTruthValueToken(y) });
        }

        private void TestDoubleInfixOperator(Func<FormulaTree, FormulaTree, FormulaNodeInfixOperator> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            var y = _random.Next(0, 10);
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)),
                tokens: new[] {FormulaTokenFactory.CreateDigitToken(x), token.Invoke(), FormulaTokenFactory.CreateDigitToken(y)});
        }

        private void TestDoubleUnaryFunction(Func<FormulaTree, FormulaNodeUnaryFunction> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateParenthesisToken(false) });
        }

        private void TestDoubleBinaryFunction(Func<FormulaTree, FormulaTree, FormulaNodeBinaryFunction> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            var y = _random.Next(0, 10);
            TestInterpret(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateParameterSeparatorToken(), FormulaTokenFactory.CreateDigitToken(y), FormulaTokenFactory.CreateParenthesisToken(false) });
        }

        private void TestVariable<TVariable>(Func<TVariable, FormulaNodeVariable> expected, Func<TVariable, IFormulaToken> token) where TVariable : Variable, new()
        {
            var x = new TVariable
            {
                Name = "TestVariable"
            };
            var y = new TVariable();
            TestInterpret(
                expected: expected.Invoke(x),
                tokens: new[] { token.Invoke(x) });
            TestInterpret(
                expected: expected.Invoke(y),
                tokens: new[] { token.Invoke(y) });
            TestInterpret(
                expected: expected.Invoke(null),
                tokens: new[] { token.Invoke(null) });
        }

        #endregion
    }
}
