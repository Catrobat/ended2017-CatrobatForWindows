using System.Windows.Controls;
using Catrobat.IDE.Phone.ViewModel.Editor.Formula;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Editor.Formula
{
    public partial class ChangeVariableView : PhoneApplicationPage
    {
        private readonly ChangeVariableViewModel _viewModel = ServiceLocator.Current.GetInstance<ChangeVariableViewModel>();

        public ChangeVariableView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
            {
                TextBoxVariableName.Focus();
                TextBoxVariableName.SelectAll();
            });
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.InitializeCommand.Execute(null);
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