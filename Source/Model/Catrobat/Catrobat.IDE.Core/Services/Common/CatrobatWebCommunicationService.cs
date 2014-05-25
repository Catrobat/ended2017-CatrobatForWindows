using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Catrobat.IDE.Core.Utilities;
using Catrobat.IDE.Core.Utilities.Helpers;
using Catrobat.IDE.Core.Utilities.JSON;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Resources;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Newtonsoft.Json;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;
//using System.Diagnostics;


namespace Catrobat.IDE.Core.Services.Common
{
    public static class CatrobatWebCommunicationService
    {
        private static int _uploadCounter = 0;

        public static async Task<List<OnlineProjectHeader>> AsyncLoadOnlineProjects(bool append, string filterText, int offset)
        {
            // TODO exception handling
            using (var http_client = new HttpClient())
            {
                //http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                http_client.BaseAddress = new Uri("https://pocketcode.org/api/");
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
                    string json_result = await http_response.Content.ReadAsStringAsync();
                    OnlineProjectOverview recent_projects = null;

                    try
        {
                        //List<OnlineProjectOverview> projects = JsonConvert.DeserializeObject<List<OnlineProjectOverview>>(json_result);
                        recent_projects = JsonConvert.DeserializeObject<OnlineProjectOverview>(json_result);
        }
                    catch (Newtonsoft.Json.JsonSerializationException)
                    {
                        //TODO Message to do on this error
                        //Debug.WriteLine(e.Message);
                    }
                    return recent_projects.CatrobatProjects;
                }
                // TODO HTTP Request failed-error
                return null;
            }
        }


        public static async Task<CatrobatVersionConverter.VersionConverterError> AsyncDownloadAndSaveProject(string downloadUrl, string projectName)
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
                            countString = " " + counter.ToString(ServiceLocator.CultureService.GetCulture());
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
                        return error;
                    }
                    catch (Exception)
                    {
                        return CatrobatVersionConverter.VersionConverterError.ProjectCodeNotValid;
                    }
                }
                // TODO HTTP Request failed-error
                return CatrobatVersionConverter.VersionConverterError.ProjectCodePathNotValid;
            }
        }


        public static async Task<bool> AsyncCheckToken(string username, string token)
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
            using (var http_client = new HttpClient())
                                {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_CHECK_TOKEN, post_parameters);
                
                if (http_response.IsSuccessStatusCode)
                                    {
                    string json_result = await http_response.Content.ReadAsStringAsync();
                    JSONStatusResponse status_response = null;

                    try
                                        {
                        status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                                        }
                    catch (Newtonsoft.Json.JsonSerializationException)
                                        {
                        //TODO Message to do on this error
                        //Debug.WriteLine(e.Message);
                                        }
                    return (status_response.statusCode == StatusCodes.ServerResponseTokenOk);
                                        }
                // TODO HTTP Request failed-error
                return false;
                                    }

        }

        public static async Task<JSONStatusResponse> AsyncLoginOrRegister(string username, string password, string userEmail, 
                string language, string country)
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
                    string json_result = await http_response.Content.ReadAsStringAsync();

                    JSONStatusResponse status_response = null;
                    try
        {
                        status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
        }
                    catch (Newtonsoft.Json.JsonSerializationException)
        {
                        //TODO Message to do on this error
                        //Debug.WriteLine(e.Message);
                    }
                    return status_response;
                }
                // TODO HTTP Request failed-error
                return null;
            }
        }


        public static async Task<JSONStatusResponse> AsyncUploadProject(string projectTitle, string projectDescription, string username,
                string language, string token)
            {
            // TODO exception handling
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_USERNAME, username),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_TOKEN, token),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, language)
            };

            using (MultipartFormDataContent post_parameters = new MultipartFormDataContent())
            {
                using (MemoryStream stream = new MemoryStream())
            {
                    await CatrobatZipService.ZipCatrobatPackage(stream, CatrobatContextBase.ProjectsPath + "/" + projectTitle);
                    Byte[] project_data = stream.ToArray();

                    parameters.Add(new KeyValuePair<string, string>(ApplicationResources.API_PARAM_CHECKSUM, UtilTokenHelper.ToHex(MD5Core.GetHash(project_data))));

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
                    file_content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/zip");
                    post_parameters.Add(file_content, String.Format("\"{0}\"", ApplicationResources.API_PARAM_UPLOAD), String.Format("\"{0}\"", projectTitle + ApplicationResources.EXTENSION));


                _uploadCounter++;
                    using (var http_client = new HttpClient())
                    {
                        http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                        HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_UPLOAD, post_parameters);

                        if (http_response.IsSuccessStatusCode)
                        {
                            string json_result = await http_response.Content.ReadAsStringAsync();

                            JSONStatusResponse status_response = null;
                            try
                            {
                                status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                            }
                            catch (Newtonsoft.Json.JsonSerializationException)
                                                                          {
                                //TODO Message to do on this error
                                //Debug.WriteLine(e.Message);
                            }
                            return status_response;
                        }

                                                                              _uploadCounter--;
                        // TODO HTTP Request failed-error
                        return null;
                    }
                }
            }


        }

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