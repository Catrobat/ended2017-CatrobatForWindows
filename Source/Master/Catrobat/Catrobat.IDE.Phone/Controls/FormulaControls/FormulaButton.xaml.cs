using System.Windows;
using System.Windows.Controls;
using Catrobat.IDE.Core.CatrobatObjects.Formulas;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.UI.Formula;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Catrobat.IDE.Phone.Views.Editor.Formula;
using Microsoft.Practices.ServiceLocation;

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
            Catrobat.IDE.Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof(FormulaEditorViewModel));
        }
    }
}
