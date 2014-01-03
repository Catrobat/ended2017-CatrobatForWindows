using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;
using Catrobat.IDE.Tests.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.IDE.Tests.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEditorTests
    {

        #region typing

        [TestMethod, TestCategory("GatedTests")]
        public void NumberTypingTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "98765432109",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void TypingTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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

            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void DecimalSeparatorTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.NumberDot));
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1.234",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void DecimalSeparatorTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void Tree_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Plus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1",
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "23"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

            valid &= editor.KeyPressed(FormulaEditorKey.Multiply);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1",
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "23",
                    },
                    RightChild = new FormulaTree
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
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = subTree,
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5",
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Tree_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0.1"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2.3"
                    }
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4.58"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Tree_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var nodeA = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var nodeB = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = nodeA,
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                },
                RightChild = nodeA
            };
            var nodeC = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = nodeB,
                RightChild = nodeB
            };
            var nodeD = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                LeftChild = nodeC,
                RightChild = nodeC
            };
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = nodeD,
                RightChild = nodeD
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Tree_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3",
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                    LeftChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void EqualsTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    }
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        }
                    },
                    RightChild = new FormulaTree
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
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = oldTree,
                RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void RelationalOperator_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "GREATEREQUAL",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "8"
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "GREATER",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "7"
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLEREQUAL",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "6"
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            RightChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "5"
                            },
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "NOTEQUAL",
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "EQUAL",
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "3"
                                    },
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "OPERATOR",
                                        VariableValue = "EQUAL",
                                        RightChild = new FormulaTree
                                        {
                                            VariableType = "NUMBER",
                                            VariableValue = "2"
                                        },
                                        LeftChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void RelationalOperator_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOTEQUAL",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "DEVIDE",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1",
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1",
                        },
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void RelationalOperator_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOTEQUAL",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0.4",
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2.3",
                    },
                    LeftChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void LogicalOperator_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3",
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "4",
                        },
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "5",
                        },
                        RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void LogicalOprator_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1",
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2",
                    },
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3",
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void LogicalOperator_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "6",
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "5",
                    },
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOTEQUAL",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "4",
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3",
                        },
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOTEQUAL",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2",
                        },
                        LeftChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                    },
                    RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "5",
                        }
                    },
                    RightChild = new FormulaTree
                      {
                          VariableType = "OPERATOR",
                          VariableValue = "MINUS",
                          RightChild = new FormulaTree
                          {
                              VariableType = "NUMBER",
                              VariableValue = "6",
                          }
                      },
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOTEQUAL",
                    RightChild = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4",
                }
            },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3",
                    },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MINUS",
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MINUS",
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "2",
                                }
                            },
                            LeftChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "5",
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    },
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "EQUAL",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "MINUS",
                        RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "3",
                                },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2",
                        },
                    },
                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "1",
                                    },
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            var expectedFormula = selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
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
            var expectedFormula = selectedFromula.FormulaRoot.FormulaTree = new FormulaTree

            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void SignedNumber_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "FALSE"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "TRUE"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region logical value

        [TestMethod, TestCategory("GatedTests")]
        public void LogicValue_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "TRUE"
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "FALSE"
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "TRUE"
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MINUS",
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "FALSE"
                            },
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "TRUE"
                                },
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "OPERATOR",
                                        VariableValue = "FALSE"
                                    },
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "OPERATOR",
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

        [TestMethod, TestCategory("GatedTests")]
        public void LogicValue_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void LogicValue_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "FALSE"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "TRUE"
                }
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "TRUE"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region deletion

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            valid &= editor.KeyPressed(FormulaEditorKey.Delete);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "4",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "200"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "30"
                        },
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "1",
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "2",
                                },
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "3",
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4",
                                },
                            }
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "MULT",
                            RightChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "7",
                            },
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5",
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "6",
                                },
                            }
                        }
                    },
                    RightChild = new FormulaTree
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
            expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "2"
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123"
            };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Deletion_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
                                {
                                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                                };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "1"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "2"
                                    }
                                };
            editor.SelectedFormula = selectedFromula;
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region logic not

        [TestMethod, TestCategory("GatedTests")]
        public void LogicNot_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "NOT",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "FALSE"
                    }
                },
                RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOT",
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "TRUE"
                        }
                    }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void LogicNot_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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

        [TestMethod, TestCategory("GatedTests")]
        public void LogicNot_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number0));
            var expectedFormula = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "OR",
                    LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "FALSE"
                            },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "NOT",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    }
                };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void LogicNot_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void Function_DefaultValues()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathAbs));
            var expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ABS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcCos));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCCOS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcSin));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCSIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathArcTan));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCTAN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathCos));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "COS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathExp));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "EXP",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathLn));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathLog));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "LOG",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMax));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMin));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathMod));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MOD",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathPi));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "PI",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathRandom));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "RANDOM",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathRound));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "ROUND",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathSin));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathSqrt));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SQRT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.MathTan));
            expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "TAN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Function_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "COS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    },
                    RightChild = new FormulaTree
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
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Function_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "SIN",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            }
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "COS",
                            LeftChild = new FormulaTree
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
 
        [TestMethod, TestCategory("GatedTests")]
        public void Function_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = new FormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "SIN",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                },
                RightChild = new FormulaTree
                {
                    VariableType = "FUNCTION",
                    VariableValue = "COS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                }
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region bracket

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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

            var node1 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var node2 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                LeftChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node1
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3"
                }
            };
            var node3 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER",
                LeftChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node2
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4"
                }
            };
            var node4 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node3
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5"
                }
            };
            var node5 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node4
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "6"
                }
            };
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node5
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        }
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
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

            var node1 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "2"
                }
            };
            var node2 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node1
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "3"
                }
            };
            var node3 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "SMALLER",
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node2
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4"
                }
            };
            var node4 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node3
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "5"
                }
            };
            var node5 = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node4
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "6"
                }
            };
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                RightChild = new FormulaTree
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = node5
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "123"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new FormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0."
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {

                VariableType = "NUMBER",
                VariableValue = "0"

            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.CloseBracket));
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_08()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "BRACKET",
                        VariableValue = "",
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "2"
                            },
                            RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void Bracket_09()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.CloseBracket));
            var expectedFormula = new FormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree
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

        [TestMethod, TestCategory("GatedTests")]
        public void Variable_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.SensorVariableSelected(SensorVariable.AccelerationX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "SENSOR",
                    VariableValue = "ACCELERATION_X"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Variable_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.ObjectVariableSelected(ObjectVariable.PositionX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Multiply));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "SENSOR",
                    VariableValue = "OBJECT_X"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Variable_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Variable_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region selection

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_NoSelection()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = FormulaTreeFactory.CreateNumber(42),
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = FormulaTreeFactory.CreateNumber(31),
                    RightChild = FormulaTreeFactory.CreateNumber(11)
                }
            };

            editor.SelectedFormula = selectedFromula;
            editor.KeyPressed(FormulaEditorKey.Delete);
            editor.KeyPressed(FormulaEditorKey.Delete);
            editor.KeyPressed(FormulaEditorKey.Delete);

            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = FormulaTreeFactory.CreateNumber(42),
                RightChild = FormulaTreeFactory.CreateNumber(31)
            };

            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "123"
                },
                RightChild = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "178"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "456"
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        }
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_Number()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = FormulaTreeFactory.CreateNumber(0),
                            RightChild = FormulaTreeFactory.CreateNumber(0)
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

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_Operator()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = FormulaTreeFactory.CreateNumber(0),
                            RightChild = FormulaTreeFactory.CreateNumber(0)
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

        [TestMethod, TestCategory("GatedTests")]
        public void Selection_Sensor()
        {
            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0",
                            LeftChild = FormulaTreeFactory.CreateNumber(0),
                            RightChild = FormulaTreeFactory.CreateNumber(0)
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

        [TestMethod, TestCategory("GatedTests")]
        public void TerminalZero_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            var terminalZero = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            selectedFromula.FormulaRoot.FormulaTree = terminalZero;
            editor.SelectedFormula = selectedFromula;
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.OpenBracket));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Minus));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "NOT"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicNot));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "TRUE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicTrue));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "FALSE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.LogicFalse));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Delete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region node with empty childs

        [TestMethod, TestCategory("GatedTests")]
        public void EmptyChilds()
        {

            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula
                    {
                        FormulaTree = FormulaTreeFactory.CreateNumber("0.0")
                    }
                }
            };

            // type a sensor variable (should replace 0.0)
            const SensorVariable variable = SensorVariable.CompassDirection;
            Assert.IsTrue(editor.SensorVariableSelected(variable));
            FormulaComparer.CompareFormulas(
                expectedFormula: FormulaTreeFactory.CreateDefaultNode(variable), 
                actualFormula: editor.SelectedFormula.FormulaRoot.FormulaTree);


            // type 0 (should replace the sensor variable)
            const FormulaEditorKey numberKey = FormulaEditorKey.Number0;
            Assert.IsTrue(editor.KeyPressed(numberKey));
            FormulaComparer.CompareFormulas(
                expectedFormula: FormulaTreeFactory.CreateDefaultNode(numberKey),
                actualFormula: editor.SelectedFormula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region undo

        [TestMethod, TestCategory("GatedTests")]
        public void Undo_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
            var expectedFormula = new FormulaTree
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
            expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Redo));
        }


        [TestMethod, TestCategory("GatedTests")]
        public void Undo_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Plus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Undo));
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

    }
}
