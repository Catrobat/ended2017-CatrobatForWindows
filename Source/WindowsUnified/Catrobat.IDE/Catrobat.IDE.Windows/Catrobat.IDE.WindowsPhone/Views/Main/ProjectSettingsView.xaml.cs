using Windows.UI.Xaml.Controls;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Main
{
    public partial class ProjectSettingsView
    {
        private readonly ProjectSettingsViewModel _viewModel =
            ServiceLocator.ViewModelLocator.ProjectSettingsViewModel;   

        public ProjectSettingsView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.InitializeCommand.Execute(null);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            (sender as TextBox).GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}