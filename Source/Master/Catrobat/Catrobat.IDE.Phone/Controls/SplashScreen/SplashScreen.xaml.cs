using System;
using System.Windows;
using Catrobat.IDE.Phone.ViewModel;
using Catrobat.IDE.Phone.ViewModel.Main;
using Microsoft.Phone.Controls;
using Microsoft.Practices.ServiceLocation;
using Catrobat.IDE.Phone.Views.Main;

namespace Catrobat.IDE.Phone.Controls.SplashScreen
{
    public partial class SplashScreen : PhoneApplicationPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            // init app
            ViewModelLocator.LoadContext();

            string fileToken;
            NavigationContext.QueryString.TryGetValue("fileToken", out fileToken);

            if (fileToken != null)
            {
                var viewModel = ServiceLocator.Current.GetInstance<ProjectImportViewModel>();
                viewModel.OnLoadCommand.Execute(fileToken);
                Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof (ProjectImportView));
            }
            else
                Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof (MainView));
        }
    }
}