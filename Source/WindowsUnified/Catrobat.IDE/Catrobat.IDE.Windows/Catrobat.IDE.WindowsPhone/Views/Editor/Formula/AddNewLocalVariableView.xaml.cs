using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Formula;
using Windows.UI.Xaml.Controls;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Formula
{
    public partial class AddNewLocalVariableView
    {
        private readonly AddNewLocalVariableViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewLocalVariableViewModel;

        

        public AddNewLocalVariableView()
        {
            InitializeComponent();
            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                //TextBoxVariableName.Focus(FocusState.Keyboard);
                TextBoxVariableName.SelectAll();
            });
        }


        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}