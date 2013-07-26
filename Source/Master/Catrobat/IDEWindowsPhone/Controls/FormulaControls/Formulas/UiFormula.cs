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
    public abstract class UiFormula : INotifyPropertyChanged
    {
        private bool _isSelected;
        private double _fontSize;

        public Formula FormulaRoot { get; set; }

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

        public List<Grid> UiControls;

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

        //private void SetSelected(bool isSelected)
        //{
        //    var root = FindRoot()
        //    root.SetSelectedRecursive(isSelected);
        //}

        //private void SetSelectedRecursive(bool isSelected)
        //{
        //    this.IsSelected = isSelected;
        //    LeftFormula.SetSelectedRecursive(isSelected);
        //    RightFormula.SetSelectedRecursive(isSelected);
        //}

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
                LeftFormula.SetStyle(false, isSelected);

            if (RightFormula != null)
                RightFormula.SetStyle(false, isSelected);
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

        public abstract FormulaPartControlList Template { get; }

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


        //public List<Grid> GetAllControls(int fontSize)
        //{
        //    var allParts = GetAllParts();
        //    var allControls = new List<Grid>();

        //    foreach (var control in allParts)
        //    {
        //        allControls.Add(control.CreateUiControls(fontSize));
        //    }

        //    return allControls;
        //}

        //public List<FormulaPartControl> GetMyParts()
        //{
        //    var allParts = Template.ToList();

        //    foreach (var part in allParts)
        //    {
        //        part.UiFormula = this;
        //    }


        //}


        public List<FormulaPartControl> GetAllParts()
        {
            UiControls = new List<Grid>();
            var allParts = new List<FormulaPartControl>();

            foreach (var control in Template.ToList())
            {
                allParts.Add(control.Copy());
            }

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

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

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
    }
}
