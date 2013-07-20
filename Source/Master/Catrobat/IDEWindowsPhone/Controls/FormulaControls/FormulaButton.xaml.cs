using System.Windows;
using System.Windows.Controls;
using Catrobat.Core.Objects.Formulas;
using Catrobat.IDEWindowsPhone.Misc;
using Catrobat.IDEWindowsPhone.Views.Editor.Formula;

namespace Catrobat.IDEWindowsPhone.Controls.FormulaControls
{
    public partial class FormulaButton : UserControl
    {
        public Formula Formula
        {
            get { return (Formula)GetValue(FormulaProperty); }
            set { SetValue(FormulaProperty, value); }
        }

        public static readonly DependencyProperty FormulaProperty = DependencyProperty.Register("Formula", typeof(Formula), typeof(FormulaButton), new PropertyMetadata(FormulaChanged));

        private static void FormulaChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((FormulaButton) d).ButtonFormula.Content = e.NewValue.ToString();
        }

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
            Navigation.NavigateTo(typeof(FormulaEditorView));
        }
    }
}
