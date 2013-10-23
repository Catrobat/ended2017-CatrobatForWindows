using System;
using System.IO;
using Windows.UI.Xaml;
using Catrobat.IDE.Core;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
using Catrobat.IDE.Core.VersionConverter;

namespace Catrobat.IDE.Store.Services
{
    public class PlayerLauncherServicePhone :IPlayerLauncherService
    {
        public void LaunchPlayer(Project project)
        {
            //var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + project.ProjectHeader.ProgramName;
            //((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
        }
    }
}
