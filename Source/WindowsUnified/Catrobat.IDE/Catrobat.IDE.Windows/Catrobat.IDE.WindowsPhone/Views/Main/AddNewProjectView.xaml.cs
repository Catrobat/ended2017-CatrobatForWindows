using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class AddNewProjectView
    {
        private readonly AddNewProjectViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewProjectViewModel;

        

        public AddNewProjectView()
        {
            InitializeComponent();

            ServiceLocator.DispatcherService.RunOnMainThread(() =>
            {
                //TextBoxProjectName.Focus(FocusState.Keyboard);
                TextBoxProjectName.SelectAll();
            });
        }

        private void TextBoxProjectName_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.ProjectName = TextBoxProjectName.Text;
        }
    }
}