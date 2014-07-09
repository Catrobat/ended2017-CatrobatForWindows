using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.WindowsPhone.Common;
using Catrobat.IDE.WindowsShared;
using Catrobat.IDE.WindowsShared.Services;

namespace Catrobat.IDE.WindowsPhone.Controls.SplashScreen
{
    public partial class SplashScreen : Page
    {
        public static ApplicationExecutionState PreviousExecutionState { get; set; }

        public SplashScreen()
        {
            InitializeComponent();
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
                Core.App.SetNativeApp(Application.Current.Resources["App"] as AppWindowsShared);
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