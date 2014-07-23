using Catrobat.IDE.Core.Models.Formulas.Tree;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using System.ComponentModel;
using Windows.UI.Xaml;
using Catrobat.IDE.WindowsShared.Converters;

namespace Catrobat.IDE.WindowsPhone.Controls.Formulas
{
    public partial class FormulaButton
    {
        #region DependencyProperties

        public FormulaTree Formula
        {
            get { return (FormulaTree)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = 
            DependencyProperty.Register("Formula", 
            typeof(FormulaTree), typeof(FormulaButton), new PropertyMetadata(null, FormulaChangedCallback));

        private static void FormulaChangedCallback(DependencyObject d, 
            DependencyPropertyChangedEventArgs args)
        {
            var formulaStringConverter = new FormulaStringConverterBootstrap();

            ((FormulaButton) d).TextBlockFormula.Text = (string)
                formulaStringConverter.Convert(args.NewValue, typeof(FormulaButton), null, "");
            //throw new NotImplementedException();
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

        private void ButtonFormula_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            viewModel.Cleanup();
            viewModel.Formula = Formula;
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
                Formula = ((FormulaEditorViewModel)sender).Formula;
            }
        }

        private void ViewModelOnReset()
        {
            UnsetFormulaBinding();
        }
    }
}
