using System.Threading.Tasks;
using Catrobat.IDE.Core.Models;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.ViewModels;
using Catrobat.IDE.Core.ViewModels.Main;
using GalaSoft.MvvmLight.Messaging;

namespace Catrobat.IDE.Phone.Services
{
    public class PlayerLauncherServicePhone : IPlayerLauncherService
    {
        public async Task LaunchPlayer(Project project, bool isLaunchedFromTile)
        {
            await project.Save();
            await LaunchPlayer(project.Name, isLaunchedFromTile);
        }

        // ReSharper disable once CSharpWarnings::CS1998
        public async Task LaunchPlayer(string projectName, bool isLaunchedFromTile)
        {
            var messageProjectName = new GenericMessage<string>(projectName);
            Messenger.Default.Send(messageProjectName, ViewModelMessagingToken.PlayProjectNameListener);

            var messageIsStartFromTile = new GenericMessage<bool>(isLaunchedFromTile);
            Messenger.Default.Send(messageIsStartFromTile, ViewModelMessagingToken.IsPlayerStartFromTileListener);

            ServiceLocator.NavigationService.NavigateTo<PlayerLauncherViewModel>();
        }
    }
}
