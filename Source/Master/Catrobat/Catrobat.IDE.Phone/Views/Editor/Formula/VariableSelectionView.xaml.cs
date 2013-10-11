using System.Windows.Controls;
using System.Windows.Input;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class VariableSelectionView : PhoneApplicationPage
    {
        readonly VariableSelectionViewModel _viewModel = ServiceLocator.Current.GetInstance<VariableSelectionViewModel>();

        public VariableSelectionView()
        {
            InitializeComponent();
        }

        private void TextBoxLocalVariableName_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.SelectedLocalVariable.Name = ((TextBox) sender).Text;
        }

        private void TextBoxGlobalVariableName_KeyDown(object sender, KeyEventArgs e)
        {
            _viewModel.SelectedGlobalVariable.Name = ((TextBox) sender).Text;
        }
    }
}