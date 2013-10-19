using System.Windows.Controls;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class AddNewLocalVariableView : PhoneApplicationPage
    {
        private readonly AddNewLocalVariableViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewLocalVariableViewModel>();
        public AddNewLocalVariableView()
        {
            InitializeComponent();
            Dispatcher.BeginInvoke(() =>
            {
                TextBoxVariableName.Focus();
                TextBoxVariableName.SelectAll();
            });
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
        }


        private void TextBoxVariableName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.UserVariableName = TextBoxVariableName.Text;
        }
    }
}