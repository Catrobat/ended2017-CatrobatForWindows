using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using Microsoft.Phone.Controls;

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
                var viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).ProjectImportViewModel;
                viewModel.OnLoadCommand.Execute(fileToken);
                Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof(ProjectImportViewModel));
            }
            else
                Core.Services.ServiceLocator.NavigationService.NavigateTo(typeof(MainViewModel));
        }
    }
}