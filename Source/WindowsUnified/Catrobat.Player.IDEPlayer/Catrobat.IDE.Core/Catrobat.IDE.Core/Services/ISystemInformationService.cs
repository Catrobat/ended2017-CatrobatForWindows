using Catrobat.IDE.Core.UI.PortableUI;

namespace Catrobat.IDE.Core.Services
{
    public interface ISystemInformationService
    {
        string PlatformName { get; }

        string PlatformVersion { get; }

        string DeviceName { get; }

        int ScreenWidth { get; }

        int ScreenHeight { get; }

        string CurrentApplicationVersion { get; }

        string CurrentApplicationBuildName { get; }

        int CurrentApplicationBulidNumber { get; }

        PortableSolidColorBrush AccentBrush { get; }
    }
}
