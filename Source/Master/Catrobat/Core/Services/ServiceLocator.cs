namespace Catrobat.Core.Services
{
    public class ServiceLocator
    {
        public static INavigationService NavigationService { get; private set; }

        public static void SetServices(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
    }
}