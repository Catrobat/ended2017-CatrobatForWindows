using System.Threading.Tasks;
using System.Windows;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using GalaSoft.MvvmLight;
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

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            string fileToken;
            NavigationContext.QueryString.TryGetValue("fileToken", out fileToken);

            await Task.Run(() =>
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    if (!ViewModelBase.IsInDesignModeStatic)
                    {
                        Core.App.SetNativeApp(new AppPhone());
                        Core.App.Initialize();
                    }

                    if (fileToken != null)
                    {
                        var viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).ProjectImportViewModel;
                        viewModel.OnLoadCommand.Execute(fileToken);
                        ServiceLocator.NavigationService.NavigateTo(typeof(ProjectImportViewModel));
                    }
                    else
                        ServiceLocator.NavigationService.NavigateTo(typeof(MainViewModel));

                });
            });

        }
    }
}