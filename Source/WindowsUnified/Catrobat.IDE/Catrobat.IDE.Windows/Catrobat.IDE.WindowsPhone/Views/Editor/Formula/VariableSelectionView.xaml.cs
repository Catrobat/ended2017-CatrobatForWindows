using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Formula
{
    public partial class VariableSelectionView
    {
        readonly VariableSelectionViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.VariableSelectionViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public VariableSelectionView()
        {
            InitializeComponent();
        }

        private void TextBoxLocalVariableName_KeyDown(object sender, KeyRoutedEventArgs args)
        {
            _viewModel.SelectedLocalVariable.Name = ((TextBox) sender).Text;
        }

        private void TextBoxGlobalVariableName_KeyDown(object sender, KeyRoutedEventArgs args)
        {
            _viewModel.SelectedGlobalVariable.Name = ((TextBox) sender).Text;
        }
    }
}