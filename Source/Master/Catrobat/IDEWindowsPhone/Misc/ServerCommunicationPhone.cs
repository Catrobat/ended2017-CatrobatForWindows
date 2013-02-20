using System.Net;
using System.Xml.Linq;
using Catrobat.Core;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Objects;
using Catrobat.Core.Resources;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;
using System;
using System.Collections.Generic;
using System.Linq;
using Catrobat.Core.Misc.ServerCommunication;

namespace Catrobat.IDEWindowsPhone.Misc
{
  public class ServerCommunicationPhone : IServerCommunication
  {
    public void LoadOnlineProjects(bool append, string filterText, int offset, ServerCommunication.LoadOnlineProjectsEvent callback)
    {
      var tempUrl = "";

      if (filterText == "")
      {
        // Recent projects
        tempUrl = String.Format(ApplicationResources.OnlineRecentUrl, append ? offset : 0);
      }
      else
      {
        tempUrl = String.Format(ApplicationResources.OnlineFilterUrl, append ? offset : 0, HttpUtility.UrlEncode(filterText));
      }

      WebClient client = new WebClient();
      client.OpenReadAsync(new Uri(tempUrl));
      client.OpenReadCompleted += new OpenReadCompletedEventHandler(
          delegate(object sender, OpenReadCompletedEventArgs e)
          {
            if (e.Error != null) return;

            XDocument xdoc = XDocument.Load(e.Result);

            List<OnlineProjectHeader> list = (from item in xdoc.Descendants("CatrobatProject")
                                              select new OnlineProjectHeader()
                                              {
                                                ProjectName = item.Element("ProjectName").Value,
                                                Author = item.Element("Author").Value,
                                                Description = item.Element("Description").Value,
                                                Uploaded = ServerCommunication.ConvertUnixTimeStamp(FormatHelper.ParseDouble(item.Element("Uploaded").Value)),
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
              callback(list, append);
            }
          });
      
    }

    public int DownloadAndSaveProject(string downloadUrl, ServerCommunication.DownloadAndSaveProjectEvent callback)
    {
      int[] downloadCounterChange = {0};

      WebClient wc = new WebClient();
      wc.OpenReadCompleted += ((s, args) =>
      {
        string filename = System.IO.Path.GetFileNameWithoutExtension(downloadUrl);
        List<string> folders;
        using (IStorage storeage = StorageSystem.GetStorage())
        {
          folders = storeage.GetDirectoryNames(CatrobatContext.ProjectsPath + "/*").ToList<string>();
        }
        string countString = "";
        int counter = 1;
        while (folders.IndexOf(filename + countString) >= 0)
        {
          countString = " " + counter++.ToString();
        }
        filename = filename + countString;
        CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(args.Result, CatrobatContext.ProjectsPath + "/" + filename);

        downloadCounterChange[0]--;

        if (callback != null)
        {
          callback(filename);
        }
      });
      downloadCounterChange[0]++;
      wc.OpenReadAsync(new System.Uri(downloadUrl, System.UriKind.RelativeOrAbsolute));

      return downloadCounterChange[0];
    }
  }
}
