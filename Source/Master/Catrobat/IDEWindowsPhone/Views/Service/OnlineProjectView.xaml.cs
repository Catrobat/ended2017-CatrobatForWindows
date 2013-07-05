using System.Windows;
using System.Windows.Navigation;
using Catrobat.Core.Objects;
using Catrobat.IDEWindowsPhone.ViewModel.Service;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Service
{
    public partial class OnlineProjectView : PhoneApplicationPage
    {
        private readonly OnlineProjectViewModel _viewModel = ServiceLocator.Current.GetInstance<OnlineProjectViewModel>();

        public OnlineProjectView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModelCommand.Execute(null);
            base.OnNavigatedFrom(e);
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            _viewModel.OnLoadCommand.Execute((OnlineProjectHeader) DataContext);
        }
    }
}