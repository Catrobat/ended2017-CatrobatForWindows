using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaInterpreterTests
    {
        private readonly Random _random = new Random();

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestNullOrEmpty()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(null, out parsingError));
            Assert.IsNull(parsingError);
            Assert.IsNull(FormulaInterpreter.Interpret(new IFormulaToken[] {}, out parsingError));
            Assert.IsNotNull(parsingError);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestConstants()
        {
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
            TestInterpreter(
                    expected: FormulaTreeFactory.CreateNumberNode(0.42),
                    tokens: new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(0), FormulaTokenFactory.CreateDecimalSeparatorToken(), FormulaTokenFactory.CreateDigitToken(4), FormulaTokenFactory.CreateDigitToken(2) });
            TestConstant(FormulaTreeFactory.CreatePiNode, FormulaTokenFactory.CreatePiToken);
            TestConstant(FormulaTreeFactory.CreateTrueNode, FormulaTokenFactory.CreateTrueToken);
            TestConstant(FormulaTreeFactory.CreateFalseNode, FormulaTokenFactory.CreateFalseToken);
        }


        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOperators()
        {
            TestDoubleInfixOperator(FormulaTreeFactory.CreateAddNode, FormulaTokenFactory.CreatePlusToken);
            TestDoubleInfixOperator(FormulaTreeFactory.CreateSubtractNode, FormulaTokenFactory.CreateMinusToken);
            TestInterpreter(
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestProperties()
        {
            TestConstant(FormulaTreeFactory.CreateBrightnessNode, FormulaTokenFactory.CreateBrightnessToken);
            TestConstant(FormulaTreeFactory.CreateLayerNode, FormulaTokenFactory.CreateLayerToken);
            TestConstant(FormulaTreeFactory.CreatePositionXNode, FormulaTokenFactory.CreatePositionXToken);
            TestConstant(FormulaTreeFactory.CreatePositionYNode, FormulaTokenFactory.CreatePositionYToken);
            TestConstant(FormulaTreeFactory.CreateRotationNode, FormulaTokenFactory.CreateRotationToken);
            TestConstant(FormulaTreeFactory.CreateSizeNode, FormulaTokenFactory.CreateSizeToken);
            TestConstant(FormulaTreeFactory.CreateTransparencyNode, FormulaTokenFactory.CreateTransparencyToken);
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestVariables()
        {
            TestVariable(FormulaTreeFactory.CreateLocalVariableNode, FormulaTokenFactory.CreateLocalVariableToken);
            TestVariable(FormulaTreeFactory.CreateGlobalVariableNode, FormulaTokenFactory.CreateGlobalVariableToken);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestBrackets()
        {
            var x = _random.Next();
            TestInterpreter(
                expected:
                    FormulaTreeFactory.CreateParenthesesNode(
                        FormulaTreeFactory.CreateNumberNode(x)), 
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateDigitToken(x), 
                    FormulaTokenFactory.CreateParenthesisToken(false) 
                });
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestOperatorOrder()
        {
            var x = _random.Next();
            var y = _random.Next();
            var z = _random.Next();

            // shortcuts to improve readability
            var xToken = FormulaTokenFactory.CreateDigitToken(x);
            var yToken = FormulaTokenFactory.CreateDigitToken(y);
            var zToken = FormulaTokenFactory.CreateDigitToken(z);
            var xNode = FormulaTreeFactory.CreateNumberNode(x);
            var yNode = FormulaTreeFactory.CreateNumberNode(y);
            var zNode = FormulaTreeFactory.CreateNumberNode(z);
            var plusToken = FormulaTokenFactory.CreatePlusToken();
            var multiplyToken = FormulaTokenFactory.CreateMultiplyToken();
            var equalsToken = FormulaTokenFactory.CreateEqualsToken();
            var lessToken = FormulaTokenFactory.CreateLessToken();
            var notToken = FormulaTokenFactory.CreateNotToken();
            Func<IFormulaTree, IFormulaTree, IFormulaTree> createAddNode = FormulaTreeFactory.CreateAddNode;
            Func<IFormulaTree, IFormulaTree, IFormulaTree> createMultiplyNode = FormulaTreeFactory.CreateMultiplyNode;
            Func<IFormulaTree, IFormulaTree, IFormulaTree> createEqualsNode = FormulaTreeFactory.CreateEqualsNode;
            Func<IFormulaTree, IFormulaTree, IFormulaTree> createLessNode = FormulaTreeFactory.CreateLessNode;
            Func<IFormulaTree, IFormulaTree> createNotNode = FormulaTreeFactory.CreateNotNode;

            TestInterpreter(
                expected: createAddNode(xNode, createMultiplyNode(yNode, zNode)),
                tokens: new IFormulaToken[] { xToken, plusToken, yToken, multiplyToken, zToken });
            TestInterpreter(
                expected: createAddNode(createMultiplyNode(xNode, yNode), zNode),
                tokens: new IFormulaToken[] { xToken, multiplyToken, yToken, plusToken, zToken });
            TestInterpreter(
                expected: createAddNode(xNode, createAddNode(yNode, zNode)), 
                tokens: new IFormulaToken[] { xToken, plusToken, yToken, plusToken, zToken });
            TestInterpreter(
                expected: createEqualsNode(createNotNode(xNode), yNode),
                tokens: new IFormulaToken[] { notToken, xToken, equalsToken, yToken });
            TestInterpreter(
                expected: createEqualsNode(xNode, createNotNode(yNode)),
                tokens: new IFormulaToken[] { xToken, equalsToken, notToken, yToken });
            TestInterpreter(
                expected: createNotNode(createLessNode(xNode, yNode)),
                tokens: new IFormulaToken[] { notToken, xToken, lessToken, yToken });
            TestInterpreter(
                expected: createLessNode(xNode, createNotNode(yNode)),
                tokens: new IFormulaToken[] { xToken, lessToken, notToken, yToken });
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestWrongParameter()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(
                tokens: new IFormulaToken[]
                {
                    FormulaTokenFactory.CreateSinToken(), 
                    FormulaTokenFactory.CreateParenthesisToken(true), 
                    FormulaTokenFactory.CreateDigitToken(0), 
                    FormulaTokenFactory.CreateParameterSeparatorToken(), 
                    FormulaTokenFactory.CreateDigitToken(0), 
                    FormulaTokenFactory.CreateParenthesisToken(false)
                },
                parsingError: out parsingError));
            Assert.IsNotNull(parsingError);
            Assert.Inconclusive();
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSemanticError()
        {
            ParsingError parsingError;
            Assert.IsNull(FormulaInterpreter.Interpret(
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


        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void MonkeyTest()
        {
            const int iterations = 100000;
            const int minLength = 0;
            const int maxLength = 10;
            
            var localVariable = new UserVariable
            {
                Name = "LocalTestVariable"
            };
            var globalVariable = new UserVariable
            {
                Name = "GlobalTestVariable"
            };
            var tokenCreators = typeof (FormulaTokenFactory).GetMethods()
                .Where(method => method.IsStatic)
                .Select<MethodInfo, Func<IFormulaToken>>(method => () => (IFormulaToken) method.Invoke(
                    obj: null,
                    parameters: method.GetParameters()
                        .Select(parameter =>
                        {
                            if (parameter.ParameterType == typeof(int)) return _random.Next();
                            if (parameter.ParameterType == typeof(bool)) return _random.NextBool();
                            if (parameter.ParameterType == typeof (UserVariable) && method.Name.ToLower().Contains("local")) return (object) localVariable;
                            if (parameter.ParameterType == typeof (UserVariable) && method.Name.ToLower().Contains("global")) return (object) globalVariable;
                            Assert.Inconclusive();
                            return null;
                        })
                        .ToArray()))
                .ToList();
            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var length = _random.Next(minLength, maxLength);
                var randomTokens = Enumerable.Range(1, length)
                    .Select(i => tokenCreators[_random.Next(0, tokenCreators.Count)].Invoke())
                    .ToList();
                ParsingError parsingError;
                var formula = FormulaInterpreter.Interpret(randomTokens, out parsingError);
                if (formula != null)
                {
                    Assert.IsTrue(formula.AsEnumerable().All(node => node != null));
                }
            }

        }

        #region Helpers

        private void TestInterpreter(IFormulaTree expected, IList<IFormulaToken> tokens)
        {
            ParsingError parsingError;
            Assert.AreEqual(expected, FormulaInterpreter.Interpret(tokens, out parsingError));
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
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateParenthesisToken(false) });
        }

        private void TestDoubleBinaryFunction(Func<IFormulaTree, IFormulaTree, FormulaNodeBinaryFunction> expected, Func<IFormulaToken> token)
        {
            var x = _random.Next(0, 10);
            var y = _random.Next(0, 10);
            TestInterpreter(
                expected: expected.Invoke(FormulaTreeFactory.CreateNumberNode(x), FormulaTreeFactory.CreateNumberNode(y)),
                tokens: new[] { token.Invoke(), FormulaTokenFactory.CreateParenthesisToken(true), FormulaTokenFactory.CreateDigitToken(x), FormulaTokenFactory.CreateParameterSeparatorToken(), FormulaTokenFactory.CreateDigitToken(y), FormulaTokenFactory.CreateParenthesisToken(false) });
        }

        private void TestVariable(Func<UserVariable, FormulaNodeVariable> expected, Func<UserVariable, IFormulaToken> token)
        {
            var x = new UserVariable
            {
                Name = "TestVariable"
            };
            var y = new UserVariable();
            TestInterpreter(
                expected: expected.Invoke(x),
                tokens: new[] { token.Invoke(x) });
            TestInterpreter(
                expected: expected.Invoke(y),
                tokens: new[] { token.Invoke(y) });
            TestInterpreter(
                expected: expected.Invoke(null),
                tokens: new[] { token.Invoke(null) });
        }

        #endregion
    }
}
