using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Editor.Scripts;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Views.Editor.Scripts
{
    public partial class AddNewScriptBrickView : PhoneApplicationPage
    {
        private readonly AddNewScriptBrickViewModel _viewModel = 
            ((ViewModelLocator)ServiceLocator.ViewModelLocator).AddNewScriptBrickViewModel;

        public AddNewScriptBrickView()
        {
            InitializeComponent();
        }

        protected override void OnBackKeyPress(CancelEventArgs e)
        {
            _viewModel.GoBackCommand.Execute(null);
            e.Cancel = true;
            base.OnBackKeyPress(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _viewModel.OnLoadBrickViewCommand.Execute(NavigationContext);
        }

        private void reorderListBoxScriptBricks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _viewModel.AddNewScriptBrickCommand.Execute(((ListBox) sender).SelectedItem as DataObject);
        }
    }
}