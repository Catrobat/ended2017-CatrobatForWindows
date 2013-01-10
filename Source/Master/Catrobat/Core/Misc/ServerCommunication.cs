using System;
using System.Net;
using System.IO;
using Catrobat.Core.Misc.Helpers;
using Catrobat.Core.Misc.JSON;
using Catrobat.Core.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Catrobat.Core.Storage;
using Catrobat.Core.ZIP;

namespace Catrobat.Core.Misc
{
  // TODO: undo comment and fix issues below
  public class ServerCommunication
  {
    public delegate void RegisterOrCheckTokenEvent(bool registered);
    public delegate void CheckTokenEvent(bool registered);
    public delegate void LoadOnlineProjectsEvent(List<OnlineProjectHeader> projects, bool append);
    public delegate void DownloadAndSaveProjectEvent(string filename);
    public delegate void UploadProjectEvent(bool successful);

    private static int UploadCounter = 0;
    private static int DownloadCounter = 0;

    public static bool NoUploadsPending()
    {
      return UploadCounter == 0;
    }

    public static bool NoDownloadsPending()
    {
      return DownloadCounter == 0;
    }

    public static void registerOrCheckToken(string username, string password, string userEmail, string language, string country, string token,
      RegisterOrCheckTokenEvent callback)
    {
      //// Generate post objects
      //Dictionary<string, object> postParameters = new Dictionary<string, object>();
      //postParameters.Add(ApplicationResources.REG_USER_NAME, username);
      //postParameters.Add(ApplicationResources.REG_USER_PASSWORD, password);
      //postParameters.Add(ApplicationResources.REG_USER_EMAIL, userEmail);
      //postParameters.Add(ApplicationResources.TOKEN, token);

      //if (country != null)
      //{
      //  postParameters.Add(ApplicationResources.REG_USER_COUNTRY, country);
      //}

      //if (language != null)
      //{
      //  postParameters.Add(ApplicationResources.REG_USER_LANGUAGE, language);
      //}

      //WebRequest request = FormUpload.MultipartFormDataPost(ApplicationResources.CheckTokenOrRegisterUrl,
      //  ApplicationResources.UserAgent, postParameters, (string a) =>
      //  {
      //    if (callback != null)
      //    {
      //      JSON.JSONStatusResponse response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
      //      if (response.StatusCode == StatusCodes.SERVER_RESPONSE_TOKEN_OK)
      //      {
      //        callback(false);
      //      }
      //      else if (response.StatusCode == StatusCodes.SERVER_RESPONSE_REGISTER_OK)
      //      {
      //        callback(true);
      //      }
      //      else
      //      {
      //        // TODO: Error Handling?
      //      }
      //    }
      //  });
    }

    public static void checkToken(string token, CheckTokenEvent callback)
    {
      //// Generate post objects
      //Dictionary<string, object> postParameters = new Dictionary<string, object>();
      //postParameters.Add(ApplicationResources.TOKEN, token);

      //WebRequest request = FormUpload.MultipartFormDataPost(ApplicationResources.CheckTokenUrl,
      //  ApplicationResources.UserAgent, postParameters, (string a) =>
      //  {
      //    if (callback != null)
      //    {
      //      JSON.JSONStatusResponse response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
      //      if (response.StatusCode == StatusCodes.SERVER_RESPONSE_TOKEN_OK)
      //      {
      //        callback(true);
      //      }
      //      else
      //      {
      //        // TODO: Error Handling?
      //        callback(false);
      //      }
      //    }
      //  });
    }

    private static DateTime ConvertUnixTimeStamp(double timestamp)
    {
      DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
      return origin.AddSeconds(timestamp);
    }

    public static void LoadOnlineProjects(bool append, string filterText, int offset, LoadOnlineProjectsEvent callback)
    {
      //String tempUrl = "";

      //if (filterText == "")
      //{
      //  // Recent projects
      //  if (append)
      //  {
      //    tempUrl = String.Format(MetroCatIDE.Content.Resources.ApplicationResources.OnlineRecentUrl, offset);
      //  }
      //  else
      //  {
      //    tempUrl = String.Format(MetroCatIDE.Content.Resources.ApplicationResources.OnlineRecentUrl, 0);
      //  }
      //}
      //else
      //{
      //  if (append)
      //  {
      //    tempUrl = String.Format(MetroCatIDE.Content.Resources.ApplicationResources.OnlineFilterUrl, offset, HttpUtility.UrlEncode(filterText));
      //  }
      //  else
      //  {
      //    tempUrl = String.Format(MetroCatIDE.Content.Resources.ApplicationResources.OnlineFilterUrl, 0, HttpUtility.UrlEncode(filterText));
      //  }
      //}

      //WebClient client = new WebClient();
      //client.OpenReadAsync(new Uri(tempUrl));
      //client.OpenReadCompleted += new OpenReadCompletedEventHandler(
      //    delegate(object sender, OpenReadCompletedEventArgs e)
      //    {
      //      if (e.Error == null)
      //      {
      //        XDocument xdoc = XDocument.Load(e.Result);

      //        List<OnlineProjectHeader> list = (from item in xdoc.Descendants("CatrobatProject")
      //                                          select new OnlineProjectHeader()
      //                                          {
      //                                            ProjectName = item.Element("ProjectName").Value,
      //                                            Author = item.Element("Author").Value,
      //                                            Description = item.Element("Description").Value,
      //                                            Uploaded = ConvertUnixTimeStamp(FormatHelper.ParseDouble(item.Element("Uploaded").Value)),
      //                                            Version = item.Element("Version").Value,
      //                                            Views = FormatHelper.ParseInt(item.Element("Views").Value),
      //                                            Downloads = FormatHelper.ParseInt(item.Element("Downloads").Value),
      //                                            ScreenshotSmallUrl = item.Element("ScreenshotSmall").Value,
      //                                            ScreenshotBigUrl = item.Element("ScreenshotBig").Value,
      //                                            ProjectUrl = item.Element("ProjectUrl").Value,
      //                                            DownloadUrl = item.Element("DownloadUrl").Value
      //                                          }).ToList();
      //        if (callback != null)
      //        {
      //          callback(list, append);
      //        }
      //      }
      //    });
    }

    public static void downloadAndSaveProject(string downloadUrl, DownloadAndSaveProjectEvent callback)
    {
      //WebClient wc = new WebClient();
      //wc.OpenReadCompleted += ((s, args) =>
      //{
      //  string filename = System.IO.Path.GetFileNameWithoutExtension(downloadUrl);
      //  List<string> folders;
      //  using (IStorage storeage = StorageSystem.GetStorage())
      //  {
      //    folders = storeage.GetDirectoryNames(CatrobatContext.ProjectsPath + "/*").ToList<string>();
      //  }
      //  string countString = "";
      //  int counter = 1;
      //  while (folders.IndexOf(filename + countString) >= 0)
      //  {
      //    countString = " " + counter++.ToString();
      //  }
      //  filename = filename + countString;
      //  CatrobatZip.UnzipCatrobatPackageIntoIsolatedStorage(args.Result, CatrobatContext.ProjectsPath + "/" + filename);

      //  DownloadCounter--;

      //  if (callback != null)
      //  {
      //    callback(filename);
      //  }
      //});
      //DownloadCounter++;
      //wc.OpenReadAsync(new System.Uri(downloadUrl, System.UriKind.RelativeOrAbsolute));
    }

    public static void uploadProject(string projectName, string projectDescription, string userEmail,
      string language, string token, UploadProjectEvent callback)
    {
      //// Generate post objects
      //Dictionary<string, object> postParameters = new Dictionary<string, object>();
      //postParameters.Add(ApplicationResources.PROJECT_NAME_TAG, projectName);
      //postParameters.Add(ApplicationResources.PROJECT_DESCRIPTION_TAG, projectDescription);
      //postParameters.Add(ApplicationResources.USER_EMAIL, userEmail);
      //postParameters.Add(ApplicationResources.TOKEN, token);

      //using (MemoryStream stream = new MemoryStream())
      //{
      //  CatrobatZip.ZipCatrobatPackage(stream, CatrobatContext.ProjectsPath + "/" + projectName);
      //  byte[] data = stream.ToArray();

      //  postParameters.Add(ApplicationResources.PROJECT_CHECKSUM_TAG, Utils.toHex(MD5Core.GetHash(data)));

      //  if (language != null)
      //  {
      //    postParameters.Add(ApplicationResources.USER_LANGUAGE, language);
      //  }

      //  postParameters.Add(ApplicationResources.FILE_UPLOAD_TAG, 
      //    new MetroCatIDE.Misc.Helpers.FormUpload.FileParameter(data, 
      //      projectName + ApplicationResources.EXTENSION, ApplicationResources.MIMETYPE));

      //  UploadCounter++;

      //  WebRequest request = FormUpload.MultipartFormDataPost(ApplicationResources.UploadFileUrl,
      //    ApplicationResources.UserAgent, postParameters, (string a) =>
      //    {
      //      UploadCounter--;

      //      if (callback != null)
      //      {
      //        JSON.JSONStatusResponse response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
      //        callback(response.StatusCode == StatusCodes.SERVER_RESPONSE_TOKEN_OK);
      //      }
      //    });
      //}
    }
  }
}
