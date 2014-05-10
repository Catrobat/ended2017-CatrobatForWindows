using System;
using Catrobat.IDE.Core.CatrobatObjects.Variables;
using Catrobat.IDE.Core.Formulas.Editor;
using System.ComponentModel;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    public class FormulaKey : INotifyPropertyChanged
    {
        #region Implements INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region Members

        private FormulaEditorKey _key;
        public FormulaEditorKey Key
        {
            get { return _key; }
            set
            {
                if (_key == value) return;
                _key = value;
                RaisePropertyChanged("Key");
                RaisePropertyChanged("MultiBindingProperty");
            }
        }

        private UserVariable _variable;
        public UserVariable Variable
        {
            get { return _variable; }
            set
            {
                if (_variable == value) return;
                if (_variable != null) _variable.PropertyChanged -= Variable_OnPropertyChanged;
                _variable = value;
                if (_variable != null) _variable.PropertyChanged += Variable_OnPropertyChanged;
                RaisePropertyChanged("Variable");
                RaisePropertyChanged("MultiBindingProperty");
            }
        }
        private void Variable_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged("MultiBindingProperty");
        }

        // workaround because MultiBinding is not supported in Windows Phone (see FormulaKeyboard.xaml) :/
        public FormulaKey MultiBindingProperty
        {
            get { return this; }
        }

        #endregion

        public FormulaKey()
        {
        }

        public FormulaKey(FormulaEditorKey key, UserVariable variable)
        {
            Key = key;
            Variable = variable;
        }

        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((FormulaKey)obj);
        }

        protected bool Equals(FormulaKey other)
        {
            // auto-implemented by ReSharper
            return Key == other.Key && Equals(Variable, other.Variable);
        }

        public override int GetHashCode()
        {
            // auto-implemented by ReSharper
            unchecked
            {
                return ((int)Key * 397) ^ (Variable != null ? Variable.GetHashCode() : 0);
            }
        }

        #endregion

    }
}
