using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor
{
    public partial class ProjectSettingsView : PhoneApplicationPage
    {
        private readonly ProjectSettingsViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).ProjectSettingsViewModel;

        public ProjectSettingsView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
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