using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModel;
using Catrobat.IDE.Core.ViewModel.Main;
using Microsoft.Phone.Controls;
using ViewModelBase = Catrobat.IDE.Core.ViewModel.ViewModelBase;

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
            string projectName = null;
            if (NavigationContext.QueryString.ContainsKey("ProjectName"))
                projectName = NavigationContext.QueryString["ProjectName"];

            string fileToken;
            NavigationContext.QueryString.TryGetValue("fileToken", out fileToken);

            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                //Core.App.SetNativeApp(new AppPhone());
                await Core.App.Initialize();

                ManualImageCache.NoScreenshotImage = new BitmapImage(new Uri("Content/Images/Screenshot/NoScreenshot.png", UriKind.Relative))
                {
                    CreateOptions = BitmapCreateOptions.None
                };

                ManualImageCache.NoScreenshotImageSquare = new BitmapImage(new Uri("Content/Images/Screenshot/NoScreenshotSquare.png", UriKind.Relative))
                {
                    CreateOptions = BitmapCreateOptions.None
                };
            }

            if (projectName != null)
            {
                await ServiceLocator.PlayerLauncherService.LaunchPlayer(projectName, true);
                //((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri("Views/Main/PlayerLauncherView#ProjectName=", UriKind.Relative));
                //ServiceLocator.NavigationService.NavigateTo<PlayerLauncherViewModel>();
            }

            if (fileToken != null)
            {
                var viewModel = ((ViewModelLocator)ServiceLocator.ViewModelLocator).ProjectImportViewModel;
                viewModel.OnLoadCommand.Execute(fileToken);
                ServiceLocator.NavigationService.NavigateTo<ProjectImportViewModel>();
            }
            else
                ServiceLocator.NavigationService.NavigateTo<MainViewModel>();
        }
    }
}