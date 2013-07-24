using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Controls.FormulaControls.Formulas;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Practices.ServiceLocation;
using GestureEventArgs = System.Windows.Input.GestureEventArgs;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class UiFormulaTreeItem : UserControl
    {
        #region DependencyProperties

        public UiFormula UiFormula
        {
            get { return (UiFormula)GetValue(UiFormulaProperty); }
            set { SetValue(UiFormulaProperty, value); }
        }

        public static readonly DependencyProperty UiFormulaProperty = DependencyProperty.Register("UiFormula", typeof(UiFormula), typeof(UiFormulaTreeItem), new PropertyMetadata(UiFormulaChanged));

        private static void UiFormulaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var that = (UiFormulaTreeItem)d;
            that.UpdateUI();
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            UpdateUI();
        }

        #endregion

        private void UpdateUI()
        {
            ContentControlContent.Content = UiFormula;
            ContentControlContent.DataContext = UiFormula;
            ContentControlContent.ContentTemplate = UiFormula.Template;
        }

        public UiFormulaTreeItem()
        {
            InitializeComponent();
        }

        private void ContentControlContent_OnTap(object sender, GestureEventArgs e)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();
            viewModel.FormulaPartSelectedComand.Execute(UiFormula);
            e.Handled = true;
        }
    }
}
