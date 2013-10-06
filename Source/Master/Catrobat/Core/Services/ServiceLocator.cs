using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.Storage;

namespace Catrobat.Core.Services
{
    public class ServiceLocator
    {
        public static INavigationService NavigationService { get; private set; }

        public static ISystemInformationService SystemInformationService { get; private set; }

        public static ICultureService CulureService { get; private set; }

        public static IImageResizeService ImageResizeService { get; private set; }

        public static IPlayerLauncherService PlayerLauncherService { get; private set; }

        public static IResourceLoaderFactory ResourceLoaderFactory { get; private set; }

        public static IStorageFactory StorageFactory { get; set; }

        public static IServerCommunicationService ServerCommunicationService { get; private set; }

        public static void SetServices(
            INavigationService navigationService,
            ISystemInformationService systemInformationService,
            ICultureService culureService,
            IImageResizeService imageResizeService,
            IPlayerLauncherService playerLauncherService,
            IResourceLoaderFactory resourceLoaderFactory,
            IStorageFactory storageFactory,
            IServerCommunicationService serverCommunicationService
            )
        {
            NavigationService = navigationService;
            SystemInformationService = systemInformationService;
            CulureService = culureService;
            ImageResizeService = imageResizeService;
            PlayerLauncherService = playerLauncherService;
            ResourceLoaderFactory = resourceLoaderFactory;
            StorageFactory = storageFactory;
            ServerCommunicationService = serverCommunicationService;
        }
    }
}