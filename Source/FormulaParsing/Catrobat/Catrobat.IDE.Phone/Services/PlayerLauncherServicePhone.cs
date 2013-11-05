using System;
using System.Windows;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Services;
using Microsoft.Phone.Controls;

namespace Catrobat.IDE.Phone.Services
{
    public class PlayerLauncherServicePhone :IPlayerLauncherService
    {
        public void LaunchPlayer(Project project)
        {
            project.Save();

            var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + project.ProjectHeader.ProgramName;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
        }
    }
}
