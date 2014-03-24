using System;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.FormulaEditor.Editor;

namespace Catrobat.IDE.Core.ViewModel.Editor.Formula
{
    public class FormulaKeyEventArgs : EventArgs
    {
        #region Members

        private readonly FormulaEditorKey _key;
        public FormulaEditorKey Key { get { return _key; } }

        private readonly ObjectVariableEntry _objectVariable;
        public ObjectVariableEntry ObjectVariable { get { return _objectVariable; } }

        private readonly UserVariable _userVariable;
        public UserVariable UserVariable { get { return _userVariable; } }

        #endregion

        public FormulaKeyEventArgs(FormulaEditorKey key, ObjectVariableEntry objectVariable, UserVariable userVariable)
        {
            _key = key;
            _objectVariable = objectVariable;
            _userVariable = userVariable;
        }

    }
}
