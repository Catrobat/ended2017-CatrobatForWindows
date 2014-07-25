using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Catrobat.IDE.Core.CatrobatObjects;
using Catrobat.IDE.Core.Xml.VersionConverter;
using Catrobat.IDE.Core.Utilities.JSON;


namespace Catrobat.IDE.Core.Services
{
    public interface IWebCommunicationService
    {
        Task<List<OnlineProjectHeader>> AsyncLoadOnlineProjects(string filterText, int offset, int count, CancellationToken taskCancellationToken);

        Task<CatrobatVersionConverter.VersionConverterError> AsyncDownloadAndSaveProject(string downloadUrl, string projectName);

        Task<JSONStatusResponse> AsyncCheckToken(string username, string token, string language = "en");

        Task<JSONStatusResponse> AsyncLoginOrRegister(string username, string password, string userEmail, string language = "en", string country = "AT");

        Task<JSONStatusResponse> AsyncUploadProject(string projectTitle, string username, string token, string language = "en");

        Task<JSONStatusResponse> AsyncReportAsInappropriate(string projectId, string flagReason, string language = "en");

        Task<JSONStatusResponse> AsyncRecoverPassword(string recoveryUserData, string language = "en");

        Task<JSONStatusResponse> AsyncChangePassword(string newPassword, string newPasswortRepeated, string hash, string language = "en");

        bool NoUploadsPending();

        DateTime ConvertUnixTimeStamp(double timestamp);
    }
}
