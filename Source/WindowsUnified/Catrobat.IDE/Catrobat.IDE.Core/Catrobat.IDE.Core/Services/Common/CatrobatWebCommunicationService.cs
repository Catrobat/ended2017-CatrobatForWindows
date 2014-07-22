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
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Newtonsoft.Json;
using Catrobat.IDE.Core.Services;
using Catrobat.IDE.Core.Services.Storage;

namespace Catrobat.IDE.Core.Services.Common
{
    public static class CatrobatWebCommunicationService // TODO: remove static keyword and make me a real Service!
                                                        // see ServiceLocator
    {
        private static int _uploadCounter = 0;

        public static async Task<List<OnlineProjectHeader>> AsyncLoadOnlineProjects(
            string filterText, int offset, int count,
            CancellationToken taskCancellationToken)
        {
            using (var http_client = new HttpClient())
            {
                //http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                http_client.BaseAddress = new Uri("https://pocketcode.org/api/");
                //https://catroid-test.catrob.at/api/
                //http_client.DefaultRequestHeaders.Accept.Clear();
                //http_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    HttpResponseMessage http_response = null;

                    if (filterText == "")
                    {
                        http_response = await http_client.GetAsync(
                            String.Format(ApplicationResources.API_RECENT_PROJECTS, 
                            count, offset), taskCancellationToken);
                    }
                    else
                    {
                        string encoded_filter_text = WebUtility.UrlEncode(filterText);
                        http_response = await http_client.GetAsync(String.Format(
                            ApplicationResources.API_SEARCH_PROJECTS, encoded_filter_text,
                            count, offset), taskCancellationToken);
                    }
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    OnlineProjectOverview recent_projects = null;

                    //List<OnlineProjectOverview> projects = JsonConvert.DeserializeObject<List<OnlineProjectOverview>>(json_result);
                    recent_projects = await Task.Run(() => JsonConvert.DeserializeObject<OnlineProjectOverview>(json_result));

                    return recent_projects.CatrobatProjects;
                }
                catch (HttpRequestException)
                {
                    return null;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }


        public static async Task<CatrobatVersionConverter.VersionConverterError> AsyncDownloadAndSaveProject(string downloadUrl, string projectName)
        {
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS);
                try
                {
                    // trigger to header-read to avoid timeouts
                    HttpResponseMessage http_response = await http_client.GetAsync(downloadUrl/*, HttpCompletionOption.ResponseHeadersRead*/);
                    http_response.EnsureSuccessStatusCode();

                    using (Stream http_stream = await http_response.Content.ReadAsStreamAsync())
                    {
                        List<string> folders;
                        using (var storage = StorageSystem.GetStorage())
                        {
                            var folder_array = await storage.GetDirectoryNamesAsync(CatrobatContextBase.ProjectsPath);
                            folders = new List<string>(folder_array);

                        }
                        string countString = "";
                        int counter = 1;
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
                    var result = await CatrobatVersionConverter.ConvertToXmlVersionByProjectName(projectName, Constants.TargetIDEVersion, true);
                    CatrobatVersionConverter.VersionConverterError error = result.Error;
                    return error;

                }
                catch (HttpRequestException)
                {
                    return CatrobatVersionConverter.VersionConverterError.ProjectCodePathNotValid;
                }
                catch (Exception)
                {
                    return CatrobatVersionConverter.VersionConverterError.ProjectCodePathNotValid;
                }
            }
        }


        public static async Task<JSONStatusResponse> AsyncCheckToken(string username, string token, string language = "en")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_USERNAME, ((username == null) ? "" : username)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_TOKEN, ((token == null) ? "" : token)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                JSONStatusResponse status_response = null;
                try
                {
                    HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_CHECK_TOKEN, post_parameters);
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                }
                catch (HttpRequestException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.HTTPRequestFailed;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.JSONSerializationFailed;
                }
                catch (Exception)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.UnknownError;
                }
                return status_response;
            }
        }

        public static async Task<JSONStatusResponse> AsyncLoginOrRegister(string username, string password, string userEmail,
                string language = "en", string country = "AT")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_USERNAME, ((username == null) ? "" : username)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_PASSWORD, ((password == null) ? "" : password)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_EMAIL, ((userEmail == null) ? "" : userEmail)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_REG_COUNTRY, ((country == null) ? "" : country)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.API_BASE_ADDRESS);
                JSONStatusResponse status_response = null;
                try
                {
                    HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.API_LOGIN_REGISTER, post_parameters);
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                }
                catch (HttpRequestException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.HTTPRequestFailed;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.JSONSerializationFailed;
                }
                catch (Exception)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.UnknownError;
                }
                return status_response;
            }
        }


        public static async Task<JSONStatusResponse> AsyncUploadProject(string projectTitle, string username, string token,
                string language = "en")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_USERNAME, ((username == null) ? "" : username)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_TOKEN, ((token == null) ? "" : token)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            using (MultipartFormDataContent post_parameters = new MultipartFormDataContent())
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    JSONStatusResponse status_response = null;
                    try
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
                            http_response.EnsureSuccessStatusCode();
                            string json_result = await http_response.Content.ReadAsStringAsync();

                            status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                        }
                        _uploadCounter--;
                    }
                    catch (HttpRequestException)
                    {
                        status_response = new JSONStatusResponse();
                        status_response.statusCode = StatusCodes.HTTPRequestFailed;
                    }
                    catch (Newtonsoft.Json.JsonSerializationException)
                    {
                        status_response = new JSONStatusResponse();
                        status_response.statusCode = StatusCodes.JSONSerializationFailed;
                    }
                    catch (Exception)
                    {
                        status_response = new JSONStatusResponse();
                        status_response.statusCode = StatusCodes.UnknownError;
                    }
                    return status_response;
                }
            }
        }


        public static async Task<JSONStatusResponse> AsyncReportAsInappropriate(string projectId, string flagReason, string language = "en")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_PROJECTID, ((projectId == null) ? "" : projectId)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_FLAG_REASON, ((flagReason == null) ? "" : flagReason)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS);
                //http_client.BaseAddress = new Uri("https://catroid-test.catrob.at");
                JSONStatusResponse status_response = null;
                try
                {
                    HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.CATROWEB_REPORT_AS_INAPPROPRIATE, post_parameters);
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                }
                catch (HttpRequestException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.HTTPRequestFailed;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.JSONSerializationFailed;
                }
                catch (Exception)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.UnknownError;
                }
                return status_response;
            }
        }


        public static async Task<JSONStatusResponse> AsyncRecoverPassword(string recoveryUserData, string language = "en")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_RECOVER_PWD, ((recoveryUserData == null) ? "" : recoveryUserData)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS);
                //http_client.BaseAddress = new Uri("https://catroid-test.catrob.at");
                JSONStatusResponse status_response = null;
                try
                {
                    HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.CATROWEB_RECOVER_PWD, post_parameters);
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                }
                catch (HttpRequestException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.HTTPRequestFailed;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.JSONSerializationFailed;
                }
                catch (Exception)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.UnknownError;
                }
                return status_response;
            }
        }


        public static async Task<JSONStatusResponse> AsyncChangePassword(string newPassword, string newPasswortRepeated, string hash, string language = "en")
        {
            var parameters = new List<KeyValuePair<string, string>>() { 
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_HASH, ((hash == null) ? "" : hash)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_NEW_PWD, ((newPassword == null) ? "" : newPassword)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_NEW_PWD_REPEAT, ((newPasswortRepeated == null) ? "" : newPasswortRepeated)),
                new KeyValuePair<string, string>(ApplicationResources.API_PARAM_LANGUAGE, ((language == null) ? "" : language))
            };

            HttpContent post_parameters = new FormUrlEncodedContent(parameters);
            using (var http_client = new HttpClient())
            {
                http_client.BaseAddress = new Uri(ApplicationResources.POCEKTCODE_BASE_ADDRESS);
                //http_client.BaseAddress = new Uri("https://catroid-test.catrob.at");
                JSONStatusResponse status_response = null;
                try
                {
                    HttpResponseMessage http_response = await http_client.PostAsync(ApplicationResources.CATROWEB_CHANGE_PWD, post_parameters);
                    http_response.EnsureSuccessStatusCode();

                    string json_result = await http_response.Content.ReadAsStringAsync();
                    status_response = JsonConvert.DeserializeObject<JSONStatusResponse>(json_result);
                }
                catch (HttpRequestException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.HTTPRequestFailed;
                }
                catch (Newtonsoft.Json.JsonSerializationException)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.JSONSerializationFailed;
                }
                catch (Exception)
                {
                    status_response = new JSONStatusResponse();
                    status_response.statusCode = StatusCodes.UnknownError;
                }
                return status_response;
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