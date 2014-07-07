using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
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

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            
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