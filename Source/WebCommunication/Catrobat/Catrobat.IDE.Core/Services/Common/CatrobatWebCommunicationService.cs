using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using Catrobat.IDE.Core.VersionConverter;

// TODO by Phil
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

using System.Diagnostics;


namespace Catrobat.IDE.Core.Services.Common
{
    public static class CatrobatWebCommunicationService
    {
        public delegate void RegisterOrCheckTokenEvent(bool registered, string errorCode, string statusMessage);

        public delegate void CheckTokenEvent(bool registered);

        public delegate void LoadOnlineProjectsEvent(string filterText, List<OnlineProjectHeader> projects, bool append);

        public delegate void DownloadAndSaveProjectEvent(string filename, CatrobatVersionConverter.VersionConverterError error);

        public delegate void UploadProjectEvent(bool successful);

        private static int _uploadCounter = 0;

        public static bool NoUploadsPending()
        {
            return _uploadCounter == 0;
        }

        public static void RegisterOrCheckToken(string username,
                                                string password,
                                                string userEmail,
                                                string language,
                                                string country,
                                                string token,
                                                RegisterOrCheckTokenEvent callback)
        {
            // Generate post objects
            var postParameters = new Dictionary<string, object>
            {
                {ApplicationResources.REG_USER_NAME, username},
                {ApplicationResources.REG_USER_PASSWORD, password},
                {ApplicationResources.REG_USER_EMAIL, userEmail},
                {ApplicationResources.TOKEN, token}
            };

            if (country != null)
            {
                postParameters.Add(ApplicationResources.REG_USER_COUNTRY, country);
            }

            if (language != null)
            {
                postParameters.Add(ApplicationResources.REG_USER_LANGUAGE, language);
            }

            WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.CheckTokenOrRegisterUrl,
                            ApplicationResources.UserAgent,
                            postParameters,
                            a =>
                                {
                                    if (callback != null)
                                    {
                                        var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
                                        if (response.StatusCode == StatusCodes.ServerResponseTokenOk)
                                        {
                                            callback(false, response.StatusCode.ToString(), response.StatusMessage);
                                        }
                                        else if (response.StatusCode == StatusCodes.ServerResponseRegisterOk)
                                        {
                                            callback(true, response.StatusCode.ToString(), response.StatusMessage);
                                        }
                                        else
                                        {
                                            callback(false, response.StatusCode.ToString(), response.StatusMessage);
                                        }
                                    }
                                });
        }

        public static void CheckToken(string token, CheckTokenEvent callback)
        {
            // Generate post objects
            var postParameters = new Dictionary<string, object> {{ApplicationResources.TOKEN, token}};

            WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.CheckTokenUrl,
                                ApplicationResources.UserAgent,
                                postParameters,
                                a =>
                                    {
                                        if (callback != null)
                                        {
                                            var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
                                            callback(response.StatusCode == StatusCodes.ServerResponseTokenOk);
                                        }
                                    });
        }

        public static DateTime ConvertUnixTimeStamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }

        public static async void LoadOnlineProjects(bool append, string filterText, int offset, LoadOnlineProjectsEvent callback)
        {
            //ServiceLocator.ServerCommunicationService.LoadOnlineProjects(append, filterText, offset, callback);
            await AsyncLoadOnlineProjects(append, filterText, offset, callback);
        }

        // by Phil - müsste evt keine eigener Task sein einfach eine async methode --> nur Task wenn JSON-processing auch lange braucht
        public static async Task AsyncLoadOnlineProjects(bool append, string filterText, int offset, LoadOnlineProjectsEvent callback)
        {
            Debug.WriteLine("Starting with HTTPClient!");
            using (var http_client = new HttpClient())
            {
                // TODO move links to ApplicationResources base URL
                http_client.BaseAddress = new Uri("https://pocketcode.org/api/");
                http_client.DefaultRequestHeaders.Accept.Clear();
                http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage http_response = null;

                // TODO adaption limit for request
                if (filterText == "")
                {
                    // TODO move links to ApplicationResources.OnlineRecentUrl
                    http_response = await http_client.GetAsync("projects/recent.json?limit=3");
                    //HttpResponseMessage http_response = await http_client.GetAsync("projects/getInfoById.json?id=1618");
                }
                else
                {
                    // TODO move links to ApplicationResources.OnlineFilterUrl
                    string encoded_filter_text = WebUtility.UrlEncode(filterText);
                    http_response = await http_client.GetAsync("projects/search.json?query=" + encoded_filter_text + "&limit=3");
                }
                
                if(http_response.IsSuccessStatusCode)
                {
                    // also async processing of the response
                    string json_result = await http_response.Content.ReadAsStringAsync();
                    OnlineProjectOverview recent_projects = null;

                    Debug.WriteLine(json_result);
                    try
                    {
                        //List<OnlineProjectOverview> projects = JsonConvert.DeserializeObject<List<OnlineProjectOverview>>(json_result);
                        recent_projects = JsonConvert.DeserializeObject<OnlineProjectOverview>(json_result);
                    }
                    catch(Newtonsoft.Json.JsonSerializationException e)
                    {
                        //TODO Message to do on this error
                        Debug.WriteLine(e.Message);
                    }

                    if (callback != null)
                    {
                        callback(filterText, recent_projects.CatrobatProjects, append);
                    }
                }
            }
        }

        public static void DownloadAndSaveProject(string downloadUrl, string projectName, DownloadAndSaveProjectEvent callback)
        {
            ServiceLocator.ServerCommunicationService.DownloadAndSaveProject(downloadUrl, projectName, callback);
        }

        public static void UploadProject(string projectName, string projectDescription, string userEmail,
                                         string language, string token, UploadProjectEvent callback)
        {
            // Generate post objects
            var postParameters = new Dictionary<string, object>
            {
                {ApplicationResources.PROJECT_NAME_TAG, projectName},
                {ApplicationResources.PROJECT_DESCRIPTION_TAG, projectDescription},
                {ApplicationResources.USER_EMAIL, userEmail},
                {ApplicationResources.TOKEN, token}
            };

            using (var stream = new MemoryStream())
            {
                CatrobatZipService.ZipCatrobatPackage(stream, CatrobatContextBase.ProjectsPath + "/" + projectName);
                var data = stream.ToArray();

                postParameters.Add(ApplicationResources.PROJECT_CHECKSUM_TAG, UtilTokenHelper.ToHex(MD5Core.GetHash(data)));

                if (language != null)
                {
                    postParameters.Add(ApplicationResources.USER_LANGUAGE, language);
                }

                postParameters.Add(ApplicationResources.FILE_UPLOAD_TAG,
                                   new CatrobatWebFormUploadService.FileParameter(data,
                                                                projectName + ApplicationResources.EXTENSION, ApplicationResources.MIMETYPE));

                _uploadCounter++;

                WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.UploadFileUrl,
                                                                      ApplicationResources.UserAgent, postParameters, 
                                                                      a =>
                                                                          {
                                                                              _uploadCounter--;

                                                                              if (callback != null)
                                                                              {
                                                                                  var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
                                                                                  callback(response.StatusCode == StatusCodes.ServerResponseTokenOk);
                                                                              }
                                                                          });
            }
        }
    }
}