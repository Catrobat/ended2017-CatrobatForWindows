using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public class VariableConteiner : INotifyPropertyChanged
    {
        private UserVariable _variable;

        public UserVariable Variable
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
