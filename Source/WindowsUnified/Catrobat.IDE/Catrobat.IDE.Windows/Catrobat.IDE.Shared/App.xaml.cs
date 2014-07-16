using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Storage;
using Windows.UI.Popups;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels.Editor.Costumes;
using Catrobat.IDE.Core.ViewModels.Main;
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
        }

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
                var pickerArgs = (FileOpenPickerContinuationEventArgs) args;
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
                if (e.PreviousExecutionState != ApplicationExecutionState.Terminated)
                {
                    var imageFiles = (from StorageFile file in e.Files
                                      from imageExtension in
                                          ServiceLocator.PictureService.SupportedFileTypes
                                      where file.Name.EndsWith(
                                      ServiceLocator.PictureService.ImageFileExtensionPrefix + imageExtension)
                                      select file).ToList();

                    ServiceLocator.PictureService.RecievedFiles(imageFiles);
                }

                if (e.Files.Count == 1 &&
                    Constants.CatrobatFileNames.Contains(Path.GetExtension(e.Files[0].Name)))
                {
                    // TODO: send message to ProjectImportViewModel that includes the new zip file

                    ServiceLocator.NavigationService.NavigateTo<ProjectImportViewModel>();
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








