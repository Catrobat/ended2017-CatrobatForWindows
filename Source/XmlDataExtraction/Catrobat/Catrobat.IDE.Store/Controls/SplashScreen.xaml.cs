using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Store.Common;
using Catrobat.IDE.Store.Services;
using ViewModelBase = GalaSoft.MvvmLight.ViewModelBase;

namespace Catrobat.IDE.Store.Controls
{
    public sealed partial class SplashScreen : Page
    {
        public static ApplicationExecutionState PreviousExecutionState { get; set; }

        public SplashScreen()
        {
            this.InitializeComponent();
            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (ViewModelBase.IsInDesignModeStatic)
                return;

            if (PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                await SuspensionManager.RestoreAsync();
            }

            if (PreviousExecutionState == ApplicationExecutionState.NotRunning ||
                PreviousExecutionState == ApplicationExecutionState.ClosedByUser ||
                PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                Core.App.SetNativeApp(Application.Current.Resources["App"] as AppStore);
                await Core.App.Initialize();
                ServiceLocator.Register(new DispatcherServiceStore(Dispatcher));

                var width = ServiceLocator.SystemInformationService.ScreenWidth; // preload width
                var height = ServiceLocator.SystemInformationService.ScreenHeight; // preload height

                var image = new BitmapImage(new Uri("ms-appx:///Content/Images/Screenshot/NoScreenshot.png", UriKind.Absolute))
                {
                    CreateOptions = BitmapCreateOptions.None
                };

                ManualImageCache.NoScreenshotImage = image;

                ServiceLocator.NavigationService = new NavigationServiceStore(Frame);
            }

            await Task.Delay(1000);

            ServiceLocator.NavigationService.NavigateTo(typeof(MainViewModel));
        }
    }
}
