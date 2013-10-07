using Catrobat.Core.Services.Common;

namespace Catrobat.Core.Services
{
    public interface IServerCommunicationService
    {
        void LoadOnlineProjects(bool append, string filterText, int offset, CatrobatWebCommunicationService.LoadOnlineProjectsEvent callback);

        void DownloadAndSaveProject(string downloadUrl, string projectName, CatrobatWebCommunicationService.DownloadAndSaveProjectEvent callback);
    }
}