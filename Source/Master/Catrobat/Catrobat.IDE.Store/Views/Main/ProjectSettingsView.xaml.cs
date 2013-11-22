using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Sprites;
using Catrobat.IDE.Core.ViewModel.Main;

namespace Catrobat.IDE.Store.Views.Main
{
    public sealed partial class ProjectSettingsView : UserControl
    {
        private readonly AddNewProjectViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewProjectViewModel;

        public ProjectSettingsView()
        {
            this.InitializeComponent();
        }

        private void TextBoxProgramName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProgramName.Text;
        }
    }
}
