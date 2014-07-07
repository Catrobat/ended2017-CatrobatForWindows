using System.ComponentModel;
using System.Windows.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class AddNewLocalVariableView : PhoneApplicationPage
    {
        private readonly AddNewLocalVariableViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewLocalVariableViewModel;
        public AddNewLocalVariableView()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxVariableName.Focus();
                TextBoxVariableName.SelectAll();
            });
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}