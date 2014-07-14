using Windows.Storage;
using Windows.UI.Popups;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.WindowsPhone.Controls.CatrobatSplashScreen;
using Catrobat.IDE.WindowsShared.Common;

namespace Catrobat.IDE
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : Application
    {
        /// <summary>
        /// Initializes the singleton Application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected async override void OnLaunched(LaunchActivatedEventArgs e)
        {
            StatusBar statusBar = Windows.UI.ViewManagement.StatusBar.GetForCurrentView();
            await statusBar.HideAsync();

            if (e.PreviousExecutionState != ApplicationExecutionState.Running)
            {
                var extendedSplash = new ExtendedSplash(e.SplashScreen, e.PreviousExecutionState);
                Window.Current.Content = extendedSplash;
            }

            Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }









        protected override async void OnFileActivated(FileActivatedEventArgs e)
        {
            try
            {
                var file = (StorageFile)e.Files[0];

                // todo navigate to picture chooser UI

                //var content = await FileIO.ReadTextAsync(file);

                //var localFile = await ApplicationData.Current.TemporaryFolder.CreateFileAsync(
                //    MainPage.CatrobatImageFileName, CreationCollisionOption.ReplaceExisting);
                //await FileIO.WriteTextAsync(localFile, content);
            }
            catch (Exception exc)
            {
                var messageDialog1 = new MessageDialog("Cannot read recieved file: " + exc.Message);
                messageDialog1.ShowAsync();
            }
        }
    }
}








