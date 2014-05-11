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
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;

using System.Text;
using System.Diagnostics;


namespace Catrobat.IDE.Core.Services.Common
{
    public static class CatrobatWebCommunicationService
    {
        public delegate void RegisterOrCheckTokenEvent(bool registered, string errorCode, string statusMessage, string token);

        public delegate void CheckTokenEvent(bool registered);

        public delegate void LoadOnlineProjectsEvent(string filterText, List<OnlineProjectHeader> projects, bool append);

        public delegate void DownloadAndSaveProjectEvent(string filename, CatrobatVersionConverter.VersionConverterError error);

        public delegate void UploadProjectEvent(bool successful);

        private static int _uploadCounter = 0;

        public static async void LoadOnlineProjects(bool append, string filterText, int offset, LoadOnlineProjectsEvent callback)
        {
            //ServiceLocator.ServerCommunicationService.LoadOnlineProjects(append, filterText, offset, callback);
            await AsyncLoadOnlineProjects(append, filterText, offset, callback);
        }

        public static async Task AsyncLoadOnlineProjects(bool append, string filterText, int offset, LoadOnlineProjectsEvent callback)
        {
            // TODO exception handling
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                //http_client.DefaultRequestHeaders.Accept.Clear();
                //http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage http_response = null;

                if (filterText == "")
                {
                    http_response = await http_client.GetAsync(String.Format(ApplicationResources.API_RECENT_PROJECTS, ApplicationResources.API_REQUEST_LIMIT, offset));
                }
                else
                {
                    string encoded_filter_text = WebUtility.UrlEncode(filterText);
                    http_response = await http_client.GetAsync(String.Format(ApplicationResources.API_SEARCH_PROJECTS, encoded_filter_text, ApplicationResources.API_REQUEST_LIMIT, offset));
                }

                if (http_response.IsSuccessStatusCode)
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
                    catch (Newtonsoft.Json.JsonSerializationException e)
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

        public static async void DownloadAndSaveProject(string downloadUrl, string projectName, DownloadAndSaveProjectEvent callback)
        {
            // ServiceLocator.ServerCommunicationService.DownloadAndSaveProject(downloadUrl, projectName, callback);
            await AsyncDownloadAndSaveProject(downloadUrl, projectName, callback);
        }

        public static async Task AsyncDownloadAndSaveProject(string downloadUrl, string projectName, DownloadAndSaveProjectEvent callback)
        {
            // TODO exception handling

            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS);

                // trigger to header-read to avoid timeouts
                HttpResponseMessage http_response = await http_client.GetAsync(downloadUrl/*, HttpCompletionOption.ResponseHeadersRead*/);
                
                if (http_response.IsSuccessStatusCode)
                {
                    using (Stream http_stream = await http_response.Content.ReadAsStreamAsync())
                    {
                        List<string> folders;
                        using (var storage = StorageSystem.GetStorage())
                        {
                            var folder_array = await storage.GetDirectoryNamesAsync(CatrobatContextBase.ProjectsPath);
                            folders = new List<string>(folder_array);

                        }
                        var countString = "";
                        var counter = 1;
                        while (folders.IndexOf(projectName + countString) >= 0)
                        {
                            countString = " " + counter.ToString(ServiceLocator.CulureService.GetCulture());
                            counter++;
                        }
                        projectName = projectName + countString;
                        await CatrobatZipService.UnzipCatrobatPackageIntoIsolatedStorage(http_stream,
                                                                            CatrobatContextBase.ProjectsPath + "/" +
                                                                            projectName);
                    }

                    try
                    {
                        var result = await CatrobatVersionConverter.ConvertToXmlVersionByProjectName(projectName, Constants.TargetIDEVersion, true);
                        var error = result.Error;

                        if (callback != null)
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
                }
            }


        }


        public static async void CheckToken(string username, string token, CheckTokenEvent callback)
        {
            // Generate post objects
            //var postParameters = new Dictionary<string, object> { { ApplicationResources.TOKEN, token } };

            //WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.CheckTokenUrl,
            //                    ApplicationResources.UserAgent,
            //                    postParameters,
            //                    a =>
            //                    {
            //                        if (callback != null)
            //                        {
            //                            var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
            //                            callback(response.StatusCode == StatusCodes.ServerResponseTokenOk);
            //                        }
            //                    });
            await AsyncCheckToken(username, token, callback);
        }


        public static async Task AsyncCheckToken(string username, string token, CheckTokenEvent callback)
        {
            // TODO exception handling

            if (token == null)
            {
                token = "";
            }
            if (username == null)
            {
                username = "";
            }
            
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_USERNAME, username),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_TOKEN, token)
            };
             
            HttpContent post_parameters = new FormUrlEncodedContent(parameters);

            Debug.WriteLine("Checking Token for user: " + username + " with token: " + token);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_CHECK_TOKEN, post_parameters);
                
                if (http_response.IsSuccessStatusCode)
                {
                    // also async processing of the response
                    string json_result = await http_response.Content.ReadAsStringAsync();

                    JSONStatusResponse status_response = null;

                    Debug.WriteLine(json_result);
                    try
                    {
                        status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                    }
                    catch (Newtonsoft.Json.JsonSerializationException e)
                    {
                        //TODO Message to do on this error
                        Debug.WriteLine(e.Message);
                    }

                    Debug.WriteLine("StatusCode: " + status_response.statusCode.ToString());

                    if (callback != null)
                    {
                        callback(status_response.statusCode == StatusCodes.ServerResponseTokenOk);
                    }
                }
            }

        }


        public static async void LoginOrRegister(string username, string password, string userEmail, 
            string language, string country, string token, RegisterOrCheckTokenEvent callback)
        {
            // Generate post objects
            //var postParameters = new Dictionary<string, object>
            //{
            //    {ApplicationResources.REG_USER_NAME, username},
            //    {ApplicationResources.REG_USER_PASSWORD, password},
            //    {ApplicationResources.REG_USER_EMAIL, userEmail},
            //    {ApplicationResources.TOKEN, token}
            //};

            //if (country != null)
            //{
            //    postParameters.Add(ApplicationResources.REG_USER_COUNTRY, country);
            //}

            //if (language != null)
            //{
            //    postParameters.Add(ApplicationResources.REG_USER_LANGUAGE, language);
            //}

            //WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.CheckTokenOrRegisterUrl,
            //                ApplicationResources.UserAgent,
            //                postParameters,
            //                a =>
            //                    {
            //                        if (callback != null)
            //                        {
            //                            var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
            //                            if (response.StatusCode == StatusCodes.ServerResponseTokenOk)
            //                            {
            //                                callback(false, response.StatusCode.ToString(), response.StatusMessage);
            //                            }
            //                            else if (response.StatusCode == StatusCodes.ServerResponseRegisterOk)
            //                            {
            //                                callback(true, response.StatusCode.ToString(), response.StatusMessage);
            //                            }
            //                            else
            //                            {
            //                                callback(false, response.StatusCode.ToString(), response.StatusMessage);
            //                            }
            //                        }
            //                    });
            await AsyncLoginOrRegister(username, password, userEmail, language, country, token, callback);
        }

        public static async Task AsyncLoginOrRegister(string username, string password, string userEmail, 
                string language, string country, string token, RegisterOrCheckTokenEvent callback)
        {
            // TODO exception handling

            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_USERNAME, username),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_PASSWORD, password),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_EMAIL, userEmail),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_COUNTRY, country),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, language)
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);

            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_LOGIN_REGISTER, post_parameters);

                if (http_response.IsSuccessStatusCode)
                {
                    // also async processing of the response
                    string json_result = await http_response.Content.ReadAsStringAsync();

                    JSONStatusResponse status_response = null;

                    Debug.WriteLine(json_result);
                    try
                    {
                        status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                    }
                    catch (Newtonsoft.Json.JsonSerializationException e)
                    {
                        //TODO Message to do on this error
                        Debug.WriteLine(e.Message);
                    }

                    Debug.WriteLine("Token: " + status_response.token);
                    Debug.WriteLine("StatusCode: " + status_response.statusCode.ToString());

                    if (callback != null)
                    {
                        if (status_response.statusCode == StatusCodes.ServerResponseTokenOk)
                        {
                            callback(false, status_response.statusCode.ToString(), status_response.answer, status_response.token);
                        }
                        else if (status_response.statusCode == StatusCodes.ServerResponseRegisterOk)
                        {
                            callback(true, status_response.statusCode.ToString(), status_response.answer, status_response.token);
                        }
                        else
                        {
                            callback(false, status_response.statusCode.ToString(), status_response.answer, status_response.token);
                        }
                    }
                }
            }
        }


        public static async void UploadProject(string projectName, string projectDescription, string username,
                string language, string token, UploadProjectEvent callback)
        {
            // Generate post objects
            //var postParameters = new Dictionary<string, object>
            //{
            //    {ApplicationResources.PROJECT_NAME_TAG, projectName},
            //    {ApplicationResources.PROJECT_DESCRIPTION_TAG, projectDescription},
            //    {ApplicationResources.USER_EMAIL, userEmail},
            //    {ApplicationResources.TOKEN, token}
            //};

            //using (var stream = new MemoryStream())
            //{
            //    CatrobatZipService.ZipCatrobatPackage(stream, CatrobatContextBase.ProjectsPath + "/" + projectName);
            //    var data = stream.ToArray();

            //    postParameters.Add(ApplicationResources.PROJECT_CHECKSUM_TAG, UtilTokenHelper.ToHex(MD5Core.GetHash(data)));

            //    if (language != null)
            //    {
            //        postParameters.Add(ApplicationResources.USER_LANGUAGE, language);
            //    }

            //    postParameters.Add(ApplicationResources.FILE_UPLOAD_TAG,
            //                       new CatrobatWebFormUploadService.FileParameter(data,
            //                                                    projectName + ApplicationResources.EXTENSION, ApplicationResources.MIMETYPE));

            //    _uploadCounter++;

            //    WebRequest request = CatrobatWebFormUploadService.MultipartFormDataPost(ApplicationResources.UploadFileUrl,
            //                                                          ApplicationResources.UserAgent, postParameters, 
            //                                                          a =>
            //                                                              {
            //                                                                  _uploadCounter--;

            //                                                                  if (callback != null)
            //                                                                  {
            //                                                                      var response = JSONClassDeserializer.Deserialise<JSONStatusResponse>(a);
            //                                                                      callback(response.statusCode == StatusCodes.ServerResponseTokenOk);
            //                                                                  }
            //                                                              });
            //}

            await AsyncUploadProject(projectName, projectDescription, username, language, token, callback);
        }


        public static async Task AsyncUploadProject(string projectTitle, string projectDescription, string username,
                string language, string token, UploadProjectEvent callback)
        {
            // TODO exception handling - e.g use language if set (does it has a effect)
            // TODO remove unused parameters

            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_USERNAME, username),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_TOKEN, token),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, language)
            };


            Debug.WriteLine("Upload!");

            using (MultipartFormDataContent post_parameters = new MultipartFormDataContent())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    await CatrobatZipService.ZipCatrobatPackage(stream, CatrobatContextBase.ProjectsPath + "/" + projectTitle);
                    Byte[] project_data = stream.ToArray();

                    parameters.Add(new KeyValuePair<string, string>(ApplicationResources.API_PARAM_CHECKSUM, UtilTokenHelper.ToHex(MD5Core.GetHash(project_data))));

                    //HttpContent post_parameters = new FormUrlEncodedContent(parameters);

                    // store parameters as MultipartFormDataContent
                    StringContent content = null;
                    foreach (var keyValuePair in parameters)
                    {
                        content = new StringContent(keyValuePair.Value);
                        content.Headers.Remove("Content-Type");
                        post_parameters.Add(content, String.Format("\"{0}\"", keyValuePair.Key));

                    }

                    ByteArrayContent file_content = new ByteArrayContent(project_data);
                    //fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                    //{
                    //    FileName = projectTitle + ".catrobat"
                    //};
                    post_parameters.Add(file_content, String.Format("\"{0}\"", ApplicationResources.API_PARAM_UPLOAD), String.Format("\"{0}\"", projectTitle + ApplicationResources.EXTENSION));


                    // TODO _uploadCounter++ -concern
                    using (var http_client = new HttpClient())
                    {
                        http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                        HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_UPLOAD, post_parameters);

                        if (http_response.IsSuccessStatusCode)
                        {
                            // also async processing of the response
                            string json_result = await http_response.Content.ReadAsStringAsync();

                            JSONStatusResponse status_response = null;

                            Debug.WriteLine(json_result);
                            try
                            {
                                status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                            }
                            catch (Newtonsoft.Json.JsonSerializationException e)
                            {
                                //TODO Message to do on this error
                                Debug.WriteLine(e.Message);
                            }

                            Debug.WriteLine("Token: " + status_response.token);
                            Debug.WriteLine("Answer: " + status_response.answer);

                            if (callback != null)
                            {
                                if (status_response.statusCode == StatusCodes.ServerResponseTokenOk)
                                {
                                    // sucessfully uploaded an new token received
                                    //callback(false, status_response.statusCode.ToString(), status_response.answer, status_response.token);
                                }
                            }
                        }

                        // TODO _uploadCounter--
                    }
                }
            }


        }




        // TODO: additional function maybe displaced
        public static bool NoUploadsPending()
        {
            return _uploadCounter == 0;
        }

        public static DateTime ConvertUnixTimeStamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }
    }
}