using System.Collections.Generic;
using System.Linq;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDECommon.Formula.Editor
{
    public class FormulaEditor
    {
        private SelectedFormulaInformation _selectedFormulaInfo;

        public SelectedFormulaInformation SelectedFormula
        {
            get
            {
                return _selectedFormulaInfo;
            }
            set
            {
                _selectedFormulaInfo = value;
            }
        }

        private FormulaTree Selection
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

        private FormulaTree SelectionParent
        {
            get
            {
                return SelectedFormula.SelectedFormulaParent;
            }
        }

        private FormulaTree RootNode
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

        private FormulaTree EffectiveRootNode
        {
            get
            {
                var node = GetRightmostOpenBracket();
                return IsNull(node) ? RootNodeOrSelection : node.RightChild;
            }
            set
            {
                var node = GetRightmostOpenBracket();
                if (IsNull(node))
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

        private FormulaTree RootNodeOrSelection
        {
            get 
            {
                return HasSelection() ? Selection : RootNode;
            }
            set
            {
                if (HasSelection())
                {
                    
                    if (!IsNull(SelectionParent))
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

        public bool KeyPressed(FormulaEditorKey key)
        {
            if (IsNumber(key)) return HandleNumberKey(key);
            if (IsDelete(key)) return HandleDeleteKey();
            if (IsDecimalSeparator(key)) return HandleDecimalSeparatorKey();
            if (IsPlusOperator(key)) return HandlePlusOperatorKey();
            if (IsMinusOperator(key)) return HandleMinusOperatorKey();
            if (IsPreceedingOperator(key)) return HandlePreceedingOperatorKey(key);
            if (IsRelationalOperator(key)) return HandleRelationalOperatorKey(key);
            if (IsLogicAnd(key)) return HandleLogicAndKey();
            if (IsLogicOr(key)) return HandleLogicOrKey();
            if (IsLogicValue(key)) return HandleLogicValueKey(key);
            if (IsLogicNot(key)) return HandleLogicNotKey();
            if (IsOpenBracket(key)) return HandleOpenBracketKey();
            if (IsClosedBracket(key)) return HandleClosedBracketKey();
            return false;
            #region Old Code
            //if (_selectedFormulaInfo.SelectedFormula == null &&
            //    _selectedFormulaInfo.FormulaRoot.FormulaTree != null &&
            //    _selectedFormulaInfo.FormulaRoot.FormulaTree.VariableType == "NUMBER")
            //{
            //    var digitString = GetKeyPressed(key);
            //    bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
            //    bool isDelete = key == FormulaEditorKey.KeyDelete;

            //    if (digitString != null || isDecimalSeperator || isDelete)
            //    {
            //        SelectedFormula.SelectedFormula = _selectedFormulaInfo.FormulaRoot.FormulaTree;
            //    }
            //}


            //if (_selectedFormulaInfo.SelectedFormula == null &&
            //    _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild != null &&
            //    _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild.VariableType == "NUMBER")
            //{
            //    var digitString = GetKeyPressed(key);
            //    bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
            //    bool isDelete = key == FormulaEditorKey.KeyDelete;

            //    if (digitString != null || isDecimalSeperator || isDelete)
            //    {
            //        SelectedFormula.SelectedFormula = _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild;
            //        SelectedFormula.SelectedFormulaParent = _selectedFormulaInfo.FormulaRoot.FormulaTree;
            //    }
            //}


            //if (_selectedFormulaInfo.SelectedFormula != null)
            //{
            //    if (_selectedFormulaInfo.SelectedFormula.VariableType == "NUMBER")
            //    {
            //        var digitString = GetKeyPressed(key);
            //        bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
            //        bool isDelete = key == FormulaEditorKey.KeyDelete;


            //        if (digitString != null || isDecimalSeperator || isDelete)
            //        {
            //            isKeyValid = NumberSelectedAndNumberKeyPressed(_selectedFormulaInfo, digitString,
            //                isDecimalSeperator, isDelete);
            //            handled = true;
            //        }
            //    }

            //    if (!handled)
            //    {
            //        if (_selectedFormulaInfo.SelectedFormulaParent != null)
            //        {
            //            var newChild = FormulaDefaultValueCreater.GetDefaultValueForKey(key);

            //            if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
            //                _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newChild;
            //            else
            //                _selectedFormulaInfo.SelectedFormulaParent.RightChild = newChild;

            //            isKeyValid = true;
            //        }
            //        else
            //        {
            //            var newRoot = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
            //            _selectedFormulaInfo.FormulaRoot.FormulaTree = newRoot;
            //            isKeyValid = true;
            //        }
            //    }
            //}
            //else
            //{
            //    if (IsLogicFormula(key))
            //    {
            //        var newRoot = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
            //        newRoot.LeftChild = _selectedFormulaInfo.FormulaRoot.FormulaTree;
            //        //newRoot.RightChild = new FormulaTree
            //        //{
            //        //    VariableType = "#EMPTY#",
            //        //    VariableValue = ""
            //        //};
            //        _selectedFormulaInfo.FormulaRoot.FormulaTree = newRoot;
            //    }
            //    else
            //    {
            //        if (_selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild == null)
            //        {
            //            var newChild = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
            //            _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild = newChild;
            //        }
            //    }
            //}
            //return isKeyValid;
            #endregion
            
        }


        #region Key Handlers

        private bool HandleNumberKey(FormulaEditorKey key)
        {
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange))
            {
                EffectiveRootNode = DefaultNode(key);
                return true;
            }
            if (IsTerminalZero(formulaToChange))
            {
                RootNodeOrSelection = DefaultNode(key);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = DefaultNode(key);
                return true;
            }
            if (IsNumber(formulaToChange))
            {
                formulaToChange.VariableValue += GetKeyPressed(key);
                return true;
            }
            if (IsEmpty(formulaToChange))
            {
                RewriteNode(formulaToChange, key);
                return true;
            }
            return false;
        }

        private bool HandleDeleteKey()
        {
            //if (HasSelection())
            //{
            //    if (IsSignedNumber(Selection) || IsLogicNot(Selection))
            //    {
            //        if (IsNull(SelectionParent))
            //        {
            //            RootNode = Selection.RightChild;
            //        }
            //        else
            //        {
            //            SwapChildNode(SelectionParent, Selection, Selection.RightChild);
            //        }
            //        return true;
            //    }
            //    if (IsNumber(Selection))
            //    {
            //        DeleteDigit(Selection);
            //        if (IsEmpty(Selection))
            //        {
            //            Selection.VariableValue = "0";
            //        }
            //        return true;
            //    }
            //    return false;
            //}
            var rightmostClosedBracket = GetRightmostClosedBracket();
            if (!IsNull(rightmostClosedBracket))
            {
                OpenBracket(rightmostClosedBracket);
                return true;
            }
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange))
            {
                formulaToChange = RootNodeOrSelection;
                if (IsOpenBracket(formulaToChange))
                {
                    ReplaceNode(formulaToChange, DefaultNode(FormulaEditorKey.Number0));
                    return true;
                }
                return false;
            }
            if (IsEmpty(formulaToChange))
            {
                return false;
            }
            if (IsNumber(formulaToChange))
            {
                DeleteDigit(formulaToChange);
                if (IsEmpty(formulaToChange))
                {
                    Extract();
                }
                return true;
            }
            Extract();
            return true;
        }

        private bool HandleDecimalSeparatorKey()
        {
            var formulaToChange = GetRightmostNode();
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = DefaultNode(FormulaEditorKey.NumberDot);
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

        private bool HandlePlusOperatorKey()
        {
            //if (HasSelection())
            //{
            //    if (!IsOperator(Selection)) return false;
            //    if (IsSignedNumber(Selection)) return false;
            //    ReplaceNodeAndReorderTree(FormulaEditorKey.KeyPlus);
            //    return true;
            //}
            var formulaToChange = RootNode;
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                return false;
            }
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(FormulaEditorKey.KeyPlus);
            return true;
        }

        private bool HandleMinusOperatorKey()
        {

            //if (HasSelection())
            //{
            //    if (IsNumber(Selection))
            //    {
            //        return NegateSelectedNumber();
            //    }
            //    if (IsOperator(Selection))
            //    {
            //        if (IsSignedNumber(Selection)) return true;
            //        ReplaceNodeAndReorderTree(FormulaEditorKey.KeyMinus);
            //        return true;
            //    }
            //    return false;
            //}
            var rightmostNode = GetRightmostNode();
            if (IsTerminalZero(rightmostNode))
            {
                ReplaceNode(rightmostNode, DefaultNode(FormulaEditorKey.KeyMinus));
                return true;
            }
            if (IsOperator(rightmostNode))
            {
                if (IsNull(rightmostNode.LeftChild)) return false;
                Subordinate(rightmostNode,FormulaEditorKey.KeyMinus);
                return true;
            }
            var formulaToChange = RootNode;
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                RootNode = DefaultNode(FormulaEditorKey.KeyMinus);
                return true;
            }
            AddOperatorNode(FormulaEditorKey.KeyMinus);
            return true;
        }

        private bool HandlePreceedingOperatorKey(FormulaEditorKey key)
        {
            //if (HasSelection())
            //{
            //    if (!IsOperator(Selection)) return false;
            //    if (IsSignedNumber(Selection)) return false;
            //    ReplaceNodeAndReorderTree(key);
            //    return true;
            //}
            var formulaToChange = GetRightmostNode();
            if (IsNumber(formulaToChange))
            {
                AddOperatorNode(key);
                return true;
            }
            if (IsLogicValue(formulaToChange))
            {
                AddOperatorNode(key);
                return true;
            }
            return false;
        }

        private bool HandleRelationalOperatorKey(FormulaEditorKey key)
        {
            //if (HasSelection())
            //{
            //    if (!IsOperator(Selection)) return false;
            //    if (IsSignedNumber(Selection)) return false;
            //    ReplaceNodeAndReorderTree(key);
            //    return true;
            //}
            if (IsNull(RootNode) || IsEmpty(RootNode)) return false;
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(key);
            return true;
        }

        private bool HandleLogicAndKey()
        {
            //if (HasSelection())
            //{
            //    if (!IsOperator(Selection)) return false;
            //    if (IsSignedNumber(Selection)) return false;
            //    ReplaceNodeAndReorderTree(FormulaEditorKey.KeyLogicAnd);
            //    return true;
            //}
            if (IsNull(EffectiveRootNode) || IsEmpty(EffectiveRootNode)) return false;
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(FormulaEditorKey.KeyLogicAnd);
            return true;
        }

        private bool HandleLogicOrKey()
        {
            //if (HasSelection())
            //{
            //    if (!IsOperator(Selection)) return false;
            //    if (IsSignedNumber(Selection)) return false;
            //    ReplaceNodeAndReorderTree(FormulaEditorKey.KeyLogicOr);
            //    return true;
            //}
            if (IsNull(EffectiveRootNode) || IsEmpty(EffectiveRootNode)) return false;
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(FormulaEditorKey.KeyLogicOr);
            return true;
        }

        private bool HandleLogicValueKey(FormulaEditorKey key)
        {
            //if (HasSelection())
            //{
            //    if (!IsLogicValue(Selection)) return false;
            //    //ReplaceNodeAndReorderTree(key);
            //    RewriteNode(Selection, key);
            //    return true;
            //}
            var formulaToChange = GetRightmostNode();
            if (IsTerminalZero(formulaToChange))
            {
                ReplaceNode(formulaToChange, DefaultNode(key));
                return true;
            }
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                RootNode = DefaultNode(key);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                formulaToChange.RightChild = DefaultNode(key);
                return true;
            }
            return false;
        }

        private bool HandleLogicNotKey()
        {
            //if (HasSelection())
            //{
            //    if (!IsLogicValue(Selection)) return false;
            //    if (IsLogicNot(SelectionParent)) return false;
            //    NegateSelectedLogicValue();
            //    return true;
            //}
            var formulaToChange = GetRightmostNode();
            if (IsTerminalZero(formulaToChange))
            {
                ReplaceNode(formulaToChange, DefaultNode(FormulaEditorKey.KeyLogicNot));
                return true;
            }
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                RootNode = DefaultNode(FormulaEditorKey.KeyLogicNot);
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                if (IsNull(formulaToChange.LeftChild)) return false;
                Subordinate(formulaToChange, FormulaEditorKey.KeyLogicNot);
                return true;
            }
            return false;
        }

        private bool HandleOpenBracketKey()
        {
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                EffectiveRootNode = DefaultNode(FormulaEditorKey.KeyOpenBrecket);
                return true;
            }
            if (IsTerminalZero(formulaToChange))
            {
                ReplaceNode(formulaToChange, DefaultNode(FormulaEditorKey.KeyOpenBrecket));
                return true;
            }
            if (IsOperator(formulaToChange))
            {
                Subordinate(formulaToChange, FormulaEditorKey.KeyOpenBrecket);
                return true;
            }
            return false;
        }

        private bool HandleClosedBracketKey()
        {
            var node = GetRightmostOpenBracket();
            if (IsNull(node)) return false;
            CloseBracket(node);
            //node.VariableValue = "";
            return true;
        }

        #endregion


        #region Type Checks

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

        private static bool IsNumber(FormulaTree node)
        {
            return node.VariableType == "NUMBER";
        }

        private static bool IsOperator(FormulaTree node)
        {
            if (IsLogicValue(node)) return false;
            return node.VariableType == "OPERATOR";
        }

        private static bool IsDelete(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.KeyDelete);
        }

        private static bool IsDecimalSeparator(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.NumberDot);
        }

        private static bool IsPreceedingOperator(FormulaEditorKey key)
        {
            var result = false;
            result |= (key == FormulaEditorKey.KeyMult);
            result |= (key == FormulaEditorKey.KeyDivide);
            return result;
        }

        private static bool IsPreceedingOperator(FormulaTree node)
        {
            switch (node.VariableValue)
            {
                case "MULT":
                case "DEVIDE":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsSucceedingOperator(FormulaTree node)
        {
            switch (node.VariableValue)
            {
                case "PLUS":
                case "MINUS":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsEmpty(FormulaTree node)
        {
            if (IsClosedBracket(node)) return false;
            var result = false;
            result |= (node.VariableValue == "");
            result |= (node.VariableValue == null);
            return result;
        }

        private static bool IsRelationalOperator(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.KeyEquals:
                case FormulaEditorKey.KeyLogicEqual:
                case FormulaEditorKey.KeyLogicNotEqual:
                case FormulaEditorKey.KeyLogicSmaller:
                case FormulaEditorKey.KeyLogicSmallerEqual:
                case FormulaEditorKey.KeyLogicGreater:
                case FormulaEditorKey.KeyLogicGreaterEqual:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsRelationalOperator(FormulaTree node)
        {
            switch (node.VariableValue)
            {
                case "EQUAL":
                case "NOTEQUAL":
                case "SMALLER":
                case "SMALLEREQUAL":
                case "GREATER":
                case "GREATEREQUAL":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsLogicAnd(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.KeyLogicAnd);
        }

        private static bool IsLogicAnd(FormulaTree node)
        {
            return (node.VariableValue == "AND");
        }

        private static bool IsLogicOr(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.KeyLogicOr);
        }

        private static bool IsLogicOr(FormulaTree node)
        {
            return (node.VariableValue == "OR");
        }

        private static bool IsNull(FormulaTree node)
        {
            return node == null;
        }

        private static bool IsMinus(FormulaTree node)
        {
            return node.VariableValue == "MINUS";
        }

        private static bool IsSignedNumber(FormulaTree node)
        {
            return IsMinus(node) 
                && IsNull(node.LeftChild) 
                && !IsNull(node.RightChild) 
                && IsNumber(node.RightChild);
        }

        private static bool IsPlusOperator(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyPlus;
        }

        private static bool IsMinusOperator(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyMinus;
        }

        private static bool IsLogicValue(FormulaEditorKey key)
        {
            var value = false;
            value |= key == FormulaEditorKey.KeyLogicTrue;
            value |= key == FormulaEditorKey.KeyLogicFalse;
            return value;
        }

        private static bool IsLogicValue(FormulaTree node)
        {
            var value = false;
            value |= node.VariableValue == "TRUE";
            value |= node.VariableValue == "FALSE";
            return value;
        }

        private static bool IsLogicNot(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyLogicNot;
        }

        private static bool IsLogicNot(FormulaTree node)
        {
            return node.VariableValue == "NOT";
        }

        private static bool IsOpenBracket(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyOpenBrecket;
        }

        private static bool IsOpenBracket(FormulaTree node)
        {
            return node.VariableType == "BRACKET" && node.VariableValue == "OPEN";
        }

        private static bool IsClosedBracket(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyClosedBrecket;
        }

        private static bool IsClosedBracket(FormulaTree node)
        {
            return node.VariableType == "BRACKET" && node.VariableValue == "";
        }

        private bool IsTerminalZero(FormulaTree node)
        {
            if (node.VariableType != "NUMBER") return false;
            if (node.VariableValue != "0") return false;
            //if (RootNodeOrSelection != node) return false;
            if (!IsTerminalNode(node)) return false;
            return true;
        }

        private bool IsTerminalNode(FormulaTree node)
        {
            if (RootNodeOrSelection != node) return false;
            if (!IsNull(node.LeftChild)) return false;
            if (!IsNull(node.RightChild)) return false;
            return true;
        }

        #endregion


        #region Tree Navigation

        private FormulaTree GetRightmostNode()
        {
            var node = EffectiveRootNode;
            if (IsNull(node)) return null;
            while (node.RightChild != null) node = node.RightChild;
            return node;
        }

        private FormulaTree GetParentOfRightmostNode()
        {
            var node = RootNode;
            var oldNode = node;
            while (node.RightChild != null)
            {
                oldNode = node;
                node = node.RightChild;
            }
            return node == oldNode ? null : oldNode;
        }

        private FormulaTree GetRightmostSuperiorNode(FormulaEditorKey key)
        {
            var nodePriority = GetNodePriority(DefaultNode(key));
            var node = EffectiveRootNode;
            if (IsNull(node)) return null;
            if (GetNodePriority(node) <= nodePriority) return null;
            while (!IsNull(node.RightChild) && GetNodePriority(node.RightChild) > nodePriority) node = node.RightChild;
            return node;
        }

        private List<FormulaTree> DepthFirstSearchLeft(FormulaTree node)
        {
            return DepthFirstSearchLeft(RootNode, node, new List<FormulaTree>());
        }

        private static List<FormulaTree> DepthFirstSearchLeft(FormulaTree node, FormulaTree expectedNode, List<FormulaTree> path)
        {
            return DepthFirstSearch(node, expectedNode, path);
        }

        private static List<FormulaTree> DepthFirstSearch(FormulaTree node, FormulaTree expectedNode, List<FormulaTree> path)
        {
            if (node == expectedNode)
            {
                path.Add(node);
                return path;
            }
            //var firstNode = leftNodeFirst ? node.LeftChild : node.RightChild;
            //var secondNode = leftNodeFirst ? node.RightChild : node.LeftChild;
            var firstNode = node.LeftChild;
            var secondNode = node.RightChild;
            if (!IsNull(firstNode))
            {
                var subSearch = DepthFirstSearch(firstNode, expectedNode, path);
                if (subSearch != null)
                {
                    subSearch.Add(node);
                    return subSearch;
                }
            }
            if (!IsNull(secondNode))
            {
                var subSearch = DepthFirstSearch(secondNode, expectedNode, path);
                if (subSearch != null)
                {
                    subSearch.Add(node);
                    return subSearch;
                }
            }
            return null;
        }

        private static FormulaTree GetMiddleChild(FormulaTree node, FormulaTree parentNode)
        {
            if (node == parentNode.LeftChild) return node.RightChild;
            if (node == parentNode.RightChild) return node.LeftChild;
            return null;
        }

        private static FormulaTree GetChildToRotateDown(FormulaTree node)
        {
            var parentPriority = GetNodePriority(node);
            if (!IsNull(node.LeftChild))
            {
                var childPriority = GetNodePriority(node.LeftChild);
                if (childPriority > parentPriority) return node.LeftChild;
            }
            if (!IsNull(node.RightChild))
            {
                var childPriority = GetNodePriority(node.RightChild);
                if (childPriority > parentPriority) return node.RightChild;
                if (childPriority == parentPriority) return node.RightChild;
            }
            return null;
        }

        private FormulaTree GetRightmostOpenBracket()
        {
            var node = RootNode;
            FormulaTree openBracketNode = null;
            while (!IsNull(node))
            {
                if (IsOpenBracket(node))
                {
                    openBracketNode = node;
                }
                node = node.RightChild;
            }
            return openBracketNode;
        }

        private FormulaTree GetRightmostClosedBracket()
        {
            var node = RootNode;
            FormulaTree closedBracketNode = null;
            while (!IsNull(node))
            {
                if (IsClosedBracket(node))
                {
                    closedBracketNode = node;
                }
                node = node.RightChild;
            }
            return closedBracketNode;
        }

        #endregion


        #region Tree Operations

        private static void RewriteNode(FormulaTree node, FormulaEditorKey key)
        {
            var modelNode = DefaultNode(key);
            node.VariableType = modelNode.VariableType;
            node.VariableValue = modelNode.VariableValue;
        }

        private void Superordinate(FormulaEditorKey key)
        {
            var newNode = DefaultNode(key);
            var node = EffectiveRootNode;
            EffectiveRootNode = newNode;
            if (!IsNull(node) && !IsEmpty(node))
            {
                newNode.LeftChild = node;
            }
        }

        private static void Subordinate(FormulaTree node, FormulaEditorKey key)
        {
            var childNode = node.RightChild;
            node.RightChild = DefaultNode(key);
            node.RightChild.LeftChild = childNode;
        }

        private void Extract()
        {
            
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange)) return;
            if (IsTerminalNode(formulaToChange))
            {
                ReplaceNode(formulaToChange, DefaultNode(FormulaEditorKey.Number0));
                return;
            }
            var replacementNode = formulaToChange.LeftChild;
            var parentNode = GetParentOfRightmostNode();
            if (IsNull(parentNode))
            {
                RootNodeOrSelection = replacementNode;
            }
            else
            {
                parentNode.RightChild = replacementNode;
            }
        }

        private void RotateUp(FormulaTree node)
        {
            var pathToNode = DepthFirstSearchLeft(node);
            while (pathToNode.Count > 1)
            {
                var upwardNode = pathToNode.ElementAt(0);
                var downwardNode = pathToNode.ElementAt(1);
                if (!ShouldRotateUp(upwardNode, downwardNode)) return;
                var middleChild = GetMiddleChild(upwardNode, downwardNode);

                if (pathToNode.Count < 3)
                {
                    RootNode = upwardNode;
                }
                else
                {
                    var parentNode = pathToNode.ElementAt(2);
                    SwapChildNode(parentNode, downwardNode, upwardNode);
                }
                SwapChildNode(downwardNode,upwardNode,middleChild);
                SwapChildNode(upwardNode,middleChild,downwardNode);
                pathToNode.RemoveAt(1);
            }
        }

        private bool RotateDown(FormulaTree downwardNode, FormulaTree parentNode)
        {
            var hasRotated = false;
            while (true)
            {
                var upwardNode = GetChildToRotateDown(downwardNode);
                if (IsNull(upwardNode)) return hasRotated;
                var middleChild = GetMiddleChild(upwardNode, downwardNode);
                if (IsNull(parentNode))
                {
                    RootNode = upwardNode;
                }
                else
                {
                    SwapChildNode(parentNode, downwardNode, upwardNode);
                }
                SwapChildNode(downwardNode, upwardNode, middleChild);
                SwapChildNode(upwardNode, middleChild, downwardNode);
                parentNode = upwardNode;
                hasRotated = true;
            }
        }

        private static void SwapChildNode(FormulaTree node, FormulaTree oldChildNode, FormulaTree newChildNode)
        {
            if (oldChildNode == node.LeftChild) node.LeftChild = newChildNode;
            if (oldChildNode == node.RightChild) node.RightChild = newChildNode;
        }

        private void ReplaceNodeAndReorderTree(FormulaEditorKey key)
        {
            RewriteNode(Selection, key);
            if (RotateDown(Selection, SelectionParent)) return;
            RotateUp(Selection);
        }

        private void ReplaceNode(FormulaTree oldNode, FormulaTree newNode)
        {
            oldNode.VariableType = newNode.VariableType;
            oldNode.VariableValue = newNode.VariableValue;
            oldNode.LeftChild = newNode.LeftChild;
            oldNode.RightChild = newNode.RightChild;
        }

        private bool NegateSelectedNumber()
        {
            var newNode = DefaultNode(FormulaEditorKey.KeyMinus);
            if (IsNull(SelectionParent))
            {
                RootNode = newNode;
            }
            else
            {
                if (IsSignedNumber(SelectionParent)) return false;
                SwapChildNode(SelectionParent, Selection, newNode);
            }
            newNode.RightChild = Selection;
            return true;
        }
        private bool NegateSelectedLogicValue()
        {
            var newNode = DefaultNode(FormulaEditorKey.KeyLogicNot);
            if (IsNull(SelectionParent))
            {
                RootNode = newNode;
            }
            else
            {
                SwapChildNode(SelectionParent, Selection, newNode);
            }
            newNode.RightChild = Selection;
            return true;
        }

        private void AddOperatorNode(FormulaEditorKey key)
        {
            var parentNode = GetRightmostSuperiorNode(key);
            if (IsNull(parentNode))
            {
                Superordinate(key);
                return;
            }
            Subordinate(parentNode, key);
        }

        private void CloseBracket(FormulaTree node)
        {
            if (!IsOpenBracket(node)) return;
            node.VariableValue = "";
        }

        private void OpenBracket(FormulaTree node)
        {
            if (!IsClosedBracket(node)) return;
            node.VariableValue = "OPEN";
        }

        #endregion


        #region Some Short Helper Methods

        //private FormulaTree GetFormulaToChange(FormulaTree standardFormula)
        //{
        //    return HasSelection() ? Selection : standardFormula;
        //}

        private bool HasSelection()
        {
            return Selection != null;
        }

        private static FormulaTree DefaultNode(FormulaEditorKey key)
        {
            return FormulaDefaultValueCreater.GetDefaultValueForKey(key);
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

        private static int GetNodePriority(FormulaTree node)
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

        private static bool ShouldRotateUp(FormulaTree upwardNode, FormulaTree downwardNode)
        {
            var upwardNodePriority = GetNodePriority(upwardNode);
            var downwardNodePriority = GetNodePriority(downwardNode);
            if (upwardNodePriority > downwardNodePriority) return true;
            if (upwardNodePriority < downwardNodePriority) return false;
            return upwardNode != downwardNode.LeftChild;
        }

        private static void DeleteDigit(FormulaTree node)
        {
            var length = node.VariableValue.Length;
            node.VariableValue = node.VariableValue.Substring(0, length - 1);
        }

        #endregion




#region Old code II

        public bool SensorVariableSelected(SensorVariable variable)
        {
            return false;
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            return false;
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            return false;
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            return false;
        }

        //private bool NumberSelectedAndNumberKeyPressed(SelectedFormulaInformation selectedFormulaInfo, string digitString, bool isDelete, bool isDecimalSeperator)
        //{
        //    bool isValid = false;

        //    var oldVariableValue = selectedFormulaInfo.SelectedFormula.VariableValue;


        //    if (digitString != null)
        //    {
        //        if (selectedFormulaInfo.SelectedFormula.VariableValue == "0")
        //            selectedFormulaInfo.SelectedFormula.VariableValue = "";

        //        selectedFormulaInfo.SelectedFormula.VariableValue += digitString;
        //        isValid = true;
        //    }

        //    if (isDelete)
        //    {
        //        if (!selectedFormulaInfo.SelectedFormula.VariableValue.Contains("."))
        //        {
        //            selectedFormulaInfo.SelectedFormula.VariableValue += ".";
        //            isValid = true;
        //        }
        //    }

        //    if (isDecimalSeperator)
        //    {
        //        if (oldVariableValue != "")
        //        {
        //            selectedFormulaInfo.SelectedFormula.VariableValue = oldVariableValue.Substring(0, oldVariableValue.Length - 1);
        //            isValid = true;
        //        }
        //    }

        //    return isValid;
        //}

        //public bool SensorVariableSelected(SensorVariable variable)
        //{
        //    var newFormula = FormulaDefaultValueCreater.GetDefaultValueForSensorVariable(variable);

        //    if (_selectedFormulaInfo.SelectedFormulaParent != null)
        //    {
        //        if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
        //            _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
        //        else
        //            _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
        //    }
        //    else
        //    {
        //        _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
        //    }

        //    return true;
        //}

        //public bool ObjectVariableSelected(ObjectVariable variable)
        //{
        //    var newFormula = FormulaDefaultValueCreater.GetDefaultValueForObjectVariable(variable);

        //    if (_selectedFormulaInfo.SelectedFormulaParent != null)
        //    {
        //        if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
        //            _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
        //        else
        //            _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
        //    }
        //    else
        //    {
        //        _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
        //    }

        //    return true;
        //}

        //public bool GlobalVariableSelected(UserVariable variable)
        //{
        //    var newFormula = FormulaDefaultValueCreater.GetDefaultValueForGlobalVariable(variable);

        //    if (_selectedFormulaInfo.SelectedFormulaParent != null)
        //    {
        //        if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
        //            _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
        //        else
        //            _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
        //    }
        //    else
        //    {
        //        _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
        //    }

        //    return true;
        //}

        //public bool LocalVariableSelected(UserVariable variable)
        //{
        //    var newFormula = FormulaDefaultValueCreater.GetDefaultValueForLocalVariable(variable);

        //    if (_selectedFormulaInfo.SelectedFormulaParent != null)
        //    {
        //        if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
        //            _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
        //        else
        //            _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
        //    }
        //    else
        //    {
        //        _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
        //    }

        //    return true;
        //}
#endregion
    }
}
