using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Phone.Controls.FormulaControls.PartControls;

namespace Catrobat.IDE.Phone.Controls.FormulaControls.Formulas
{
    [Obsolete("Included directly in FormulaViewer3")]
    public class UiFormulaTokenList : INotifyPropertyChanged, IPortableUIFormula
    {
        [Obsolete("Create abstract IProtableFormulaViewer")]
        public FormulaViewer3 Viewer { get; set; }

        public List<IFormulaToken> Tokens { get; set; }

        public List<FormulaPartControl> Templates { get; set; }

        public List<Grid> Children { get; set; }

        public bool IsEditEnabled { get; set; }

        public double FontSize { get; set; }

        private bool _isSelected;
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

        public void SetSelection(int startIndex, int count)
        {
            var endIndex = startIndex + count;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var partControl = child.DataContext as FormulaPartControl;
                if (partControl == null) continue;

                var styles = partControl.Style;
                if (styles == null) continue;

                var isSelected = startIndex <= i && i <= endIndex;
                ApplyStyle(
                    control: child, 
                    containerStyle: isSelected ? styles.SelectedContainerStyle : styles.ContainerStyle, 
                    textStyle: isSelected ? styles.SelectedTextStyle : styles.TextStyle);
            }
        }

        public void SetError(int startIndex, int count)
        {
            throw new NotImplementedException();
            // TODO: create error styles
            var endIndex = startIndex + count;
            for (var i = 0; i < Children.Count; i++)
            {
                var child = Children[i];
                var partControl = child.DataContext as FormulaPartControl;
                if (partControl == null) continue;

                var styles = partControl.Style;
                if (styles == null) continue;


                var isError = startIndex <= i && i <= endIndex;
                //ApplyStyle(
                //    control: child,
                //    containerStyle: isError ? styles.ErrorContainerStyle : styles.ContainerStyle,
                //    textStyle: isError ? styles.ErrorTextStyle : styles.TextStyle);
            }
        }

        internal void ApplyStyle(Grid control, Style containerStyle, Style textStyle)
        {
            control.Style = containerStyle;
            foreach (var textBlock in control.Children.OfType<TextBlock>())
            {
                textBlock.Style = textStyle;
            }
        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion


        public void ClearAllSelection()
        {
            // TODO: change implemented interface
            throw new NotImplementedException();
        }
    }
}
