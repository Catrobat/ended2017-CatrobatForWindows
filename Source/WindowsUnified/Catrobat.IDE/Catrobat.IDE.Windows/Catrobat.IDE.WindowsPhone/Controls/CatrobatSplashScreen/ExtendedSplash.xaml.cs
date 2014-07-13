using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.UI;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.WindowsPhone.Common;
using Catrobat.IDE.WindowsPhone.Views.Main;
using Catrobat.IDE.WindowsShared;
using Catrobat.IDE.WindowsShared.Services;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace Catrobat.IDE.WindowsPhone.Controls.CatrobatSplashScreen
{
    partial class ExtendedSplash
    {
        private Rect _splashImageRect; // Rect to store splash screen image coordinates.
        private bool _dismissed = false; // Variable to track splash screen dismissal status.
        private readonly Frame _rootFrame;

        private readonly SplashScreen _splash; // Variable to hold the splash screen object.

        public ExtendedSplash(SplashScreen splashscreen, ApplicationExecutionState executionState)
        {
            InitializeComponent();

            //LearnMoreButton.Click += new RoutedEventHandler(LearnMoreButton_Click);
            // Listen for window resize events to reposition the extended splash screen image accordingly.
            // This is important to ensure that the extended splash screen is formatted properly in response to snapping, unsnapping, rotation, etc...
            Window.Current.SizeChanged += new WindowSizeChangedEventHandler(ExtendedSplash_OnResize);

            _splash = splashscreen;

            if (_splash != null)
            {
                // Register an event handler to be executed when the splash screen has been dismissed.
                _splash.Dismissed += new TypedEventHandler<SplashScreen, Object>(DismissedEventHandler);

                // Retrieve the window coordinates of the splash screen image.
                _splashImageRect = _splash.ImageLocation;
                PositionImage();
            }

            // Create a Frame to act as the navigation context
            _rootFrame = new Frame();

            // Restore the saved session state if necessary
            RestoreStateAsync(executionState);
        }

        async void RestoreStateAsync(ApplicationExecutionState executionState)
        {
            if (executionState == ApplicationExecutionState.Terminated)
                await SuspensionManager.RestoreAsync();

            await RestoreCatrobatStateAsync(executionState);
        }

        // Position the extended splash screen image in the same location as the system splash screen image.
        void PositionImage()
        {
            ExtendedSplashImage.SetValue(Viewbox.HeightProperty, _splashImageRect.Height);
            ExtendedSplashImage.SetValue(Viewbox.WidthProperty, _splashImageRect.Width);
        }

        void ExtendedSplash_OnResize(Object sender, WindowSizeChangedEventArgs e)
        {
            // Safely update the extended splash screen image coordinates. This function will be fired in response to snapping, unsnapping, rotation, etc...
            if (_splash != null)
            {
                // Update the coordinates of the splash screen image.
                _splashImageRect = _splash.ImageLocation;
                PositionImage();
            }
        }

        // Include code to be executed when the system has transitioned from the splash screen to the extended splash screen (application's first view).
        void DismissedEventHandler(SplashScreen sender, object e)
        {
            _dismissed = true;

            // Navigate away from the app's extended splash screen after completing setup operations here...
            // This sample navigates away from the extended splash screen when the "Learn More" button is clicked.
        }






        private async Task RestoreCatrobatStateAsync(ApplicationExecutionState executionState)
        {
            if (ViewModelBase.IsInDesignModeStatic)
                return;

            if (executionState == ApplicationExecutionState.NotRunning || 
                executionState == ApplicationExecutionState.ClosedByUser || 
                executionState == ApplicationExecutionState.Terminated)
            {
                Core.App.SetNativeApp(Application.Current.Resources["App"]
                    as AppWindowsShared);
                await Core.App.Initialize();
                ServiceLocator.Register(new DispatcherServiceStore(Dispatcher));

                var width = ServiceLocator.SystemInformationService.ScreenWidth; // preload width
                var height = ServiceLocator.SystemInformationService.ScreenHeight; // preload height

                var image = new BitmapImage(new Uri("ms-appx:///Content/Images/Screenshot/NoScreenshot.png", UriKind.Absolute))
                {
                    CreateOptions = BitmapCreateOptions.None
                };

                ManualImageCache.NoScreenshotImage = image;

                Window.Current.Content = _rootFrame;
                ServiceLocator.NavigationService = new NavigationServiceStore(_rootFrame);
            }

            _rootFrame.Navigate(typeof(MainView));
        }
    }
}
