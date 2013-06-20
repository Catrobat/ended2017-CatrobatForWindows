using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using Catrobat.Core;
using Catrobat.IDEWindowsPhone.Misc;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Windows.Phone.Storage.SharedAccess;
using Windows.Storage;
using Catrobat.IDEWindowsPhone.ViewModel.Main;
using Microsoft.Practices.ServiceLocation;

namespace Catrobat.IDEWindowsPhone.Views.Main
{
    public partial class ProjectImportView : PhoneApplicationPage
    {
        private readonly ProjectImportViewModel _viewModel = ServiceLocator.Current.GetInstance<ProjectImportViewModel>();

        public ProjectImportView()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            _viewModel.ResetViewModel();

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            _viewModel.OnLoadCommand.Execute(NavigationContext);
        }
    }
}