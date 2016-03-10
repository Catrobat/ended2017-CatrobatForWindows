using System.Diagnostics;
using Catrobat.IDE.Core.Formulas.Editor;
using System.ComponentModel;
using Catrobat.IDE.Core.Models;
using GalaSoft.MvvmLight;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.Core.ViewModels.Editor.Formula
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class FormulaKey : ObservableObject
    {
        private bool _enabled = false;

        public bool IsEnabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
            }
        }

        #region Members

        private FormulaEditorKey _key;
        public FormulaEditorKey Key
        {
            get { return _key; }
            set
            {
                if (_key == value) return;
                _key = value;
                RaisePropertyChanged();
                RaisePropertyChanged(() => MultiBindingProperty);
            }
        }

        private LocalVariable _localVariable;
        public LocalVariable LocalVariable
        {
            get { return _localVariable; }
            set
            {
                if (ReferenceEquals(_localVariable, value)) return;
                if (_localVariable != null) _localVariable.PropertyChanged -= Variable_OnPropertyChanged;
                _localVariable = value;
                if (_localVariable != null) _localVariable.PropertyChanged += Variable_OnPropertyChanged;
                RaisePropertyChanged();
                RaisePropertyChanged(() => MultiBindingProperty);
            }
        }

        private GlobalVariable _globalVariable;
        public GlobalVariable GlobalVariable
        {
            get { return _globalVariable; }
            set
            {
                if (ReferenceEquals(_globalVariable, value)) return;
                if (_globalVariable != null) _globalVariable.PropertyChanged -= Variable_OnPropertyChanged;
                _globalVariable = value;
                if (_globalVariable != null) _globalVariable.PropertyChanged += Variable_OnPropertyChanged;
                RaisePropertyChanged();
                RaisePropertyChanged(() => MultiBindingProperty);
            }
        }

        private void Variable_OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            RaisePropertyChanged(() => MultiBindingProperty);
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

        public FormulaKey(FormulaEditorKey key, LocalVariable localVariable = null, GlobalVariable globalVariable = null)
        {
            Key = key;
            LocalVariable = localVariable;
            GlobalVariable = globalVariable;
        }

        protected string DebuggerDisplay
        {
            get
            {
                return "Key = " + Key +
                    (LocalVariable == null ? string.Empty : ", Variable = " + LocalVariable.Name) +
                    (GlobalVariable == null ? string.Empty : ", Variable = " + GlobalVariable.Name);
            }
        }

        #region Overrides Equals

        public override bool Equals(object obj)
        {
            // auto-implemented by ReSharper
            return !ReferenceEquals(null, obj) &&
                   (ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((FormulaKey)obj));
        }

        protected bool Equals(FormulaKey other)
        {
            // auto-implemented by ReSharper
            return _key == other._key && Equals(_localVariable, other._localVariable) && Equals(_globalVariable, other._globalVariable);
        }

        public override int GetHashCode()
        {
            // no readonly fields
            return 0;
        }

        #endregion

    }
}
