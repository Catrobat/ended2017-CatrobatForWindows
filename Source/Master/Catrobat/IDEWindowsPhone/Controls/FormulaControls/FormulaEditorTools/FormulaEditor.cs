using System;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.FormulaEditorTools
{
    public class FormulaEditor
    {
        private readonly SelectedFormulaInformation _selectedFormulaInfo;

        public FormulaEditor(SelectedFormulaInformation selectedFormulaInfo)
        {
            _selectedFormulaInfo = selectedFormulaInfo;
        }

        public bool KeyPressed(FormulaEditorKey key)
        {
            bool isKeyValid = false;
            bool handled = false;

            if (_selectedFormulaInfo != null)
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
                    }
                    else
                    {
                        var newRoot = FormulaDefaultValueCreater.GetDefaultValueForKey(key);
                        _selectedFormulaInfo.FormulaRoot.FormulaTree = newRoot;
                    }
                }
            }

            return isKeyValid;
        }


        private bool NumberSelectedAndNumberKeyPressed(SelectedFormulaInformation selectedFormulaInfo, string digitString, bool isDelete, bool isDecimalSeperator)
        {
            bool isValid = false;

            var oldVariableValue = selectedFormulaInfo.SelectedFormula.VariableValue;


            if (digitString != null)
            {
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

        public bool SensorVariableSelected(Controls.FormulaControls.SensorVariable variable)
        {
            throw new NotImplementedException();

        }

        public bool ObjectVariableSelected(Controls.FormulaControls.ObjectVariable variable)
        {
            throw new NotImplementedException();
        }

        public bool GlobalVariableSelected(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
        }

        public bool LocalVariableSelected(Core.Objects.Variables.UserVariable variable)
        {
            throw new NotImplementedException();
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
    }
}
