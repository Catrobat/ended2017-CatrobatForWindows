using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;

namespace Catrobat.IDE.Store.Services
{
    public class PlayerLauncherServiceStore :IPlayerLauncherService
    {
        public void LaunchPlayer(Project project)
        {
            //var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + project.ProjectHeader.ProgramName;
            //((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
        }
    }
}
