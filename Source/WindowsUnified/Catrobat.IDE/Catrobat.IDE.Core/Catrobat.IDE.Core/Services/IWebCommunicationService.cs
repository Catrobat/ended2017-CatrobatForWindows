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
        Task<List<OnlineProgramHeader>> LoadOnlineProjectsAsync(string filterText, int offset, int count, CancellationToken taskCancellationToken);

        Task<Stream> DownloadAsync(string downloadUrl, string projectName);

        Task DownloadAsyncAlternativ(string downloadUrl, string projectName);

        Task<JSONStatusResponse> CheckTokenAsync(string username, string token, string language = "en");

        Task<JSONStatusResponse> LoginOrRegisterAsync(string username, string password, string userEmail, string language = "en", string country = "AT");

        Task<JSONStatusResponse> UploadProjectAsync(string projectTitle, string username, string token, string language = "en");

        Task<JSONStatusResponse> ReportAsInappropriateAsync(string projectId, string flagReason, string language = "en");

        Task<JSONStatusResponse> RecoverPasswordAsync(string recoveryUserData, string language = "en");

        Task<JSONStatusResponse> ChangePasswordAsync(string newPassword, string newPasswortRepeated, string hash, string language = "en");

        bool NoUploadsPending();

        DateTime ConvertUnixTimeStamp(double timestamp);
    }
}
