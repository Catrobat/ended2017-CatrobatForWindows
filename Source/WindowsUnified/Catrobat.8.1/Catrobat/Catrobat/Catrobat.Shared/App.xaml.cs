using System.Threading.Tasks;
using Windows.Storage;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.ViewModels.Service;
using Catrobat.IDE.WindowsPhone;
using Catrobat.IDE.WindowsShared.Common;

namespace Catrobat.IDE.WindowsShared
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.UnhandledException += OnUnhandledException;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            ServiceLocator.TraceService.Add(TraceType.Info, "Application launched");

            await ShowSplashScreen(e);
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            ServiceLocator.TraceService.Add(TraceType.Info, "Application suspending");

            var deferral = e.SuspendingOperation.GetDeferral();
            var mainViewModel = ServiceLocator.GetInstance<MainViewModel>();
            await Core.App.SaveContext(mainViewModel.CurrentProgram);
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        private async Task Activated(IActivatedEventArgs e)
        {
            ServiceLocator.TraceService.Add(TraceType.Info, "Application Activated",
                "PreviousState = " + e.PreviousExecutionState);

            switch (e.PreviousExecutionState)
            {
                case ApplicationExecutionState.NotRunning:
                    await ShowSplashScreen(e);
                    break;
                case ApplicationExecutionState.Running:
                    await SkipSplashScreen(e);
                    break;
                case ApplicationExecutionState.Suspended:
                    await SkipSplashScreen(e);
                    break;
                case ApplicationExecutionState.Terminated:
                    await ShowSplashScreen(e);
                    break;
                case ApplicationExecutionState.ClosedByUser:
                    await ShowSplashScreen(e);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private static async Task ShowSplashScreen(IActivatedEventArgs e)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();

            var file = await StorageFile.GetFileFromApplicationUriAsync(
                new Uri("ms-appx:///Assets/SplashScreen.png", UriKind.Absolute));
            var randomAccessStream = await file.OpenReadAsync();

            var splashImage = new BitmapImage()
            {
                CreateOptions = BitmapCreateOptions.None
            };
            await splashImage.SetSourceAsync(randomAccessStream);


            var extendedSplash = new ExtendedSplash(e.SplashScreen,
                e, splashImage);
            Window.Current.Content = extendedSplash;

            Window.Current.Activate();
        }


        private static async Task SkipSplashScreen(IActivatedEventArgs e)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();

            await ExtendedSplash.InitializationFinished(e);
            if (e.Kind == ActivationKind.Protocol)
            {
                ServiceLocator.NavigationService.NavigateTo<UploadProgramNewPasswordViewModel>();
            }
        }

        protected override async void OnActivated(IActivatedEventArgs e)
        {
            await Activated(e);
        }

        protected override async void OnFileActivated(FileActivatedEventArgs e)
        {
            await Activated(e);
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            string stackTrace = null;
            if (e.Exception != null)
                stackTrace = e.Exception.StackTrace;

            ServiceLocator.TraceService.Add(TraceType.Error, "Application crashed",
                e.Message, stackTrace);
            ServiceLocator.TraceService.SaveLocal();
        }
    }
}
