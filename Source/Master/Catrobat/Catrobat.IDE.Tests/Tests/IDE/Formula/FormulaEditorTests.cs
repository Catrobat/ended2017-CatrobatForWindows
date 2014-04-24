using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.ExtensionMethods;
using Catrobat.IDE.Core.Formulas.Editor;
using Catrobat.IDE.Core.Models.Formulas.FormulaToken;
using Catrobat.IDE.Core.Models.Formulas.FormulaTree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Tests.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEditorTests
    {
        [TestInitialize]
        public void TestClassInitialize()
        {
            ServiceLocator.Register<CultureServiceTest>(TypeCreationMode.Lazy);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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


        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestFunctions()
        {
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateExpToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Exp);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateLogToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Log);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateLnToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Ln);
            TestEditor(new IFormulaToken[] {  FormulaTokenFactory.CreateMinToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Min);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateMaxToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Max);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateSinToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Sin);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateCosToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Cos);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateTanToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Tan);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArcsinToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Arcsin);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArccosToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Arccos);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateArctanToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Arctan);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateSqrtToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Sqrt);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateAbsToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Abs);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateRoundToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Round);
            TestEditor(new IFormulaToken[] { FormulaTokenFactory.CreateRandomToken(), FormulaTokenFactory.CreateParenthesisToken(true) }, FormulaEditorKey.Random);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestVariables()
        {
            TestEditor(FormulaTokenFactory.CreateLocalVariableToken, FormulaEditorKey.LocalVariable);
            TestEditor(FormulaTokenFactory.CreateGlobalVariableToken, FormulaEditorKey.GlobalVariable);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestBrackets()
        {
            TestEditor(FormulaTokenFactory.CreateParenthesisToken(true), FormulaEditorKey.OpeningParenthesis);
            TestEditor(FormulaTokenFactory.CreateParenthesisToken(false), FormulaEditorKey.ClosingParenthesis);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestDelete()
        {
            var editor = new FormulaEditor();
            Assert.IsFalse(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Exp));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsFalse(editor.HandleKey(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.Exp));
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestUndoRedo()
        {
            var editor = new FormulaEditor();
            Assert.IsFalse(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.D2);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.Left);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);
            editor.SelectionLength = 1;

            editor.HandleKey(FormulaEditorKey.D4);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.Undo);
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(2) }, editor.Tokens);
            Assert.AreEqual(0, editor.CaretIndex);
            Assert.AreEqual(1, editor.SelectionLength);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.Undo);
            Assert.IsNull(editor.Tokens);
            Assert.AreEqual(0, editor.CaretIndex);
            Assert.AreEqual(0, editor.SelectionLength);
            Assert.IsFalse(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.Redo);
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(2) }, editor.Tokens);
            Assert.AreEqual(0, editor.CaretIndex);
            Assert.AreEqual(1, editor.SelectionLength);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsTrue(editor.CanRedo);

            editor.HandleKey(FormulaEditorKey.Redo);
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            Assert.AreEqual(1, editor.CaretIndex);
            Assert.AreEqual(0, editor.SelectionLength);
            Assert.IsTrue(editor.CanUndo);
            Assert.IsFalse(editor.CanRedo);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestBindings()
        {
            var editor = new FormulaEditor
            {
                Formula = FormulaTreeFactory.CreateNumberNode(4)
            };
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            editor.HandleKey(FormulaEditorKey.D2);
            Assert.AreEqual(FormulaTreeFactory.CreateNumberNode(42), editor.Formula);
            Assert.IsNull(editor.ParsingError);
            editor.HandleKey(FormulaEditorKey.Plus);
            Assert.AreEqual(FormulaTreeFactory.CreateNumberNode(42), editor.Formula);
            Assert.IsNotNull(editor.ParsingError);
            editor.HandleKey(FormulaEditorKey.D1);
            Assert.AreEqual(FormulaTreeFactory.CreateAddNode(FormulaTreeFactory.CreateNumberNode(42), FormulaTreeFactory.CreateNumberNode(1)), editor.Formula);
            Assert.IsNull(editor.ParsingError);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
        public void TestSelection()
        {
            var editor = new FormulaEditor();
            editor.HandleKey(FormulaEditorKey.D4);
            editor.CaretIndex = 0;
            editor.SelectionLength = 1;
            editor.HandleKey(FormulaEditorKey.D2);
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(2) }, editor.Tokens);
            Assert.AreEqual(0, editor.SelectionLength);
            editor.HandleKey(FormulaEditorKey.D4);
            EnumerableAssert.AreEqual(new[] { FormulaTokenFactory.CreateDigitToken(2), FormulaTokenFactory.CreateDigitToken(4) }, editor.Tokens);
            Assert.AreEqual(0, editor.SelectionLength);
        }

        [TestMethod, TestCategory("Catrobat.IDE.Core.FormulaEditor")]
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
                    var randomKey = keys[random.Next(0, keys.Count)];
                    editor.HandleKey(randomKey);
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
            Assert.AreEqual(expectedToken, editor.Tokens.Single());
        }

        private void TestEditor(Func<IFormulaToken> expectedToken, FormulaEditorKey key)
        {
            TestEditor(expectedToken.Invoke(), key);
        }

        private void TestEditor(IEnumerable<IFormulaToken> expectedTokens, FormulaEditorKey key)
        {
            var editor = new FormulaEditor();
            Assert.IsTrue(editor.HandleKey(key));
            EnumerableAssert.AreEqual(expectedTokens, editor.Tokens);
        }

        private void TestEditor(Func<UserVariable, IFormulaToken> expectedToken, FormulaEditorKey key)
        {
            foreach (var variable in new[] {new UserVariable {Name = "TestVariable"}, new UserVariable(), null})
            {
                var editor = new FormulaEditor();
                Assert.IsTrue(editor.HandleKey(key, variable));
                Assert.AreEqual(expectedToken.Invoke(variable), editor.Tokens.Single());
            }
        }

        #endregion
    }
}
