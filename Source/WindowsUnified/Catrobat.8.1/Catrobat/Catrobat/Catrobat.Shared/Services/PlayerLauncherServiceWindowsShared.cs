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
        private const string TempProgramName = "TempProject.catrobat_play";
        private static Catrobat_PlayerAdapter PlayerObject = null;
        private static Frame pageFrame = null;

        public async Task LaunchPlayer(Core.Models.Program program, bool isLaunchedFromTile)
        {
            await ShowSplashScreen(program.Name);
            
            var zipService = new ZipService();
            var tempFolder = ApplicationData.Current.TemporaryFolder;
            var file = await tempFolder.CreateFileAsync(TempProgramName, 
                                                        CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenStreamForWriteAsync();

            await zipService.ZipCatrobatPackage(stream, program.BasePath);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            //await project.Save(); ??? TODO: this was in the previous version of catrobat --> do we need to save the project at this point?
            await LaunchPlayer(program.Name, isLaunchedFromTile);
            // TODO: manage closing/relaunching of the Player 
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

        public async Task LaunchPlayer(string programName, bool isLaunchedFromTile)
        {
            var messageProjectName = new GenericMessage<string>(programName);
            Messenger.Default.Send(messageProjectName, 
                                   ViewModelMessagingToken.PlayerProgramNameListener);

            var messageIsStartFromTile = new GenericMessage<bool>(isLaunchedFromTile);
            Messenger.Default.Send(messageIsStartFromTile, 
                                   ViewModelMessagingToken.PlayerIsStartFromTileListener);

            Window.Current.Content = pageFrame;
            ServiceLocator.NavigationService.NavigateTo<PlayerViewModel>();
        }

        private async Task ShowSplashScreen(string programName)
        {
            // TODO check: very similar method in App.xaml.cs --> put it to some global 
            //      space where it can be accessed from both classes?!?

            // save the current Frame
            pageFrame = (Frame)Window.Current.Content;

            BitmapImage programScreenshot = await GetProgramScreenshot(programName);
            Window.Current.Content = new ExtendedSplash(programScreenshot, 0.5); 

            Window.Current.Activate();
        }

        private async Task<BitmapImage> GetProgramScreenshot(string programName)
        {
            // TODO check: introduce global constant for "ms-appx:///Assets/SplashScreen.png" or "ms-appdata:///Local/"?
            // TODO: review

            var programFolder = "ms-appdata:///Local/" + StorageConstants.ProgramsPath 
                                + "/" + programName + "/";
            StorageFile file = null;


            // try to get manual_screenshot
            var manualScreenshotPath = programFolder + StorageConstants.ProgramManualScreenshotPath;

            try
            {
                file = await StorageFile.GetFileFromApplicationUriAsync(
                        new Uri(manualScreenshotPath, UriKind.Absolute));
            }
            catch (FileNotFoundException e)
            {
                file = null;
            }

            // if the manual screenshot is not available, try to get automatic_screenshot
            if (file == null)
            {
                var automaticScreenshotPath = programFolder 
                                                + StorageConstants.ProgramAutomaticScreenshotPath;
                try
                {
                    file = await StorageFile.GetFileFromApplicationUriAsync(
                            new Uri(automaticScreenshotPath, UriKind.Absolute));
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

            var programScreenshot = new BitmapImage()
            {
                CreateOptions = BitmapCreateOptions.None
            };
            await programScreenshot.SetSourceAsync(await file.OpenReadAsync());

            return programScreenshot;
        }

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

        public void AxesAction(bool showAxes, string label)
        {
            if (PlayerObject != null)
            {
                PlayerObject.AxesButtonClicked(showAxes, label);
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
