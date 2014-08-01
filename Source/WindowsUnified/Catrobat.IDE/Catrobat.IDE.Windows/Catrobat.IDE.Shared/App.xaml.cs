using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Looks;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.WindowsPhone;
using Catrobat.IDE.WindowsPhone.Views.Main;
using Catrobat.IDE.WindowsShared.Common;
using GalaSoft.MvvmLight.Threading;

namespace Catrobat.IDE.WindowsShared
{
    sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            StatusBar statusBar = StatusBar.GetForCurrentView();
            await statusBar.HideAsync();

            await ShowSplashScreen(e);
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            var mainViewModel = ServiceLocator.GetInstance<MainViewModel>();
            await Core.App.SaveContext(mainViewModel.CurrentProgram);
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        private async Task Activated(IActivatedEventArgs e)
        {
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
            await ExtendedSplash.InitializationFinished(e);
        }

        protected override async void OnActivated(IActivatedEventArgs e)
        {
            await Activated(e);
        }

        protected override async void OnFileActivated(FileActivatedEventArgs e)
        {
            await Activated(e);
        }
    }
}