using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Common;

namespace Catrobat.IDE.WindowsShared.Services
{
    public class PlayerLauncherServiceWindowsShared : IPlayerLauncherService
    {
        private const string TempProjectName = "TempProject.catrobat_play";

        public async Task LaunchPlayer(Project project, bool isLaunchedFromTile)
        {
            var tempFolder = ApplicationData.Current.TemporaryFolder;
            var file = await tempFolder.CreateFileAsync(TempProjectName, CreationCollisionOption.ReplaceExisting);
            var stream = await file.OpenStreamForWriteAsync();

            await CatrobatZipService.ZipCatrobatPackage(stream, project.BasePath);

            var options = new Windows.System.LauncherOptions { DisplayApplicationPicker = false };

            bool success = await Windows.System.Launcher.LaunchFileAsync(file, options);
            if (success)
            {
                // File launch success
            }
            else
            {
                // File launch failed
            }
        }

        public async Task LaunchPlayer(string projectName, bool isLaunchedFromTile)
        {
            // TODO: replace with

        }
    }
}
