using System;
using System.IO;
using System.Windows;
using Catrobat.Core;
using Catrobat.Core.CatrobatObjects;
using Catrobat.Core.Utilities.Storage;
using Catrobat.Core.Services;
using Catrobat.Core.VersionConverter;
using Microsoft.Phone.Controls;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class PlayerLauncherServicePhone :IPlayerLauncherService
    {
        public void LaunchPlayer(Project project)
        {
            //CatrobatVersionConverter.VersionConverterError error;

            //var tempProjectName = projectName + "PlayerTemp";
            //var sourcePath = Path.Combine(CatrobatContextBase.ProjectsPath, projectName);
            //var destinationPath = Path.Combine(CatrobatContextBase.ProjectsPath, tempProjectName);

            //using (var storage = StorageSystem.GetStorage())
            //{
            //    storage.DeleteDirectory(destinationPath);
            //    storage.CopyDirectory(sourcePath, destinationPath);
            //    //storage.DeleteFile(Path.Combine(destinationPath, Project.ProjectCodePath));
            //    //storage.WriteTextFile(Path.Combine(destinationPath, Project.ProjectCodePath), );

            //    CatrobatVersionConverter.ConvertToXmlVersionByProjectName(tempProjectName,
            //        CatrobatVersionConfig.TargetPlayerVersion, out error, true);
            //}

            //if (error == CatrobatVersionConverter.VersionConverterError.NoError)
            //{
            //    var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + tempProjectName;
            //    ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
            //}
            //else
            //{
            //   // TODO: show error?
            //}

            project.Save();

            var navigationUri = "/Views/Main/PlayerLauncherView.xaml?ProjectName=" + project.ProjectHeader.ProgramName;
            ((PhoneApplicationFrame)Application.Current.RootVisual).Navigate(new Uri(navigationUri, UriKind.Relative));
        }
    }
}
