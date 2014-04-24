using System;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas.Editor;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class FormulaKeyEventArgs : EventArgs
    {
        #region Members

        private readonly FormulaEditorKey _key;
        public FormulaEditorKey Key { get { return _key; } }

        private readonly UserVariable _userVariable;
        public UserVariable UserVariable { get { return _userVariable; } }

        #endregion

        public FormulaKeyEventArgs(FormulaEditorKey key, UserVariable userVariable)
        {
            _key = key;
            _userVariable = userVariable;
        }

    }
}
