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
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
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

        private async Task ShowSplashScreen(IActivatedEventArgs e)
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
                e.PreviousExecutionState, splashImage);
            Window.Current.Content = extendedSplash;

            Window.Current.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            var mainViewModel = ServiceLocator.GetInstance<MainViewModel>();
            await Core.App.SaveContext(mainViewModel.CurrentProject);
            await SuspensionManager.SaveAsync();
            deferral.Complete();
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            if (args is FileOpenPickerContinuationEventArgs)
            {
                var pickerArgs = (FileOpenPickerContinuationEventArgs)args;
                var files = pickerArgs.Files;

                if (files.Count == 1)
                {
                    if (ServiceLocator.PictureService.SupportedFileTypes.
                        Contains(Path.GetExtension(files[0].Name)))
                    {
                        ServiceLocator.PictureService.RecievedFiles(
                            (args as FileOpenPickerContinuationEventArgs).Files);
                    }

                    if (ServiceLocator.SoundService.SupportedFileTypes.
                        Contains(Path.GetExtension(files[0].Name)))
                    {
                        ServiceLocator.SoundService.RecievedFiles(
                            (args as FileOpenPickerContinuationEventArgs).Files);
                    }
                }
            }
        }

        protected override async void OnFileActivated(FileActivatedEventArgs e)
        {
            try
            {
                if (e.Files.Count == 1 && Constants.CatrobatFileNames.Contains(
                    Path.GetExtension(e.Files[0].Name)))
                {
                    if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                    {
                        await ShowSplashScreen(e);
                        // TODO: wait for initializatioin and open files
                    }
                    else
                    {
                        var catrobatFileStream = (await ((StorageFile)e.Files[0]).
                        OpenReadAsync()).AsStream();

                        ServiceLocator.ProjectImporterService.SetProjectStream(catrobatFileStream);
                        ServiceLocator.NavigationService.NavigateTo<ProjectImportViewModel>();
                    }
                }

                if (e.PreviousExecutionState != ApplicationExecutionState.Terminated)
                {
                    var imageFiles = (from StorageFile file in e.Files
                                      from imageExtension in
                                          ServiceLocator.PictureService.SupportedFileTypes
                                      where file.Name.EndsWith(
                                      ServiceLocator.PictureService.ImageFileExtensionPrefix + imageExtension)
                                      select file).ToList();

                    if (imageFiles.Count > 0)
                        ServiceLocator.PictureService.RecievedFiles(imageFiles);
                }
            }
            catch (Exception exc)
            {
                // TODO: Handle error
                //var messageDialog1 = new MessageDialog("Cannot read recieved file: " + exc.Message);
                //messageDialog1.ShowAsync();
            }
        }
    }
}








