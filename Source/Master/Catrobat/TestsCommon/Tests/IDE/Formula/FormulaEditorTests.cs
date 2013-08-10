using Catrobat.Core.Objects.Formulas;
using Catrobat.IDECommon.Formula.Editor;
using Catrobat.TestsCommon.Misc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Catrobat.TestsCommon.Tests.IDE.Formula
{
    [TestClass]
    public class FormulaEditorTests
    {
        [TestMethod]
        public void FormulaEditorNumberTypingTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        //[TestMethod]
        //public void FormulaEditorNumberTypingTest_02()
        //{
        //    var editor = new FormulaEditor();
        //    var selectedFromula = new SelectedFormulaInformation
        //    {
        //        FormulaRoot = new Core.Objects.Formulas.Formula()
        //    };
        //    selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
        //    {
        //        VariableType = "NUMBER",
        //        VariableValue = "123",
        //    };
        //    //selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
        //    editor.SelectedFormula = selectedFromula;
        //    bool valid = true;
        //    valid &= editor.KeyPressed(FormulaEditorKey.Number4);
        //    valid &= editor.KeyPressed(FormulaEditorKey.Number5);
        //    valid &= editor.KeyPressed(FormulaEditorKey.Number6);
        //    var expectedFormula = new FormulaTree
        //    {
        //        VariableType = "NUMBER",
        //        VariableValue = "456",
        //    };
        //    Assert.IsTrue(valid);
        //    FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        //}

        [TestMethod]
        public void FormulaEditorDeletionTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorDeletionTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number1);
            valid &= editor.KeyPressed(FormulaEditorKey.Number2);
            valid &= editor.KeyPressed(FormulaEditorKey.Number3);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            Assert.IsFalse(editor.KeyPressed(FormulaEditorKey.KeyDelete));
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "4",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorDecimalSeparatorTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorTreeTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorTreeTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorNoSelectionTest1()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorEqualsTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorDeletionTest_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "5",
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "6",
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "7",
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
        public void FormulaEditorSelectionTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorSelectionTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMinus);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "MINUS",
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
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorDecimalSeparatorTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorRelationalOperatorTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorRelationalOperatorTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorRelationalOperatorTest_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorLogicalOperatorTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorLogicalOpratorTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
        public void FormulaEditorDeletionTest_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyEquals);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicGreater);
            valid &= editor.KeyPressed(FormulaEditorKey.KeyDelete);
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(null, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorLogicalOperatorTest_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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

        [TestMethod]
        public void FormulaEditorRotationTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "PLUS",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4",
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
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
                        },
                    },
                },
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "1",
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "4",
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2",
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3",
                        },
                    },
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorRotationTest_02()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4",
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
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
                        },
                    },
                },
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.LeftChild.LeftChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.LeftChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "4",
                },
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
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
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
                },
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorRotationTest_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                  VariableType  = "NUMBER",
                  VariableValue = "1"
                },
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
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "4"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "7"
                                    },
                                }
                            }
                        }
                    } 
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild.RightChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                LeftChild = new FormulaTree
                {
                  VariableType  = "NUMBER",
                  VariableValue = "1"
                },
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
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            RightChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "7"
                            },
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
       
        }

        [TestMethod]
        public void FormulaEditorRotationTest_04()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "4"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "7"
                                    },
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild.RightChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicGreater);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
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
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorRotationTest_05()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "4"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "7"
                                    },
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild.RightChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "7"
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorRotationTest_06()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "4"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "7"
                                    },
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild.RightChild;
            selectedFromula.SelectedFormulaParent = selectedFromula.FormulaRoot.FormulaTree.RightChild.RightChild.RightChild.RightChild;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                },
                LeftChild = new FormulaTree
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
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorRotationTest_07()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                },
                LeftChild = new FormulaTree
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
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyMult);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "4"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "MULT",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "5"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "7"
                                    },
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorRotationTest_08()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                },
                LeftChild = new FormulaTree
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
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "SMALLER",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "3"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "PLUS",
                            RightChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "7"
                            },
                            LeftChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorRotationTest_09()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                },
                LeftChild = new FormulaTree
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
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicGreater);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
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
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            } 
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorRotationTest_10()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "OR",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "7"
                },
                LeftChild = new FormulaTree
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
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "2"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            var expectedFormula = new FormulaTree
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
                    VariableType = "OPERATOR",
                    VariableValue = "AND",
                    LeftChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "2"
                    },
                    RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "AND",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "7"
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "OPERATOR",
                            VariableValue = "SMALLER",
                            LeftChild = new FormulaTree
                            {
                                VariableType = "NUMBER",
                                VariableValue = "3"
                            },
                            RightChild = new FormulaTree
                            {
                                VariableType = "OPERATOR",
                                VariableValue = "PLUS",
                                LeftChild = new FormulaTree
                                {
                                    VariableType = "NUMBER",
                                    VariableValue = "4"
                                },
                                RightChild = new FormulaTree
                                {
                                    VariableType = "OPERATOR",
                                    VariableValue = "MULT",
                                    LeftChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "5"
                                    },
                                    RightChild = new FormulaTree
                                    {
                                        VariableType = "NUMBER",
                                        VariableValue = "6"
                                    }
                                }
                            }
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
        }

        [TestMethod]
        public void FormulaEditorSelectionTest_03()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123",
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = false;
            valid |= editor.KeyPressed(FormulaEditorKey.KeyMult);
            valid |= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            valid |= editor.KeyPressed(FormulaEditorKey.KeyLogicGreater);
            valid |= editor.KeyPressed(FormulaEditorKey.KeyLogicAnd);
            valid |= editor.KeyPressed(FormulaEditorKey.KeyLogicOr);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "123",
            };
            Assert.IsFalse(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
       
        }

        [TestMethod]
        public void FormulaEditorRotationTest_11()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };

            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "AND",
                LeftChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "6"
                },
                RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "GREATER",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "9"
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "7"
                        },
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "8"
                        }
                    }
                }
            };
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            var valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.KeyPlus);
            var expectedFormula = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "GREATER",
                RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = "9"
                },
                LeftChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = "PLUS",
                    RightChild = new FormulaTree
                    {
                        VariableType = "NUMBER",
                        VariableValue = "8"
                    },
                    LeftChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = "PLUS",
                        RightChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "7"
                        },
                        LeftChild = new FormulaTree
                        {
                            VariableType = "NUMBER",
                            VariableValue = "6"
                        }
                    }
                }
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);
          
        }

        [TestMethod]
        public void FormulaEditorSignedNumberTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
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
    }
}
