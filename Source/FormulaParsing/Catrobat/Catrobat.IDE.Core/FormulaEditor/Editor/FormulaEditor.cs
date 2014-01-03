using System;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using System.Collections.Generic;
using System.Linq;

namespace Catrobat.IDE.Core.FormulaEditor.Editor
{
    [Obsolete("Use FormulaEditor2 instead. ")]
    public class FormulaEditor
    {

        #region members

        private Stack<XmlFormulaTree> _undoStack;
        private Stack<XmlFormulaTree> _redoStack;

        public SelectedFormulaInformation SelectedFormula { get; set; }

        public Stack<XmlFormulaTree> UndoStack
        {
            get
            {
                if (_undoStack == null)
                {
                    _undoStack = new Stack<XmlFormulaTree>();
                }
                return _undoStack;
            }
            set
            {
                _undoStack = value;
            }
        }

        public Stack<XmlFormulaTree> RedoStack
        {
            get
            {
                if (_redoStack == null)
                {
                    _redoStack = new Stack<XmlFormulaTree>();
                }
                return _redoStack;
            }
            set
            {
                _redoStack = value;
            }
        }

        private XmlFormulaTree Selection
        {
            get
            {
                return SelectedFormula.SelectedFormula;
            }
            set
            {
                SelectedFormula.SelectedFormula = value;
            }
        }

        private bool HasSelection()
        {
            return Selection != null;
        }

        private XmlFormulaTree SelectionParent
        {
            get
            {
                return SelectedFormula.SelectedFormulaParent;
            }

            set
            {
                SelectedFormula.SelectedFormulaParent = value;
            }
        }

        private XmlFormulaTree RootNode
        {
            get
            {
                return SelectedFormula.FormulaRoot.FormulaTree;
            }
            set
            {
                SelectedFormula.FormulaRoot.FormulaTree = value;
            }
        }

        private XmlFormulaTree EffectiveRootNode
        {
            get
            {
                var node = GetRightmostOpenBracket();
                return node == null ? RootNodeOrSelection : node.RightChild;
            }
            set
            {
                var node = GetRightmostOpenBracket();
                if (node == null)
                {
                    RootNodeOrSelection = value;
                }
                else
                {
                    //ReplaceNode(node, value);
                    node.RightChild = value;
                }
            }
        }

        private XmlFormulaTree RootNodeOrSelection
        {
            get
            {
                return HasSelection() ? Selection : RootNode;
            }
            set
            {
                if (HasSelection())
                {

                    if (SelectionParent != null)
                    {
                        SwapChildNode(SelectionParent, Selection, value);
                    }
                    else
                    {
                        RootNode = value;
                    }
                    Selection = value;
                }
                else
                {
                    RootNode = value;
                }
            }
        }

        #endregion

        #region key handlers

        public bool KeyPressed(FormulaEditorKey key)
        {
            if (key == FormulaEditorKey.Undo) return HandleUndoCommand();
            if (key == FormulaEditorKey.Redo) return HandleRedoCommand();

            UndoStack.Push(RootNode.Copy() as XmlFormulaTree);
            if (HandleKey(key))
            {
                RedoStack.Clear();
                return true;
            }
            UndoStack.Pop();
            return false;
        }

        public bool SensorVariableSelected(SensorVariable variable)
        {
            UndoStack.Push(RootNode.Copy() as XmlFormulaTree);
            var node = XmlFormulaTreeFactory.CreateDefaultNode(variable);
            if (HandleRightSubordinatingNode(node))
            {
                RedoStack.Clear();
                return true;
            }
            UndoStack.Pop();
            return false;
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            UndoStack.Push(RootNode.Copy() as XmlFormulaTree);
            var node = XmlFormulaTreeFactory.CreateDefaultNode(variable);
            if (HandleRightSubordinatingNode(node))
            {
                RedoStack.Clear();
                return true;
            }
            UndoStack.Pop();
            return false;
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            UndoStack.Push(RootNode.Copy() as XmlFormulaTree);
            var node = XmlFormulaTreeFactory.CreateDefaultNode(variable);
            if (HandleRightSubordinatingNode(node))
            {
                RedoStack.Clear();
                return true;
            }
            UndoStack.Pop();
            return false;
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            UndoStack.Push(RootNode.Copy() as XmlFormulaTree);
            var node = XmlFormulaTreeFactory.CreateDefaultNode(variable);
            if (HandleRightSubordinatingNode(node))
            {
                RedoStack.Clear();
                return true;
            }
            UndoStack.Pop();
            return false;
        }

        #endregion

        #region specific key handlers

        private bool HandleKey(FormulaEditorKey key)
        {
            if (IsNumber(key)) return HandleNumberKey(key);
            if (key == FormulaEditorKey.Delete) return HandleDeleteKey();
            if (key == FormulaEditorKey.NumberDot) return HandleDecimalSeparatorKey();
            if (IsGeneralOperator(key)) return HandleGeneralOperatorKey(key);
            if (IsNegatingOperator(key)) return HandleNegatingOperator(key);
            if (IsLogicValue(key)) return HandleRightSubordinatingKey(key);
            if (key == FormulaEditorKey.OpenBracket) return HandleOpenBracketKey();
            if (key == FormulaEditorKey.CloseBracket) return HandleCloseBracketKey();
            if (IsFunction(key)) return HandleRightSubordinatingKey(key);
            return false;
        }

        private bool HandleNumberKey(FormulaEditorKey key)
        {
            var formulaToChange = GetRightmostNode();
            if (formulaToChange == null)
            {
                EffectiveRootNode = XmlFormulaTreeFactory.CreateDefaultNode(key);
                return true;
            }
            if (IsNumber(formulaToChange))
            {
                if (formulaToChange.VariableValue != "0")
                {
                    formulaToChange.VariableValue += GetKeyPressed(key);
                }
                else
                {
                    formulaToChange.VariableValue = GetKeyPressed(key);
                }
                return true;
            }
            if (IsTerminalZero(formulaToChange))
            {
                RootNodeOrSelection = XmlFormulaTreeFactory.CreateDefaultNode(key);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = XmlFormulaTreeFactory.CreateDefaultNode(key);
                return true;
            }
            if (!HasChildren(formulaToChange))
            {
                ReplaceNode(formulaToChange, XmlFormulaTreeFactory.CreateDefaultNode(key));
                return true;
            }
            if (!HasChildren(formulaToChange))
            {
                ReplaceNode(formulaToChange, XmlFormulaTreeFactory.CreateDefaultNode(key));
                return true;
            }
            if (IsEmpty(formulaToChange))
            {
                RewriteNode(formulaToChange, key);
                return true;
            }
            return false;
        }

        private static string GetKeyPressed(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                    return "0";
                case FormulaEditorKey.Number1:
                    return "1";
                case FormulaEditorKey.Number2:
                    return "2";
                case FormulaEditorKey.Number3:
                    return "3";
                case FormulaEditorKey.Number4:
                    return "4";
                case FormulaEditorKey.Number5:
                    return "5";
                case FormulaEditorKey.Number6:
                    return "6";
                case FormulaEditorKey.Number7:
                    return "7";
                case FormulaEditorKey.Number8:
                    return "8";
                case FormulaEditorKey.Number9:
                    return "9";
                default:
                    return null;
            }
        }

        private bool HandleDeleteKey()
        {
            var rightmostClosedBracket = GetRightmostClosedBracket();
            if (rightmostClosedBracket != null)
            {
                OpenBracket(rightmostClosedBracket);
                return true;
            }
            var formulaToChange = GetRightmostNode();
            if (formulaToChange == null)
            {
                var rightmostOpenBracket = GetRightmostOpenBracket();
                if (rightmostOpenBracket != null)
                {
                    Extract(rightmostOpenBracket);
                    return true;
                }
            }
            if (IsTerminalZero(formulaToChange))
            {
                return false;
            }
            if (IsNumber(formulaToChange))
            {
                DeleteDigit(formulaToChange);
                if (IsEmpty(formulaToChange))
                {
                    Extract(formulaToChange);
                }
                return true;
            }
            if (IsFunction(formulaToChange))
            {
                formulaToChange.LeftChild = null;
                formulaToChange.RightChild = null;
                Extract(formulaToChange);
                return true;
            }
            Extract(formulaToChange);
            return true;
        }

        private static void DeleteDigit(XmlFormulaTree node)
        {
            var length = node.VariableValue.Length;
            node.VariableValue = node.VariableValue.Substring(0, length - 1);
        }

        private bool HandleDecimalSeparatorKey()
        {
            var formulaToChange = GetRightmostNode();
            if (formulaToChange == null)
            {
                var rightmostOpenBracket = GetRightmostOpenBracket();
                if (rightmostOpenBracket == null) return false;
                Subordinate(rightmostOpenBracket, FormulaEditorKey.NumberDot);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.NumberDot);
                return true;
            }
            if (IsEmpty(formulaToChange))
            {
                RewriteNode(formulaToChange, FormulaEditorKey.NumberDot);
                return true;
            }
            if (!IsNumber(formulaToChange)) return false;
            if (formulaToChange.VariableValue.Contains("."))
            {
                return false;
            }
            formulaToChange.VariableValue += ".";
            return true;
        }

        private bool HandleOpenBracketKey()
        {
            var formulaToChange = GetRightmostNode();
            if (formulaToChange == null || IsEmpty(formulaToChange))
            {
                EffectiveRootNode = XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.OpenBracket);
                return true;
            }
            if (IsTerminalZero(formulaToChange))
            {
                ReplaceNode(formulaToChange, XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.OpenBracket));
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                Subordinate(formulaToChange, FormulaEditorKey.OpenBracket);
                return true;
            }
            return false;
        }

        private bool HandleCloseBracketKey()
        {
            var node = GetRightmostOpenBracket();
            if (node == null || node.RightChild == null) return false;
            CloseBracket(node);
            return true;
        }

        private bool HandleRightSubordinatingKey(FormulaEditorKey key)
        {
            return HandleRightSubordinatingNode(XmlFormulaTreeFactory.CreateDefaultNode(key));
        }

        private bool HandleRightSubordinatingNode(XmlFormulaTree node)
        {
            var formulaToChange = GetRightmostNode();
            if (formulaToChange == null || IsEmpty(formulaToChange))
            {
                EffectiveRootNode = node;
                return true;
            }
            if (IsTerminalZero(formulaToChange))
            {
                ReplaceNode(formulaToChange, node);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = node;
                return true;
            }
            if (!HasChildren(node))
            {
                ReplaceNode(formulaToChange, node);
                return true;
            }
            return false;
        }

        private bool HandleGeneralOperatorKey(FormulaEditorKey key)
        {
            if (RootNode == null || IsEmpty(RootNode)) return false;
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(key);
            return true;
        }

        private bool HandleNegatingOperator(FormulaEditorKey key)
        {
            var rightmostNode = GetRightmostNode();
            if (rightmostNode == null)
            {
                EffectiveRootNode = XmlFormulaTreeFactory.CreateDefaultNode(key);
                return true;
            }
            if (IsTerminalZero(rightmostNode))
            {
                ReplaceNode(rightmostNode, XmlFormulaTreeFactory.CreateDefaultNode(key));
                return true;
            }
            if (IsOperator(rightmostNode))
            {
                if (rightmostNode.LeftChild == null) return false;
                Subordinate(rightmostNode, key);
                return true;
            }
            var formulaToChange = RootNode;
            if (formulaToChange == null || IsEmpty(formulaToChange))
            {
                RootNode = XmlFormulaTreeFactory.CreateDefaultNode(key);
                return true;
            }
            if (key == FormulaEditorKey.LogicNot) return false;
            AddOperatorNode(key);
            return true;
        }

        private bool HandleUndoCommand()
        {
            if (UndoStack.Count == 0) return false;
            RedoStack.Push(RootNode.Copy() as XmlFormulaTree);
            RootNode = UndoStack.Pop();
            Selection = null;
            SelectionParent = null;
            return true;
        }

        private bool HandleRedoCommand()
        {
            if (RedoStack.Count == 0) return false;
            UndoStack.Push(RootNode);
            RootNode = RedoStack.Pop();
            Selection = null;
            SelectionParent = null;
            return true;
        }

        #endregion

        #region node checks

        private static bool IsNumber(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.Number0:
                case FormulaEditorKey.Number1:
                case FormulaEditorKey.Number2:
                case FormulaEditorKey.Number3:
                case FormulaEditorKey.Number4:
                case FormulaEditorKey.Number5:
                case FormulaEditorKey.Number6:
                case FormulaEditorKey.Number7:
                case FormulaEditorKey.Number8:
                case FormulaEditorKey.Number9:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsNumber(XmlFormulaTree node)
        {
            return node.VariableType == "NUMBER";
        }

        private static bool IsOperator(XmlFormulaTree node)
        {
            return !IsLogicValue(node) && node.VariableType == "OPERATOR";
        }

        private static bool IsEmpty(XmlFormulaTree node)
        {
            return !IsCloseBracket(node) && string.IsNullOrEmpty(node.VariableValue);
        }

        private static bool IsPreceedingOperator(XmlFormulaTree node)
        {
            var operatorName = node.VariableValue;
            return 
                operatorName == "MULT" || 
                operatorName == "DIVIDE";
        }

        private static bool IsSucceedingOperator(XmlFormulaTree node)
        {
            var operatorName = node.VariableValue;
            return 
                operatorName == "PLUS" || 
                operatorName == "MINUS";
        }

        private static bool IsRelationalOperator(XmlFormulaTree node)
        {
            var operatorName = node.VariableValue;
            return
                operatorName == "EQUAL" ||
                operatorName == "NOTEQUAL" ||
                operatorName == "SMALLER_THAN" ||
                operatorName == "SMALLER_OR_EQUAL" ||
                operatorName == "GREATER_THAN" ||
                operatorName == "GREATER_OR_EQUAL";
        }

        private static bool IsLogicAnd(XmlFormulaTree node)
        {
            return (node.VariableValue == "LOGICAL_AND");
        }

        private static bool IsLogicOr(XmlFormulaTree node)
        {
            return (node.VariableValue == "LOGICAL_OR");
        }

        private static bool IsMinusOperator(XmlFormulaTree node)
        {
            return node.VariableValue == "MINUS";
        }

        private static bool IsSignedNumber(XmlFormulaTree node)
        {
            return IsMinusOperator(node)
                && node.LeftChild == null
                && node.RightChild != null
                && IsNumber(node.RightChild);
        }

        private static bool IsLogicValue(FormulaEditorKey key)
        {
            return 
                key == FormulaEditorKey.LogicTrue || 
                key == FormulaEditorKey.LogicFalse;
        }

        private static bool IsLogicValue(XmlFormulaTree node)
        {
            var value = node.VariableValue;
            return
                value == "TRUE" ||
                value == "FALSE";
        }

        private static bool IsOpenBracket(XmlFormulaTree node)
        {
            return node.VariableType == "BRACKET" && node.VariableValue == "OPEN";
        }

        private static bool IsCloseBracket(XmlFormulaTree node)
        {
            return node.VariableType == "BRACKET" && node.VariableValue == "";
        }

        private static bool HasChildren(XmlFormulaTree node)
        {
            return node.LeftChild != null || node.RightChild != null;
        }

        private bool IsTerminalZero(XmlFormulaTree node)
        {
            return node.VariableType == "NUMBER" && node.VariableValue == "0" && IsTerminalNode(node);
        }

        private bool IsTerminalNode(XmlFormulaTree node)
        {
            return RootNodeOrSelection == node && !HasChildren(node);
        }

        private static bool IsFunction(XmlFormulaTree node)
        {
            return node.VariableType == "FUNCTION";
        }

        private static bool IsFunction(FormulaEditorKey key)
        {
            return IsUnaryFunction(key) || IsBinaryFunction(key) || IsLiteralFunction(key);
        }

        private static bool IsUnaryFunction(FormulaEditorKey key)
        {
            return
                key == FormulaEditorKey.MathSin ||
                key == FormulaEditorKey.MathCos ||
                key == FormulaEditorKey.MathTan ||
                key == FormulaEditorKey.MathLn ||
                key == FormulaEditorKey.MathLog ||
                key == FormulaEditorKey.MathSqrt ||
                key == FormulaEditorKey.MathAbs ||
                key == FormulaEditorKey.MathRound ||
                key == FormulaEditorKey.MathArcSin ||
                key == FormulaEditorKey.MathArcCos ||
                key == FormulaEditorKey.MathArcTan ||
                key == FormulaEditorKey.MathExp;
        }

        private static bool IsBinaryFunction(FormulaEditorKey key)
        {
            return
                key == FormulaEditorKey.MathRandom ||
                key == FormulaEditorKey.MathMod ||
                key == FormulaEditorKey.MathMin ||
                key == FormulaEditorKey.MathMax;
        }

        private static bool IsLiteralFunction(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.MathPi;
        }

        private static bool IsGeneralOperator(FormulaEditorKey key)
        {
            return 
                key == FormulaEditorKey.Plus ||
                key == FormulaEditorKey.Multiply ||
                key == FormulaEditorKey.Divide ||
                key == FormulaEditorKey.NumberEquals ||
                key == FormulaEditorKey.LogicEqual ||
                key == FormulaEditorKey.LogicNotEqual ||
                key == FormulaEditorKey.LogicSmaller ||
                key == FormulaEditorKey.LogicSmallerEqual ||
                key == FormulaEditorKey.LogicGreater ||
                key == FormulaEditorKey.LogicGreaterEqual ||
                key == FormulaEditorKey.LogicAnd ||
                key == FormulaEditorKey.LogicOr;
        }

        private static bool IsNegatingOperator(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.Minus || key == FormulaEditorKey.LogicNot;
        }

        #endregion

        #region tree navigation

        private XmlFormulaTree GetRightmostNode()
        {
            var node = EffectiveRootNode;
            if (node == null) return null;
            while ((node.RightChild != null) && (!IsFunction(node)))
            {
                node = node.RightChild;
            }
            return node;
        }

        private XmlFormulaTree GetRightmostSuperiorNode(FormulaEditorKey key)
        {
            var nodePriority = GetNodePriority(XmlFormulaTreeFactory.CreateDefaultNode(key));
            var node = EffectiveRootNode;
            if (node == null) return null;
            if (GetNodePriority(node) <= nodePriority) return null;
            while (node.RightChild != null && GetNodePriority(node.RightChild) > nodePriority)
            {
                node = node.RightChild;
            }
            return node;
        }

        private static List<XmlFormulaTree> DepthFirstSearch(XmlFormulaTree node, XmlFormulaTree expectedNode, List<XmlFormulaTree> path)
        {
            if (node == expectedNode)
            {
                path.Add(node);
                return path;
            }
            var leftChild = node.LeftChild;
            if (leftChild != null)
            {
                var subSearch = DepthFirstSearch(leftChild, expectedNode, path);
                if (subSearch != null)
                {
                    subSearch.Add(node);
                    return subSearch;
                }
            }
            var rightChild = node.RightChild;
            if (rightChild != null)
            {
                var subSearch = DepthFirstSearch(rightChild, expectedNode, path);
                if (subSearch != null)
                {
                    subSearch.Add(node);
                    return subSearch;
                }
            }
            return null;
        }

        private List<XmlFormulaTree> DepthFirstSearch(XmlFormulaTree node)
        {
            return DepthFirstSearch(RootNode, node, new List<XmlFormulaTree>());
        }

        private XmlFormulaTree GetRightmostOpenBracket()
        {
            var node = RootNodeOrSelection;
            XmlFormulaTree openBracketNode = null;
            while (node != null)
            {
                if (IsOpenBracket(node))
                {
                    openBracketNode = node;
                }
                node = node.RightChild;
            }
            return openBracketNode;
        }

        private XmlFormulaTree GetRightmostClosedBracket()
        {
            var node = RootNodeOrSelection;
            while (node != null)
            {
                if (IsCloseBracket(node))
                {
                    return node;
                }
                node = node.RightChild;
            }
            return null;
        }

        #endregion

        #region tree manipulation

        private static void RewriteNode(XmlFormulaTree node, FormulaEditorKey key)
        {
            var modelNode = XmlFormulaTreeFactory.CreateDefaultNode(key);
            node.VariableType = modelNode.VariableType;
            node.VariableValue = modelNode.VariableValue;
        }

        private void Superordinate(FormulaEditorKey key)
        {
            var newNode = XmlFormulaTreeFactory.CreateDefaultNode(key);
            var node = EffectiveRootNode;
            EffectiveRootNode = newNode;
            if (node != null && !IsEmpty(node))
            {
                newNode.LeftChild = node;
            }
        }

        private static void Subordinate(XmlFormulaTree node, FormulaEditorKey key)
        {
            var childNode = node.RightChild;
            node.RightChild = XmlFormulaTreeFactory.CreateDefaultNode(key);
            node.RightChild.LeftChild = childNode;
        }

        private void Extract(XmlFormulaTree node)
        {
            if (IsTerminalNode(node))
            {
                ReplaceNode(node, XmlFormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.Number0));
                return;
            }
            var replacementNode = node.LeftChild;
            var path = DepthFirstSearch(node);
            XmlFormulaTree parentNode = null;
            if (path.Count > 1)
            {
                parentNode = path.ElementAt(1);
            }
            if (parentNode == null)
            {
                RootNodeOrSelection = replacementNode;
            }
            else
            {
                parentNode.RightChild = replacementNode;
            }
        }

        //private void RotateUp(FormulaTree node)
        //{
        //    var pathToNode = DepthFirstSearchLeft(node);
        //    while (pathToNode.Count > 1)
        //    {
        //        var upwardNode = pathToNode.ElementAt(0);
        //        var downwardNode = pathToNode.ElementAt(1);
        //        if (!ShouldRotateUp(upwardNode, downwardNode)) return;
        //        var middleChild = GetMiddleChild(upwardNode, downwardNode);

        //        if (pathToNode.Count < 3)
        //        {
        //            RootNode = upwardNode;
        //        }
        //        else
        //        {
        //            var parentNode = pathToNode.ElementAt(2);
        //            SwapChildNode(parentNode, downwardNode, upwardNode);
        //        }
        //        SwapChildNode(downwardNode, upwardNode, middleChild);
        //        SwapChildNode(upwardNode, middleChild, downwardNode);
        //        pathToNode.RemoveAt(1);
        //    }
        //}

        //private bool RotateDown(FormulaTree downwardNode, FormulaTree parentNode)
        //{
        //    var hasRotated = false;
        //    while (true)
        //    {
        //        var upwardNode = GetChildToRotateDown(downwardNode);
        //        if (IsNull(upwardNode)) return hasRotated;
        //        var middleChild = GetMiddleChild(upwardNode, downwardNode);
        //        if (IsNull(parentNode))
        //        {
        //            RootNode = upwardNode;
        //        }
        //        else
        //        {
        //            SwapChildNode(parentNode, downwardNode, upwardNode);
        //        }
        //        SwapChildNode(downwardNode, upwardNode, middleChild);
        //        SwapChildNode(upwardNode, middleChild, downwardNode);
        //        parentNode = upwardNode;
        //        hasRotated = true;
        //    }
        //}

        private static void SwapChildNode(XmlFormulaTree node, XmlFormulaTree oldChildNode, XmlFormulaTree newChildNode)
        {
            if (oldChildNode == node.LeftChild) node.LeftChild = newChildNode;
            if (oldChildNode == node.RightChild) node.RightChild = newChildNode;
        }

        //private void ReplaceNodeAndReorderTree(FormulaEditorKey key)
        //{
        //    RewriteNode(Selection, key);
        //    if (RotateDown(Selection, SelectionParent)) return;
        //    RotateUp(Selection);
        //}

        private static void ReplaceNode(XmlFormulaTree oldNode, XmlFormulaTree newNode)
        {
            oldNode.VariableType = newNode.VariableType;
            oldNode.VariableValue = newNode.VariableValue;
            oldNode.LeftChild = newNode.LeftChild;
            oldNode.RightChild = newNode.RightChild;
        }

        //private bool NegateSelectedNumber()
        //{
        //    var newNode = FormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.KeyMinus);
        //    if (IsNull(SelectionParent))
        //    {
        //        RootNode = newNode;
        //    }
        //    else
        //    {
        //        if (IsSignedNumber(SelectionParent)) return false;
        //        SwapChildNode(SelectionParent, Selection, newNode);
        //    }
        //    newNode.RightChild = Selection;
        //    return true;
        //}
        //private bool NegateSelectedLogicValue()
        //{
        //    var newNode = FormulaTreeFactory.CreateDefaultNode(FormulaEditorKey.KeyLogicNot);
        //    if (IsNull(SelectionParent))
        //    {
        //        RootNode = newNode;
        //    }
        //    else
        //    {
        //        SwapChildNode(SelectionParent, Selection, newNode);
        //    }
        //    newNode.RightChild = Selection;
        //    return true;
        //}

        private void AddOperatorNode(FormulaEditorKey key)
        {
            var parentNode = GetRightmostSuperiorNode(key);
            if (parentNode == null)
            {
                Superordinate(key);
                return;
            }
            Subordinate(parentNode, key);
        }

        private static void CloseBracket(XmlFormulaTree node)
        {
            if (!IsOpenBracket(node)) return;
            node.VariableValue = "";
        }

        private static void OpenBracket(XmlFormulaTree node)
        {
            if (!IsCloseBracket(node)) return;
            node.VariableValue = "OPEN";
        }

        #endregion

        private static int GetNodePriority(XmlFormulaTree node)
        {
            if (IsSignedNumber(node)) return 0;
            if (IsNumber(node)) return 0;
            if (IsPreceedingOperator(node)) return 1;
            if (IsSucceedingOperator(node)) return 2;
            if (IsRelationalOperator(node)) return 3;
            if (IsLogicAnd(node)) return 4;
            if (IsLogicOr(node)) return 5;
            return -1;
        }

    }
}
