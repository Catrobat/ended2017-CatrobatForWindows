using System.ComponentModel;
using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;

namespace Catrobat.IDE.Phone.Controls.Formulas
{
    public partial class FormulaButton
    {
        #region DependencyProperties

        public Formula Formula
        {
            get { return (Formula)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(Formula), typeof(FormulaButton), new PropertyMetadata(null));

        #endregion

        private new static void IsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaButton)d).IsEnabled = (bool) e.NewValue;
        }

        public FormulaButton()
        {
            InitializeComponent();
        }

        private void ButtonFormula_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            viewModel.Cleanup();
            viewModel.Formula = Formula.FormulaTree2;
            SetFormulaBinding();
            ServiceLocator.NavigationService.NavigateTo(typeof(FormulaEditorViewModel));
        }

        private void SetFormulaBinding()
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            viewModel.PropertyChanged += ViewModelOnPropertyChanged;
            viewModel.Reset += ViewModelOnReset;
        }

        private void UnsetFormulaBinding()
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            viewModel.PropertyChanged -= ViewModelOnPropertyChanged;
            viewModel.Reset -= ViewModelOnReset;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs arg)
        {
            if (arg.PropertyName == "Formula")
            {
                Formula.FormulaTree2 = ((FormulaEditorViewModel)sender).Formula;
            }
        }

        private void ViewModelOnReset()
        {
            UnsetFormulaBinding();
        }
    }
}
