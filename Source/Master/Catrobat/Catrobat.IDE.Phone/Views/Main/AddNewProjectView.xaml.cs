using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Phone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDE.Phone.Views.Main
{
    public partial class AddNewProjectView : PhoneApplicationPage
    {
        private readonly AddNewProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewProjectViewModel>();

        public AddNewProjectView()
        {
            InitializeComponent();

            Dispatcher.BeginInvoke(() =>
                {
                    TextBoxProjectName.Focus();
                    TextBoxProjectName.SelectAll();
                });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProjectName.Text;
        }
    }
}