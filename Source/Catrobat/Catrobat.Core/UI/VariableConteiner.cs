using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Catrobat.IDE.Core.Models;
using System.Diagnostics;

namespace Catrobat.IDE.Core.UI
{
    [Obsolete("Replace with binding. ")]
    public class VariableConteiner : INotifyPropertyChanged
    {
        private Variable _variable;

        public Variable Variable
        {
            get { return _variable; }
            set
            {
                _variable = value; 
                RaisePropertyChanged();
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
