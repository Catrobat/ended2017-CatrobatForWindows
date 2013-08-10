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

        public bool KeyPressed(FormulaEditorKey key)
        {
            if (IsNumber(key)) return HandleNumberKey(key);
            if (IsDelete(key)) return HandleDeleteKey();
            if (IsDecimalSeparator(key)) return HandleDecimalSeparatorKey();
            if (IsSucceedingOperator(key)) return HandleSucceedingOperatorKey(key);
            if (IsPreceedingOperator(key)) return HandlePreceedingOperatorKey(key);
            if (IsRelationalOperator(key)) return HandleRelationalOperatorKey(key);
            if (IsLogicAnd(key)) return HandleLogicAndKey();
            if (IsLogicOr(key)) return HandleLogicOrKey();
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
            var formulaToChange = GetFormulaToChange(GetRightmostNode());
            if (IsNull(formulaToChange))
            {
                RootNode = DefaultNode(key);
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

            var formulaToChange = GetFormulaToChange(GetRightmostNode());
            if (IsNull(formulaToChange))
            {
                return false;
            }
            if (IsEmpty(formulaToChange))
            {
                return false;
            }
            if (IsNumber(formulaToChange))
            {
                var length = formulaToChange.VariableValue.Length;
                formulaToChange.VariableValue = formulaToChange.VariableValue.Substring(0, length - 1);
                if (IsEmpty(formulaToChange))
                {
                    Extract();
                }
                return true;
            }
            if (IsArithmeticOperator(formulaToChange))
            {
                Extract();
                return true;
            }
            if (IsLogicOperator(formulaToChange))
            {
                Extract();
                return true;
            }
            if (IsRelationalOperator(formulaToChange))
            {
                RootNode = formulaToChange.LeftChild;
                return true;
            }
            return false;
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
            if (formulaToChange.VariableValue.Contains("."))
            {
                return false;
            }
            formulaToChange.VariableValue += ".";
            return true;
        }

        private bool HandleSucceedingOperatorKey(FormulaEditorKey key)
        {
            
            if (HasSelection())
            {
                if (!IsOperator(Selection)) return false;
                ReplaceNodeAndReorderTree(key);
                return true;
            }
            var formulaToChange = RootNode;
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                if (!IsMinus(key)) return false;
                RootNode = DefaultNode(key);
                return true;
            }
            if (IsNumber(formulaToChange))
            {
                Superordinate(key);
                return true;
            }
            if (IsArithmeticOperator(formulaToChange))
            {
                if (formulaToChange.RightChild != null)
                {
                    Superordinate(key);
                    return true;
                }
                if (!IsMinus(key)) return false;
                Subordinate(formulaToChange, key);
                return true;
            }
            if (IsRelationalOperator(formulaToChange))
            {
                Subordinate(formulaToChange, key);
                return true;
            }
            if (IsLogicOperator(formulaToChange))
            {
                var parentNode = GetRightmostLogicOperatorNode();
                Subordinate(parentNode, key);
                return true;
            }
            return false;
        }

        private bool HandlePreceedingOperatorKey(FormulaEditorKey key)
        {
            if (HasSelection())
            {
                if (!IsOperator(Selection)) return false;
                RewriteNode(Selection, key);
                RotateDown(Selection, SelectionParent);
                return true;
            }
            var formulaToChange = GetRightmostNode();
            if (IsNumber(formulaToChange))
            {
                if (RootNode == formulaToChange)
                {
                    Superordinate(key);
                    return true;
                }
                formulaToChange = GetRightmostInnerNode();
                if (IsSignedNumber(formulaToChange))
                {
                    Superordinate(formulaToChange, key);
                }
                Subordinate(formulaToChange, key);
                return true;
            }
            return false;
        }

        private bool HandleRelationalOperatorKey(FormulaEditorKey key)
        {
            if (HasSelection())
            {
                if (!IsOperator(Selection)) return false;
                ReplaceNodeAndReorderTree(key);
                return true;
            }
            var formulaToChange = GetRightmostLogicOperatorNode();
            if (IsNull(formulaToChange))
            {
                Superordinate(key);
                return true;
            }
            Subordinate(formulaToChange, key);
            return true;
        }

        private bool HandleLogicAndKey()
        {
            if (HasSelection())
            {
                if (!IsOperator(Selection)) return false;
                ReplaceNodeAndReorderTree(FormulaEditorKey.KeyLogicAnd);
                return true;
            }
            var formulaToChange = GetRightmostLogicOperatorNode();
            if (IsNull(formulaToChange))
            {
                Superordinate(FormulaEditorKey.KeyLogicAnd);
                return true;
            }
            Subordinate(formulaToChange, FormulaEditorKey.KeyLogicAnd);
            return true;
        }

        private bool HandleLogicOrKey()
        {
            if (HasSelection())
            {
                if (!IsOperator(Selection)) return false;
                RewriteNode(Selection, FormulaEditorKey.KeyLogicOr);
                RotateUp(Selection);
                return true;
            }
            Superordinate(FormulaEditorKey.KeyLogicOr);
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

        private static bool IsSucceedingOperator(FormulaEditorKey key)
        {
            var result = false;
            result |= (key == FormulaEditorKey.KeyPlus);
            result |= (key == FormulaEditorKey.KeyMinus);
            return result;
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

        private static bool IsArithmeticOperator(FormulaTree node)
        {
            switch (node.VariableValue)
            {
                case "MULT":
                case "DEVIDE":
                case "PLUS":
                case "MINUS":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsEmpty(FormulaTree node)
        {
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

        private static bool IsLogicOperator(FormulaTree node)
        {
            if (IsNull(node)) return false;
            switch (node.VariableValue)
            {
                case "AND":
                case "OR":
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsNull(FormulaTree node)
        {
            return node == null;
        }

        private static bool IsMinus(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyMinus;
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
        

        #endregion


        #region Tree Navigation

        private FormulaTree GetRightmostNode()
        {
            var node = RootNode;
            if (IsNull(node)) return null;
            while (node.RightChild != null) node = node.RightChild;
            return node;
        }

        private FormulaTree GetRightmostInnerNode()
        {
            var node = RootNode;
            var oldNode = node;
            while (node.RightChild != null)
            {
                oldNode = node;
                node = node.RightChild;
            }
            return IsNull(node.LeftChild) ? oldNode : node;
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

        private FormulaTree GetRightmostLogicOperatorNode()
        {
            var node = RootNode;
            if (!IsLogicOperator(node)) return null;
            while (IsLogicOperator(node.RightChild))
            {
                node = node.RightChild;
            }
            return node;
        }

        private List<FormulaTree> GetPathToNode(FormulaTree node)
        {
            return Search(RootNode, node, new List<FormulaTree>());
        }

        private static List<FormulaTree> Search(FormulaTree node, FormulaTree expectedNode, List<FormulaTree> path)
        {
            if (node == expectedNode)
            {
                path.Add(node);
                return path;
            }
            if (!IsNull(node.LeftChild))
            {
                var subSearch = Search(node.LeftChild, expectedNode, path);
                if (subSearch != null)
                {
                    subSearch.Add(node);
                    return subSearch;
                }
            }
            if (!IsNull(node.RightChild))
            {
                var subSearch = Search(node.RightChild, expectedNode, path);
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
                if (childPriority == parentPriority && !IsLeftBranching(node)) return node.LeftChild;
            }
            if (!IsNull(node.RightChild))
            {
                var childPriority = GetNodePriority(node.RightChild);
                if (childPriority > parentPriority) return node.RightChild;
                if (childPriority == parentPriority && IsLeftBranching(node)) return node.RightChild;
            }
            return null;
        }

        #endregion


        #region Tree Operations

        private static void RewriteNode(FormulaTree node, FormulaEditorKey key)
        {
            var modelNode = DefaultNode(key);
            node.VariableType = modelNode.VariableType;
            node.VariableValue = modelNode.VariableValue;
        }

        private void Superordinate(FormulaTree node, FormulaEditorKey key)
        {
            
        }

        private void Superordinate(FormulaEditorKey key)
        {
            var node = RootNode;
            RootNode = DefaultNode(key);
            if (node != null && !IsEmpty(node))
            {
                RootNode.LeftChild = node;
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
            var replacementNode = formulaToChange.LeftChild;
            var parentNode = GetParentOfRightmostNode();
            if (IsNull(parentNode))
            {
                RootNode = replacementNode;
            }
            else
            {
                parentNode.RightChild = replacementNode;
            }
        }

        private void RotateUp(FormulaTree node)
        {
            var pathToNode = GetPathToNode(node);
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

        #endregion


        #region Some Short Helper Methods

        private FormulaTree GetFormulaToChange(FormulaTree standardFormula)
        {
            return HasSelection() ? Selection : standardFormula;
        }

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
            if (IsLeftBranching(upwardNode))
            {
                return upwardNode != downwardNode.LeftChild;
            }
            return upwardNode != downwardNode.RightChild;
        }

        private static bool IsLeftBranching(FormulaTree node)
        {
            return IsLogicOr(node) || IsRelationalOperator(node) || IsSucceedingOperator(node);
        }

        #endregion




#region Old code II
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
