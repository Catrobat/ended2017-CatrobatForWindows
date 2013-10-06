using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using Catrobat.Core.CatrobatObjects.Formulas;
using Catrobat.Core.CatrobatObjects.Variables;
using Catrobat.IDEWindowsPhone.Converters;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.ViewModel;
using Catrobat.IDEWindowsPhone.ViewModel.Editor.Formula;
using Catrobat.IDEWindowsPhone.Views.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class FormulaButton : UserControl
    {
        #region DependencyProperties

        public Formula Formula
        {
            get { return (Formula)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(Formula), typeof(FormulaButton), new PropertyMetadata(FormulaChanged));

        private static void FormulaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaButton)d).FormulaViewer.Formula = e.NewValue as Formula;
        }

        #endregion

        private new static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaButton)d).IsEnabled = (bool) e.NewValue;
        }

        public FormulaButton()
        {
            InitializeComponent();
        }

        public void FormulaChanged()
        {
            FormulaViewer.Formula = Formula;
        }

        private void ButtonFormula_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.Current.GetInstance<FormulaEditorViewModel>();
            viewModel.Formula = Formula;
            viewModel.FormulaButton = this;
            Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof(FormulaEditorView));
        }
    }
}
