using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Editor.Scripts;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Catrobat.IDE.WindowsPhone.Views.Editor.Scripts
{
    public partial class AddNewScriptBrickView
    {
        private readonly AddNewScriptBrickViewModel _viewModel = 
            ServiceLocator.ViewModelLocator.AddNewScriptBrickViewModel;

        protected override ViewModelBase GetViewModel() { return _viewModel; }

        public AddNewScriptBrickView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.OnLoadBrickViewCommand.Execute(null);
        }

        private void reorderListBoxScriptBricks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.AddNewScriptBrickCommand.Execute(((ListView) sender).SelectedItem as Model);
        }
    }
}