using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public interface IPlayerLauncherService
    {
        Task LaunchPlayer(Program project, bool isLaunchedFromTile = false);

        Task LaunchPlayer(string projectName, bool isLaunchedFromTile = false);

        void RestartProgramAction();

        void ResumeProgramAction();

        void SetThumbnailAction();

        void AxisAction();
         
        void TakeScreenshotAction();

        bool HardwareBackButtonPressed();
    }
}
