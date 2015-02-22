using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;

namespace Catrobat.IDE.Core.Services
{
    public interface IPlayerLauncherService
    {
        Task LaunchPlayer(Program program, bool isLaunchedFromTile = false);

        Task LaunchPlayer(string programName, bool isLaunchedFromTile = false);

        void RestartProgramAction();

        void ResumeProgramAction();

        void SetThumbnailAction();

        void AxisAction();
         
        void TakeScreenshotAction();

        bool HardwareBackButtonPressed();
    }
}
