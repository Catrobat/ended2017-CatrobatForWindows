using System.ComponentModel;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Catrobat.IDE.Core.Xml.XmlObjects.Formulas;

namespace Catrobat.IDE.Phone.Controls.Formulas
{
    public partial class FormulaButton
    {
        #region DependencyProperties

        public XmlFormula Formula
        {
            get { return (XmlFormula)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(XmlFormula), typeof(FormulaButton), new PropertyMetadata(null));

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
