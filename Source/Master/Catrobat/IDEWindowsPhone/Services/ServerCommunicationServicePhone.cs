using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Catrobat.Core;
using Catrobat.Core.Misc.Storage;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.Core.Services;
using Catrobat.Core.Services.Common;
using Catrobat.Core.VersionConverter;

namespace Catrobat.IDEWindowsPhone.Services
{
    public class ServerCommunicationServicePhone : IServerCommunicationService
    {
        public void LoadOnlineProjects(bool append, string filterText, int offset,
                                       CatrobatWebCommunicationService.LoadOnlineProjectsEvent callback)
        {
            var tempUrl = "";

            if (filterText == "")
            {
                // Recent projects
                tempUrl = String.Format(ApplicationResources.OnlineRecentUrl, append ? offset : 0);
            }
            else
            {
                tempUrl = String.Format(ApplicationResources.OnlineFilterUrl, append ? offset : 0,
                                        HttpUtility.UrlEncode(filterText));
            }

            var client = new WebClient();
            client.OpenReadAsync(new Uri(tempUrl));
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(
                delegate(object sender, OpenReadCompletedEventArgs e)
                    {
                        if (e.Error != null)
                        {
                            return;
                        }

                        var xdoc = XDocument.Load(e.Result);

                        var list = (from item in xdoc.Descendants("CatrobatProject")
                                    select new OnlineProjectHeader()
                                    {
                                        //TODO: Exception handling
                                        ProjectName = item.Element("ProjectName").Value,
                                        Author = item.Element("Author").Value,
                                        Description = item.Element("Description").Value,
                                        Uploaded =
                                            CatrobatWebCommunicationService.ConvertUnixTimeStamp(
                                                StringFormatHelper.ParseDouble(item.Element("Uploaded").Value)),
                                        Version = item.Element("Version").Value,
                                        Views = StringFormatHelper.ParseInt(item.Element("Views").Value),
                                        Downloads = StringFormatHelper.ParseInt(item.Element("Downloads").Value),
                                        ScreenshotSmallUrl = ApplicationResources.OnlineImagesBaseUrl + item.Element("ScreenshotSmall").Value,
                                        ScreenshotBigUrl = ApplicationResources.OnlineImagesBaseUrl + item.Element("ScreenshotBig").Value,
                                        ProjectUrl = ApplicationResources.OnlineImagesBaseUrl + item.Element("ProjectUrl").Value,
                                        DownloadUrl = ApplicationResources.OnlineImagesBaseUrl + item.Element("DownloadUrl").Value
                                    }).ToList();
                        if (callback != null)
                        {
                            callback(filterText, list, append);
                        }
                    });
        }

        public void DownloadAndSaveProject(string downloadUrl, string projectName,
                                          CatrobatWebCommunicationService.DownloadAndSaveProjectEvent callback)
        {
            var wc = new WebClient();
            wc.OpenReadCompleted += ((s, args) =>
                {
                    try
                    {
                        //string filename = System.IO.Path.GetFileNameWithoutExtension(downloadUrl);
                        List<string> folders;
                        using (var storeage = StorageSystem.GetStorage())
                        {
                            folders = storeage.GetDirectoryNames(CatrobatContextBase.ProjectsPath).ToList<string>();
                        }
                        var countString = "";
                        var counter = 1;
                        while (folders.IndexOf(projectName + countString) >= 0)
                        {
                            countString = " " + counter++.ToString(CultureInfo.InvariantCulture);
                        }
                        projectName = projectName + countString;


                        CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(args.Result,
                                                                            CatrobatContextBase.ProjectsPath + "/" +
                                                                            projectName);

                        var error = CatrobatVersionConverter.VersionConverterError.NoError;
                        CatrobatVersionConverter.ConvertToXmlVersionByProjectName(projectName, CatrobatVersionConfig.TargetIDEVersion, out error, true);

                        if (callback != null) //TODO
                        {
                            callback(projectName, error);
                        }
                    }
                    catch (Exception)
                    {
                        if (callback != null)
                        {
                            callback("", CatrobatVersionConverter.VersionConverterError.ProjectCodeNotValid);
                        }
                    }
                });

            wc.OpenReadAsync(new Uri(downloadUrl, UriKind.RelativeOrAbsolute));
        }
    }
}