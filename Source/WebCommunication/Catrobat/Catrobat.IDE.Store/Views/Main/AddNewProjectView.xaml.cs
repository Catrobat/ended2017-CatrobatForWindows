using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.Store.Views.Main
{
    public sealed partial class AddNewProjectView : UserControl
    {
        private readonly AddNewProjectViewModel _viewModel =
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewProjectViewModel;

        public AddNewProjectView()
        {
            this.InitializeComponent();
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProjectName.Text;
        }
    }
}
