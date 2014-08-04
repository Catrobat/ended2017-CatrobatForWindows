using System;
using System.Globalization;
using System.Linq;
using System.Text;
using Catrobat.IDE.Core.Formulas;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Resources.Localization;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaInterpreter.Interpret" /> with syntactically invalid tokens. </summary>
    [TestClass]
    public class FormulaInterpreterSyntaxErrorTests
    {
        private readonly Random _random = new Random();
        private readonly FormulaTokenizer _tokenizer = new FormulaTokenizer(Enumerable.Empty<LocalVariable>(), Enumerable.Empty<GlobalVariable>());

        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
            ServiceLocator.CultureService.SetCulture(CultureInfo.InvariantCulture);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNullOrEmpty()
        {
            var message = AppResources.FormulaInterpreter_NullOrEmpty;
            TestParsingError(message, 0, 0, null);
            TestParsingError(message, 0, 0, string.Empty);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestDoubleValue()
        {
            var message1 = AppResources.FormulaInterpreter_DoubleValue;
            var message2 = AppResources.FormulaInterpreter_Brackets_ArgumentDoubleValue;

            TestParsingError(message1, 1, 0, "1 Inclination.X,");
            TestParsingError(message1, 3, 0, "1*2 Transparency)");
            TestParsingError(message1, 4, 0, "sin(1)cos");
            TestParsingError(message1, 1, 0, "1(2-)");
            TestParsingError(message1, 1, 0, "1(2-");
            TestParsingError(message1, 4, 0, "1*(2 π)*");
            TestParsingError(message2, 5, 0, "1*sin(2 π.)*");
            TestParsingError(message2, 3, 0, "sin(2 π,)*");
        }

        #region Number

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNumberSingleDecimalSeparator()
        {
            var message = AppResources.FormulaInterpreter_Number_SingleDecimalSeparator;

            TestParsingError(message, 0, 1, ".,");
            TestParsingError(message, 1, 1, "(.*)");
            TestParsingError(message, 3, 1, "1/-.-");
            TestParsingError(message, 2, 1, "exp(.)(.)");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNumberDoubleDecimalSeparator()
        {
            var message = AppResources.FormulaInterpreter_Number_DoubleDecimalSeparator;

            TestParsingError(message, 1, 1, "..,");
            TestParsingError(message, 2, 1, "(..,)");
            TestParsingError(message, 2, 1, "1..π");
            TestParsingError(message, 2, 1, ".1.2 sin");
            TestParsingError(message, 3, 1, "1.2.3=");
            TestParsingError(message, 4, 1, "1/-..)");
            TestParsingError(message, 3, 1, "exp(..)(..)");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestNumberOverflow()
        {
            var message = AppResources.FormulaInterpreter_Number_Overflow;

            // 2E308
            var sb = new StringBuilder();
            for (var i = 1; i <= 308; i++) sb.Append(0);
            var zeros = sb.ToString();

            TestParsingError(message, 0, 309, "2" + zeros + "+", randomDigits: false);
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBracketsUnmatchedParenthesis()
        {
            var message1 = AppResources.FormulaInterpreter_Brackets_UnmatchedOpeningParenthesis;
            var message2 = AppResources.FormulaInterpreter_Brackets_UnmatchedClosingParenthesis;

            TestParsingError(message1, 1, 0, "(");
            TestParsingError(message2, 0, 1, ").");
            TestParsingError(message1, 5, 0, "1/-((");
            TestParsingError(message1, 7, 0, "(1)*sin(π");
            TestParsingError(message2, 3, 1, "1/-))");
            TestParsingError(message2, 3, 1, "1+2)");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBracketsEmpty()
        {
            var message1 = AppResources.FormulaInterpreter_Brackets_EmptyParentheses;
            var message2 = AppResources.FormulaInterpreter_Brackets_EmptyArgument;

            TestParsingError(message1, 1, 0, "().");
            TestParsingError(message2, 2, 0, "sin().");
            TestParsingError(message2, 2, 0, "min().");
            TestParsingError(message2, 2, 0, "min(,).");
            TestParsingError(message2, 4, 0, "min(1,).");
            TestParsingError(message1, 2, 0, "(()<1).");
            TestParsingError(message2, 3, 0, "(sin())))");
            TestParsingError(message1, 5, 0, "min(1<()).");
            TestParsingError(message2, 6, 0, "min(1<log()))");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBracketsNonParameterParameterSeparator()
        {
            var message = AppResources.FormulaInterpreter_Brackets_NonArgumentParameterSeparator;

            TestParsingError(message, 0, 1, ",)");
            TestParsingError(message, 1, 1, "1,)");
            TestParsingError(message, 1, 1, "(,.))");
            TestParsingError(message, 3, 1, "1+(,))");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBracketsTooFewArguments()
        {
            var message = AppResources.FormulaInterpreter_Brackets_TooFewArguments;

            TestParsingError(message, 3, 0, "random(1))");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestBracketsTooManyArguments()
        {
            var message = AppResources.FormulaInterpreter_Brackets_TooManyArguments;

            TestParsingError(message, 3, 2, "sin(1,.))");
            TestParsingError(message, 5, 2, "random(1, 2,.))");
            TestParsingError(message, 5, 1, "1+sin(2,))");
        }

        #endregion

        #region Function

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestFunctionEmpty()
        {
            var message = AppResources.FormulaInterpreter_Function_Empty;

            TestParsingError(message, 1, 0, "sin");
            TestParsingError(message, 1, 0, "sin 1)");
            TestParsingError(message, 3, 0, "exp(log)");
            TestParsingError(message, 1, 0, "sin)");
        }

        #endregion

        #region Operator

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperatorEmptyPrefixOperator()
        {
            var message = AppResources.FormulaInterpreter_Operator_EmptyPrefixOperator;

            TestParsingError(message, 1, 0, "-");
            TestParsingError(message, 2, 0, "(-))");
            TestParsingError(message, 3, 0, "min(-),");
            TestParsingError(message, 2, 0, "--,");
            TestParsingError(message, 1, 0, "-*,");
            TestParsingError(message, 3, 0, " 1+-,");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperatorLeftEmptyInfixOperator()
        {
            var message = AppResources.FormulaInterpreter_Operator_LeftEmptyInfixOperator;

            TestParsingError(message, 0, 0, "*+");
            TestParsingError(message, 0, 0, "+");
            TestParsingError(message, 1, 0, "(+1))");
            TestParsingError(message, 2, 0, "min(+),");
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.Formulas")]
        public void TestOperatorRightEmptyInfixOperator()
        {
            var message = AppResources.FormulaInterpreter_Operator_RightEmptyInfixOperator;

            TestParsingError(message, 2, 0, "1+");
            TestParsingError(message, 3, 0, "(1+))");
            TestParsingError(message, 4, 0, "min(1+),");
            TestParsingError(message, 2, 0, "1*+");
        }

        #endregion

        #region Helpers

        private void TestParsingError(string expectedMessage, int expectedIndex, int expectedLength, string input, bool randomDigits = true)
        {
            // tokenize input
            ParsingError parsingError;
            var tokens = _tokenizer.Tokenize(input, out parsingError);
            Assert.IsNull(parsingError);

            // randomize digits
            if (tokens != null) tokens = tokens.Select(token => randomDigits && token is FormulaNodeNumber
                        ? FormulaTokenFactory.CreateDigitToken(_random.Next(0, 10))
                        : token);

            // interpret tokens
            ParsingError actual;
            Assert.IsNull(FormulaInterpreter.Interpret(tokens == null ? null : tokens.ToList(), out actual));

            // validate parsing errors
            Assert.IsNotNull(actual);
            Assert.AreEqual(expectedMessage, actual.Message);
            Assert.AreEqual(expectedIndex, actual.Index);
            Assert.AreEqual(expectedLength, actual.Length);
        }

        #endregion
    }
}
