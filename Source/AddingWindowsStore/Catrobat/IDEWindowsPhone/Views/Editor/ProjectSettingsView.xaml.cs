using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Editor;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor
{
    public partial class ProjectSettingsView : PhoneApplicationPage
    {
        private readonly ProjectSettingsViewModel _viewModel = ServiceLocator.Current.GetInstance<ProjectSettingsViewModel>();

        public ProjectSettingsView()
        {
            InitializeComponent();
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

        private void TextBoxProjectDescription_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectDescription = TextBoxProjectDescription.Text;
        }
    }
}