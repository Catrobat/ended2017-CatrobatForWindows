using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using Catrobat.Core;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.ServerCommunication;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.IDEWindowsPhone.Misc
{
    public class ServerCommunicationPhone : IServerCommunication
    {
        public void LoadOnlineProjects(bool append, string filterText, int offset,
                                       ServerCommunication.LoadOnlineProjectsEvent callback)
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
                                            ServerCommunication.ConvertUnixTimeStamp(
                                                FormatHelper.ParseDouble(item.Element("Uploaded").Value)),
                                        Version = item.Element("Version").Value,
                                        Views = FormatHelper.ParseInt(item.Element("Views").Value),
                                        Downloads = FormatHelper.ParseInt(item.Element("Downloads").Value),
                                        ScreenshotSmallUrl = item.Element("ScreenshotSmall").Value,
                                        ScreenshotBigUrl = item.Element("ScreenshotBig").Value,
                                        ProjectUrl = item.Element("ProjectUrl").Value,
                                        DownloadUrl = item.Element("DownloadUrl").Value
                                    }).ToList();
                        if (callback != null)
                        {
                            callback(filterText, list, append);
                        }
                    });
        }

        public int DownloadAndSaveProject(string downloadUrl, string projectName,
                                          ServerCommunication.DownloadAndSaveProjectEvent callback)
        {
            int[] downloadCounterChange = {0};

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
                            countString = " " + counter++.ToString();
                        }
                        projectName = projectName + countString;


                        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(args.Result,
                                                                            CatrobatContextBase.ProjectsPath + "/" +
                                                                            projectName);

                        downloadCounterChange[0]--;

                        if (callback != null)
                        {
                            callback(projectName);
                        }
                    }
                    catch (Exception)
                    {
                        if (callback != null)
                        {
                            callback("");
                        }
                    }
                });

            downloadCounterChange[0]++;
            wc.OpenReadAsync(new Uri(downloadUrl, UriKind.RelativeOrAbsolute));

            return downloadCounterChange[0];
        }
    }
}