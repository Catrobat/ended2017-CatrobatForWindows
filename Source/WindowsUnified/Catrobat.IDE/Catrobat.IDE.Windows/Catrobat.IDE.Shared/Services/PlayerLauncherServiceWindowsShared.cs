using System;
using System.IO;
using System.Threading.Tasks;

using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

using GalaSoft.MvvmLight.Messaging;

using Catrobat_Player;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat.IDE.WindowsPhone;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class PlayerLauncherServiceWindowsShared : IPlayerLauncherService
    {
        private const string TempProjectName = "TempProject.catrobat_play";
        private static Catrobat_PlayerAdapter PlayerObject = null;
        private static Frame pageFrame = null;

        public async Task LaunchPlayer(Core.Models.Program project, bool isLaunchedFromTile)
        {
            await ShowSplashScreen(project.Name);
            
            var zipService = new ZipService();
            var tempFolder = ApplicationData.Current.TemporaryFolder;
            var file = await tempFolder.CreateFileAsync(TempProjectName, CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenStreamForWriteAsync();

            await zipService.ZipCatrobatPackage(stream, project.BasePath);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            //await project.Save(); ??? TODO: this was in the previous version of catrobat --> do we need to save the project at this point?
            await LaunchPlayer(project.Name, isLaunchedFromTile);
            // TODO: review ...LaunchFileAsync (1 line underneath) --> seems to be that it never finishes
            //bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            //if (success)
            //{
            //    // File launch success
            //}
            //else
            //{
            //    // File launch failed
            //}
        }

        public async Task LaunchPlayer(string projectName, bool isLaunchedFromTile)
        {
            var messageProjectName = new GenericMessage<string>(projectName);
            Messenger.Default.Send(messageProjectName, ViewModelMessagingToken.PlayerProjectNameListener);

            var messageIsStartFromTile = new GenericMessage<bool>(isLaunchedFromTile);
            Messenger.Default.Send(messageIsStartFromTile, ViewModelMessagingToken.PlayerIsStartFromTileListener);

            Window.Current.Content = pageFrame;
            ServiceLocator.NavigationService.NavigateTo<PlayerViewModel>();
        }

        private async Task ShowSplashScreen(string projectName)
        {
            // TODO use project's thumbnail/screenshot as SplashScreen image
            // TODO duplicate of this method (again in App.xaml.cs) --> put it to some global 
            //      space where it can be accessed from both classes?!?

            pageFrame = (Frame)Window.Current.Content;
            var splashImage = await GetThumbnailFromProject(projectName);
            Window.Current.Content = new ExtendedSplash(splashImage);
            Window.Current.Activate();
        }

        private async Task<BitmapImage> GetThumbnailFromProject(string projectName)
        {
            // TODO check: global constant for "ms-appx:///Assets/SplashScreen.png" or "ms-appdata:///Local/"?

            var projectFolder = "ms-appdata:///Local/" + StorageConstants.ProgramsPath + 
                                "/" + projectName + "/";
            StorageFile file;

            // TODO: problem with getting manual_screenshot.png, even if the file is present...and it is possible to access automatic_screenshot..
            // firstly, try to get manual_screenshot
            var manualScreenshotPath = projectFolder + StorageConstants.ProgramManualScreenshotPath;
            try
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri(manualScreenshotPath, UriKind.Absolute));
            }
            catch (FileNotFoundException e)
            {
                file = null;
            }

            // secondly, if the manual_screenshot is not available, try to get automatic_screenshot
            if (file == null)
            {
                var automaticProjectScreenshotPath = projectFolder + 
                                                    StorageConstants.ProgramAutomaticScreenshotPath;
                try
                {
                    file = await StorageFile.GetFileFromApplicationUriAsync(
                            new Uri(automaticProjectScreenshotPath, UriKind.Absolute));
                }
                catch (FileNotFoundException e)
                {
                    file = null;
                }
            }

            // if neither of them is available --> get default SplashScreen image
            if (file == null)
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri("ms-appx:///Assets/SplashScreen.png", UriKind.Absolute));
            }


            var randomAccessStream = await file.OpenReadAsync();
            var splashImage = new BitmapImage()
            {
                CreateOptions = BitmapCreateOptions.None
            };
            await splashImage.SetSourceAsync(randomAccessStream);

            return splashImage;
        }

        // here is an old code for getting the screenshot/thumbnail out of the
        /*private async void SetSourceOfThumbnail()
       {
           var projectFolder = "ms-appdata:///Local/" + StorageConstants.ProgramsPath + "/" + _viewModel.ProjectName + "/";

           StorageFile file = null;
           try
           {
               var manualScreenshotPath = projectFolder + StorageConstants.ProgramManualScreenshotPath;
               file = await StorageFile.GetFileFromApplicationUriAsync(
                       new Uri(manualScreenshotPath, UriKind.Absolute));
           } 
           catch (Exception e)
           {
               file = null;                
           }

           if (file == null)
           {
               try
               {
                   var automaticProjectScreenshotPath = projectFolder + StorageConstants.ProgramAutomaticScreenshotPath;
                   file = await StorageFile.GetFileFromApplicationUriAsync(
                           new Uri(automaticProjectScreenshotPath, UriKind.Absolute));
               }
               catch (Exception e)
               {
                   file = null;
               }
           }

           
       } */

        public static void SetPlayerObject(Catrobat_PlayerAdapter playerObject)
        {
            PlayerObject = playerObject;
        }

        public void RestartProgramAction()
        {
            if (PlayerObject != null)
            {
                PlayerObject.RestartButtonClicked();
            }
        }

        public void ResumeProgramAction()
        {
            if (PlayerObject != null)
            {
                PlayerObject.ResumeButtonClicked();
            }
        }

        public void SetThumbnailAction()
        {
            if (PlayerObject != null)
            {
                PlayerObject.ThumbnailButtonClicked();
            }
        }

        public void AxisAction()
        {
            if (PlayerObject != null)
            {
                PlayerObject.AxisButtonClicked();
            }
        }

        public void TakeScreenshotAction()
        {
            if (PlayerObject != null)
            {
                PlayerObject.ScreenshotButtonClicked();
            }
        }

        public bool HardwareBackButtonPressed()
        {
            if (PlayerObject != null)
            {
                return PlayerObject.HardwareBackButtonPressed();
            }

            return true;
        }
    }
}
