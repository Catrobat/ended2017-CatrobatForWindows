using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public abstract class UiFormula : INotifyPropertyChanged
    {
        private bool _isSelected;
        private double _fontSize;

        public FormulaViewer Viewer { get; set; }

        public FormulaTree TreeItem { get; set; }

        public string FormulaValue
        {
            get
            {
                return TreeItem.VariableValue;
            }
        }


        public UiFormula LeftFormula { get; set; }

        public UiFormula RightFormula { get; set; }

        public UiFormula ParentFormula { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                RaisePropertyChanged();
            }
        }

        public double FontSize
        {
            get { return _fontSize; }
            set
            {
                _fontSize = value;

                if (LeftFormula != null)
                    LeftFormula.FontSize = value;

                if (RightFormula != null)
                    RightFormula.FontSize = value;
            }
        }

        public bool IsEditEnabled { get; set; }

        public abstract DataTemplate Template { get; }

        public void ClearChildrensSelection()
        {
            this.IsSelected = false;

            if (LeftFormula != null)
                LeftFormula.ClearChildrensSelection();

            if (RightFormula != null)
                RightFormula.ClearChildrensSelection();
        }

        public void ClearAllSelection()
        {
            FindRoot().ClearChildrensSelection();
        }

        private UiFormula FindRoot()
        {
            var parent = this;
            while (true)
            {
                if (parent.ParentFormula != null)
                    parent = parent.ParentFormula;
                else
                    break;
            }

            return parent;
        }

        public void SetViewer(FormulaViewer viewer)
        {
            FindRoot().SetChildrensViewer(viewer);
        }

        private void SetChildrensViewer(FormulaViewer viewer)
        {
            Viewer = viewer;

            if (LeftFormula != null)
                LeftFormula.SetChildrensViewer(viewer);

            if (RightFormula != null)
                RightFormula.SetChildrensViewer(viewer);
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
