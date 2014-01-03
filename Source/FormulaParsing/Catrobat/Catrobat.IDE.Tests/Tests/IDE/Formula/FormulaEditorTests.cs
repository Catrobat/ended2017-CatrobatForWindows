using System;
using System.Collections.Generic;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Formulas.FormulaNodes;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEditorTests
    {

        [TestMethod]
        public void TestCommonKeys()
        {
            TestKey(FormulaEditorKey.Number0, FormulaTreeFactory.CreateNumberNode(0));
            TestKey(FormulaEditorKey.Number1, FormulaTreeFactory.CreateNumberNode(1));
            TestKey(FormulaEditorKey.Number2, FormulaTreeFactory.CreateNumberNode(2));
            TestKey(FormulaEditorKey.Number3, FormulaTreeFactory.CreateNumberNode(3));
            TestKey(FormulaEditorKey.Number4, FormulaTreeFactory.CreateNumberNode(4));
            TestKey(FormulaEditorKey.Number5, FormulaTreeFactory.CreateNumberNode(5));
            TestKey(FormulaEditorKey.Number6, FormulaTreeFactory.CreateNumberNode(6));
            TestKey(FormulaEditorKey.Number7, FormulaTreeFactory.CreateNumberNode(7));
            TestKey(FormulaEditorKey.Number8, FormulaTreeFactory.CreateNumberNode(8));
            TestKey(FormulaEditorKey.Number9, FormulaTreeFactory.CreateNumberNode(9));
            TestKey(
                keys: new[] { FormulaEditorKey.Number0, FormulaEditorKey.NumberDot, FormulaEditorKey.Number1 },
                expectedFormula: FormulaTreeFactory.CreateNumberNode(0.1));
            TestKey(FormulaEditorKey.NumberEquals, FormulaTreeFactory.CreateEqualsNode);
            TestKey(FormulaEditorKey.Plus, FormulaTreeFactory.CreateAddNode);
            TestKey(FormulaEditorKey.Minus, FormulaTreeFactory.CreateSubtractNode);
            TestKey(FormulaEditorKey.Multiply, FormulaTreeFactory.CreateMultiplyNode);
            TestKey(FormulaEditorKey.Divide, FormulaTreeFactory.CreateDivideNode);
            TestKey(FormulaEditorKey.LogicEqual, FormulaTreeFactory.CreateEqualsNode);
            TestKey(FormulaEditorKey.LogicNotEqual, FormulaTreeFactory.CreateNotEqualsNode);
            TestKey(FormulaEditorKey.LogicSmaller, FormulaTreeFactory.CreateLessNode);
            TestKey(FormulaEditorKey.LogicSmallerEqual, FormulaTreeFactory.CreateLessEqualNode);
            TestKey(FormulaEditorKey.LogicGreater, FormulaTreeFactory.CreateGreaterNode);
            TestKey(FormulaEditorKey.LogicGreaterEqual, FormulaTreeFactory.CreateGreaterEqualNode);
            TestKey(FormulaEditorKey.LogicAnd, FormulaTreeFactory.CreateAndNode);
            TestKey(FormulaEditorKey.LogicOr, FormulaTreeFactory.CreateOrNode);
            TestKey(FormulaEditorKey.LogicTrue, FormulaTreeFactory.CreateTrueNode);
            TestKey(FormulaEditorKey.LogicFalse, FormulaTreeFactory.CreateFalseNode);
            TestKey(
                keys: new[] { FormulaEditorKey.LogicNot, FormulaEditorKey.LogicTrue },
                expectedFormula: FormulaTreeFactory.CreateNotNode(FormulaTreeFactory.CreateTrueNode()));
            TestKey(FormulaEditorKey.MathSin, FormulaTreeFactory.CreateSinNode);
            TestKey(FormulaEditorKey.MathCos, FormulaTreeFactory.CreateCosNode);
            TestKey(FormulaEditorKey.MathTan, FormulaTreeFactory.CreateTanNode);
            TestKey(FormulaEditorKey.MathArcSin, FormulaTreeFactory.CreateArcsinNode);
            TestKey(FormulaEditorKey.MathArcCos, FormulaTreeFactory.CreateArccosNode);
            TestKey(FormulaEditorKey.MathArcTan, FormulaTreeFactory.CreateArctanNode);
            TestKey(FormulaEditorKey.MathExp, FormulaTreeFactory.CreateExpNode);
            TestKey(FormulaEditorKey.MathLn, FormulaTreeFactory.CreateLnNode);
            TestKey(FormulaEditorKey.MathLog, FormulaTreeFactory.CreateLogNode);
            // TODO
            TestKey(FormulaEditorKey.MathPi, FormulaTreeFactory.CreatePiNode);
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
            var editor = new FormulaEditor2();
            Assert.IsTrue(editor.KeyPressed(key));
            Assert.AreEqual(expectedFormula, editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree> expectedFormula)
        {
            var editor = new FormulaEditor2();
            Assert.IsTrue(editor.KeyPressed(key));
            Assert.AreEqual(expectedFormula(), editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree, FormulaNodeUnaryFunction> expectedFormula)
        {
            var editor = new FormulaEditor2();
            Assert.IsTrue(editor.KeyPressed(key));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.AreEqual(expectedFormula(FormulaTreeFactory.CreateNumberNode(1)), editor.Formula);
        }

        private void TestKey(FormulaEditorKey key, Func<IFormulaTree, IFormulaTree, FormulaNodeInfixOperator> expectedFormula)
        {
            var editor = new FormulaEditor2();
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(key));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.AreEqual(expectedFormula(FormulaTreeFactory.CreateNumberNode(1), FormulaTreeFactory.CreateNumberNode(2)), editor.Formula);
        }

        private void TestKey(IEnumerable<FormulaEditorKey> keys, IFormulaTree expectedFormula)
        {
            var editor = new FormulaEditor2();
            foreach (var key in keys)
            {
                Assert.IsTrue(editor.KeyPressed(key));
            }
            Assert.AreEqual(expectedFormula, editor.Formula);
        }

        #region typing

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void NumberTypingTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number9);
            valid &= editor.KeyPressed(FormulaEditorKey.Number8);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Number0);
            valid &= editor.KeyPressed(FormulaEditorKey.Number9);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "98765432109",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void TypingTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Divide));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicNotEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicSmallerEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicGreater));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicGreaterEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));

            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Divide));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicNotEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicSmallerEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicGreater));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicGreaterEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Minus));

            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));

            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "9"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region decimal separator

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void DecimalSeparatorTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.NumberDot));
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1.234",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void DecimalSeparatorTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0.5"
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region tree

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Tree_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1",
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "23"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1",
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "23",
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

            var subTree = expectedFormula;
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = subTree,
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5",
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Tree_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Number8);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0.1"
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2.3"
                    }
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4.58"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Tree_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            var nodeA = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var nodeB = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = nodeA,
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                },
                RightChild = nodeA
            };
            var nodeC = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = nodeB,
                RightChild = nodeB
            };
            var nodeD = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = nodeC,
                RightChild = nodeC
            };
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = nodeD,
                RightChild = nodeD
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Tree_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3",
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region equals

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void EqualsTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    }
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        }
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            var oldTree = expectedFormula;
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = oldTree,
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region relational operator

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void RelationalOperator_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicSmaller);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicSmallerEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicGreater);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicGreaterEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number8);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "GREATER_OR_EQUAL",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "8"
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "GREATER_THAN",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "7"
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER_OR_EQUAL",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "6"
                        },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER_THAN",
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "5"
                            },
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "NOTEQUAL",
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                LeftChild = new XmlFormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "EQUAL",
                                    RightChild = new XmlFormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "3"
                                    },
                                    LeftChild = new XmlFormulaTree
                                    {
                                        VariableType = "OPERATOR",
                                        VariableValue = "EQUAL",
                                        RightChild = new XmlFormulaTree
                                        {
                                            VariableType = "NUMBER",
                                            VariableValue = "2"
                                        },
                                        LeftChild = new XmlFormulaTree
                                        {
                                            VariableType = "NUMBER",
                                            VariableValue = "1"
                                        },
                                    }
                                }
                            }
                        }
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void RelationalOperator_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Divide);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOTEQUAL",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "DIVIDE",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1",
                        },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1",
                        },
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void RelationalOperator_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOTEQUAL",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0.4",
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2.3",
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region logical operator

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicalOperator_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_AND",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3",
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "4",
                        },
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "5",
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "6",
                        },
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicalOprator_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_AND",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_AND",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3",
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicalOperator_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "6",
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "5",
                    },
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_AND",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOTEQUAL",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "4",
                        },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3",
                        },
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOTEQUAL",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2",
                        },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1",
                        },
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region signed number

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            var expectedFormula = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                    },
                    RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                    }
                }
                };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "5",
                        }
                    },
                    RightChild = new XmlFormulaTree
                      {
                          VariableType = "OPERATOR",
                          VariableValue = "MINUS",
                          RightChild = new XmlFormulaTree
                          {
                              VariableType = "NUMBER",
                              VariableValue = "6",
                          }
                      },
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4",
                }
            },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3",
                    },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MINUS",
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MINUS",
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "2",
                                }
                            },
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "1",
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.LogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Minus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "5",
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    },
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "EQUAL",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "3",
                                },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2",
                        },
                    },
                    LeftChild = new XmlFormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "1",
                                    },
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            var expectedFormula = selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    }
                }
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree

            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void SignedNumber_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "FALSE"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "TRUE"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region logical value

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicValue_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "TRUE"
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_AND",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "FALSE"
                    },
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER_THAN",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "TRUE"
                        },
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MINUS",
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "FUNCTION",
                                VariableValue = "FALSE"
                            },
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "FUNCTION",
                                    VariableValue = "TRUE"
                                },
                                LeftChild = new XmlFormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    RightChild = new XmlFormulaTree
                                    {
                                        VariableType = "FUNCTION",
                                        VariableValue = "FALSE"
                                    },
                                    LeftChild = new XmlFormulaTree
                                    {
                                        VariableType = "FUNCTION",
                                        VariableValue = "TRUE"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicValue_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicValue_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "FALSE"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "TRUE"
                }
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "TRUE"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region deletion

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "4",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number0);
            valid &= editor.KeyPressed(FormulaEditorKey.Number0);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Number0);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number8);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "200"
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "30"
                        },
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "1",
                                },
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "2",
                                },
                            },
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "3",
                                },
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4",
                                },
                            }
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MULT",
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "7",
                            },
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5",
                                },
                                RightChild = new XmlFormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "6",
                                },
                            }
                        }
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "8",
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            for (int i = 0; i < 23; i++)
            {
                valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            }
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "2"
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number0));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicGreater));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123"
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Deletion_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
                                {
                                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                                };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new XmlFormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "1"
                                    },
                                    RightChild = new XmlFormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "2"
                                    }
                                };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region logic not

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicNot_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_NOT",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "FALSE"
                    }
                },
                RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "LOGICAL_NOT",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "TRUE"
                        }
                    }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicNot_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicOr));
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicNot_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number0));
            var expectedFormula = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "LOGICAL_OR",
                    LeftChild = new XmlFormulaTree
                            {
                                VariableType = "FUNCTION",
                                VariableValue = "FALSE"
                            },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "LOGICAL_NOT",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    }
                };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void LogicNot_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {

                VariableType = "NUMBER",
                VariableValue = "0"

            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.LogicNot));
        }

        #endregion

        #region function

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Function_DefaultValues()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathAbs));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ABS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcCos));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCCOS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcSin));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCSIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcTan));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCTAN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathCos));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "COS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathExp));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "EXP",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathLn));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathLog));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LOG",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMax));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMin));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMod));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MOD",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathPi));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "PI",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathRandom));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "RAND",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathRound));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ROUND",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathSin));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathSqrt));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SQRT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathTan));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "TAN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Function_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathCos));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "COS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Function_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathSin));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathCos));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "SIN",
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            }
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "COS",
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            }
                        }
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }
 
        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Function_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "SIN",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "COS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                }
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region bracket

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number5));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number7));

            var node1 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var node2 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node1
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3"
                }
            };
            var node3 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER_THAN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node2
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4"
                }
            };
            var node4 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node3
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5"
                }
            };
            var node5 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node4
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "6"
                }
            };
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node5
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "LOGICAL_AND",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        }
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number7));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number5));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));

            var node1 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_OR",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var node2 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_AND",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node1
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3"
                }
            };
            var node3 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER_THAN",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node2
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4"
                }
            };
            var node4 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node3
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5"
                }
            };
            var node5 = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node4
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "6"
                }
            };
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node5
                },
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "123"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0."
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {

                VariableType = "NUMBER",
                VariableValue = "0"

            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.CloseBracket));
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_08()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            //Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "BRACKET",
                        VariableValue = "",
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "2"
                            },
                            RightChild = new XmlFormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            }
                        }
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Bracket_09()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    }
                }

            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region variable

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Variable_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.SensorVariableSelected(SensorVariable.AccelerationX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "SENSOR",
                    VariableValue = "ACCELERATION_X"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Variable_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.ObjectVariableSelected(ObjectVariable.PositionX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "SENSOR",
                    VariableValue = "OBJECT_X"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Variable_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            var variable = new UserVariable
            {
                Name = "MyVar"
            };
            Assert.IsTrue(editor.GlobalVariableSelected(variable));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Variable_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            var variable = new UserVariable
            {
                Name = "MyVar"
            };
            Assert.IsTrue(editor.LocalVariableSelected(variable));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region selection

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_NoSelection()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = XmlFormulaTreeFactory.CreateNumber(42),
                RightChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = XmlFormulaTreeFactory.CreateNumber(31),
                    RightChild = XmlFormulaTreeFactory.CreateNumber(11)
                }
            };

            editor.SelectedFormula = selectedFromula;
            editor.KeyPressed(FormulaEditorKey.Delete);
            editor.KeyPressed(FormulaEditorKey.Delete);
            editor.KeyPressed(FormulaEditorKey.Delete);

            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = XmlFormulaTreeFactory.CreateNumber(42),
                RightChild = XmlFormulaTreeFactory.CreateNumber(31)
            };

            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "123"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "456"
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.Number8);
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "178"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "456"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            editor.SelectedFormula = selectedFromula;
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            editor.SelectedFormula = selectedFromula;
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new XmlFormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new XmlFormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                        RightChild = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        }
                    },
                    RightChild = new XmlFormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_Number()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = XmlFormulaTreeFactory.CreateNumber(0),
                            RightChild = XmlFormulaTreeFactory.CreateNumber(0)
                        }
                    }
                }
            };

            var selections = new[]
            {
                editor.SelectedFormula.FormulaRoot.FormulaTree.LeftChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree.RightChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree
            };
            foreach (var selection in selections)
            {
                editor.SelectedFormula.SelectedFormula = selection;
                selection.LeftChild = null;
                selection.RightChild = null;


                // type numbers
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
                Assert.AreSame(selection, editor.SelectedFormula.SelectedFormula);
            }
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_Operator()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = XmlFormulaTreeFactory.CreateNumber(0),
                            RightChild = XmlFormulaTreeFactory.CreateNumber(0)
                        }
                    }
                }
            };

            var selections = new[]
            {
                editor.SelectedFormula.FormulaRoot.FormulaTree.LeftChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree.RightChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree
            };
            foreach (var selection in selections)
        {
                editor.SelectedFormula.SelectedFormula = selection;
                selection.LeftChild = null;
                selection.RightChild = null;

                // type numbers
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.AreSame(selection, editor.SelectedFormula.SelectedFormula);
            }
        }

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Selection_Sensor()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new XmlFormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = XmlFormulaTreeFactory.CreateNumber(0),
                            RightChild = XmlFormulaTreeFactory.CreateNumber(0)
                        }
                    }
                }
            };

            var selections = new[]
            {
                editor.SelectedFormula.FormulaRoot.FormulaTree.LeftChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree.RightChild, 
                editor.SelectedFormula.FormulaRoot.FormulaTree
            };
            foreach (var selection in selections)
        {
                editor.SelectedFormula.SelectedFormula = selection;
                selection.LeftChild = null;
                selection.RightChild = null;

                // type numbers
                Assert.IsTrue(editor.SensorVariableSelected(SensorVariable.CompassDirection));
                Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
                Assert.AreSame(selection, editor.SelectedFormula.SelectedFormula);
            }
        }

        #endregion

        #region terminal zero

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void TerminalZero_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            var terminalZero = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            selectedFromula.FormulaRoot.FormulaTree = terminalZero;
            editor.SelectedFormula = selectedFromula;
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "LOGICAL_NOT"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "TRUE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "FALSE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region node with empty childs

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void EmptyChilds()
        {

            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = XmlFormulaTreeFactory.CreateNumber("0.0")
                    }
                }
            };

            // type a sensor variable (should replace 0.0)
            const SensorVariable variable = SensorVariable.CompassDirection;
            Assert.IsTrue(editor.SensorVariableSelected(variable));
            FormulaComparer.CompareFormulas(
                expectedFormula: XmlFormulaTreeFactory.CreateDefaultNode(variable), 
                actualFormula: editor.SelectedFormula.FormulaRoot.FormulaTree);


            // type 0 (should replace the sensor variable)
            const FormulaEditorKey numberKey = FormulaEditorKey.Number0;
            Assert.IsTrue(editor.KeyPressed(numberKey));
            FormulaComparer.CompareFormulas(
                expectedFormula: XmlFormulaTreeFactory.CreateDefaultNode(numberKey),
                actualFormula: editor.SelectedFormula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region undo

        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Undo_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Undo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Redo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Redo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Redo));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Redo));
            expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Redo));
        }


        [TestMethod]
        [Obsolete("Rewrite for FormulaEditor2")]
        public void Undo_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            var expectedFormula = new XmlFormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

    }
}
