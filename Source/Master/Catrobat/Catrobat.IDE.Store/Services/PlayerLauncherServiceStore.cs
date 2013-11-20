using System;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModel.Main;

namespace Catrobat.IDE.Store.Services
{
    public class PlayerLauncherServiceStore : IPlayerLauncherService
    {
        // ReSharper disable once CSharpWarnings::CS1998
        public async Task LaunchPlayer(Project project, bool isLaunchedFromTile)
        {
            ServiceLocator.NavigationService.NavigateTo<PlayerLauncherViewModel>();
        }

        // ReSharper disable once CSharpWarnings::CS1998
        public async Task LaunchPlayer(string projectName, bool isLaunchedFromTile)
        {
            ServiceLocator.NavigationService.NavigateTo<PlayerLauncherViewModel>();
        }
    }
}
