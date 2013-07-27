using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Catrobat.Core.Objects.Formulas;
using Catrobat.Core.Objects.Variables;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.PartControls;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas
{
    public class UiFormula : INotifyPropertyChanged
    {
        private bool _isSelected;
        private double _fontSize;

        public Formula FormulaRoot { get; set; }

        public FormulaViewer Viewer { get; set; }

        public FormulaTree TreeItem { get; set; }

        public UiFormula LeftFormula { get; set; }

        public UiFormula RightFormula { get; set; }

        public UiFormula ParentFormula { get; set; }

        public bool IsEditEnabled { get; set; }

        public FormulaPartControlList Template { get; set; }

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


        public string FormulaValue
        {
            get
            {
                return TreeItem.VariableValue;
            }
        }

        public void SetViewer(FormulaViewer viewer)
        {
            FindRoot().SetChildrensViewer(viewer);
        }

        public List<Grid> UiControls { get; set; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;

                RaisePropertyChanged();
            }
        }

        public void ClearAllBackground()
        {
            var root = FindRoot();
            root.SetStyle(false, false);
        }

        internal void UpdateStyles(bool isParentSelected)
        {
            SetStyle(_isSelected, isParentSelected);

            if (LeftFormula != null)
                LeftFormula.UpdateStyles(_isSelected);

            if (RightFormula != null)
                RightFormula.UpdateStyles(_isSelected);
        }

        public void SetStyle(bool isSelected, bool isParentSelected)
        {
            foreach (var control in UiControls)
            {
                var formulaPartControl = control.DataContext as FormulaPartControl;
                if (formulaPartControl != null)
                {
                    var styles = formulaPartControl.Style;

                    var textBlocks = control.Children.OfType<TextBlock>().Select(child => child as TextBlock).ToList();

                    if (styles != null)
                    {

                        Style containerStyle = styles.ContainerStyle;
                        Style textStyle = styles.TextStyle;

                        if (isParentSelected)
                        {
                            textStyle = styles.ParentSelectedTextStyle;
                            containerStyle = styles.ParentSelectedContainerStyle;
                        }

                        if (isSelected)
                        {
                            textStyle = styles.SelectedTextStyle;
                            containerStyle = styles.SelectedContainerStyle;
                        }

                        control.Style = containerStyle;

                        foreach (var textBlock in textBlocks)
                            textBlock.Style = textStyle;
                    }
                }
            }

            if (LeftFormula != null)
                LeftFormula.SetStyle(false, isSelected || isParentSelected);

            if (RightFormula != null)
                RightFormula.SetStyle(false, isSelected || isParentSelected);
        }


        public void ClearAllSelection()
        {
            FindRoot().ClearChildrensSelection();
        }

        public void ClearChildrensSelection()
        {
            this.IsSelected = false;

            if (LeftFormula != null)
                LeftFormula.ClearChildrensSelection();

            if (RightFormula != null)
                RightFormula.ClearChildrensSelection();
        }


        public List<FormulaPartControl> GetAllParts()
        {
            UiControls = new List<Grid>();

            var allParts = Template.ToList().Select(control => control.Copy()).ToList();

            foreach (var part in allParts)
            {
                part.UiFormula = this;
            }

            var leftPartIndex = -1;
            for (int i = 0; i < allParts.Count; i++)
            {
                if (allParts[i] is FormulaPartControlPlaceholderLeft)
                    leftPartIndex = i;
            }

            if (leftPartIndex != -1 && LeftFormula != null)
            {
                allParts.RemoveAt(leftPartIndex);
                var leftParts = LeftFormula.GetAllParts();

                allParts.InsertRange(leftPartIndex, leftParts);
            }

            var rightPartIndex = -1;
            for (int i = 0; i < allParts.Count; i++)
            {
                if (allParts[i] is FormulaPartControlPlaceholderRight)
                    rightPartIndex = i;
            }

            if (rightPartIndex != -1 && RightFormula != null)
            {
                allParts.RemoveAt(rightPartIndex);
                var rightParts = RightFormula.GetAllParts();
                allParts.InsertRange(rightPartIndex, rightParts);
            }

            return allParts;
        }

        public SelectedFormulaInformation GetSelectedFormula()
        {
            SelectedFormulaInformation formulaInformation = null;

            if (this.IsSelected)
            {
                FormulaTree parent = null;

                if (ParentFormula != null)
                    parent = ParentFormula.TreeItem;

                formulaInformation = new SelectedFormulaInformation
                {
                    SelectedFormula = TreeItem,
                    SelectedUiFormula = this,
                    SelectedFormulaParent = parent,
                    FormulaRoot = FormulaRoot
                };
            }

            if (formulaInformation == null && LeftFormula != null)
                formulaInformation = LeftFormula.GetSelectedFormula();

            if (formulaInformation == null && RightFormula != null)
                formulaInformation = RightFormula.GetSelectedFormula();

            return formulaInformation;
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
