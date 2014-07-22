using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProjectDetailView
    {
        private readonly ProjectSettingsViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProjectSettingsViewModel;

        public ProjectDetailView()
        {
            InitializeComponent();
        }

        //private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    _viewModel.ProjectName = TextBoxProjectName.Text;
        //}

        //private void TextBoxProjectDescription_OnTextChanged(object sender, TextChangedEventArgs e)
        //{
        //    _viewModel.ProjectDescription = TextBoxProjectDescription.Text;
        //}
    }
}