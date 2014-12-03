using System;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class FormulaKeyEventArgs : EventArgs
    {
        #region Members

        private readonly FormulaKey _data;
        public FormulaKey Data { get { return _data; } }

        #endregion

        public FormulaKeyEventArgs(FormulaKey data)
        {
            _data = data;
        }
    }
}
