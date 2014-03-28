using System;
using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaTree;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEditorTests
    {

        [TestMethod]
        public void TestCommonKeys()
        {
            TestKey(FormulaEditorKey.D0, FormulaTreeFactory.CreateNumberNode(0));
            TestKey(FormulaEditorKey.D1, FormulaTreeFactory.CreateNumberNode(1));
            TestKey(FormulaEditorKey.D2, FormulaTreeFactory.CreateNumberNode(2));
            TestKey(FormulaEditorKey.D3, FormulaTreeFactory.CreateNumberNode(3));
            TestKey(FormulaEditorKey.D4, FormulaTreeFactory.CreateNumberNode(4));
            TestKey(FormulaEditorKey.D5, FormulaTreeFactory.CreateNumberNode(5));
            TestKey(FormulaEditorKey.D6, FormulaTreeFactory.CreateNumberNode(6));
            TestKey(FormulaEditorKey.D7, FormulaTreeFactory.CreateNumberNode(7));
            TestKey(FormulaEditorKey.D8, FormulaTreeFactory.CreateNumberNode(8));
            TestKey(FormulaEditorKey.D9, FormulaTreeFactory.CreateNumberNode(9));
            TestKey(
                keys: new[] { FormulaEditorKey.D0, FormulaEditorKey.DecimalSeparator, FormulaEditorKey.D1 },
                expectedFormula: FormulaTreeFactory.CreateNumberNode(0.1));
            TestKey(FormulaEditorKey.Equals, FormulaTreeFactory.CreateEqualsNode);
            TestKey(FormulaEditorKey.Plus, FormulaTreeFactory.CreateAddNode);
            TestKey(FormulaEditorKey.Minus, FormulaTreeFactory.CreateSubtractNode);
            TestKey(FormulaEditorKey.Multiply, FormulaTreeFactory.CreateMultiplyNode);
            TestKey(FormulaEditorKey.Divide, FormulaTreeFactory.CreateDivideNode);
            TestKey(FormulaEditorKey.Equals, FormulaTreeFactory.CreateEqualsNode);
            TestKey(FormulaEditorKey.NotEquals, FormulaTreeFactory.CreateNotEqualsNode);
            TestKey(FormulaEditorKey.Less, FormulaTreeFactory.CreateLessNode);
            TestKey(FormulaEditorKey.LessEqual, FormulaTreeFactory.CreateLessEqualNode);
            TestKey(FormulaEditorKey.Greater, FormulaTreeFactory.CreateGreaterNode);
            TestKey(FormulaEditorKey.GreaterEqual, FormulaTreeFactory.CreateGreaterEqualNode);
            TestKey(FormulaEditorKey.And, FormulaTreeFactory.CreateAndNode);
            TestKey(FormulaEditorKey.Or, FormulaTreeFactory.CreateOrNode);
            TestKey(FormulaEditorKey.True, FormulaTreeFactory.CreateTrueNode);
            TestKey(FormulaEditorKey.False, FormulaTreeFactory.CreateFalseNode);
            TestKey(
                keys: new[] { FormulaEditorKey.Not, FormulaEditorKey.True },
                expectedFormula: FormulaTreeFactory.CreateNotNode(FormulaTreeFactory.CreateTrueNode()));
            TestKey(FormulaEditorKey.Sin, FormulaTreeFactory.CreateSinNode);
            TestKey(FormulaEditorKey.Cos, FormulaTreeFactory.CreateCosNode);
            TestKey(FormulaEditorKey.Tan, FormulaTreeFactory.CreateTanNode);
            TestKey(FormulaEditorKey.Arcsin, FormulaTreeFactory.CreateArcsinNode);
            TestKey(FormulaEditorKey.Arccos, FormulaTreeFactory.CreateArccosNode);
            TestKey(FormulaEditorKey.Arctan, FormulaTreeFactory.CreateArctanNode);
            TestKey(FormulaEditorKey.Exp, FormulaTreeFactory.CreateExpNode);
            TestKey(FormulaEditorKey.Ln, FormulaTreeFactory.CreateLnNode);
            TestKey(FormulaEditorKey.Log, FormulaTreeFactory.CreateLogNode);
            // TODO
            TestKey(FormulaEditorKey.Pi, FormulaTreeFactory.CreatePiNode);
            // TODO
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestUndoRedo()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void TestBrackets()
        {
            Assert.Inconclusive();
        }

        private void TestKey(FormulaEditorKey key, IFormulaTree expectedFormula)
        {
            var editor = new FormulaEditor3();
            Assert.IsTrue(editor.HandleKey(key, null, null));
            Assert.AreEqual(expectedFormula, editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree> expectedFormula)
        {
            var editor = new FormulaEditor3();
            Assert.IsTrue(editor.HandleKey(key, null, null));
            Assert.AreEqual(expectedFormula(), editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree, FormulaNodeUnaryFunction> expectedFormula)
        {
            var editor = new FormulaEditor3();
            Assert.IsTrue(editor.HandleKey(key, null, null));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D1, null, null));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.ClosingParenthesis, null, null));
            Assert.AreEqual(expectedFormula(FormulaTreeFactory.CreateNumberNode(1)), editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree, IFormulaTree, FormulaNodeInfixOperator> expectedFormula)
        {
            var editor = new FormulaEditor3();
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D1, null, null));
            Assert.IsTrue(editor.HandleKey(key, null, null));
            Assert.IsTrue(editor.HandleKey(FormulaEditorKey.D2, null, null));
            Assert.AreEqual(expectedFormula(FormulaTreeFactory.CreateNumberNode(1), FormulaTreeFactory.CreateNumberNode(2)), editor.Formula);
        }

        private void TestKey(IEnumerable<FormulaEditorKey> keys, IFormulaTree expectedFormula)
        {
            var editor = new FormulaEditor3();
            foreach (var key in keys)
            {
                Assert.IsTrue(editor.HandleKey(key, null, null));
            }
            Assert.AreEqual(expectedFormula, editor.Formula);
        }
    }
}
