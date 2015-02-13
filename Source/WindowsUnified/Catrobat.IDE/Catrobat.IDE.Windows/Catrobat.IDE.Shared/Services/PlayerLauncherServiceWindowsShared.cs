using GalaSoft.MvvmLight.Messaging;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;
using Catrobat_Player;


namespace Catrobat.IDE.WindowsShared.Services
{
    public class PlayerLauncherServiceWindowsShared : IPlayerLauncherService
    {
        private const string TempProjectName = "TempProject.catrobat_play";
        private static Catrobat_PlayerAdapter PlayerObject = null;

        public async Task LaunchPlayer(Core.Models.Program project, bool isLaunchedFromTile)
        {
            var zipService = new ZipService();

            var tempFolder = ApplicationData.Current.TemporaryFolder;
            var file = await tempFolder.CreateFileAsync(TempProjectName, CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenStreamForWriteAsync();

            await zipService.ZipCatrobatPackage(stream, project.BasePath);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            //await project.Save(); ??? TODO: this was in the previous version of catrobat --> do we need to save the project at this point?
            await LaunchPlayer(project.Name, isLaunchedFromTile);
            // TODO: review ...LaunchFileAsync --> seems to be that it never finishes
            //bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            //if (success)
            //{
            //    // File launch success
            //}
            //else
            //{
            //    // File launch failed
            //}

            bool test;
        }

        public async Task LaunchPlayer(string projectName, bool isLaunchedFromTile)
        {
            // TODO: review

            var messageProjectName = new GenericMessage<string>(projectName);
            Messenger.Default.Send(messageProjectName, ViewModelMessagingToken.PlayerProjectNameListener);

            var messageIsStartFromTile = new GenericMessage<bool>(isLaunchedFromTile);
            Messenger.Default.Send(messageIsStartFromTile, ViewModelMessagingToken.PlayerIsStartFromTileListener);

            ServiceLocator.NavigationService.NavigateTo<PlayerViewModel>();
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
