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

        [TestMethod,TestCategory("GatedTests")]
        public void FormulaEditorTests_NumberTypingTest_01()
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

        [TestMethod]
        public void FormulaEditorTests_TypingTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyDivide));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicSmallerEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicGreater));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicGreaterEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));

            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyDivide));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicSmallerEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicGreater));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicGreaterEqual));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyMinus));

            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number9));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyPlus));

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

        [TestMethod, TestCategory("GuardedTests")]
        public void FormulaEditorTests_DecimalSeparatorTest_01()
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

        [TestMethod]
        public void FormulaEditorTests_DecimalSeparatorTest_02()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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

        [TestMethod, TestCategory("GuardedTests")]
        public void FormulaEditorTests_Tree_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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

            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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

        [TestMethod, TestCategory("GuardedTests")]
        public void FormulaEditorTests_Tree_02()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
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

        [TestMethod]
        public void FormulaEditorTests_Tree_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
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

        [TestMethod]
        public void FormulaEditorTests_Tree_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
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

        [TestMethod]
        public void FormulaEditorTests_EqualsTest_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
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

        [TestMethod]
        public void FormulaEditorTests_RelationalOperator_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicSmallerEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicGreater);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicGreaterEqual);
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

        [TestMethod]
        public void FormulaEditorTests_RelationalOperator_02()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDivide);
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

        [TestMethod]
        public void FormulaEditorTests_RelationalOperator_03()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.NumberDot);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
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

        [TestMethod]
        public void FormulaEditorTests_LogicalOperator_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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

        [TestMethod]
        public void FormulaEditorTests_LogicalOprator_02()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
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

        [TestMethod]
        public void FormulaEditorTests_LogicalOperator_03()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_02()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicNotEqual);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_03()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_05()
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
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_06()
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
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
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

        [TestMethod]
        public void FormulaEditorTests_SignedNumber_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
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

        [TestMethod]
        public void FormulaEditorTests_LogicValue_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
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

        [TestMethod]
        public void FormulaEditorTests_LogicValue_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.Number9));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.NumberDot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);


        }

        [TestMethod]
        public void FormulaEditorTests_LogicValue_03()
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
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            editor.SelectedFormula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
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

        [TestMethod, TestCategory("GuardedTests")]
        public void FormulaEditorTests_Deletion_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "1",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod, TestCategory("GuardedTests")]
        public void FormulaEditorTests_Deletion_02()
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
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "4",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Deletion_03()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.Number0);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid &= editor.KeyPressed(FormulaEditorKey.Number7);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
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
                valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            }
            expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "2"
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorTests_Deletion_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number0));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyEquals));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicGreater));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Deletion_05()
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
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorTests_Deletion_06()
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
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
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

        [TestMethod]
        public void FormulaEditorTests_LogicNot_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
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

        [TestMethod]
        public void FormulaEditorTests_LogicNot_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
        }

        [TestMethod]
        public void FormulaEditorTests_LogicNot_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
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

        [TestMethod]
        public void FormulaEditorTests_LogicNot_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {

                VariableType = "NUMBER",
                VariableValue = "0"

            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
        }

        #endregion

        #region function

        [TestMethod]
        public void FormulaEditorTests_Function_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathAbs));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "ABS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathArcCos));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCCOS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathArcSin));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCSIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathArcTan));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "ARCTAN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathCos));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "COS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathExp));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "EXP",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathLn));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "LN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathLog));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "LOG",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathMax));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathMin));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathMod));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MOD",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathPi));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "PI",
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathRandom));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "RANDOM",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathRound));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "ROUND",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathSin));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathSqrt));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "SQRT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathTan));
            expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "TAN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
        }

        [TestMethod]
        public void FormulaEditorTests_Function_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathCos));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree()
                    {
                        VariableType = "FUNCTION",
                        VariableValue = "COS",
                        LeftChild = new FormulaTree()
                        {
                            VariableType = "NUMBER",
                            VariableValue = "0"
                        }
                    },
                    RightChild = new FormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Function_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathSin));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMathCos));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "BRACKET",
                    VariableValue = "",
                    RightChild = new FormulaTree()
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree()
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "SIN",
                            LeftChild = new FormulaTree()
                            {
                                VariableType = "NUMBER",
                                VariableValue = "0"
                            }
                        },
                        RightChild = new FormulaTree()
                        {
                            VariableType = "FUNCTION",
                            VariableValue = "COS",
                            LeftChild = new FormulaTree()
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
        public void FormulaEditorTests_Function_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MAX",
                LeftChild = new FormulaTree()
                {
                    VariableType = "FUNCTION",
                    VariableValue = "SIN",
                    LeftChild = new FormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "FUNCTION",
                    VariableValue = "COS",
                    LeftChild = new FormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "0"
                    }
                }
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        #endregion

        #region bracket

        [TestMethod]
        public void FormulaEditorTests_Bracket_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number5));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
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

        [TestMethod]
        public void FormulaEditorTests_Bracket_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
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

        [TestMethod]
        public void FormulaEditorTests_Bracket_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number7));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number6));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number5));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number4));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicSmaller));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicAnd));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicOr));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));

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

        [TestMethod]
        public void FormulaEditorTests_Bracket_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "123"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Bracket_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Bracket_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.NumberDot));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0."
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorTests_Bracket_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {

                VariableType = "NUMBER",
                VariableValue = "0"

            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
        }

        [TestMethod]
        public void FormulaEditorTests_Bracket_08()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            //Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN",
                RightChild = new FormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MULT",
                    LeftChild = new FormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "1"
                    },
                    RightChild = new FormulaTree()
                    {
                        VariableType = "BRACKET",
                        VariableValue = "",
                        RightChild = new FormulaTree()
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree()
                            {
                                VariableType = "NUMBER",
                                VariableValue = "2"
                            },
                            RightChild = new FormulaTree()
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
        public void FormulaEditorTests_Bracket_09()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyClosedBrecket));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "BRACKET",
                VariableValue = "",
                RightChild = new FormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "MINUS",
                    RightChild = new FormulaTree()
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
        public void FormulaEditorTests_Variable_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.SensorVariableSelected(SensorVariable.AccelerationX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "SENSOR",
                    VariableValue = "ACCELERATION_X"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Variable_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.ObjectVariableSelected(ObjectVariable.PositionX));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "SENSOR",
                    VariableValue = "OBJECT_X"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Variable_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            var variable = new UserVariable()
            {
                Name = "MyVar"
            };
            Assert.IsTrue(editor.GlobalVariableSelected(variable));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Variable_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            var variable = new UserVariable()
            {
                Name = "MyVar"
            };
            Assert.IsTrue(editor.LocalVariableSelected(variable));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMult));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MULT",
                LeftChild = new FormulaTree()
                {
                    VariableType = "USER_VARIABLE",
                    VariableValue = "MyVar"
                },
                RightChild = new FormulaTree()
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
        public void FormulaEditorTests_Selection_NoSelection()
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
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "42",
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "31",
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "11",
                    }
                }
            };

            editor.SelectedFormula = selectedFromula;
            editor.KeyPressed(FormulaEditorKey.KeyDelete);
            editor.KeyPressed(FormulaEditorKey.KeyDelete);
            editor.KeyPressed(FormulaEditorKey.KeyDelete);

            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "42",
                },
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "31"
                }
            };

            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Selection_01()
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
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
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

        [TestMethod]
        public void FormulaEditorTests_Selection_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "1"
                }
            };
            editor.SelectedFormula = selectedFromula;
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "MIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
                RightChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                }
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Selection_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "NUMBER",
                    VariableValue = "0"
                },
            };
            editor.SelectedFormula = selectedFromula;
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "FUNCTION",
                VariableValue = "SIN",
                LeftChild = new FormulaTree()
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    LeftChild = new FormulaTree()
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree()
                        {
                            VariableType = "NUMBER",
                            VariableValue = "1"
                        },
                        RightChild = new FormulaTree()
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        }
                    },
                    RightChild = new FormulaTree()
                    {
                        VariableType = "NUMBER",
                        VariableValue = "3"
                    }
                },
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorTests_Selection_Plus()
        {
            Assert.Inconclusive("Write test");
        }

        [TestMethod]
        public void FormulaEditorTests_Selection_More()
        {
            Assert.Inconclusive("Write more tests like Selection_Plus");
        }

        #endregion

        #region terminal zero

        [TestMethod]
        public void FormulaEditorTests_TerminalZero_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };
            var terminalZero = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            selectedFromula.FormulaRoot.FormulaTree = terminalZero;
            editor.SelectedFormula = selectedFromula;
            var expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree()
            {
                VariableType = "BRACKET",
                VariableValue = "OPEN"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyOpenBrecket));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyMinus));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "NOT"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicNot));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "TRUE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicTrue));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
            expectedFormula = new FormulaTree()
            {
                VariableType = "OPERATOR",
                VariableValue = "FALSE"
            };
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyLogicFalse));
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            FormulaComparer.CompareFormulas(terminalZero, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region node with empty childs

        [TestMethod]
        public void FormulaEditorTests_EmptyChilds()
        {
            var inputFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0.0",
                LeftChild = null,
                RightChild = null
            };
            const SensorVariable variable = SensorVariable.CompassDirection;
            var expectedFormula = FormulaDefaultValueCreater.GetDefaultValueForSensorVariable(variable);

            var editor = new FormulaEditor
            {
                SelectedFormula = new SelectedFormulaInformation
                {
                    FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
                    {
                        FormulaTree = inputFormula
                    }
                }
            };
            Assert.IsTrue(editor.SensorVariableSelected(variable));
            FormulaComparer.CompareFormulas(expectedFormula, editor.SelectedFormula.FormulaRoot.FormulaTree);
        }

        #endregion

        #region undo

        [TestMethod]
        public void FormulaEditorTests_Undo_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number2));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyRedo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyRedo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyRedo));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyRedo));
            expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "123"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number3));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyRedo));
        }


        [TestMethod]
        public void FormulaEditorTests_Undo_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.CatrobatObjects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "0"
            };
            editor.SelectedFormula = selectedFromula;
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.Number1));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyPlus));
            Assert.IsTrue(editor.KeyPressed(FormulaEditorKey.KeyUndo));
            var expectedFormula = new FormulaTree()
            {
                VariableType = "NUMBER",
                VariableValue = "1"
            };
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        #endregion

    }
}
