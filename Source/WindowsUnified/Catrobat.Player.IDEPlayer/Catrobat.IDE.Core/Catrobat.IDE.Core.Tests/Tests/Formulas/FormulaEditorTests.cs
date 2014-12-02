using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Models.Formulas.Tokens;
using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Tests.Extensions;
using Catrobat.IDE.Core.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Core.Tests.Tests.Formulas
{
    /// <summary>Tests <see cref="FormulaEditor" />. </summary>
    [TestClass]
    public class FormulaEditorTests
    {
        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);

            // use culture different to CultureInfo.CurrentCulture (1.2 vs. 1,2)
            ServiceLocator.CultureService.SetCulture(new CultureInfo("de"));
        }

        #region Constants

        [TestMethod, TestCategory("Formulas")]
        public void TestConstants()
        {
            TestEditor(FormulaTokenFactory.CreateDigitToken(0), FormulaEditorKey.D0);
            TestEditor(FormulaTokenFactory.CreateDigitToken(1), FormulaEditorKey.D1);
            TestEditor(FormulaTokenFactory.CreateDigitToken(2), FormulaEditorKey.D2);
            TestEditor(FormulaTokenFactory.CreateDigitToken(3), FormulaEditorKey.D3);
            TestEditor(FormulaTokenFactory.CreateDigitToken(4), FormulaEditorKey.D4);
            TestEditor(FormulaTokenFactory.CreateDigitToken(5), FormulaEditorKey.D5);
            TestEditor(FormulaTokenFactory.CreateDigitToken(6), FormulaEditorKey.D6);
            TestEditor(FormulaTokenFactory.CreateDigitToken(7), FormulaEditorKey.D7);
            TestEditor(FormulaTokenFactory.CreateDigitToken(8), FormulaEditorKey.D8);
            TestEditor(FormulaTokenFactory.CreateDigitToken(9), FormulaEditorKey.D9);
            TestEditor(FormulaTokenFactory.CreateDecimalSeparatorToken, FormulaEditorKey.DecimalSeparator);
            TestEditor(FormulaTokenFactory.CreatePiToken, FormulaEditorKey.Pi);
            TestEditor(FormulaTokenFactory.CreateTrueToken, FormulaEditorKey.True);
            TestEditor(FormulaTokenFactory.CreateFalseToken, FormulaEditorKey.False);
        }

        #endregion

        #region Operators

        [TestMethod, TestCategory("Formulas")]
        public void TestOperators()
        {
            TestEditor(FormulaTokenFactory.CreatePlusToken, FormulaEditorKey.Plus);
            TestEditor(FormulaTokenFactory.CreateMinusToken, FormulaEditorKey.Minus);
            TestEditor(FormulaTokenFactory.CreateMultiplyToken, FormulaEditorKey.Multiply);
            TestEditor(FormulaTokenFactory.CreateDivideToken, FormulaEditorKey.Divide);
            TestEditor(FormulaTokenFactory.CreateCaretToken, FormulaEditorKey.Caret);
            TestEditor(FormulaTokenFactory.CreateEqualsToken, FormulaEditorKey.Equals);
            TestEditor(FormulaTokenFactory.CreateNotEqualsToken, FormulaEditorKey.NotEquals);
            TestEditor(FormulaTokenFactory.CreateLessToken, FormulaEditorKey.Less);
            TestEditor(FormulaTokenFactory.CreateLessEqualToken, FormulaEditorKey.LessEqual);
            TestEditor(FormulaTokenFactory.CreateGreaterToken, FormulaEditorKey.Greater);
            TestEditor(FormulaTokenFactory.CreateGreaterEqualToken, FormulaEditorKey.GreaterEqual);
            TestEditor(FormulaTokenFactory.CreateAndToken, FormulaEditorKey.And);
            TestEditor(FormulaTokenFactory.CreateOrToken, FormulaEditorKey.Or);
            TestEditor(FormulaTokenFactory.CreateNotToken, FormulaEditorKey.Not);
            TestEditor(FormulaTokenFactory.CreateModToken, FormulaEditorKey.Mod);
        }

        #endregion

        #region Functions

        [TestMethod, TestCategory("Formulas")]
        public void TestFunctions()
        {
            var openingParenthesis = FormulaTokenFactory.CreateParenthesisToken(true);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateExpToken(), openingParenthesis }, FormulaEditorKey.Exp);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateLogToken(), openingParenthesis }, FormulaEditorKey.Log);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateLnToken(), openingParenthesis }, FormulaEditorKey.Ln);
            TestEditor(new IFormulaToken[] {  FormulaTokenFactory.CreateMinToken(), openingParenthesis }, FormulaEditorKey.Min);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateMaxToken(), openingParenthesis }, FormulaEditorKey.Max);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateSinToken(), openingParenthesis }, FormulaEditorKey.Sin);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateCosToken(), openingParenthesis }, FormulaEditorKey.Cos);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateTanToken(), openingParenthesis }, FormulaEditorKey.Tan);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArcsinToken(), openingParenthesis }, FormulaEditorKey.Arcsin);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArccosToken(), openingParenthesis }, FormulaEditorKey.Arccos);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArctanToken(), openingParenthesis }, FormulaEditorKey.Arctan);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateSqrtToken(), openingParenthesis }, FormulaEditorKey.Sqrt);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateAbsToken(), openingParenthesis }, FormulaEditorKey.Abs);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateRoundToken(), openingParenthesis }, FormulaEditorKey.Round);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateRandomToken(), openingParenthesis }, FormulaEditorKey.Random);
        }

        #endregion

        #region Sensors

        [TestMethod, TestCategory("Formulas")]
        public void TestSensors()
        {
            TestEditor(FormulaTokenFactory.CreateAccelerationXToken, FormulaEditorKey.AccelerationX);
            TestEditor(FormulaTokenFactory.CreateAccelerationYToken, FormulaEditorKey.AccelerationY);
            TestEditor(FormulaTokenFactory.CreateAccelerationZToken, FormulaEditorKey.AccelerationZ);
            TestEditor(FormulaTokenFactory.CreateCompassToken, FormulaEditorKey.Compass);
            TestEditor(FormulaTokenFactory.CreateInclinationXToken, FormulaEditorKey.InclinationX);
            TestEditor(FormulaTokenFactory.CreateInclinationYToken, FormulaEditorKey.InclinationY);
            TestEditor(FormulaTokenFactory.CreateLoudnessToken, FormulaEditorKey.Loudness);
        }

        #endregion

        #region Properties

        [TestMethod, TestCategory("Formulas")]
        public void TestProperties()
        {
            TestEditor(FormulaTokenFactory.CreateBrightnessToken, FormulaEditorKey.Brightness);
            TestEditor(FormulaTokenFactory.CreateLayerToken, FormulaEditorKey.Layer);
            TestEditor(FormulaTokenFactory.CreateTransparencyToken, FormulaEditorKey.Transparency);
            TestEditor(FormulaTokenFactory.CreatePositionXToken, FormulaEditorKey.PositionX);
            TestEditor(FormulaTokenFactory.CreatePositionYToken, FormulaEditorKey.PositionY);
            TestEditor(FormulaTokenFactory.CreateRotationToken, FormulaEditorKey.Rotation);
            TestEditor(FormulaTokenFactory.CreateSizeToken, FormulaEditorKey.Size);
        }

        #endregion

        #region Variables

        [TestMethod, TestCategory("Formulas")]
        public void TestVariables()
        {
            TestEditor<LocalVariable>(FormulaTokenFactory.CreateLocalVariableToken, FormulaEditorKey.LocalVariable);
            TestEditor<GlobalVariable>(FormulaTokenFactory.CreateGlobalVariableToken, FormulaEditorKey.GlobalVariable);
        }

        #endregion

        #region Brackets

        [TestMethod, TestCategory("Formulas")]
        public void TestBrackets()
        {
            TestEditor(FormulaTokenFactory.CreateParenthesisToken(true), FormulaEditorKey.OpeningParenthesis);
            TestEditor(FormulaTokenFactory.CreateParenthesisToken(false), FormulaEditorKey.ClosingParenthesis);
        }

        #endregion

        [TestMethod, TestCategory("Formulas")]
        public void TestDelete()
        {
            var editor = new FormulaEditor();
            Assert.IsFalse(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Exp));
            editor.CaretIndex = 2;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Delete));
            editor.CaretIndex = 1;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Delete));
            editor.CaretIndex = 0;
            Assert.IsFalse(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Exp));
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestUndoRedo()
        {
            var editor = new FormulaEditor();
            Assert.IsFalse(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D4));
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            editor.CaretIndex = 1;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D2));
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            editor.CaretIndex = 0;
            Assert.IsTrue(editor.Undo());
            EnumerableAssert.AreTestEqual(new[] { FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            Assert.AreEqual(1, editor.CaretIndex);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            Assert.IsTrue(editor.Undo());
            EnumerableAssert.AreTestEqual(null, editor.Tokens);
            Assert.IsFalse(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            Assert.IsTrue(editor.Redo());
            EnumerableAssert.AreTestEqual(new[] { FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            editor.SelectionStart = 0;
            editor.SelectionLength = 1;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D2));
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            Assert.IsTrue(editor.Undo());
            EnumerableAssert.AreTestEqual(new[] { FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            Assert.AreEqual(0, editor.SelectionStart);
            Assert.AreEqual(1, editor.SelectionLength);
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestBindings()
        {
            var editor = new FormulaEditor
            {
                Formula = FormulaTreeFactory.CreateNumberNode(2)
            };
            EnumerableAssert.AreTestEqual(new[] { FormulaTokenFactory.CreateDigitToken(2) }, editor.Tokens);

            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D4));
            ModelAssert.AreTestEqual(FormulaTreeFactory.CreateNumberNode(42), editor.Formula);
            Assert.IsNull(editor.ParsingError);

            editor.CaretIndex = 1;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Plus));
            ModelAssert.AreTestEqual(FormulaTreeFactory.CreateAddNode(FormulaTreeFactory.CreateNumberNode(4), FormulaTreeFactory.CreateNumberNode(2)), editor.Formula);
            Assert.IsNull(editor.ParsingError);

            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Plus));
            ModelAssert.AreTestEqual(FormulaTreeFactory.CreateAddNode(FormulaTreeFactory.CreateNumberNode(4), FormulaTreeFactory.CreateNumberNode(2)), editor.Formula);
            Assert.IsNotNull(editor.ParsingError);
        }

        [TestMethod, TestCategory("Formulas")]
        public void TestSelection()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            var editor = new FormulaEditor
            {
                Formula = FormulaTreeFactory.CreateNumberNode(1234)
            };

            editor.SelectionStart = 1;
            editor.SelectionLength = 3;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D5));
            EnumerableAssert.AreTestEqual(new[] { FormulaTokenFactory.CreateDigitToken(1), FormulaTokenFactory.CreateDigitToken(5) }, editor.Tokens);

            editor.SelectionStart = 0;
            editor.SelectionLength = 1;
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Delete));
            EnumerableAssert.AreTestEqual(new IFormulaToken[] { FormulaTokenFactory.CreateDigitToken(5) }, editor.Tokens);
        }

        [TestMethod, TestCategory("Formulas")]
        public void MonkeyTest()
        {
            const int iterations = 1000;
            const int pressedKeys = 10;
            var random = new Random();
            var keys = EnumExtensions.AsEnumerable<FormulaEditorKey>().ToList();
            for (var iteration = 1; iteration <= iterations; iteration++)
            {
                var editor = new FormulaEditor();
                for (var i = 1; i <= pressedKeys; i++)
                {
                    editor.HandleKey(random.Next(keys));
                }
                if (editor.Formula != null)
                {
                    Assert.IsTrue(editor.Formula.AsEnumerable().All(node => node != null));
                }
            }
        }

        #region Helpers

        private void TestEditor(IFormulaToken expectedToken, FormulaEditorKey key)
        {
            var editor = new FormulaEditor();
            Assert.IsTrue(editor.HandleKey(key));
            ModelAssert.AreTestEqual(expectedToken, editor.Tokens.Single());
        }

        private void TestEditor(Func<IFormulaToken> expectedToken, FormulaEditorKey key)
        {
            TestEditor(expectedToken.Invoke(), key);
        }

        private void TestEditor(IEnumerable<IFormulaToken> expectedTokens, FormulaEditorKey key)
        {
            var editor = new FormulaEditor();
            Assert.IsTrue(editor.HandleKey(key));
            EnumerableAssert.AreTestEqual(expectedTokens, editor.Tokens);
        }

        private void TestEditor<TVariable>(Func<TVariable, IFormulaToken> expectedToken, FormulaEditorKey key) where TVariable : Variable, new()
        {
            foreach (var variable in new[] {new TVariable {Name = "TestVariable"}, new TVariable(), null})
            {
                var editor = new FormulaEditor();
                Assert.IsTrue(editor.HandleKey(key, variable as LocalVariable, variable as GlobalVariable));
                ModelAssert.AreTestEqual(expectedToken.Invoke(variable), editor.Tokens.Single());
            }
        }

        #endregion
    }
}
