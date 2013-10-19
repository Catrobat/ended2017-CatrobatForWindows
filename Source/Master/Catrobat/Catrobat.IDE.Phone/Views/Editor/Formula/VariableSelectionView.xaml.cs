using System.Windows.Controls;
using System.Windows.Input;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class VariableSelectionView : PhoneApplicationPage
    {
        readonly VariableSelectionViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).VariableSelectionViewModel;

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