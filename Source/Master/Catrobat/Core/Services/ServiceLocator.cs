using Catrobat.Core.Misc.Helpers;

namespace Catrobat.Core.Services
{
    public class ServiceLocator
    {
        public static INavigationService NavigationService { get; private set; }

        public static ISystemInformationService SystemInformationService { get; private set; }

        public static ICultureService CulureService { get; private set; }

        public static void SetServices(
            INavigationService navigationService,
            ISystemInformationService systemInformationService,
            ICultureService culureService
            )
        {
            NavigationService = navigationService;
            SystemInformationService = systemInformationService;
            CulureService = culureService;
        }
    }
}