using System.Windows.Controls;
using System.Windows.Navigation;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel.Scripts;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Editor.Scripts
{
    public partial class AddNewBrickView : PhoneApplicationPage
    {
        private readonly AddNewScriptBrickViewModel _viewModel = ServiceLocator.Current.GetInstance<AddNewScriptBrickViewModel>();

        public AddNewBrickView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
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