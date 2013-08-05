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
            bool valid = true;
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
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "9876543210",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorNumberTypingTest_02()
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
            bool valid = true;
            valid &= editor.KeyPressed(FormulaEditorKey.Number4);
            valid &= editor.KeyPressed(FormulaEditorKey.Number5);
            valid &= editor.KeyPressed(FormulaEditorKey.Number6);
            var expectedFormula = new FormulaTree
            {
                VariableType = "NUMBER",
                VariableValue = "456",
            };
            Assert.IsTrue(valid);
            FormulaComparer.CompareFormulas(expectedFormula, selectedFromula.FormulaRoot.FormulaTree);

        }

        [TestMethod]
        public void FormulaEditorDeletionTest_01()
        {
            var editor = new FormulaEditor();
            var selectedFromula = new SelectedFormulaInformation
            {
                FormulaRoot = new Core.Objects.Formulas.Formula()
            };
            selectedFromula.FormulaRoot.FormulaTree = new FormulaTree();
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            bool valid = true;
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
            selectedFromula.SelectedFormula = selectedFromula.FormulaRoot.FormulaTree;
            editor.SelectedFormula = selectedFromula;
            bool valid = true;
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
            bool valid = true;
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
            bool valid = true;
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
            bool valid = true;
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
    }
}
