using System.Collections.Generic;
using System.Linq;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.CatrobatObjects.Variables;

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
            if (IsGeneralOperator(key)) return HandleGeneralOperatorKey(key);
            if (IsNegatingOperator(key)) return HandleNegatingOperator(key);
            if (IsLogicValue(key)) return HandleRightSubordinatingKey(key);
            if (IsOpenBracket(key)) return HandleOpenBracketKey();
            if (IsClosedBracket(key)) return HandleClosedBracketKey();
            if (IsFunction(key)) return HandleRightSubordinatingKey(key);
            return false;
        }

        public bool SensorVariableSelected(SensorVariable variable)
        {
            var node = DefaultNode(variable);
            return HandleRightSubordinatingNode(node);
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            var node = DefaultNode(variable);
            return HandleRightSubordinatingNode(node);
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            var node = FormulaDefaultValueCreater.GetDefaultValueForGlobalVariable(variable);
            return HandleRightSubordinatingNode(node);
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            var node = FormulaDefaultValueCreater.GetDefaultValueForLocalVariable(variable);
            return HandleRightSubordinatingNode(node);
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
            var rightmostClosedBracket = GetRightmostClosedBracket();
            if (!IsNull(rightmostClosedBracket))
            {
                OpenBracket(rightmostClosedBracket);
                return true;
            }
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange))
            {
                var rightmostOpenBracket = GetRightmostOpenBracket();
                if (!IsNull(rightmostOpenBracket))
                {
                    Extract(rightmostOpenBracket);
                    return true;
                }
                //formulaToChange = RootNodeOrSelection;
                //if (IsOpenBracket(formulaToChange))
                //{
                //    ReplaceNode(formulaToChange, DefaultNode(FormulaEditorKey.Number0));
                //    return true;
                //}
                //return false;
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

        private bool HandleDecimalSeparatorKey()
        {
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange))
            {
                var rightmostOpenBracket = GetRightmostOpenBracket();
                if (!IsNull(rightmostOpenBracket))
                {
                    Subordinate(rightmostOpenBracket, FormulaEditorKey.NumberDot);
                    return true;
                }
                return false;
            }
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
            if (IsNull(node.RightChild)) return false;
            CloseBracket(node);
            return true;
        }

        private bool HandleRightSubordinatingKey(FormulaEditorKey key)
        {
            return HandleRightSubordinatingNode(DefaultNode(key));
        }

        private bool HandleRightSubordinatingNode(FormulaTree node)
        {
            var formulaToChange = GetRightmostNode();
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
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
            return false;
        }

        private bool HandleGeneralOperatorKey(FormulaEditorKey key)
        {
            if (IsNull(RootNode) || IsEmpty(RootNode)) return false;
            if (IsOperator(GetRightmostNode())) return false;
            AddOperatorNode(key);
            return true;
        }

        private bool HandleNegatingOperator(FormulaEditorKey key)
        {
            var rightmostNode = GetRightmostNode();
            if (IsTerminalZero(rightmostNode))
            {
                ReplaceNode(rightmostNode, DefaultNode(key));
                return true;
            }
            if (IsOperator(rightmostNode))
            {
                if (IsNull(rightmostNode.LeftChild)) return false;
                Subordinate(rightmostNode, key);
                return true;
            }
            var formulaToChange = RootNode;
            if (IsNull(formulaToChange) || IsEmpty(formulaToChange))
            {
                RootNode = DefaultNode(key);
                return true;
            }
            if (key == FormulaEditorKey.KeyLogicNot) return false;
            AddOperatorNode(key);
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

        private static bool IsMinusOperator(FormulaTree node)
        {
            return node.VariableValue == "MINUS";
        }

        private static bool IsSignedNumber(FormulaTree node)
        {
            return IsMinusOperator(node)
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

        private static bool IsUnaryFunction(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.KeyMathSin:
                    return true;
                case FormulaEditorKey.KeyMathCos:
                    return true;
                case FormulaEditorKey.KeyMathTan:
                    return true;
                case FormulaEditorKey.KeyMathLn:
                    return true;
                case FormulaEditorKey.KeyMathLog:
                    return true;
                case FormulaEditorKey.KeyMathSqrt:
                    return true;
                case FormulaEditorKey.KeyMathAbs:
                    return true;
                case FormulaEditorKey.KeyMathRound:
                    return true;
                case FormulaEditorKey.KeyMathArcSin:
                    return true;
                case FormulaEditorKey.KeyMathArcCos:
                    return true;
                case FormulaEditorKey.KeyMathArcTan:
                    return true;
                case FormulaEditorKey.KeyMathExp:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsFunction(FormulaTree node)
        {
            return node.VariableType == "FUNCTION";
        }

        private static bool IsFunction(FormulaEditorKey key)
        {
            return IsUnaryFunction(key) || IsBinaryFunction(key) || IsLiteralFunction(key);
        }

        private static bool IsBinaryFunction(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.KeyMathRandom:
                    return true;
                case FormulaEditorKey.KeyMathMod:
                    return true;
                case FormulaEditorKey.KeyMathMax:
                    return true;
                case FormulaEditorKey.KeyMathMin:
                    return true;
                default:
                    return false;
            }
        }

        private static bool IsLiteralFunction(FormulaEditorKey key)
        {
            return key == FormulaEditorKey.KeyMathPi;
        }

        private static bool IsGeneralOperator(FormulaEditorKey key)
        {
            if (IsPlusOperator(key)) return true;
            if (IsPreceedingOperator(key)) return true;
            if (IsRelationalOperator(key)) return true;
            if (IsLogicAnd(key)) return true;
            if (IsLogicOr(key)) return true;
            return false;
        }

        private static bool IsNegatingOperator(FormulaEditorKey key)
        {
            return IsMinusOperator(key) || IsLogicNot(key);
        }

        #endregion


        #region Tree Navigation

        private FormulaTree GetRightmostNode()
        {
            var node = EffectiveRootNode;
            if (IsNull(node)) return null;
            while ((!IsNull(node.RightChild)) && (!IsFunction(node))) node = node.RightChild;
            return node;
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

        private static List<FormulaTree> DepthFirstSearch(FormulaTree node, FormulaTree expectedNode, List<FormulaTree> path)
        {
            if (node == expectedNode)
            {
                path.Add(node);
                return path;
            }
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

        private List<FormulaTree> DepthFirstSearch(FormulaTree node)
        {
            return DepthFirstSearch(RootNode, node, new List<FormulaTree>());
        }

        private FormulaTree GetRightmostOpenBracket()
        {
            var node = RootNodeOrSelection;
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
            var node = RootNodeOrSelection;
            //FormulaTree closedBracketNode = null;
            while (!IsNull(node))
            {
                if (IsClosedBracket(node))
                {
                    //closedBracketNode = node;
                    return node;
                }
                node = node.RightChild;
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

        private void Extract(FormulaTree node)
        {
            if (IsTerminalNode(node))
            {
                ReplaceNode(node, DefaultNode(FormulaEditorKey.Number0));
                return;
            }
            var replacementNode = node.LeftChild;
            var path = DepthFirstSearch(node);
            FormulaTree parentNode = null;
            if (path.Count > 1)
            {
                parentNode = path.ElementAt(1);
            }
            if (IsNull(parentNode))
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

        private static void SwapChildNode(FormulaTree node, FormulaTree oldChildNode, FormulaTree newChildNode)
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

        private static void ReplaceNode(FormulaTree oldNode, FormulaTree newNode)
        {
            oldNode.VariableType = newNode.VariableType;
            oldNode.VariableValue = newNode.VariableValue;
            oldNode.LeftChild = newNode.LeftChild;
            oldNode.RightChild = newNode.RightChild;
        }

        //private bool NegateSelectedNumber()
        //{
        //    var newNode = DefaultNode(FormulaEditorKey.KeyMinus);
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
        //    var newNode = DefaultNode(FormulaEditorKey.KeyLogicNot);
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
            if (IsNull(parentNode))
            {
                Superordinate(key);
                return;
            }
            Subordinate(parentNode, key);
        }

        private static void CloseBracket(FormulaTree node)
        {
            if (!IsOpenBracket(node)) return;
            node.VariableValue = "";
        }

        private static void OpenBracket(FormulaTree node)
        {
            if (!IsClosedBracket(node)) return;
            node.VariableValue = "OPEN";
        }

        #endregion


        #region Some Short Helper Methods

        private bool HasSelection()
        {
            return !IsNull(Selection);
        }

        private static FormulaTree DefaultNode(FormulaEditorKey key)
        {
            return FormulaDefaultValueCreater.GetDefaultValueForKey(key);
        }

        private static FormulaTree DefaultNode(SensorVariable variable)
        {
            return FormulaDefaultValueCreater.GetDefaultValueForSensorVariable(variable);
        }

        private static FormulaTree DefaultNode(ObjectVariable variable)
        {
            return FormulaDefaultValueCreater.GetDefaultValueForObjectVariable(variable);
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

        private static void DeleteDigit(FormulaTree node)
        {
            var length = node.VariableValue.Length;
            node.VariableValue = node.VariableValue.Substring(0, length - 1);
        }

        #endregion

    }
}
