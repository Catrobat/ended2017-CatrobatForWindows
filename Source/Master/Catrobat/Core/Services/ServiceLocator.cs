using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Services
{
    public class ServiceLocator
    {
        public static INavigationService NavigationService { get; private set; }

        public static ISystemInformationService SystemInformationService { get; private set; }

        public static ICultureService CulureService { get; private set; }

        public static IImageResizeService ImageResizeService { get; private set; }

        public static IPlayerLauncherService PlayerLauncherService { get; private set; }


        public static void SetServices(
            INavigationService navigationService,
            ISystemInformationService systemInformationService,
            ICultureService culureService,
            IImageResizeService imageResizeService,
            IPlayerLauncherService playerLauncherService
            )
        {
            NavigationService = navigationService;
            SystemInformationService = systemInformationService;
            CulureService = culureService;
            ImageResizeService = imageResizeService;
            PlayerLauncherService = playerLauncherService;
        }
    }
}