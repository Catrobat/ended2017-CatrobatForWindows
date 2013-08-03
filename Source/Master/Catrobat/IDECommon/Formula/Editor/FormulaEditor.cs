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

        public FormulaEditor()
        {

        }

        public bool KeyPressed(FormulaEditorKey key)
        {
            bool isKeyValid = false;
            bool handled = false;


            if (_selectedFormulaInfo.SelectedFormula == null &&
                _selectedFormulaInfo.FormulaRoot.FormulaTree != null &&
                _selectedFormulaInfo.FormulaRoot.FormulaTree.VariableType == "NUMBER")
            {
                var digitString = GetKeyPressed(key);
                bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
                bool isDelete = key == FormulaEditorKey.KeyDelete;

                if (digitString != null || isDecimalSeperator || isDelete)
                {
                    SelectedFormula.SelectedFormula = _selectedFormulaInfo.FormulaRoot.FormulaTree;
                }
            }


            if (_selectedFormulaInfo.SelectedFormula == null &&
                _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild != null &&
                _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild.VariableType == "NUMBER")
            {
                var digitString = GetKeyPressed(key);
                bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
                bool isDelete = key == FormulaEditorKey.KeyDelete;

                if (digitString != null || isDecimalSeperator || isDelete)
                {
                    SelectedFormula.SelectedFormula = _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild;
                    SelectedFormula.SelectedFormulaParent = _selectedFormulaInfo.FormulaRoot.FormulaTree;
                }
            }


            if (_selectedFormulaInfo.SelectedFormula != null)
            {
                if (_selectedFormulaInfo.SelectedFormula.VariableType == "NUMBER")
                {
                    var digitString = GetKeyPressed(key);
                    bool isDecimalSeperator = key == FormulaEditorKey.NumberDot;
                    bool isDelete = key == FormulaEditorKey.KeyDelete;


                    if (digitString != null || isDecimalSeperator || isDelete)
                    {
                        isKeyValid = NumberSelectedAndNumberKeyPressed(_selectedFormulaInfo, digitString,
                            isDecimalSeperator, isDelete);
                        handled = true;
                    }
                }

                if (!handled)
                {
                    if (_selectedFormulaInfo.SelectedFormulaParent != null)
                    {
                        var newChild = FormulaDefaultValueCreater.GetDefaultValueForKey(key);

                        if (_selectedFormulaInfo.SelectedFormulaParent.LeftChild == _selectedFormulaInfo.SelectedFormula)
                            _selectedFormulaInfo.SelectedFormulaParent.LeftChild = newChild;
                        else
                            _selectedFormulaInfo.SelectedFormulaParent.RightChild = newChild;

                        isKeyValid = true;
                    }
                    else
                    {
                        var newRoot = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
                        _selectedFormulaInfo.FormulaRoot.FormulaTree = newRoot;
                        isKeyValid = true;
                    }
                }
            }
            else
            {
                if(IsLogicFormula(key))
                {
                    var newRoot = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
                    newRoot.LeftChild = _selectedFormulaInfo.FormulaRoot.FormulaTree;
                    //newRoot.RightChild = new FormulaTree
                    //{
                    //    VariableType = "#EMPTY#",
                    //    VariableValue = ""
                    //};
                    _selectedFormulaInfo.FormulaRoot.FormulaTree = newRoot;
                }
                else
                {
                    if (_selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild == null)
                    {
                        var newChild = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
                        _selectedFormulaInfo.FormulaRoot.FormulaTree.RightChild = newChild;
                    }
                }
            }


            return isKeyValid;
        }

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
