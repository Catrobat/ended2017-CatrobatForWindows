using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using Windows.Storage;
using Windows.System;
using Catrobat.IDEWindowsPhone.Views.Main;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class PlayerLauncher
    {
        public static void LaunchPlayer(String projectName)
        {
            var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + projectName;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
        }
    }
}
