using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDECommon.Formula.Editor
{
    public class FormulaEditor
    {
        private SelectedFormulaInformation _selectedFormulaInfo;
        private FormulaTree _currentSelection;

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

        public FormulaEditor()
        {

        }

        public bool KeyPressed(FormulaEditorKey key)
        {
            bool isKeyValid = false;
            bool handled = false;
            _currentSelection = SelectedFormula.SelectedFormula;
            SelectedFormula.SelectedFormula = null;
            if (_currentSelection != null)
            {
                _currentSelection.VariableValue = null;
            }

            if (IsNumber(key)) return HandleNumberKey(key);
            if (IsDelete(key)) return HandleDeleteKey();
            if (IsDecimalSeparator(key)) return HandleDecimalSeparatorKey();
            if (IsSucceedingOperator(key)) return HandleSucceedingOperatorKey(key);
            if (IsPreceedingOperator(key)) return HandlePreceedingOperatorKey(key);
            if (IsEquals(key)) return HandleEqualsKey();
            return isKeyValid;
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
            FormulaTree formulaToChange = GetRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);

            if (formulaToChange.VariableType == "OPERATOR")
            {
                formulaToChange.RightChild = new FormulaTree
                {
                    VariableType = "NUMBER",
                    VariableValue = GetKeyPressed(key)
                };
                return true;
            }
            if (formulaToChange.VariableType == "NUMBER")
            {
                formulaToChange.VariableValue += GetKeyPressed(key);
                return true;
            }
            if (formulaToChange.VariableValue == null)
            {
                formulaToChange.VariableValue = GetKeyPressed(key);
                formulaToChange.VariableType = "NUMBER";
                return true;
            }

            return false;
        }

        private bool HandleDeleteKey()
        {

            FormulaTree formulaToChange = GetRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
            if (formulaToChange.VariableValue == null)
            {
                return false;
            }
            if (formulaToChange.VariableValue == "")   return false;
            if (formulaToChange.VariableType == "NUMBER")
            {
                var length = formulaToChange.VariableValue.Length;
                formulaToChange.VariableValue = formulaToChange.VariableValue.Substring(0, length - 1);
                if (formulaToChange.VariableValue == "")
                {
                    var parentNode = GetParentOfRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
                    if (parentNode != null)
                    {
                        parentNode.RightChild = null;
                    }
                }
                return true;
            }
            if (formulaToChange.VariableType == "OPERATOR")
            {
                var replacementNode = formulaToChange.LeftChild;
                if (IsPreceedingOperator(formulaToChange.VariableValue))
                {
                    var parentNode = GetParentOfRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
                    if (parentNode == null)
                    {
                        SelectedFormula.FormulaRoot.FormulaTree = formulaToChange.LeftChild;
                        return true;
                    }
                    parentNode.RightChild = replacementNode;
                    return true;
                }
                if (IsSucceedingOperator(formulaToChange.VariableValue))
                {
                    var parentNode = GetParentOfRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
                    if (parentNode == null)
                    {
                        SelectedFormula.FormulaRoot.FormulaTree = formulaToChange.LeftChild;
                        return true;
                    }
                    parentNode.RightChild = replacementNode;
                    return true;
                }
                if (IsEquals(formulaToChange.VariableValue))
                {
                    SelectedFormula.FormulaRoot.FormulaTree = formulaToChange.LeftChild;
                    return true;
                }

            }

            return false;
        }

        private bool HandleDecimalSeparatorKey()
        {
            FormulaTree formulaToChange = GetRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
            if (formulaToChange.VariableValue == null || formulaToChange.VariableValue == "")
            {
                formulaToChange.VariableValue = "0.";
                formulaToChange.VariableType = "NUMBER";
            }
            else
            {
                if (formulaToChange.VariableValue.Contains(".")) return false;
                formulaToChange.VariableValue +=".";
            }
            return true;
        }

        private bool HandleSucceedingOperatorKey(FormulaEditorKey key)
        {
            FormulaTree formulaToChange = SelectedFormula.FormulaRoot.FormulaTree;
            if (formulaToChange.VariableType == "OPERATOR")
            {
                if (IsEquals(formulaToChange.VariableValue))
                {
                    var childNode = formulaToChange.RightChild;
                    formulaToChange.RightChild = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = GetKeyPressed(key),
                        LeftChild = childNode
                    };
                    return true;
                }
                SelectedFormula.FormulaRoot.FormulaTree = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = GetKeyPressed(key),
                    LeftChild = formulaToChange
                };
                return true;
            }
            if (formulaToChange.VariableType == "NUMBER")
            {
                SelectedFormula.FormulaRoot.FormulaTree = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = GetKeyPressed(key),
                    LeftChild = formulaToChange,
                };
                return true;
            }
            return false;
        }

        private bool HandlePreceedingOperatorKey(FormulaEditorKey key)
        {
            FormulaTree formulaToChange = GetRightmostNode(SelectedFormula.FormulaRoot.FormulaTree);
            if (formulaToChange.VariableType == "NUMBER")
            {
                var parentNode = GetRightmostInnerNode(SelectedFormula.FormulaRoot.FormulaTree);
                if (parentNode == formulaToChange)
                {
                    SelectedFormula.FormulaRoot.FormulaTree = new FormulaTree
                    {
                        VariableType = "OPERATOR",
                        VariableValue = GetKeyPressed(key),
                        LeftChild = formulaToChange
                    };
                    return true;

                }
                parentNode.RightChild = new FormulaTree
                {
                    VariableType = "OPERATOR",
                    VariableValue = GetKeyPressed(key),
                    LeftChild = formulaToChange
                };
                return true;
            }
            return false;
        }

        private bool HandleEqualsKey()
        {
            FormulaTree formulaToChange = SelectedFormula.FormulaRoot.FormulaTree;
            //if (IsEquals(formulaToChange.VariableValue))
            //{
            //    return false;
            //}
            SelectedFormula.FormulaRoot.FormulaTree = new FormulaTree
            {
                VariableType = "OPERATOR",
                VariableValue = "EQUAL",
                LeftChild = formulaToChange
            };

            return true;
        }

        #endregion

        #region Type Checks

        private bool IsLogicFormula(FormulaEditorKey key)
        {
            switch (key)
            {
                case FormulaEditorKey.KeyEquals:
                case FormulaEditorKey.KeyPlus:
                case FormulaEditorKey.KeyMinus:
                case FormulaEditorKey.KeyMult:
                case FormulaEditorKey.KeyDivide:
                case FormulaEditorKey.KeyLogicEqual:
                case FormulaEditorKey.KeyLogicNotEqual:
                case FormulaEditorKey.KeyLogicSmaller:
                case FormulaEditorKey.KeyLogicSmallerEqual:
                case FormulaEditorKey.KeyLogicGreater:
                case FormulaEditorKey.KeyLogicGreaterEqual:
                case FormulaEditorKey.KeyLogicAnd:
                case FormulaEditorKey.KeyLogicOr:
                case FormulaEditorKey.KeyLogicNot:
                case FormulaEditorKey.KeyLogicTrue:
                case FormulaEditorKey.KeyLogicFalse:
                    return true;
                default:
                    return false;
            }
        }

        private bool IsNumber(FormulaEditorKey key)
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

        private bool IsDelete(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.KeyDelete);
        }

        private bool IsDecimalSeparator(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.NumberDot);
        }

        private bool IsPreceedingOperator(FormulaEditorKey key)
        {
            bool result = false;
            result |= (key == FormulaEditorKey.KeyMult);
            result |= (key == FormulaEditorKey.KeyDivide);
            return result;
        }

        private bool IsPreceedingOperator(string operatorValue)
        {
            switch (operatorValue)
            {
                case "MULT":
                case "DEVIDE":
                    return true;
                default:
                    return false;
            }
        }

        private bool IsSucceedingOperator(FormulaEditorKey key)
        {
            bool result = false;
            result |= (key == FormulaEditorKey.KeyPlus);
            result |= (key == FormulaEditorKey.KeyMinus);
            return result;
        }

        private bool IsSucceedingOperator(string operatorValue)
        {
            switch (operatorValue)
            {
                case "PLUS":
                case "MINUS":
                    return true;
                default:
                    return false;
            }
        }

        private bool IsEquals(FormulaEditorKey key)
        {
            return (key == FormulaEditorKey.KeyEquals);
        }

        private bool IsEquals(string operatorValue)
        {
            return operatorValue == "EQUAL";
        }

        

        #endregion

        #region Tree Navigation

        private FormulaTree GetRightmostNode(FormulaTree root)
        {
            while (root.RightChild != null) root = root.RightChild;
            return root;
        }

        private FormulaTree GetRightmostInnerNode(FormulaTree root)
        {
            FormulaTree oldRoot = root;
            while (root.RightChild != null)
            {
                oldRoot = root;
                root = root.RightChild;
            }
            if (root.LeftChild == null) return oldRoot;
            return root;
        }

        private FormulaTree GetParentOfRightmostNode(FormulaTree root)
        {
            FormulaTree oldRoot = root;
            while (root.RightChild != null)
            {
                oldRoot = root;
                root = root.RightChild;
            }
            if (root == oldRoot) return null;
            return oldRoot;
        }

        #endregion




        private bool NumberSelectedAndNumberKeyPressed(SelectedFormulaInformation selectedFormulaInfo, string digitString, bool isDelete, bool isDecimalSeperator)
        {
            bool isValid = false;

            var oldVariableValue = selectedFormulaInfo.SelectedFormula.VariableValue;


            if (digitString != null)
            {
                if (selectedFormulaInfo.SelectedFormula.VariableValue == "0")
                    selectedFormulaInfo.SelectedFormula.VariableValue = "";

                selectedFormulaInfo.SelectedFormula.VariableValue += digitString;
                isValid = true;
            }

            if (isDelete)
            {
                if (!selectedFormulaInfo.SelectedFormula.VariableValue.Contains("."))
                {
                    selectedFormulaInfo.SelectedFormula.VariableValue += ".";
                    isValid = true;
                }
            }

            if (isDecimalSeperator)
            {
                if (oldVariableValue != "")
                {
                    selectedFormulaInfo.SelectedFormula.VariableValue = oldVariableValue.Substring(0, oldVariableValue.Length - 1);
                    isValid = true;
                }
            }

            return isValid;
        }

        private string GetKeyPressed(FormulaEditorKey key)
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
                case FormulaEditorKey.KeyPlus:
                    return "PLUS";
                case FormulaEditorKey.KeyMinus:
                    return "MINUS";
                case FormulaEditorKey.KeyMult:
                    return "MULT";
                case FormulaEditorKey.KeyDivide:
                    return "DEVIDE";
                default:
                    return null;
            }
        }

        public bool SensorVariableSelected(SensorVariable variable)
        {
            var newFormula = FormulaDefaultValueCreater.GetDefaultValueForSensorVariable(variable);

            if (_selectedFormulaInfo.SelectedFormulaParent != null)
            {
                if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
                    _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
                else
                    _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
            }
            else
            {
                _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
            }

            return true;
        }

        public bool ObjectVariableSelected(ObjectVariable variable)
        {
            var newFormula = FormulaDefaultValueCreater.GetDefaultValueForObjectVariable(variable);

            if (_selectedFormulaInfo.SelectedFormulaParent != null)
            {
                if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
                    _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
                else
                    _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
            }
            else
            {
                _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
            }

            return true;
        }

        public bool GlobalVariableSelected(UserVariable variable)
        {
            var newFormula = FormulaDefaultValueCreater.GetDefaultValueForGlobalVariable(variable);

            if (_selectedFormulaInfo.SelectedFormulaParent != null)
            {
                if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
                    _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
                else
                    _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
            }
            else
            {
                _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
            }

            return true;
        }

        public bool LocalVariableSelected(UserVariable variable)
        {
            var newFormula = FormulaDefaultValueCreater.GetDefaultValueForLocalVariable(variable);

            if (_selectedFormulaInfo.SelectedFormulaParent != null)
            {
                if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
                    _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newFormula;
                else
                    _selectedFormulaInfo.SelectedFormulaParent.RightChild = newFormula;
            }
            else
            {
                _selectedFormulaInfo.FormulaRoot.FormulaTree = newFormula;
            }

            return true;
        }
    }
}
