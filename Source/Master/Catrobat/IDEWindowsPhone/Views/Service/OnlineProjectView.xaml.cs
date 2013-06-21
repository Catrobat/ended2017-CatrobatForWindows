using System;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.IDECommon.Resources.Main;
using Catrobat.IDEWindowsPhone.Misc;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Globalization;
using Catrobat.Core.Resources;
using Microsoft.Practices.ServiceLocation;
using System.Windows.Navigation;
using Catrobat.IDEWindowsPhone.ViewModel.Service;

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
            _viewModel.OnLoadCommand.Execute((OnlineProjectHeader)DataContext);
        }
    }
}