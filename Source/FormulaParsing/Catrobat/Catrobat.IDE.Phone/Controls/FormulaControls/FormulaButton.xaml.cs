using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using System.Windows;

namespace Catrobat.IDE.Phone.Controls.FormulaControls
{
    public partial class FormulaButton : IPortableFormulaButton
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

        public void FormulaChanged()
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            Formula.FormulaTree2 = viewModel.Formula;
        }

        private void ButtonFormula_OnClick(object sender, RoutedEventArgs e)
        {
            var viewModel = ServiceLocator.ViewModelLocator.FormulaEditorViewModel;
            viewModel.Formula = Formula.FormulaTree2;
            viewModel.CaretIndex = viewModel.FormulaString.Length;
            viewModel.FormulaButton = this;
            ServiceLocator.NavigationService.NavigateTo(typeof(FormulaEditorViewModel));
        }
    }
}
